using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SemVeyor.AssemblyScanning.Events;
using SemVeyor.Infrastructure;

namespace SemVeyor.AssemblyScanning
{
	public class MethodDetails : IMemberDetails, IDeltaProducer<MethodDetails>
	{
		public string Name { get; set; }
		public Visibility Visibility { get; set; }
		public Type Type { get; set; }
		public IEnumerable<ArgumentDetails> Arguments { get; set; }
		public IEnumerable<GenericArgumentDetails> GenericArguments { get; set; }

		public static MethodDetails From(MethodInfo method)
		{
			return new MethodDetails
			{
				Name = method.Name,
				Visibility = method.GetVisibility(),
				Type = method.ReturnType,
				Arguments = method.GetParameters().Select(ArgumentDetails.From).ToArray(),
				GenericArguments = method.GetGenericArguments().Select(GenericArgumentDetails.From).ToArray()
			};
		}

		public IEnumerable<object> UpdatedTo(MethodDetails second)
		{
			if (Name != second.Name)
				yield return new MethodNameChanged();

			if (Visibility < second.Visibility)
				yield return new MethodVisibilityIncreased();

			if (Visibility > second.Visibility)
				yield return new MethodVisibilityDecreased();

			if (Type != second.Type)
				yield return new MethodTypeChanged();

			var paramChanges = Deltas.ForCollections(
				Arguments.ToList(),
				second.Arguments.ToList(),
				new LambdaComparer<ArgumentDetails>(x => x.Name),
				x => new MethodArgumentAdded(),
				x => new MethodArgumentRemoved());

			foreach (var change in paramChanges)
				yield return change;

			var genericChanges = Deltas.ForCollections(
				GenericArguments.ToList(),
				second.GenericArguments.ToList(),
				new LambdaComparer<GenericArgumentDetails>(ga => ga.Position),
				x => new MethodGenericArgumentAdded(),
				x => new MethodGenericArgumentRemoved());

			foreach (var change in genericChanges)
				yield return change;

		}
	}
}
