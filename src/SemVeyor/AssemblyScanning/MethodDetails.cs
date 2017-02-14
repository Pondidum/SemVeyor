using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
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
			if (Visibility < second.Visibility)
				yield return new MethodVisibilityIncreased();

			if (Visibility > second.Visibility)
				yield return new MethodVisibilityDecreased();

			if (Type != second.Type)
				yield return new MethodTypeChanged();

			var paramComparer = new LambdaComparer<ArgumentDetails>(x => x.Name);
			var paramAdded = second.Arguments.Except(Arguments, paramComparer);
			var paramRemoved = Arguments.Except(second.Arguments, paramComparer);

			foreach (var arg in paramAdded)
				yield return new MethodArgumentAdded();

			foreach (var arg in paramRemoved)
				yield return new MethodArgumentRemoved();

			var paramMatching = Arguments.Join(second.Arguments, a => a.Name, a => a.Name, (o, n) => new { Older = o, Newer = n });

			foreach (var pair in paramMatching)
			foreach (var change in pair.Older.UpdatedTo(pair.Newer))
				yield return change;


			var genericsComparer = new LambdaComparer<GenericArgumentDetails>(x => x.Position);
			var genericsAdded = second.GenericArguments.Except(GenericArguments, genericsComparer).ToArray();
			var genericsRemoved = GenericArguments.Except(second.GenericArguments, genericsComparer).ToArray();

			foreach (var arg in genericsAdded)
				yield return new MethodGenericArgumentAdded();

			foreach (var arg in genericsRemoved)
				yield return new MethodGenericArgumentRemoved();

			var genericsMatching = GenericArguments.Join(second.GenericArguments, a => a.Name, a => a.Name, (o, n) => new { Older = o, Newer = n });

			foreach (var pair in genericsMatching)
			foreach (var change in pair.Older.UpdatedTo(pair.Newer))
				yield return change;

		}
	}
}
