using NexusCore.Interfaces.AggregrateInterfaces.Forms;
using NexusCore.Interfaces.Widgets;

namespace NexusCore.Components.AggregrateInterfaces.Controller
{
    public class BackgroundController : IBackgroundController
    {
        public List<IElementWidget> widgets { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void handle(Packet packet)
        {
            throw new NotImplementedException();
        }
    }
}