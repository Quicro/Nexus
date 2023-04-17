namespace PatdocQuantum {
    partial class PoliciesForm {
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
            padocGrid1 = new PadocQuantum.Controls.PadocGrid();
            SuspendLayout();
            // 
            // padocGrid1
            // 
            padocGrid1.Location = new Point(12, 12);
            padocGrid1.Name = "padocGrid1";
            padocGrid1.Size = new Size(548, 426);
            padocGrid1.TabIndex = 8;
            // 
            // PoliciesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(567, 447);
            Controls.Add(padocGrid1);
            Name = "PoliciesForm";
            Text = "PoliciesForm";
            Load += DataLoad;
            ResumeLayout(false);
        }

        #endregion
        private PadocQuantum.Controls.PadocGrid padocGrid1;
    }
}