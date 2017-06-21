using System;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Ploeh.AutoFixture;
using SemVeyor.Domain;
using SemVeyor.Storage;
using Shouldly;
using Xunit;

namespace SemVeyor.Tests.Storage
{
	public class StoreSerializerTests
	{
		private void Test<T>()
			where T : IDeltaProducer<T>
		{
			var input = new Fixture().Create<T>();
			var sut = new StoreSerializer();

			var built = sut.Deserialize<T>(sut.Serialize(input));
			
			input.UpdatedTo(built).ShouldBeEmpty();
			built.UpdatedTo(input).ShouldBeEmpty();
		}
		
		[Theory]
		[InlineData(typeof(AssemblyDetails))]
		[InlineData(typeof(TypeDetails))]
		[InlineData(typeof(FieldDetails))]
		[InlineData(typeof(CtorDetails))]
		[InlineData(typeof(PropertyDetails))]
		[InlineData(typeof(MethodDetails))]
		[InlineData(typeof(ParameterDetails))]
		[InlineData(typeof(GenericArgumentDetails))]
		public void When_serializing_and_deserializing_an_assembly_details_graph(Type type)
		{
			var method = this.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic).Single(m => m.Name == nameof(Test));
			var generic = method.MakeGenericMethod(type);

			generic.Invoke(this, new object[0]);
		}
	}
}