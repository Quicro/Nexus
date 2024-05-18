using NexusCore.Components.AggregrateInterfaces.Forms;
using NexusCore.Interfaces.AggregrateInterfaces.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusCore.Interfaces.AggregrateInterfaces.Controller
{
    internal interface IViewerController : IForegroundController
    {
        ViewerForm ViewerForm { get; set; }
    }
}
