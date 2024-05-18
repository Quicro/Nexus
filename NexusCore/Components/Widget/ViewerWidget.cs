using NexusCore.Interfaces.AggregrateInterfaces.Controller;

namespace NexusCore.Components.AggregrateInterfaces.Widget
{
    internal class ViewerWidget : IViewerWidget
    {
        public event EventHandler<Packet> sent;
    }
}
