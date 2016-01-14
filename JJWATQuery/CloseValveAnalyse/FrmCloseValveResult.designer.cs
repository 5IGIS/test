namespace JJWATQuery
{
    partial class FrmQueryResult
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQueryResult));
            this.trvQueryResult = new System.Windows.Forms.TreeView();
            this.lbl_DataViewCLSID = new System.Windows.Forms.Label();
            this.dataGrid_QueryResult = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGrid_Detail4clsid = new System.Windows.Forms.DataGridView();
            this.btn_Locate = new System.Windows.Forms.Button();
            this.btn_Export = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.progressBarSearching = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lbl_FigureOutStyle = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnOutExcel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_QueryResult)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Detail4clsid)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // trvQueryResult
            // 
            this.trvQueryResult.Dock = System.Windows.Forms.DockStyle.Left;
            this.trvQueryResult.HideSelection = false;
            this.trvQueryResult.Location = new System.Drawing.Point(0, 0);
            this.trvQueryResult.Name = "trvQueryResult";
            this.trvQueryResult.Size = new System.Drawing.Size(145, 630);
            this.trvQueryResult.TabIndex = 0;
            this.trvQueryResult.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvQueryResult_NodeMouseClick);
            // 
            // lbl_DataViewCLSID
            // 
            this.lbl_DataViewCLSID.AutoSize = true;
            this.lbl_DataViewCLSID.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_DataViewCLSID.Location = new System.Drawing.Point(6, 7);
            this.lbl_DataViewCLSID.Name = "lbl_DataViewCLSID";
            this.lbl_DataViewCLSID.Size = new System.Drawing.Size(57, 12);
            this.lbl_DataViewCLSID.TabIndex = 2;
            this.lbl_DataViewCLSID.Text = "方案号：";
            // 
            // dataGrid_QueryResult
            // 
            this.dataGrid_QueryResult.AllowUserToAddRows = false;
            this.dataGrid_QueryResult.AllowUserToDeleteRows = false;
            this.dataGrid_QueryResult.AllowUserToResizeRows = false;
            this.dataGrid_QueryResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid_QueryResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_QueryResult.Location = new System.Drawing.Point(6, 13);
            this.dataGrid_QueryResult.MultiSelect = false;
            this.dataGrid_QueryResult.Name = "dataGrid_QueryResult";
            this.dataGrid_QueryResult.ReadOnly = true;
            this.dataGrid_QueryResult.RowTemplate.Height = 23;
            this.dataGrid_QueryResult.RowTemplate.ReadOnly = true;
            this.dataGrid_QueryResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid_QueryResult.Size = new System.Drawing.Size(786, 238);
            this.dataGrid_QueryResult.TabIndex = 4;
            this.dataGrid_QueryResult.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGrid_QueryResult_CellMouseClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dataGrid_Detail4clsid);
            this.groupBox1.Controls.Add(this.dataGrid_QueryResult);
            this.groupBox1.Location = new System.Drawing.Point(8, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(798, 523);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // dataGrid_Detail4clsid
            // 
            this.dataGrid_Detail4clsid.AllowUserToAddRows = false;
            this.dataGrid_Detail4clsid.AllowUserToDeleteRows = false;
            this.dataGrid_Detail4clsid.AllowUserToResizeRows = false;
            this.dataGrid_Detail4clsid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid_Detail4clsid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGrid_Detail4clsid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGrid_Detail4clsid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_Detail4clsid.Location = new System.Drawing.Point(6, 264);
            this.dataGrid_Detail4clsid.MultiSelect = false;
            this.dataGrid_Detail4clsid.Name = "dataGrid_Detail4clsid";
            this.dataGrid_Detail4clsid.ReadOnly = true;
            this.dataGrid_Detail4clsid.RowHeadersWidth = 6;
            this.dataGrid_Detail4clsid.RowTemplate.Height = 23;
            this.dataGrid_Detail4clsid.RowTemplate.ReadOnly = true;
            this.dataGrid_Detail4clsid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid_Detail4clsid.Size = new System.Drawing.Size(786, 252);
            this.dataGrid_Detail4clsid.TabIndex = 5;
            // 
            // btn_Locate
            // 
            this.btn_Locate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Locate.Location = new System.Drawing.Point(507, 564);
            this.btn_Locate.Name = "btn_Locate";
            this.btn_Locate.Size = new System.Drawing.Size(85, 23);
            this.btn_Locate.TabIndex = 6;
            this.btn_Locate.Text = "定 位(&L)";
            this.btn_Locate.UseVisualStyleBackColor = true;
            this.btn_Locate.Click += new System.EventHandler(this.btn_Locate_Click);
            // 
            // btn_Export
            // 
            this.btn_Export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Export.Location = new System.Drawing.Point(612, 564);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(85, 23);
            this.btn_Export.TabIndex = 7;
            this.btn_Export.Text = "导出(&E)";
            this.btn_Export.UseVisualStyleBackColor = true;
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(151, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(827, 630);
            this.tabControl1.TabIndex = 9;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.progressBarSearching);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.lbl_DataViewCLSID);
            this.tabPage1.Controls.Add(this.btn_Locate);
            this.tabPage1.Controls.Add(this.btn_Export);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(819, 604);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据视图";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(715, 564);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "退出(&O)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBarSearching
            // 
            this.progressBarSearching.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBarSearching.Location = new System.Drawing.Point(14, 558);
            this.progressBarSearching.Name = "progressBarSearching";
            this.progressBarSearching.Size = new System.Drawing.Size(288, 29);
            this.progressBarSearching.TabIndex = 8;
            this.progressBarSearching.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(8, 546);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(798, 55);
            this.panel1.TabIndex = 10;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chart1);
            this.tabPage2.Controls.Add(this.lbl_FigureOutStyle);
            this.tabPage2.Controls.Add(this.comboBox1);
            this.tabPage2.Controls.Add(this.btnOutExcel);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(819, 604);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "图表视图";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(6, 6);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(774, 514);
            this.chart1.TabIndex = 11;
            this.chart1.Text = "chart1";
            // 
            // lbl_FigureOutStyle
            // 
            this.lbl_FigureOutStyle.AutoSize = true;
            this.lbl_FigureOutStyle.Location = new System.Drawing.Point(84, 540);
            this.lbl_FigureOutStyle.Name = "lbl_FigureOutStyle";
            this.lbl_FigureOutStyle.Size = new System.Drawing.Size(53, 12);
            this.lbl_FigureOutStyle.TabIndex = 9;
            this.lbl_FigureOutStyle.Text = "报表样式";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "柱型",
            "圆型",
            "折线型"});
            this.comboBox1.Location = new System.Drawing.Point(155, 537);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnOutExcel
            // 
            this.btnOutExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOutExcel.Location = new System.Drawing.Point(562, 534);
            this.btnOutExcel.Name = "btnOutExcel";
            this.btnOutExcel.Size = new System.Drawing.Size(75, 23);
            this.btnOutExcel.TabIndex = 6;
            this.btnOutExcel.Text = "导出Excel";
            this.btnOutExcel.UseVisualStyleBackColor = true;
            this.btnOutExcel.Visible = false;
            this.btnOutExcel.Click += new System.EventHandler(this.btnOutExcel_Click);
            // 
            // FrmQueryResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 630);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.trvQueryResult);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmQueryResult";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查询结果";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmQueryResult_FormClosed);
            this.Load += new System.EventHandler(this.FrmQueryResult_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_QueryResult)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Detail4clsid)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView trvQueryResult;
        private System.Windows.Forms.Label lbl_DataViewCLSID;
        private System.Windows.Forms.DataGridView dataGrid_QueryResult;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Locate;
        private System.Windows.Forms.Button btn_Export;
        private System.Windows.Forms.DataGridView dataGrid_Detail4clsid;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lbl_FigureOutStyle;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnOutExcel;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ProgressBar progressBarSearching;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
    }
}