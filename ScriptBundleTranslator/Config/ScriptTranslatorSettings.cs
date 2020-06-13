using System;
using System.Configuration;
using System.Linq;

namespace ScriptBundleTranslator.Config
{
    public class ScriptTranslatorSettings : ConfigurationSection
    {
        public static ScriptTranslatorSettings Instance
        {
            get
            {
                if (_s == null)
                {
                    _s = (ScriptTranslatorSettings)ConfigurationManager.GetSection("ScriptTranslatorSettings/Settings");
                }

                return _s;
            }
        }

        private static ScriptTranslatorSettings _s;

        /// <summary>Full Assembly Name</summary>
        [ConfigurationProperty("Assembly", IsRequired = true)]
        public string FullName => (string)base["Assembly"];

        /// <summary>Assembly Name, only contains the Project name</summary>
        public string Assembly => FullName.Split('.')[0];

        [ConfigurationProperty("IgnoreFiles", IsDefaultCollection = true)]
        public IgnoreScriptElementCollection IgnoreScriptCollection => (IgnoreScriptElementCollection)base["IgnoreFiles"];
    }
}