namespace SemVeyor.Domain.Events
{
	public class PropertyArgumentRemoved : PropertyEvent, IMajor
	{
		public PropertyArgumentRemoved(PropertyDetails older, PropertyDetails newer) : base(older, newer)
		{
		}
	}
}
