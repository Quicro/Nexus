namespace NexusOld {
    internal class NexusColumn<T, F> where F : Form, new() {
        public string headerText;
        public Func<T, string>? getText;
        public Func<T, IQueryable>? getQuery;
        public Func<F, Control>? getControl;
        public Type ctrlType;
        public bool dontShowInTheGrid;
    }

    public struct NexusTag {
        public IQueryable query;
        public Type type;
    }
}
