using Microsoft.EntityFrameworkCore;
using NexusEF.Models;
using NexusEF.Models.Context;

namespace NexusEF {
#if DEBUG
    public class DatabaseManager : GenericDatabaseManager<NexusContextInMemory> {
        private static readonly GenericDatabaseManager<NexusContext>? databaseManager;
#endif
#if !DEBUG
    public class DatabaseManager : GenericDatabaseManager<NexusContext> {
        static GenericDatabaseManager<NexusContext> databaseManager;
#endif
    }

    public class GenericDatabaseManager<T> where T : NexusContext, new() {
#if DEBUG
        public static NexusContextInMemory context = new();
#endif
#if !DEBUG
        public static NexusContext context = new();
#endif
        public static List<NexusTask> tasks = new();

        static GenericDatabaseManager() {
            if (context is not null) {
                setupInMemeryDatabase();
            }
        }

        private static void setupInMemeryDatabase() {
            context.Permission.Add(new Permission() { Name = "ALL" });
            context.Permission.Add(new Permission() { Name = "READ" });
            context.Permission.Add(new Permission() { Name = "WRITE" });
            context.Permission.Add(new Permission() { Name = "DELETE" });
            context.Permission.Add(new Permission() { Name = "EXECUTE" });

            context.Role.Add(new Role() { Name = "Test" }); 
            context.Role.Add(new Role() { Name = "Admin" });
            context.Role.Add(new Role() { Name = "Manager" });
            context.Role.Add(new Role() { Name = "Analyst" });
            context.Role.Add(new Role() { Name = "Clerk" });

            context.User.Add(new User() { Name = "Q", Adname = "q.croes", Email = "q.croes@test.be", Password = "", Phone = "+32 0471 12 34 56" });
            context.User.Add(new User() { Name = "Alice Johnson", Adname = "alice.j", Email = "alice.j@test.be", Password = "password1", Phone = "+32 0471 23 45 67" });
            context.User.Add(new User() { Name = "Bob Smith", Adname = "bob.smith", Email = "bob.smith@test.be", Password = "password2", Phone = "+32 0471 34 56 78" });
            context.User.Add(new User() { Name = "Charlie Brown", Adname = "charlie.b", Email = "charlie.b@test.be", Password = "password3", Phone = "+32 0471 45 67 89" });

            context.UserRole.Add(new UserRole() { Role = context.Role.Find(1), User = context.User.Find(1) }); 
            context.UserRole.Add(new UserRole() { Role = context.Role.Find(2), User = context.User.Find(2) }); // Bob -> Manager
            context.UserRole.Add(new UserRole() { Role = context.Role.Find(3), User = context.User.Find(3) }); // Charlie -> Analyst
            context.UserRole.Add(new UserRole() { Role = context.Role.Find(4), User = context.User.Find(4) }); // Diana -> Clerk

            context.RolePermission.Add(new RolePermission() { Role = context.Role.Find(1), Permission = context.Permission.Find(1) }); 
            context.RolePermission.Add(new RolePermission() { Role = context.Role.Find(1), Permission = context.Permission.Find(2) }); // Admin -> WRITE
            context.RolePermission.Add(new RolePermission() { Role = context.Role.Find(1), Permission = context.Permission.Find(3) }); // Admin -> DELETE
            context.RolePermission.Add(new RolePermission() { Role = context.Role.Find(1), Permission = context.Permission.Find(4) }); // Admin -> EXECUTE
            context.RolePermission.Add(new RolePermission() { Role = context.Role.Find(2), Permission = context.Permission.Find(2) }); // Manager -> WRITE
            context.RolePermission.Add(new RolePermission() { Role = context.Role.Find(3), Permission = context.Permission.Find(1) }); // Analyst -> READ


            context.Client.Add(new Client() { Name = "TESTCLIENT", Number = "C001" }); 
            context.Client.Add(new Client() { Name = "Global Insurance", Number = "C002" });
            context.Client.Add(new Client() { Name = "Secure Life", Number = "C003" });
            context.Client.Add(new Client() { Name = "Safe Auto", Number = "C004" });
            context.Client.Add(new Client() { Name = "OmniHealth", Number = "C005" });

            context.Policy.Add(new Policy() { Name = "TESTPOLICY", Number = "P001", Client = context.Client.Find(1) });
            context.Policy.Add(new Policy() { Name = "Fire Insurance", Number = "P002", Client = context.Client.Find(2) });
            context.Policy.Add(new Policy() { Name = "Life Insurance", Number = "P003", Client = context.Client.Find(3) });
            context.Policy.Add(new Policy() { Name = "Car Insurance", Number = "P004", Client = context.Client.Find(4) });
            context.Policy.Add(new Policy() { Name = "Omnium Insurance", Number = "P005", Client = context.Client.Find(5) });
            context.Policy.Add(new Policy() { Name = "Health Insurance", Number = "P006", Client = context.Client.Find(5) });

            context.Claim.Add(new Claim() { Name = "TESTCLAIM", Number = "S001", Policy = context.Policy.Find(1) });
            context.Claim.Add(new Claim() { Name = "Fire Damage Claim", Number = "S002", Policy = context.Policy.Find(2) });
            context.Claim.Add(new Claim() { Name = "Life Insurance Claim", Number = "S003", Policy = context.Policy.Find(3) });
            context.Claim.Add(new Claim() { Name = "Car Accident Claim", Number = "S004", Policy = context.Policy.Find(4) });
            context.Claim.Add(new Claim() { Name = "Full Coverage Claim", Number = "S005", Policy = context.Policy.Find(5) });
            context.Claim.Add(new Claim() { Name = "Health Claim", Number = "S006", Policy = context.Policy.Find(6) });
            context.Claim.Add(new Claim() { Name = "Theft Claim", Number = "S007", Policy = context.Policy.Find(4) });
            context.Claim.Add(new Claim() { Name = "Natural Disaster Claim", Number = "S008", Policy = context.Policy.Find(2) });
            context.Claim.Add(new Claim() { Name = "Life Coverage Claim", Number = "S009", Policy = context.Policy.Find(3) });
            context.Claim.Add(new Claim() { Name = "Medical Expense Claim", Number = "S010", Policy = context.Policy.Find(6) });
            context.Claim.Add(new Claim() { Name = "Collision Claim", Number = "S011", Policy = context.Policy.Find(4) });
            context.Claim.Add(new Claim() { Name = "Health Wellness Claim", Number = "S012", Policy = context.Policy.Find(6) });
            context.Claim.Add(new Claim() { Name = "Fire Incident Claim", Number = "S013", Policy = context.Policy.Find(2) });
            context.Claim.Add(new Claim() { Name = "Accident Coverage Claim", Number = "S014", Policy = context.Policy.Find(4) });
            context.Claim.Add(new Claim() { Name = "Omnium Coverage Claim", Number = "S015", Policy = context.Policy.Find(5) });
            context.Claim.Add(new Claim() { Name = "Life Benefit Claim", Number = "S016", Policy = context.Policy.Find(3) });
            context.Claim.Add(new Claim() { Name = "Comprehensive Coverage Claim", Number = "S017", Policy = context.Policy.Find(5) });
            context.Claim.Add(new Claim() { Name = "Routine Checkup Claim", Number = "S018", Policy = context.Policy.Find(6) });
            context.Claim.Add(new Claim() { Name = "Major Surgery Claim", Number = "S019", Policy = context.Policy.Find(6) });
            context.Claim.Add(new Claim() { Name = "Total Loss Claim", Number = "S020", Policy = context.Policy.Find(4) });

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
