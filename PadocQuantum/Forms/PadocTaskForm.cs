using PadocEF;

namespace PadocQuantum {
    public partial class PadocTaskForm : Form {
        public PadocTaskForm() {
            InitializeComponent();
        }

        private void PadocTaskForm_Load(object sender, EventArgs e) {
            UpdateGridView();

            timer1.Start();

        }

        private void UpdateGridView() {
            var tasks = DatabaseManager.tasks;

            gridView.Columns.Clear();
            gridView.Rows.Clear();

            gridView.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "ID" });
            gridView.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Status" });
            gridView.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "SourceToken" });
            gridView.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Source" });
            gridView.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Task" });
            gridView.Columns.Add(new DataGridViewButtonColumn() { HeaderText = "Cancel" });

            foreach (var task in tasks) {
                DataGridViewRow row = new DataGridViewRow() { };

                row.Cells.Add(new DataGridViewTextBoxCell() { Value = task.task.Id });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = task.task.Status });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = task.source.Token.IsCancellationRequested });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = task.source.IsCancellationRequested });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = task.isCanceled });
                row.Cells.Add(new DataGridViewButtonCell() { Value = "Cancel", Tag = task });

                gridView.Rows.Add(row);

            }
        }


        private void dataGridView1_Click(object sender, EventArgs e) {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            DataGridView gridView = (DataGridView)sender;
            DataGridViewButtonCell cell = gridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;

            if (cell is null) {
                return;
            }

            PadocTask task = (PadocTask)cell.Tag;
            task.source.Cancel();
            task.isCanceled = true;
        }

        private void timer1_Tick(object sender, EventArgs e) {
            UpdateGridView();
        }
    }
}
