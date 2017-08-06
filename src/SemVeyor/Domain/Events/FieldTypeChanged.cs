namespace SemVeyor.Domain.Events
{
	public class FieldTypeChanged : FieldEvent
	{
		public FieldTypeChanged(FieldDetails older, FieldDetails newer) : base(older, newer)
		{
		}
	}
}
