namespace PadocQuantum.Controls {
    public partial class PadocDetail : UserControl {
        public PadocDetail() {
            InitializeComponent();
        }

        private void buttonClick(object sender, EventArgs e) {
            PadocForm.buttonClick(sender,e);
        }

        private void PadocDetail_Load(object sender, EventArgs e) {
            PadocForm.onLoad(groupBox);
        }
    }
}