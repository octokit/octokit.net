using System.IO;
using System.Reflection;
using System.Text;

namespace Octokit.Tests
{
    /// <summary>
    /// Helper class for handling our embedded resources.
    /// </summary>
    public class EmbeddedResource
    {
        public EmbeddedResource(Assembly assembly, string resourceName)
        {
            Assembly = assembly;
            ResourceName = resourceName;
        }

        public string ResourceName { get; private set; }
        public Assembly Assembly { get; private set; }

        public string GetResourceAsString(Encoding encoding = null)
        {
            encoding = encoding ??
#if HAS_DEFAULT_ENCODING
                Encoding.Default;
#else
                // http://stackoverflow.com/questions/35929391/how-can-i-determine-the-default-encoding-in-a-portable-class-library
                Encoding.GetEncoding(0);
#endif

            using (var sr = new StreamReader(GetResourceStream(), encoding))
            {
                return sr.ReadToEnd();
            }
        }

        public Stream GetResourceStream()
        {
            var assembly = Assembly ?? typeof(EmbeddedResource).GetTypeInfo().Assembly;
            return assembly.GetManifestResourceStream(ResourceName);
        }

        public override string ToString()
        {
            return Assembly.FullName + " " + ResourceName;
        }
    }
}
