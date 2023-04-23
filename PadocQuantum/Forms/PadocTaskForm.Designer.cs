namespace PadocQuantum {
    partial class PadocTaskForm {
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
            gridView = new DataGridView();
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)gridView).BeginInit();
            SuspendLayout();
            // 
            // gridView
            // 
            gridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridView.Location = new Point(12, 12);
            gridView.Name = "gridView";
            gridView.RowTemplate.Height = 25;
            gridView.Size = new Size(766, 330);
            gridView.TabIndex = 0;
            gridView.CellClick += dataGridView1_CellContentClick;
            gridView.Click += dataGridView1_Click;
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // PadocTaskForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(gridView);
            Name = "PadocTaskForm";
            Text = "PadocTaskForm";
            Load += PadocTaskForm_Load;
            ((System.ComponentModel.ISupportInitialize)gridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView gridView;
        private System.Windows.Forms.Timer timer1;
    }
}