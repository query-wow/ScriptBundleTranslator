using System;
using System.Configuration;
using System.Linq;

namespace ScriptBundleTranslator.Config
{
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
}
