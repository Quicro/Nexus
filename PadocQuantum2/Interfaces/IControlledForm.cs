namespace PadocQuantum2.Interfaces {
    public interface IControlledForm : IForm
    {

        /// <summary>
        /// After checking for an connection to database
        /// </summary>
        event EventHandler OnDataLoading;

        /// <summary>
        /// After data is loaded
        /// </summary>
        event EventHandler OnDataLoaded;

        /// <summary>
        /// Connection was broken while loading data
        /// </summary>
        event EventHandler OnDataLoadCancelled;


        public void Open();
        public void LoadData();
    }
}
