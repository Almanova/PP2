namespace Intranet
{
    partial class InsertSubject
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textSubjectName = new System.Windows.Forms.TextBox();
            this.textTeacher = new System.Windows.Forms.TextBox();
            this.textRoom = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 31);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(404, 241);
            this.dataGridView1.TabIndex = 2;
            // 
            // textSubjectName
            // 
            this.textSubjectName.Location = new System.Drawing.Point(434, 77);
            this.textSubjectName.Name = "textSubjectName";
            this.textSubjectName.Size = new System.Drawing.Size(114, 20);
            this.textSubjectName.TabIndex = 3;
            // 
            // textTeacher
            // 
            this.textTeacher.Location = new System.Drawing.Point(434, 113);
            this.textTeacher.Name = "textTeacher";
            this.textTeacher.Size = new System.Drawing.Size(114, 20);
            this.textTeacher.TabIndex = 4;
            // 
            // textRoom
            // 
            this.textRoom.Location = new System.Drawing.Point(434, 149);
            this.textRoom.Name = "textRoom";
            this.textRoom.Size = new System.Drawing.Size(114, 20);
            this.textRoom.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(434, 190);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 38);
            this.button1.TabIndex = 7;
            this.button1.Text = "Add Subject";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // InsertSubject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 299);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textRoom);
            this.Controls.Add(this.textTeacher);
            this.Controls.Add(this.textSubjectName);
            this.Controls.Add(this.dataGridView1);
            this.Name = "InsertSubject";
            this.Text = "InsertSubject";
            this.Load += new System.EventHandler(this.InsertSubject_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textSubjectName;
        private System.Windows.Forms.TextBox textTeacher;
        private System.Windows.Forms.TextBox textRoom;
        private System.Windows.Forms.Button button1;
    }
}