/*using NexusCore.Controllers; //UITZETTEN VAN WINDOWS FORMS REFERENCES
using NexusCore.BigForms;
using NexusCore.Interfaces;

namespace NexusCore
{

    public partial class NexusMDIForm : Form
    {
        public static NexusMDIForm singleton;
        public static List<IPacketHandler> packetHandlers = new();

        public static event EventHandler<Packet> sent;

        static NexusMDIForm()
        {
            sent += NexusMDIForm_sent;
            if (singleton is null)
                singleton = new();

            NexusBuilder.doMyFirstPacket();
        }

        private static void NexusMDIForm_sent(object? sender, Packet packet)
        {
            sent?.Invoke(singleton, packet);
        }

        public void handle(object sender, Packet packet)
        {
            packet.sender = (IPacketSender)sender;
            packet.handler.handle(packet);
        }

        public NexusMDIForm()
        {
            InitializeComponent();
        }

        private void typesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TypeViewer()
            {
                MdiParent = this
            }.Show();
        }
    }



}
*/