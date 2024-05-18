namespace NexusEF {
    public class NexusTask {
        public CancellationTokenSource source;
        public Task task;
        public bool isCanceled;

        public NexusTask(CancellationTokenSource source, Task task) {
            this.source = source;
            this.task = task;
        }
    }
}
