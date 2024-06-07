namespace NexusCore.Interfaces.AggregrateInterfaces.Forms {
    public interface IMainForm : IForm {


        /// <summary>
        /// Form was closed
        /// </summary>
        event EventHandler<Packet> OnPacket;

        public NexusApp nexusApp { get; set; }
        public bool SetUpStartMenu(List<MenuItem> setup);
    }
}
