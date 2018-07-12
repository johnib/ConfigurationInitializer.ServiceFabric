[README] https://github.com/ordanon/ConfigurationInitializer/blob/master/README.md

# How to use?
Assume a SF Service with the following `Config/Settings.xml` file:

```XML
<?xml version="1.0" encoding="utf-8"?>
<Settings xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
          xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
          xmlns="http://schemas.microsoft.com/2011/01/fabric">

  <Section Name="ApplicationInsights">
    <Parameter Name="InstrumentationKey" Value="" MustOverride="true" />
  </Section>
  <Section Name="BlobPersistency">
    <Parameter Name="StorageAccounts" Value="['storage1', 'storage2']" MustOverride="true" />
  </Section>
  <Section Name="AAD">
    <Parameter Name="TenantId" Value="" MustOverride="true" />
    <Parameter Name="ClientId" Value="" MustOverride="true" />
    <Parameter Name="CertificateThumbprint" Value="" MustOverride="true" />
  </Section>
</Settings>
```

Then the settings class representing the that config file would look like:

```CSharp
public class SchedulerJobSettings : ConfigurationInitializer
{
    public SchedulerJobSettings(ConfigurationSettings settings)
        : base(new ServiceFabricConfigurationReader(settings))
    {
    }

    [ConfigKey("ApplicationInsights.InstrumentationKey")]
    public string AppInsightsInstrumentationKey { get; set; }

    [ConfigKey("BlobPersistency.StorageAccounts")]
    public List<string> BlobPersistencyStoragesConnectionStrings { get; set; }

    [ConfigKey("AAD.TenantId")]
    public string AadTenantId { get;set; }

    [ConfigKey("AAD.ClientId")]
    public string AadClientId { get;set; }

    [ConfigKey("AAD.CertificateThumbprint")]
    public string AadCertificateThumbprint { get; set; }
}
```