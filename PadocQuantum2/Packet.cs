using PadocEF;
using PadocEF.Models;
using PadocQuantum2.BigControls;
using PadocQuantum2.Interfaces;
using PadocQuantum2.Logging;
using System.Reflection;

namespace PadocQuantum2 {
    [Flags]
    public enum HandlerEnum {
        flagList = 1,
        flagEntity = 2,
        flagType = 4,
        flagEditor = 8,
        flagViewer = 16,
        flagTree = 32,
        flagRef = 64,

        Null = 0
        , Single = flagViewer | flagEntity
        , DummyArray = flagViewer | flagList
        , Array = flagViewer | flagList | flagEntity
        , Type = flagViewer | flagEditor | flagList | flagEntity | flagType
        , Update = flagViewer | flagEditor | flagTree | flagEntity
        , Edit = flagRef | flagEntity
        , SingleEditor = flagEditor | flagEntity

        /*FOR SUDDEN UNINTENDED REFORMATS.
		THESE TABS WILL NOT BE REMOVED WHEN AUTO REFORMATTING

		Null = 0
		, Single =			flagViewer									|	flagEntity
		, DummyArray =		flagViewer	|					flagList
		, Array =			flagViewer	|					flagList	|	flagEntity
		, Type =			flagViewer	|	flagEditor	|	flagList	|	flagEntity | flagType
		, Update =			flagViewer	|	flagEditor	|	flagTree	|	flagEntity
		, Edit =			flagRef										|   flagEntity
		, SingleEditor =					flagEditor	|					flagEntity

		*/
    }

    /// <summary>
    /// Packets are used for detemining how data should be presented.
    /// Packets say if it is a list or not; if it references 0, 1 or many objects;
    /// if it references all instances of a Entity type; if it's just a string or integer.
    /// All these different cases must be handled differently by the UI.
    /// </summary>
    public abstract class Packet {
        protected Packet() { }

        public HandlerEnum handlerEnum = HandlerEnum.Null;
        public IPacketReceiver handler;
        public IPacketSender sender;
        public IQueryable query;

        public Type packetType;
        public List<IPadocEntity> entities { get; set; }

        /// <summary> handlerEnum.HasFlag(HandlerEnum.flagEntity); </summary>
        public bool hasPadocEntities => handlerEnum.HasFlag(HandlerEnum.flagEntity);
        /// <summary> handlerEnum.HasFlag(HandlerEnum.flagList); </summary>
        public bool isList => handlerEnum.HasFlag(HandlerEnum.flagList);



        public static Packet Create<C, P>(Type type, IQueryable query, ViewerUserControl viewerUserControl)
        where C : IController, new()
        where P : Packet, new() {
            Logger.debug($"Made new Packet: {typeof(C).Name} {typeof(P).Name} {type.Name}");

            Packet packet = new P() {
                sender = viewerUserControl,
                handler = new C(),
                query = query,
                packetType = type
            };

            return packet;
        }

        public override string ToString() => throw new NotImplementedException();

        public List<IPadocEntity> getEntities() {
            if (entities == null) {
                entities = query.Cast<IPadocEntity>().ToList();
            }

            if (!handlerEnum.HasFlag(HandlerEnum.flagList))
                if (entities.Count > 1) {
                    throw new InvalidOperationException("Packet cannot handle lists");
                }

            return entities;
        }
        public void setEntities(List<IPadocEntity> entities) {
            if (entities == null) throw new InvalidDataException("Entities are already loaded");
            if (!handlerEnum.HasFlag(HandlerEnum.flagList)) throw new InvalidOperationException("Packet cannot handle lists");

            this.entities = entities;
        }
        public void setEntity(IPadocEntity entity) {
            if (entities == null) throw new InvalidDataException("Entities are already loaded");
            if (handlerEnum.HasFlag(HandlerEnum.flagList)) throw new InvalidOperationException("Packet cannot handle singles");

            this.entities.Add(entity);
        }

        /// <summary> Creates a packet (Array) for the given entities </summary>
        public static PacketArray byEntity(List<IPadocEntity> entities) {
            PacketArray packet = new();
            packet.setEntities(entities);
            return packet;
        }

        /// <summary> Creates a packet (Type) for the given type </summary>
        public static PacketType byType(Type type) {
            return new PacketType(type);
        }
    }

    /// <summary>
    /// Packet for a single entity
    /// </summary>
    public sealed class PacketSingle : Packet  {
        public PacketSingle() {
            handlerEnum = HandlerEnum.Single;
        }

        //public override string ToString() => $"Single<{type.Name}>({getEntity().id})";
    }

    /// <summary>
    /// Packet for an 0, 1 or many entities. 
    /// </summary>
    public sealed class PacketArray : Packet {
        //TODO: type kan niet worden gevonden als de lijst leeg is
        // -> Verwijder alle waypoints uit een route en probeer het project te updaten
        public PacketArray() {
            handlerEnum = HandlerEnum.Array;
        }

        //public override string ToString() => $"Array<{type.Name}>[{entities.Count}]";
    }

    /// <summary>
    /// Packet for all entities of given type.
    /// </summary>
    public sealed class PacketType : Packet {
        /// <param name="type">Packeted type of the entities</param>
        public PacketType(Type type) {
            handlerEnum = HandlerEnum.Type;
            packetType = type;
            query = Helper.getQuery(packetType);
        }

        public override string ToString() => $"Type<{packetType.Name}>";
    }

    /// <summary>
    /// Packet for updating the controls 
    /// </summary>
    public sealed class PacketUpdate: Packet  {
        public PacketUpdate() {
            handlerEnum = HandlerEnum.Update;
        }

        public override string ToString() => $"Update";
    }

    /// <summary>
    /// Packet with a value of entities that is going to be edited, referencing a packet for the edited value.
    /// Used when editing the value by selecting the entities from the underlying request.
    /// </summary>
    public sealed class PacketEdit : Packet {
        public Packet packet;

        public IPadocEntity entity;
        public FieldInfo field;

        /// <param name="packet">Underlying packet</param>
        public PacketEdit(Packet packet, IQueryable<IPadocEntity> query, FieldInfo field) {
            this.packet = packet;
            this.field = field;
            this.entity = entity;

            handlerEnum = HandlerEnum.Edit;
        }

        /*public override string ToString() =>
            $"Edit<{packet.type?.Name}>[{packet..Count}] from {packet}";*/
    }

    /// <summary>
    /// Updates only the editor
    /// </summary>
    public sealed class PacketSingleEditor : Packet {
        public PacketSingleEditor() {
            handlerEnum = HandlerEnum.SingleEditor;
        }

        public override string ToString() => $"SingleEditor<{packetType.Name}>({entities.First().Id})";
    }
}