namespace SemVeyor.Domain.Events
{
	public class PropertyTypeChanged : PropertyEvent
	{
		public PropertyTypeChanged(PropertyDetails older, PropertyDetails newer) : base(older, newer)
		{
		}
	}
}
