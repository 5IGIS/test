namespace JJWATQuery
{
    partial class FrmQuerySQL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQuerySQL));
            this.gbLayers = new System.Windows.Forms.GroupBox();
            this.cbLayer = new System.Windows.Forms.ComboBox();
            this.gbFields = new System.Windows.Forms.GroupBox();
            this.lbFields = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lsbSelect = new System.Windows.Forms.ListBox();
            this.gbSelect = new System.Windows.Forms.GroupBox();
            this.btnDelt = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.tbSql = new System.Windows.Forms.TextBox();
            this.gbArea = new System.Windows.Forms.GroupBox();
            this.btnAreaClear = new System.Windows.Forms.Button();
            this.btnAreaSelect = new System.Windows.Forms.Button();
            this.lbArea = new System.Windows.Forms.ListBox();
            this.cbArea = new System.Windows.Forms.ComboBox();
            this.tbSelect = new System.Windows.Forms.TextBox();
            this.btnValue = new System.Windows.Forms.Button();
            this.btnLikes = new System.Windows.Forms.Button();
            this.btnLine = new System.Windows.Forms.Button();
            this.btnIs = new System.Windows.Forms.Button();
            this.btnBracket = new System.Windows.Forms.Button();
            this.btnNot = new System.Windows.Forms.Button();
            this.btnOr = new System.Windows.Forms.Button();
            this.btnLike = new System.Windows.Forms.Button();
            this.btnLittle = new System.Windows.Forms.Button();
            this.btnAnd = new System.Windows.Forms.Button();
            this.btnBigEqual = new System.Windows.Forms.Button();
            this.btnLittleEqual = new System.Windows.Forms.Button();
            this.btnBig = new System.Windows.Forms.Button();
            this.btnNotEqual = new System.Windows.Forms.Button();
            this.btnEqual = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gbLayers.SuspendLayout();
            this.gbFields.SuspendLayout();
            this.gbSelect.SuspendLayout();
            this.gbArea.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbLayers
            // 
            this.gbLayers.Controls.Add(this.cbLayer);
            this.gbLayers.Location = new System.Drawing.Point(9, 6);
            this.gbLayers.Name = "gbLayers";
            this.gbLayers.Size = new System.Drawing.Size(446, 47);
            this.gbLayers.TabIndex = 43;
            this.gbLayers.TabStop = false;
            this.gbLayers.Text = "图层:";
            // 
            // cbLayer
            // 
            this.cbLayer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLayer.FormattingEnabled = true;
            this.cbLayer.Location = new System.Drawing.Point(13, 17);
            this.cbLayer.Name = "cbLayer";
            this.cbLayer.Size = new System.Drawing.Size(427, 20);
            this.cbLayer.TabIndex = 1;
            this.cbLayer.SelectedIndexChanged += new System.EventHandler(this.cbLayer_SelectedIndexChanged);
            // 
            // gbFields
            // 
            this.gbFields.Controls.Add(this.lbFields);
            this.gbFields.Location = new System.Drawing.Point(9, 59);
            this.gbFields.Name = "gbFields";
            this.gbFields.Size = new System.Drawing.Size(142, 173);
            this.gbFields.TabIndex = 44;
            this.gbFields.TabStop = false;
            this.gbFields.Text = "字段";
            // 
            // lbFields
            // 
            this.lbFields.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFields.FormattingEnabled = true;
            this.lbFields.ItemHeight = 12;
            this.lbFields.Location = new System.Drawing.Point(6, 15);
            this.lbFields.Name = "lbFields";
            this.lbFields.Size = new System.Drawing.Size(130, 148);
            this.lbFields.TabIndex = 0;
            this.lbFields.Click += new System.EventHandler(this.lbFields_Click);
            this.lbFields.SelectedIndexChanged += new System.EventHandler(this.lbFields_SelectedIndexChanged);
            this.lbFields.DoubleClick += new System.EventHandler(this.lbFields_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 59;
            this.label1.Text = "转至";
            // 
            // lsbSelect
            // 
            this.lsbSelect.FormattingEnabled = true;
            this.lsbSelect.ItemHeight = 12;
            this.lsbSelect.Location = new System.Drawing.Point(6, 15);
            this.lsbSelect.Name = "lsbSelect";
            this.lsbSelect.Size = new System.Drawing.Size(145, 124);
            this.lsbSelect.TabIndex = 58;
            this.lsbSelect.DoubleClick += new System.EventHandler(this.lsbSelect_DoubleClick);
            // 
            // gbSelect
            // 
            this.gbSelect.Controls.Add(this.btnDelt);
            this.gbSelect.Controls.Add(this.btnRead);
            this.gbSelect.Controls.Add(this.btnSave);
            this.gbSelect.Controls.Add(this.btnDel);
            this.gbSelect.Controls.Add(this.tbSql);
            this.gbSelect.Location = new System.Drawing.Point(9, 238);
            this.gbSelect.Name = "gbSelect";
            this.gbSelect.Size = new System.Drawing.Size(446, 128);
            this.gbSelect.TabIndex = 57;
            this.gbSelect.TabStop = false;
            this.gbSelect.Text = "SQL子句";
            // 
            // btnDelt
            // 
            this.btnDelt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelt.Location = new System.Drawing.Point(232, 99);
            this.btnDelt.Name = "btnDelt";
            this.btnDelt.Size = new System.Drawing.Size(47, 23);
            this.btnDelt.TabIndex = 31;
            this.btnDelt.Text = "管理";
            this.btnDelt.UseVisualStyleBackColor = true;
            this.btnDelt.Click += new System.EventHandler(this.btnDelt_Click);
            // 
            // btnRead
            // 
            this.btnRead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRead.Location = new System.Drawing.Point(391, 99);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(47, 23);
            this.btnRead.TabIndex = 30;
            this.btnRead.Text = "读取";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(338, 99);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(47, 23);
            this.btnSave.TabIndex = 29;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDel.Location = new System.Drawing.Point(285, 99);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(47, 23);
            this.btnDel.TabIndex = 28;
            this.btnDel.Text = "清空";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // tbSql
            // 
            this.tbSql.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSql.Location = new System.Drawing.Point(6, 14);
            this.tbSql.Multiline = true;
            this.tbSql.Name = "tbSql";
            this.tbSql.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbSql.Size = new System.Drawing.Size(434, 81);
            this.tbSql.TabIndex = 27;
            // 
            // gbArea
            // 
            this.gbArea.Controls.Add(this.btnAreaClear);
            this.gbArea.Controls.Add(this.btnAreaSelect);
            this.gbArea.Controls.Add(this.lbArea);
            this.gbArea.Controls.Add(this.cbArea);
            this.gbArea.Location = new System.Drawing.Point(9, 372);
            this.gbArea.Name = "gbArea";
            this.gbArea.Size = new System.Drawing.Size(316, 184);
            this.gbArea.TabIndex = 56;
            this.gbArea.TabStop = false;
            this.gbArea.Text = "区域选择";
            // 
            // btnAreaClear
            // 
            this.btnAreaClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAreaClear.Location = new System.Drawing.Point(230, 149);
            this.btnAreaClear.Name = "btnAreaClear";
            this.btnAreaClear.Size = new System.Drawing.Size(80, 23);
            this.btnAreaClear.TabIndex = 46;
            this.btnAreaClear.Text = "清除";
            this.btnAreaClear.UseVisualStyleBackColor = true;
            this.btnAreaClear.Click += new System.EventHandler(this.btnAreaClear_Click);
            // 
            // btnAreaSelect
            // 
            this.btnAreaSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAreaSelect.Enabled = false;
            this.btnAreaSelect.Location = new System.Drawing.Point(144, 149);
            this.btnAreaSelect.Name = "btnAreaSelect";
            this.btnAreaSelect.Size = new System.Drawing.Size(80, 23);
            this.btnAreaSelect.TabIndex = 45;
            this.btnAreaSelect.Text = "选择";
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
            this.lbArea.Location = new System.Drawing.Point(6, 39);
            this.lbArea.Name = "lbArea";
            this.lbArea.Size = new System.Drawing.Size(303, 100);
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
            this.cbArea.Location = new System.Drawing.Point(7, 14);
            this.cbArea.Name = "cbArea";
            this.cbArea.Size = new System.Drawing.Size(303, 20);
            this.cbArea.TabIndex = 1;
            this.cbArea.SelectedIndexChanged += new System.EventHandler(this.cbArea_SelectedIndexChanged);
            // 
            // tbSelect
            // 
            this.tbSelect.Location = new System.Drawing.Point(36, 144);
            this.tbSelect.Name = "tbSelect";
            this.tbSelect.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbSelect.Size = new System.Drawing.Size(115, 21);
            this.tbSelect.TabIndex = 24;
            this.tbSelect.TextChanged += new System.EventHandler(this.tbSelect_TextChanged);
            // 
            // btnValue
            // 
            this.btnValue.Location = new System.Drawing.Point(50, 132);
            this.btnValue.Name = "btnValue";
            this.btnValue.Size = new System.Drawing.Size(80, 22);
            this.btnValue.TabIndex = 22;
            this.btnValue.Text = "获取唯一值";
            this.btnValue.UseVisualStyleBackColor = true;
            this.btnValue.Click += new System.EventHandler(this.btnValue_Click);
            // 
            // btnLikes
            // 
            this.btnLikes.Location = new System.Drawing.Point(28, 104);
            this.btnLikes.Name = "btnLikes";
            this.btnLikes.Size = new System.Drawing.Size(18, 22);
            this.btnLikes.TabIndex = 20;
            this.btnLikes.Text = "%";
            this.btnLikes.UseVisualStyleBackColor = true;
            this.btnLikes.Click += new System.EventHandler(this.btnLikes_Click);
            // 
            // btnLine
            // 
            this.btnLine.Location = new System.Drawing.Point(8, 104);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(18, 22);
            this.btnLine.TabIndex = 19;
            this.btnLine.Text = "_";
            this.btnLine.UseVisualStyleBackColor = true;
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // btnIs
            // 
            this.btnIs.Location = new System.Drawing.Point(8, 132);
            this.btnIs.Name = "btnIs";
            this.btnIs.Size = new System.Drawing.Size(38, 22);
            this.btnIs.TabIndex = 18;
            this.btnIs.Text = "Is";
            this.btnIs.UseVisualStyleBackColor = true;
            this.btnIs.Click += new System.EventHandler(this.btnIs_Click);
            // 
            // btnBracket
            // 
            this.btnBracket.Location = new System.Drawing.Point(50, 104);
            this.btnBracket.Name = "btnBracket";
            this.btnBracket.Size = new System.Drawing.Size(38, 22);
            this.btnBracket.TabIndex = 17;
            this.btnBracket.Text = "()";
            this.btnBracket.UseVisualStyleBackColor = true;
            this.btnBracket.Click += new System.EventHandler(this.btnBracket_Click);
            // 
            // btnNot
            // 
            this.btnNot.Location = new System.Drawing.Point(92, 104);
            this.btnNot.Name = "btnNot";
            this.btnNot.Size = new System.Drawing.Size(38, 22);
            this.btnNot.TabIndex = 16;
            this.btnNot.Text = "Not";
            this.btnNot.UseVisualStyleBackColor = true;
            this.btnNot.Click += new System.EventHandler(this.btnNot_Click);
            // 
            // btnOr
            // 
            this.btnOr.Location = new System.Drawing.Point(92, 76);
            this.btnOr.Name = "btnOr";
            this.btnOr.Size = new System.Drawing.Size(38, 22);
            this.btnOr.TabIndex = 15;
            this.btnOr.Text = "Or";
            this.btnOr.UseVisualStyleBackColor = true;
            this.btnOr.Click += new System.EventHandler(this.btnOr_Click);
            // 
            // btnLike
            // 
            this.btnLike.Location = new System.Drawing.Point(92, 20);
            this.btnLike.Name = "btnLike";
            this.btnLike.Size = new System.Drawing.Size(38, 22);
            this.btnLike.TabIndex = 14;
            this.btnLike.Text = "Like";
            this.btnLike.UseVisualStyleBackColor = true;
            this.btnLike.Click += new System.EventHandler(this.btnLike_Click);
            // 
            // btnLittle
            // 
            this.btnLittle.Location = new System.Drawing.Point(8, 76);
            this.btnLittle.Name = "btnLittle";
            this.btnLittle.Size = new System.Drawing.Size(38, 22);
            this.btnLittle.TabIndex = 13;
            this.btnLittle.Text = "<";
            this.btnLittle.UseVisualStyleBackColor = true;
            this.btnLittle.Click += new System.EventHandler(this.btnLittle_Click);
            // 
            // btnAnd
            // 
            this.btnAnd.Location = new System.Drawing.Point(92, 48);
            this.btnAnd.Name = "btnAnd";
            this.btnAnd.Size = new System.Drawing.Size(38, 22);
            this.btnAnd.TabIndex = 12;
            this.btnAnd.Text = "And";
            this.btnAnd.UseVisualStyleBackColor = true;
            this.btnAnd.Click += new System.EventHandler(this.btnAnd_Click);
            // 
            // btnBigEqual
            // 
            this.btnBigEqual.Location = new System.Drawing.Point(50, 48);
            this.btnBigEqual.Name = "btnBigEqual";
            this.btnBigEqual.Size = new System.Drawing.Size(38, 22);
            this.btnBigEqual.TabIndex = 8;
            this.btnBigEqual.Text = ">=";
            this.btnBigEqual.UseVisualStyleBackColor = true;
            this.btnBigEqual.Click += new System.EventHandler(this.btnBigEqual_Click);
            // 
            // btnLittleEqual
            // 
            this.btnLittleEqual.Location = new System.Drawing.Point(50, 76);
            this.btnLittleEqual.Name = "btnLittleEqual";
            this.btnLittleEqual.Size = new System.Drawing.Size(38, 22);
            this.btnLittleEqual.TabIndex = 7;
            this.btnLittleEqual.Text = "<=";
            this.btnLittleEqual.UseVisualStyleBackColor = true;
            this.btnLittleEqual.Click += new System.EventHandler(this.btnLittleEqual_Click);
            // 
            // btnBig
            // 
            this.btnBig.Location = new System.Drawing.Point(8, 48);
            this.btnBig.Name = "btnBig";
            this.btnBig.Size = new System.Drawing.Size(38, 22);
            this.btnBig.TabIndex = 6;
            this.btnBig.Text = ">";
            this.btnBig.UseVisualStyleBackColor = true;
            this.btnBig.Click += new System.EventHandler(this.btnBig_Click);
            // 
            // btnNotEqual
            // 
            this.btnNotEqual.Location = new System.Drawing.Point(50, 20);
            this.btnNotEqual.Name = "btnNotEqual";
            this.btnNotEqual.Size = new System.Drawing.Size(38, 22);
            this.btnNotEqual.TabIndex = 5;
            this.btnNotEqual.Text = "<>";
            this.btnNotEqual.UseVisualStyleBackColor = true;
            this.btnNotEqual.Click += new System.EventHandler(this.btnNotEqual_Click);
            // 
            // btnEqual
            // 
            this.btnEqual.Location = new System.Drawing.Point(8, 20);
            this.btnEqual.Name = "btnEqual";
            this.btnEqual.Size = new System.Drawing.Size(38, 22);
            this.btnEqual.TabIndex = 4;
            this.btnEqual.Text = "=";
            this.btnEqual.UseVisualStyleBackColor = true;
            this.btnEqual.Click += new System.EventHandler(this.btnEqual_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQuery.Location = new System.Drawing.Point(346, 449);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(100, 23);
            this.btnQuery.TabIndex = 48;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.Location = new System.Drawing.Point(346, 487);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 23);
            this.btnClear.TabIndex = 49;
            this.btnClear.Text = "重置";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.Location = new System.Drawing.Point(346, 521);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 23);
            this.btnClose.TabIndex = 61;
            this.btnClose.Text = "退出";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnEqual);
            this.groupBox1.Controls.Add(this.btnNotEqual);
            this.groupBox1.Controls.Add(this.btnBig);
            this.groupBox1.Controls.Add(this.btnLittleEqual);
            this.groupBox1.Controls.Add(this.btnValue);
            this.groupBox1.Controls.Add(this.btnBigEqual);
            this.groupBox1.Controls.Add(this.btnLikes);
            this.groupBox1.Controls.Add(this.btnAnd);
            this.groupBox1.Controls.Add(this.btnLine);
            this.groupBox1.Controls.Add(this.btnLittle);
            this.groupBox1.Controls.Add(this.btnIs);
            this.groupBox1.Controls.Add(this.btnLike);
            this.groupBox1.Controls.Add(this.btnBracket);
            this.groupBox1.Controls.Add(this.btnOr);
            this.groupBox1.Controls.Add(this.btnNot);
            this.groupBox1.Location = new System.Drawing.Point(154, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(138, 173);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "罗辑符";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lsbSelect);
            this.groupBox2.Controls.Add(this.tbSelect);
            this.groupBox2.Location = new System.Drawing.Point(295, 59);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(160, 173);
            this.groupBox2.TabIndex = 62;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "字段样值";
            // 
            // FrmQuerySQL
            // 
            this.AcceptButton = this.btnQuery;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 566);
            this.Controls.Add(this.gbArea);
            this.Controls.Add(this.gbSelect);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbFields);
            this.Controls.Add(this.gbLayers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmQuerySQL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQL查询";
            this.Load += new System.EventHandler(this.FrmQuerySQL_Load);
            this.gbLayers.ResumeLayout(false);
            this.gbFields.ResumeLayout(false);
            this.gbSelect.ResumeLayout(false);
            this.gbSelect.PerformLayout();
            this.gbArea.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbLayers;
        private System.Windows.Forms.ComboBox cbLayer;
        private System.Windows.Forms.GroupBox gbFields;
        private System.Windows.Forms.Button btnEqual;
        private System.Windows.Forms.Button btnLittleEqual;
        private System.Windows.Forms.Button btnBig;
        private System.Windows.Forms.Button btnNotEqual;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnBigEqual;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnLittle;
        private System.Windows.Forms.Button btnAnd;
        private System.Windows.Forms.TextBox tbSelect;
        private System.Windows.Forms.Button btnValue;
        private System.Windows.Forms.Button btnLikes;
        private System.Windows.Forms.Button btnLine;
        private System.Windows.Forms.Button btnIs;
        private System.Windows.Forms.Button btnBracket;
        private System.Windows.Forms.Button btnNot;
        private System.Windows.Forms.Button btnOr;
        private System.Windows.Forms.Button btnLike;
        private System.Windows.Forms.GroupBox gbArea;
        private System.Windows.Forms.Button btnAreaClear;
        private System.Windows.Forms.Button btnAreaSelect;
        private System.Windows.Forms.ListBox lbArea;
        private System.Windows.Forms.ComboBox cbArea;
        private System.Windows.Forms.GroupBox gbSelect;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.TextBox tbSql;
        private System.Windows.Forms.ListBox lsbSelect;
        private System.Windows.Forms.ListBox lbFields;
        private System.Windows.Forms.Button btnDelt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}