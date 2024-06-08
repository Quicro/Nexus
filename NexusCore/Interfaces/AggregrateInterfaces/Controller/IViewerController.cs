using NexusCore.Interfaces.AggregrateInterfaces.Forms;

namespace NexusCore.Interfaces.AggregrateInterfaces.Controller {
    internal interface IViewerController : IForegroundController {
        IViewerForm viewerForm { get; set; }
    }
}
