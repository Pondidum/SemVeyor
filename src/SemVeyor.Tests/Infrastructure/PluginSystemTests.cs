using System;
using SemVeyor.CommandLine;
using SemVeyor.Infrastructure;
using SemVeyor.Storage;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Infrastructure
{
	public class PluginSystemTests
	{
		private readonly PluginSystem _plugins;

		public PluginSystemTests()
		{
			_plugins = new PluginSystem();
		}

		[Fact]
		public void When_discovering_types()
		{
			_plugins
				.DiscoverImplementations<IStorage>()
				.ShouldHaveSingleItem()
				.ShouldBe(typeof(FileStore));
		}

		[Fact]
		public void When_building_a_type_not_castable()
		{
			Should.Throw<ArgumentException>(() => _plugins.Build<IStorage>(typeof(Guid), null));
		}

		[Fact]
		public void When_building_a_type_with_no_visible_constructors()
		{
			Should.Throw<MissingMethodException>(() => _plugins.Build<NoVisibleCtor>(typeof(NoVisibleCtor), null));
		}

		[Fact]
		public void When_building_a_type_with_a_parameterless_constructor()
		{
			_plugins
				.Build<ParameterlessCtor>(typeof(ParameterlessCtor), null)
				.ShouldBeOfType<ParameterlessCtor>();
		}

		[Fact]
		public void When_building_a_type_with_an_options_parameter()
		{
			var options = new Options();

			_plugins
				.Build<OptionsParameter>(typeof(OptionsParameter), options)
				.Options
				.ShouldBe(options);
		}

		[Fact]
		public void When_building_a_type_with_a_poco()
		{
		}

		public class NoVisibleCtor
		{
			private NoVisibleCtor() {}
		}

		public class ParameterlessCtor
		{
		}

		public class OptionsParameter
		{
			public Options Options { get; }
			public OptionsParameter(Options options)
			{
				Options = options;
			}
		}
	}
}