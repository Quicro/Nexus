using Microsoft.EntityFrameworkCore;
using NexusEF.Models;
using NexusEF.Models.Context;

namespace NexusEF {
#if DEBUG
    public class DatabaseManager : GenericDatabaseManager<NexusOldContextInMemory> {
        private static readonly GenericDatabaseManager<NexusOldContext>? databaseManager;
#endif
#if !DEBUG
    public class DatabaseManager : GenericDatabaseManager<NexusOldContext> {
        static GenericDatabaseManager<NexusOldContext> databaseManager;
#endif
    }

    public class GenericDatabaseManager<T> where T : NexusOldContext, new() {
#if DEBUG
        public static NexusOldContextInMemory context = new();
#endif
#if !DEBUG
        public static NexusOldContext context = new();
#endif
        public static List<NexusTask> tasks = new();

        static GenericDatabaseManager() {
            if (context is not null) {
                setupInMemeryDatabase();
            }
        }

        private static void setupInMemeryDatabase() {
            context.Permission.Add(new Permission() { Name = "ALL" });
            context.Role.Add(new Role() { Name = "TESTROLE" });
            context.User.Add(new User() { Name = "Q", Adname = "q.croes", Email = "q.croes@test.be", Password = "", Phone = "+32 0471 12 34 56" });

            context.UserRole.Add(new UserRole() { Role = context.Role.Find(1), User = context.User.Find(1) });
            context.RolePermission.Add(new RolePermission() { Role = context.Role.Find(1), Permission = context.Permission.Find(1) });

            context.Client.Add(new Client() { Name = "TESTCLIENT", Number = "C001" });
            context.Policy.Add(new Policy() { Name = "TESTPOLICY", Number = "P001", Client = context.Client.Find(1) });
            context.Claim.Add(new Claim() { Name = "TESTCLAIM", Number = "S001", Policy = context.Policy.Find(1) });
            context.SaveChanges();
        }

        public static Task Load<T>(IQueryable<T> queryable, Action<List<T>?> actionWhenDone) {
            List<T>? list = null;
            CancellationTokenSource source = new();

            Task task = new TaskFactory().StartNew(() => {
                Task<List<T>> task = queryable.ToListAsync(source.Token);

                try {
                    task.Wait();
                    list = task.Result;
                } catch (AggregateException) {
                    //LoggerBla.DatabaseManagerError(aggexp.Message);
                }
            }, source.Token);

            task.ContinueWith(
                t => actionWhenDone(list),
                TaskContinuationOptions.OnlyOnRanToCompletion
            );

            tasks.Add(
                new NexusTask(
                    source,
                    task
                )
            );

            return task;
        }
    }
}
