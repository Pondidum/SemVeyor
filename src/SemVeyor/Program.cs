using System;
using System.Collections.Generic;

namespace SemVeyor
{
	public static class Program
	{
		public static void Main(string[] args)
		{
		}
	}

	public class CommandLineParser
	{
		public CommandLineResult<Options> Parse(string[] args)
		{
			throw new NotImplementedException();
		}
	}

	public class CommandLineResult<T> where T : new()
	{
		public T Result { get; }
		public bool WasSuccessful { get; }

		public CommandLineResult(T result, bool success)
		{
			Result = result;
			WasSuccessful = success;
		}

		public void OnSuccess(Action<T> callback)
		{
			throw new NotImplementedException();
		}

		public void OnFailure(Action<IEnumerable<string>> callback)
		{
			throw new NotImplementedException();
		}
	}

	public class Options
	{
		public string AssemblyPath { get; set; }
	}

}
