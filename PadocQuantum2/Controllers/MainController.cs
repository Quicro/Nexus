using PadocQuantum2.Interfaces;

namespace PadocQuantum2.Controllers {
    public class MainController : IController, IPacketReceiver {
        public event EventHandler<Packet> sent;

        public void handle(Packet packet) {
            throw new NotImplementedException();
        }
    }

}
