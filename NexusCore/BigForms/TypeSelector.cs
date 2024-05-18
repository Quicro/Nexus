using NexusCore.BigControls;
using NexusCore.Interfaces;

namespace NexusCore.BigForms {
    public partial class TypeViewer : Form {
        public TypeViewer() {
            InitializeComponent();

            var NexusEntityTypes = typeof(NexusEF.Models.Claim).Assembly
                                    .GetTypes()
                                    .Where(x => typeof(NexusEF.Models.INexusEntity).IsAssignableFrom(x));

            foreach (Type type in NexusEntityTypes) {
                TypeSelectorItem item = new(type, type.Name + ".png") { };

                flowLayoutPanel1.Controls.Add(item);
            }
        }
    }



    public class PacketSender : IPacketSender {
        public PacketSender() {
            sent += NexusMDIForm.singleton.handle;
        }

        public event EventHandler<Packet> sent;

        public void send(IPacketReceiver receiver, Packet packet) {
            packet.handler = receiver;
            sent(this, packet);
        }
    }
}
