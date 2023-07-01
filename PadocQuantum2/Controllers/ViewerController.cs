using Microsoft.EntityFrameworkCore;
using PadocEF.Extentions;
using PadocEF.Models;
using PadocQuantum2.BigControls;
using PadocQuantum2.BigForms;
using PadocQuantum2.Interfaces;
using PadocQuantum2.Logging;
using System.Reflection;
using static PadocQuantum2.Helper;
using static System.Windows.Forms.ListViewItem;

namespace PadocQuantum2.Controllers {
    public class ViewerController : IController {
        public Viewer viewerForm;
        public ViewerUserControl viewerUserControl;
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
            viewerForm = new Viewer();
            viewerForm.MdiParent = PadocMDIForm.singleton;
            viewerUserControl = viewerForm.viewerUserControl;
            listView = viewerUserControl.listView;

            listView.Parent = viewerUserControl;
            viewerUserControl.Parent = viewerForm;

            viewerUserControl.controller = this;
        }

        public void handle(Packet packet) {
            if (!packet.hasPadocEntities) {
                Logger.ViewerPacketHasNoEntitiesError();
                throw new Exception();
            }
            List<IPadocEntity> entities = null;

            packet.entities = packet.getEntities();



            listView.BeginUpdate();

            Type type = packet.packetType;

            if (isCollection(packet.packetType) == true && isListOf<IPadocEntity>(packet.packetType) == true) {
                type = getListType(packet.packetType);
            }

            updateColumns(type);

            if (packet.entities.Count != 0) {
                updateItems(type, packet.entities.ToList());
            }

            listView.EndUpdate();

            viewerForm.Show();
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

        internal void updateItems(Type type, List<IPadocEntity> entities) {
            listView.Items.Clear();

            foreach (var entity in entities) {
                var listViewItem = new ListViewItem() { UseItemStyleForSubItems = false };

                foreach (PropertyInfo column in columns) {
                    object value = column.GetValue(entity);
                    Type columnType = column.PropertyType;
                    IQueryable<IPadocEntity> query = getQuery(type).Where(e => e.Id == entity.Id);
                    Packet packet = Create<PacketSingleEditor, EditorController>(type, query);
                    string textItem = "error";
                    Font fontItem = fontDefault;
                    Color foreColor = Color.Black;

                    if (value is null) {
                        textItem = "NULL";
                        fontItem = fontNull;
                        foreColor = Color.Gray;
                    }

                    //O- (Null)
                    else if (isList(value) == false && isSubTypeOfEntity(columnType) == false) {
                        textItem = value.ToString();
                    }

                    //E- (Single)
                    else if (isList(value) == false && isSubTypeOfEntity(columnType) == true) {
                        if (column.Name.EndsWith("Id") && column.Name != "Id") {
                            PropertyInfo reference = type.GetProperties().Where(c => c.Name == column.Name.Replace("Id", "")).Single();
                            Type referenceType = reference.PropertyType;
                            if (value is not null) {
                                int refID = (int)value;

                                MethodInfo getQueryableMethod = typeof(Extentions)
                                    .GetMethod(nameof(Extentions.getQueryableByID))
                                    .MakeGenericMethod(referenceType, type);
                                IQueryable<IPadocEntity> queryOfRelatedEntities = ((IQueryable)getQueryableMethod.Invoke(null, new object[] { entity, refID })).Cast<IPadocEntity>();

                                if (columnType.Name.StartsWith("Nullable")) {
                                    packet = Create<PacketSingle, ViewerController>(referenceType, queryOfRelatedEntities);
                                } else {
                                    packet = Create<PacketSingle, ViewerController>(columnType, queryOfRelatedEntities);
                                }

                            } else {
                                ;
                            }
                        }
                        //packetItem = Packet.byEntity((Entity)value);
                        textItem = value.ToString();
                        fontItem = fontReference;
                        foreColor = Color.Blue;
                    }

                    //O+ (DummyArray)
                    else if (isList(value) == true && isListOf<IPadocEntity>(value) == false) {
                        textItem = getListType(value).Name + "[]";
                        fontItem = fontReference;
                        foreColor = Color.Blue;
                    }

                    //E+ (Array)
                    else if (isList(value) == true && isListOf<IPadocEntity>(value) == true) {
                        var referenceType = getListType(value);


                        MethodInfo getQueryableMethod = typeof(Extentions)
                            .GetMethod(nameof(Extentions.getRelatedQueryableByID))
                            .MakeGenericMethod(type, referenceType);

                        IQueryable<IPadocEntity> queryOfRelatedEntities = ((IQueryable)getQueryableMethod.Invoke(null, new object[] { entity })).Cast<IPadocEntity>();

                        if (columnType.Name.StartsWith("ICollection") ) {
                            Type genericType = columnType.GetGenericArguments()[0];
                            Logger.debug(genericType.Name);

                            packet = Create<PacketArray, ViewerController>(genericType, queryOfRelatedEntities);
                        } else {
                            packet = Create<PacketArray, ViewerController>(columnType, queryOfRelatedEntities);
                        }



                        textItem = getListType(value).Name + "[]";
                        fontItem = fontReference;
                        foreColor = Color.Blue;
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

        private Packet Create<T, C>(Type type, IQueryable query) where T : Packet, new() where C : IController, new() {
            return CreatePacket<C, T>(type, query, new C());
        }

        private Packet CreatePacket<C, P>(Type type, IQueryable queryable, IController controller)
            where C : IController, new()
            where P : Packet, new() {

            Packet packet = new P() {
                handler = controller ?? new C(),
                query = queryable,
                handlerEnum = HandlerEnum.Single,
                sender = viewerUserControl
                ,
                packetType = type
            };

            return packet;
        }
    }
}