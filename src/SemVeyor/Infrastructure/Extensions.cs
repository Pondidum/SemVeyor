using System.Collections.Generic;

namespace SemVeyor.Infrastructure
{
	public static class Extensions
	{
		public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, TValue defaultValue)
		{
			TValue value;

			return self.TryGetValue(key, out value)
				? value
				: defaultValue;
		}
	}
}