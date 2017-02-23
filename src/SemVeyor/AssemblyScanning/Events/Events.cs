﻿namespace SemVeyor.AssemblyScanning.Events
{
	public class AssemblyTypeAdded { }
	public class AssemblyTypeRemoved { }

	public class TypeVisibilityIncreased { }
	public class TypeVisibilityDecreased { }
	public class FieldAdded { }
	public class FieldRemoved { }
	public class MethodAdded { }
	public class MethodRemoved { }
	public class PropertyAdded { }
	public class PropertyRemoved { }
	public class CtorAdded { }
	public class CtorRemoved { }
	public class GenericArgumentAdded { }
	public class GenericArgumentRemoved { }

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

	public class PropertyVisibilityDecreased { }
	public class PropertyVisibilityIncreased { }
	public class PropertyTypeChanged { }
	public class PropertyArgumentAdded { }
	public class PropertyArgumentRemoved { }

	public class CtorVisibilityDecreased { }
	public class CtorVisibilityIncreased { }
	public class CtorArgumentAdded { }
	public class CtorArgumentRemoved { }
}
