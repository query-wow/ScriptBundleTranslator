using System.Threading;
using System.Web;
using System.Web.Optimization;

namespace ScriptBundleTranslator.Mvc
{
    public class HtmlHelpers
    {
        /// <summary>  
        /// Localized JavaScript bundle   
        /// </summary>  
        /// <param name="fileName"></param>  
        /// <returns></returns>  
        public static HtmlString LocalizedJsBundle(string fileName)
        {
            fileName = string.Concat(fileName, "_", Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName);
            HtmlString output = (HtmlString)Scripts.Render(fileName);
            return output;
        }
    }
}
