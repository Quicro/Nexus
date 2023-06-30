using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PadocEF;
using PadocEF.Extentions;
using PadocEF.Models;
using PadocQuantum2.BigControls;
using PadocQuantum2.BigForms;
using PadocQuantum2.Interfaces;
using PadocQuantum2.Logging;
using System;
using System.Linq.Expressions;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
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

                Logger.debug(packet.query.ToQueryString());
                packet.entities = packet.getEntities();


            Logger.debug(string.Join("  ", packet.entities.Select(e => e.Id)));

            listView.BeginUpdate();

            Type type = packet.entities.First().GetType();
            updateColumns(type);
            updateItems(type, packet.entities.ToList());

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

        internal void updateItems(Type type, List<IPadocEntity> entities)  {
            listView.Items.Clear();

            foreach (var entity in entities) {
                var listViewItem = new ListViewItem() { UseItemStyleForSubItems = false };

                foreach (PropertyInfo column in columns) {
                    object value = column.GetValue(entity);
                    Type propertyType = column.PropertyType;
                    IQueryable<IPadocEntity> query = getQuery(type).Where(e => e.Id == entity.Id);
                    Packet packet = Create<PacketSingleEditor>(type, query);
                    string textItem = "error";
                    Font fontItem = fontDefault;
                    Color foreColor = Color.Black;

                    if (column.Name.EndsWith("Id") && column.Name != "Id") {
                        PropertyInfo reference = type.GetProperties().Where(c => c.Name == column.Name.Replace("Id", "")).Single();
                        Type referenceType = reference.PropertyType;
                        if (value is not null) {
                            int id = (int)value;
                            MethodInfo getQueryableMethod = GetType()
                                .GetMethod("getQueryable", BindingFlags.NonPublic | BindingFlags.Static) // Specify the appropriate binding flags
                                .MakeGenericMethod(referenceType, type);
                            IQueryable<IPadocEntity> result = ((IQueryable)getQueryableMethod.Invoke(null, new object[] { id })).Cast<IPadocEntity>();
                            //packet = Create<PacketSingle>(propertyType, result);

                            Logger.info(type.Name + " " + value);
                        } else {
                            ;
                        }
                    }

                    //Logger.debug("HandlerEnum: " + packet.handlerEnum + packet.GetType().Name);

                    if (value is null) {
                        textItem = "NULL";
                        fontItem = fontNull;
                        foreColor = Color.Gray;
                    }

                    //O- (Null)
                    else if (isList(value) == false && isSubTypeOfEntity(propertyType) == false) {
                        //packet = (Packet)Activator.CreateInstance(typeof(PacketSingleEditor<>).MakeGenericType(typeof(Policy)));
                        textItem = value.ToString();
                    }

                    //E- (Single)
                    else if (isList(value) == false && isSubTypeOfEntity(propertyType) == true) {
                        //packetItem = Packet.byEntity((Entity)value);
                        textItem = value.ToString();
                        fontItem = fontReference;
                        foreColor = Color.Blue;
                    }

                    //O+ (DummyArray)
                    else if (isList(value) == true && isListOf<IPadocEntity>(value) == false) {
                        //packetItem = (PacketDummyArray)Packet.byString(typeof(object), ((List<string>)value).ToArray());
                        textItem = getListType(value).Name + "[]";
                        fontItem = fontReference;
                        foreColor = Color.Blue;
                    }

                    //E+ (Array)
                    else if (isList(value) == true && isListOf<IPadocEntity>(value) == true) {
                        //packetItem = Packet.byEntity(((IList)value).Cast<Entity>().ToArray(), Helper.getListType(value));
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

        private static IQueryable<T> getQueryable<T, R>(int id) 
            where T : class, IPadocEntity
            where R : class, IPadocEntity {
            var set = DatabaseManager.context.Set<R>(); 
            IPadocEntity entityItem = set.Single(e => e.Id == id);
            IQueryable<T> a = Extentions.getQueryable2(entityItem, typeof(T)).Cast<T>();
            return a;
        }

        private Packet Create<T>(Type type, IQueryable query) where T : Packet, new() {
            return CreatePacket<ViewerController, T>(type, query, this);
        }

        private Packet CreateEditor<T>(Type type, IQueryable query) where T : Packet, new() {
            return CreatePacket<EditorController, T>(type, query, this);
        }

        private Packet CreatePacket<C, P>(Type type, IQueryable queryable, IController controller) 
            where C : IController, new()
            where P : Packet, new() {
            Packet packet = new P() {
                handler = controller ?? new C(),
                query = queryable,
                handlerEnum = HandlerEnum.Single,
                sender = viewerUserControl
                ,packetType = type
            };

            return packet;
        }
    }
}