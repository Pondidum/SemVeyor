namespace SemVeyor.Domain.Events
{
	public class FieldVisibilityIncreased : FieldEvent
	{
		public FieldVisibilityIncreased(FieldDetails older, FieldDetails newer) : base(older, newer)
		{
		}
	}
}
