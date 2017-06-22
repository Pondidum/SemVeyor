using System;
using System.Collections.Generic;
using System.Linq;

namespace SemVeyor.Infrastructure
{
	public class PluginSystem
	{
		public IEnumerable<Type> DiscoverImplementations<T>()
		{
			var assembly = GetType().Assembly;

			return assembly
				.GetExportedTypes()
				.Where(t => t.IsAbstract == false && t.IsClass)
				.Where(t => t.GetInterfaces().Contains(typeof(T)));
		}
	}
}
