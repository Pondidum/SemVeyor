namespace SemVeyor.AssemblyScanning.Events
{
	public class FieldAdded { }
	public class FieldRemoved { }
	public class MethodAdded { }
	public class MethodRemoved { }

	public class FieldVisibilityIncreased { }
	public class FieldVisibilityDecreased { }
	public class FieldTypeChanged { }

	public class GenericArgumentPositionChanged { }
	public class GenericArgumentNameChanged { }
	public class GenericArgumentConstraintAdded { }
	public class GenericArgumentConstraintRemoved { }

	public class ArgumentNameChanged { }
	public class ArgumentTypeChanged { }
	public class ArgumentMoved { }

	public class MethodVisibilityIncreased { }
	public class MethodVisibilityDecreased { }
	public class MethodNameChanged { }
	public class MethodTypeChanged { }
	public class MethodArgumentAdded { }
	public class MethodArgumentRemoved { }
	public class MethodGenericArgumentAdded { }
	public class MethodGenericArgumentRemoved { }
}
