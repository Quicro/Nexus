using Microsoft.EntityFrameworkCore;
using PadocEF;
using PadocEF.Models;
using PadocQuantum2.Controllers;
using PadocQuantum2.Interfaces;
using System.Reflection;

namespace PadocQuantum2.BigControls {
    public partial class ViewerUserControl : UserControl, IPacketSender {
        public ViewerController controller;
        public event EventHandler<Packet> sent;

        public ViewerUserControl() {
            InitializeComponent();

            sent += PadocMDIForm.singleton.handle;
        }

        


        public void view_Click(object sender, MouseEventArgs e) {
            var mousePositionInListView = listView.PointToClient(MousePosition);
            var hitTest = listView.HitTest(mousePositionInListView);

            if (hitTest.Item != null) {
                var listItem = hitTest.Item;
                var rowIndex = listItem.Index;
                var subItem = hitTest.SubItem;
                var columnIndex = listItem.SubItems.IndexOf(subItem);

                var associatedPacket = (Packet)listItem.Tag;
                var subItemPacket = (Packet)subItem.Tag;
                var subItemText = listItem.SubItems[columnIndex].Text;

                if (subItemPacket != null)
                    sent(this, subItemPacket);
            }
        }

    }
}
