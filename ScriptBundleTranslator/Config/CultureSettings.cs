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
                    _s = (CultureSettings)ConfigurationManager.GetSection("ScriptBundleTranslator/Cultures");
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

    [ConfigurationCollection(typeof(CultureElement), AddItemName = "add")]
    public class CultureElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new CultureElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            return ((CultureElement)element).Key;
        }
    }

    public class CultureElement : ConfigurationElement
    {
        [ConfigurationProperty("Name")]
        public string Name => (string)base["Name"];

        [ConfigurationProperty("Key", IsKey = true, IsRequired = true)]
        public string Key => (string)base["Key"];

        [ConfigurationProperty("TwoLetterIso", IsRequired = true)]
        public string TwoLetterIso => (string)base["TwoLetterIso"];
    }
}