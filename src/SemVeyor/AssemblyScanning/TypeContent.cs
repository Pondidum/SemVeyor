using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Reflection;

namespace SemVeyor.AssemblyScanning
{
	public class TypeContent
	{
		private static BindingFlags ExternalVisibleFlags;

		static TypeContent()
		{
			ExternalVisibleFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
		}

		public IEnumerable<PropertyDetails> Properties { get; set; }
		public IEnumerable<MethodDetails> Methods { get; set; }
		public IEnumerable<FieldDetails> Fields { get; set; }
		public IEnumerable<CtorDetails> Constructors { get; set; }

		public string Name { get; set; }
		public string Namespace { get; set; }
		public string FullName => $"{Namespace}.{Name}";

		public string BaseType { get; set; }
		public IEnumerable<string> Interfaces { get; set; }

		public static TypeContent From(Type type)
		{
			return new TypeContent
			{
				Name = type.Name,
				Namespace = type.Namespace,

				BaseType = type.BaseType?.Name,
				Interfaces = type.GetInterfaces().Select(i => i.Name),

				Constructors = ConstructorsFor(type),
				Properties = PropertiesFor(type),
				Methods = MethodsFor(type),
				Fields = FieldsFor(type)
			};
		}

		private static IEnumerable<PropertyDetails> PropertiesFor(IReflect type)
		{
			return type
				.GetProperties(ExternalVisibleFlags)
				.Select(prop => new PropertyDetails(prop))
				.Where(IsExternal);
		}

		private static IEnumerable<MethodDetails> MethodsFor(IReflect type)
		{
			var methods = type
				.GetMethods(ExternalVisibleFlags)
				.Select(met => new MethodDetails(met));

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
				.Select(ctor => new CtorDetails(ctor))
				.Where(IsExternal);
		}

		private static IEnumerable<FieldDetails> FieldsFor(Type type)
		{
			return type
				.GetFields(ExternalVisibleFlags)
				.Select(field => new FieldDetails(field))
				.Where(IsExternal);
		}

		private static bool IsExternal(MemberDetails info)
		{
			return info.Visibility > Visibility.Internal;
		}
	}
}
