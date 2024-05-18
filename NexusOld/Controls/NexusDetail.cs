namespace NexusOld.Controls {
    public partial class NexusDetail : UserControl {
        public NexusDetail() {
            InitializeComponent();
        }

        private void buttonClick(object sender, EventArgs e) {
            NexusForm.buttonClick(sender,e);
        }

        private void NexusDetail_Load(object sender, EventArgs e) {
            NexusForm.onLoad(groupBox);
        }
    }
}
