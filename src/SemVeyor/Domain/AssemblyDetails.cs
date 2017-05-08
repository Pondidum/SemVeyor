using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SemVeyor.Domain.Events;
using SemVeyor.Domain.Queries;
using SemVeyor.Infrastructure;

namespace SemVeyor.Domain
{
	public class AssemblyDetails : IDeltaProducer<AssemblyDetails>
	{
		public string Name { get; set; }
		public IEnumerable<TypeDetails> Types { get; set; }

		public AssemblyDetails()
		{
			Types = Enumerable.Empty<TypeDetails>();
		}

		public IEnumerable<object> UpdatedTo(AssemblyDetails newer)
		{
			var changes = Deltas.ForCollections(
				Types.ToList(),
				newer.Types.ToList(),
				new LambdaComparer<TypeDetails>(t => t.FullName),
				x => new AssemblyTypeAdded(x),
				x => new AssemblyTypeRemoved(x));

			foreach (var change in changes)
				yield return change;
		}
	}
}
