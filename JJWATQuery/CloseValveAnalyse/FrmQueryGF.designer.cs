namespace JJWATQuery
{
    partial class FrmQueryGF
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQueryGF));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkAllNot = new System.Windows.Forms.CheckBox();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.clbNumber = new System.Windows.Forms.CheckedListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnRegional = new System.Windows.Forms.Button();
            this.chkRegional = new System.Windows.Forms.CheckBox();
            this.chkAllCity = new System.Windows.Forms.CheckBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chkAllNot);
            this.groupBox1.Controls.Add(this.chkAll);
            this.groupBox1.Controls.Add(this.clbNumber);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(219, 298);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "阀门数量：";
            // 
            // chkAllNot
            // 
            this.chkAllNot.AutoSize = true;
            this.chkAllNot.Location = new System.Drawing.Point(60, 271);
            this.chkAllNot.Name = "chkAllNot";
            this.chkAllNot.Size = new System.Drawing.Size(60, 16);
            this.chkAllNot.TabIndex = 2;
            this.chkAllNot.Text = "全不选";
            this.chkAllNot.UseVisualStyleBackColor = true;
            this.chkAllNot.CheckedChanged += new System.EventHandler(this.chkAllNot_CheckedChanged);
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(7, 271);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(48, 16);
            this.chkAll.TabIndex = 1;
            this.chkAll.Text = "全选";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // clbNumber
            // 
            this.clbNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbNumber.CheckOnClick = true;
            this.clbNumber.FormattingEnabled = true;
            this.clbNumber.Location = new System.Drawing.Point(6, 20);
            this.clbNumber.Name = "clbNumber";
            this.clbNumber.Size = new System.Drawing.Size(207, 244);
            this.clbNumber.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnRegional);
            this.groupBox2.Controls.Add(this.chkRegional);
            this.groupBox2.Controls.Add(this.chkAllCity);
            this.groupBox2.Location = new System.Drawing.Point(12, 316);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(219, 51);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "空间区域：";
            // 
            // btnRegional
            // 
            this.btnRegional.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegional.Enabled = false;
            this.btnRegional.Location = new System.Drawing.Point(135, 17);
            this.btnRegional.Name = "btnRegional";
            this.btnRegional.Size = new System.Drawing.Size(81, 23);
            this.btnRegional.TabIndex = 2;
            this.btnRegional.Text = "选择区域(&S)";
            this.btnRegional.UseVisualStyleBackColor = true;
            this.btnRegional.Click += new System.EventHandler(this.btnRegional_Click);
            // 
            // chkRegional
            // 
            this.chkRegional.AutoSize = true;
            this.chkRegional.Location = new System.Drawing.Point(60, 20);
            this.chkRegional.Name = "chkRegional";
            this.chkRegional.Size = new System.Drawing.Size(72, 16);
            this.chkRegional.TabIndex = 1;
            this.chkRegional.Text = "选择区域";
            this.chkRegional.UseVisualStyleBackColor = true;
            this.chkRegional.CheckedChanged += new System.EventHandler(this.chkRegional_CheckedChanged);
            // 
            // chkAllCity
            // 
            this.chkAllCity.AutoSize = true;
            this.chkAllCity.Checked = true;
            this.chkAllCity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllCity.Location = new System.Drawing.Point(6, 20);
            this.chkAllCity.Name = "chkAllCity";
            this.chkAllCity.Size = new System.Drawing.Size(48, 16);
            this.chkAllCity.TabIndex = 0;
            this.chkAllCity.Text = "全市";
            this.chkAllCity.UseVisualStyleBackColor = true;
            this.chkAllCity.CheckedChanged += new System.EventHandler(this.chkAllCity_CheckedChanged);
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQuery.Location = new System.Drawing.Point(12, 375);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 2;
            this.btnQuery.Text = "分析(&I)";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(153, 375);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "退出(&Q)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmQueryGF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 404);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmQueryGF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "关阀数量分析";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmQueryGF_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkAllNot;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.CheckedListBox clbNumber;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnRegional;
        private System.Windows.Forms.CheckBox chkRegional;
        private System.Windows.Forms.CheckBox chkAllCity;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnClose;
    }
}