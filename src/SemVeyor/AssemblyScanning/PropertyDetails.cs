using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SemVeyor.AssemblyScanning.Events;
using SemVeyor.Infrastructure;

namespace SemVeyor.AssemblyScanning
{
	public class PropertyDetails : IMemberDetails, IDeltaProducer<PropertyDetails>
	{
		public string Name { get; set; }
		public Visibility Visibility { get; set; }
		public Type Type { get; set; }
		public Visibility? SetterVisibility { get; set; }
		public IEnumerable<ParameterDetails> Parameters { get; set; }

		public static PropertyDetails From(PropertyInfo prop)
		{
			return new PropertyDetails
			{
				Name = prop.Name,
				Visibility = prop.GetVisibility(),
				Type = prop.PropertyType,
				Parameters = prop.GetIndexParameters().Select(ParameterDetails.From),
				SetterVisibility = prop.SetMethod != null
					? prop.SetMethod.GetVisibility()
					: (Visibility?)null
			};
		}

		public IEnumerable<object> UpdatedTo(PropertyDetails newer)
		{
			if (Visibility > newer.Visibility)
				yield return new PropertyVisibilityDecreased();

			if (Visibility < newer.Visibility)
				yield return new PropertyVisibilityIncreased();

			if (SetterVisibility > newer.SetterVisibility || (SetterVisibility.HasValue && newer.SetterVisibility.HasValue == false))
				yield return new PropertyVisibilityDecreased();

			if (SetterVisibility < newer.SetterVisibility || (SetterVisibility.HasValue == false && newer.SetterVisibility.HasValue))
				yield return new PropertyVisibilityIncreased();

			if (Type != newer.Type)
				yield return new PropertyTypeChanged();

			var paramChanges = Deltas.ForCollections(
				Parameters.ToList(),
				newer.Parameters.ToList(),
				new LambdaComparer<ParameterDetails>(x => x.Name),
				x => new PropertyArgumentAdded(),
				x => new PropertyArgumentRemoved());

			foreach (var change in paramChanges)
				yield return change;

		}
	}
}
