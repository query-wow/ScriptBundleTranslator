using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Hosting;
using System.Web.Optimization;
using ScriptBundleTranslator.Config;
using ScriptBundleTranslator.Extensions;

namespace ScriptBundleTranslator.Bundling
{
    public class TranslatedBundleBuilder : IBundleBuilder
    {
        public string BuildBundleContent(Bundle bundle, BundleContext context, IEnumerable<BundleFile> files)
        {
            if (files == null)
            {
                return string.Empty;
            }
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (bundle == null)
            {
                throw new ArgumentNullException("bundle");
            }

            StringBuilder stringBuilder = new StringBuilder();

            string text = "";
            if (context.EnableInstrumentation)
            {
                text = GetBoundaryIdentifier(bundle);
                stringBuilder.AppendLine(GenerateBundlePreamble(text));
            }

            string text2 = null;

            if (!string.IsNullOrEmpty(bundle.ConcatenationToken))
            {
                text2 = bundle.ConcatenationToken;
            }
            else
            {
                foreach (IBundleTransform transform in bundle.Transforms)
                {
                    if (typeof(JsMinify).IsAssignableFrom(transform.GetType()))
                    {
                        text2 = ";" + Environment.NewLine;
                        break;
                    }
                }
            }

            if (text2 == null || context.EnableInstrumentation)
            {
                text2 = Environment.NewLine;
            }

            foreach (BundleFile file in files)
            {
                if (context.EnableInstrumentation)
                {
                    string fileHeader = GetFileHeader(file.VirtualFile, GetInstrumentedFileHeaderFormat(text));
                    stringBuilder.Append(fileHeader);
                }

                string transformedText = file.ApplyTransforms();

                if (!ScriptTranslatorSettings.Instance.IgnoreScriptCollection.ContainsKey(file.IncludedVirtualPath.NormalizeVirtualPath()))
                {
                    text = TranslateScript(transformedText);
                }
                stringBuilder.Append(text);

                stringBuilder.Append(text2);
            }
            return stringBuilder.ToString();
        }

        #region Privates

        private static string GetFileHeader(VirtualFile file, string fileHeaderFormat)
        {
            if (string.IsNullOrEmpty(fileHeaderFormat))
            {
                return string.Empty;
            }

            string applicationPath = GetApplicationPath(HostingEnvironment.VirtualPathProvider);
            return string.Format(CultureInfo.InvariantCulture, fileHeaderFormat, new object[1]
            {
            ConvertToAppRelativePath(applicationPath, file.VirtualPath)
            }) + "\r\n";
        }

        private static string GetInstrumentedFileHeaderFormat(string boundaryValue)
        {
            return "/* " + boundaryValue + " \"{0}\" */";
        }

        private static string ConvertToAppRelativePath(string appPath, string fullName)
        {
            if (string.Equals("/", appPath, StringComparison.OrdinalIgnoreCase))
            {
                return fullName;
            }
            string text = (string.IsNullOrEmpty(appPath) || !fullName.StartsWith(appPath, StringComparison.OrdinalIgnoreCase)) ? fullName : fullName.Replace(appPath, "~/");
            return text.Replace('\\', '/');
        }

        private static string GetApplicationPath(VirtualPathProvider vpp)
        {
            if (vpp != null && vpp.DirectoryExists("~"))
            {
                VirtualDirectory directory = vpp.GetDirectory("~");
                if (directory != null)
                {
                    return directory.VirtualPath;
                }
            }
            return null;
        }

        private static Dictionary<string, string> GetInstrumentedBundlePreamble(string boundaryValue)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>
            {
                ["Bundle"] = "System.Web.Optimization.Bundle",
                ["Boundary"] = boundaryValue
            };
            return dictionary;
        }

        private static string GenerateBundlePreamble(string bundleHash)
        {
            Dictionary<string, string> instrumentedBundlePreamble = GetInstrumentedBundlePreamble(bundleHash);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("/* ");
            foreach (string key in instrumentedBundlePreamble.Keys)
            {
                stringBuilder.Append(key + "=" + instrumentedBundlePreamble[key] + ";");
            }
            stringBuilder.Append(" */");
            return stringBuilder.ToString();
        }

        private static string GetBoundaryIdentifier(Bundle bundle)
        {
            Type type = (bundle.Transforms == null || bundle.Transforms.Count <= 0) ? typeof(TranslatedBundleBuilder) : bundle.Transforms[0].GetType();
            return Convert.ToBase64String(Encoding.Unicode.GetBytes(type.FullName.GetHashCode().ToString(CultureInfo.InvariantCulture)));
        }

        #region Localization

        private static Regex _regex = new Regex(@"{(.*[a-z]\/.*[a-z])}", RegexOptions.Compiled);

        /// <summary>
        /// Translates the text keys in the script file. The format is {key}.
        /// </summary>
        /// <param name="text">The text in the script file.</param>
        /// <returns>A localized version of the script</returns>
        private string TranslateScript(string text)
        {
            MatchCollection matches = _regex.Matches(text);

            //first loop for the tags
            foreach (Match match in matches)
            {
                string[] args = match.Groups[1].Value.Split('/');
                string assembly = $"{ScriptTranslatorSettings.Instance.FullName}.{args[0]}, {ScriptTranslatorSettings.Instance.Assembly}";
                ResourceManager manager = new ResourceManager(Type.GetType(assembly));
                string key = args[1];
                string translatedString = manager.GetString(key);
                if (!string.IsNullOrEmpty(translatedString))
                {
                    text = text.Replace(match.Value, CleanText(translatedString));
                }
                else
                {
                    throw new ArgumentNullException($"Couldn't find translation for key: {key}");
                }
            }

            return text;
        }

        /// <summary>
        /// Cleans the localized script text from making invalid javascript.
        /// </summary>
        private static string CleanText(string text)
        {
            text = text.Replace("'", "\\'");
            text = text.Replace("\\", "\\\\");

            return text;
        }

        #endregion Localization

        #endregion Privates
    }
}