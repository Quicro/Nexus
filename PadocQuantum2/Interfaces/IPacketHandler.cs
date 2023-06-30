using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadocQuantum2.Interfaces {



    public interface IPacketHandler  {

    }

    public interface IPacketReceiver  : IPacketHandler {
        void handle(Packet packet);
    }

    public interface IPacketSender : IPacketHandler {
        event EventHandler<Packet> sent;
    }

    public interface IPacketStaticSender : IPacketHandler {
        public event EventHandler<Packet> sent;
    }
}
