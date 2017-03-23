using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SemVeyor.Infrastructure
{
	public interface IFileSystem
	{
		void AppendLine(string path, string line);
		IEnumerable<string> ReadAllLines(string path);
	}

	public class PhysicalFileSystem : IFileSystem
	{
		public void AppendLine(string path, string line)
		{
			using (var fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Read))
			using (var writer = new StreamWriter(fs))
				writer.WriteLineAsync(line);
		}

		public IEnumerable<string> ReadAllLines(string path)
		{
			if (File.Exists(path) == false)
				return Enumerable.Empty<string>();

			return File.ReadAllLines(path);
		}
	}
}