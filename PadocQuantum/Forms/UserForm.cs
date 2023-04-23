/*using PadocEF;
using PadocEF.Extentions;
using PadocEF.Models;
using PadocQuantum.FormControllers;
using PadocQuantum.Forms;
using System.Windows.Forms;

namespace PadocQuantum {

    public partial class UserForm : Form {
        List<User>? users;
        List<PadocColumn<User, UserForm>> columnList;
        public CancellationTokenSource source = new();

        public UserForm(IQueryable<User> query) {
            InitializeComponent();

            columnList = new List<PadocColumn<User>> {
                new PadocColumn<User, UserForm>("ID", u => u.Id, txtID ),
                new PadocColumn<User, UserForm>("Name", u => u.Name ?? "", txtName),
                new PadocColumn<User, UserForm>("AD Name", u => u.Adname ?? "", txtAdName),
                new PadocColumn<User, UserForm>("Email", u => u.Email ?? "", txtEmail),
                new PadocColumn<User, UserForm>("Phone", u => u.Phone ?? "", txtPhone),
                new PadocColumn<User, UserForm>("Password", u => u.Password ?? "", txtPassword),
                new PadocColumn<User, UserForm>("Role", u => "Roles", btnRoles, dontShowInTheGrid: true, UserExtentions.getRoles),
                new PadocColumn<User, UserForm>("Permission", u => "Permissions", btnPermissions, dontShowInTheGrid: true, UserExtentions.getPermissions),
            };

            padocGrid.loadGrid(
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
                    foreach (PadocColumn<User, UserForm> col in controlledColumns) {
                        col.control.Text = "???";
                    }
                } else {
                    foreach (PadocColumn<User, UserForm> col in controlledColumns) {
                        string textControl = col.getText.Invoke((User)tag).ToString();
                        col.control.Text = textControl;

                        if (col.getTag is not null) {
                            IQueryable tagControl = col.getTag.Invoke((User)tag);
                            col.control.Tag = tagControl;
                        }
                    }
                }
            } catch (Exception) {
                foreach (PadocColumn<User> col in controlledColumns) {
                    col.control.Text = "ERROR";
                }
            }
        }

        private void onDataLoaded(List<User>? users) {
            var griddableColums = columnList.Where(col => col.dontShowInTheGrid == false);

            this.users = users;
            if (users == null) {
                padocGrid.lblStatus.Invoke(() => {
                    padocGrid.lblStatus.Text = "Fout!";
                    padocGrid.btnCancel.Visible = false;
                });
                return;
            }

            padocGrid.lblStatus.Invoke(() => {
                padocGrid.lblStatus.Text = "Geladen!";
                padocGrid.lblStatus.Visible = false;

                padocGrid.btnCancel.Visible = false;
            });

            padocGrid.Invoke(() => {
                foreach (PadocColumn<User> col in griddableColums) {
                    padocGrid.theGrid.Columns.Add(new DataGridViewTextBoxColumn() {
                        HeaderText = col.headerText
                    });
                }

                foreach (User user in this.users!) {
                    DataGridViewRow row = new DataGridViewRow();

                    foreach (PadocColumn<User> col in griddableColums) {
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

                    padocGrid.theGrid.Rows.Add(row);
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