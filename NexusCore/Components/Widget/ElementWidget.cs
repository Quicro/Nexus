using NexusCore.Interfaces.Widgets;

namespace NexusCore.Components.Widget {
    public class ElementWidget : IElementWidget {
        public ElementWidget(bool isElemental) {
            this.isElemental = isElemental;
        }

        public bool isElemental { get; }

        public event EventHandler<Packet> sent;
    }
}
