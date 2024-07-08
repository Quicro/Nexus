using Microsoft.EntityFrameworkCore;

namespace NexusEF.DatabaseManager {
    public class DatabaseManager<T> : IDatabaseManager where T : DbContext, new() {

        public object getTableExtention<T>() {
            throw new NotImplementedException();
        }
        public DbContext context { get; set; }
        private T _context = new();


        public static List<NexusTask> tasks = new();

        public DatabaseManager() {
            if (_context is not null) {
                setupInMemeryDatabase();
            }
        }

        private void setupInMemeryDatabase() {
            _ = _context.SaveChanges();
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
                    //LoggerBla.NexusDatabaseManagerError(aggexp.Message);
                }
            }, source.Token);

            _ = task.ContinueWith(
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
