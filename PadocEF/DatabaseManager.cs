using Microsoft.EntityFrameworkCore;
using PadocEF.Models.Context;

namespace PadocEF {
    public static class DatabaseManager {
        public static PatdocQuantumContext context;
        public static List<Task> tasks;

        static DatabaseManager() {
            context = new();
            tasks = new();
        }

        public static void Load<T>(IQueryable<T> queryable, Action<Task, object?> actionWhenDone, CancellationToken token) {
            List<T> list = null;

            Task task = new TaskFactory().StartNew(() => {
                //Thread.Sleep(3000);
                var task = queryable.ToListAsync(token);
                tasks.Add(task);

                try {
                    task.Wait();
                    list = task.Result;
                } catch (AggregateException aggexp) {
                    
                }
            }, token);

            task.ContinueWith(
                t => actionWhenDone(t, list), 
                TaskContinuationOptions.OnlyOnRanToCompletion
            );
            tasks.Add(task);
        }
    }
}