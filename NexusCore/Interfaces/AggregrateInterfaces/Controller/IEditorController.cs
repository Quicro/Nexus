using NexusCore.Interfaces.AggregrateInterfaces.Forms;

namespace NexusCore.Interfaces.AggregrateInterfaces.Controller {
    internal interface IEditorController : IForegroundController {
        IEditorForm editorForm { get; set; }
    }
}
