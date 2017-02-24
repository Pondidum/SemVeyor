using System;
using System.Linq;
using System.Reflection;
using SemVeyor.Domain;
using SemVeyor.Domain.Events;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Domain
{
	public class ParameterDetailsTests
	{
		private void OneArgument(string test) {}
		private void ActionArguments(Action<int> test) {}
		private void ParamsArgument(int[] test) {}
		private void TwoArguments(int first, string second) {}

		private static ParameterDetails For(string methodName)
		{
			return typeof(ParameterDetailsTests)
				.GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic)
				.GetParameters()
				.Select(ParameterDetails.From)
				.Last();
		}

		[Fact]
		public void The_position_is_populated() => For(nameof(TwoArguments)).Position.ShouldBe(1);

		[Fact]
		public void The_name_is_populated() => For(nameof(OneArgument)).Name.ShouldBe("test");

		[Fact]
		public void The_type_is_populated() => For(nameof(OneArgument)).Type.ShouldBe(typeof(string));

		[Fact]
		public void Generic_types_are_handled() => For(nameof(ActionArguments)).Type.ShouldBe(typeof(Action<int>));

		[Fact]
		public void Params_types_are_handled() => For(nameof(ParamsArgument)).Type.ShouldBe(typeof(int[]));

		[Fact]
		public void When_an_argument_has_not_changed()
		{
			var older = From<int>("first");
			var newer = From<int>("first");

			var changes = older.UpdatedTo(newer);

			changes.ShouldBeEmpty();
		}

		[Fact]
		public void When_an_argument_has_changed_name()
		{
			var older = From<int>("first");
			var newer = From<int>("second");

			var changes = older.UpdatedTo(newer);

			changes.Select(c => c.GetType()).ShouldBe(new []
			{
				typeof(ArgumentNameChanged)
			});
		}

		[Fact]
		public void When_an_argument_has_changed_type()
		{
			var older = From<int>("first");
			var newer = From<string>("first");

			var changes = older.UpdatedTo(newer);

			changes.Select(c => c.GetType()).ShouldBe(new []
			{
				typeof(ArgumentTypeChanged)
			});
		}

		[Fact]
		public void When_an_argument_has_changed_position()
		{
			var older = From<int>("first");
			var newer = From<int>("first", 1);

			var changes = older.UpdatedTo(newer);

			changes.Select(c => c.GetType()).ShouldBe(new []
			{
				typeof(ArgumentMoved)
			});
		}

		private static ParameterDetails From<T>(string name, int position = 0)
		{
			return new ParameterDetails
			{
				Name = name,
				Type = typeof(T),
				Position = position
			};
		}
	}
}
