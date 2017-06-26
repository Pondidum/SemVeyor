using System;
using System.Collections.Generic;
using System.Linq;
using SemVeyor.CommandLine;
using SemVeyor.Config;

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

		public Type ForKey<T>(string key)
		{
			return DiscoverImplementations<T>()
				.Single(t => string.Equals(t.Name, key + BuildName<T>(), StringComparison.OrdinalIgnoreCase));
		}
		
		public T Build<T>(Type type, Options options, Func<Type, object> buildParameter)
		{
			if (typeof(T).IsAssignableFrom(type) == false)
				throw new ArgumentException($"{type.FullName} cannot be cast to {typeof(T).FullName}", nameof(type));

			var constructors = type.GetConstructors();

			if (constructors.Any() == false)
				throw new MissingMethodException(type.FullName, "ctor");

			var blank = constructors.FirstOrDefault(c => c.GetParameters().Length == 0);

			if (blank != null)
				return (T)blank.Invoke(new object[0] { });

			var ctor = constructors.First();
			
			var parameters = ctor
				.GetParameters()
				.Select(pi => pi.ParameterType == typeof(Options) ? options : buildParameter(pi.ParameterType))
				.ToArray();

			return (T)ctor.Invoke(parameters);
		}

		private static string BuildName<T>()
		{
			var type = typeof(T);

			return type.IsInterface
				? type.Name.Substring(1)
				: type.Name;
		}
	}
}
