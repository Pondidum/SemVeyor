using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SemVeyor.AssemblyScanning
{
	public class AssemblyDetails : IDeltaProducer<AssemblyDetails>
	{
		public string Name { get; set; }
		public IEnumerable<TypeDetails> Types { get; set; }

		public static AssemblyDetails From(Assembly assembly)
		{
			return new AssemblyDetails
			{
				Name = assembly.FullName,
				Types = assembly.GetExportedTypes().Select(TypeDetails.From).ToArray()
			};
		}

		public IEnumerable<object> UpdatedTo(AssemblyDetails newer)
		{
			throw new System.NotImplementedException();
		}
	}
}
