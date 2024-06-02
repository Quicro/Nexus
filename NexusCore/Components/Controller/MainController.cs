using NexusCore.Interfaces;
using NexusCore.Interfaces.AggregrateInterfaces.Controller;
using NexusCore.Interfaces.Widgets;

namespace NexusCore.Components.Controller {
    /// <summary>
    /// Main controller that handles packet reception.
    /// </summary>
    public class MainController : IMainController, IPacketReceiver {
        public List<IElementWidget> widgets { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Handles the specified packet.
        /// </summary>
        /// <param name="packet">The packet to handle.</param>
        public void handle(Packet packet) {
            throw new NotImplementedException();
        }
    }
}
