using System;
using System.Linq;
using System.Reflection;
using SemVeyor.Domain;
using SemVeyor.Storage;

namespace SemVeyor
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			var store = new FileStore("history.lsj");

			var previous = store.Read();
			var current = AssemblyDetails.From(Assembly.LoadFile(args[0]));

			Console.WriteLine(previous != null
				? "Previous run found"
				: "No History found");

			if (previous != null)
			{
				var changes = previous.UpdatedTo(current).ToArray();

				Console.WriteLine(changes.Any()
					? "Changes since previous run:"
					: "No changes since previous run");

				foreach (var change in changes)
				{
					Console.WriteLine(change);
				}
			}

			store.Write(current);

			Console.WriteLine("Done.");
		}
	}
}
