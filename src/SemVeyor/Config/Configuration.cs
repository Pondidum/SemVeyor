using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using SemVeyor.CommandLine;
using Stronk;
using Stronk.Policies;

namespace SemVeyor.Config
{
	public class Configuration
	{
		public HashSet<string> StorageTypes { get; }
		public Options GlobalOptions { get; }

		private readonly IDictionary<string, IDictionary<string, string>> _stores;

		public Configuration(Options options, IDictionary<string, IDictionary<string, string>> storage)
		{
			GlobalOptions = options;

			ApplyDefaultsTo(storage);

			StorageTypes = new HashSet<string>(storage.Keys, StringComparer.OrdinalIgnoreCase);
			_stores = storage;
		}

		private static void ApplyDefaultsTo(IDictionary<string, IDictionary<string, string>> storage)
		{
			if (storage.Any() == false)
				storage.Add(Options.DefaultStorage, new Dictionary<string, string>());
		}

		public T StorageOptions<T>(string key)
		{
			IDictionary<string, string> options;

			if (_stores.TryGetValue(key, out options) == false)
				options = new Dictionary<string, string>();


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
			public NameValueCollection AppSettings { get; }
			public ConnectionStringSettingsCollection ConnectionStrings { get; }

			public CliConfigurationSource(IDictionary<string, string> options)
			{
				ConnectionStrings = new ConnectionStringSettingsCollection();
				AppSettings = new NameValueCollection();

				foreach (var pair in options)
					AppSettings.Add(pair.Key, pair.Value);
			}
		}
	}
}