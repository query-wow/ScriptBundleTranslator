using System;
using System.Collections.Generic;
using System.Web.Optimization;
using ScriptBundleTranslator.Config;

namespace ScriptBundleTranslator.Bundling
{
    public static class ScriptTranslationBundle
    {
        public static TranslatedBundle[] Create(string virtualPath, params string[] scriptPaths)
        {
            List<TranslatedBundle> bundles = new List<TranslatedBundle>();

            foreach (CultureElement culture in CultureSettings.Instance.CultureCollection)
            {
                TranslatedBundle b = new TranslatedBundle(string.Format("{0}_{1}", virtualPath, culture.TwoLetterIso));
                b.Include(scriptPaths);
                b.Transforms.Add(new JsMinify());
                bundles.Add(b);
            }

            return bundles.ToArray();
        }
    }
}