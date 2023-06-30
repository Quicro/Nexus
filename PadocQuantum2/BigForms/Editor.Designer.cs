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
            editorUserControl = new BigControls.EditorUserControl();
            SuspendLayout();
            // 
            // editorUserControl
            // 
            editorUserControl.Location = new Point(12, 12);
            editorUserControl.Name = "editorUserControl";
            editorUserControl.Size = new Size(776, 426);
            editorUserControl.TabIndex = 0;
            // 
            // Editor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(editorUserControl);
            Name = "Editor";
            Text = "Editor";
            ResumeLayout(false);
        }

        #endregion

        public BigControls.EditorUserControl editorUserControl;
    }
}