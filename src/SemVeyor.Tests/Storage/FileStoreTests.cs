using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileSystem;
using Ploeh.AutoFixture;
using SemVeyor.CommandLine;
using SemVeyor.Config;
using SemVeyor.Domain;
using SemVeyor.Infrastructure;
using SemVeyor.Storage;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Storage
{
	public class FileStoreTests
	{
		private readonly FileStore _store;
		private Options _options;

		public FileStoreTests()
		{
			_options = new Options();
			_store = new FileStore(
				new InMemoryFileSystem(),
				new StoreSerializer(),
				_options,
				new FileStoreOptions());
		}

		[Fact]
		public void When_readonly_nothing_is_written()
		{
			_options.ReadOnly = true;

			var input = new Fixture().Create<AssemblyDetails>();
			_store.Write(input);

			_store.Read().ShouldBeNull();
		}

		[Fact]
		public void Storing_and_loading_assembly_details_works_correctly()
		{
			var input = new Fixture().Create<AssemblyDetails>();

			_store.Write(input);

			var loaded = _store.Read();

			input.UpdatedTo(loaded).ShouldBeEmpty();
		}

		[Fact]
		public void Loading_returns_the_most_recent_input()
		{
			var fixture = new Fixture();
			var input = fixture.Create<AssemblyDetails>();

			_store.Write(fixture.Create<AssemblyDetails>());
			_store.Write(fixture.Create<AssemblyDetails>());
			_store.Write(input);

			var loaded = _store.Read();

			input.UpdatedTo(loaded).ShouldBeEmpty();
		}
	}
}