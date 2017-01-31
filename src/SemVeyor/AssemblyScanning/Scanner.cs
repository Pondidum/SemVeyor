using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SemVeyor.AssemblyScanning
{
	public class Scanner
	{
		public IEnumerable<TypeContent> Scan(Assembly assembly)
		{
			return assembly.GetExportedTypes().Select(TypeContent.From);
		}
	}
}
