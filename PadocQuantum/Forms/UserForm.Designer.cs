/*namespace PadocQuantum {
    partial class UserForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            padocGrid = new Controls.PadocGrid();
            splitContainer1 = new SplitContainer();
            groupBox1 = new GroupBox();
            txtID = new TextBox();
            lblID = new Label();
            btnPermissions = new Button();
            btnRoles = new Button();
            txtPassword = new TextBox();
            label5 = new Label();
            txtPhone = new TextBox();
            label4 = new Label();
            txtEmail = new TextBox();
            label3 = new Label();
            txtAdName = new TextBox();
            label2 = new Label();
            txtName = new TextBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // padocGrid
            // 
            padocGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            padocGrid.Location = new Point(3, 3);
            padocGrid.Name = "padocGrid";
            padocGrid.Size = new Size(874, 489);
            padocGrid.TabIndex = 0;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(12, 12);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(padocGrid);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(groupBox1);
            splitContainer1.Size = new Size(1106, 495);
            splitContainer1.SplitterDistance = 868;
            splitContainer1.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(txtID);
            groupBox1.Controls.Add(lblID);
            groupBox1.Controls.Add(btnPermissions);
            groupBox1.Controls.Add(btnRoles);
            groupBox1.Controls.Add(txtPassword);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(txtPhone);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txtEmail);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtAdName);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtName);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(216, 489);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "groupBox1";
            // 
            // txtID
            // 
            txtID.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtID.Location = new Point(71, 16);
            txtID.Name = "txtID";
            txtID.Size = new Size(141, 23);
            txtID.TabIndex = 13;
            // 
            // lblID
            // 
            lblID.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblID.AutoSize = true;
            lblID.Location = new Point(8, 19);
            lblID.Name = "lblID";
            lblID.Size = new Size(18, 15);
            lblID.TabIndex = 12;
            lblID.Text = "ID";
            // 
            // btnPermissions
            // 
            btnPermissions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnPermissions.Location = new Point(71, 219);
            btnPermissions.Name = "btnPermissions";
            btnPermissions.Size = new Size(141, 23);
            btnPermissions.TabIndex = 11;
            btnPermissions.Text = "Permissies";
            btnPermissions.UseVisualStyleBackColor = true;
            btnPermissions.Click += btnPermissions_Click;
            // 
            // btnRoles
            // 
            btnRoles.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnRoles.Location = new Point(71, 190);
            btnRoles.Name = "btnRoles";
            btnRoles.Size = new Size(141, 23);
            btnRoles.TabIndex = 10;
            btnRoles.Text = "Rollen";
            btnRoles.UseVisualStyleBackColor = true;
            btnRoles.Click += btnRoles_Click;
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPassword.Location = new Point(71, 161);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(141, 23);
            txtPassword.TabIndex = 9;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Location = new Point(8, 164);
            label5.Name = "label5";
            label5.Size = new Size(57, 15);
            label5.TabIndex = 8;
            label5.Text = "Password";
            // 
            // txtPhone
            // 
            txtPhone.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPhone.Location = new Point(71, 132);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(141, 23);
            txtPhone.TabIndex = 7;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(8, 135);
            label4.Name = "label4";
            label4.Size = new Size(41, 15);
            label4.TabIndex = 6;
            label4.Text = "Phone";
            // 
            // txtEmail
            // 
            txtEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtEmail.Location = new Point(71, 103);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(141, 23);
            txtEmail.TabIndex = 5;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(8, 106);
            label3.Name = "label3";
            label3.Size = new Size(36, 15);
            label3.TabIndex = 4;
            label3.Text = "Email";
            // 
            // txtAdName
            // 
            txtAdName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtAdName.Location = new Point(71, 74);
            txtAdName.Name = "txtAdName";
            txtAdName.Size = new Size(141, 23);
            txtAdName.TabIndex = 3;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(8, 77);
            label2.Name = "label2";
            label2.Size = new Size(58, 15);
            label2.TabIndex = 2;
            label2.Text = "AD Name";
            // 
            // txtName
            // 
            txtName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtName.Location = new Point(71, 45);
            txtName.Name = "txtName";
            txtName.Size = new Size(141, 23);
            txtName.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(8, 48);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 0;
            label1.Text = "Name";
            // 
            // UserForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1130, 519);
            Controls.Add(splitContainer1);
            Name = "UserForm";
            Text = "UserForm";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Controls.PadocGrid padocGrid;
        private SplitContainer splitContainer1;
        private GroupBox groupBox1;
        private Button btnPermissions;
        private Button btnRoles;
        private TextBox txtPassword;
        private Label label5;
        private TextBox txtPhone;
        private Label label4;
        private TextBox txtEmail;
        private Label label3;
        private TextBox txtAdName;
        private Label label2;
        private TextBox txtName;
        private Label label1;
        private TextBox txtID;
        private Label lblID;
    }
}*/