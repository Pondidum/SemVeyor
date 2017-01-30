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
				.Select(prop => new PropertyDetails(Visbility.Protected, prop.Name));

			var publicProperties = type
				.GetProperties(BindingFlags.Public | BindingFlags.Instance)
				.Select(prop => new PropertyDetails(Visbility.Public, prop.Name));

			return publicProperties
				.Concat(protectedProperties);
		}

		private static IEnumerable<MethodDetails> MethodsFor(IReflect type)
		{
			var protectedMethods = type
				.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
				.Where(c => c.IsFamily)
				.Select(met => new MethodDetails(Visbility.Protected, met.Name));

			var publicMethods = type
				.GetMethods(BindingFlags.Public | BindingFlags.Instance)
				.Select(met => new MethodDetails(Visbility.Public, met.Name));

			var objectProtectedMethods = new HashSet<string>(typeof(object)
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
				.Select(ctor => new CtorDetails(Visbility.Public, "ctor"));

			var protectedCtors = type
				.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
				.Where(c => c.IsFamily)
				.Select(ctor => new CtorDetails(Visbility.Protected, "ctor"));

			return publicCtors
				.Concat(protectedCtors);
		}

		private static IEnumerable<FieldDetails> FieldsFor(Type type)
		{
			var publicFields = type
				.GetFields(BindingFlags.Public | BindingFlags.Instance)
				.Select(field => new FieldDetails(Visbility.Public, field.Name));

			var protectedFields = type
				.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
				.Where(c => c.IsFamily)
				.Select(field => new FieldDetails(Visbility.Protected, field.Name));

			return publicFields
				.Concat(protectedFields);
		}
	}

	public class MemberDetails
	{
		public Visbility Visibility { get; }
		public string Name { get; }

		public MemberDetails(Visbility visibility, string name)
		{
			Visibility = visibility;
			Name = name;
		}
	}

	public class CtorDetails : MemberDetails
	{
		public IEnumerable<ArgumentModel> Arguments { get; set; }

		public CtorDetails(Visbility visibility, string name)
			: base(visibility, name)
		{
		}
	}

	public class FieldDetails : MemberDetails
	{
		public FieldDetails(Visbility visibility, string name)
			: base(visibility, name)
		{
		}
	}

	public class PropertyDetails : MemberDetails
	{
		public PropertyDetails(Visbility visibility, string name)
			: base(visibility, name)
		{
		}
	}

	public class MethodDetails : MemberDetails
	{
		public MethodDetails(Visbility visibility, string name)
			: base(visibility, name)
		{
		}
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