using System;
using System.Collections.Generic;
using System.Reflection;
using SemVeyor.Domain.Events;

namespace SemVeyor.Domain
{
	public class ParameterDetails : IDeltaProducer<ParameterDetails>
	{
		public int Position { get; set; }
		public TypeName Type { get; set; }
		public string Name { get; set; }

		public static ParameterDetails From(ParameterInfo parameter)
		{
			return new ParameterDetails
			{
				Position =  parameter.Position,
				Name = parameter.Name,
				Type = parameter.ParameterType
			};
		}

		public IEnumerable<object> UpdatedTo(ParameterDetails newer)
		{
			if (Name != newer.Name)
				yield return new ParameterNameChanged(this, newer);

			if (Type != newer.Type)
				yield return new ParameterTypeChanged(this, newer);

			if (Position != newer.Position)
				yield return new ParameterMoved(this, newer);
		}

		public override string ToString() => $"{Type} {Name}";
	}
}
