namespace JJWATQuery
{
    partial class UtilitysResultForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UtilitysResultForm));
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btn_ExportExcel = new System.Windows.Forms.Button();
            this.lab_PageInfo = new System.Windows.Forms.Label();
            this.btn_LastPage = new System.Windows.Forms.Button();
            this.btn_NextPage = new System.Windows.Forms.Button();
            this.btn_BackPage = new System.Windows.Forms.Button();
            this.btn_FirstPage = new System.Windows.Forms.Button();
            this.trvQueryResult = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgv_QueryResult = new System.Windows.Forms.DataGridView();
            this.btn_Selection = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_GraphicPosition = new System.Windows.Forms.Button();
            this.groupBox_Count = new System.Windows.Forms.GroupBox();
            this.lvwQueryResult = new System.Windows.Forms.ListView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.reportControl1 = new JJWATQuery.SelReportControl();
            this.cmb_IconStyle = new System.Windows.Forms.ComboBox();
            this.cmb_StatSort = new System.Windows.Forms.ComboBox();
            this.btn_SetReportMgs = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_QueryResult)).BeginInit();
            this.groupBox_Count.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(691, 524);
            this.tabControl2.TabIndex = 2;
            this.tabControl2.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl2_Selected);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btn_ExportExcel);
            this.tabPage3.Controls.Add(this.lab_PageInfo);
            this.tabPage3.Controls.Add(this.btn_LastPage);
            this.tabPage3.Controls.Add(this.btn_NextPage);
            this.tabPage3.Controls.Add(this.btn_BackPage);
            this.tabPage3.Controls.Add(this.btn_FirstPage);
            this.tabPage3.Controls.Add(this.trvQueryResult);
            this.tabPage3.Controls.Add(this.tabControl1);
            this.tabPage3.Controls.Add(this.btn_Selection);
            this.tabPage3.Controls.Add(this.btn_Close);
            this.tabPage3.Controls.Add(this.btn_GraphicPosition);
            this.tabPage3.Controls.Add(this.groupBox_Count);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(683, 498);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "数据显示";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btn_ExportExcel
            // 
            this.btn_ExportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ExportExcel.Enabled = false;
            this.btn_ExportExcel.Location = new System.Drawing.Point(486, 469);
            this.btn_ExportExcel.Name = "btn_ExportExcel";
            this.btn_ExportExcel.Size = new System.Drawing.Size(94, 23);
            this.btn_ExportExcel.TabIndex = 7;
            this.btn_ExportExcel.Text = "转出Excel(&O)";
            this.btn_ExportExcel.UseVisualStyleBackColor = true;
            this.btn_ExportExcel.Click += new System.EventHandler(this.btn_ExportExcel_Click);
            // 
            // lab_PageInfo
            // 
            this.lab_PageInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lab_PageInfo.AutoSize = true;
            this.lab_PageInfo.Location = new System.Drawing.Point(178, 172);
            this.lab_PageInfo.Name = "lab_PageInfo";
            this.lab_PageInfo.Size = new System.Drawing.Size(131, 12);
            this.lab_PageInfo.TabIndex = 13;
            this.lab_PageInfo.Text = "第0页，共0页，每页0条";
            // 
            // btn_LastPage
            // 
            this.btn_LastPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_LastPage.Location = new System.Drawing.Point(625, 167);
            this.btn_LastPage.Name = "btn_LastPage";
            this.btn_LastPage.Size = new System.Drawing.Size(52, 23);
            this.btn_LastPage.TabIndex = 12;
            this.btn_LastPage.Text = "尾页";
            this.btn_LastPage.UseVisualStyleBackColor = true;
            this.btn_LastPage.Click += new System.EventHandler(this.btn_LastPage_Click);
            // 
            // btn_NextPage
            // 
            this.btn_NextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_NextPage.Location = new System.Drawing.Point(567, 167);
            this.btn_NextPage.Name = "btn_NextPage";
            this.btn_NextPage.Size = new System.Drawing.Size(52, 23);
            this.btn_NextPage.TabIndex = 12;
            this.btn_NextPage.Text = "下一页";
            this.btn_NextPage.UseVisualStyleBackColor = true;
            this.btn_NextPage.Click += new System.EventHandler(this.btn_NextPage_Click);
            // 
            // btn_BackPage
            // 
            this.btn_BackPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_BackPage.Location = new System.Drawing.Point(509, 167);
            this.btn_BackPage.Name = "btn_BackPage";
            this.btn_BackPage.Size = new System.Drawing.Size(52, 23);
            this.btn_BackPage.TabIndex = 12;
            this.btn_BackPage.Text = "上一页";
            this.btn_BackPage.UseVisualStyleBackColor = true;
            this.btn_BackPage.Click += new System.EventHandler(this.btn_BackPage_Click);
            // 
            // btn_FirstPage
            // 
            this.btn_FirstPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_FirstPage.Location = new System.Drawing.Point(451, 167);
            this.btn_FirstPage.Name = "btn_FirstPage";
            this.btn_FirstPage.Size = new System.Drawing.Size(52, 23);
            this.btn_FirstPage.TabIndex = 12;
            this.btn_FirstPage.Text = "首页";
            this.btn_FirstPage.UseVisualStyleBackColor = true;
            this.btn_FirstPage.Click += new System.EventHandler(this.btn_FirstPage_Click);
            // 
            // trvQueryResult
            // 
            this.trvQueryResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.trvQueryResult.HideSelection = false;
            this.trvQueryResult.Location = new System.Drawing.Point(3, 3);
            this.trvQueryResult.Name = "trvQueryResult";
            this.trvQueryResult.Size = new System.Drawing.Size(171, 460);
            this.trvQueryResult.TabIndex = 4;
            this.trvQueryResult.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvQueryResult_NodeMouseClick);
            this.trvQueryResult.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trvQueryResult_MouseDown);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(180, 195);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(500, 239);
            this.tabControl1.TabIndex = 11;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgv_QueryResult);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(492, 213);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgv_QueryResult
            // 
            this.dgv_QueryResult.AllowUserToAddRows = false;
            this.dgv_QueryResult.AllowUserToDeleteRows = false;
            this.dgv_QueryResult.AllowUserToResizeRows = false;
            this.dgv_QueryResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_QueryResult.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgv_QueryResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_QueryResult.Location = new System.Drawing.Point(3, 3);
            this.dgv_QueryResult.Name = "dgv_QueryResult";
            this.dgv_QueryResult.ReadOnly = true;
            this.dgv_QueryResult.RowHeadersVisible = false;
            this.dgv_QueryResult.RowTemplate.Height = 23;
            this.dgv_QueryResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_QueryResult.Size = new System.Drawing.Size(486, 207);
            this.dgv_QueryResult.TabIndex = 0;
            // 
            // btn_Selection
            // 
            this.btn_Selection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Selection.Location = new System.Drawing.Point(3, 469);
            this.btn_Selection.Name = "btn_Selection";
            this.btn_Selection.Size = new System.Drawing.Size(171, 23);
            this.btn_Selection.TabIndex = 7;
            this.btn_Selection.Text = "生成选择集(&S)";
            this.btn_Selection.UseVisualStyleBackColor = true;
            this.btn_Selection.Click += new System.EventHandler(this.btn_Selection_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.Location = new System.Drawing.Point(586, 469);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(94, 23);
            this.btn_Close.TabIndex = 6;
            this.btn_Close.Text = "关闭(&C)";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // btn_GraphicPosition
            // 
            this.btn_GraphicPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_GraphicPosition.Enabled = false;
            this.btn_GraphicPosition.Location = new System.Drawing.Point(180, 440);
            this.btn_GraphicPosition.Name = "btn_GraphicPosition";
            this.btn_GraphicPosition.Size = new System.Drawing.Size(500, 23);
            this.btn_GraphicPosition.TabIndex = 9;
            this.btn_GraphicPosition.Text = "对象图形定位(&G)";
            this.btn_GraphicPosition.UseVisualStyleBackColor = true;
            this.btn_GraphicPosition.Click += new System.EventHandler(this.btn_GraphicPosition_Click);
            // 
            // groupBox_Count
            // 
            this.groupBox_Count.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_Count.Controls.Add(this.lvwQueryResult);
            this.groupBox_Count.Location = new System.Drawing.Point(180, 3);
            this.groupBox_Count.Name = "groupBox_Count";
            this.groupBox_Count.Size = new System.Drawing.Size(500, 158);
            this.groupBox_Count.TabIndex = 10;
            this.groupBox_Count.TabStop = false;
            // 
            // lvwQueryResult
            // 
            this.lvwQueryResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwQueryResult.FullRowSelect = true;
            this.lvwQueryResult.GridLines = true;
            this.lvwQueryResult.HideSelection = false;
            this.lvwQueryResult.Location = new System.Drawing.Point(3, 17);
            this.lvwQueryResult.MultiSelect = false;
            this.lvwQueryResult.Name = "lvwQueryResult";
            this.lvwQueryResult.Size = new System.Drawing.Size(494, 138);
            this.lvwQueryResult.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.lvwQueryResult.TabIndex = 0;
            this.lvwQueryResult.UseCompatibleStateImageBehavior = false;
            this.lvwQueryResult.View = System.Windows.Forms.View.Details;
            this.lvwQueryResult.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwQueryResult_ColumnClick);
            this.lvwQueryResult.Click += new System.EventHandler(this.lvwQueryResult_Click);
            this.lvwQueryResult.DoubleClick += new System.EventHandler(this.lvwQueryResult_DoubleClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.elementHost1);
            this.tabPage2.Controls.Add(this.cmb_IconStyle);
            this.tabPage2.Controls.Add(this.cmb_StatSort);
            this.tabPage2.Controls.Add(this.btn_SetReportMgs);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(683, 498);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "视图显示";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // elementHost1
            // 
            this.elementHost1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.elementHost1.Location = new System.Drawing.Point(3, 6);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(672, 453);
            this.elementHost1.TabIndex = 4;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.reportControl1;
            // 
            // cmb_IconStyle
            // 
            this.cmb_IconStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmb_IconStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_IconStyle.FormattingEnabled = true;
            this.cmb_IconStyle.Items.AddRange(new object[] {
            "饼图",
            "柱形图"});
            this.cmb_IconStyle.Location = new System.Drawing.Point(375, 467);
            this.cmb_IconStyle.Name = "cmb_IconStyle";
            this.cmb_IconStyle.Size = new System.Drawing.Size(121, 20);
            this.cmb_IconStyle.TabIndex = 3;
            // 
            // cmb_StatSort
            // 
            this.cmb_StatSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmb_StatSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_StatSort.FormattingEnabled = true;
            this.cmb_StatSort.Location = new System.Drawing.Point(131, 467);
            this.cmb_StatSort.Name = "cmb_StatSort";
            this.cmb_StatSort.Size = new System.Drawing.Size(121, 20);
            this.cmb_StatSort.TabIndex = 3;
            // 
            // btn_SetReportMgs
            // 
            this.btn_SetReportMgs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_SetReportMgs.Location = new System.Drawing.Point(566, 465);
            this.btn_SetReportMgs.Name = "btn_SetReportMgs";
            this.btn_SetReportMgs.Size = new System.Drawing.Size(93, 22);
            this.btn_SetReportMgs.TabIndex = 2;
            this.btn_SetReportMgs.Text = "生成图表(&S)";
            this.btn_SetReportMgs.UseVisualStyleBackColor = true;
            this.btn_SetReportMgs.Click += new System.EventHandler(this.btn_SetReportMgs_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(304, 470);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "图标样式：";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(155, 533);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "统计分类：";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 470);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 1;
            // 
            // UtilitysResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 524);
            this.Controls.Add(this.tabControl2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UtilitysResultForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查询结果显示";
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_QueryResult)).EndInit();
            this.groupBox_Count.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TreeView trvQueryResult;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgv_QueryResult;
        private System.Windows.Forms.Button btn_Selection;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_ExportExcel;
        private System.Windows.Forms.Button btn_GraphicPosition;
        private System.Windows.Forms.GroupBox groupBox_Count;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lab_PageInfo;
        private System.Windows.Forms.Button btn_LastPage;
        private System.Windows.Forms.Button btn_NextPage;
        private System.Windows.Forms.Button btn_BackPage;
        private System.Windows.Forms.Button btn_FirstPage;
        private System.Windows.Forms.ListView lvwQueryResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_IconStyle;
        private System.Windows.Forms.ComboBox cmb_StatSort;
        private System.Windows.Forms.Button btn_SetReportMgs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private SelReportControl reportControl1;
    }
}