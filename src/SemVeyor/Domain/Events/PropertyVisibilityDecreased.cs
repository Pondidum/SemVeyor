namespace SemVeyor.Domain.Events
{
	public class PropertyVisibilityDecreased : PropertyEvent
	{
		public PropertyVisibilityDecreased(PropertyDetails older, PropertyDetails newer) : base(older, newer)
		{
		}
	}
}
