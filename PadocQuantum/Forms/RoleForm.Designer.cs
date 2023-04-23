namespace PadocQuantum.Forms {
    partial class RoleForm {
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
            textBox1 = new TextBox();
            padocGrid = new Controls.PadocGrid();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(612, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 0;
            // 
            // padocGrid
            // 
            padocGrid.Location = new Point(12, 12);
            padocGrid.Name = "padocGrid";
            padocGrid.Size = new Size(531, 403);
            padocGrid.TabIndex = 1;
            // 
            // RoleForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(padocGrid);
            Controls.Add(textBox1);
            Name = "RoleForm";
            Text = "RoleForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public TextBox textBox1;
        public Controls.PadocGrid padocGrid;
    }
}