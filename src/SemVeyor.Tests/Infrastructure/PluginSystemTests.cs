using System;
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
	}
}