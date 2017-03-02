namespace SemVeyor.Domain.Events
{
	public class FieldTypeChanged : FieldEvent, IMajor
	{
		public FieldTypeChanged(FieldDetails older, FieldDetails newer) : base(older, newer)
		{
		}
	}
}
