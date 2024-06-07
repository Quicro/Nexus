using NexusEF.Models;

namespace NexusCore.Interfaces {
    public interface IController : IPacketReceiver {
        public void Start();
        public void Stop();
    }
    public interface IController<T> : IPacketReceiver where T : class, INexusEntity { }
}
