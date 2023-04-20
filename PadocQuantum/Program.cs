using Microsoft.Win32;
using PadocEF.Models.Context;
using System.Globalization;

namespace PatdocQuantum {
    internal static class Program {

        public static string[] protocols = new string[] { "Padoc", "Patdoc", "pa" };
        public static PatdocQuantumContext Context = new PatdocQuantumContext();

        [STAThread]
        static void Main(params string[] args) {
            CreateProtols();

            Thread.CurrentThread.CurrentCulture = new CultureInfo("nl");

            ApplicationConfiguration.Initialize();
            Application.Run(new PadocMDIForm(args.Length != 0 ? args[0] : null));
        }

        private static void CreateProtols() {
            var applicationPath = @"C:\Users\q.croes\source\repos\PadocQuantum\PadocQuantum\bin\Debug\net7.0-windows\PadocQuantum.exe";
            var iconPath = @"P:\logoPadoc/logoPadoc-removebg-preview.ico";
            var KeyTest = Registry.CurrentUser.OpenSubKey("Software", true).OpenSubKey("Classes", true);

            foreach (var protocol in protocols) {
                RegistryKey key = KeyTest.CreateSubKey(protocol.ToLower());
                key.SetValue("URL Protocol", protocol);
                key.CreateSubKey(@"DefaultIcon").SetValue("", iconPath);
                key.CreateSubKey(@"shell\open\command").SetValue("", "\"" + applicationPath + "\" %1 externalSource");
            }
        }
    }
}