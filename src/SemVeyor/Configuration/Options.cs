using System.Collections.Generic;
using System.Linq;
using SemVeyor.Classification;
using SemVeyor.Domain.Events;

namespace SemVeyor.Configuration
{
	public class Options
	{
		public const string DefaultStorage = "file";
		public const string DefaultReporter = "simple";

		public bool ReadOnly { get; set; }
		public IEnumerable<string> Paths { get; set; }
		public IDictionary<string, SemVer> Classifications { get; set; }

		public Options()
		{
			ReadOnly = false;
			Paths = Enumerable.Empty<string>();
			Classifications = DefaultClassificationMap;
		}

		public static readonly Dictionary<string, SemVer> DefaultClassificationMap = new Dictionary<string, SemVer>
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
