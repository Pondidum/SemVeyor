using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SemVeyor.AssemblyScanning.Events;
using SemVeyor.Infrastructure;

namespace SemVeyor.AssemblyScanning
{
	public class TypeDetails : IMemberDetails
	{
		private const BindingFlags ExternalVisibleFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

		public IEnumerable<GenericArgumentDetails> GenericArguments { get; set; }
		public IEnumerable<PropertyDetails> Properties { get; set; }
		public IEnumerable<MethodDetails> Methods { get; set; }
		public IEnumerable<FieldDetails> Fields { get; set; }
		public IEnumerable<CtorDetails> Constructors { get; set; }

		public Visibility Visibility { get; set; }
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
				Visibility = type.GetVisibility(),
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

		public IEnumerable<object> UpdatedTo(TypeDetails second)
		{
			var fieldComparer = new LambdaComparer<FieldDetails>(fd => fd.Name);

			var removedFields = Fields.Except(second.Fields, fieldComparer);
			var addedFields = second.Fields.Except(Fields, fieldComparer);
			var remainingFields = Fields.Concat(second.Fields).GroupBy(fd => fd.Name);

			foreach (var field in removedFields)
				yield return new FieldRemoved();

			foreach (var field in addedFields)
				yield return new FieldAdded();

			foreach (var pair in remainingFields)
			foreach (var @event in pair.First().UpdatedTo(pair.Last()))
				yield return @event;

			var methodComparer = new LambdaComparer<MethodDetails>(md => md.Name);

			var removedMethods = Methods.Except(second.Methods, methodComparer);
			var addedMethods = second.Methods.Except(Methods, methodComparer);
			var remainingMethods = Methods.Concat(second.Methods).GroupBy(fd => fd.Name);

			foreach (var method in removedMethods)
				yield return new MethodRemoved();

			foreach (var method in addedMethods)
				yield return new MethodAdded();

			foreach (var pair in remainingMethods)
			foreach (var @event in pair.First().UpdatedTo(pair.Last()))
				yield return @event;
		}
	}
}
