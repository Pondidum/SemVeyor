using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SemVeyor.Domain.Queries
{
	public class GetAllMethodsQuery
	{
		public IEnumerable<MethodDetails> Execute(Type type)
		{
			var methods = type
				.GetMethods(MemberDetails.ExternalVisibleFlags)
				.Where(m => m.IsSpecialName == false)
				.Select(MethodDetails.From);

			var objectProtectedMethods = new HashSet<string>(typeof(object)
				.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
				.Where(c => c.IsFamily)
				.Where(m => m.IsSpecialName == false)
				.Select(met => met.Name));

			return methods
				.Where(met => objectProtectedMethods.Contains(met.Name) == false)
				.Where(MemberDetails.IsExternal);
		}
	}
}
