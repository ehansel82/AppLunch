To prevent private keys from being stored in public source control, AppSettings and Connection strings are stored in non-source controlled files.

This is a reference to the settings files with the critical data removed.

The files are appSettings.config and connections.config respectively.

<appSettings>
  <add key=""/>
  <add key="fromAddress" value=""/>
  <add key="InstrumentationKey" value="" />
  <add key="webpages:Version" value="3.0.0.0" />
  <add key="webpages:Enabled" value="false" />
  <add key="ClientValidationEnabled" value="true" />
  <add key="UnobtrusiveJavaScriptEnabled" value="true" />
</appSettings>

<connectionStrings>
  <add name="DefaultConnection" connectionString="" providerName="" />
</connectionStrings>