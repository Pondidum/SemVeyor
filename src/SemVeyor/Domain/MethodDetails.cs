using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SemVeyor.Domain.Events;
using SemVeyor.Infrastructure;

namespace SemVeyor.Domain
{
	public class MethodDetails : MemberDetails, IDeltaProducer<MethodDetails>
	{
		public override string Name { get; set; }
		public override Visibility Visibility { get; set; }
		public Type Type { get; set; }
		public IEnumerable<ParameterDetails> Parameters { get; set; }
		public IEnumerable<GenericArgumentDetails> GenericArguments { get; set; }

		public static MethodDetails From(MethodInfo method)
		{
			return new MethodDetails
			{
				Name = method.Name,
				Visibility = method.GetVisibility(),
				Type = method.ReturnType,
				Parameters = method.GetParameters().Select(ParameterDetails.From).ToArray(),
				GenericArguments = method.GetGenericArguments().Select(GenericArgumentDetails.From).ToArray()
			};
		}

		public IEnumerable<object> UpdatedTo(MethodDetails newer)
		{
			if (Name != newer.Name)
				yield return new MethodNameChanged(this, newer);

			if (Visibility < newer.Visibility)
				yield return new MethodVisibilityIncreased(this, newer);

			if (Visibility > newer.Visibility)
				yield return new MethodVisibilityDecreased(this, newer);

			if (Type != newer.Type)
				yield return new MethodTypeChanged(this, newer);

			var paramChanges = Deltas.ForCollections(
				Parameters.ToList(),
				newer.Parameters.ToList(),
				new LambdaComparer<ParameterDetails>(x => x.Name),
				x => new MethodArgumentAdded(this, newer),
				x => new MethodArgumentRemoved(this, newer));

			foreach (var change in paramChanges)
				yield return change;

			var genericChanges = Deltas.ForCollections(
				GenericArguments.ToList(),
				newer.GenericArguments.ToList(),
				new LambdaComparer<GenericArgumentDetails>(ga => ga.Position),
				x => new MethodGenericArgumentAdded(this, newer),
				x => new MethodGenericArgumentRemoved(this, newer));

			foreach (var change in genericChanges)
				yield return change;

		}

		public override string ToString()
		{
			var type = Type?.ToString() ?? "void";
			var parameters = string.Join(", ", Parameters);

			var generics = string.Empty;
			var constraints = string.Empty;

			if (GenericArguments.Any())
			{
				generics = "<" + string.Join(", ", GenericArguments.Select(a => a.Name)) + ">";

				constraints =  GenericArguments
					.Where(g => g.Constraints.Any())
					.Select(g => $" where {g.Name} : {string.Join(", ", g.Constraints)}")
					.Aggregate("", (a, cs) => a + cs);
			}


			return $"{Visibility} {type} {Name}{generics}({parameters}){constraints}";
		}
	}
}
