using System;
using System.Collections.Generic;

namespace SemVeyor.CommandLine
{
	public class CommandLineResult<T> where T : new()
	{
		private readonly T _options;
		private readonly bool _wasSuccessful;
		private readonly IEnumerable<string> _errors;

		public CommandLineResult(T options)
		{
			_options = options;
			_wasSuccessful = true;
		}

		public CommandLineResult(IEnumerable<string> errors)
		{
			_errors = errors;
			_wasSuccessful = false;
		}

		public void OnSuccess(Action<T> callback)
		{
			if (_wasSuccessful)
				callback(_options);
		}

		public void OnFailure(Action<IEnumerable<string>> callback)
		{
			if (_wasSuccessful == false)
				callback(_errors);
		}
	}
}