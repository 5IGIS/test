namespace JJWATQuery
{
    partial class FrmQuerySheet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQuerySheet));
            this.gbLayers = new System.Windows.Forms.GroupBox();
            this.cbData = new System.Windows.Forms.CheckBox();
            this.cbLayer = new System.Windows.Forms.ComboBox();
            this.gbNumber = new System.Windows.Forms.GroupBox();
            this.cmb_CHECKEDATE = new System.Windows.Forms.ComboBox();
            this.check_CHECKEDATE = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.time_CHECKEDATE2 = new System.Windows.Forms.DateTimePicker();
            this.time_CHECKEDATE1 = new System.Windows.Forms.DateTimePicker();
            this.cbTime = new System.Windows.Forms.ComboBox();
            this.chbTime = new System.Windows.Forms.CheckBox();
            this.lblETime = new System.Windows.Forms.Label();
            this.DTimeE = new System.Windows.Forms.DateTimePicker();
            this.DTimeB = new System.Windows.Forms.DateTimePicker();
            this.btn_ExportExcel = new System.Windows.Forms.Button();
            this.btnStats = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.gbStats = new System.Windows.Forms.GroupBox();
            this.clbStats = new System.Windows.Forms.CheckedListBox();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.gbList = new System.Windows.Forms.GroupBox();
            this.cbAll = new System.Windows.Forms.CheckBox();
            this.clbList = new System.Windows.Forms.CheckedListBox();
            this.dGridView = new System.Windows.Forms.DataGridView();
            this.gbLayers.SuspendLayout();
            this.gbNumber.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.gbStats.SuspendLayout();
            this.gbList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // gbLayers
            // 
            this.gbLayers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLayers.Controls.Add(this.cbData);
            this.gbLayers.Controls.Add(this.cbLayer);
            this.gbLayers.Location = new System.Drawing.Point(9, 98);
            this.gbLayers.Name = "gbLayers";
            this.gbLayers.Size = new System.Drawing.Size(466, 50);
            this.gbLayers.TabIndex = 8;
            this.gbLayers.TabStop = false;
            this.gbLayers.Text = "图层类别";
            // 
            // cbData
            // 
            this.cbData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbData.AutoSize = true;
            this.cbData.Location = new System.Drawing.Point(396, 23);
            this.cbData.Name = "cbData";
            this.cbData.Size = new System.Drawing.Size(60, 16);
            this.cbData.TabIndex = 10;
            this.cbData.Text = "数据集";
            this.cbData.UseVisualStyleBackColor = true;
            // 
            // cbLayer
            // 
            this.cbLayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLayer.FormattingEnabled = true;
            this.cbLayer.Location = new System.Drawing.Point(8, 20);
            this.cbLayer.Name = "cbLayer";
            this.cbLayer.Size = new System.Drawing.Size(378, 20);
            this.cbLayer.TabIndex = 9;
            this.cbLayer.SelectedIndexChanged += new System.EventHandler(this.cbLayer_SelectedIndexChanged);
            // 
            // gbNumber
            // 
            this.gbNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbNumber.Controls.Add(this.cmb_CHECKEDATE);
            this.gbNumber.Controls.Add(this.check_CHECKEDATE);
            this.gbNumber.Controls.Add(this.label1);
            this.gbNumber.Controls.Add(this.time_CHECKEDATE2);
            this.gbNumber.Controls.Add(this.time_CHECKEDATE1);
            this.gbNumber.Controls.Add(this.cbTime);
            this.gbNumber.Controls.Add(this.chbTime);
            this.gbNumber.Controls.Add(this.lblETime);
            this.gbNumber.Controls.Add(this.DTimeE);
            this.gbNumber.Controls.Add(this.DTimeB);
            this.gbNumber.Location = new System.Drawing.Point(9, 6);
            this.gbNumber.Name = "gbNumber";
            this.gbNumber.Size = new System.Drawing.Size(466, 87);
            this.gbNumber.TabIndex = 9;
            this.gbNumber.TabStop = false;
            this.gbNumber.Text = "统计条件";
            // 
            // cmb_CHECKEDATE
            // 
            this.cmb_CHECKEDATE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_CHECKEDATE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_CHECKEDATE.FormattingEnabled = true;
            this.cmb_CHECKEDATE.Items.AddRange(new object[] {
            "大于",
            "大于等于",
            "等于",
            "小于等于",
            "小于",
            "介于"});
            this.cmb_CHECKEDATE.Location = new System.Drawing.Point(88, 50);
            this.cmb_CHECKEDATE.Name = "cmb_CHECKEDATE";
            this.cmb_CHECKEDATE.Size = new System.Drawing.Size(157, 20);
            this.cmb_CHECKEDATE.TabIndex = 9;
            this.cmb_CHECKEDATE.SelectedIndexChanged += new System.EventHandler(this.cmb_CHECKEDATE_SelectedIndexChanged);
            // 
            // check_CHECKEDATE
            // 
            this.check_CHECKEDATE.AutoSize = true;
            this.check_CHECKEDATE.Location = new System.Drawing.Point(10, 53);
            this.check_CHECKEDATE.Name = "check_CHECKEDATE";
            this.check_CHECKEDATE.Size = new System.Drawing.Size(72, 16);
            this.check_CHECKEDATE.TabIndex = 10;
            this.check_CHECKEDATE.Text = "审核日期";
            this.check_CHECKEDATE.UseVisualStyleBackColor = true;
            this.check_CHECKEDATE.CheckedChanged += new System.EventHandler(this.check_CHECKEDATE_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(344, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "到";
            // 
            // time_CHECKEDATE2
            // 
            this.time_CHECKEDATE2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.time_CHECKEDATE2.Enabled = false;
            this.time_CHECKEDATE2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.time_CHECKEDATE2.Location = new System.Drawing.Point(365, 50);
            this.time_CHECKEDATE2.Name = "time_CHECKEDATE2";
            this.time_CHECKEDATE2.Size = new System.Drawing.Size(89, 21);
            this.time_CHECKEDATE2.TabIndex = 7;
            this.time_CHECKEDATE2.Value = new System.DateTime(2012, 2, 23, 0, 0, 0, 0);
            // 
            // time_CHECKEDATE1
            // 
            this.time_CHECKEDATE1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.time_CHECKEDATE1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.time_CHECKEDATE1.Location = new System.Drawing.Point(251, 50);
            this.time_CHECKEDATE1.Name = "time_CHECKEDATE1";
            this.time_CHECKEDATE1.Size = new System.Drawing.Size(89, 21);
            this.time_CHECKEDATE1.TabIndex = 6;
            this.time_CHECKEDATE1.Value = new System.DateTime(2012, 2, 23, 0, 0, 0, 0);
            // 
            // cbTime
            // 
            this.cbTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTime.FormattingEnabled = true;
            this.cbTime.Items.AddRange(new object[] {
            "大于",
            "大于等于",
            "等于",
            "小于等于",
            "小于",
            "介于"});
            this.cbTime.Location = new System.Drawing.Point(88, 19);
            this.cbTime.Name = "cbTime";
            this.cbTime.Size = new System.Drawing.Size(157, 20);
            this.cbTime.TabIndex = 4;
            this.cbTime.SelectedIndexChanged += new System.EventHandler(this.cbTime_SelectedIndexChanged);
            // 
            // chbTime
            // 
            this.chbTime.AutoSize = true;
            this.chbTime.Location = new System.Drawing.Point(10, 22);
            this.chbTime.Name = "chbTime";
            this.chbTime.Size = new System.Drawing.Size(72, 16);
            this.chbTime.TabIndex = 5;
            this.chbTime.Text = "竣工日期";
            this.chbTime.UseVisualStyleBackColor = true;
            this.chbTime.CheckedChanged += new System.EventHandler(this.chbTime_CheckedChanged);
            // 
            // lblETime
            // 
            this.lblETime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblETime.AutoSize = true;
            this.lblETime.Location = new System.Drawing.Point(344, 23);
            this.lblETime.Name = "lblETime";
            this.lblETime.Size = new System.Drawing.Size(17, 12);
            this.lblETime.TabIndex = 3;
            this.lblETime.Text = "到";
            // 
            // DTimeE
            // 
            this.DTimeE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DTimeE.Enabled = false;
            this.DTimeE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTimeE.Location = new System.Drawing.Point(365, 19);
            this.DTimeE.Name = "DTimeE";
            this.DTimeE.Size = new System.Drawing.Size(89, 21);
            this.DTimeE.TabIndex = 1;
            this.DTimeE.Value = new System.DateTime(2012, 2, 23, 0, 0, 0, 0);
            // 
            // DTimeB
            // 
            this.DTimeB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DTimeB.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTimeB.Location = new System.Drawing.Point(251, 19);
            this.DTimeB.Name = "DTimeB";
            this.DTimeB.Size = new System.Drawing.Size(89, 21);
            this.DTimeB.TabIndex = 0;
            this.DTimeB.Value = new System.DateTime(2012, 2, 23, 0, 0, 0, 0);
            // 
            // btn_ExportExcel
            // 
            this.btn_ExportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ExportExcel.Location = new System.Drawing.Point(211, 598);
            this.btn_ExportExcel.Name = "btn_ExportExcel";
            this.btn_ExportExcel.Size = new System.Drawing.Size(88, 23);
            this.btn_ExportExcel.TabIndex = 12;
            this.btn_ExportExcel.Text = "转出Excel(&E)";
            this.btn_ExportExcel.UseVisualStyleBackColor = true;
            this.btn_ExportExcel.Click += new System.EventHandler(this.btn_ExportExcel_Click);
            // 
            // btnStats
            // 
            this.btnStats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStats.Location = new System.Drawing.Point(313, 598);
            this.btnStats.Name = "btnStats";
            this.btnStats.Size = new System.Drawing.Size(73, 23);
            this.btnStats.TabIndex = 13;
            this.btnStats.Text = "统  计(&S)";
            this.btnStats.UseVisualStyleBackColor = true;
            this.btnStats.Click += new System.EventHandler(this.btnStats_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(405, 598);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(73, 23);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "退 出(&Q)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(9, 154);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dGridView);
            this.splitContainer1.Size = new System.Drawing.Size(469, 436);
            this.splitContainer1.SplitterDistance = 244;
            this.splitContainer1.TabIndex = 16;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.gbStats);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.gbList);
            this.splitContainer2.Size = new System.Drawing.Size(469, 244);
            this.splitContainer2.SplitterDistance = 228;
            this.splitContainer2.TabIndex = 17;
            // 
            // gbStats
            // 
            this.gbStats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbStats.Controls.Add(this.clbStats);
            this.gbStats.Controls.Add(this.btnDown);
            this.gbStats.Controls.Add(this.btnUp);
            this.gbStats.Location = new System.Drawing.Point(4, 9);
            this.gbStats.Name = "gbStats";
            this.gbStats.Size = new System.Drawing.Size(221, 232);
            this.gbStats.TabIndex = 11;
            this.gbStats.TabStop = false;
            this.gbStats.Text = "分项统计";
            // 
            // clbStats
            // 
            this.clbStats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbStats.CheckOnClick = true;
            this.clbStats.FormattingEnabled = true;
            this.clbStats.Location = new System.Drawing.Point(5, 16);
            this.clbStats.Name = "clbStats";
            this.clbStats.Size = new System.Drawing.Size(164, 212);
            this.clbStats.TabIndex = 0;
            this.clbStats.SelectedIndexChanged += new System.EventHandler(this.clbStats_SelectedIndexChanged);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDown.Location = new System.Drawing.Point(175, 44);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(40, 24);
            this.btnDown.TabIndex = 2;
            this.btnDown.Text = "下移";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUp.Location = new System.Drawing.Point(175, 16);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(40, 22);
            this.btnUp.TabIndex = 1;
            this.btnUp.Text = "上移";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // gbList
            // 
            this.gbList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbList.Controls.Add(this.cbAll);
            this.gbList.Controls.Add(this.clbList);
            this.gbList.Location = new System.Drawing.Point(3, 9);
            this.gbList.Name = "gbList";
            this.gbList.Size = new System.Drawing.Size(231, 232);
            this.gbList.TabIndex = 16;
            this.gbList.TabStop = false;
            this.gbList.Text = "分组类型:";
            // 
            // cbAll
            // 
            this.cbAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAll.AutoSize = true;
            this.cbAll.Location = new System.Drawing.Point(179, 16);
            this.cbAll.Name = "cbAll";
            this.cbAll.Size = new System.Drawing.Size(48, 16);
            this.cbAll.TabIndex = 1;
            this.cbAll.Text = "全选";
            this.cbAll.UseVisualStyleBackColor = true;
            this.cbAll.CheckedChanged += new System.EventHandler(this.cbAll_CheckedChanged);
            // 
            // clbList
            // 
            this.clbList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbList.CheckOnClick = true;
            this.clbList.FormattingEnabled = true;
            this.clbList.Location = new System.Drawing.Point(5, 16);
            this.clbList.Name = "clbList";
            this.clbList.Size = new System.Drawing.Size(171, 212);
            this.clbList.TabIndex = 0;
            this.clbList.SelectedIndexChanged += new System.EventHandler(this.clbList_SelectedIndexChanged);
            // 
            // dGridView
            // 
            this.dGridView.AllowUserToAddRows = false;
            this.dGridView.AllowUserToDeleteRows = false;
            this.dGridView.AllowUserToResizeRows = false;
            this.dGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dGridView.Location = new System.Drawing.Point(0, 0);
            this.dGridView.Name = "dGridView";
            this.dGridView.ReadOnly = true;
            this.dGridView.RowHeadersVisible = false;
            this.dGridView.RowTemplate.Height = 23;
            this.dGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dGridView.Size = new System.Drawing.Size(469, 188);
            this.dGridView.TabIndex = 1;
            // 
            // FrmQuerySheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 627);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnStats);
            this.Controls.Add(this.btn_ExportExcel);
            this.Controls.Add(this.gbNumber);
            this.Controls.Add(this.gbLayers);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmQuerySheet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "固定格式报表";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmQuerySheet_Load);
            this.gbLayers.ResumeLayout(false);
            this.gbLayers.PerformLayout();
            this.gbNumber.ResumeLayout(false);
            this.gbNumber.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.gbStats.ResumeLayout(false);
            this.gbList.ResumeLayout(false);
            this.gbList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbLayers;
        private System.Windows.Forms.ComboBox cbLayer;
        private System.Windows.Forms.GroupBox gbNumber;
        private System.Windows.Forms.ComboBox cbTime;
        private System.Windows.Forms.Label lblETime;
        private System.Windows.Forms.DateTimePicker DTimeE;
        private System.Windows.Forms.DateTimePicker DTimeB;
        private System.Windows.Forms.Button btn_ExportExcel;
        private System.Windows.Forms.Button btnStats;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chbTime;
        private System.Windows.Forms.CheckBox cbData;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox gbStats;
        private System.Windows.Forms.CheckedListBox clbStats;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.GroupBox gbList;
        private System.Windows.Forms.CheckBox cbAll;
        private System.Windows.Forms.CheckedListBox clbList;
        private System.Windows.Forms.DataGridView dGridView;
        private System.Windows.Forms.ComboBox cmb_CHECKEDATE;
        private System.Windows.Forms.CheckBox check_CHECKEDATE;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker time_CHECKEDATE2;
        private System.Windows.Forms.DateTimePicker time_CHECKEDATE1;


    }
}