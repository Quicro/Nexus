using NexusCore.Components.Forms;

namespace NexusCore.Interfaces.AggregrateInterfaces.Controller {
    internal interface IViewerController : IForegroundController {
        ViewerForm viewerForm { get; set; }
    }
}
