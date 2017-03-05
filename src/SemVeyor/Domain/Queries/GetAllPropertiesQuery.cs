using System;
using System.Collections.Generic;
using System.Linq;

namespace SemVeyor.Domain.Queries
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
