using NexusEF;

namespace NexusOld.Controls {
    public partial class NexusGrid : UserControl {
        public Action<object, DataGridViewCellEventArgs> onClick;

        public NexusGrid() {
            InitializeComponent();
        }

        public void loadGrid<T>(IQueryable<T> query, Action<List<T>> onLoaded, Action<object, DataGridViewCellEventArgs> onClick) {
            lblStatus.Text = "Laden";
            this.onClick = onClick;
            DatabaseManager.Load(
                query,
                onLoaded
            );
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            //UserForm? parentForm = (UserForm?)ParentForm;

            //parentForm!.source.Cancel();
            lblStatus.Text = "Canceling";
            btnCancel.Enabled = false;
        }

        private void theGrid_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            onClick(sender, e);
        }
    }
}
