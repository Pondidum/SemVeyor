namespace SemVeyor.Domain.Events
{
	public class PropertyTypeChanged : PropertyEvent, IMajor
	{
		public PropertyTypeChanged(PropertyDetails older, PropertyDetails newer) : base(older, newer)
		{
		}
	}
}
