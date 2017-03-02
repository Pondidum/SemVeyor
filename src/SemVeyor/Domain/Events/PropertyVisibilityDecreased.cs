namespace SemVeyor.Domain.Events
{
	public class PropertyVisibilityDecreased : PropertyEvent, IMajor
	{
		public PropertyVisibilityDecreased(PropertyDetails older, PropertyDetails newer) : base(older, newer)
		{
		}
	}
}
