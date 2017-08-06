namespace SemVeyor.Domain.Events
{
	public class FieldVisibilityDecreased : FieldEvent
	{
		public FieldVisibilityDecreased(FieldDetails older, FieldDetails newer) : base(older, newer)
		{
		}
	}
}
