using NexusCore.Interfaces.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusCore.Components.Widgets
{
    public class ElementWidget : IElementWidget
    {
        public ElementWidget(bool isElemental)
        {
            this.isElemental = isElemental;
        }

        public bool isElemental { get; }

        public event EventHandler<Packet> sent;
    }
}
