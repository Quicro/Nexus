using NexusCore.Interfaces;

namespace NexusCore.Components.Forms {
    public class ViewerForm : IControlledForm {
        public IController controller { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler OnDataLoading;
        public event EventHandler OnDataLoaded;
        public event EventHandler OnDataLoadCancelled;
        public event EventHandler OnOpen;
        public event EventHandler OnClose;

        public void Close() {
            throw new NotImplementedException();
        }

        public void End() {
            throw new NotImplementedException();
        }

        public void LoadData() {
            throw new NotImplementedException();
        }

        public void Open() {
            throw new NotImplementedException();


        }

        public void Start() {
            throw new NotImplementedException();
        }
    }
}
