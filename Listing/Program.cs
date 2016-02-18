using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Listing
{
    internal class Program
    {
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/system.reflection.assembly.getreferencedassemblies.aspx
        /// </summary>

        #region Main

        private static void Main()
        {
            Console.WindowHeight = 50;
            Console.WindowWidth = 120;

            // This variable holds the amount of indenting that
            // should be used when displaying each line of information.
            int indent = 0;

            Assembly assembly = typeof(Program).Assembly;
            Version version = assembly.GetName().Version;
            Display(indent, nameof(version).ToUpperInvariant()[0] + nameof(version).ToLowerInvariant().Substring(1)
                + "\t" + version);

            // Display the set of assemblies our assemblies reference.
            Display(indent, string.Empty);
            Display(indent, string.Empty);
            Display(indent, string.Empty);
            Display(indent, "Assemblies:");
            Display(indent, string.Empty);
            string path = Path.GetDirectoryName(assembly.Location) + Path.DirectorySeparatorChar;
            foreach (string str in Directory.GetFiles(path, "Penelope.*.dll", SearchOption.AllDirectories))
            {
                Display(indent + 1, Assembly.LoadFrom(str).FullName.Replace(", ", "\t"));
            }
            Display(indent, string.Empty);
            Display(indent, "--END--");
            Console.ReadLine();
        }

        #endregion Main

        #region Methods

        // Display a formatted string indented by the specified amount.
        public static void Display(int indent, string format, params object[] param)
        {
            Console.Write(new string(' ', indent * 3));
            Console.WriteLine(format, param);
        }

        #endregion Methods
    }
}
