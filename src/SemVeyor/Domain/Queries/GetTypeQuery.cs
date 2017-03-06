using System;
using System.Linq;

namespace SemVeyor.Domain.Queries
{
	public class GetTypeQuery
	{
		private readonly GetAllGenericArgumentsQuery _getGenerics;
		private readonly GetAllCtorsQuery _getCtors;
		private readonly GetAllFieldsQuery _getFields;
		private readonly GetAllPropertiesQuery _getProps;
		private readonly GetAllMethodsQuery _getMethods;

		public GetTypeQuery()
		{
			_getGenerics = new GetAllGenericArgumentsQuery();
			_getCtors = new GetAllCtorsQuery();
			_getFields = new GetAllFieldsQuery();
			_getProps = new GetAllPropertiesQuery();
			_getMethods = new GetAllMethodsQuery();
		}

		public TypeDetails Execute(Type type)
		{
			return new TypeDetails
			{
				Name = type.Name,
				Visibility = type.GetVisibility(),
				Namespace = type.Namespace,

				BaseType = type.BaseType?.Name,
				Interfaces = type.GetInterfaces().Select(i => i.Name),

				GenericArguments = _getGenerics.Execute(type),
				Constructors = _getCtors.Execute(type),
				Properties = _getProps.Execute(type),
				Methods = _getMethods.Execute(type),
				Fields = _getFields.Execute(type)
			};
		}
	}
}
