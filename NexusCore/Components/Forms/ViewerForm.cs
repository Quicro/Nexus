using NexusCore.Components.Controller;
using NexusCore.Interfaces;
using NexusCore.Interfaces.AggregrateInterfaces.Forms;
using NexusCore.Interfaces.Widgets;

namespace NexusCore.Components.Forms {
    public class ViewerForm : IViewerForm {
        IController IControlledForm.controller {
            get => viewerController;
            set => viewerController = (ViewerController)value;
        }
        public ViewerController viewerController { get; set; }
        public List<IElementWidget> widgets { get; set; } = new();

        public event EventHandler OnOpen;
        public event EventHandler OnClose;

        public void Open() {
            OnOpen?.Invoke(this, EventArgs.Empty);
        }

        public void Close() {
            OnClose?.Invoke(this, EventArgs.Empty);
        }
    }
}
