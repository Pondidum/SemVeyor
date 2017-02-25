namespace SemVeyor.Domain.Events
{
	public class AssemblyTypeAdded : IMinor { }
	public class AssemblyTypeRemoved : IMajor { }

	public class TypeVisibilityIncreased : IMinor { }
	public class TypeVisibilityDecreased : IMajor { }
	public class FieldAdded : IMinor { }
	public class FieldRemoved : IMajor { }
	public class MethodAdded : IMinor { }
	public class MethodRemoved : IMajor { }
	public class PropertyAdded : IMinor { }
	public class PropertyRemoved : IMajor { }
	public class CtorAdded : IMinor { }
	public class CtorRemoved : IMajor { }
	public class GenericArgumentAdded : IMinor { }
	public class GenericArgumentRemoved : IMajor { }

	public class FieldVisibilityIncreased : IMinor { }
	public class FieldVisibilityDecreased : IMajor { }
	public class FieldTypeChanged : IMajor { }

	public class GenericArgumentPositionChanged : IMajor { }
	public class GenericArgumentNameChanged : IMajor { }
	public class GenericArgumentConstraintAdded : IMinor { }
	public class GenericArgumentConstraintRemoved : IMajor { }

	public class ArgumentNameChanged : IMajor  { }
	public class ArgumentTypeChanged : IMajor { }
	public class ArgumentMoved : IMajor { }

	public class MethodVisibilityIncreased : IMinor { }
	public class MethodVisibilityDecreased : IMajor { }
	public class MethodNameChanged : IMajor { }
	public class MethodTypeChanged : IMajor { }
	public class MethodArgumentAdded : IMinor { }
	public class MethodArgumentRemoved : IMajor { }
	public class MethodGenericArgumentAdded : IMinor { }
	public class MethodGenericArgumentRemoved : IMajor { }

	public class PropertyVisibilityDecreased : IMajor { }
	public class PropertyVisibilityIncreased : IMinor { }
	public class PropertyTypeChanged : IMajor { }
	public class PropertyArgumentAdded : IMinor { }
	public class PropertyArgumentRemoved : IMajor { }

	public class CtorVisibilityDecreased : IMajor { }
	public class CtorVisibilityIncreased : IMinor { }
	public class CtorArgumentAdded : IMinor { }
	public class CtorArgumentRemoved : IMajor { }

	public interface IMajor { }
	public interface IMinor { }
}
