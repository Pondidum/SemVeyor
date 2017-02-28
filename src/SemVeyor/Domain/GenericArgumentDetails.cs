using System;
using System.Collections.Generic;
using System.Linq;
using SemVeyor.Domain.Events;

namespace SemVeyor.Domain
{
	public class GenericArgumentDetails : IDeltaProducer<GenericArgumentDetails>
	{
		public int Position { get; set; }
		public string Name { get; set; }
		public IEnumerable<string> Constraints { get; set; }

		public static GenericArgumentDetails From(Type type)
		{
			return new GenericArgumentDetails
			{
				Name = type.Name,
				Position = type.GenericParameterPosition,
				Constraints =  type.GetGenericParameterConstraints().Select(c => c.Name)
			};
		}

		public IEnumerable<object> UpdatedTo(GenericArgumentDetails newer)
		{
			if (Position != newer.Position)
				yield return new GenericArgumentPositionChanged(this, newer);

			if (Name != newer.Name)
				yield return new GenericArgumentNameChanged(this, newer);

			var addedConstraints = newer.Constraints.Except(Constraints);
			var removedConstraints = Constraints.Except(newer.Constraints);

			foreach (var constraint in addedConstraints)
				yield return new GenericArgumentConstraintAdded(constraint);

			foreach (var constraint in removedConstraints)
				yield return new GenericArgumentConstraintRemoved(constraint);
		}

		public override string ToString()
		{
			return $"[{Position}]{Name}{{{string.Join(", ", Constraints)}}}";
		}
	}
}
