using Microsoft.EntityFrameworkCore;
using NexusEF.Extentions;
using NexusEF.Models;
using NexusCore.BigControls;
using NexusCore.BigForms;
using NexusCore.Interfaces;
using NexusCore.Logging;
using System.Reflection;
using static NexusCore.Helper;
using static System.Windows.Forms.ListViewItem;

namespace NexusCore.Controllers
{
    public class ViewerController : IController {
        public IViewerForm viewerForm; //ref => BigForms
        public ViewerUserControl viewerUserControl; //ref => BigControls
        public ListView listView;
        protected List<PropertyInfo> columns;

        /// <summary> Underlined text </summary>
        public static Font fontReference = new Font("Microsoft Sans Serif", 8.5f, FontStyle.Underline);
        /// <summary> Regular text </summary>
        public static Font fontDefault = new Font("Microsoft Sans Serif", 8.5f, FontStyle.Regular);
        /// <summary> Bold text </summary>
        public static Font fontSelected = new Font("Microsoft Sans Serif", 8.5f, FontStyle.Bold);
        /// <summary> Italic text </summary>
        public static Font fontNull = new Font("Microsoft Sans Serif", 8.5f, FontStyle.Italic);


        public ViewerController() {
            viewerForm = new Viewer(); //ref => BigForms
            //viewerForm.MdiParent = NexusMDIForm.singleton;
            //viewerUserControl = viewerForm.viewerUserControl;
            listView = viewerUserControl.listView;

            listView.Parent = viewerUserControl;
            //viewerUserControl.Parent = viewerForm;

            viewerUserControl.controller = this;

            /*
            viewerForm.FormClosing += (sender, e) => {
                if (e.CloseReason == CloseReason.UserClosing) {
                    e.Cancel = true; // Cancel the close operation
                    viewerForm.Hide();     // Hide the form
                }
            };
            */
        }

        public void handle(Packet packet) {
            if (!packet.hasNexusEntities) {
                Logger.ViewerPacketHasNoEntitiesError();
                throw new Exception();
            }

            packet.entities = packet.getEntities();

            listView.BeginUpdate();

            Type type = packet.packetType;

            if (isCollection(packet.packetType) == true && isListOf<INexusEntity>(packet.packetType) == true) {
                type = getListType(packet.packetType);
            }

            updateColumns(type);

            if (packet.entities.Count != 0) {
                updateItems(type, packet.entities.ToList());
            }

            listView.EndUpdate();


            viewerForm.Open();
        }

        internal void updateColumns(Type type) {
            listView.Columns.Clear();

            var fields = type.GetProperties();
            columns = fields
                .Where(field => !fields.Select(f => f.Name).Contains(field.Name + "Id"))
                .ToList();


            listView.Columns.Add(
                  ""
            );

            foreach (var column in columns) {
                listView.Columns.Add(
                   column.Name
                );
            }
        }

        internal void updateItems(Type type, List<INexusEntity> entities) {
            listView.Items.Clear();

            foreach (var entity in entities) {
                var listViewItem = new ListViewItem() { UseItemStyleForSubItems = false };

                foreach (PropertyInfo column in columns) {
                    object value = column.GetValue(entity);
                    Type columnType = column.PropertyType;
                    IQueryable<INexusEntity> query = getQuery(type).Where(e => e.Id == entity.Id);
                    Packet packet = Packet.Create<EditorController, PacketSingleEditor>(type, query, viewerUserControl);
                    string textItem = "error";
                    Font fontItem = fontDefault;
                    Color foreColor = Color.Black;

                    if (value is null) {
                        textItem = "NULL";
                        fontItem = fontNull;
                        foreColor = Color.Gray;
                    }
                    else {
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
                                var __sql__ = queryOfRelatedEntities.ToQueryString();

                                packet = Packet.Create<ViewerController, PacketSingle>(referenceType, queryOfRelatedEntities, viewerUserControl);
                                textItem = referenceType.Name;
                                fontItem = fontReference;
                                foreColor = Color.Blue;
                            }
                            else
                                //ik wil graag weten waarom ik dit hierin had gezet, het vangt niks
                                ;
                        }

                        if (packetRelationshipType == PacketRelationshipType.Array) {
                            var referenceType = getListType(value);

                            referenceType = Extentions.getPossibleMoreMoreRelationType(type, referenceType) ?? referenceType;

                            IQueryable<INexusEntity> queryOfRelatedEntities = (IQueryable<INexusEntity>)callStaticGenericMethod(
                                typeof(Extentions),
                                nameof(Extentions.getRelatedQueryableByID),
                                new Type[] { type, referenceType },
                                new object[] { entity }
                            );
                            var __sql__ = queryOfRelatedEntities.ToQueryString();

                            packet = Packet.Create<ViewerController, PacketArray>(referenceType, queryOfRelatedEntities, viewerUserControl);
                            fontItem = fontReference;
                            foreColor = Color.Blue;
                            textItem = getListType(value).Name + "[]";
                        }
                    }



                    listViewItem.SubItems.Add(new ListViewSubItem() {
                        Tag = packet,
                        Text = textItem,
                        Font = fontItem,
                        ForeColor = foreColor
                    });
                }
                listView.Items.Add(listViewItem);
            }


        }
    }
}