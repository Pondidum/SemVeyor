using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using Stronk;
using Stronk.ConfigurationSourcing;
using Stronk.Policies;

namespace SemVeyor.Config
{
	public class Configuration
	{
		public HashSet<string> ReporterTypes { get; }
		public HashSet<string> StorageTypes { get; }
		public Options GlobalOptions { get; }

		private readonly IDictionary<string, IDictionary<string, string>> _reporters;
		private readonly IDictionary<string, IDictionary<string, string>> _stores;

		public Configuration(Options options, IDictionary<string, IDictionary<string, string>> storage, IDictionary<string, IDictionary<string, string>> reporters)
		{
			GlobalOptions = options;

			ApplyStorageDefaults(storage);
			ApplyReportingDefaults(reporters);

			StorageTypes = new HashSet<string>(storage.Keys, StringComparer.OrdinalIgnoreCase);
			ReporterTypes = new HashSet<string>(reporters.Keys, StringComparer.OrdinalIgnoreCase);

			_stores = storage;
			_reporters = reporters;
		}

		private static void ApplyStorageDefaults(IDictionary<string, IDictionary<string, string>> storage)
		{
			if (storage.Any() == false)
				storage.Add(Options.DefaultStorage, new Dictionary<string, string>());
		}

		private static void ApplyReportingDefaults(IDictionary<string, IDictionary<string, string>> reporting)
		{
			if (reporting.Any() == false)
				reporting.Add(Options.DefaultReporter, new Dictionary<string, string>());
		}

		public T StorageOptions<T>(string key)
		{
			IDictionary<string, string> options;

			if (_stores.TryGetValue(key, out options) == false)
				options = new Dictionary<string, string>();


			return PopulateOptions<T>(options);
		}

		public T ReporterOptions<T>(string key)
		{
			IDictionary<string, string> options;

			if (_reporters.TryGetValue(key, out options) == false)
				options = new Dictionary<string, string>();


			return PopulateOptions<T>(options);
		}

		private static T PopulateOptions<T>(IDictionary<string, string> options)
		{
			var builder = new ConfigBuilder(new StronkOptions
			{
				ErrorPolicy = new ErrorPolicy
				{
					OnSourceValueNotFound = new SourceValueNotFoundPolicy(PolicyActions.Skip)
				}
			});

			var target = Activator.CreateInstance<T>();
			builder.Populate(target, new CliConfigurationSource(options));
			return target;
		}

		private class CliConfigurationSource : IConfigurationSource
		{
			public IDictionary<string, string> AppSettings { get; }
			public IDictionary<string, ConnectionStringSettings> ConnectionStrings { get; }

			public CliConfigurationSource(IDictionary<string, string> options)
			{
				ConnectionStrings = new Dictionary<string, ConnectionStringSettings>();
				AppSettings = options;
			}
		}
	}
}
