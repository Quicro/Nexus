namespace PadocQuantum2.BigControls {
    partial class TypeSelectorItem {
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
            components = new System.ComponentModel.Container();
            btn = new Button();
            claimBindingSource = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)claimBindingSource).BeginInit();
            SuspendLayout();
            // 
            // btn
            // 
            btn.Cursor = Cursors.Hand;
            btn.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            btn.ImageAlign = ContentAlignment.MiddleRight;
            btn.Location = new Point(0, 3);
            btn.Name = "btn";
            btn.Size = new Size(305, 70);
            btn.TabIndex = 0;
            btn.Text = "placeholder";
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.TextImageRelation = TextImageRelation.TextBeforeImage;
            btn.UseVisualStyleBackColor = true;
            btn.Click += button1_Click;
            // 
            // claimBindingSource
            // 
            claimBindingSource.DataSource = typeof(PadocEF.Models.Claim);
            // 
            // TypeSelectorItem
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btn);
            Name = "TypeSelectorItem";
            Size = new Size(308, 73);
            ((System.ComponentModel.ISupportInitialize)claimBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button btn;
        private BindingSource claimBindingSource;
    }
}
