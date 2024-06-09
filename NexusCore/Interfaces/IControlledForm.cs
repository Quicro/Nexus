namespace NexusCore.Interfaces {
    public interface IControlledForm : IForm {

        public IController controller { get; set; }
    }
}
