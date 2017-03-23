using System;
using System.IO;
using System.Threading.Tasks;
using SemVeyor.Infrastructure;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Infrastructure
{
	public class PhysicalFileSystemTests : IDisposable
	{
		private readonly string _path;
		private readonly string _directory;
		private readonly PhysicalFileSystem _fs;

		public PhysicalFileSystemTests()
		{
			_directory = Guid.NewGuid().ToString();
			_path = Path.Combine(_directory, Guid.NewGuid().ToString() + ".txt");

			_fs = new PhysicalFileSystem();
		}

		private void CreateDirectory() => Directory.CreateDirectory(_directory);

		[Fact]
		public void When_appending_to_non_existing_file()
		{
			CreateDirectory();

			_fs.AppendLine(_path, "test line!");

			var lines = _fs.ReadAllLines(_path);

			lines.ShouldBe(new[]
			{
				"test line!"
			});
		}

		[Fact]
		public void When_appending_to_existing_file()
		{
			CreateDirectory();
			File.WriteAllText(_path, "line one\r\nline two\r\n");

			_fs.AppendLine(_path, "test line!");

			var lines = _fs.ReadAllLines(_path);

			lines.ShouldBe(new[]
			{
				"line one",
				"line two",
				"test line!"
			});
		}

		[Fact]
		public void When_appending_multiple_times()
		{
			CreateDirectory();

			_fs.AppendLine(_path, "test line!");
			_fs.AppendLine(_path, "another?");

			var lines = _fs.ReadAllLines(_path);

			lines.ShouldBe(new[]
			{
				"test line!",
				"another?"
			});
		}

		public void Dispose()
		{
			try
			{
				Directory.Delete(_directory, recursive: true);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
	}
}