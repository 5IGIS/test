namespace JJWATQuery
{
    partial class FrmQuerySpace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQuerySpace));
            this.gbBuff = new System.Windows.Forms.GroupBox();
            this.tbBuff = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.gbSelect = new System.Windows.Forms.GroupBox();
            this.cbSelcet = new System.Windows.Forms.ComboBox();
            this.gbArea = new System.Windows.Forms.GroupBox();
            this.btnAreaClear = new System.Windows.Forms.Button();
            this.btnAreaSelect = new System.Windows.Forms.Button();
            this.cbArea = new System.Windows.Forms.ComboBox();
            this.gbLayer = new System.Windows.Forms.GroupBox();
            this.cbLayerAll = new System.Windows.Forms.CheckBox();
            this.cblLayer = new System.Windows.Forms.CheckedListBox();
            this.gbBuff.SuspendLayout();
            this.gbSelect.SuspendLayout();
            this.gbArea.SuspendLayout();
            this.gbLayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbBuff
            // 
            this.gbBuff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbBuff.Controls.Add(this.tbBuff);
            this.gbBuff.Location = new System.Drawing.Point(165, 146);
            this.gbBuff.Name = "gbBuff";
            this.gbBuff.Size = new System.Drawing.Size(190, 52);
            this.gbBuff.TabIndex = 65;
            this.gbBuff.TabStop = false;
            this.gbBuff.Text = "缓冲区(单位:米)";
            // 
            // tbBuff
            // 
            this.tbBuff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBuff.Location = new System.Drawing.Point(10, 17);
            this.tbBuff.Name = "tbBuff";
            this.tbBuff.Size = new System.Drawing.Size(169, 21);
            this.tbBuff.TabIndex = 0;
            this.tbBuff.Text = "0";
            this.tbBuff.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbBuff_KeyPress);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(289, 213);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(55, 23);
            this.btnClose.TabIndex = 64;
            this.btnClose.Text = "退出";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.Location = new System.Drawing.Point(166, 213);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(55, 23);
            this.btnQuery.TabIndex = 63;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(228, 213);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(55, 23);
            this.btnClear.TabIndex = 62;
            this.btnClear.Text = "重置";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // gbSelect
            // 
            this.gbSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSelect.Controls.Add(this.cbSelcet);
            this.gbSelect.Location = new System.Drawing.Point(166, 94);
            this.gbSelect.Name = "gbSelect";
            this.gbSelect.Size = new System.Drawing.Size(190, 46);
            this.gbSelect.TabIndex = 61;
            this.gbSelect.TabStop = false;
            this.gbSelect.Text = "空间逻辑关系:";
            // 
            // cbSelcet
            // 
            this.cbSelcet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSelcet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSelcet.FormattingEnabled = true;
            this.cbSelcet.Items.AddRange(new object[] {
            "包含",
            "相接",
            "相交"});
            this.cbSelcet.Location = new System.Drawing.Point(10, 17);
            this.cbSelcet.Name = "cbSelcet";
            this.cbSelcet.Size = new System.Drawing.Size(169, 20);
            this.cbSelcet.TabIndex = 0;
            this.cbSelcet.SelectedIndexChanged += new System.EventHandler(this.cbSelcet_SelectedIndexChanged);
            // 
            // gbArea
            // 
            this.gbArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbArea.Controls.Add(this.btnAreaClear);
            this.gbArea.Controls.Add(this.btnAreaSelect);
            this.gbArea.Controls.Add(this.cbArea);
            this.gbArea.Location = new System.Drawing.Point(166, 6);
            this.gbArea.Name = "gbArea";
            this.gbArea.Size = new System.Drawing.Size(190, 82);
            this.gbArea.TabIndex = 60;
            this.gbArea.TabStop = false;
            this.gbArea.Text = "查询边界";
            // 
            // btnAreaClear
            // 
            this.btnAreaClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAreaClear.Location = new System.Drawing.Point(72, 47);
            this.btnAreaClear.Name = "btnAreaClear";
            this.btnAreaClear.Size = new System.Drawing.Size(55, 23);
            this.btnAreaClear.TabIndex = 46;
            this.btnAreaClear.Text = "清除";
            this.btnAreaClear.UseVisualStyleBackColor = true;
            this.btnAreaClear.Click += new System.EventHandler(this.btnAreaClear_Click);
            // 
            // btnAreaSelect
            // 
            this.btnAreaSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAreaSelect.Location = new System.Drawing.Point(10, 47);
            this.btnAreaSelect.Name = "btnAreaSelect";
            this.btnAreaSelect.Size = new System.Drawing.Size(55, 23);
            this.btnAreaSelect.TabIndex = 45;
            this.btnAreaSelect.Text = "选择";
            this.btnAreaSelect.UseVisualStyleBackColor = true;
            this.btnAreaSelect.Click += new System.EventHandler(this.btnAreaSelect_Click);
            // 
            // cbArea
            // 
            this.cbArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbArea.FormattingEnabled = true;
            this.cbArea.Items.AddRange(new object[] {
            "区域",
            "数据集"});
            this.cbArea.Location = new System.Drawing.Point(10, 20);
            this.cbArea.Name = "cbArea";
            this.cbArea.Size = new System.Drawing.Size(169, 20);
            this.cbArea.TabIndex = 1;
            this.cbArea.SelectedIndexChanged += new System.EventHandler(this.cbArea_SelectedIndexChanged);
            // 
            // gbLayer
            // 
            this.gbLayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLayer.Controls.Add(this.cbLayerAll);
            this.gbLayer.Controls.Add(this.cblLayer);
            this.gbLayer.Location = new System.Drawing.Point(9, 6);
            this.gbLayer.Name = "gbLayer";
            this.gbLayer.Size = new System.Drawing.Size(147, 230);
            this.gbLayer.TabIndex = 8;
            this.gbLayer.TabStop = false;
            this.gbLayer.Text = "图层:";
            // 
            // cbLayerAll
            // 
            this.cbLayerAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbLayerAll.AutoSize = true;
            this.cbLayerAll.Location = new System.Drawing.Point(6, 207);
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
            this.cblLayer.Location = new System.Drawing.Point(6, 20);
            this.cblLayer.Name = "cblLayer";
            this.cblLayer.Size = new System.Drawing.Size(135, 180);
            this.cblLayer.TabIndex = 0;
            // 
            // FrmQuerySpace
            // 
            this.AcceptButton = this.btnQuery;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 245);
            this.Controls.Add(this.gbLayer);
            this.Controls.Add(this.gbSelect);
            this.Controls.Add(this.gbBuff);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.gbArea);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmQuerySpace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "空间查询";
            this.Shown += new System.EventHandler(this.frmQuerySpace_Shown);
            this.gbBuff.ResumeLayout(false);
            this.gbBuff.PerformLayout();
            this.gbSelect.ResumeLayout(false);
            this.gbArea.ResumeLayout(false);
            this.gbLayer.ResumeLayout(false);
            this.gbLayer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbBuff;
        private System.Windows.Forms.TextBox tbBuff;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.GroupBox gbSelect;
        private System.Windows.Forms.ComboBox cbSelcet;
        private System.Windows.Forms.GroupBox gbArea;
        private System.Windows.Forms.Button btnAreaClear;
        private System.Windows.Forms.Button btnAreaSelect;
        private System.Windows.Forms.ComboBox cbArea;
        private System.Windows.Forms.GroupBox gbLayer;
        private System.Windows.Forms.CheckBox cbLayerAll;
        private System.Windows.Forms.CheckedListBox cblLayer;


    }
}