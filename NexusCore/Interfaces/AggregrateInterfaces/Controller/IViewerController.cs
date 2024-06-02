using NexusCore.Components.Forms;

namespace NexusCore.Interfaces.AggregrateInterfaces.Controller {
    internal interface IViewerController : IForegroundController {
        ViewerForm ViewerForm { get; set; }
    }
}
