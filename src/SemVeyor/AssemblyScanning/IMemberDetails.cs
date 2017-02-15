using System;
using System.Collections.Generic;
using System.Linq;

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
	}
}
