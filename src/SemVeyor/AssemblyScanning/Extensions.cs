using System;
using System.Reflection;

namespace SemVeyor.AssemblyScanning
{
	public static class Extensions
	{
		public static Visibility GetVisibility(this MemberInfo info)
		{
			var prop = info as PropertyInfo;

			if (prop != null)
				info = prop?.GetMethod ?? prop?.SetMethod;

			var methodInfo = info as MethodBase;
			if (methodInfo != null)
				return VisibilityFromMethod(methodInfo);

			var field = info as FieldInfo;
			if (field != null)
				return VisibilityFromField(field);

			throw new NotSupportedException();
		}

		private static Visibility VisibilityFromMethod(MethodBase method)
		{
			if (method.IsPublic)
				return Visibility.Public;

			if (method.IsFamily)
				return Visibility.Protected;

			if (method.IsAssembly)
				return Visibility.Internal;

			return Visibility.Private;
		}

		private static Visibility VisibilityFromField(FieldInfo field)
		{
			if (field.IsPublic)
				return Visibility.Public;

			if (field.IsFamily)
				return Visibility.Protected;

			if (field.IsAssembly)
				return Visibility.Internal;

			return Visibility.Private;
		}
	}
}
