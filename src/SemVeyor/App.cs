﻿using System.Linq;
using SemVeyor.Classification;
using SemVeyor.Domain;
using SemVeyor.Reporting;
using SemVeyor.Storage;

namespace SemVeyor
{
	public class App
	{
		private readonly IStorage _store;
		private readonly IReporter _reporter;

		public App(IStorage store, IReporter reporter)
		{
			_store = store;
			_reporter = reporter;
		}

		public void Execute(AssemblyDetails previous, AssemblyDetails current)
		{
			if (previous != null)
			{
				var changes = previous.UpdatedTo(current);
				var classifier = new EventClassification();

				var processed = classifier.ClassifyAll(changes).ToArray();
				var semVerChange = processed
					.DefaultIfEmpty(new ChangeClassification { Classification = SemVer.None })
					.Max(c => c.Classification);

				_reporter.Write(new ReportArgs
				{
					PreviousAssembly = previous,
					CurrentAssembly = current,
					Changes = processed,
					SemVerChange = semVerChange
				});
			}

			_store.Write(current);
		}
	}
}