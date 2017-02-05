using System.Collections.Generic;
using System.Linq;
using SemVeyor.AssemblyScanning;

namespace SemVeyor.Tests.AssemblyScanning
{
	public static class MemberDetailsExtensions
	{
		public static T ByVisibility<T>(this IEnumerable<T> self, Visibility visibility)
			where T : IMemberDetails
		{
			return self.FirstOrDefault(m => m.Visibility == visibility);
		}
	}
}
