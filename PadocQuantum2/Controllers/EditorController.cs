using PadocEF.Models;
using PadocQuantum2.BigControls;
using PadocQuantum2.BigForms;
using PadocQuantum2.Interfaces;
using System.Reflection;

namespace PadocQuantum2.Controllers
{
    public class EditorController : IController
    {
        public Editor editor;
        public EditorUserControl editorUserControl;

        public EditorController()
        {
            editor = new Editor();
            editor.MdiParent = PadocMDIForm.singleton;

            editorUserControl = editor.editorUserControl;

            editor.controller = this;
        }

        public void handle(Packet packet) {
            throw new NotImplementedException();
        }
    }

    public class EditorController<T> : EditorController, IController<T>, IPacketReceiver where T : class, IPadocEntity
    {
        public void handle(Packet packet)
        {

            PacketSingleEditor packetSingleEditor = (PacketSingleEditor)packet;
            IPadocEntity entity = packetSingleEditor.getEntities().Single();

            editorUserControl.Controls.Add(new Label() { Text = "ID: " + entity.Id });

            PropertyInfo[] fields = typeof(T).GetProperties();
            int i = 1;
            foreach (PropertyInfo field in fields)
            {
                var fieldname = field.Name;
                var fieldvalue = field.GetValue(entity)?.ToString();



                editorUserControl.Controls.Add(new Label() { Text = fieldname + ": ", Location = new Point(10, i * 40 + 10) });
                editorUserControl.Controls.Add(new TextBox() { Text = fieldvalue, Location = new Point(150, i * 40 + 10) });


                i++;

            }

            editor.Show();
        }
    }
}
