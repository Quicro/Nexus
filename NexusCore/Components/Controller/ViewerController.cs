using Microsoft.EntityFrameworkCore;
using NexusCore.Components.Forms;
using NexusCore.Components.Widget;
using NexusCore.Forms;
using NexusCore.Interfaces.AggregrateInterfaces.Controller;
using NexusCore.Interfaces.Widgets;
using NexusEF;
using NexusEF.Models;
using NexusLogging;
using System.Drawing;
using System.Reflection;
using static NexusCore.Helper;

namespace NexusCore.Components.Controller {
    /// <summary>
    /// Controller for viewing entities and handling packets in the viewer.
    /// </summary>
    public class ViewerController : IViewerController {
        /// <summary>
        /// Reference to the viewer form.
        /// </summary>
        public ViewerForm viewerForm; //ref => BigForms


        /// <summary>
        /// Reference to the list view control.
        /// </summary>
        public ListViewWidget listView;


        protected List<PropertyInfo> columns;

        /// <summary> Underlined text </summary>
        public static Font fontReference = new("Microsoft Sans Serif", 8.5f, FontStyle.Underline);
        /// <summary> Regular text </summary>
        public static Font fontDefault = new("Microsoft Sans Serif", 8.5f, FontStyle.Regular);
        /// <summary> Bold text </summary>
        public static Font fontSelected = new("Microsoft Sans Serif", 8.5f, FontStyle.Bold);
        /// <summary> Italic text </summary>
        public static Font fontNull = new("Microsoft Sans Serif", 8.5f, FontStyle.Italic);

        public List<IWidget> widgets { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        List<IElementWidget> IForegroundController.widgets { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ViewerForm ViewerForm { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewerController"/> class.
        /// </summary>
        public ViewerController() {
            viewerForm = new ViewerForm(); //ref => BigForms
            listView = new ListViewWidget(new ListView());
        }

        /// <summary>
        /// Handles the specified packet.
        /// </summary>
        /// <param name="packet">The packet to handle.</param>
        public void handle(Packet packet) {
            if (!packet.hasNexusEntities) {
                Logger.ViewerPacketHasNoEntitiesError();
                throw new Exception();
            }

            packet.entities = packet.getEntities();

            listView.control.BeginUpdate();

            Type type = packet.packetType;

            if (isCollection(packet.packetType) == true && isListOf<INexusEntity>(packet.packetType) == true) {
                type = getListType(packet.packetType);
            }

            updateColumns(type);

            if (packet.entities.Count != 0) {
                updateItems(type, packet.entities.ToList());
            }

            listView.control.EndUpdate();
            viewerForm.Open();
        }

        internal void updateColumns(Type type) {
            listView.control.Columns.Clear();

            PropertyInfo[] fields = type.GetProperties();
            columns = fields
                .Where(field => !fields.Select(f => f.Name).Contains(field.Name + "Id"))
                .ToList();

            listView.control.Columns.Add("");

            foreach (PropertyInfo column in columns) {
                listView.control.Columns.Add(column.Name);
            }
        }

        internal void updateItems(Type type, List<INexusEntity> entities) {
            listView.control.Items.Clear();

            foreach (INexusEntity entity in entities) {
                ListViewItem listViewItem = new() { UseItemStyleForSubItems = false };

                foreach (PropertyInfo column in columns) {
                    object value = column.GetValue(entity);
                    Type columnType = column.PropertyType;
                    IQueryable<INexusEntity> query = getQuery(type).Where(e => e.Id == entity.Id);
                    Packet packet = Packet.Create<EditorController, PacketSingleEditor>(type, query, listView);
                    string textItem = "error";
                    Font fontItem = fontDefault;
                    Color foreColor = Color.Black;

                    if (value is null) {
                        textItem = "NULL";
                        fontItem = fontNull;
                        foreColor = Color.Gray;
                    } else {
                        bool list = isList(value);
                        bool subTypeOfEntity = isSubTypeOfEntity(columnType);
                        bool listOf = isListOf<INexusEntity>(value);

                        PacketRelationshipType packetRelationshipType = getPacketRelationshipType(list, subTypeOfEntity, listOf);

                        if (packetRelationshipType == PacketRelationshipType.Dummy) {
                            textItem = value.ToString();
                        }

                        if (packetRelationshipType == PacketRelationshipType.Single) {
                            if (column.Name.EndsWith("Id") && column.Name != "Id") {
                                PropertyInfo reference = type.GetProperties().Where(c => c.Name == column.Name.Replace("Id", "")).Single();
                                Type referenceType = reference.PropertyType;
                                int refID = (int)value;

                                IQueryable<INexusEntity> queryOfRelatedEntities = (IQueryable<INexusEntity>)callStaticGenericMethod(
                                    typeof(Extentions),
                                    nameof(Extentions.getQueryableByID),
                                    new Type[] { referenceType, type },
                                    new object[] { entity, refID }
                                );
                                string __sql_queryOfRelatedEntities.ToQueryString();

                                packet = Packet.Create<ViewerController, PacketSingle>(referenceType, queryOfRelatedEntities, listView);
                                textItem = referenceType.Name;
                                fontItem = fontReference;
                                foreColor = Color.Blue;
                            }
                        }

                        if (packetRelationshipType == PacketRelationshipType.Array) {
                            Type referenceType = getListType(value);

                            referenceType = Extentions.getPossibleMoreMoreRelationType(type, referenceType) ?? referenceType;

                            IQueryable<INexusEntity> queryOfRelatedEntities = (IQueryable<INexusEntity>)callStaticGenericMethod(
                                typeof(Extentions),
                                nameof(Extentions.getRelatedQueryableByID),
                                new Type[] { type, referenceType },
                                new object[] { entity }
                            );
                            string __sql_queryOfRelatedEntities.ToQueryString();

                            packet = Packet.Create<ViewerController, PacketArray>(referenceType, queryOfRelatedEntities, listView);
                            fontItem = fontReference;
                            foreColor = Color.Blue;
                            textItem = getListType(value).Name + "[]";
                        }
                    }

                    /*listViewItem.SubItems.Add(new ListViewSubItem()
                    {
                        Tag = packet,
                        Text = textItem,
                        Font = fontItem,
                        ForeColor = foreColor
                    });*/
                }
                listView.control.Items.Add(listViewItem);
            }
        }
    }
}
