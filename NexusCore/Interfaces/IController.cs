using NexusEF.Models;

namespace NexusCore.Interfaces {
    public interface IController : IPacketReceiver {
        public List<IWidget> widgets { get; set; }
    }
    public interface IController<T> : IPacketReceiver where T : class, INexusEntity { }
}
