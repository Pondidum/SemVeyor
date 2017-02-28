using System;

namespace SemVeyor.Domain.Events
{
	public class GenericArgumentPositionChanged : IMajor
	{
		private readonly GenericArgumentDetails _older;
		private readonly GenericArgumentDetails _newer;

		public GenericArgumentPositionChanged(GenericArgumentDetails older, GenericArgumentDetails newer)
		{
			_older = older;
			_newer = newer;
		}

		public override string ToString() => $"{nameof(GenericArgumentPositionChanged)}: {_older} => {_newer}";
	}
}
