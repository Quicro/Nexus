using NexusCore.Components.Widget;

namespace NexusCore.Interfaces.Widgets {
    public interface IElementWidget : IWidget {
    }
    public interface IElementWidget<T> : IElementWidget where T : IControl {
    }
}
