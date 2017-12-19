namespace SortBigFile
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtFile = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSort = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.txtHashToFind = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtLineCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(22, 12);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(241, 27);
            this.txtFile.TabIndex = 0;
            this.txtFile.Text = "D:\\D\\_BIGFILE\\smallfile.txt";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(22, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(276, 38);
            this.button1.TabIndex = 1;
            this.button1.Text = "Create file with random text";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSort
            // 
            this.btnSort.Location = new System.Drawing.Point(22, 106);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(144, 38);
            this.btnSort.TabIndex = 1;
            this.btnSort.Text = "Sort file";
            this.btnSort.UseVisualStyleBackColor = true;
            this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(22, 223);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(144, 38);
            this.button3.TabIndex = 1;
            this.button3.Text = "Check if exists";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(172, 241);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(62, 20);
            this.lblResult.TabIndex = 2;
            this.lblResult.Text = "Result:";
            // 
            // txtHashToFind
            // 
            this.txtHashToFind.Location = new System.Drawing.Point(22, 190);
            this.txtHashToFind.Name = "txtHashToFind";
            this.txtHashToFind.Size = new System.Drawing.Size(490, 27);
            this.txtHashToFind.TabIndex = 0;
            this.txtHashToFind.Text = "39 9E 90 E7 51 62 98 39  99 C9 71 83 D4 22 5E 55";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(18, 302);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(57, 20);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Status";
            // 
            // txtLineCount
            // 
            this.txtLineCount.Location = new System.Drawing.Point(384, 12);
            this.txtLineCount.Name = "txtLineCount";
            this.txtLineCount.Size = new System.Drawing.Size(148, 27);
            this.txtLineCount.TabIndex = 0;
            this.txtLineCount.Text = "200";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(269, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "File size, Mb:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 360);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnSort);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtHashToFind);
            this.Controls.Add(this.txtLineCount);
            this.Controls.Add(this.txtFile);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSort;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.TextBox txtHashToFind;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtLineCount;
        private System.Windows.Forms.Label label1;
    }
}

