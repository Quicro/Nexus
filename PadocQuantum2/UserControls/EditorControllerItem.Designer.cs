namespace PadocQuantum2.Controls {
    partial class EditorControllerItem {
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
            label = new Label();
            textBox = new TextBox();
            button = new Button();
            SuspendLayout();
            // 
            // label
            // 
            label.AutoSize = true;
            label.Location = new Point(12, 11);
            label.Name = "label";
            label.Size = new Size(32, 15);
            label.TabIndex = 0;
            label.Text = "label";
            // 
            // textBox
            // 
            textBox.Location = new Point(91, 8);
            textBox.Name = "textBox";
            textBox.Size = new Size(128, 23);
            textBox.TabIndex = 1;
            // 
            // button
            // 
            button.Location = new Point(225, 8);
            button.Name = "button";
            button.Size = new Size(129, 23);
            button.TabIndex = 2;
            button.Text = "button1";
            button.UseVisualStyleBackColor = true;
            // 
            // EditorControllerItem
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(button);
            Controls.Add(textBox);
            Controls.Add(label);
            Name = "EditorControllerItem";
            Size = new Size(357, 38);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label;
        private TextBox textBox;
        private Button button;
    }
}
