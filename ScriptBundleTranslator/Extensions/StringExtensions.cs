using System;

namespace ScriptBundleTranslator.Extensions
{
    public static class StringExtensions
    {
        public static string NormalizeVirtualPath(this string virtualPath)
        {
            if (virtualPath == null)
                throw new ArgumentNullException("VirtualPath cannot be null");

            return virtualPath.Replace("~", string.Empty);
        }
    }
}
