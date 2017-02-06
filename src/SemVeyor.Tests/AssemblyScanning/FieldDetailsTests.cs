﻿using System;
using System.Collections.Generic;
using System.Linq;
using SemVeyor.AssemblyScanning;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.AssemblyScanning
{
	public class FieldDetailsTests
	{
		public FieldDetailsTests()
		{
			var first = new FieldDetails { };
			var second = new FieldDetails { };


		}

		[Fact]
		public void When_checking_the_log()
		{
			var first = From<int>("first", Visibility.Public);
			var second = From<int>("first", Visibility.Public);

			var change = first.UpdatedTo(second);

			change.ShouldBeEmpty();
		}

		[Fact]
		public void When_the_types_are_different()
		{
			var first = From<int>("first", Visibility.Public);
			var second = From<string>("first", Visibility.Public);

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				typeof(TypeChanged),
			});
		}

		[Theory]
		[InlineData(Visibility.Public, Visibility.Protected, typeof(VisibilityDecreased))]
		[InlineData(Visibility.Public, Visibility.Internal, typeof(VisibilityDecreased))]
		[InlineData(Visibility.Public, Visibility.Private, typeof(VisibilityDecreased))]
		[InlineData(Visibility.Protected, Visibility.Public, typeof(VisibilityIncreased))]
		public void When_the_type_visibility_changes(Visibility older, Visibility newer, Type expectedEvent)
		{
			var first = From<int>("first", older);
			var second = From<int>("first", newer);

			var changes = first.UpdatedTo(second);

			changes.Select(c => c.GetType()).ShouldBe(new[]
			{
				expectedEvent,
			});
		}

		private static FieldDetails From<T>(string name, Visibility visibility)
		{
			return new FieldDetails
			{
				Name = name,
				Visibility = visibility,
				Type = typeof(T)
			};
		}
	}
}