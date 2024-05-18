using NexusEF.Extentions;
using NexusEF.Models;
using System.Globalization;
using System.Resources;

namespace PatdocQuantum {
    public partial class LoginForm : Form {
        public string username;
        public string password;
        public bool doLogin = false;

        public LoginForm() {
            InitializeComponent();

            foreach (Control control in Controls) {
                updateTextOfControl(control);
            }
            updateTextOfControl(this);
        }


        protected void updateTextOfControl(Control control) {
            ResourceManager resourceManager = new ResourceManager("NexusOld.Translations.Strings", typeof(NexusMDIForm).Assembly);
            string translatedText = resourceManager.GetString(control.Text.Replace("$", ""), CultureInfo.CurrentCulture) ?? "";
            control.Text = translatedText;
        }

        private void button1_Click(object sender, EventArgs e) {
            username = txtUsername.Text;
            password = txtPassword.Text;

            User? loggedInUser = UserExtention.validate(username, password);
            if (loggedInUser is null) {
                MessageBox.Show("Combination does not exist", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else {
                ((NexusMDIForm?)MdiParent)!.loginAsUser(loggedInUser);
                Close();
            }
        }
    }
}
