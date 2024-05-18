using NexusEF.Models;
using NexusCore.Interfaces;
using NexusCore.Logging;
using System.Reflection;

namespace NexusCore {

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
    /// Represents a base class for data packets in the NexusCore system.
    /// Packets determine how data should be presented and managed.
    /// Packets are used for detemining how data should be presented.
    /// Packets say if it is a list or not; if it references 0, 1 or many objects;
    /// if it references all instances of a Entity type; if it's just a string or integer.
    /// All these different cases must be handled differently by the UI.
    /// </summary>
    public abstract class Packet {
        protected Packet() { }

        /// <summary>
        /// Gets or sets the handler enumeration flags that define the packet characteristics.
        /// </summary>
        public HandlerEnum handlerEnum = HandlerEnum.Null;

        /// <summary>
        /// Gets or sets the packet handler.
        /// </summary>
        public IPacketReceiver handler;

        /// <summary>
        /// Gets or sets the packet sender.
        /// </summary>
        public IPacketSender sender;

        /// <summary>
        /// Gets or sets the query used to retrieve entities.
        /// </summary>
        public IQueryable query;

        /// <summary>
        /// Gets or sets the type of the packet.
        /// </summary>
        public Type packetType;

        /// <summary>
        /// Gets or sets the list of entities associated with the packet.
        /// </summary>
        public List<INexusEntity> entities { get; set; }

        /// <summary> handlerEnum.HasFlag(HandlerEnum.flagEntity); </summary>
        public bool hasNexusEntities => handlerEnum.HasFlag(HandlerEnum.flagEntity);
        /// <summary> handlerEnum.HasFlag(HandlerEnum.flagList); </summary>
        public bool isList => handlerEnum.HasFlag(HandlerEnum.flagList);

        /// <summary>
        /// Creates a new packet of the specified type.
        /// </summary>
        /// <typeparam name="C">The type of the controller.</typeparam>
        /// <typeparam name="P">The type of the packet.</typeparam>
        /// <param name="type">The type of the packet.</param>
        /// <param name="query">The query to retrieve entities.</param>
        /// <param name="packetSender">The packet sender.</param>
        /// <returns>A new instance of the packet.</returns>
        public static Packet Create<C, P>(Type type, IQueryable query, IPacketSender packetSender)
            where C : IController, new()
            where P : Packet, new() {
            Logger.debug($"Made new Packet: {typeof(C).Name} {typeof(P).Name} {type.Name}");

            Packet packet = new P() {
                sender = packetSender,
                handler = new C(),
                query = query,
                packetType = type
            };

            return packet;
        }

        /// <summary>
        /// Returns a string representation of the packet.
        /// </summary>
        /// <returns>A string representing the packet.</returns>
        public override string ToString() => throw new NotImplementedException();

        /// <summary>
        /// Gets the entities associated with the packet.
        /// </summary>
        /// <returns>A list of Nexus entities.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the packet cannot handle lists.</exception>
        public List<INexusEntity> getEntities() {
            if (entities == null) {
                entities = query.Cast<INexusEntity>().ToList();
            }

            if (!handlerEnum.HasFlag(HandlerEnum.flagList) && entities.Count > 1) {
                throw new InvalidOperationException("Packet cannot handle lists");
            }

            return entities;
        }

        /// <summary>
        /// Sets the entities associated with the packet.
        /// </summary>
        /// <param name="entities">The entities to set.</param>
        /// <exception cref="InvalidDataException">Thrown when entities are already loaded.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the packet cannot handle lists.</exception>
        public void setEntities(List<INexusEntity> entities) {
            if (this.entities != null) throw new InvalidDataException("Entities are already loaded");
            if (!handlerEnum.HasFlag(HandlerEnum.flagList)) throw new InvalidOperationException("Packet cannot handle lists");

            this.entities = entities;
        }

        /// <summary>
        /// Sets a single entity associated with the packet.
        /// </summary>
        /// <param name="entity">The entity to set.</param>
        /// <exception cref="InvalidDataException">Thrown when entities are already loaded.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the packet cannot handle single entities.</exception>
        public void setEntity(INexusEntity entity) {
            if (this.entities != null) throw new InvalidDataException("Entities are already loaded");
            if (handlerEnum.HasFlag(HandlerEnum.flagList)) throw new InvalidOperationException("Packet cannot handle singles");

            this.entities.Add(entity);
        }

        /// <summary> Creates a packet (Array) for the given entities </summary>
        /// <param name="entities">The entities to associate with the packet.</param>
        /// <returns>A new <see cref="PacketArray"/> instance.</returns>
        public static PacketArray byEntity(List<INexusEntity> entities) {
            PacketArray packet = new();
            packet.setEntities(entities);
            return packet;
        }

        /// <summary> Creates a packet (Type) for the given type </summary>
        /// <param name="type">The type to associate with the packet.</param>
        /// <returns>A new <see cref="PacketType"/> instance.</returns>
        public static PacketType byType(Type type) {
            return new PacketType(type);
        }
    }

    /// <summary>
    /// Represents a packet for a single entity.
    /// </summary>
    public sealed class PacketSingle : Packet  {
        public PacketSingle() {
            handlerEnum = HandlerEnum.Single;
        }

       // public override string ToString() => $"Single<{type.Name}>({getEntity().id})";
    }

    /// <summary>
    /// Represents a packet for one or more entities.
    /// </summary>
    public sealed class PacketArray : Packet {
        public PacketArray() {
            handlerEnum = HandlerEnum.Array;
        }

        //public override string ToString() => $"Array<{type.Name}>[{entities.Count}]";
    }

    /// <summary>
    /// Represents a packet for all entities of a given type.
    /// </summary>
    public sealed class PacketType : Packet {
        /// <summary>
        /// Initializes a new instance of the <see cref="PacketType"/> class with the specified type.
        /// </summary>
        /// <param name="type">The type of the entities.</param>
        public PacketType(Type type) {
            handlerEnum = HandlerEnum.Type;
            packetType = type;
            query = Helper.getQuery(packetType);
        }

        public override string ToString() => $"Type<{packetType.Name}>";
    }

    /// <summary>
    /// Represents a packet for updating controls.
    /// </summary>
    public sealed class PacketUpdate: Packet  {
        public PacketUpdate() {
            handlerEnum = HandlerEnum.Update;
        }

        public override string ToString() => $"Update";
    }

    /// <summary>
    /// Represents a packet with entities that are going to be edited, referencing a packet for the edited value.
    /// Used when editing the value by selecting the entities from the underlying request.
    /// </summary>
    public sealed class PacketEdit : Packet {
        public Packet packet;
        public INexusEntity entity;
        public FieldInfo field;

        /// <summary>
        /// Initializes a new instance of the <see cref="PacketEdit"/> class with the specified packet, query, and field.
        /// </summary>
        /// <param name="packet">The underlying packet.</param>
        /// <param name="query">The query to retrieve entities.</param>
        /// <param name="field">The field to be edited.</param>
        public PacketEdit(Packet packet, IQueryable<INexusEntity> query, FieldInfo field) {
            this.packet = packet;
            this.field = field;
            this.entity = entity;

            handlerEnum = HandlerEnum.Edit;
        }

        //public override string ToString() => $"Edit<{packet.type?.Name}>[{packet..Count}] from {packet}";
    }

    /// <summary>
    /// Represents a packet for updating only the editor.
    /// </summary>
    public sealed class PacketSingleEditor : Packet {
        public PacketSingleEditor() {
            handlerEnum = HandlerEnum.SingleEditor;
        }

        public override string ToString() => $"SingleEditor<{packetType.Name}>({entities.First().Id})";
    }
}
