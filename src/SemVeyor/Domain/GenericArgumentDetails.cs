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

		public IEnumerable<object> UpdatedTo(GenericArgumentDetails second)
		{
			if (Position != second.Position)
				yield return new GenericArgumentPositionChanged();

			if (Name != second.Name)
				yield return new GenericArgumentNameChanged();

			var addedConstraints = second.Constraints.Except(Constraints);
			var removedConstraints = Constraints.Except(second.Constraints);

			foreach (var constraint in addedConstraints)
				yield return new GenericArgumentConstraintAdded();

			foreach (var constraint in removedConstraints)
				yield return new GenericArgumentConstraintRemoved();
		}
	}
}
