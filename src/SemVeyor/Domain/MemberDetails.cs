using System.Reflection;

namespace SemVeyor.Domain
{
	public abstract class MemberDetails
	{
		public const BindingFlags ExternalVisibleFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

		public abstract Visibility Visibility { get; set; }
		public abstract string Name { get; set; }

		public static bool IsExternal(MemberDetails info)
		{
			return info.Visibility > Visibility.Internal;
		}
	}
}
