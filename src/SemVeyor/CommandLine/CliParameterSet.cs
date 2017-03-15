using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using Stronk;
using Stronk.Policies;

namespace SemVeyor.CommandLine
{
	public class CliParameterSet
	{
		public IDictionary<string, string> Arguments { get; set; }
		public ISet<string> Flags { get; set; }
		public IEnumerable<string> Paths { get; }

		public CliParameterSet(IEnumerable<string> paths)
		{
			Arguments = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			Flags = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			Paths = paths;
		}

		public T Build<T>()
		{
			return (T)Build((typeof(T)));
		}

		public object Build(Type type)
		{
			var builder = new ConfigBuilder(new StronkOptions
			{
				ErrorPolicy = new ErrorPolicy
				{
					OnSourceValueNotFound = new SourceValueNotFoundPolicy(PolicyActions.Skip)
				}
			});

			var instance = Activator.CreateInstance(type);

			builder.Populate(instance, new CliConfigurationSource(this));

			return instance;
		}

		private class CliConfigurationSource : IConfigurationSource
		{
			public NameValueCollection AppSettings { get; }
			public ConnectionStringSettingsCollection ConnectionStrings { get; }

			public CliConfigurationSource(CliParameterSet parameters)
			{
				ConnectionStrings = new ConnectionStringSettingsCollection();
				AppSettings = new NameValueCollection();

				foreach (var pair in parameters.Arguments)
					AppSettings.Add(pair.Key, pair.Value);

				foreach (var flag in parameters.Flags)
					AppSettings.Add(flag, "true");

				AppSettings.Add("Paths", string.Join(",", parameters.Paths));
			}
		}
	}
}
