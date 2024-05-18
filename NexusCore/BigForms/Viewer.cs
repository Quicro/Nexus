using NexusCore.Interfaces;

namespace NexusCore.BigForms {
    public partial class Viewer : Form, IViewerForm {
        public Viewer() {
            InitializeComponent();
        }

        public event EventHandler OnDataLoading;
        public event EventHandler OnDataLoaded;
        public event EventHandler OnDataLoadCancelled;
        public event EventHandler OnOpen;
        public event EventHandler OnClose;

        public void End() {
            throw new NotImplementedException();
        }

        public void LoadData() {
            throw new NotImplementedException();
        }

        public void Open() {
            throw new NotImplementedException();
        }


        public void Start(List<MenuItem> menu) {
            throw new NotImplementedException();
        }
    }
}
