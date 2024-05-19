namespace NexusCore.Interfaces {
    public interface IForm
    {


        /// <summary>
        /// Called when the form is constructed, used after the ctor is finishes as last step of the ctor
        /// </summary>
        event EventHandler OnOpen;

        /// <summary>
        /// Form was closed
        /// </summary>
        event EventHandler OnClose;


        public void Open();
        public void Start();
        public void End();
        public void Close();
    }
}
