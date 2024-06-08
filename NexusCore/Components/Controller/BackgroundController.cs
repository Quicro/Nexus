using NexusCore.Interfaces.AggregrateInterfaces.Controller;
using NexusCore.Interfaces.Widgets;

namespace NexusCore.Components.Controller {
    public class BackgroundController : IBackgroundController {
        public List<IElementWidget> widgets { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public NexusApp nexusApp { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void handle(Packet packet) {
            throw new NotImplementedException();
        }

        public void Start() {
            throw new NotImplementedException();
        }

        public void Stop() {
            throw new NotImplementedException();
        }
    }
}