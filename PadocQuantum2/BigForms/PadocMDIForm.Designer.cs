using PadocQuantum2.Interfaces;

namespace PadocQuantum2 {
    partial class PadocMDIForm  {
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
            menuStrip1 = new MenuStrip();
            typesToolStripMenuItem = new ToolStripMenuItem();
            entityViewerToolStripMenuItem = new ToolStripMenuItem();
            entityEditorToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { typesToolStripMenuItem, entityViewerToolStripMenuItem, entityEditorToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1360, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // typesToolStripMenuItem
            // 
            typesToolStripMenuItem.Name = "typesToolStripMenuItem";
            typesToolStripMenuItem.Size = new Size(48, 20);
            typesToolStripMenuItem.Text = "&Types";
            typesToolStripMenuItem.Click += typesToolStripMenuItem_Click;
            // 
            // entityViewerToolStripMenuItem
            // 
            entityViewerToolStripMenuItem.Name = "entityViewerToolStripMenuItem";
            entityViewerToolStripMenuItem.Size = new Size(54, 20);
            entityViewerToolStripMenuItem.Text = "&Viewer";
            // 
            // entityEditorToolStripMenuItem
            // 
            entityEditorToolStripMenuItem.Name = "entityEditorToolStripMenuItem";
            entityEditorToolStripMenuItem.Size = new Size(50, 20);
            entityEditorToolStripMenuItem.Text = "&Editor";
            // 
            // PadocMDIForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1360, 494);
            Controls.Add(menuStrip1);
            HelpButton = true;
            IsMdiContainer = true;
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Name = "PadocMDIForm";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem typesToolStripMenuItem;
        private ToolStripMenuItem entityViewerToolStripMenuItem;
        private ToolStripMenuItem entityEditorToolStripMenuItem;
    }
}