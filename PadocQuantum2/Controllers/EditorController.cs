
using PadocEF.Models;
using PadocQuantum2.BigControls;
using PadocQuantum2.BigForms;
using PadocQuantum2.Interfaces;
using PadocQuantum2.Logging;
using System.Reflection;
using static PadocQuantum2.Helper;

namespace PadocQuantum2.Controllers {
    public class EditorController : IController {
        public Editor editor;
        public EditorUserControl editorUserControl;

        public EditorController() {
            editor = new Editor();
            editor.MdiParent = PadocMDIForm.singleton;

            editorUserControl = editor.editorUserControl;

            editor.controller = this;
        }

        public void handle(Packet packet) {
            MethodInfo genericMethod = typeof(EditorController<>).GetMethod(nameof(handleGeneric))
                .MakeGenericMethod(packet.packetType);
            genericMethod.Invoke(this, new object[] { (PacketSingleEditor)packet });
        }

        public void handleGeneric<T>(PacketSingleEditor packet) where T : class, IPadocEntity {
            new EditorController<T>().handle(packet);
        }
    }

    public class EditorController<T> : EditorController, IController<T>, IPacketReceiver where T : class, IPadocEntity {
        public void handle(Packet packet) {
            PacketSingleEditor packetSingleEditor = (PacketSingleEditor)packet;
            IPadocEntity entity = packetSingleEditor.getEntities().Single();

            editorUserControl.Controls.Add(new Label() { Text = "ID: " + entity.Id });

            PropertyInfo[] fields = typeof(T).GetProperties();
            int i = 1;
            foreach (PropertyInfo field in fields) {

            }


            foreach (PropertyInfo column in typeof(T).GetProperties()) {
                string value = column.GetValue(entity)?.ToString();
                Type columnType = column.PropertyType;
                var fieldname = column.Name;

                var list = isList(value);
                var subTypeOfEntity = isSubTypeOfEntity(columnType);
                var listOf = isListOf<IPadocEntity>(value);

                Logger.debug($"Editor Field Name: {typeof(T).Name}.{fieldname} = {value} islist={isList(value)} isSub={isSubTypeOfEntity(columnType)} isListOf={isListOf<IPadocEntity>(value)}");

                if (value is null) {
                    editorUserControl.Controls.Add(new Label() { Text = "NULL", Location = new Point(200, i * 40 + 10) });
                    editorUserControl.Controls.Add(new Label() { Text = fieldname + ": ", Location = new Point(10, i * 40 + 10) });
                    editorUserControl.Controls.Add(new Label() { Text = "NULL", Location = new Point(150, i * 40 + 10) });
                }

                //O- (Dummy)
                else if (isList(value) == false && isSubTypeOfEntity(columnType) == false) {
                    editorUserControl.Controls.Add(new Label() { Text = "Dummy", Location = new Point(200, i * 40 + 10) });
                    editorUserControl.Controls.Add(new Label() { Text = fieldname + ": ", Location = new Point(10, i * 40 + 10) });
                    editorUserControl.Controls.Add(new TextBox() { Text = value, Location = new Point(150, i * 40 + 10) });
                }

                //E- (Single)
                else if (isList(value) == false && isSubTypeOfEntity(columnType) == true) {
                    editorUserControl.Controls.Add(new Label() { Text = "Single", Location = new Point(200, i * 40 + 10) });
                    editorUserControl.Controls.Add(new Label() { Text = fieldname + ": ", Location = new Point(10, i * 40 + 10) });
                    editorUserControl.Controls.Add(new Button() { Text = value, Location = new Point(150, i * 40 + 10) });
                }

                //E+ (Array)
                else if (isList(value) == true && isListOf<IPadocEntity>(value) == true) {
                    editorUserControl.Controls.Add(new Label() { Text = "Array", Location = new Point(200, i * 40 + 10) });
                    editorUserControl.Controls.Add(new Label() { Text = fieldname + ": ", Location = new Point(10, i * 40 + 10) });
                    editorUserControl.Controls.Add(new Button() { Text = columnType.Name + "[]", Location = new Point(150, i * 40 + 10) });
                }
                i++;

            }

            editor.Show();
        }

        public static void doDummy<C>(Editor editor, C item) {


        }

        public static void doSingle<C>(Editor editor, C item)
            where C : IPadocEntity, new() {


        }

        public static void doArray<T, C>(Editor editor, T array) 
            where T: List<C>
            where C: IPadocEntity, new()
        {


            /*foreach (C item in array) {
                Logger.debug("type in generic bla  "+item.GetType().Name);

                editor.dataGridView1.DataSource = array;
                editor.dataGridView1.Text = "CHANGED QUINNEn";

                if (item is Claim claim) {
                    claim.Name = "changed bitch2";
                }
            }

            if (array is List<Claim> claims) {
                claims.Add(new Claim(
                    
                ) { 
                    Name = "newly made"
                    ,Number = "48"
                });
            }

            DatabaseManager.context.SaveChanges();*/
        }
    }
}
