namespace SemVeyor.Domain.Events
{
	public class FieldVisibilityIncreased : FieldEvent, IMinor
	{
		public FieldVisibilityIncreased(FieldDetails older, FieldDetails newer) : base(older, newer)
		{
		}
	}
}
