/*using NexusEF;
using NexusEF.Extentions;
using NexusEF.Models;
using NexusOld.FormControllers;
using NexusOld.Forms;
using System.Windows.Forms;

namespace NexusOld {

    public partial class UserForm : Form {
        List<User>? users;
        List<NexusColumn<User, UserForm>> columnList;
        public CancellationTokenSource source = new();

        public UserForm(IQueryable<User> query) {
            InitializeComponent();

            columnList = new List<NexusColumn<User>> {
                new NexusColumn<User, UserForm>("ID", u => u.Id, txtID ),
                new NexusColumn<User, UserForm>("Name", u => u.Name ?? "", txtName),
                new NexusColumn<User, UserForm>("AD Name", u => u.Adname ?? "", txtAdName),
                new NexusColumn<User, UserForm>("Email", u => u.Email ?? "", txtEmail),
                new NexusColumn<User, UserForm>("Phone", u => u.Phone ?? "", txtPhone),
                new NexusColumn<User, UserForm>("Password", u => u.Password ?? "", txtPassword),
                new NexusColumn<User, UserForm>("Role", u => "Roles", btnRoles, dontShowInTheGrid: true, UserExtentions.getRoles),
                new NexusColumn<User, UserForm>("Permission", u => "Permissions", btnPermissions, dontShowInTheGrid: true, UserExtentions.getPermissions),
            };

            NexusGrid.loadGrid(
                query,
                onDataLoaded,
                onCellClick
            );
        }

        private void onCellClick(object sender, EventArgs e) {
            var controlledColumns = columnList.Where(col => col.getControl(this) is not null);

            try {
                DataGridView dgv = (DataGridView)sender;
                object? tag = dgv.CurrentCell.Tag;

                if (tag == null) {
                    foreach (NexusColumn<User, UserForm> col in controlledColumns) {
                        col.control.Text = "???";
                    }
                } else {
                    foreach (NexusColumn<User, UserForm> col in controlledColumns) {
                        string textControl = col.getText.Invoke((User)tag).ToString();
                        col.control.Text = textControl;

                        if (col.getTag is not null) {
                            IQueryable tagControl = col.getTag.Invoke((User)tag);
                            col.control.Tag = tagControl;
                        }
                    }
                }
            } catch (Exception) {
                foreach (NexusColumn<User> col in controlledColumns) {
                    col.control.Text = "ERROR";
                }
            }
        }

        private void onDataLoaded(List<User>? users) {
            var griddableColums = columnList.Where(col => col.dontShowInTheGrid == false);

            this.users = users;
            if (users == null) {
                NexusGrid.lblStatus.Invoke(() => {
                    NexusGrid.lblStatus.Text = "Fout!";
                    NexusGrid.btnCancel.Visible = false;
                });
                return;
            }

            NexusGrid.lblStatus.Invoke(() => {
                NexusGrid.lblStatus.Text = "Geladen!";
                NexusGrid.lblStatus.Visible = false;

                NexusGrid.btnCancel.Visible = false;
            });

            NexusGrid.Invoke(() => {
                foreach (NexusColumn<User> col in griddableColums) {
                    NexusGrid.theGrid.Columns.Add(new DataGridViewTextBoxColumn() {
                        HeaderText = col.headerText
                    });
                }

                foreach (User user in this.users!) {
                    DataGridViewRow row = new DataGridViewRow();

                    foreach (NexusColumn<User> col in griddableColums) {
                        string value;
                        try {
                            var Getter = col.getText;
                            value = Getter.Invoke(user).ToString();
                        } catch (Exception) {
                            value = "ERROR";
                        }

                        //TODO: andere soorten cellen ook kunnen aanmaken zoals ButtonCell
                        var cell = new DataGridViewTextBoxCell() {
                            Value = value,
                            Tag = user
                        };
                        row.Cells.Add(cell);

                    }

                    NexusGrid.theGrid.Rows.Add(row);
                }
            });
        }

        private void btnRoles_Click(object sender, EventArgs e) {
            Button button = (Button)sender;
            IQueryable<Role> query = (IQueryable<Role>)button.Tag;

        }

        private void btnPermissions_Click(object sender, EventArgs e) {
            Button button = (Button)sender;
            IQueryable<Permission> query = (IQueryable<Permission>)button.Tag;
            List<Permission> list = query.ToList();
        }
    }
}
*/
