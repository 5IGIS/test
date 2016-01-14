namespace JJWATQuery
{
    partial class FrmSelInvest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSelInvest));
            this.gBinSelectInvest = new System.Windows.Forms.GroupBox();
            this.cboBuildDt = new System.Windows.Forms.ComboBox();
            this.dTPDateTo = new System.Windows.Forms.DateTimePicker();
            this.lbl_To = new System.Windows.Forms.Label();
            this.dTPDateFrom = new System.Windows.Forms.DateTimePicker();
            this.lbl_InputDate = new System.Windows.Forms.Label();
            this.cboInvestStyle = new System.Windows.Forms.ComboBox();
            this.lbl_tzfs = new System.Windows.Forms.Label();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.gBinSelectInvest.SuspendLayout();
            this.SuspendLayout();
            // 
            // gBinSelectInvest
            // 
            this.gBinSelectInvest.Controls.Add(this.cboBuildDt);
            this.gBinSelectInvest.Controls.Add(this.dTPDateTo);
            this.gBinSelectInvest.Controls.Add(this.lbl_To);
            this.gBinSelectInvest.Controls.Add(this.dTPDateFrom);
            this.gBinSelectInvest.Controls.Add(this.lbl_InputDate);
            this.gBinSelectInvest.Controls.Add(this.cboInvestStyle);
            this.gBinSelectInvest.Controls.Add(this.lbl_tzfs);
            this.gBinSelectInvest.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gBinSelectInvest.Location = new System.Drawing.Point(5, 6);
            this.gBinSelectInvest.Name = "gBinSelectInvest";
            this.gBinSelectInvest.Size = new System.Drawing.Size(480, 111);
            this.gBinSelectInvest.TabIndex = 0;
            this.gBinSelectInvest.TabStop = false;
            this.gBinSelectInvest.Text = "条件设置";
            // 
            // cboBuildDt
            // 
            this.cboBuildDt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBuildDt.FormattingEnabled = true;
            this.cboBuildDt.Location = new System.Drawing.Point(88, 64);
            this.cboBuildDt.Name = "cboBuildDt";
            this.cboBuildDt.Size = new System.Drawing.Size(103, 20);
            this.cboBuildDt.TabIndex = 6;
            this.cboBuildDt.SelectedIndexChanged += new System.EventHandler(this.cboBuildDt_SelectedIndexChanged);
            this.cboBuildDt.Click += new System.EventHandler(this.cboBuildDt_Click);
            // 
            // dTPDateTo
            // 
            this.dTPDateTo.Location = new System.Drawing.Point(342, 63);
            this.dTPDateTo.Name = "dTPDateTo";
            this.dTPDateTo.Size = new System.Drawing.Size(121, 21);
            this.dTPDateTo.TabIndex = 5;
            this.dTPDateTo.ValueChanged += new System.EventHandler(this.dTPDateTo_ValueChanged);
            // 
            // lbl_To
            // 
            this.lbl_To.AutoSize = true;
            this.lbl_To.Location = new System.Drawing.Point(319, 67);
            this.lbl_To.Name = "lbl_To";
            this.lbl_To.Size = new System.Drawing.Size(18, 12);
            this.lbl_To.TabIndex = 4;
            this.lbl_To.Text = "到";
            // 
            // dTPDateFrom
            // 
            this.dTPDateFrom.Location = new System.Drawing.Point(197, 63);
            this.dTPDateFrom.Name = "dTPDateFrom";
            this.dTPDateFrom.Size = new System.Drawing.Size(118, 21);
            this.dTPDateFrom.TabIndex = 3;
            this.dTPDateFrom.ValueChanged += new System.EventHandler(this.dTPDateFrom_ValueChanged);
            // 
            // lbl_InputDate
            // 
            this.lbl_InputDate.AutoSize = true;
            this.lbl_InputDate.Location = new System.Drawing.Point(18, 65);
            this.lbl_InputDate.Name = "lbl_InputDate";
            this.lbl_InputDate.Size = new System.Drawing.Size(64, 12);
            this.lbl_InputDate.TabIndex = 2;
            this.lbl_InputDate.Text = "审核日期:";
            // 
            // cboInvestStyle
            // 
            this.cboInvestStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInvestStyle.FormattingEnabled = true;
            this.cboInvestStyle.Location = new System.Drawing.Point(88, 24);
            this.cboInvestStyle.Name = "cboInvestStyle";
            this.cboInvestStyle.Size = new System.Drawing.Size(105, 20);
            this.cboInvestStyle.TabIndex = 1;
            // 
            // lbl_tzfs
            // 
            this.lbl_tzfs.AutoSize = true;
            this.lbl_tzfs.Location = new System.Drawing.Point(19, 28);
            this.lbl_tzfs.Name = "lbl_tzfs";
            this.lbl_tzfs.Size = new System.Drawing.Size(64, 12);
            this.lbl_tzfs.TabIndex = 0;
            this.lbl_tzfs.Text = "投资方式:";
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(283, 123);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(90, 24);
            this.btn_OK.TabIndex = 1;
            this.btn_OK.Text = "确定(&O)";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(394, 123);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(91, 24);
            this.btn_Cancel.TabIndex = 2;
            this.btn_Cancel.Text = "取消(&C)";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // FrmSelInvest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 151);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.gBinSelectInvest);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSelInvest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "固定资产统计报表";
            this.Load += new System.EventHandler(this.FrmSelInvest_Load);
            this.Shown += new System.EventHandler(this.FrmSelInvest_Shown);
            this.gBinSelectInvest.ResumeLayout(false);
            this.gBinSelectInvest.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gBinSelectInvest;
        public System.Windows.Forms.DateTimePicker dTPDateTo;
        private System.Windows.Forms.Label lbl_To;
        public System.Windows.Forms.DateTimePicker dTPDateFrom;
        private System.Windows.Forms.Label lbl_InputDate;
        public System.Windows.Forms.ComboBox cboInvestStyle;
        private System.Windows.Forms.Label lbl_tzfs;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.ComboBox cboBuildDt;
    }
}