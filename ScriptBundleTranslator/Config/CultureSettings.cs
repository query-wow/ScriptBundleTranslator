using System;
using System.Configuration;

namespace ScriptBundleTranslator.Config
{
    public class CultureSettings : ConfigurationSection
    {
        public static CultureSettings Instance
        {
            get
            {
                if (_s == null)
                {
                    _s = (CultureSettings)ConfigurationManager.GetSection("ScriptTranslatorSettings/Cultures");
                }

                return _s;
            }
        }

        private static CultureSettings _s;

        [ConfigurationProperty("", IsDefaultCollection = true)]
        public CultureElementCollection CultureCollection
        {
            get
            {
                return (CultureElementCollection)base[""];
            }
        }
    }
}