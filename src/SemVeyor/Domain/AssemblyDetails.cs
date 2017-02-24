using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SemVeyor.Domain.Events;
using SemVeyor.Infrastructure;

namespace SemVeyor.Domain
{
	public class AssemblyDetails : IDeltaProducer<AssemblyDetails>
	{
		public string Name { get; set; }
		public IEnumerable<TypeDetails> Types { get; set; }

		public static AssemblyDetails From(Assembly assembly)
		{
			return new AssemblyDetails
			{
				Name = assembly.FullName,
				Types = assembly.GetExportedTypes().Select(TypeDetails.From).ToArray()
			};
		}

		public IEnumerable<object> UpdatedTo(AssemblyDetails newer)
		{
			var changes = Deltas.ForCollections(
				Types.ToList(),
				newer.Types.ToList(),
				new LambdaComparer<TypeDetails>(t => t.FullName),
				x => new AssemblyTypeAdded(),
				x => new AssemblyTypeRemoved());

			foreach (var change in changes)
				yield return change;
		}
	}
}
