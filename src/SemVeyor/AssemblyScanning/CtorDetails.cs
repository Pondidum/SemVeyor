using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SemVeyor.AssemblyScanning.Events;
using SemVeyor.Infrastructure;

namespace SemVeyor.AssemblyScanning
{
	public class CtorDetails : IMemberDetails, IDeltaProducer<CtorDetails>
	{
		public string Name { get; set; }
		public Visibility Visibility { get; set; }
		public IEnumerable<ParameterDetails> Parameters { get; set; }

		public static CtorDetails From(ConstructorInfo ctor)
		{
			return new CtorDetails
			{
				Name = ctor.Name,
				Visibility = ctor.GetVisibility(),
				Parameters = ctor.GetParameters().Select(ParameterDetails.From).ToArray()
			};

		}

		public IEnumerable<object> UpdatedTo(CtorDetails newer)
		{
			if (Visibility > newer.Visibility)
				yield return new CtorVisibilityDecreased();

			if (Visibility < newer.Visibility)
				yield return new CtorVisibilityIncreased();

			var changes = Deltas.ForCollections(
				Parameters.ToList(),
				newer.Parameters.ToList(),
				new LambdaComparer<ParameterDetails>(ad => ad.Name),
				x => new CtorArgumentAdded(),
				x => new CtorArgumentRemoved());

			foreach (var change in changes)
				yield return change;
		}
	}
}
