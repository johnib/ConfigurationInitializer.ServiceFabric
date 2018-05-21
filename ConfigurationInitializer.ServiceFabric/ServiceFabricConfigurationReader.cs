using System;
using System.Fabric.Description;
using ConfigurationUtils;

namespace ConfigurationInitializer.ServiceFabric
{
    public class ServiceFabricConfigurationReader : IConfigurationReader
    {
        private readonly ConfigurationSettings _settings;

        public ServiceFabricConfigurationReader(ConfigurationSettings settings)
        {
            _settings = settings;
        }

        public string GetSettingValue(string settingKey)
        {
            if (string.IsNullOrEmpty(settingKey))
            {
                throw new ArgumentException($"Key is null or empty: {settingKey}", nameof(settingKey));
            }

            string[] parts = settingKey.Split(new[] {"."}, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
            {
                throw new ArgumentOutOfRangeException(nameof(settingKey), $"Setting key is expected to be composed by section name and property name, separated by a single dot, found: {parts.Length - 1} dots in key '{settingKey}'");
            }

            string section = parts[0];
            string property = parts[1];
            if (!_settings.Sections.Contains(section))
            {
                throw new ArgumentOutOfRangeException($"Settings file does not contain section: '{section}'");
            }

            if (!_settings.Sections[section].Parameters.Contains(property))
            {
                throw new ArgumentOutOfRangeException($"Section '{section}' file does not contain section: '{property}'");
            }

            return _settings.Sections[section].Parameters[property].Value;
        }
    }
}
