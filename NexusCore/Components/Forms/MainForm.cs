using NexusCore.Interfaces;

namespace NexusCore.Components.Forms {
    public interface MainForm : IForm {


        /// <summary>
        /// Form was closed
        /// </summary>
        event EventHandler<Packet> OnPacket;

        public bool SetUpStartMenu(List<MenuItem> setup);
    }
}
