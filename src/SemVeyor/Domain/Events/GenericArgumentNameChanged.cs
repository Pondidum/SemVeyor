using System;

namespace SemVeyor.Domain.Events
{
	public class GenericArgumentNameChanged : IMajor
	{
		private readonly GenericArgumentDetails _older;
		private readonly GenericArgumentDetails _newer;

		public GenericArgumentNameChanged(GenericArgumentDetails older, GenericArgumentDetails newer)
		{
			_older = older;
			_newer = newer;
		}

		public override string ToString() => $"{nameof(GenericArgumentNameChanged)}: {_older} => {_newer}";
	}
}
