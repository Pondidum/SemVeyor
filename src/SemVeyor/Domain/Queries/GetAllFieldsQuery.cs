using System;
using System.Collections.Generic;
using System.Linq;

namespace SemVeyor.Domain.Queries
{
	public class GetAllFieldsQuery
	{
		public IEnumerable<FieldDetails> Execute(Type type)
		{
			return type
				.GetFields(MemberDetails.ExternalVisibleFlags)
				.Select(FieldDetails.From)
				.Where(MemberDetails.IsExternal);
		}
	}
}
