using System;
using System.Collections.Generic;

namespace SemVeyor.Domain.Queries
{
	public class GetAllCtors
	{
		IEnumerable<CtorDetails> Execute(Type type)
		{
			return type
				.GetConstructors(ExternalVisibleFlags)
				.Select(ctor => CtorDetails.From(ctor))
				.Where(IsExternal);
		}
	}
}