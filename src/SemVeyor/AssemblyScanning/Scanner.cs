using System;
using System.Collections.Generic;
using System.Reflection;

namespace SemVeyor.AssemblyScanning
{
	public class Scanner
	{
		public IEnumerable<TypeContent> Scan(Assembly assembly)
		{
			throw new NotImplementedException();
		}
	}

	public class TypeContent
	{
		public IEnumerable<string> Properties { get; set; }
		public IEnumerable<string> Methods { get; set; }
		public IEnumerable<string> Fields { get; set; }
		public IEnumerable<string> Constructors { get; set; }

		public string Name { get; set; }
		public string Namespace { get; set; }
		public string FullName => $"{Namespace}.{Name}";
	}
}
