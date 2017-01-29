using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SemVeyor.AssemblyScanning
{
	public class Scanner
	{
		public IEnumerable<TypeContent> Scan(Assembly assembly)
		{
			return assembly.GetExportedTypes().Select(TypeContent.From);
		}
	}

	public class TypeContent
	{
		public IEnumerable<string> Properties { get; set; }
		public IEnumerable<string> Methods { get; set; }
		public IEnumerable<string> Fields { get; set; }
		public IEnumerable<string> Constructors { get; set; }

		public string Name { get; set; }
		public string Namespace { get; set; }
		public string FullName => $"{Namespace}.{Name}";

		public static TypeContent From(Type type)
		{
			return new TypeContent
			{
				Name = type.Name,
				Namespace = type.Namespace,

				Properties = PropertiesFor(type)
			};
		}

		private static IEnumerable<string> PropertiesFor(IReflect type)
		{
			var protectedProperties = type
				.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance)
				.Where(c => c.GetMethod != null && c.GetMethod.IsFamily || c.SetMethod != null && c.SetMethod.IsFamily)
				.Select(prop => prop.Name);

			var publicProperties = type
				.GetProperties(BindingFlags.Public | BindingFlags.Instance)
				.Select(prop => prop.Name);

			return publicProperties
				.Concat(protectedProperties)
				.Distinct(StringComparer.OrdinalIgnoreCase);
		}
	}
}
