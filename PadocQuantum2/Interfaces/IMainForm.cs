namespace PadocQuantum2.Interfaces
{
    public interface IMainForm : IForm {


        /// <summary>
        /// Form was closed
        /// </summary>
        event EventHandler<Packet> OnPacket;

        public bool SetUpStartMenu(List<MenuItem> setup);
    }
}
