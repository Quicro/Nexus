using Microsoft.EntityFrameworkCore;
using PadocEF.Models.Context;
using System.Globalization;

namespace PatdocQuantum {
    internal static class Program {

        public static PatdocQuantumContext Context = new PatdocQuantumContext();

        [STAThread]
        static void Main() {
            /* using (PatdocQuantumContext pqC = new PatdocQuantumContext()) {

                 //pqC.Policy.Add(new Policy() { Name = "Test", Number = "T0001" });
                 pqC.SaveChanges();



                 var policies = pqC.Policy.ToList();
             }*/

            var usersWithAllPermission = new PatdocQuantumContext().User
    .Where(u => u.UserRole
        .Any(ur => ur.Role.RolePermission
            .Any(rp => rp.Permission.Name == "ALL")))
    .ToQueryString();

            var policiesOfClient004 = new PatdocQuantumContext().Policy
    .Where(p => p.ClientId == "004")
    .ToQueryString();


            var options = new DbContextOptionsBuilder<PatdocQuantumContext>()
                .Options;


            Thread.CurrentThread.CurrentCulture = new CultureInfo("nl");

            ApplicationConfiguration.Initialize();
            Application.Run(new PadocMDIForm());
        }
    }
}