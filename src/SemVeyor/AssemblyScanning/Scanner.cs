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
		public IEnumerable<PropertyDetails> Properties { get; set; }
		public IEnumerable<MethodDetails> Methods { get; set; }
		public IEnumerable<FieldDetails> Fields { get; set; }
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
				Methods = MethodsFor(type),
				Fields = FieldsFor(type)
			};
		}

		private static IEnumerable<PropertyDetails> PropertiesFor(IReflect type)
		{
			var protectedProperties = type
				.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance)
				.Where(c => c.GetMethod != null && c.GetMethod.IsFamily || c.SetMethod != null && c.SetMethod.IsFamily)
				.Select(prop => new PropertyDetails { Name = prop.Name, Visibility = Visbility.Protected });

			var publicProperties = type
				.GetProperties(BindingFlags.Public | BindingFlags.Instance)
				.Select(prop => new PropertyDetails { Name = prop.Name, Visibility = Visbility.Public });

			return publicProperties
				.Concat(protectedProperties);
		}

		private static IEnumerable<MethodDetails> MethodsFor(IReflect type)
		{
			var protectedMethods = type
				.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
				.Where(c => c.IsFamily)
				.Select(met => new MethodDetails { Name = met.Name, Visibility = Visbility.Protected });

			var publicMethods = type
				.GetMethods(BindingFlags.Public | BindingFlags.Instance)
				.Select(met => new MethodDetails { Name = met.Name, Visibility = Visbility.Public });

			var objectProtectedMethods = new HashSet<string>( typeof(object)
				.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
				.Where(c => c.IsFamily)
				.Select(met => met.Name));

			return publicMethods
				.Concat(protectedMethods)
				.Where(met => objectProtectedMethods.Contains(met.Name) == false);
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

		private static IEnumerable<FieldDetails> FieldsFor(Type type)
		{
			var publicFields = type
				.GetFields(BindingFlags.Public | BindingFlags.Instance)
				.Select(ctor => new FieldDetails { Visibility = Visbility.Public});

			var protectedFields = type
				.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
				.Where(c => c.IsFamily)
				.Select(ctor => new FieldDetails { Visibility = Visbility.Protected});

			return publicFields
				.Concat(protectedFields);
		}
	}

	public class MemberDetails
	{
		public Visbility Visibility { get; set; }
		public string Name { get; set; }
	}

	public class CtorDetails : MemberDetails
	{
		public IEnumerable<ArgumentModel> Arguments { get; set; }
	}

	public class FieldDetails: MemberDetails
	{
	}

	public class PropertyDetails  : MemberDetails
	{
	}

	public class MethodDetails: MemberDetails
	{
	}

	public class ArgumentModel
	{
		public Type Type { get; set; }
		public string Name { get; set; }
	}

	public enum Visbility
	{
		Private,
		Internal,
		Protected,
		Public
	}
}
