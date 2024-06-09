using NexusCore.Interfaces.AggregrateInterfaces.Widget;
using NexusCore.Interfaces.Widgets;

namespace NexusCore.Components.Widget {
    public class editorForm : IeditorForm {
        public editorForm() {
            this.widgets = new();
        }

        public List<IWidget> widgets { get; set ; }

        public event EventHandler<Packet> sent;
    }
}
