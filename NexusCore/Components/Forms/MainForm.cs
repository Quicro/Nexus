using NexusCore.Interfaces;
using NexusCore.Interfaces.AggregrateInterfaces.Forms;
using NexusCore.Interfaces.Widgets;

namespace NexusCore.Components.Forms {
    public class MainForm : IMainForm {
        public List<IElementWidget> widgets { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public NexusApp nexusApp { get; set; }

        public event EventHandler<Packet> OnPacket;
        public event EventHandler OnOpen;
        public event EventHandler OnClose;

        public void Close() {
            throw new NotImplementedException();
        }

        public void Open() {
            throw new NotImplementedException();
        }

        public bool SetUpStartMenu(List<MenuItem> setup) {
            throw new NotImplementedException();
        }
    }
}
