
using NexusCore.Interfaces.AggregrateInterfaces.Controller;
using NexusCore.Interfaces.Widgets;

namespace NexusCore.Components.AggregrateInterfaces.Widgets
{
    public class EditorWidget : IEditorWidget
    {
        public List<IWidget> widgets { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler<Packet> sent;
    }
}
