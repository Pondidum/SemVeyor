using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SemVeyor.AssemblyScanning
{
	public class TypeDetails
	{
		private const BindingFlags ExternalVisibleFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

		public IEnumerable<GenericArgumentDetails> GenericArguments { get; set; }
		public IEnumerable<PropertyDetails> Properties { get; set; }
		public IEnumerable<MethodDetails> Methods { get; set; }
		public IEnumerable<FieldDetails> Fields { get; set; }
		public IEnumerable<CtorDetails> Constructors { get; set; }

		public string Name { get; set; }
		public string Namespace { get; set; }
		public string FullName => $"{Namespace}.{Name}";

		public string BaseType { get; set; }
		public IEnumerable<string> Interfaces { get; set; }

		public static TypeDetails From(Type type)
		{
			return new TypeDetails
			{
				Name = type.Name,
				Namespace = type.Namespace,

				BaseType = type.BaseType?.Name,
				Interfaces = type.GetInterfaces().Select(i => i.Name),

				GenericArguments = GenericArgumentsFor(type),
				Constructors = ConstructorsFor(type),
				Properties = PropertiesFor(type),
				Methods = MethodsFor(type),
				Fields = FieldsFor(type)
			};
		}

		private static IEnumerable<GenericArgumentDetails> GenericArgumentsFor(Type type)
		{
			return type
				.GetGenericArguments()
				.Select(GenericArgumentDetails.From)
				.ToArray();
		}

		private static IEnumerable<PropertyDetails> PropertiesFor(IReflect type)
		{
			return type
				.GetProperties(ExternalVisibleFlags)
				.Select(PropertyDetails.From)
				.Where(IsExternal);
		}

		private static IEnumerable<MethodDetails> MethodsFor(IReflect type)
		{
			var methods = type
				.GetMethods(ExternalVisibleFlags)
				.Select(MethodDetails.From);

			var objectProtectedMethods = new HashSet<string>(typeof(object)
				.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
				.Where(c => c.IsFamily)
				.Select(met => met.Name));

			return methods
				.Where(met => objectProtectedMethods.Contains(met.Name) == false)
				.Where(IsExternal);
		}

		private static IEnumerable<CtorDetails> ConstructorsFor(Type type)
		{
			return type
				.GetConstructors(ExternalVisibleFlags)
				.Select(ctor => CtorDetails.From(ctor))
				.Where(IsExternal);
		}

		private static IEnumerable<FieldDetails> FieldsFor(Type type)
		{
			return type
				.GetFields(ExternalVisibleFlags)
				.Select(FieldDetails.From)
				.Where(IsExternal);
		}

		private static bool IsExternal(IMemberDetails info)
		{
			return info.Visibility > Visibility.Internal;
		}
	}
}