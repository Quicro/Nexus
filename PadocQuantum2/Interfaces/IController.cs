using PadocEF.Models;

namespace PadocQuantum2.Interfaces {
    internal interface IController : IPacketReceiver { }
    internal interface IController<T> : IPacketReceiver where T : class, IPadocEntity { }
}
