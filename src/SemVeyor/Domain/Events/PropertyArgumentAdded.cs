namespace SemVeyor.Domain.Events
{
	public class PropertyArgumentAdded : PropertyEvent
	{
		public PropertyArgumentAdded(PropertyDetails older, PropertyDetails newer) : base(older, newer)
		{
		}
	}
}
