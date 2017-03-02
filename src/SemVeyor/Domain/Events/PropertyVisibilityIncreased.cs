namespace SemVeyor.Domain.Events
{
	public class PropertyVisibilityIncreased : PropertyEvent, IMinor
	{
		public PropertyVisibilityIncreased(PropertyDetails older, PropertyDetails newer) : base(older, newer)
		{
		}
	}
}
