﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SemVeyor.Domain.Queries
{
	public class GetAllCtorsQuery
	{
		public IEnumerable<CtorDetails> Execute(Type type)
		{
			return type
				.GetConstructors(MemberDetails.ExternalVisibleFlags)
				.Select(CtorDetails.From)
				.Where(MemberDetails.IsExternal);
		}
	}
}
