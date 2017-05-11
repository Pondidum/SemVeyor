using System;
using System.Collections.Generic;
using System.Linq;
using SemVeyor.Domain;

namespace SemVeyor.Scanning.CompiledAssembly.Queries
{
	public class GetAllPropertiesQuery
	{
		public IEnumerable<PropertyDetails> Execute(Type type)
		{
			return type
				.GetProperties(MemberDetails.ExternalVisibleFlags)
				.Select(PropertyDetails.From)
				.Where(MemberDetails.IsExternal);
		}
	}
}
