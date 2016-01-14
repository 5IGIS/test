namespace JJWATQuery
{
    partial class CrossRoadPositionForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.isECBx = new System.Windows.Forms.CheckBox();
            this.domainUpDownBL = new System.Windows.Forms.DomainUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.roadNameATxt = new System.Windows.Forms.TextBox();
            this.roadAList = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.roadNameBTxt = new System.Windows.Forms.TextBox();
            this.roadBList = new System.Windows.Forms.ListBox();
            this.closeBtn = new System.Windows.Forms.Button();
            this.pBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.isECBx);
            this.groupBox1.Controls.Add(this.domainUpDownBL);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(287, 47);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "比例尺设定";
            // 
            // isECBx
            // 
            this.isECBx.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.isECBx.AutoSize = true;
            this.isECBx.Checked = true;
            this.isECBx.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isECBx.Location = new System.Drawing.Point(192, 18);
            this.isECBx.Name = "isECBx";
            this.isECBx.Size = new System.Drawing.Size(48, 16);
            this.isECBx.TabIndex = 2;
            this.isECBx.Text = "默认";
            this.isECBx.UseVisualStyleBackColor = true;
            this.isECBx.CheckedChanged += new System.EventHandler(this.isECBx_CheckedChanged);
            // 
            // domainUpDownBL
            // 
            this.domainUpDownBL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.domainUpDownBL.Enabled = false;
            this.domainUpDownBL.Location = new System.Drawing.Point(62, 15);
            this.domainUpDownBL.Name = "domainUpDownBL";
            this.domainUpDownBL.Size = new System.Drawing.Size(116, 21);
            this.domainUpDownBL.TabIndex = 1;
            this.domainUpDownBL.Text = "3000";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "1：";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.roadNameATxt);
            this.groupBox2.Controls.Add(this.roadAList);
            this.groupBox2.Location = new System.Drawing.Point(13, 56);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(286, 170);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "道路选择";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "道路名";
            // 
            // roadNameATxt
            // 
            this.roadNameATxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.roadNameATxt.Location = new System.Drawing.Point(55, 19);
            this.roadNameATxt.Name = "roadNameATxt";
            this.roadNameATxt.Size = new System.Drawing.Size(222, 21);
            this.roadNameATxt.TabIndex = 0;
            this.roadNameATxt.TextChanged += new System.EventHandler(this.roadNameATxt_TextChanged);
            // 
            // roadAList
            // 
            this.roadAList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.roadAList.FormattingEnabled = true;
            this.roadAList.ItemHeight = 12;
            this.roadAList.Location = new System.Drawing.Point(55, 45);
            this.roadAList.Name = "roadAList";
            this.roadAList.Size = new System.Drawing.Size(222, 112);
            this.roadAList.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.roadNameBTxt);
            this.groupBox3.Controls.Add(this.roadBList);
            this.groupBox3.Location = new System.Drawing.Point(12, 232);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(287, 143);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "道路选择";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "道路名";
            // 
            // roadNameBTxt
            // 
            this.roadNameBTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.roadNameBTxt.Location = new System.Drawing.Point(56, 20);
            this.roadNameBTxt.Name = "roadNameBTxt";
            this.roadNameBTxt.Size = new System.Drawing.Size(222, 21);
            this.roadNameBTxt.TabIndex = 3;
            this.roadNameBTxt.TextChanged += new System.EventHandler(this.roadNameBTxt_TextChanged);
            // 
            // roadBList
            // 
            this.roadBList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.roadBList.FormattingEnabled = true;
            this.roadBList.ItemHeight = 12;
            this.roadBList.Location = new System.Drawing.Point(56, 46);
            this.roadBList.Name = "roadBList";
            this.roadBList.Size = new System.Drawing.Size(222, 88);
            this.roadBList.TabIndex = 2;
            // 
            // closeBtn
            // 
            this.closeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeBtn.Location = new System.Drawing.Point(224, 381);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(75, 23);
            this.closeBtn.TabIndex = 3;
            this.closeBtn.Text = "退出";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // pBtn
            // 
            this.pBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pBtn.Location = new System.Drawing.Point(140, 381);
            this.pBtn.Name = "pBtn";
            this.pBtn.Size = new System.Drawing.Size(75, 23);
            this.pBtn.TabIndex = 4;
            this.pBtn.Text = "定位";
            this.pBtn.UseVisualStyleBackColor = true;
            this.pBtn.Click += new System.EventHandler(this.pBtn_Click);
            // 
            // CrossRoadPositionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 413);
            this.Controls.Add(this.pBtn);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "CrossRoadPositionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "交叉路口定位";
            this.Load += new System.EventHandler(this.CrossRoadPositionForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DomainUpDown domainUpDownBL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox isECBx;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox roadNameATxt;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox roadAList;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button pBtn;
        private System.Windows.Forms.TextBox roadNameBTxt;
        private System.Windows.Forms.ListBox roadBList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}