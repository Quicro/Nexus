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
            cLIENTToolStripMenuItem = new ToolStripMenuItem();
            claimsToolStripMenuItem = new ToolStripMenuItem();
            adminToolStripMenuItem = new ToolStripMenuItem();
            usersToolStripMenuItem = new ToolStripMenuItem();
            rolesToolStripMenuItem = new ToolStripMenuItem();
            pERMISSIONToolStripMenuItem = new ToolStripMenuItem();
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
            dossiersToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { polissenToolStripMenuItem, cLIENTToolStripMenuItem, claimsToolStripMenuItem });
            dossiersToolStripMenuItem.Name = "dossiersToolStripMenuItem";
            dossiersToolStripMenuItem.Size = new Size(58, 23);
            dossiersToolStripMenuItem.Tag = "StartMenu.Dossier.Schade;StartMenu.Dossier.Polis";
            dossiersToolStripMenuItem.Text = "$FILES$";
            // 
            // polissenToolStripMenuItem
            // 
            polissenToolStripMenuItem.Name = "polissenToolStripMenuItem";
            polissenToolStripMenuItem.Size = new Size(180, 22);
            polissenToolStripMenuItem.Tag = "StartMenu.Dossier.Polis";
            polissenToolStripMenuItem.Text = "$POLICIES$";
            polissenToolStripMenuItem.Click += polissenToolStripMenuItem_Click;
            // 
            // cLIENTToolStripMenuItem
            // 
            cLIENTToolStripMenuItem.Name = "cLIENTToolStripMenuItem";
            cLIENTToolStripMenuItem.Size = new Size(180, 22);
            cLIENTToolStripMenuItem.Text = "CLIENT";
            cLIENTToolStripMenuItem.Click += cLIENTToolStripMenuItem_Click;
            // 
            // claimsToolStripMenuItem
            // 
            claimsToolStripMenuItem.Name = "claimsToolStripMenuItem";
            claimsToolStripMenuItem.Size = new Size(180, 22);
            claimsToolStripMenuItem.Text = "Claims";
            claimsToolStripMenuItem.Click += claimsToolStripMenuItem_Click;
            // 
            // adminToolStripMenuItem
            // 
            adminToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { usersToolStripMenuItem, rolesToolStripMenuItem, pERMISSIONToolStripMenuItem });
            adminToolStripMenuItem.Name = "adminToolStripMenuItem";
            adminToolStripMenuItem.Size = new Size(70, 23);
            adminToolStripMenuItem.Tag = "StartMenu.Admin";
            adminToolStripMenuItem.Text = "$ADMIN$";
            // 
            // usersToolStripMenuItem
            // 
            usersToolStripMenuItem.Name = "usersToolStripMenuItem";
            usersToolStripMenuItem.Size = new Size(180, 22);
            usersToolStripMenuItem.Text = "$USERS$";
            usersToolStripMenuItem.Click += usersToolStripMenuItem_Click;
            // 
            // rolesToolStripMenuItem
            // 
            rolesToolStripMenuItem.Name = "rolesToolStripMenuItem";
            rolesToolStripMenuItem.Size = new Size(180, 22);
            rolesToolStripMenuItem.Text = "$ROLES$";
            rolesToolStripMenuItem.Click += rolesToolStripMenuItem_Click;
            // 
            // pERMISSIONToolStripMenuItem
            // 
            pERMISSIONToolStripMenuItem.Name = "pERMISSIONToolStripMenuItem";
            pERMISSIONToolStripMenuItem.Size = new Size(180, 22);
            pERMISSIONToolStripMenuItem.Text = "PERMISSION";
            pERMISSIONToolStripMenuItem.Click += pERMISSIONToolStripMenuItem_Click;
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
            // txtURL
            // 
            txtURL.Alignment = ToolStripItemAlignment.Right;
            txtURL.Name = "txtURL";
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
        private ToolStripMenuItem adminToolStripMenuItem;
        private ToolStripMenuItem usersToolStripMenuItem;
        private ToolStripMenuItem rolesToolStripMenuItem;
        private ToolStripMenuItem dATABASEToolStripMenuItem;
        private ToolStripMenuItem tASKSToolStripMenuItem;
        private ToolStripTextBox txtURL;
        private ToolStripMenuItem pERMISSIONToolStripMenuItem;
        private ToolStripMenuItem cLIENTToolStripMenuItem;
        private ToolStripMenuItem claimsToolStripMenuItem;
    }
}