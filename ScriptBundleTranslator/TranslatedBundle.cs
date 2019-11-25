using System.Web.Optimization;

namespace ScriptBundleTranslator.Bundling
{
    public class TranslatedBundle : Bundle
    {
        public TranslatedBundle(string virtualPath) : base(virtualPath)
        {
            Builder = new TranslatedBundleBuilder();
        }
    }
}