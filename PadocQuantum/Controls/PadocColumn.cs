namespace PadocQuantum {
    internal class PadocColumn<T, F> where F : Form, new() {
        public string headerText;
        public Func<T, string>? getText;
        public Func<T, IQueryable>? getQuery;
        public Func<F, Control>? getControl;
        public Type ctrlType;
        public bool dontShowInTheGrid;
    }

    public struct PadocTag {
        public IQueryable query;
        public Type type;
    }
}
