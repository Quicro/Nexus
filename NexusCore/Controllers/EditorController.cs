using NexusCore.BigControls;
using NexusCore.Interfaces;
using NexusEF.Models;
using System.Reflection;
using static NexusCore.Helper;

namespace NexusCore.Controllers
{
    /// <summary>
    /// Controller for handling packets in the editor.
    /// </summary>
    public class EditorController : IController
    {
        /// <summary>
        /// Reference to the editor form.
        /// </summary>
        public Editor editor; //ref => BigForms

        /// <summary>
        /// Reference to the editor user control.
        /// </summary>
        public EditorUserControl editorUserControl; //ref => BigControls

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorController"/> class.
        /// </summary>
        public EditorController()
        {
            editor = new Editor();//ref => BigForms
            editor.MdiParent = NexusMDIForm.singleton;

            editorUserControl = editor.editorUserControl;

            editor.controller = this;
        }

        /// <summary>
        /// Handles the specified packet.
        /// </summary>
        /// <param name="packet">The packet to handle.</param>
        public void handle(Packet packet)
        {
            MethodInfo genericMethod = typeof(EditorController<>).GetMethod(nameof(handleGeneric))
                .MakeGenericMethod(packet.packetType);
            genericMethod.Invoke(this, new object[] { (PacketSingleEditor)packet });
        }

        /// <summary>
        /// Handles the specified packet for a specific type of entity.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="packet">The packet to handle.</param>
        public void handleGeneric<T>(PacketSingleEditor packet) where T : class, INexusEntity
        {
            new EditorController<T>().handle(packet);
        }
    }

    /// <summary>
    /// Generic controller for handling packets in the editor.
    /// </summary>
    /// <typeparam name="T">The type of the Nexus entity.</typeparam>
    public class EditorController<T> : EditorController, IController<T>, IPacketReceiver where T : class, INexusEntity
    {
        /// <summary>
        /// Handles the specified packet.
        /// </summary>
        /// <param name="packet">The packet to handle.</param>
        public void handle(Packet packet)
        {
            PacketSingleEditor packetSingleEditor = (PacketSingleEditor)packet;
            INexusEntity entity = packetSingleEditor.getEntities().Single();

            editorUserControl.Controls.Add(new Label() { Text = "ID: " + entity.Id });

            PropertyInfo[] fields = typeof(T).GetProperties();
            int i = 1;
            foreach (PropertyInfo field in fields)
            {

            }

            foreach (PropertyInfo column in typeof(T).GetProperties())
            {
                string value = column.GetValue(entity)?.ToString();
                Type columnType = column.PropertyType;
                var fieldname = column.Name;

                var list = isList(value);
                var subTypeOfEntity = isSubTypeOfEntity(columnType);
                var listOf = isListOf<INexusEntity>(value);

                Logger.debug($"Editor Field Name: {typeof(T).Name}.{fieldname} = {value} islist={isList(value)} isSub={isSubTypeOfEntity(columnType)} isListOf={isListOf<INexusEntity>(value)}");

                if (value is null)
                {
                    editorUserControl.Controls.Add(new Label() { Text = "NULL", Location = new Point(200, i * 40 + 10) });
                    editorUserControl.Controls.Add(new Label() { Text = fieldname + ": ", Location = new Point(10, i * 40 + 10) });
                    editorUserControl.Controls.Add(new Label() { Text = "NULL", Location = new Point(150, i * 40 + 10) });
                }

                // O- (Dummy)
                else if (isList(value) == false && isSubTypeOfEntity(columnType) == false)
                {
                    editorUserControl.Controls.Add(new Label() { Text = "Dummy", Location = new Point(200, i * 40 + 10) });
                    editorUserControl.Controls.Add(new Label() { Text = fieldname + ": ", Location = new Point(10, i * 40 + 10) });
                    editorUserControl.Controls.Add(new TextBox() { Text = value, Location = new Point(150, i * 40 + 10) });
                }

                // E- (Single)
                else if (isList(value) == false && isSubTypeOfEntity(columnType) == true)
                {
                    editorUserControl.Controls.Add(new Label() { Text = "Single", Location = new Point(200, i * 40 + 10) });
                    editorUserControl.Controls.Add(new Label() { Text = fieldname + ": ", Location = new Point(10, i * 40 + 10) });
                    editorUserControl.Controls.Add(new Button() { Text = value, Location = new Point(150, i * 40 + 10) });
                }

                // E+ (Array)
                else if (isList(value) == true && isListOf<INexusEntity>(value) == true)
                {
                    editorUserControl.Controls.Add(new Label() { Text = "Array", Location = new Point(200, i * 40 + 10) });
                    editorUserControl.Controls.Add(new Label() { Text = fieldname + ": ", Location = new Point(10, i * 40 + 10) });
                    editorUserControl.Controls.Add(new Button() { Text = columnType.Name + "[]", Location = new Point(150, i * 40 + 10) });
                }
                i++;
            }

            editor.Show();
        }

        /// <summary>
        /// Handles dummy packet operations.
        /// </summary>
        /// <typeparam name="C">The type of the item.</typeparam>
        /// <param name="editor">The editor form.</param>
        /// <param name="item">The item to handle.</param>
        public static void doDummy<C>(Editor editor, C item)
        {//ref => BigForms

        }

        /// <summary>
        /// Handles single packet operations.
        /// </summary>
        /// <typeparam name="C">The type of the item.</typeparam>
        /// <param name="editor">The editor form.</param>
        /// <param name="item">The item to handle.</param>
        public static void doSingle<C>(Editor editor, C item)//ref => BigForms
            where C : INexusEntity, new()
        {

        }

        /// <summary>
        /// Handles array packet operations.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <typeparam name="C">The type of the item in the array.</typeparam>
        /// <param name="editor">The editor form.</param>
        /// <param name="array">The array to handle.</param>
        public static void doArray<T, C>(Editor editor, T array) //ref => BigForms
            where T : List<C>
            where C : INexusEntity, new()
        {
            // Implement array handling logic here
        }
    }
}
