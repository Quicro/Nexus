using NexusCore.Components.Controller;
using NexusCore.Interfaces;
using NexusCore.Interfaces.AggregrateInterfaces.Forms;
using NexusCore.Interfaces.Widgets;

namespace NexusCore.Components.Forms {
    public class EditorForm : IEditorForm {
        IController IControlledForm.controller {
            get => editorController;
            set => editorController = (EditorController)value;
        }
        public EditorController editorController { get; set; }
        public List<IElementWidget> widgets { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler OnDataLoading;
        public event EventHandler OnDataLoaded;
        public event EventHandler OnDataLoadCancelled;
        public event EventHandler OnOpen;
        public event EventHandler OnClose;


        public void Start() {

        }

        public void Open() {

        }

        public void Close() {


        }
        public void Stop() {

        }

        public void LoadData() {

        }

    }
}
