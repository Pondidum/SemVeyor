using System;
// ReSharper disable All

namespace SemVeyor.Tests.TestUtils
{
	public class TestType: ParentType, ITestInterfaceOne, ITestInterfaceTwo
	{
		public int PublicField;
		internal int InternalField;
		protected int ProtectedField;
		private int PrivateField;


		public TestType() {}
		internal TestType(int arg) {}
		protected TestType(string first, int second, Guid third) {}
		private TestType(Guid arg) {}


		public int Property { get; set; }
		public int ReadonlyProperty { get; }

		public int PrivateSetProperty { get; private set; }
		public int InternalSetProperty { get; internal set; }
		public int ProtectedSetProperty { get; protected set; }

		internal int InternalProperty { get; set; }
		protected int ProtectedProperty { get; set; }
		private int PrivateProperty { get; set; }

		public string this[int index]
		{
			get { throw new Exception(); }
			set { throw new Exception(); }
		}

		public Guid this[int x, int y]
		{
			get { throw new Exception(); }
			set { throw new Exception(); }
		}

		public int Method(string first, int second, Action<int> third) { throw new NotSupportedException(); }
		internal int InternalMethod() { throw new NotSupportedException(); }
		protected int ProtectedMethod() { throw new NotSupportedException(); }
		private int PrivateMethod() { throw new NotSupportedException(); }

		protected T GenericMethod<T>(T test, Action<T> action) { throw new NotSupportedException(); }

		public static string GenericMethodName => nameof(GenericMethod);
	}

	public class ParentType {}
	public interface ITestInterfaceOne {}
	public interface ITestInterfaceTwo {}
}
