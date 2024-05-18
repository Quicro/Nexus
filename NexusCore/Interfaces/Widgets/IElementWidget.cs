using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusCore.Interfaces.Widgets
{
    public interface IElementWidget : IWidget
    {
    }
    public interface IElementWidget<T> : IElementWidget where T : Control
    {
    }
}
