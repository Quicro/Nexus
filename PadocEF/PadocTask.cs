namespace PadocEF {
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