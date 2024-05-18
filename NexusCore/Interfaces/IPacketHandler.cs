namespace NexusCore.Interfaces {
    /// <summary>
    /// Defines a contract for handling packets.
    /// </summary>
    public interface IPacketHandler { }

    /// <summary>
    /// Defines a contract for receiving and processing packets.
    /// </summary>
    public interface IPacketReceiver : IPacketHandler {
        /// <summary>
        /// Handles the specified packet.
        /// </summary>
        /// <param name="packet">The packet to handle.</param>
        void handle(Packet packet);
    }

    /// <summary>
    /// Defines a contract for sending packets.
    /// </summary>
    public interface IPacketSender : IPacketHandler {
        /// <summary>
        /// Occurs when a packet is sent.
        /// </summary>
        event EventHandler<Packet> sent;
    }

    /// <summary>
    /// Defines a contract for statically sending packets.
    /// Developers Note: should be used in MainForm, I think. I'm not there yet, but I think so.
    /// </summary>
    public interface IPacketStaticSender : IPacketHandler {
        /// <summary>
        /// Occurs when a packet is sent.
        /// </summary>
        public event EventHandler<Packet> sent;
    }
}
