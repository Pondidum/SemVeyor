using SemVeyor.AssemblyScanning;
using SemVeyor.AssemblyScanning.Events;
using System;
using System.Linq;
using System.Reflection;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.AssemblyScanning
{
	public class ArgumentDetailsTests
	{
		private void OneArgument(string test) {}
		private void ActionArguments(Action<int> test) {}
		private void ParamsArgument(int[] test) {}

		private static ArgumentDetails For(string methodName)
		{
			return typeof(ArgumentDetailsTests)
				.GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic)
				.GetParameters()
				.Select(ArgumentDetails.From)
				.First();
		}

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

		private static ArgumentDetails From<T>(string name)
		{
			return new ArgumentDetails
			{
				Name = name,
				Type = typeof(T)
			};
		}
	}
}
