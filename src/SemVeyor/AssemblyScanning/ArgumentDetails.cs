using System;
using System.Collections.Generic;
using System.Reflection;
using SemVeyor.AssemblyScanning.Events;

namespace SemVeyor.AssemblyScanning
{
	public class ArgumentDetails
	{
		public int Position { get; set; }
		public Type Type { get; set; }
		public string Name { get; set; }

		public static ArgumentDetails From(ParameterInfo parameter)
		{
			return new ArgumentDetails
			{
				Position =  parameter.Position,
				Name = parameter.Name,
				Type = parameter.ParameterType
			};
		}

		public IEnumerable<object> UpdatedTo(ArgumentDetails newer)
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
