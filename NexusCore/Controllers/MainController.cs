using NexusCore.Interfaces;

namespace NexusCore
{
    /// <summary>
    /// Main controller that handles packet reception.
    /// </summary>
    public class MainController : IController, IPacketReceiver
    {
        /// <summary>
        /// Handles the specified packet.
        /// </summary>
        /// <param name="packet">The packet to handle.</param>
        public void handle(Packet packet)
        {
            throw new NotImplementedException();
        }
    }
}
