using NexusCore.Interfaces.AggregrateInterfaces.Widget;

namespace NexusCore.Components.Widget {
    internal class ViewerWidget : IViewerWidget {
        public event EventHandler<Packet> sent;
    }
}
