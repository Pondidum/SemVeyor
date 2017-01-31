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
				.Select(prop => new PropertyDetails(prop));

			var publicProperties = type
				.GetProperties(BindingFlags.Public | BindingFlags.Instance)
				.Select(prop => new PropertyDetails(prop));

			return publicProperties
				.Concat(protectedProperties);
		}

		private static IEnumerable<MethodDetails> MethodsFor(IReflect type)
		{
			var protectedMethods = type
				.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
				.Where(c => c.IsFamily)
				.Select(met => new MethodDetails(met));

			var publicMethods = type
				.GetMethods(BindingFlags.Public | BindingFlags.Instance)
				.Select(met => new MethodDetails(met));

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
				.Select(ctor => new CtorDetails(ctor));

			var protectedCtors = type
				.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
				.Where(c => c.IsFamily)
				.Select(ctor => new CtorDetails(ctor));

			return publicCtors
				.Concat(protectedCtors);
		}

		private static IEnumerable<FieldDetails> FieldsFor(Type type)
		{
			var publicFields = type
				.GetFields(BindingFlags.Public | BindingFlags.Instance)
				.Select(field => new FieldDetails(field));

			var protectedFields = type
				.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
				.Where(c => c.IsFamily)
				.Select(field => new FieldDetails(field));

			return publicFields
				.Concat(protectedFields);
		}
	}

	public class MemberDetails
	{
		public Visibility Visibility => VisibilityFromType(_info);
		public string Name { get; }

		private readonly MemberInfo _info;

		protected MemberDetails(MemberInfo info)
		{
			_info = info;
			Name = info.Name;
		}

		protected virtual Visibility VisibilityFromType(MemberInfo info)
		{
			var methodInfo = info as MethodBase;
			if (methodInfo != null)
				return VisibilityFromMethod(methodInfo);

			var field = info as FieldInfo;
			if (field != null)
				return VisibilityFromField(field);

			throw new NotSupportedException();
		}

		protected virtual Visibility VisibilityFromMethod(MethodBase method)
		{
			if (method.IsPublic)
				return Visibility.Public;

			if (method.IsFamily)
				return Visibility.Protected;

			if (method.IsAssembly)
				return Visibility.Internal;

			return Visibility.Private;
		}

		protected virtual Visibility VisibilityFromField(FieldInfo field)
		{
			if (field.IsPublic)
				return Visibility.Public;

			if (field.IsFamily)
				return Visibility.Protected;

			if (field.IsAssembly)
				return Visibility.Internal;

			return Visibility.Private;
		}
	}

	public class CtorDetails : MemberDetails
	{
		//public IEnumerable<ArgumentModel> Arguments { get; set; }

		public CtorDetails(ConstructorInfo ctor)
			: base(ctor)
		{
		}
	}

	public class FieldDetails : MemberDetails
	{
		public FieldDetails(FieldInfo info)
			: base(info)
		{
		}
	}

	public class PropertyDetails : MemberDetails
	{
		public PropertyDetails(PropertyInfo prop)
			: base(prop)
		{
		}

		protected override Visibility VisibilityFromType(MemberInfo info)
		{
			var prop = info as PropertyInfo;

			return base.VisibilityFromType(prop?.GetMethod ?? prop?.SetMethod);
		}
	}

	public class MethodDetails : MemberDetails
	{
		public MethodDetails(MethodBase method)
			: base(method)
		{
		}
	}

	public class ArgumentModel
	{
		public Type Type { get; set; }
		public string Name { get; set; }
	}

	public enum Visibility
	{
		Private,
		Internal,
		Protected,
		Public
	}
}