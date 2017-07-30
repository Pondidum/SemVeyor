using System.Collections.Generic;
using System.Linq;
using SemVeyor.Domain;
using SemVeyor.Tests.Domain;
using SemVeyor.Tests.TestUtils;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Scanning
{
	public abstract class GetAllFieldsQueryTestBase
	{
		private readonly IEnumerable<FieldDetails> _fields;

		public GetAllFieldsQueryTestBase()
		{
			_fields = BuildFields();
		}

		protected abstract IEnumerable<FieldDetails> BuildFields();

		[Fact]
		public void There_are_2_fields() => _fields.Count().ShouldBe(2);

		[Fact]
		public void The_public_field_is_listed() => _fields.ShouldContain(x => x.Visibility == Visibility.Public);

		[Fact]
		public void The_protected_field_is_listed() => _fields.ShouldContain(x => x.Visibility == Visibility.Protected);

		[Fact]
		public void The_internal_field_is_not_listed() => _fields.ShouldNotContain(x => x.Visibility == Visibility.Internal);

		[Fact]
		public void The_private_field_is_not_listed() => _fields.ShouldNotContain(x => x.Visibility == Visibility.Private);

		[Fact]
		public void The_field_name_is_populated() => PublicField.Name.ShouldBe(nameof(TestType.PublicField));

		[Fact]
		public void The_field_type_is_populated() => PublicField.Type.ShouldBe(typeof(int));

		private FieldDetails PublicField => _fields.ByVisibility(Visibility.Public);
	}
}
