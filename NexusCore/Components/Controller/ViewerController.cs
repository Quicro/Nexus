using Microsoft.EntityFrameworkCore;
using NexusCore.Components.Forms;
using NexusCore.Interfaces.AggregrateInterfaces.Controller;
using NexusCore.Interfaces.Widgets;
using NexusCore.Widgets;
using NexusLogging;
using System.Reflection;

namespace NexusCore.Components.Controller {
    /// <summary>
    /// Controller for viewing entities and handling packets in the viewer.
    /// </summary>
    public class ViewerController : IViewerController {
        private const int PageSize = 50;

        protected List<PropertyInfo> columns;
        public ViewerForm viewerForm { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewerController"/> class.
        /// </summary>
        public ViewerController() {
            viewerForm = new ViewerForm() { viewerController = this };
            
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

            PropertyInfo[] fields = packet.packetType.GetProperties();
            columns = fields
                .Where(field => !fields.Select(f => f.Name).Contains(field.Name + "Id"))
                .ToList();

            MenuTable table = new(columns.Count, PageSize);
            table.BeginUpdate();

            table.FillIn(columns, packet.entities, packet.packetType);
            this.viewerForm.widgets.Add(table);


            table.EndUpdate();

            /*listView.control.BeginUpdate();

            Type type = packet.packetType;

            if (isCollection(packet.packetType) == true && isListOf<INexusEntity>(packet.packetType) == true) {
                type = getListType(packet.packetType);
            }



            updateColumns(type);

            if (packet.entities.Count != 0) {
                updateItems(type, packet.entities.ToList());
            }

            listView.control.EndUpdate();*/
            viewerForm.Open();
        }

        public void Start() {
            throw new NotImplementedException();
        }

        public void Stop() {
            throw new NotImplementedException();
        }

        /*internal void updateColumns(Type type) {
            listView.control.Columns.Clear();

            PropertyInfo[] fields = type.GetProperties();
            columns = fields
                .Where(field => !fields.Select(f => f.Name).Contains(field.Name + "Id"))
                .ToList();

            listView.control.Columns.Add("");

            foreach (PropertyInfo column in columns) {
                listView.control.Columns.Add(column.Name);
            }
        }*/

        /*internal void updateItems(Type type, List<INexusEntity> entities) {
            listView.control.Items.Clear();

            foreach (INexusEntity entity in entities) {
                ListViewItem listViewItem = new() { UseItemStyleForSubItems = false };

                foreach (PropertyInfo column in columns) {
                    object value = column.GetValue(entity);
                    Type columnType = column.PropertyType;
                    IQueryable<INexusEntity> query = getQuery(type).Where(e => e.Id == entity.Id);
                    Packet packet = Packet.Create<EditorController, PacketSingleEditor>(type, query, listView);
                    string textItem = "error";
                    Font fontItem = Fonts.fontDefault;
                    Color foreColor = Color.Black;

                    if (value is null) {
                        textItem = "NULL";
                        fontItem = Fonts.fontNull;
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

                                packet = Packet.Create<ViewerController, PacketSingle>(referenceType, queryOfRelatedEntities, listView);
                                textItem = referenceType.Name;
                                fontItem = Fonts.fontReference;
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

                            packet = Packet.Create<ViewerController, PacketArray>(referenceType, queryOfRelatedEntities, listView);
                            fontItem = Fonts.fontReference;
                            foreColor = Color.Blue;
                            textItem = getListType(value).Name + "[]";
                        }
                    }

                    ///*listViewItem.SubItems.Add(new ListViewSubItem()
                    //{
                    //    Tag = packet,
                    //    Text = textItem,
                    //    Font = fontItem,
                    //    ForeColor = foreColor
                    //});
                }
                listView.control.Items.Add(listViewItem);
            }
        }*/
    }
}
