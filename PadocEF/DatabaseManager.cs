using Microsoft.EntityFrameworkCore;
using PadocEF.Models;
using PadocEF.Models.Context;

namespace PadocEF {
#if DEBUG
    public class DatabaseManager : GenericDatabaseManager<PadocQuantumContextInMemory> {
        static GenericDatabaseManager<PadocQuantumContextInMemory> databaseManager;
#endif
#if !DEBUG
        public class DatabaseManager : GenericDatabaseManager<PadocQuantumContext> {
        static GenericDatabaseManager<PadocQuantumContext> databaseManager;
#endif


    }

    public class GenericDatabaseManager<T> where T : PadocQuantumContext, new() {
#if DEBUG
        public static PadocQuantumContextInMemory context = new();
#endif
#if !DEBUG
        public static PadocQuantumContext context = new();
#endif
        public static List<PadocTask> tasks = new();

        static GenericDatabaseManager() {
            if (context is PadocQuantumContextInMemory) {
                setupInMemeryDatabase();
            }
        }

        private static void setupInMemeryDatabase() {
            context.Permission.Add(new Permission() { Name = "ALL" });
            context.Role.Add(new Role() { Name = "TESTROLE" });
            context.User.Add(new User() { Name = "TEST", Adname = "", Email = "test@ic-verzekeringen.be", Password = "", Phone = "+32 0471 12 34 56" });

            context.UserRole.Add(new UserRole() { Role = context.Role.Find(1), User = context.User.Find(1) });
            context.RolePermission.Add(new RolePermission() { Role = context.Role.Find(1), Permission = context.Permission.Find(1) });

            context.Client.Add(new Client() { Name = "TESTCLIENT", Number = "C001" });
            context.Policy.Add(new Policy() { Name = "TESTPOLICY", Number = "P001", Client = context.Client.Find(1) });
            context.Claim.Add(new Claim() { Name = "TESTCLAIM", Number = "S001", Policy = context.Policy.Find(1) });
            context.SaveChanges();
        }

        public static Task Load<T>(IQueryable<T> queryable, Action<List<T>?> actionWhenDone) {
            List<T> list = null;
            CancellationTokenSource source = new();

            Task task = new TaskFactory().StartNew(() => {
                var task = queryable.ToListAsync(source.Token);

                try {
                    task.Wait();
                    list = task.Result;
                } catch (AggregateException aggexp) {

                }
            }, source.Token);

            task.ContinueWith(
                t => actionWhenDone(list),
                TaskContinuationOptions.OnlyOnRanToCompletion
            );

            tasks.Add(
                new PadocTask(
                    source,
                    task
                )
            );

            return task;
        }
    }
}