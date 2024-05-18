namespace NexusOld.Controls {
    partial class NexusGrid {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            theGrid = new DataGridView();
            lblStatus = new Label();
            btnCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)theGrid).BeginInit();
            SuspendLayout();
            // 
            // theGrid
            // 
            theGrid.AllowUserToAddRows = false;
            theGrid.AllowUserToDeleteRows = false;
            theGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            theGrid.Location = new Point(15, 17);
            theGrid.Name = "theGrid";
            theGrid.RowTemplate.Height = 25;
            theGrid.Size = new Size(499, 366);
            theGrid.TabIndex = 0;
            theGrid.CellClick += theGrid_CellContentClick;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.None;
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(210, 169);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(59, 15);
            lblStatus.TabIndex = 1;
            lblStatus.Text = "$STATUS$";
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.None;
            btnCancel.Location = new Point(210, 187);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(102, 29);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel query";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // NexusGrid
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnCancel);
            Controls.Add(lblStatus);
            Controls.Add(theGrid);
            Name = "NexusGrid";
            Size = new Size(531, 403);
            ((System.ComponentModel.ISupportInitialize)theGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        public DataGridView theGrid;
        public Label lblStatus;
        public Button btnCancel;
    }
}
