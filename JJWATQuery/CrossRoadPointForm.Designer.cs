namespace JJWATQuery
{
    partial class CrossRoadPointForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CrossRoadPointForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.roadAList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.roadNameATxt = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.roadBList = new System.Windows.Forms.ListBox();
            this.roadNameBTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.roadAList);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.roadNameATxt);
            this.groupBox1.Location = new System.Drawing.Point(13, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(286, 155);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "道路选择";
            // 
            // roadAList
            // 
            this.roadAList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.roadAList.FormattingEnabled = true;
            this.roadAList.ItemHeight = 12;
            this.roadAList.Location = new System.Drawing.Point(55, 53);
            this.roadAList.Name = "roadAList";
            this.roadAList.Size = new System.Drawing.Size(221, 88);
            this.roadAList.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "道路名";
            // 
            // roadNameATxt
            // 
            this.roadNameATxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.roadNameATxt.Location = new System.Drawing.Point(55, 25);
            this.roadNameATxt.Name = "roadNameATxt";
            this.roadNameATxt.Size = new System.Drawing.Size(222, 21);
            this.roadNameATxt.TabIndex = 0;
            this.roadNameATxt.TextChanged += new System.EventHandler(this.roadNameATxt_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.roadBList);
            this.groupBox2.Controls.Add(this.roadNameBTxt);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(13, 168);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(287, 174);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "道路选择";
            // 
            // roadBList
            // 
            this.roadBList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.roadBList.FormattingEnabled = true;
            this.roadBList.ItemHeight = 12;
            this.roadBList.Location = new System.Drawing.Point(56, 60);
            this.roadBList.Name = "roadBList";
            this.roadBList.Size = new System.Drawing.Size(222, 100);
            this.roadBList.TabIndex = 2;
            // 
            // roadNameBTxt
            // 
            this.roadNameBTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.roadNameBTxt.Location = new System.Drawing.Point(56, 26);
            this.roadNameBTxt.Name = "roadNameBTxt";
            this.roadNameBTxt.Size = new System.Drawing.Size(222, 21);
            this.roadNameBTxt.TabIndex = 1;
            this.roadNameBTxt.TextChanged += new System.EventHandler(this.roadNameBTxt_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "道路名";
            // 
            // pBtn
            // 
            this.pBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pBtn.Location = new System.Drawing.Point(134, 348);
            this.pBtn.Name = "pBtn";
            this.pBtn.Size = new System.Drawing.Size(75, 23);
            this.pBtn.TabIndex = 2;
            this.pBtn.Text = "定位(&L)";
            this.pBtn.UseVisualStyleBackColor = true;
            this.pBtn.Click += new System.EventHandler(this.pBtn_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeBtn.Location = new System.Drawing.Point(223, 348);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(75, 23);
            this.closeBtn.TabIndex = 3;
            this.closeBtn.Text = "退出(&Q)";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // CrossRoadPointForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 377);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.pBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CrossRoadPointForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "交叉路口定位";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CrossRoadPointForm_FormClosed);
            this.Load += new System.EventHandler(this.CrossRoadPointForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox roadAList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox roadNameATxt;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox roadBList;
        private System.Windows.Forms.TextBox roadNameBTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button pBtn;
        private System.Windows.Forms.Button closeBtn;
    }
}