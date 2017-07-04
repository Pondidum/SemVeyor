using System;
using System.Collections.Generic;
using SemVeyor.CommandLine;
using SemVeyor.Configuration;
using SemVeyor.Storage;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Storage
{
	public class StorageFactoryTests
	{
		private readonly StorageFactory _factory;

		public StorageFactoryTests()
		{
			_factory = new StorageFactory();
		}

//		[Fact]
//		public void When_the_storage_option_is_unknown()
//		{
//			var options = new Options { Storage = "weofinwf" };
//
//			Should.Throw<NotSupportedException>(() => _factory.CreateStore(new CliParameters(), options));
//		}

		[Fact]
		public void When_the_storage_option_is_file()
		{
			var options = new Configuration.Config(new Options(), new Dictionary<string, IDictionary<string, string>>
			{
				{ Options.DefaultStorage, new Dictionary<string, string>() }
			}, new Dictionary<string, IDictionary<string, string>>());

			var store = _factory.CreateStore(options);

			store.ShouldBeOfType<FileStore>();
		}

		[Fact]
		public void When_the_storage_option_is_a_different_case()
		{
			var options = new Configuration.Config(new Options(), new Dictionary<string, IDictionary<string, string>>
			{
				{ Options.DefaultStorage.ToUpper(), new Dictionary<string, string>() }
			}, new Dictionary<string, IDictionary<string, string>>());

			var store = _factory.CreateStore(options);

			store.ShouldBeOfType<FileStore>();
		}
	}
}
