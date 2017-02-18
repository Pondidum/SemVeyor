using System;
using System.Collections.Generic;
using System.Linq;
using SemVeyor.AssemblyScanning.Events;
using SemVeyor.Infrastructure;

namespace SemVeyor.AssemblyScanning
{
	public interface IMemberDetails
	{
		Visibility Visibility { get; }
		string Name { get; }
	}

	public interface IDeltaProducer<T>
	{
		string Name { get; }
		IEnumerable<object> UpdatedTo(T newer);
	}

	public class Deltas
	{
		public static IEnumerable<object> ForCollections<T>(
			T[] older,
			T[] newer,
			IEqualityComparer<T> comparer,
			Func<T, object> onAdded,
			Func<T, object> onRemoved)
			where T : IDeltaProducer<T>
		{
			var removed = older.Except(newer, comparer);
			var added = newer.Except(older, comparer);
			var remaining = older.Concat(newer).GroupBy(x => x.Name);


			foreach (var item in removed)
				yield return onRemoved(item);

			foreach (var item in added)
				yield return onAdded(item);

			foreach (var pair in remaining)
			foreach (var @event in pair.First().UpdatedTo(pair.Last()))
				yield return @event;
		}

		public static IEnumerable<object> ForCollections(
			MethodDetails[] older,
			MethodDetails[] newer,
			IEqualityComparer<MethodDetails> comparer,
			Func<MethodDetails, object> onAdded,
			Func<MethodDetails, object> onRemoved)
		{
			return ForCollections(older.ToList(), newer.ToList(), comparer, onAdded, onRemoved);
		}

		public static IEnumerable<object> ForCollections(
			List<MethodDetails> older,
			List<MethodDetails> newer,
			IEqualityComparer<MethodDetails> comparer,
			Func<MethodDetails, object> onAdded,
			Func<MethodDetails, object> onRemoved)
		{
			var namesInBoth = older.Select(o => o.Name).Intersect(newer.Select(n => n.Name)).ToArray();

			foreach (var name in namesInBoth)
			{
				while (older.Count(o => o.Name == name) >= 1 && newer.Count(n => n.Name == name) >= 1)
				{
					var olderGroup = older.Where(o => o.Name == name).ToArray();
					var newerGroup = newer.Where(n => n.Name == name).ToArray();

					var best = olderGroup
						.SelectMany(o => newerGroup.Select(n => new Link(o, n)))
						.OrderBy(link => link.Changes.Count())
						.First();

					foreach (var change in best.Changes)
						yield return change;

					older.Remove(best.Older);
					newer.Remove(best.Newer);
				}

			}

			foreach (var om in older)
				yield return new MethodRemoved();

			foreach (var nm in newer)
				yield return new MethodAdded();
		}

		private class Link
		{
			public MethodDetails Older { get; set; }
			public MethodDetails Newer { get; set; }
			public IEnumerable<object> Changes { get; }

			public Link(MethodDetails older, MethodDetails newer)
			{
				Older = older;
				Newer = newer;
				Changes = older.UpdatedTo(Newer).ToArray();
			}
		}
	}
}
