namespace NexusAI {
    partial class AIForm {
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
            txtResponse = new TextBox();
            listBox = new ListBox();
            btnSend = new Button();
            txtSQL = new TextBox();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // txtResponse
            // 
            txtResponse.Location = new Point(6, 22);
            txtResponse.Multiline = true;
            txtResponse.Name = "txtResponse";
            txtResponse.Size = new Size(426, 309);
            txtResponse.TabIndex = 2;
            // 
            // listBox
            // 
            listBox.FormattingEnabled = true;
            listBox.ItemHeight = 15;
            listBox.Location = new Point(12, 12);
            listBox.Name = "listBox";
            listBox.Size = new Size(538, 214);
            listBox.TabIndex = 3;
            listBox.Click += listBox_Click;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(556, 12);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(232, 214);
            btnSend.TabIndex = 0;
            btnSend.Text = "Verzenden";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // txtSQL
            // 
            txtSQL.Location = new Point(6, 22);
            txtSQL.Multiline = true;
            txtSQL.Name = "txtSQL";
            txtSQL.Size = new Size(320, 309);
            txtSQL.TabIndex = 4;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtSQL);
            groupBox1.Location = new Point(12, 244);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(332, 337);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "SQL";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtResponse);
            groupBox2.Location = new Point(350, 244);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(438, 337);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Antwoord";
            // 
            // AIForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(804, 593);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(listBox);
            Controls.Add(btnSend);
            Name = "AIForm";
            Text = "AIForm";
            Load += AIForm_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        public TextBox txtResponse;
        private ListBox listBox;
        private Button btnSend;
        public TextBox txtSQL;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
    }
}
