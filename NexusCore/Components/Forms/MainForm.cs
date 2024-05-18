using NexusCore.Interfaces;

namespace NexusCore.AggregrateInterfaces.Forms
{
    public interface MainForm : IForm
    {


        /// <summary>
        /// Form was closed
        /// </summary>
        event EventHandler<Packet> OnPacket;

        public bool SetUpStartMenu(List<MenuItem> setup);
    }
}
