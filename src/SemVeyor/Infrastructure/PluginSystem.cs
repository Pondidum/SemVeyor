using System;
using System.Collections.Generic;
using System.Linq;
using SemVeyor.CommandLine;

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

		public T Build<T>(Type type, Options options)
		{
			if (typeof(T).IsAssignableFrom(type) == false)
				throw new ArgumentException($"{type.FullName} cannot be cast to {typeof(T).FullName}", nameof(type));

			var constructors = type.GetConstructors();

			if (constructors.Any() == false)
				throw new MissingMethodException(type.FullName, "ctor");

			var blank = constructors.FirstOrDefault(c => c.GetParameters().Length == 0);

			if (blank != null)
				return (T)blank.Invoke(new object[0] { });

			var optionsOnly = constructors
				.FirstOrDefault(c => c.GetParameters().Length == 1 && c.GetParameters().Single().ParameterType == typeof(Options));

			if (optionsOnly != null)
				return (T)optionsOnly.Invoke(new object[] { options });

			throw new NotImplementedException();
		}
	}
}
