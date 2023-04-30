using Microsoft.EntityFrameworkCore;
using PadocEF;
using PadocEF.Extentions;
using PadocEF.Models;
using PadocQuantum;
using PadocQuantum.FormControllers;
using System.Globalization;
using System.Resources;

namespace PatdocQuantum {
    public partial class PadocMDIForm : Form {
        public static PadocMDIForm mdiForm;
        User loggedInUser;

        public PadocMDIForm() { }

        public PadocMDIForm(string arg) {
            if (mdiForm == null) {
                mdiForm = this;
            } else {
                throw new Exception("There is already an PadocMDIForm");
            }

            InitializeComponent();
            TranslateWorkItem(menuStrip.Items);
            IterateMenuItems(menuStrip.Items);

            txtURL.Text = arg;
            RequestHandler.Handle(arg);
        }

        internal void loginAsUser(User loggedInUser) {
            this.loggedInUser = loggedInUser;

            IterateMenuItems(menuStrip.Items);
        }

        private void PadocMDIForm_Load(object sender, EventArgs e) {
#if DEBUG
            loginAsUser(DatabaseManager.context.User.Find(1));
#endif
#if !DEBUG

            LoginForm loginForm = new LoginForm() {
                MdiParent = this,
                StartPosition = FormStartPosition.CenterParent
            };

            loginForm.Show();
            
#endif
        }

        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == '\r') {
                RequestHandler.Handle(txtURL.Text);
            }
        }

        private void TranslateWorkItem(ToolStripItemCollection items) {
            foreach (ToolStripItem item in items) {
                if (item.Text.Contains("$")) {
                    ResourceManager resourceManager = new ResourceManager("PadocQuantum.Translations.Strings", typeof(PadocMDIForm).Assembly);
                    string translatedText = resourceManager.GetString(item.Text.Replace("$", ""), CultureInfo.CurrentCulture) ?? "";
                    item.Text = translatedText;
                }
                if (item is ToolStripMenuItem toolStripMenuItem && toolStripMenuItem.DropDownItems.Count > 0) {
                    TranslateWorkItem(toolStripMenuItem.DropDownItems);
                }
            }
        }

        private void IterateMenuItems(ToolStripItemCollection items) {
            foreach (ToolStripItem item in items) {
                item.Visible = false;

                if (loggedInUser != null) {

                    if (UserExtention.hasPermissions(loggedInUser, "ALL")) {
                        item.Visible = true;
                        continue;
                    }
                    var itemTag = item.Tag;

                    if (itemTag != null) {
                        item.Visible = UserExtention.hasPermissions(loggedInUser, itemTag?.ToString().Split(';'));
                    } else {
                        item.Visible = true;
                    }
                    if (item is ToolStripMenuItem toolStripMenuItem && toolStripMenuItem.DropDownItems.Count > 0) {
                        IterateMenuItems(toolStripMenuItem.DropDownItems);
                    }
                }
            }
        }

        private void tASKSToolStripMenuItem_Click(object sender, EventArgs e) {
            new PadocTaskForm() { MdiParent = this }.Show();

        }

        /*
         * PADOC FORMS
        */
        private void rolesToolStripMenuItem_Click(object sender, EventArgs e) {
            new RoleFormController(
                DatabaseManager.context.Role
            ).loadGrid();
        }

        private void polissenToolStripMenuItem_Click(object sender, EventArgs e) {
            new PolicyFormController(
                DatabaseManager.context.Policy
            ).loadGrid();
        }

        private void cLIENTToolStripMenuItem_Click(object sender, EventArgs e) {
            new ClientFormController(
                DatabaseManager.context.Client
            ).loadGrid();
        }

        private void claimsToolStripMenuItem_Click(object sender, EventArgs e) {
            new ClaimFormController(
                DatabaseManager.context.Claim
            ).loadGrid();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e) {
            new UserFormController(
                DatabaseManager.context.User
            ).loadGrid();
        }

        private void pERMISSIONToolStripMenuItem_Click(object sender, EventArgs e) {
            new PermissionFormController(
                DatabaseManager.context.Permission
            ).loadGrid();
        }

        private void stelEenVraagToolStripMenuItem_Click(object sender, EventArgs e) {
            new PadocAI.AIForm().Show();
        }
    }
}