using System.Collections.Generic;
using System.Web.Optimization;

namespace ScriptBundleTranslator.Extensions
{
    public static class BundlerExtensions
    {
        public static void AddRange(this BundleCollection col, IEnumerable<Bundle> bundles)
        {
            foreach (Bundle bundle in bundles)
            {
                col.Add(bundle);
            }
        }
    }
}