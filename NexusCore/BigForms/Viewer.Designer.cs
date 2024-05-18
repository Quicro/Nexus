namespace NexusCore.BigForms {
    partial class Viewer {
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
            viewerUserControl = new BigControls.ViewerUserControl();
            SuspendLayout();
            // 
            // viewerUserControl1
            // 
            viewerUserControl.Location = new Point(12, 12);
            viewerUserControl.Name = "viewerUserControl1";
            viewerUserControl.Size = new Size(776, 426);
            viewerUserControl.TabIndex = 0;
            // 
            // Viewer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(viewerUserControl);
            Name = "Viewer";
            Text = "Viewer";
            ResumeLayout(false);
        }

        #endregion

        public BigControls.ViewerUserControl viewerUserControl;
    }
}
