namespace JJWATQuery
{
    partial class FrmQueryTime
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQueryTime));
            this.gbNumber = new System.Windows.Forms.GroupBox();
            this.cbTime = new System.Windows.Forms.ComboBox();
            this.lblETime = new System.Windows.Forms.Label();
            this.DTimeE = new System.Windows.Forms.DateTimePicker();
            this.DTimeB = new System.Windows.Forms.DateTimePicker();
            this.gbLayer = new System.Windows.Forms.GroupBox();
            this.cbLayerAll = new System.Windows.Forms.CheckBox();
            this.cblLayer = new System.Windows.Forms.CheckedListBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.gbArea = new System.Windows.Forms.GroupBox();
            this.btnAreaClear = new System.Windows.Forms.Button();
            this.btnAreaSelect = new System.Windows.Forms.Button();
            this.lbArea = new System.Windows.Forms.ListBox();
            this.cbArea = new System.Windows.Forms.ComboBox();
            this.gbNumber.SuspendLayout();
            this.gbLayer.SuspendLayout();
            this.gbArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbNumber
            // 
            this.gbNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbNumber.Controls.Add(this.cbTime);
            this.gbNumber.Controls.Add(this.lblETime);
            this.gbNumber.Controls.Add(this.DTimeE);
            this.gbNumber.Controls.Add(this.DTimeB);
            this.gbNumber.Location = new System.Drawing.Point(12, 12);
            this.gbNumber.Name = "gbNumber";
            this.gbNumber.Size = new System.Drawing.Size(357, 51);
            this.gbNumber.TabIndex = 6;
            this.gbNumber.TabStop = false;
            this.gbNumber.Text = "竣工时间:";
            // 
            // cbTime
            // 
            this.cbTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTime.FormattingEnabled = true;
            this.cbTime.Items.AddRange(new object[] {
            "大于",
            "大于等于",
            "等于",
            "小于等于",
            "小于",
            "介于"});
            this.cbTime.Location = new System.Drawing.Point(11, 20);
            this.cbTime.Name = "cbTime";
            this.cbTime.Size = new System.Drawing.Size(76, 20);
            this.cbTime.TabIndex = 4;
            this.cbTime.SelectedIndexChanged += new System.EventHandler(this.cbTime_SelectedIndexChanged);
            // 
            // lblETime
            // 
            this.lblETime.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblETime.AutoSize = true;
            this.lblETime.Location = new System.Drawing.Point(211, 24);
            this.lblETime.Name = "lblETime";
            this.lblETime.Size = new System.Drawing.Size(17, 12);
            this.lblETime.TabIndex = 3;
            this.lblETime.Text = "到";
            // 
            // DTimeE
            // 
            this.DTimeE.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.DTimeE.Enabled = false;
            this.DTimeE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTimeE.Location = new System.Drawing.Point(234, 20);
            this.DTimeE.Name = "DTimeE";
            this.DTimeE.Size = new System.Drawing.Size(110, 21);
            this.DTimeE.TabIndex = 1;
            this.DTimeE.Value = new System.DateTime(2012, 2, 23, 0, 0, 0, 0);
            // 
            // DTimeB
            // 
            this.DTimeB.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.DTimeB.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTimeB.Location = new System.Drawing.Point(95, 20);
            this.DTimeB.Name = "DTimeB";
            this.DTimeB.Size = new System.Drawing.Size(110, 21);
            this.DTimeB.TabIndex = 0;
            this.DTimeB.Value = new System.DateTime(2012, 2, 23, 0, 0, 0, 0);
            // 
            // gbLayer
            // 
            this.gbLayer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbLayer.Controls.Add(this.cbLayerAll);
            this.gbLayer.Controls.Add(this.cblLayer);
            this.gbLayer.Location = new System.Drawing.Point(12, 69);
            this.gbLayer.Name = "gbLayer";
            this.gbLayer.Size = new System.Drawing.Size(167, 231);
            this.gbLayer.TabIndex = 9;
            this.gbLayer.TabStop = false;
            this.gbLayer.Text = "图层:";
            // 
            // cbLayerAll
            // 
            this.cbLayerAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbLayerAll.AutoSize = true;
            this.cbLayerAll.Location = new System.Drawing.Point(11, 209);
            this.cbLayerAll.Name = "cbLayerAll";
            this.cbLayerAll.Size = new System.Drawing.Size(48, 16);
            this.cbLayerAll.TabIndex = 4;
            this.cbLayerAll.Text = "全选";
            this.cbLayerAll.UseVisualStyleBackColor = true;
            this.cbLayerAll.CheckedChanged += new System.EventHandler(this.cbLayerAll_CheckedChanged);
            // 
            // cblLayer
            // 
            this.cblLayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cblLayer.CheckOnClick = true;
            this.cblLayer.FormattingEnabled = true;
            this.cblLayer.Location = new System.Drawing.Point(11, 20);
            this.cblLayer.Name = "cblLayer";
            this.cblLayer.Size = new System.Drawing.Size(144, 180);
            this.cblLayer.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.Location = new System.Drawing.Point(314, 277);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(55, 23);
            this.btnClose.TabIndex = 61;
            this.btnClose.Text = "退出(&Q)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQuery.Location = new System.Drawing.Point(194, 277);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(55, 23);
            this.btnQuery.TabIndex = 60;
            this.btnQuery.Text = "查询(&I)";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.Location = new System.Drawing.Point(254, 277);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(55, 23);
            this.btnClear.TabIndex = 59;
            this.btnClear.Text = "重置(&R)";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // gbArea
            // 
            this.gbArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbArea.Controls.Add(this.btnAreaClear);
            this.gbArea.Controls.Add(this.btnAreaSelect);
            this.gbArea.Controls.Add(this.lbArea);
            this.gbArea.Controls.Add(this.cbArea);
            this.gbArea.Location = new System.Drawing.Point(185, 69);
            this.gbArea.Name = "gbArea";
            this.gbArea.Size = new System.Drawing.Size(184, 202);
            this.gbArea.TabIndex = 58;
            this.gbArea.TabStop = false;
            this.gbArea.Text = "区域选择";
            // 
            // btnAreaClear
            // 
            this.btnAreaClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAreaClear.Location = new System.Drawing.Point(115, 169);
            this.btnAreaClear.Name = "btnAreaClear";
            this.btnAreaClear.Size = new System.Drawing.Size(55, 23);
            this.btnAreaClear.TabIndex = 46;
            this.btnAreaClear.Text = "清除(&C)";
            this.btnAreaClear.UseVisualStyleBackColor = true;
            this.btnAreaClear.Click += new System.EventHandler(this.btnAreaClear_Click);
            // 
            // btnAreaSelect
            // 
            this.btnAreaSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAreaSelect.Enabled = false;
            this.btnAreaSelect.Location = new System.Drawing.Point(9, 169);
            this.btnAreaSelect.Name = "btnAreaSelect";
            this.btnAreaSelect.Size = new System.Drawing.Size(55, 23);
            this.btnAreaSelect.TabIndex = 45;
            this.btnAreaSelect.Text = "选择(&S)";
            this.btnAreaSelect.UseVisualStyleBackColor = true;
            this.btnAreaSelect.Click += new System.EventHandler(this.btnAreaSelect_Click);
            // 
            // lbArea
            // 
            this.lbArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbArea.FormattingEnabled = true;
            this.lbArea.ItemHeight = 12;
            this.lbArea.Location = new System.Drawing.Point(9, 45);
            this.lbArea.Name = "lbArea";
            this.lbArea.Size = new System.Drawing.Size(162, 112);
            this.lbArea.TabIndex = 44;
            // 
            // cbArea
            // 
            this.cbArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbArea.FormattingEnabled = true;
            this.cbArea.Items.AddRange(new object[] {
            "全图",
            "区域",
            "图幅"});
            this.cbArea.Location = new System.Drawing.Point(9, 20);
            this.cbArea.Name = "cbArea";
            this.cbArea.Size = new System.Drawing.Size(162, 20);
            this.cbArea.TabIndex = 1;
            this.cbArea.SelectedIndexChanged += new System.EventHandler(this.cbArea_SelectedIndexChanged);
            // 
            // FrmQueryTime
            // 
            this.AcceptButton = this.btnQuery;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 309);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.gbArea);
            this.Controls.Add(this.gbLayer);
            this.Controls.Add(this.gbNumber);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmQueryTime";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "竣工日期查询";
            this.Load += new System.EventHandler(this.FrmQueryTime_Load);
            this.gbNumber.ResumeLayout(false);
            this.gbNumber.PerformLayout();
            this.gbLayer.ResumeLayout(false);
            this.gbLayer.PerformLayout();
            this.gbArea.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbNumber;
        private System.Windows.Forms.Label lblETime;
        private System.Windows.Forms.DateTimePicker DTimeE;
        private System.Windows.Forms.DateTimePicker DTimeB;
        private System.Windows.Forms.ComboBox cbTime;
        private System.Windows.Forms.GroupBox gbLayer;
        private System.Windows.Forms.CheckBox cbLayerAll;
        private System.Windows.Forms.CheckedListBox cblLayer;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.GroupBox gbArea;
        private System.Windows.Forms.Button btnAreaClear;
        private System.Windows.Forms.Button btnAreaSelect;
        private System.Windows.Forms.ListBox lbArea;
        private System.Windows.Forms.ComboBox cbArea;
    }
}