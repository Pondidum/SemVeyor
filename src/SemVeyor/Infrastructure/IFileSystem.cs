using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SemVeyor.Infrastructure
{
	public interface IFileSystem
	{
		Task AppendLine(string path, string line);
		Task<IEnumerable<string>> ReadAllLines(string path);
	}

	public class PhysicalFileSystem : IFileSystem
	{
		public async Task AppendLine(string path, string line)
		{
			using (var fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Read))
			using (var writer = new StreamWriter(fs))
				await writer.WriteLineAsync(line);
		}

		public async Task<IEnumerable<string>> ReadAllLines(string path)
		{
			if (File.Exists(path) == false)
				return Enumerable.Empty<string>();

			return await Task.Run(() =>  File.ReadAllLines(path));
		}
	}
}