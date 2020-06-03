using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Example Config
/*
   <?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <sectionGroup name="atisWebServiceLibrarySettings">
      <section name="global" type="System.Configuration.NameValueSectionHandler"/>
      <section name="production" type="System.Configuration.NameValueSectionHandler"/>
      <section name="development" type="System.Configuration.NameValueSectionHandler"/>
      <section name="stage" type="System.Configuration.NameValueSectionHandler"/>
      </sectionGroup>
    <sectionGroup name="atisWebServiceLibrarySecureSettings">     
      <section name="production" type="System.Configuration.NameValueSectionHandler"/>
      <section name="development" type="System.Configuration.NameValueSectionHandler"/>
      <section name="stage" type="System.Configuration.NameValueSectionHandler"/>
    </sectionGroup>
  </configSections>
  
   
  <atisWebServiceLibrarySettings>
    <global>      
      <add key ="Environment" value="stage"/>   
    </global>
    <!-- Environment specific settings -->
    <production>
      <add key="AtisServiceurl" value="https://interfaces.atsc.army.mil/transcript-ws/api/"/>
      <add key="AtisUserName" value="system_sms"/>
      <add key="ConnectionString" value ="Server=cgsca4doet021; database=CIS_Prod; trusted_connection=true" />
    </production>
    <stage>
      <add key="AtisServiceurl" value="https://interfacestest.atsc.army.mil/transcript-ws/api/"/>
      <add key="AtisUserName" value="system_sms"/>
      <add key="ConnectionString" value ="Server=cgsca4doet020; database=CIS_Prod; trusted_connection=true" />
    </stage>
    <development>
      <add key="AtisServiceurl" value="https://interfacestest.atsc.army.mil/transcript-ws/api/"/>
      <add key="AtisUserName" value="system_sms"/>
      <add key="ConnectionString" value ="Server=cgsca4doet017; database=CIS_Prod; trusted_connection=true" />
    </development>
  </atisWebServiceLibrarySettings>
  <atisWebServiceLibrarySecureSettings>
    <production>    
      <add key="AtisPassword" value="$m$P30d_2o!5aUg"/>   
    </production>
    <stage>      
      <add key="AtisPassword" value="Sm3s1$tE*+_2014"/>      
    </stage>
    <development>    
      <add key="AtisPassword" value="Sm3s1$tE*+_2014"/>
    </development>
  </atisWebServiceLibrarySecureSettings>
</configuration>
 */
#endregion

namespace Cgsc.Doet.AtisWebServiceLibrary
{
   /// <summary>
   /// 
   /// </summary>
    public  class ConfigurationSettings
    {
        /// <summary>
        /// Collection of configuration values for the current environment.
        /// </summary>
        public  NameValueCollection EnvironmentConfig
        {
            get
            {
                return (NameValueCollection)ConfigurationManager.GetSection(String.Format("{0}/{1}", Resources.ConfigSection,Environment));
            }
        }
        public  NameValueCollection SecureEnvironmentConfig
        {
            get
            {
                return (NameValueCollection)ConfigurationManager.GetSection(String.Format("{0}/{1}", Resources.SecureConfigSection, Environment));
            }
        }
        /// <summary>
        /// Collection of global settings.
        /// </summary>
        public  NameValueCollection GlobalConfig
        {
            get
            {
                return (NameValueCollection)ConfigurationManager.GetSection(String.Format("{0}/global",Resources.ConfigSection));
            }
        }
        /// <summary>
        /// The username for the ATIS service. 
        /// </summary>
        public  string AtisUserName
        {
            get
            {
                return EnvironmentConfig["AtisUserName"];
            }
        }

        /// <summary>
        /// The password to the ATIS service. 
        /// </summary>
        public  string AtisPassword
        {
            get
            {
                return SecureEnvironmentConfig["AtisPassword"];
            }
        }

        /// <summary>
        /// Certificate common name used for PKI authentication
        /// </summary>
        public string CertificateCommonName
        {
            get
            {
                return SecureEnvironmentConfig["CertificateCommonName"];
            }
        }

        /// <summary>
        /// The URL of the ATIS service.
        /// </summary>
        public  string AtisServiceurl
        {
            get
            {
                return EnvironmentConfig["AtisServiceurl"];
            }
        }

       
        /// <summary>
        /// The environment to be used. Determines which SQL Server connection string will be used.
        /// </summary>
        public  string Environment
        {
            get
            {
                return GlobalConfig["Environment"];
            }
        }

        /// <summary>
        /// Returns the connection string to the database based on the value of Environment
        /// </summary>
        public  string ConnectionString
        {
            get
            {
                return EnvironmentConfig["ConnectionString"];
            }
        }

       
        /// <summary>
        /// Any custom configuration value needed.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public  object CustomConfigurationSetting(string key, string section="Global")
        {
            object returnValue = new object();

            switch (section)
            {
                case "Global":
                    returnValue = GlobalConfig[key];
                    break;
                case "Environment":
                    returnValue = EnvironmentConfig[key];
                    break;
                case "Secure":
                    returnValue = SecureEnvironmentConfig[key];
                    break;
            }
            return returnValue;
        }

        /// <summary>
        /// Returns an instance of the ConfigurationSettings class.
        /// </summary>
        private static ConfigurationSettings _settings = new ConfigurationSettings();

        /// <summary>
        /// Constructor is private to ensure the class is a singleton        
        /// </summary>
        protected ConfigurationSettings()
        {

        }
        /// <summary>
        /// Factory method to return the singleton instance of ConfiguratinSettings
        /// </summary>
        /// <returns></returns>
        public static ConfigurationSettings Settings
        {
            get
            {
                return _settings;
            }
        }
    }
}
