using NexusCore.Interfaces.Widgets;
using NexusCore.Forms;

namespace NexusCore.Components.Widget {
    public class Widget : IWidget {
        public readonly bool isElemental;

        public Widget(bool isElemental) {
            this.isElemental = isElemental;
        }

        public event EventHandler<Packet> sent;
    }

    public class ElementWidget : IElementWidget {
        public ElementWidget(bool isElemental) {
            this.isElemental = isElemental;
        }

        public bool isElemental { get; }

        public event EventHandler<Packet> sent;
    }

    public class ElementWidget<T> : ElementWidget where T : IControl {
        public ElementWidget(T control) : base(true) {
            this.control = control;
        }

        public T control { get; set; }
    }

    public class LabelWidget : ElementWidget<Label> {
        public LabelWidget(Label label) : base(label) {
            control = label;
        }
    }
    /*
    public class DummyWidget : ElementWidget<Dummy>
    {
        public DummyWidget(Dummy Dummy) : base(Dummy)
        {
            this.control = Dummy;
        }
    }*/

    public class ButtonWidget : ElementWidget<Button> {
        public ButtonWidget(Button Button) : base(Button) {
            control = Button;
        }
    }
    public class TextBoxWidget : ElementWidget<TextBox> {
        public TextBoxWidget(TextBox TextBox) : base(TextBox) {
            control = TextBox;
        }
    }
    public class ListViewWidget : ElementWidget<ListView> {
        public ListViewWidget(ListView ListView) : base(ListView) {
            control = ListView;
        }
    }
}
