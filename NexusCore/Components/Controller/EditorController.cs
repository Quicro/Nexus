using NexusCore.Components.Widget;
using NexusCore.Interfaces;
using NexusCore.Interfaces.AggregrateInterfaces.Controller;
using NexusCore.Interfaces.AggregrateInterfaces.Forms;
using NexusCore.Interfaces.Widgets;
using NexusEF.Models;
using NexusLogging;
using System.Drawing;
using System.Reflection;
using static NexusCore.Helper;

namespace NexusCore.Components.Controller {
    /// <summary>
    /// Controller for handling packets in the editor.
    /// </summary>
    public class EditorController : IEditorController {

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorController"/> class.
        /// </summary>
        public EditorController() {

        }
        public EditorController(NexusApp nexusApp) {
            this.nexusApp = nexusApp;
        }
        public IEditorForm editorForm { get ; set; }
        public NexusApp nexusApp { get; set; }

        /// <summary>
        /// Handles the specified packet.
        /// </summary>
        /// <param name="packet">The packet to handle.</param>
        public void handle(Packet packet) {
            MethodInfo genericMethod = typeof(EditorController<>).GetMethod(nameof(handleGeneric))
                .MakeGenericMethod(packet.packetType);
            _ = genericMethod.Invoke(this, new object[] { (PacketSingleEditor)packet });
        }

        /// <summary>
        /// Handles the specified packet for a specific type of entity.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="packet">The packet to handle.</param>
        public void handleGeneric<T>(PacketSingleEditor packet) where T : class, INexusEntity {
            new EditorController<T>(nexusApp).handle(packet);
        }

        public void Start() {
            throw new NotImplementedException();
        }

        public void Stop() {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Generic controller for handling packets in the editor.
    /// </summary>
    /// <typeparam name="T">The type of the Nexus entity.</typeparam>
    public class EditorController<T> : EditorController, IController<T>, IPacketReceiver where T : class, INexusEntity {
        public EditorController(NexusApp nexusApp) {
            Type type = nexusApp.mainForm.nexusApp.editorFormType;
            editorForm = (IEditorForm)Activator.CreateInstance(type);
            editorForm.controller = this;
            editorForm.widgets = [];
            nexusApp = nexusApp;
            editorForm.nexusApp = nexusApp;
        }

        /// <summary>
        /// Handles the specified packet.
        /// </summary>
        /// <param name="packet">The packet to handle.</param>
        public new void handle(Packet packet) {
            PacketSingleEditor packetSingleEditor = (PacketSingleEditor)packet;
            INexusEntity entity = packetSingleEditor.getEntities().Single();

            editorForm.widgets.Add(new LabelWidget(new() { Text = $"{entity.GetType().Name} id={entity.Id}" }));

            PropertyInfo[] fields = typeof(T).GetProperties();
            int i = 1;
            

            foreach (PropertyInfo column in typeof(T).GetProperties()) {
                string? value = column.GetValue(entity)?.ToString();
                Type columnType = column.PropertyType;
                string fieldname = column.Name;

                Logger.LogDebug($"Editor Field Name: {typeof(T).Name}.{fieldname} = {value} islist={isList(value)} isSub={isSubTypeOfEntity(columnType)} isListOf={isListOf<INexusEntity>(value)}");

                ///////
                ///
                if (value is null) {
                    editorForm.widgets.Add(new LabelWidget(new() { Text = "NULL", Location = new Point(200, ( i * 40 ) + 10) }));
                    editorForm.widgets.Add(new LabelWidget(new() { Text = fieldname + ": ", Location = new Point(10, ( i * 40 ) + 10) }));
                    editorForm.widgets.Add(new LabelWidget(new() { Text = "NULL", Location = new Point(150, ( i * 40 ) + 10) }));
                } else {
                    bool list = isList(value);
                    bool subTypeOfEntity = isSubTypeOfEntity(columnType);
                    bool listOf = isListOf<INexusEntity>(value);

                    PacketRelationshipType packetRelationshipType = getPacketRelationshipType(list, subTypeOfEntity, listOf);

                    if (packetRelationshipType == PacketRelationshipType.Dummy) {
                        editorForm.widgets.Add(new LabelWidget(new() { Text = "Dummy", Location = new Point(200, ( i * 40 ) + 10) }));
                        editorForm.widgets.Add(new LabelWidget(new() { Text = fieldname + ": ", Location = new Point(10, ( i * 40 ) + 10) }));
                        editorForm.widgets.Add(new TextBoxWidget(new() { Text = value, Location = new Point(150, ( i * 40 ) + 10) }));
                    }

                    if (packetRelationshipType == PacketRelationshipType.Single) {
                        editorForm.widgets.Add(new LabelWidget(new() { Text = "Single", Location = new Point(200, ( i * 40 ) + 10) }));
                        editorForm.widgets.Add(new LabelWidget(new() { Text = fieldname + ": ", Location = new Point(10, ( i * 40 ) + 10) }));
                        editorForm.widgets.Add(new ButtonWidget(new() { Text = value, Location = new Point(150, ( i * 40 ) + 10) }));
                    }

                    if (packetRelationshipType == PacketRelationshipType.Array) {
                        editorForm.widgets.Add(new LabelWidget(new() { Text = "Array", Location = new Point(200, ( i * 40 ) + 10) }));
                        editorForm.widgets.Add(new LabelWidget(new() { Text = fieldname + ": ", Location = new Point(10, ( i * 40 ) + 10) }));
                        editorForm.widgets.Add(new ButtonWidget(new() { Text = columnType.Name + "[]", Location = new Point(150, ( i * 40 ) + 10) }));
                    }
                }
                i++;
            }

            editorForm.Open();
        }

        /// <summary>
        /// Handles dummy packet operations.
        /// </summary>
        /// <typeparam name="C">The type of the item.</typeparam>
        /// <param name="editor">The editor form.</param>
        /// <param name="item">The item to handle.</param>
        public static void doDummy<C>(IEditorForm editor, C item) {//ref => BigForms

        }

        /// <summary>
        /// Handles single packet operations.
        /// </summary>
        /// <typeparam name="C">The type of the item.</typeparam>
        /// <param name="editor">The editor form.</param>
        /// <param name="item">The item to handle.</param>
        public static void doSingle<C>(IEditorForm editor, C item)//ref => BigForms
            where C : INexusEntity, new() {

        }

        /// <summary>
        /// Handles array packet operations.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <typeparam name="C">The type of the item in the array.</typeparam>
        /// <param name="editor">The editor form.</param>
        /// <param name="array">The array to handle.</param>
        public static void doArray<T, C>(IEditorForm editor, T array) //ref => BigForms
            where T : List<C>
            where C : INexusEntity, new() {
            // Implement array handling logic here
        }
    }
}
