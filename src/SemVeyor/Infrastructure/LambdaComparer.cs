using System;
using System.Collections.Generic;

namespace SemVeyor.Infrastructure
{
	public class LambdaComparer<T> : IEqualityComparer<T>
	{
		private readonly Func<T, object> _selector;

		public LambdaComparer(Func<T, object> selector)
		{
			_selector = selector;
		}

		public bool Equals(T x, T y) => _selector(x).Equals(_selector(y));
		public int GetHashCode(T obj) => _selector(obj).GetHashCode();
	}
}