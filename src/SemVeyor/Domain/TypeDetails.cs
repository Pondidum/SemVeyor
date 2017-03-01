using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SemVeyor.Domain.Events;
using SemVeyor.Infrastructure;

namespace SemVeyor.Domain
{
	public class TypeDetails : IMemberDetails, IDeltaProducer<TypeDetails>
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
			if (Visibility > second.Visibility)
				yield return new TypeVisibilityDecreased();

			if (Visibility < second.Visibility)
				yield return new TypeVisibilityIncreased();

			var fieldChanges = Deltas.ForCollections(
				Fields.ToList(),
				second.Fields.ToList(),
				new LambdaComparer<FieldDetails>(fd => fd.Name),
				f => new FieldAdded(f),
				f => new FieldRemoved(f));

			foreach (var change in fieldChanges)
				yield return change;

			var methodChanges = Deltas.ForCollections(
				Methods.ToList(),
				second.Methods.ToList(),
				new LambdaComparer<MethodDetails>(md => md.Name),
				m => new MethodAdded(),
				m => new MethodRemoved());

			foreach (var change in methodChanges)
				yield return change;

			var propertyChanges = Deltas.ForCollections(
				Properties.ToList(),
				second.Properties.ToList(),
				new LambdaComparer<PropertyDetails>(pd => pd.Name),
				m => new PropertyAdded(),
				m => new PropertyRemoved());

			foreach (var change in propertyChanges)
				yield return change;

			var ctorChanges = Deltas.ForCollections(
				Constructors.ToList(),
				second.Constructors.ToList(),
				new LambdaComparer<CtorDetails>(pd => pd.Name),
				m => new CtorAdded(),
				m => new CtorRemoved());

			foreach (var change in ctorChanges)
				yield return change;

			var genericChanges = Deltas.ForCollections(
				GenericArguments.ToList(),
				second.GenericArguments.ToList(),
				new LambdaComparer<GenericArgumentDetails>(ga => ga.Position),
				m => new GenericArgumentAdded(),
				m => new GenericArgumentRemoved());

			foreach (var change in genericChanges)
				yield return change;
		}
	}
}