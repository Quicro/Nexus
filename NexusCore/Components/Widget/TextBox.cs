using System.Drawing;
using System.Windows.Forms;

namespace NexusCore.Components.Widget {
    public class TextBox : IControl {
        public TextBox() {
        }

        public string Text { get;  set; }
        public Point Location { get;  set; }
        public Control control { get;  set; }
    }
}