using Microsoft.Win32;
using NexusEF.Models.Context;
using NexusOld.Logging;
using System.Globalization;

namespace PatdocQuantum {
    internal static class QuantumStartup {

        public static string[] protocols = new string[] { "Nexus", "Patdoc", "pa" };
        public static NexusOldContext context = new NexusOldContext();

        [STAThread]
        static void Main(params string[] args) {
            LoggerBla.ApplicationStarted();

            try {
                CreateProtols();

                Thread.CurrentThread.CurrentCulture = new CultureInfo("nl");

                ApplicationConfiguration.Initialize();
                Application.Run(new NexusMDIForm(args.Length != 0 ? args[0] : null));
            } catch (Exception) {
                LoggerBla.ApplicationCrashed();
                throw;
            }

            LoggerBla.ApplicationEnded();
        }

        private static void CreateProtols() {
            var applicationPath = @"C:\Users\q.croes\source\repos\NexusOld\NexusOld\bin\Debug\net7.0-windows\NexusOld.exe";
            var iconPath = @"P:\logoNexus/logoNexus-removebg-preview.ico";
            var KeyTest = Registry.CurrentUser.OpenSubKey("Software", true).OpenSubKey("Classes", true);

            foreach (var protocol in protocols) {
                RegistryKey key = KeyTest.CreateSubKey(protocol.ToLower());
                key.SetValue("URL Protocol", protocol);
                key.CreateSubKey(@"DefaultIcon").SetValue("", iconPath);
                key.CreateSubKey(@"shell\open\command").SetValue("", "\"" + applicationPath + "\" %1 externalSource");
            }

            LoggerBla.ApplicationProtocolCreated();
        }
    }
}
