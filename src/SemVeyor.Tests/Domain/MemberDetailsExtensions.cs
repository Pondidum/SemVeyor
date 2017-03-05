using System.Collections.Generic;
using System.Linq;
using SemVeyor.Domain;

namespace SemVeyor.Tests.Domain
{
	public static class MemberDetailsExtensions
	{
		public static T ByVisibility<T>(this IEnumerable<T> self, Visibility visibility)
			where T : MemberDetails
		{
			return self.FirstOrDefault(m => m.Visibility == visibility);
		}
	}
}
