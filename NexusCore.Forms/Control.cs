using System.Windows.Forms;

namespace NexusCore.Forms
{
    public interface IControl
    {
    }
    public class Label : System.Windows.Forms.Label, IControl
    {
    }
    public class Button : System.Windows.Forms.Button, IControl
    {
    }
    public class TextBox : System.Windows.Forms.TextBox, IControl
    {
    }
    public class ListView : System.Windows.Forms.ListView, IControl
    {
    }
    public class ListViewItem : System.Windows.Forms.ListViewItem, IControl
    {
    }
}