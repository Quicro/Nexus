using NexusCore.Interfaces;
using NexusCore.Interfaces.AggregrateInterfaces.Forms;

namespace NexusCore.Components.Forms {
    public class EditorForm : IEditorForm {
        public IController controller { get; set; }

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
