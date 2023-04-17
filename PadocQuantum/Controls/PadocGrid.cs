using Microsoft.EntityFrameworkCore;
using PatdocQuantum;

namespace PadocQuantum.Controls {
    public partial class PadocGrid : UserControl {
        public PadocGrid() {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            PoliciesForm? parentForm = (PoliciesForm?)ParentForm;

            parentForm!.source.Cancel();
            lblStatus.Text = "Canceling";
            btnCancel.Enabled = false;
        }
    }
}
