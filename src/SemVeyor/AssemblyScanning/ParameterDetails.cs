using System;
using System.Collections.Generic;
using System.Reflection;
using SemVeyor.AssemblyScanning.Events;

namespace SemVeyor.AssemblyScanning
{
	public class ParameterDetails : IDeltaProducer<ParameterDetails>
	{
		public int Position { get; set; }
		public Type Type { get; set; }
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
				yield return new ArgumentNameChanged();

			if (Type != newer.Type)
				yield return new ArgumentTypeChanged();

			if (Position != newer.Position)
				yield return new ArgumentMoved();
		}
	}
}
