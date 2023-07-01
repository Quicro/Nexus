using PadocEF.Models;

namespace PadocQuantum2.Interfaces {
    public interface IController : IPacketReceiver { }
    public interface IController<T> : IPacketReceiver where T : class, IPadocEntity { }
}
