namespace GameRandomStarter
{
    partial class TagAdder
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
            this.Add = new System.Windows.Forms.Button();
            this.Delete = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.TagList = new System.Windows.Forms.ListBox();
            this.Done = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Add
            // 
            this.Add.Enabled = false;
            this.Add.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Add.Location = new System.Drawing.Point(275, 10);
            this.Add.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(79, 28);
            this.Add.TabIndex = 0;
            this.Add.Text = "Add";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // Delete
            // 
            this.Delete.Enabled = false;
            this.Delete.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Delete.Location = new System.Drawing.Point(275, 42);
            this.Delete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(79, 28);
            this.Delete.TabIndex = 1;
            this.Delete.Text = "Delete";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox1.Location = new System.Drawing.Point(10, 12);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(259, 25);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // TagList
            // 
            this.TagList.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TagList.FormattingEnabled = true;
            this.TagList.ItemHeight = 15;
            this.TagList.Location = new System.Drawing.Point(10, 75);
            this.TagList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TagList.Name = "TagList";
            this.TagList.Size = new System.Drawing.Size(344, 139);
            this.TagList.TabIndex = 3;
            this.TagList.SelectedValueChanged += new System.EventHandler(this.TagList_SelectedValueChanged);
            // 
            // Done
            // 
            this.Done.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Done.Location = new System.Drawing.Point(275, 227);
            this.Done.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Done.Name = "Done";
            this.Done.Size = new System.Drawing.Size(79, 28);
            this.Done.TabIndex = 4;
            this.Done.Text = "Done";
            this.Done.UseVisualStyleBackColor = true;
            this.Done.Click += new System.EventHandler(this.Done_Click);
            // 
            // TagAdder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(364, 262);
            this.Controls.Add(this.Done);
            this.Controls.Add(this.TagList);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Delete);
            this.Controls.Add(this.Add);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "TagAdder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TagAdder";
            this.Load += new System.EventHandler(this.TagAdder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListBox TagList;
        private System.Windows.Forms.Button Done;
    }
}