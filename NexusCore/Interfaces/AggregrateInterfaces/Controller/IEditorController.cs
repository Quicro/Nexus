using NexusCore.Components.AggregrateInterfaces.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusCore.Interfaces.AggregrateInterfaces.Forms
{
    internal interface IEditorController : IForegroundController
    {
        EditorForm EditorForm { get; set; }
    }
}
