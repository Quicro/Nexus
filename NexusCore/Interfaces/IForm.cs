using NexusCore.Interfaces.Widgets;

namespace NexusCore.Interfaces {
    public interface IForm : IPacketSender {


        public NexusApp nexusApp { get; set; }
        public List<IWidget> widgets { get; set; }
        /// <summary>
        /// Called when the form is constructed, used after the ctor is finishes as last step of the ctor
        /// </summary>
        event EventHandler OnOpen;

        /// <summary>
        /// Form was closed
        /// </summary>
        event EventHandler OnClose;


        public void Open();
        public void Close();
    }
}
