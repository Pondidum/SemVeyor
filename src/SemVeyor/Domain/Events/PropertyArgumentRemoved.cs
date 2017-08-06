namespace SemVeyor.Domain.Events
{
	public class PropertyArgumentRemoved : PropertyEvent
	{
		public PropertyArgumentRemoved(PropertyDetails older, PropertyDetails newer) : base(older, newer)
		{
		}
	}
}
