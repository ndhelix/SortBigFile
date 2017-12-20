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
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtLineCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtMaxNum = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMinLen = new System.Windows.Forms.TextBox();
            this.txtMaxLen = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtFile
            // 
            this.txtFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFile.Location = new System.Drawing.Point(12, 12);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(295, 27);
            this.txtFile.TabIndex = 0;
            this.txtFile.Text = "D:\\D\\_BIGFILE\\smallfile.txt";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 203);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(276, 38);
            this.button1.TabIndex = 100;
            this.button1.Text = "Create file with random text";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(18, 260);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(57, 20);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Status";
            // 
            // txtLineCount
            // 
            this.txtLineCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLineCount.Location = new System.Drawing.Point(169, 56);
            this.txtLineCount.MaxLength = 6;
            this.txtLineCount.Name = "txtLineCount";
            this.txtLineCount.Size = new System.Drawing.Size(138, 27);
            this.txtLineCount.TabIndex = 10;
            this.txtLineCount.Text = "100";
            this.txtLineCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLineCount_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "File size, Mb:";
            // 
            // txtMaxNum
            // 
            this.txtMaxNum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaxNum.Location = new System.Drawing.Point(169, 89);
            this.txtMaxNum.MaxLength = 10;
            this.txtMaxNum.Name = "txtMaxNum";
            this.txtMaxNum.Size = new System.Drawing.Size(138, 27);
            this.txtMaxNum.TabIndex = 20;
            this.txtMaxNum.Text = "1000000";
            this.txtMaxNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLineCount_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Max number value";
            // 
            // txtMinLen
            // 
            this.txtMinLen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMinLen.Location = new System.Drawing.Point(169, 123);
            this.txtMinLen.MaxLength = 4;
            this.txtMinLen.Name = "txtMinLen";
            this.txtMinLen.Size = new System.Drawing.Size(138, 27);
            this.txtMinLen.TabIndex = 30;
            this.txtMinLen.Text = "5";
            this.txtMinLen.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLineCount_KeyPress);
            // 
            // txtMaxLen
            // 
            this.txtMaxLen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaxLen.Location = new System.Drawing.Point(169, 156);
            this.txtMaxLen.MaxLength = 4;
            this.txtMaxLen.Name = "txtMaxLen";
            this.txtMaxLen.Size = new System.Drawing.Size(138, 27);
            this.txtMaxLen.TabIndex = 40;
            this.txtMaxLen.Text = "100";
            this.txtMaxLen.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLineCount_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Min string length";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Max string length";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 294);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtMaxLen);
            this.Controls.Add(this.txtMinLen);
            this.Controls.Add(this.txtMaxNum);
            this.Controls.Add(this.txtLineCount);
            this.Controls.Add(this.txtFile);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Big file generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtLineCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtMaxNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMinLen;
        private System.Windows.Forms.TextBox txtMaxLen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

