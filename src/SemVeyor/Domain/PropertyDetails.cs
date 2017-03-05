using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SemVeyor.Domain.Events;
using SemVeyor.Infrastructure;

namespace SemVeyor.Domain
{
	public class PropertyDetails : MemberDetails, IDeltaProducer<PropertyDetails>
	{
		public override string Name { get; set; }
		public override Visibility Visibility { get; set; }
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
				yield return new PropertyVisibilityDecreased(this, newer);

			if (Visibility < newer.Visibility)
				yield return new PropertyVisibilityIncreased(this, newer);

			if (SetterVisibility > newer.SetterVisibility || (SetterVisibility.HasValue && newer.SetterVisibility.HasValue == false))
				yield return new PropertyVisibilityDecreased(this, newer);

			if (SetterVisibility < newer.SetterVisibility || (SetterVisibility.HasValue == false && newer.SetterVisibility.HasValue))
				yield return new PropertyVisibilityIncreased(this, newer);

			if (Type != newer.Type)
				yield return new PropertyTypeChanged(this, newer);

			var paramChanges = Deltas.ForCollections(
				Parameters.ToList(),
				newer.Parameters.ToList(),
				new LambdaComparer<ParameterDetails>(x => x.Name),
				x => new PropertyArgumentAdded(this, newer),
				x => new PropertyArgumentRemoved(this, newer));

			foreach (var change in paramChanges)
				yield return change;
		}

		public override string ToString()
		{
			var setter = string.Empty;

			if (SetterVisibility.HasValue)
				setter = SetterVisibility == Visibility
					? "set; "
					: $"{SetterVisibility} set; ";

			var indexer = string.Empty;

			if (Parameters.Any())
				indexer = "[" + string.Join(", ", Parameters) + "]";

			return $"{Visibility} {Type} {Name}{indexer} {{ get; {setter}}}";
		}
	}
}
