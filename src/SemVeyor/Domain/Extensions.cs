using System;
using System.Reflection;
using System.Text;

namespace SemVeyor.Domain
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

			var type = info as Type;
			if (type != null)
				return VisibilityFromType(type);

			throw new NotSupportedException($"{info.GetType().Name}: {info.Name}");
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

		private static Visibility VisibilityFromType(Type type)
		{
			if (type.IsPublic || type.IsNestedPublic)
				return Visibility.Public;

			if (type.IsNestedPrivate)
				return Visibility.Private;

			if (type.IsNestedFamily)
				return Visibility.Protected;

			if (type.IsNestedAssembly || type.IsNotPublic)
				return Visibility.Internal;

			var sb = new StringBuilder();

			sb.AppendLine(type.Name);
			sb.AppendLine($"IsPublic: {type.IsPublic}");
			sb.AppendLine($"IsNotPublic: {type.IsNotPublic}");
			sb.AppendLine($"IsNestedAsm: {type.IsNestedAssembly}");
			sb.AppendLine($"IsNestedFam: {type.IsNestedFamily}");
			sb.AppendLine($"IsNestedPublic: {type.IsNestedPublic}");
			sb.AppendLine($"IsNestedPrivate: {type.IsNestedPrivate}");
			sb.AppendLine($"IsVisible: {type.IsVisible}");

			throw new NotImplementedException(sb.ToString());
		}
	}
}
