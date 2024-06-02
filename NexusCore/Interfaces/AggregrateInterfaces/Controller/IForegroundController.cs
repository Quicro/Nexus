using NexusCore.Interfaces.Widgets;

namespace NexusCore.Interfaces.AggregrateInterfaces.Controller {
    internal interface IForegroundController : IController {
        public List<IElementWidget> widgets { get; set; }
    }
}
