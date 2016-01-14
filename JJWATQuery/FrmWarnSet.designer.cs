namespace JJWATQuery
{
    partial class FrmWarnSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWarnSet));
            this.gbLayer = new System.Windows.Forms.GroupBox();
            this.lbLayer = new System.Windows.Forms.ListBox();
            this.gbSelect = new System.Windows.Forms.GroupBox();
            this.lbSelect = new System.Windows.Forms.ListBox();
            this.gbWhere = new System.Windows.Forms.GroupBox();
            this.cbMonth = new System.Windows.Forms.ComboBox();
            this.lblMonth = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.upYear = new System.Windows.Forms.NumericUpDown();
            this.upNum = new System.Windows.Forms.NumericUpDown();
            this.lblYearS = new System.Windows.Forms.Label();
            this.lblNum = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnFrom = new System.Windows.Forms.Button();
            this.gbLayer.SuspendLayout();
            this.gbSelect.SuspendLayout();
            this.gbWhere.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upNum)).BeginInit();
            this.SuspendLayout();
            // 
            // gbLayer
            // 
            this.gbLayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLayer.Controls.Add(this.lbLayer);
            this.gbLayer.Location = new System.Drawing.Point(8, 3);
            this.gbLayer.Name = "gbLayer";
            this.gbLayer.Size = new System.Drawing.Size(155, 229);
            this.gbLayer.TabIndex = 9;
            this.gbLayer.TabStop = false;
            this.gbLayer.Text = "图层:";
            // 
            // lbLayer
            // 
            this.lbLayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLayer.FormattingEnabled = true;
            this.lbLayer.ItemHeight = 12;
            this.lbLayer.Location = new System.Drawing.Point(13, 17);
            this.lbLayer.Name = "lbLayer";
            this.lbLayer.Size = new System.Drawing.Size(130, 184);
            this.lbLayer.TabIndex = 0;
            this.lbLayer.SelectedIndexChanged += new System.EventHandler(this.lbLayer_SelectedIndexChanged);
            // 
            // gbSelect
            // 
            this.gbSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSelect.Controls.Add(this.lbSelect);
            this.gbSelect.Location = new System.Drawing.Point(169, 3);
            this.gbSelect.Name = "gbSelect";
            this.gbSelect.Size = new System.Drawing.Size(145, 229);
            this.gbSelect.TabIndex = 53;
            this.gbSelect.TabStop = false;
            this.gbSelect.Text = "类型:";
            // 
            // lbSelect
            // 
            this.lbSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSelect.FormattingEnabled = true;
            this.lbSelect.ItemHeight = 12;
            this.lbSelect.Location = new System.Drawing.Point(9, 17);
            this.lbSelect.Name = "lbSelect";
            this.lbSelect.Size = new System.Drawing.Size(127, 184);
            this.lbSelect.TabIndex = 0;
            this.lbSelect.SelectedIndexChanged += new System.EventHandler(this.lbSelect_SelectedIndexChanged);
            // 
            // gbWhere
            // 
            this.gbWhere.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbWhere.Controls.Add(this.cbMonth);
            this.gbWhere.Controls.Add(this.lblMonth);
            this.gbWhere.Controls.Add(this.lblYear);
            this.gbWhere.Controls.Add(this.upYear);
            this.gbWhere.Controls.Add(this.upNum);
            this.gbWhere.Controls.Add(this.lblYearS);
            this.gbWhere.Controls.Add(this.lblNum);
            this.gbWhere.Location = new System.Drawing.Point(8, 236);
            this.gbWhere.Name = "gbWhere";
            this.gbWhere.Size = new System.Drawing.Size(306, 98);
            this.gbWhere.TabIndex = 54;
            this.gbWhere.TabStop = false;
            this.gbWhere.Text = "预警条件";
            // 
            // cbMonth
            // 
            this.cbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMonth.FormattingEnabled = true;
            this.cbMonth.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cbMonth.Location = new System.Drawing.Point(157, 57);
            this.cbMonth.Name = "cbMonth";
            this.cbMonth.Size = new System.Drawing.Size(58, 20);
            this.cbMonth.TabIndex = 7;
            this.cbMonth.SelectedIndexChanged += new System.EventHandler(this.cbMonth_SelectedIndexChanged);
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Location = new System.Drawing.Point(217, 61);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(17, 12);
            this.lblMonth.TabIndex = 6;
            this.lblMonth.Text = "月";
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(138, 61);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(17, 12);
            this.lblYear.TabIndex = 4;
            this.lblYear.Text = "年";
            // 
            // upYear
            // 
            this.upYear.Location = new System.Drawing.Point(77, 57);
            this.upYear.Name = "upYear";
            this.upYear.Size = new System.Drawing.Size(58, 21);
            this.upYear.TabIndex = 3;
            this.upYear.ValueChanged += new System.EventHandler(this.upYear_ValueChanged);
            // 
            // upNum
            // 
            this.upNum.Location = new System.Drawing.Point(77, 23);
            this.upNum.Name = "upNum";
            this.upNum.Size = new System.Drawing.Size(58, 21);
            this.upNum.TabIndex = 2;
            this.upNum.ValueChanged += new System.EventHandler(this.upNum_ValueChanged);
            // 
            // lblYearS
            // 
            this.lblYearS.AutoSize = true;
            this.lblYearS.Location = new System.Drawing.Point(18, 61);
            this.lblYearS.Name = "lblYearS";
            this.lblYearS.Size = new System.Drawing.Size(53, 12);
            this.lblYearS.TabIndex = 1;
            this.lblYearS.Text = "服务年限";
            // 
            // lblNum
            // 
            this.lblNum.AutoSize = true;
            this.lblNum.Location = new System.Drawing.Point(18, 27);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(53, 12);
            this.lblNum.TabIndex = 0;
            this.lblNum.Text = "维修次数";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(244, 340);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(61, 23);
            this.btnClose.TabIndex = 55;
            this.btnClose.Text = "退出";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(178, 340);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(61, 23);
            this.btnOK.TabIndex = 56;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click_1);
            // 
            // btnFrom
            // 
            this.btnFrom.Location = new System.Drawing.Point(21, 340);
            this.btnFrom.Name = "btnFrom";
            this.btnFrom.Size = new System.Drawing.Size(71, 23);
            this.btnFrom.TabIndex = 57;
            this.btnFrom.Text = "预警分析";
            this.btnFrom.UseVisualStyleBackColor = true;
            this.btnFrom.Click += new System.EventHandler(this.btnFrom_Click);
            // 
            // FrmWarnSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 375);
            this.Controls.Add(this.btnFrom);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.gbWhere);
            this.Controls.Add(this.gbSelect);
            this.Controls.Add(this.gbLayer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmWarnSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "预警分析参数设置";
            this.Shown += new System.EventHandler(this.FrmWarnSet_Shown);
            this.gbLayer.ResumeLayout(false);
            this.gbSelect.ResumeLayout(false);
            this.gbWhere.ResumeLayout(false);
            this.gbWhere.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upNum)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbLayer;
        private System.Windows.Forms.GroupBox gbSelect;
        private System.Windows.Forms.GroupBox gbWhere;
        private System.Windows.Forms.Label lblYearS;
        private System.Windows.Forms.Label lblNum;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.NumericUpDown upYear;
        private System.Windows.Forms.NumericUpDown upNum;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.ListBox lbSelect;
        private System.Windows.Forms.ListBox lbLayer;
        private System.Windows.Forms.ComboBox cbMonth;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnFrom;
    }
}