using NexusEF.Models;

namespace NexusCore.Interfaces {
    public interface IController : IPacketReceiver { }
    public interface IController<T> : IPacketReceiver where T : class, INexusEntity { }
}
