using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PadocEF.Models;
using PadocEF.Models.Context;
using System.Globalization;

namespace PatdocQuantum {
    internal static class Program {

        public static PatdocQuantumContext Context = new PatdocQuantumContext();

        [STAThread]
        static void Main() {
            using (PatdocQuantumContext pqC = new PatdocQuantumContext()) {

                pqC.Policy.Add(new Policy() { Name = "Test", Number = "T0001" });
                pqC.SaveChanges();


                
                var policies = pqC.Policy.ToList();
            }



            Thread.CurrentThread.CurrentCulture = new CultureInfo("nl");

            ApplicationConfiguration.Initialize();
            Application.Run(new PadocMDIForm());
        }
    }
}