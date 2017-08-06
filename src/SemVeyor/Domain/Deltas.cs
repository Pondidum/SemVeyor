using System;
using System.Collections.Generic;
using System.Linq;

namespace SemVeyor.Domain
{
	public class Deltas
	{
		public static IEnumerable<object> ForCollections<T>(
			List<T> older,
			List<T> newer,
			IEqualityComparer<T> comparer,
			Func<T, object> onAdded,
			Func<T, object> onRemoved)
			where T : class, IDeltaProducer<T>
		{
			var namesInBoth = older.Intersect(newer, comparer).Select(md => md.Name).ToArray();

			foreach (var name in namesInBoth)
			{
				var olderGroup = older.Where(o => o.Name == name).ToArray();
				var newerGroup = newer.Where(n => n.Name == name).ToArray();

				var olderCount = olderGroup.Count();
				var newerCount = newerGroup.Count();

				var group = olderGroup
					.SelectMany(o => newerGroup.Select(n => new Link<T>(o, n)))
					.OrderBy(link => link.Changes.Count())
					.ToList();

				while (olderCount >= 1 && newerCount >= 1)
				{
					var best = group.First();

					foreach (var change in best.Changes)
						yield return change;

					older.Remove(best.Older);
					newer.Remove(best.Newer);

					group.RemoveAll(link => link.Older == best.Older || link.Newer == best.Newer);
					olderCount--;
					newerCount--;
				}
			}

			foreach (var om in older)
				yield return onRemoved(om);

			foreach (var nm in newer)
				yield return onAdded(nm);
		}

		private class Link<T> where T: IDeltaProducer<T>
		{
			public T Older { get; }
			public T Newer { get; }
			public IEnumerable<object> Changes { get; }

			public Link(T older, T newer)
			{
				Older = older;
				Newer = newer;
				Changes = older.UpdatedTo(Newer).ToArray();
			}
		}
	}
}
