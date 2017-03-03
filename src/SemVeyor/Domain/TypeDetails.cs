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
				.Where(m => m.IsSpecialName == false)
				.Select(MethodDetails.From);

			var objectProtectedMethods = new HashSet<string>(typeof(object)
				.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
				.Where(c => c.IsFamily)
				.Where(m => m.IsSpecialName == false)
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
				yield return new TypeVisibilityDecreased(Visibility, second.Visibility);

			if (Visibility < second.Visibility)
				yield return new TypeVisibilityIncreased(Visibility, second.Visibility);

			var fieldChanges = Deltas.ForCollections(
				Fields.ToList(),
				second.Fields.ToList(),
				new LambdaComparer<FieldDetails>(fd => fd.Name),
				f => new TypeFieldAdded(f),
				f => new TypeFieldRemoved(f));

			foreach (var change in fieldChanges)
				yield return change;

			var methodChanges = Deltas.ForCollections(
				Methods.ToList(),
				second.Methods.ToList(),
				new LambdaComparer<MethodDetails>(md => md.Name),
				m => new TypeMethodAdded(m),
				m => new TypeMethodRemoved(m));

			foreach (var change in methodChanges)
				yield return change;

			var propertyChanges = Deltas.ForCollections(
				Properties.ToList(),
				second.Properties.ToList(),
				new LambdaComparer<PropertyDetails>(pd => pd.Name),
				p => new TypePropertyAdded(p),
				p => new TypePropertyRemoved(p));

			foreach (var change in propertyChanges)
				yield return change;

			var ctorChanges = Deltas.ForCollections(
				Constructors.ToList(),
				second.Constructors.ToList(),
				new LambdaComparer<CtorDetails>(pd => pd.Name),
				c => new TypeCtorAdded(c),
				c => new TypeCtorRemoved(c));

			foreach (var change in ctorChanges)
				yield return change;

			var genericChanges = Deltas.ForCollections(
				GenericArguments.ToList(),
				second.GenericArguments.ToList(),
				new LambdaComparer<GenericArgumentDetails>(ga => ga.Position),
				g => new TypeGenericArgumentAdded(g),
				g => new TypeGenericArgumentRemoved(g));

			foreach (var change in genericChanges)
				yield return change;
		}
	}
}
