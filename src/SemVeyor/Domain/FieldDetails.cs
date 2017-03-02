using System;
using System.Collections.Generic;
using System.Reflection;
using SemVeyor.Domain.Events;

namespace SemVeyor.Domain
{
	public class FieldDetails : IMemberDetails, IDeltaProducer<FieldDetails>
	{
		public string Name { get; set; }
		public Visibility Visibility { get; set;}
		public Type Type { get; set;}

		public static FieldDetails From(FieldInfo info)
		{
			return new FieldDetails
			{
				Name = info.Name,
				Visibility = info.GetVisibility(),
				Type = info.FieldType
			};
		}

		public IEnumerable<object> UpdatedTo(FieldDetails newer)
		{
			if (Type != newer.Type)
				yield return new FieldTypeChanged(this, newer);

			if (Visibility > newer.Visibility)
				yield return new FieldVisibilityDecreased(this, newer);

			if (Visibility < newer.Visibility)
				yield return new FieldVisibilityIncreased(this, newer);
		}

		public override string ToString()
		{
			return $"{Visibility} {Type} {Name}";
		}
	}
}
