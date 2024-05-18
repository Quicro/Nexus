namespace NexusOld {
    public partial class ClaimForm : Form {
        public ClaimForm() {
            InitializeComponent();

            Text = GetType().Name;
        }

        private void buttonClick(object sender, EventArgs e) {
            NexusForm.buttonClick(sender, e);
        }

        private void onLoad(object sender, EventArgs e) {
            NexusForm.onLoad(groupBox);
        }
    }
}
