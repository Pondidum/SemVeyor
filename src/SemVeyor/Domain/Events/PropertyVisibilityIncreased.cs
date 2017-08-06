namespace SemVeyor.Domain.Events
{
	public class PropertyVisibilityIncreased : PropertyEvent
	{
		public PropertyVisibilityIncreased(PropertyDetails older, PropertyDetails newer) : base(older, newer)
		{
		}
	}
}
