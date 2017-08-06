using System;
using System.Collections.Generic;
using System.Linq;
using SemVeyor.Domain.Events;

namespace SemVeyor.Classification
{
	public class EventClassification
	{
		public SemVer ClassifyEvent(object @event)
		{
			var eventType = @event as Type ?? @event.GetType();

			SemVer version;
			return VersionMap.TryGetValue(eventType.Name, out version)
				? version
				: SemVer.None;
		}

		public IEnumerable<ChangeClassification> ClassifyAll(IEnumerable<object> events)
		{
			return events.Select(@event => new ChangeClassification
			{
				Change = @event,
				Classification = ClassifyEvent(@event)
			});
		}

		private static readonly Dictionary<string, SemVer> VersionMap = new Dictionary<string, SemVer>
		{
			{ nameof(AssemblyTypeAdded), SemVer.Minor },
			{ nameof(AssemblyTypeRemoved), SemVer.Major },

			{ nameof(TypeVisibilityIncreased), SemVer.Minor },
			{ nameof(TypeVisibilityDecreased), SemVer.Major },
			{ nameof(TypeFieldAdded), SemVer.Minor },
			{ nameof(TypeFieldRemoved), SemVer.Major },
			{ nameof(TypeMethodAdded), SemVer.Minor },
			{ nameof(TypeMethodRemoved), SemVer.Major },
			{ nameof(TypePropertyAdded), SemVer.Minor },
			{ nameof(TypePropertyRemoved), SemVer.Major },
			{ nameof(TypeCtorAdded), SemVer.Minor },
			{ nameof(TypeCtorRemoved), SemVer.Major },
			{ nameof(TypeGenericArgumentAdded), SemVer.Minor },
			{ nameof(TypeGenericArgumentRemoved), SemVer.Major },

			{ nameof(FieldVisibilityIncreased), SemVer.Minor },
			{ nameof(FieldVisibilityDecreased), SemVer.Major },
			{ nameof(FieldTypeChanged), SemVer.Major },

			{ nameof(GenericArgumentPositionChanged), SemVer.Major },
			{ nameof(GenericArgumentNameChanged), SemVer.Major },
			{ nameof(GenericArgumentConstraintAdded), SemVer.Minor },
			{ nameof(GenericArgumentConstraintRemoved), SemVer.Major },

			{ nameof(ParameterNameChanged), SemVer.Major },
			{ nameof(ParameterTypeChanged), SemVer.Major },
			{ nameof(ParameterMoved), SemVer.Major },

			{ nameof(MethodVisibilityIncreased), SemVer.Minor },
			{ nameof(MethodVisibilityDecreased), SemVer.Major },
			{ nameof(MethodNameChanged), SemVer.Major },
			{ nameof(MethodTypeChanged), SemVer.Major },
			{ nameof(MethodArgumentAdded), SemVer.Minor },
			{ nameof(MethodArgumentRemoved), SemVer.Major },
			{ nameof(MethodGenericArgumentAdded), SemVer.Minor },
			{ nameof(MethodGenericArgumentRemoved), SemVer.Major },

			{ nameof(PropertyVisibilityDecreased), SemVer.Major },
			{ nameof(PropertyVisibilityIncreased), SemVer.Minor },
			{ nameof(PropertyTypeChanged), SemVer.Major },
			{ nameof(PropertyArgumentAdded), SemVer.Minor },
			{ nameof(PropertyArgumentRemoved), SemVer.Major },

			{ nameof(CtorVisibilityDecreased), SemVer.Major },
			{ nameof(CtorVisibilityIncreased), SemVer.Minor },
			{ nameof(CtorArgumentAdded), SemVer.Minor },
			{ nameof(CtorArgumentRemoved), SemVer.Major },
		};
	}
}
