using System;
using System.Configuration;

namespace ScriptBundleTranslator.Config
{
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
}
