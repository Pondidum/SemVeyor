using System;

namespace SemVeyor.Domain
{
	public struct TypeName : IEquatable<TypeName>
	{
		private readonly string _fullname;

		public TypeName(string fullname)
		{
			_fullname = fullname;
		}

		public override string ToString()
		{
			return _fullname ?? "void";
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
			return (_fullname != null ? _fullname.GetHashCode() : 0);
		}

		public static bool operator ==(TypeName left, TypeName right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(TypeName left, TypeName right)
		{
			return !left.Equals(right);
		}

		public static implicit operator TypeName (Type type)
		{
			return new TypeName(type.FullName);
		}
	}
}
