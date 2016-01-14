namespace JJWATQuery
{
    partial class FrmQueryRangeResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQueryRangeResult));
            this.trvQueryResult = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.progressBarSearching = new System.Windows.Forms.ProgressBar();
            this.btn_Locate = new System.Windows.Forms.Button();
            this.btn_Export = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGrid_Detail4clsid = new System.Windows.Forms.DataGridView();
            this.dataGrid_QueryResult = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_DataViewCLSID = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Detail4clsid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_QueryResult)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // trvQueryResult
            // 
            this.trvQueryResult.Dock = System.Windows.Forms.DockStyle.Left;
            this.trvQueryResult.HideSelection = false;
            this.trvQueryResult.Location = new System.Drawing.Point(0, 0);
            this.trvQueryResult.Name = "trvQueryResult";
            this.trvQueryResult.Size = new System.Drawing.Size(145, 661);
            this.trvQueryResult.TabIndex = 1;
            this.trvQueryResult.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvQueryResult_NodeMouseClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(145, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(975, 661);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(967, 635);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据视图";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.progressBarSearching);
            this.panel2.Controls.Add(this.btn_Locate);
            this.panel2.Controls.Add(this.btn_Export);
            this.panel2.Location = new System.Drawing.Point(0, 590);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(957, 45);
            this.panel2.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(842, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 25);
            this.button1.TabIndex = 12;
            this.button1.Text = "退出";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBarSearching
            // 
            this.progressBarSearching.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBarSearching.Location = new System.Drawing.Point(12, 8);
            this.progressBarSearching.Name = "progressBarSearching";
            this.progressBarSearching.Size = new System.Drawing.Size(288, 29);
            this.progressBarSearching.TabIndex = 11;
            this.progressBarSearching.Visible = false;
            // 
            // btn_Locate
            // 
            this.btn_Locate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Locate.Location = new System.Drawing.Point(629, 12);
            this.btn_Locate.Name = "btn_Locate";
            this.btn_Locate.Size = new System.Drawing.Size(85, 25);
            this.btn_Locate.TabIndex = 9;
            this.btn_Locate.Text = "定 位(&L)";
            this.btn_Locate.UseVisualStyleBackColor = true;
            this.btn_Locate.Click += new System.EventHandler(this.btn_Locate_Click);
            // 
            // btn_Export
            // 
            this.btn_Export.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Export.Location = new System.Drawing.Point(736, 12);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(85, 25);
            this.btn_Export.TabIndex = 10;
            this.btn_Export.Text = "导出(&E)";
            this.btn_Export.UseVisualStyleBackColor = true;
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dataGrid_Detail4clsid);
            this.groupBox1.Controls.Add(this.dataGrid_QueryResult);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Location = new System.Drawing.Point(0, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(957, 546);
            this.groupBox1.TabIndex = 6;
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
            this.dataGrid_Detail4clsid.Location = new System.Drawing.Point(6, 295);
            this.dataGrid_Detail4clsid.MultiSelect = false;
            this.dataGrid_Detail4clsid.Name = "dataGrid_Detail4clsid";
            this.dataGrid_Detail4clsid.ReadOnly = true;
            this.dataGrid_Detail4clsid.RowHeadersWidth = 6;
            this.dataGrid_Detail4clsid.RowTemplate.Height = 23;
            this.dataGrid_Detail4clsid.RowTemplate.ReadOnly = true;
            this.dataGrid_Detail4clsid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid_Detail4clsid.Size = new System.Drawing.Size(945, 244);
            this.dataGrid_Detail4clsid.TabIndex = 5;
            // 
            // dataGrid_QueryResult
            // 
            this.dataGrid_QueryResult.AllowUserToAddRows = false;
            this.dataGrid_QueryResult.AllowUserToDeleteRows = false;
            this.dataGrid_QueryResult.AllowUserToResizeRows = false;
            this.dataGrid_QueryResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid_QueryResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_QueryResult.Location = new System.Drawing.Point(6, 13);
            this.dataGrid_QueryResult.MultiSelect = false;
            this.dataGrid_QueryResult.Name = "dataGrid_QueryResult";
            this.dataGrid_QueryResult.ReadOnly = true;
            this.dataGrid_QueryResult.RowTemplate.Height = 23;
            this.dataGrid_QueryResult.RowTemplate.ReadOnly = true;
            this.dataGrid_QueryResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid_QueryResult.Size = new System.Drawing.Size(945, 278);
            this.dataGrid_QueryResult.TabIndex = 4;
            this.dataGrid_QueryResult.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGrid_QueryResult_CellMouseClick);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Location = new System.Drawing.Point(3, 13);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(954, 492);
            this.panel3.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lbl_DataViewCLSID);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(967, 37);
            this.panel1.TabIndex = 0;
            // 
            // lbl_DataViewCLSID
            // 
            this.lbl_DataViewCLSID.AutoSize = true;
            this.lbl_DataViewCLSID.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_DataViewCLSID.Location = new System.Drawing.Point(2, 15);
            this.lbl_DataViewCLSID.Name = "lbl_DataViewCLSID";
            this.lbl_DataViewCLSID.Size = new System.Drawing.Size(57, 12);
            this.lbl_DataViewCLSID.TabIndex = 3;
            this.lbl_DataViewCLSID.Text = "方案号：";
            // 
            // FrmQueryRangeResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 661);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.trvQueryResult);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmQueryRangeResult";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查询结果";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmQueryRangeResult_FormClosed);
            this.Load += new System.EventHandler(this.FrmQueryRangeResult_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Detail4clsid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_QueryResult)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView trvQueryResult;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_DataViewCLSID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGrid_Detail4clsid;
        private System.Windows.Forms.DataGridView dataGrid_QueryResult;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ProgressBar progressBarSearching;
        private System.Windows.Forms.Button btn_Locate;
        private System.Windows.Forms.Button btn_Export;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button1;
    }
}