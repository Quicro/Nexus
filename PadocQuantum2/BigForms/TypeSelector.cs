using PadocQuantum2.BigControls;
using PadocQuantum2.Interfaces;

namespace PadocQuantum2.BigForms {
    public partial class TypeViewer : Form {
        public TypeViewer() {
            InitializeComponent();

            var padocEntityTypes = typeof(PadocEF.Models.Claim).Assembly
                                    .GetTypes()
                                    .Where(x => typeof(PadocEF.Models.IPadocEntity).IsAssignableFrom(x));

            foreach (Type type in padocEntityTypes) {
                TypeSelectorItem item = new(type, type.Name + ".png") { };

                flowLayoutPanel1.Controls.Add(item);
            }
        }
    }



    public class PacketSender : IPacketSender {
        public PacketSender() {
            sent += PadocMDIForm.singleton.handle;
        }

        public event EventHandler<Packet> sent;

        public void send(IPacketReceiver receiver, Packet packet) {
            packet.handler = receiver;
            sent(this, packet);
        }
    }
}
