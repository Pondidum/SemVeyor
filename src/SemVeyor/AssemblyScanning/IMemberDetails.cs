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
		IEnumerable<object> UpdatedTo(T newer);
	}

	public class Deltas
	{
		public static IEnumerable<object> ForCollections<T>(
			IEnumerable<T> older,
			IEnumerable<T> newer,
			IEqualityComparer<T> comparer,
			Func<T, object> onAdded,
			Func<T, object> onRemoved)
			where T : IMemberDetails, IDeltaProducer<T>
		{
			var removedMethods = older.Except(newer, comparer);
			var addedMethods = newer.Except(older, comparer);
			var remainingMethods = older.Concat(newer).GroupBy(x => x.Name);


			foreach (var method in removedMethods)
				yield return onRemoved(method);

			foreach (var method in addedMethods)
				yield return onAdded(method);

			foreach (var pair in remainingMethods)
			foreach (var @event in pair.First().UpdatedTo(pair.Last()))
				yield return @event;
		}
	}
}
