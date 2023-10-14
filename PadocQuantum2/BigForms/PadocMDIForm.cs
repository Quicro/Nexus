using PadocEF;
using PadocEF.Models;
using PadocQuantum2.BigControls;
using PadocQuantum2.BigForms;
using PadocQuantum2.Interfaces;
using PadocQuantum2.Logging;

namespace PadocQuantum2 {

    public partial class PadocMDIForm : Form {
        public static PadocMDIForm singleton;
        public static List<IPacketHandler> packetHandlers = new();

        public static event EventHandler<Packet> sent;

        static PadocMDIForm() {
            sent += PadocMDIForm_sent;
            if (singleton is null)
                singleton = new();

            PadocBuilder.doMyFirstPacket();
        }

        private static void PadocMDIForm_sent(object? sender, Packet packet) {
            sent?.Invoke(singleton, packet);
        }

        public void handle(object sender, Packet packet) {
            packet.sender = (IPacketSender)sender;
            packet.handler.handle(packet);
        }

        public PadocMDIForm() {
            InitializeComponent();
        }

        private void typesToolStripMenuItem_Click(object sender, EventArgs e) {
            new TypeViewer() {
                MdiParent = this
            }.Show();
        }
    }



}