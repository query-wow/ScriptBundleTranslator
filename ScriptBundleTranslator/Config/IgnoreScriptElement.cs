using System.Configuration;

namespace ScriptBundleTranslator.Config
{
    public class IgnoreScriptElement : ConfigurationElement
    {
        [ConfigurationProperty("Path", IsKey = true, IsRequired = true)]
        public string Path => (string)base["Path"];
    }
}
