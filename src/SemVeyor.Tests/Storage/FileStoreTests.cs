using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileSystem;
using Ploeh.AutoFixture;
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

		public FileStoreTests()
		{
			_store = new FileStore(
				new InMemoryFileSystem(),
				new FileStoreOptions());
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

//		private class MemoryFileSystem : IFileSystem
//		{
//			private readonly Dictionary<string, string> _store;
//
//			public MemoryFileSystem()
//			{
//				_store = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
//			}
//
//			public void AppendLine(string path, string line)
//			{
//				var existing = _store.ContainsKey(path)
//					? _store[path]
//					: string.Empty;
//
//				_store[path] = existing + line + Environment.NewLine;
//			}
//
//			public IEnumerable<string> ReadAllLines(string path)
//			{
//				return _store.ContainsKey(path)
//					? _store[path].Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
//					: Enumerable.Empty<string>();
//			}
//		}
	}
}