using NexusCore.Interfaces.Widgets;

namespace NexusCore.Interfaces.AggregrateInterfaces.Forms
{
    internal interface IForegroundController : IController
    {
        public List<IElementWidget> widgets { get; set; }
    }
}
