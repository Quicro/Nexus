namespace PatdocQuantum {
    partial class PadocMDIForm {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            menuStrip = new MenuStrip();
            dossiersToolStripMenuItem = new ToolStripMenuItem();
            polissenToolStripMenuItem = new ToolStripMenuItem();
            schadesToolStripMenuItem = new ToolStripMenuItem();
            aOToolStripMenuItem = new ToolStripMenuItem();
            bRAToolStripMenuItem = new ToolStripMenuItem();
            bASBAGToolStripMenuItem = new ToolStripMenuItem();
            bAXToolStripMenuItem = new ToolStripMenuItem();
            oMNToolStripMenuItem = new ToolStripMenuItem();
            adminToolStripMenuItem = new ToolStripMenuItem();
            usersToolStripMenuItem = new ToolStripMenuItem();
            rolesToolStripMenuItem = new ToolStripMenuItem();
            dATABASEToolStripMenuItem = new ToolStripMenuItem();
            tASKSToolStripMenuItem = new ToolStripMenuItem();
            txtURL = new ToolStripTextBox();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { dossiersToolStripMenuItem, adminToolStripMenuItem, dATABASEToolStripMenuItem, txtURL });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1841, 27);
            menuStrip.TabIndex = 1;
            menuStrip.Text = "menuStrip";
            // 
            // dossiersToolStripMenuItem
            // 
            dossiersToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { polissenToolStripMenuItem, schadesToolStripMenuItem });
            dossiersToolStripMenuItem.Name = "dossiersToolStripMenuItem";
            dossiersToolStripMenuItem.Size = new Size(58, 23);
            dossiersToolStripMenuItem.Tag = "StartMenu.Dossier.Schade;StartMenu.Dossier.Polis";
            dossiersToolStripMenuItem.Text = "$FILES$";
            // 
            // polissenToolStripMenuItem
            // 
            polissenToolStripMenuItem.Name = "polissenToolStripMenuItem";
            polissenToolStripMenuItem.Size = new Size(134, 22);
            polissenToolStripMenuItem.Tag = "StartMenu.Dossier.Polis";
            polissenToolStripMenuItem.Text = "$POLICIES$";
            polissenToolStripMenuItem.Click += polissenToolStripMenuItem_Click;
            // 
            // schadesToolStripMenuItem
            // 
            schadesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aOToolStripMenuItem, bRAToolStripMenuItem, bASBAGToolStripMenuItem, bAXToolStripMenuItem, oMNToolStripMenuItem });
            schadesToolStripMenuItem.Name = "schadesToolStripMenuItem";
            schadesToolStripMenuItem.Size = new Size(134, 22);
            schadesToolStripMenuItem.Tag = "StartMenu.Dossier.Schade";
            schadesToolStripMenuItem.Text = "$CLAIMS$";
            // 
            // aOToolStripMenuItem
            // 
            aOToolStripMenuItem.Name = "aOToolStripMenuItem";
            aOToolStripMenuItem.Size = new Size(135, 22);
            aOToolStripMenuItem.Text = "1 - AO";
            // 
            // bRAToolStripMenuItem
            // 
            bRAToolStripMenuItem.Name = "bRAToolStripMenuItem";
            bRAToolStripMenuItem.Size = new Size(135, 22);
            bRAToolStripMenuItem.Text = "3 - BRA";
            // 
            // bASBAGToolStripMenuItem
            // 
            bASBAGToolStripMenuItem.Name = "bASBAGToolStripMenuItem";
            bASBAGToolStripMenuItem.Size = new Size(135, 22);
            bASBAGToolStripMenuItem.Text = "5 - BASBAG";
            // 
            // bAXToolStripMenuItem
            // 
            bAXToolStripMenuItem.Name = "bAXToolStripMenuItem";
            bAXToolStripMenuItem.Size = new Size(135, 22);
            bAXToolStripMenuItem.Text = "6 - BAX";
            // 
            // oMNToolStripMenuItem
            // 
            oMNToolStripMenuItem.Name = "oMNToolStripMenuItem";
            oMNToolStripMenuItem.Size = new Size(135, 22);
            oMNToolStripMenuItem.Text = "7 - OMN";
            // 
            // adminToolStripMenuItem
            // 
            adminToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { usersToolStripMenuItem, rolesToolStripMenuItem });
            adminToolStripMenuItem.Name = "adminToolStripMenuItem";
            adminToolStripMenuItem.Size = new Size(70, 23);
            adminToolStripMenuItem.Tag = "StartMenu.Admin";
            adminToolStripMenuItem.Text = "$ADMIN$";
            // 
            // usersToolStripMenuItem
            // 
            usersToolStripMenuItem.Name = "usersToolStripMenuItem";
            usersToolStripMenuItem.Size = new Size(120, 22);
            usersToolStripMenuItem.Text = "$USERS$";
            // 
            // rolesToolStripMenuItem
            // 
            rolesToolStripMenuItem.Name = "rolesToolStripMenuItem";
            rolesToolStripMenuItem.Size = new Size(120, 22);
            rolesToolStripMenuItem.Text = "$ROLES$";
            // 
            // dATABASEToolStripMenuItem
            // 
            dATABASEToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tASKSToolStripMenuItem });
            dATABASEToolStripMenuItem.Name = "dATABASEToolStripMenuItem";
            dATABASEToolStripMenuItem.Size = new Size(87, 23);
            dATABASEToolStripMenuItem.Tag = "StartMenu.Database";
            dATABASEToolStripMenuItem.Text = "$DATABASE$";
            // 
            // tASKSToolStripMenuItem
            // 
            tASKSToolStripMenuItem.Name = "tASKSToolStripMenuItem";
            tASKSToolStripMenuItem.Size = new Size(119, 22);
            tASKSToolStripMenuItem.Text = "$TASKS$";
            tASKSToolStripMenuItem.Click += tASKSToolStripMenuItem_Click;
            // 
            // toolStripTextBox1
            // 
            txtURL.Alignment = ToolStripItemAlignment.Right;
            txtURL.Name = "toolStripTextBox1";
            txtURL.Size = new Size(500, 23);
            txtURL.Text = "padoc://";
            txtURL.KeyPress += toolStripTextBox1_KeyPress;
            // 
            // PadocMDIForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1841, 810);
            Controls.Add(menuStrip);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip;
            Name = "PadocMDIForm";
            Text = "Padoc";
            Load += PadocMDIForm_Load;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem dossiersToolStripMenuItem;
        private ToolStripMenuItem polissenToolStripMenuItem;
        private ToolStripMenuItem schadesToolStripMenuItem;
        private ToolStripMenuItem adminToolStripMenuItem;
        private ToolStripMenuItem usersToolStripMenuItem;
        private ToolStripMenuItem rolesToolStripMenuItem;
        private ToolStripMenuItem aOToolStripMenuItem;
        private ToolStripMenuItem bRAToolStripMenuItem;
        private ToolStripMenuItem bASBAGToolStripMenuItem;
        private ToolStripMenuItem bAXToolStripMenuItem;
        private ToolStripMenuItem oMNToolStripMenuItem;
        private ToolStripMenuItem dATABASEToolStripMenuItem;
        private ToolStripMenuItem tASKSToolStripMenuItem;
        private ToolStripTextBox txtURL;
    }
}