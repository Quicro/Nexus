namespace NexusCore.Interfaces.AggregrateInterfaces.Forms
{
    public interface IMainForm : IForm
    {

        public NexusBuilder builder { get; set; }

        /// <summary>
        /// Form was closed
        /// </summary>
        event EventHandler<Packet> OnPacket;

        public bool SetUpStartMenu(List<MenuItem> setup);
    }
}
