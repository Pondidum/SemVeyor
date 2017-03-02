namespace SemVeyor.Domain.Events
{
	public class FieldVisibilityDecreased : FieldEvent, IMajor
	{
		public FieldVisibilityDecreased(FieldDetails older, FieldDetails newer) : base(older, newer)
		{
		}
	}
}
