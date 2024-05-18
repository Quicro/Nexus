using NexusCore.Interfaces.Widgets;

namespace NexusCore.Components.Widgets
{
    public class Widget : IWidget
    {
        public readonly bool isElemental;

        public Widget(bool isElemental)
        {
            this.isElemental = isElemental;
        }

        public event EventHandler<Packet> sent;
    }
    public class ElementWidget<T> : ElementWidget where T : Control
    {
        public ElementWidget(T control) : base(true)
        {
            this.control = control;
        }

        public T control { get; set; }
    }

    public class LabelWidget : ElementWidget<Label>
    {
        public LabelWidget(Label label) : base(label)
        {
            this.control = label;
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

    public class ButtonWidget : ElementWidget<Button>
    {
        public ButtonWidget(Button Button) : base(Button)
        {
            this.control = Button;
        }
    }
    public class TextBoxWidget : ElementWidget<TextBox>
    {
        public TextBoxWidget(TextBox TextBox) : base(TextBox)
        {
            this.control = TextBox;
        }
    }
    public class ListViewWidget : ElementWidget<ListView>
    {
        public ListViewWidget(ListView ListView) : base(ListView)
        {
            this.control = ListView;
        }
    }
}
