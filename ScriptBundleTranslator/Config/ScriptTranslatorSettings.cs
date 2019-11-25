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
                    _s = (ScriptTranslatorSettings)ConfigurationManager.GetSection("ScriptBundleTranslator/Settings");
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

    [ConfigurationCollection(typeof(IgnoreScriptElement), AddItemName = "add")]
    public class IgnoreScriptElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new IgnoreScriptElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            return ((IgnoreScriptElement)element).Path;
        }

        public bool ContainsKey(string path)
        {
            return Array.ConvertAll(BaseGetAllKeys(), x => x.ToString()).Contains(path);
        }
    }

    public class IgnoreScriptElement : ConfigurationElement
    {
        [ConfigurationProperty("Path", IsKey = true, IsRequired = true)]
        public string Path => (string)base["Path"];
    }
}