using Microsoft.EntityFrameworkCore;
using PadocEF.Models.Context;

namespace PadocEF {
    public static class DatabaseManager {
        public static PatdocQuantumContext context;
        public static List<PadocTask> tasks;

        static DatabaseManager() {
            context = new();
            tasks = new();
        }

        public static Task Load<T>(IQueryable<T> queryable, Action<Task, object?> actionWhenDone, CancellationTokenSource source) {
            List<T> list = null;

            Task task = new TaskFactory().StartNew(() => {
                //Thread.Sleep(10000);
                var task = queryable.ToListAsync(source.Token);


                try {
                    task.Wait();
                    list = task.Result;
                } catch (AggregateException aggexp) {

                }
            }, source.Token);

            task.ContinueWith(
                t => actionWhenDone(t, list),
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

        public class PadocTask {
            public CancellationTokenSource source;
            public Task task;
            public bool isCanceled;

            public PadocTask(CancellationTokenSource source, Task task) {
                this.source = source;
                this.task = task;
            }
        }

    }
}