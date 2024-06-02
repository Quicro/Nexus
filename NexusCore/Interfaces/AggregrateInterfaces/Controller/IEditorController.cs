using NexusCore.Components.Forms;

namespace NexusCore.Interfaces.AggregrateInterfaces.Controller {
    internal interface IEditorController : IForegroundController {
        EditorForm EditorForm { get; set; }
    }
}
