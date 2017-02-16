using System;
using System.Collections.Generic;
using System.Linq;
using SemVeyor.AssemblyScanning.Events;

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
			var found = new Dictionary<string, Pair>();

			foreach (var method in older)
			{
				if (found.ContainsKey(method.Name) == false)
					found[method.Name] = new Pair();

				found[method.Name].Older.Add(method);
			}

			foreach (var method in newer)
			{
				if (found.ContainsKey(method.Name) == false)
					found[method.Name] = new Pair();

				found[method.Name].Newer.Add(method);
			}

			foreach (var pair in found.Values)
			{
				if (pair.Older.Count == 1 && pair.Newer.Count == 1)
				{
					var start = pair.Older.Single();
					var finish = pair.Newer.Single();

					foreach (var change in start.UpdatedTo(finish))
						yield return change;
				}
				else
				{
					for (var i = 0; i < pair.Older.Count; i++)
					{
						var o = pair.Older[i];

						for (var j = 0; j < pair.Newer.Count; j++)
						{
							var n = pair.Newer[j];

							if (n == null)
								continue;

							var potentialChanges = o.UpdatedTo(n).ToArray();

							if (potentialChanges.Any() == false)
							{
								pair.Older[i] = null;
								pair.Newer[j] = null;
								break;
							}
						}
					}

					foreach (var method in pair.Newer.Where(n => n != null))
						yield return new MethodAdded();

					foreach (var method in pair.Older.Where(o => o != null))
						yield return new MethodRemoved();
				}
			}
		}

		private class Pair
		{
			public List<MethodDetails> Older { get; set; } = new List<MethodDetails>();
			public List<MethodDetails> Newer { get; set; } = new List<MethodDetails>();
		}
	}
}
