using System;
using System.Reflection;

namespace SemVeyor.AssemblyScanning
{
	public class MemberDetails
	{
		public Visibility Visibility => VisibilityFromType(_info);
		public string Name { get; }

		private readonly MemberInfo _info;

		protected MemberDetails(MemberInfo info)
		{
			_info = info;
			Name = info.Name;
		}

		protected virtual Visibility VisibilityFromType(MemberInfo info)
		{
			var methodInfo = info as MethodBase;
			if (methodInfo != null)
				return VisibilityFromMethod(methodInfo);

			var field = info as FieldInfo;
			if (field != null)
				return VisibilityFromField(field);

			throw new NotSupportedException();
		}

		protected virtual Visibility VisibilityFromMethod(MethodBase method)
		{
			if (method.IsPublic)
				return Visibility.Public;

			if (method.IsFamily)
				return Visibility.Protected;

			if (method.IsAssembly)
				return Visibility.Internal;

			return Visibility.Private;
		}

		protected virtual Visibility VisibilityFromField(FieldInfo field)
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
