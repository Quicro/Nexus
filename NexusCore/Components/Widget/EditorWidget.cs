using NexusCore.Interfaces.AggregrateInterfaces.Widget;
using NexusCore.Interfaces.Widgets;

namespace NexusCore.Components.Widget {
    public class EditorWidget : IEditorWidget {
        public List<IWidget> widgets { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler<Packet> sent;
    }
}
