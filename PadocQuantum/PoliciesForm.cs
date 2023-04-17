using Microsoft.EntityFrameworkCore;
using PadocEF;
using PadocEF.Models;


namespace PatdocQuantum {
    public partial class PoliciesForm : Form {

        List<Policy>? policies;
        public CancellationTokenSource source;
        CancellationToken token;

        public PoliciesForm() {
            InitializeComponent();

            source = new();
            token = source.Token;
        }

        private void DataLoad(object sender, EventArgs e) {
            padocGrid1.lblStatus.Text = "Laden";

            DatabaseManager.Load(
                Program.Context.Policy.Include(p => p.Claim).Include(p => p.Client),
                DataLoading,
                token
            );
        }

        private void DataLoading(Task task, object? data) {
            if (data == null) {
                padocGrid1.lblStatus.Invoke((MethodInvoker)delegate {
                    padocGrid1.lblStatus.Text = "Fout!";

                    padocGrid1.btnCancel.Visible = false;
                });
                return;
            }

            padocGrid1.lblStatus.Invoke((MethodInvoker)delegate {
                padocGrid1.lblStatus.Text = "Geladen!";
                padocGrid1.lblStatus.Visible = false;

                padocGrid1.btnCancel.Visible = false;
            });

            policies = data as List<Policy>;

            padocGrid1.theGrid.Invoke((MethodInvoker)delegate {
                padocGrid1.theGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Id" });
                padocGrid1.theGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Name" });
                padocGrid1.theGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Number" });
                padocGrid1.theGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Claim.Count" });
                padocGrid1.theGrid.Columns.Add(new DataGridViewButtonColumn()  { HeaderText = "ClientId" });
            });

            foreach (Policy policy in policies!) {
                DataGridViewRow row = new DataGridViewRow();
                row.Cells.AddRange(
                    new DataGridViewTextBoxCell() { Value = policy.Id },
                    new DataGridViewTextBoxCell() { Value = policy.Name },
                    new DataGridViewTextBoxCell() { Value = policy.Number },
                    new DataGridViewTextBoxCell() { Value = policy.Claim.Count },
                    new DataGridViewButtonCell() { Value = policy.Client?.Name }
                );
                padocGrid1.theGrid.Invoke((MethodInvoker)delegate {
                    padocGrid1.theGrid.Rows.Add(row);
                });
            }

        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e) {
            try {

                DataGridView dgv = (DataGridView)sender;
                object? tag = dgv.CurrentRow.Tag;

                if (tag == null) {
                    //textBox1.Text = "NVT";
                    //textBox2.Text = "NVT";
                } else {
                    var client = Program.Context.Client.Single(c => c.Id == (int?)tag);

                    //textBox1.Text = client.Number;
                    //textBox2.Text = client.Name;
                }
            } catch (Exception) {
                //textBox1.Text = "ERROR";
                //textBox2.Text = "ERROR";
            }
        }
    }
}
