using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileSystem;
using Newtonsoft.Json.Linq;

using ConfigDictionary = System.Collections.Generic.Dictionary<string, System.Collections.Generic.IDictionary<string, string>>;

namespace SemVeyor.Configuration
{
    public class ConfigFileReader
    {
        public static readonly string[] FilePaths =
        {
            "SemVeyor.json"
        };

        private readonly IFileSystem _fs;

        public ConfigFileReader(IFileSystem fs)
        {
            _fs = fs;
        }

        public Config Read()
        {
            if (_fs.FileExists(FilePaths.First()).Result == false)
                return new Config();

            using (var stream = _fs.ReadFile("SemVeyor.json").Result)
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();

                if (string.IsNullOrWhiteSpace(json))
                    return new Config();

                var parsed = JObject.Parse(json);

                var options = parsed.ToObject<Options>();

                var storage = GetOrDefault(parsed, "storage");
                var reports = GetOrDefault(parsed, "reporters");

                return new Config(options, storage, reports);
            }
        }

        private ConfigDictionary GetOrDefault(JObject json, string key)
        {
            JToken token;
            return json.TryGetValue(key, out token)
                ? token.ToObject<ConfigDictionary>()
                : new ConfigDictionary();
        }
    }
}
