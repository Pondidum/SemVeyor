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
		public IEnumerable<CtorDetails> Constructors { get; set; }

		public string Name { get; set; }
		public string Namespace { get; set; }
		public string FullName => $"{Namespace}.{Name}";

		public static TypeContent From(Type type)
		{
			return new TypeContent
			{
				Name = type.Name,
				Namespace = type.Namespace,

				Constructors = ConstructorsFor(type),
				Properties = PropertiesFor(type),
				Methods =  MethodsFor(type)
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

		private static IEnumerable<string> MethodsFor(IReflect type)
		{
			var protectedMethods = type
				.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
				.Where(c => c.IsFamily)
				.Select(m => m.Name);

			var publicMethods = type
				.GetMethods(BindingFlags.Public | BindingFlags.Instance)
				.Select(m => m.Name);

			var objectProtectedMethods = typeof(object)
				.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
				.Where(c => c.IsFamily)
				.Select(m => m.Name);

			return publicMethods
				.Concat(protectedMethods)
				.Except(objectProtectedMethods)
				.Distinct(StringComparer.OrdinalIgnoreCase);
		}

		private static IEnumerable<CtorDetails> ConstructorsFor(Type type)
		{
			var publicCtors = type
				.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
				.Select(ctor => new CtorDetails { Visibility = Visbility.Public});

			var protectedCtors = type
				.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
				.Where(c => c.IsFamily)
				.Select(ctor => new CtorDetails { Visibility = Visbility.Protected});

			return publicCtors
				.Concat(protectedCtors);
		}
	}

	public class CtorDetails
	{
		public IEnumerable<ArgumentModel> Arguments { get; set; }
		public Visbility Visibility { get; set; }
	}

	public class ArgumentModel
	{
		public Type Type { get; set; }
		public string Name { get; set; }
	}

	public enum Visbility
	{
		Private,
		Protected,
		Internal,
		Public
	}
}
