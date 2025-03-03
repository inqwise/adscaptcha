using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace CreditGuard
{
    public class CreditGuardConfiguration : ConfigurationSection
    {
        public static CreditGuardConfiguration Get()
        {
            return (CreditGuardConfiguration)ConfigurationManager.GetSection("creditGuard");
        }

        [ConfigurationProperty("version", IsRequired = true)]
        public ConfigElement Version
        {
            get
            {
                return (ConfigElement)this["version"];
            }
        }

        [ConfigurationProperty("debug", IsRequired = true)]
        public ConfigElement Debug
        {
            get
            {
                return (ConfigElement)this["debug"];
            }
        }
        
        [ConfigurationProperty("terminalURI", IsRequired = true)]
        public ConfigElement TerminalURI
        {
            get
            {
                return (ConfigElement)this["terminalURI"];
            }
        }

        [ConfigurationProperty("terminalNumber", IsRequired = true)]
         public ConfigElement TerminalNumber
        {
            get
            {
                return (ConfigElement)this["terminalNumber"];
            }
        }

        [ConfigurationProperty("terminalUser", IsRequired = true)]
        public ConfigElement TerminalUser
        {
            get
            {
                return (ConfigElement)this["terminalUser"];
            }
        }

        [ConfigurationProperty("terminalPass", IsRequired = true)]
        public ConfigElement TerminalPass
        {
            get
            {
                return (ConfigElement)this["terminalPass"];
            }
        }
    }

    public class ConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get
            {
                return this["value"] as string;
            }
        }
    }
}
