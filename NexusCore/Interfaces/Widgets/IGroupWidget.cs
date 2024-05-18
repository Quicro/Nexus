namespace NexusCore.Interfaces.Widgets
{
    public interface IGroupWidget : IWidget
    {
        public List<IWidget> widgets { get; set; }
    }
}
