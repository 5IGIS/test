namespace JJWATQuery
{
    partial class FrmQueryCaliber
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQueryCaliber));
            this.btnAreaClear = new System.Windows.Forms.Button();
            this.gbSelect = new System.Windows.Forms.GroupBox();
            this.cbSelectAll = new System.Windows.Forms.CheckBox();
            this.cblSelect = new System.Windows.Forms.CheckedListBox();
            this.lbArea = new System.Windows.Forms.ListBox();
            this.btnAreaSelect = new System.Windows.Forms.Button();
            this.cbLayerAll = new System.Windows.Forms.CheckBox();
            this.gbLayer = new System.Windows.Forms.GroupBox();
            this.cblLayer = new System.Windows.Forms.CheckedListBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.gbArea = new System.Windows.Forms.GroupBox();
            this.cbArea = new System.Windows.Forms.ComboBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.gbSelect.SuspendLayout();
            this.gbLayer.SuspendLayout();
            this.gbArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAreaClear
            // 
            this.btnAreaClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAreaClear.Location = new System.Drawing.Point(183, 45);
            this.btnAreaClear.Name = "btnAreaClear";
            this.btnAreaClear.Size = new System.Drawing.Size(61, 23);
            this.btnAreaClear.TabIndex = 43;
            this.btnAreaClear.Text = "清除(&C)";
            this.btnAreaClear.UseVisualStyleBackColor = true;
            this.btnAreaClear.Click += new System.EventHandler(this.btnAreaClear_Click);
            // 
            // gbSelect
            // 
            this.gbSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSelect.Controls.Add(this.cbSelectAll);
            this.gbSelect.Controls.Add(this.cblSelect);
            this.gbSelect.Location = new System.Drawing.Point(176, 6);
            this.gbSelect.Name = "gbSelect";
            this.gbSelect.Size = new System.Drawing.Size(160, 234);
            this.gbSelect.TabIndex = 52;
            this.gbSelect.TabStop = false;
            this.gbSelect.Text = "管径:";
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.Location = new System.Drawing.Point(11, 208);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(48, 16);
            this.cbSelectAll.TabIndex = 3;
            this.cbSelectAll.Text = "全选";
            this.cbSelectAll.UseVisualStyleBackColor = true;
            this.cbSelectAll.CheckedChanged += new System.EventHandler(this.cbSelectAll_CheckedChanged);
            // 
            // cblSelect
            // 
            this.cblSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cblSelect.CheckOnClick = true;
            this.cblSelect.FormattingEnabled = true;
            this.cblSelect.Location = new System.Drawing.Point(11, 20);
            this.cblSelect.Name = "cblSelect";
            this.cblSelect.Size = new System.Drawing.Size(135, 180);
            this.cblSelect.TabIndex = 0;
            // 
            // lbArea
            // 
            this.lbArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbArea.FormattingEnabled = true;
            this.lbArea.ItemHeight = 12;
            this.lbArea.Location = new System.Drawing.Point(12, 45);
            this.lbArea.Name = "lbArea";
            this.lbArea.Size = new System.Drawing.Size(165, 88);
            this.lbArea.TabIndex = 44;
            // 
            // btnAreaSelect
            // 
            this.btnAreaSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAreaSelect.Enabled = false;
            this.btnAreaSelect.Location = new System.Drawing.Point(183, 19);
            this.btnAreaSelect.Name = "btnAreaSelect";
            this.btnAreaSelect.Size = new System.Drawing.Size(61, 23);
            this.btnAreaSelect.TabIndex = 42;
            this.btnAreaSelect.Text = "选择(&S)";
            this.btnAreaSelect.UseVisualStyleBackColor = true;
            this.btnAreaSelect.Click += new System.EventHandler(this.btnAreaSelect_Click);
            // 
            // cbLayerAll
            // 
            this.cbLayerAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbLayerAll.AutoSize = true;
            this.cbLayerAll.Location = new System.Drawing.Point(12, 208);
            this.cbLayerAll.Name = "cbLayerAll";
            this.cbLayerAll.Size = new System.Drawing.Size(48, 16);
            this.cbLayerAll.TabIndex = 4;
            this.cbLayerAll.Text = "全选";
            this.cbLayerAll.UseVisualStyleBackColor = true;
            this.cbLayerAll.CheckedChanged += new System.EventHandler(this.cbLayerAll_CheckedChanged);
            // 
            // gbLayer
            // 
            this.gbLayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLayer.Controls.Add(this.cbLayerAll);
            this.gbLayer.Controls.Add(this.cblLayer);
            this.gbLayer.Location = new System.Drawing.Point(12, 6);
            this.gbLayer.Name = "gbLayer";
            this.gbLayer.Size = new System.Drawing.Size(158, 234);
            this.gbLayer.TabIndex = 8;
            this.gbLayer.TabStop = false;
            this.gbLayer.Text = "图层:";
            // 
            // cblLayer
            // 
            this.cblLayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cblLayer.CheckOnClick = true;
            this.cblLayer.FormattingEnabled = true;
            this.cblLayer.Location = new System.Drawing.Point(12, 20);
            this.cblLayer.Name = "cblLayer";
            this.cblLayer.Size = new System.Drawing.Size(134, 180);
            this.cblLayer.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(275, 378);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(61, 23);
            this.btnClose.TabIndex = 46;
            this.btnClose.Text = "退出(&Q)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.Location = new System.Drawing.Point(275, 320);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(61, 23);
            this.btnQuery.TabIndex = 45;
            this.btnQuery.Text = "查询(&I)";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click_1);
            // 
            // gbArea
            // 
            this.gbArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbArea.Controls.Add(this.btnAreaClear);
            this.gbArea.Controls.Add(this.lbArea);
            this.gbArea.Controls.Add(this.btnAreaSelect);
            this.gbArea.Controls.Add(this.cbArea);
            this.gbArea.Location = new System.Drawing.Point(12, 252);
            this.gbArea.Name = "gbArea";
            this.gbArea.Size = new System.Drawing.Size(257, 150);
            this.gbArea.TabIndex = 43;
            this.gbArea.TabStop = false;
            this.gbArea.Text = "区域选择";
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
            this.cbArea.Location = new System.Drawing.Point(12, 20);
            this.cbArea.Name = "cbArea";
            this.cbArea.Size = new System.Drawing.Size(165, 20);
            this.cbArea.TabIndex = 1;
            this.cbArea.SelectedIndexChanged += new System.EventHandler(this.cbArea_SelectedIndexChanged);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(275, 349);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(61, 23);
            this.btnClear.TabIndex = 44;
            this.btnClear.Text = "重置(&R)";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click_1);
            // 
            // FrmQueryCaliber
            // 
            this.AcceptButton = this.btnQuery;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 413);
            this.Controls.Add(this.gbLayer);
            this.Controls.Add(this.gbSelect);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.gbArea);
            this.Controls.Add(this.btnClear);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmQueryCaliber";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "管径查询";
            this.Shown += new System.EventHandler(this.QueryBySizeForm_Shown);
            this.gbSelect.ResumeLayout(false);
            this.gbSelect.PerformLayout();
            this.gbLayer.ResumeLayout(false);
            this.gbLayer.PerformLayout();
            this.gbArea.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAreaClear;
        private System.Windows.Forms.GroupBox gbSelect;
        private System.Windows.Forms.CheckBox cbSelectAll;
        private System.Windows.Forms.CheckedListBox cblSelect;
        private System.Windows.Forms.ListBox lbArea;
        private System.Windows.Forms.Button btnAreaSelect;
        private System.Windows.Forms.CheckBox cbLayerAll;
        private System.Windows.Forms.GroupBox gbLayer;
        private System.Windows.Forms.CheckedListBox cblLayer;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.GroupBox gbArea;
        private System.Windows.Forms.ComboBox cbArea;
        private System.Windows.Forms.Button btnClear;

    }
}