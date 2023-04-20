using PadocEF;
using PadocEF.Extentions;
using PadocEF.Models;
using PadocQuantum;
using System.Globalization;
using System.Resources;

namespace PatdocQuantum {
    public partial class PadocMDIForm : Form {
        User loggedInUser;

        public PadocMDIForm(string arg) {
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

                    if (UserExtentions.userHasPermissions(loggedInUser, "ALL")) {
                        item.Visible = true;
                        continue;
                    }
                    var itemTag = item.Tag;

                    if (itemTag != null) {
                        item.Visible = UserExtentions.userHasPermissions(loggedInUser, itemTag?.ToString().Split(';'));
                    } else {
                        item.Visible = true;
                    }
                    if (item is ToolStripMenuItem toolStripMenuItem && toolStripMenuItem.DropDownItems.Count > 0) {
                        IterateMenuItems(toolStripMenuItem.DropDownItems);
                    }
                }
            }
        }

        private void polissenToolStripMenuItem_Click(object sender, EventArgs e) {
            PoliciesForm form = new PoliciesForm() {
                MdiParent = this,
                StartPosition = FormStartPosition.CenterParent
            };

            form.Show();
        }

        private void tASKSToolStripMenuItem_Click(object sender, EventArgs e) {
            new PadocTaskForm() { MdiParent = this}.Show();

        }

        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == '\r') {
                RequestHandler.Handle(txtURL.Text);
            }
        }
    }
}