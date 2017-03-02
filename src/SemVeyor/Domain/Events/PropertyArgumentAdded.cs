namespace SemVeyor.Domain.Events
{
	public class PropertyArgumentAdded : PropertyEvent, IMinor
	{
		public PropertyArgumentAdded(PropertyDetails older, PropertyDetails newer) : base(older, newer)
		{
		}
	}
}
