using System;

namespace SemVeyor.Domain
{
	public struct TypeName : IEquatable<TypeName>
	{
		private readonly string _fullname;

		public TypeName(string fullname)
		{
			if (string.IsNullOrWhiteSpace(fullname))
				throw new ArgumentNullException(nameof(fullname));

			_fullname = fullname;
		}

		public bool Equals(TypeName other)
		{
			return string.Equals(_fullname, other._fullname);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			return obj is TypeName && Equals((TypeName)obj);
		}

		public override int GetHashCode()
		{
			return _fullname.GetHashCode();
		}

		public static bool operator ==(TypeName left, TypeName right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(TypeName left, TypeName right)
		{
			return !left.Equals(right);
		}
	}
}
