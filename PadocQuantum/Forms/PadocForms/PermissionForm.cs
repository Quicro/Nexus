﻿namespace PadocQuantum {
    public partial class PermissionForm : Form {
        public PermissionForm() {
            InitializeComponent();

            Text = GetType().Name;
        }

        private void buttonClick(object sender, EventArgs e) {
            PadocForm.buttonClick(sender, e);
        }

        private void onLoad(object sender, EventArgs e) {
            PadocForm.onLoad(groupBox);
        }
    }
}
