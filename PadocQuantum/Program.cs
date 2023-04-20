using Microsoft.EntityFrameworkCore;
using PadocEF.Models.Context;
using System.Globalization;

namespace PatdocQuantum {
    internal static class Program {

        public static PatdocQuantumContext Context = new PatdocQuantumContext();

        [STAThread]
        static void Main() {

            Thread.CurrentThread.CurrentCulture = new CultureInfo("nl");

            ApplicationConfiguration.Initialize();
            Application.Run(new PadocMDIForm());
        }
    }
}