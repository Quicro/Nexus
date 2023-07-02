namespace PadocQuantum2.BigForms {
    partial class Editor {
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
            components = new System.ComponentModel.Container();
            editorUserControl = new BigControls.EditorUserControl();
            dataGridView1 = new DataGridView();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            nameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            numberDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            policyIdDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            policyDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            claimBindingSource = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)claimBindingSource).BeginInit();
            SuspendLayout();
            // 
            // editorUserControl
            // 
            editorUserControl.Location = new Point(12, 12);
            editorUserControl.Name = "editorUserControl";
            editorUserControl.Size = new Size(776, 426);
            editorUserControl.TabIndex = 0;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { idDataGridViewTextBoxColumn, nameDataGridViewTextBoxColumn, numberDataGridViewTextBoxColumn, policyIdDataGridViewTextBoxColumn, policyDataGridViewTextBoxColumn });
            dataGridView1.DataSource = claimBindingSource;
            dataGridView1.Location = new Point(12, 307);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(776, 131);
            dataGridView1.TabIndex = 1;
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            idDataGridViewTextBoxColumn.HeaderText = "Id";
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // nameDataGridViewTextBoxColumn
            // 
            nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            nameDataGridViewTextBoxColumn.HeaderText = "Name";
            nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // numberDataGridViewTextBoxColumn
            // 
            numberDataGridViewTextBoxColumn.DataPropertyName = "Number";
            numberDataGridViewTextBoxColumn.HeaderText = "Number";
            numberDataGridViewTextBoxColumn.Name = "numberDataGridViewTextBoxColumn";
            // 
            // policyIdDataGridViewTextBoxColumn
            // 
            policyIdDataGridViewTextBoxColumn.DataPropertyName = "PolicyId";
            policyIdDataGridViewTextBoxColumn.HeaderText = "PolicyId";
            policyIdDataGridViewTextBoxColumn.Name = "policyIdDataGridViewTextBoxColumn";
            // 
            // policyDataGridViewTextBoxColumn
            // 
            policyDataGridViewTextBoxColumn.DataPropertyName = "Policy";
            policyDataGridViewTextBoxColumn.HeaderText = "Policy";
            policyDataGridViewTextBoxColumn.Name = "policyDataGridViewTextBoxColumn";
            // 
            // claimBindingSource
            // 
            claimBindingSource.DataSource = typeof(PadocEF.Models.Claim);
            // 
            // Editor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridView1);
            Controls.Add(editorUserControl);
            Name = "Editor";
            Text = "Editor";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)claimBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public BigControls.EditorUserControl editorUserControl;
        private BindingSource claimBindingSource;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn numberDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn policyIdDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn policyDataGridViewTextBoxColumn;
        public DataGridView dataGridView1;
    }
}