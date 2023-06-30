
using PadocQuantum2.Logging;
using PadocQuantum2.BigForms;
using PadocQuantum2.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using PadocEF;
using PadocQuantum2.Controllers;

namespace PadocQuantum2.BigControls
{
    public partial class TypeSelectorItem : UserControl {
        private Type type;

        public bool isClickable() => btn.Enabled && this.Visible;

        public TypeSelectorItem() {
            InitializeComponent();
        }

        public void setEnabled(bool enabled) {
            btn.Enabled = enabled;
        }

        public void setVisible(bool visible) {
            this.Visible = visible;
        }

        public void click() {
            var sender = new PacketSender();
            IPacketReceiver viewerController = (IPacketReceiver)Activator.CreateInstance(typeof(ViewerController));
            Packet packet = new PacketType(type);

            Logger.debug(packet.query.GetType().Name);

            sender.send(viewerController, packet);
        }

        public TypeSelectorItem(Type type, string icon) : this() {
            this.type = type;
            btn.Text = type.Name;
            if (File.Exists(@"C:\Users\q.croes\source\repos\PadocQuantum\PadocQuantum2\Icons\" + icon)) {
                btn.Image = ResizeImage(Image.FromFile(@"C:\Users\q.croes\source\repos\PadocQuantum\PadocQuantum2\Icons\" + icon), 50, 50);
                btn.Cursor = Cursors.Hand;
            } else {
                btn.ForeColor = Color.DarkGray;
                btn.Cursor = Cursors.No;
            }
        }

        public Image ResizeImage(Image inputImage, int width, int height) {
            // Create a new bitmap with the desired dimensions
            Bitmap resizedImage = new Bitmap(width, height);

            // Create a Graphics object from the resized bitmap
            using (Graphics graphics = Graphics.FromImage(resizedImage)) {
                // Configure the graphics settings for high quality
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                graphics.DrawImage(inputImage, 0, 0, width, height);
            }

            // Return the resized image
            return resizedImage;
        }

        private void button1_Click(object sender, EventArgs e) {
            Button button = sender as Button;

            if (button.Cursor == Cursors.Hand)
                click();
            else {
                System.Media.SystemSounds.Hand.Play();
                Logger.OpenForbiddenTypeError(button.Text);
            }
        }
    }
}
