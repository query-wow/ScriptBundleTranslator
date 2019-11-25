using System.Configuration;

namespace ScriptBundleTranslator.Config
{
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
