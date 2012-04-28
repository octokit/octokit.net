using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Burr.Tests
{
    /// <summary>
    /// Helper class for handling our embedded resources.
    /// </summary>
    public class EmbeddedResource
    {
        public EmbeddedResource(Assembly assembly, string resourceName)
            : this(assembly, resourceName, null)
        {
        }

        public EmbeddedResource(Assembly assembly, string resourceName, string fileName)
        {
            Assembly = assembly;
            ResourceName = resourceName;
            FileName = fileName;
        }

        public string ResourceName { get; private set; }
        public string FileName { get; private set; }
        public Assembly Assembly { get; private set; }

        public string ExtractToFile(string directoryPath)
        {
            string outputPath = Path.Combine(directoryPath, FileName);

            var fi = new FileInfo(outputPath);
            if (fi.Exists)
            {
                return fi.FullName;
            }

            try
            {
                using (var stream = GetResourceStream())
                using (var output = File.Create(fi.FullName))
                {
                    if (stream == null) throw new InvalidOperationException("This should never happen in the wild and should be caught by our automated tests");
                    stream.CopyTo(output);
                }

                return fi.FullName;
            }
            catch (IOException ex)
            {
                return null;
            }
        }

        public string GetResourceAsString(Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.Default;

            using (var sr = new StreamReader(GetResourceStream(), encoding))
            {
                return sr.ReadToEnd();
            }
        }

        public string ExtractNativeDll(string directoryPath)
        {
            return EnsureNativeDllIsInPath(ExtractToFile(directoryPath));
        }

        public static string EnsureNativeDllIsInPath(string filePath)
        {
            var fi = new FileInfo(filePath);

            // NB: This is necessary for unit test runners 
            // (*cough* Resharper *cough*) that love to put DLLs in bizarre 
            // places. Make sure the native DLL loader can find git2.dll
            var dir = fi.Directory.FullName;

            if (!Environment.GetEnvironmentVariable("PATH").Split(Path.PathSeparator).Contains(dir))
            {
                Environment.SetEnvironmentVariable("PATH",
                    String.Format("{0}{1}{2}", Environment.GetEnvironmentVariable("PATH"), Path.PathSeparator, dir));
            }

            return fi.FullName;
        }

        public Stream GetResourceStream()
        {
            var assembly = Assembly ?? Assembly.GetExecutingAssembly();
            return assembly.GetManifestResourceStream(ResourceName);
        }

        public override string ToString()
        {
            return Assembly.FullName + " " + ResourceName;
        }
    }
}
