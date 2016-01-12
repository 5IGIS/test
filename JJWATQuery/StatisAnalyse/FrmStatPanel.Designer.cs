namespace JJWATQuery
{
    partial class FrmStatPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStatPanel));
            this.rdBtn_PipeLenMons = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboBuildDt = new System.Windows.Forms.ComboBox();
            this.lbl_InputDate = new System.Windows.Forms.Label();
            this.lbl_To = new System.Windows.Forms.Label();
            this.dTPDateFrom = new System.Windows.Forms.DateTimePicker();
            this.dTPDateTo = new System.Windows.Forms.DateTimePicker();
            this.btn_Export = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnPrintRoute = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdBtn_PipeLenMons
            // 
            this.rdBtn_PipeLenMons.AutoSize = true;
            this.rdBtn_PipeLenMons.Checked = true;
            this.rdBtn_PipeLenMons.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdBtn_PipeLenMons.Location = new System.Drawing.Point(9, 20);
            this.rdBtn_PipeLenMons.Name = "rdBtn_PipeLenMons";
            this.rdBtn_PipeLenMons.Size = new System.Drawing.Size(137, 18);
            this.rdBtn_PipeLenMons.TabIndex = 0;
            this.rdBtn_PipeLenMons.TabStop = true;
            this.rdBtn_PipeLenMons.Text = "管线长度统计月报";
            this.rdBtn_PipeLenMons.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboBuildDt);
            this.groupBox1.Controls.Add(this.lbl_InputDate);
            this.groupBox1.Controls.Add(this.lbl_To);
            this.groupBox1.Controls.Add(this.dTPDateFrom);
            this.groupBox1.Controls.Add(this.dTPDateTo);
            this.groupBox1.Location = new System.Drawing.Point(3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(520, 128);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // cboBuildDt
            // 
            this.cboBuildDt.FormattingEnabled = true;
            this.cboBuildDt.Location = new System.Drawing.Point(117, 82);
            this.cboBuildDt.Name = "cboBuildDt";
            this.cboBuildDt.Size = new System.Drawing.Size(64, 20);
            this.cboBuildDt.TabIndex = 7;
            this.cboBuildDt.SelectedIndexChanged += new System.EventHandler(this.cboBuildDt_SelectedIndexChanged);
            // 
            // lbl_InputDate
            // 
            this.lbl_InputDate.AutoSize = true;
            this.lbl_InputDate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_InputDate.Location = new System.Drawing.Point(8, 84);
            this.lbl_InputDate.Name = "lbl_InputDate";
            this.lbl_InputDate.Size = new System.Drawing.Size(98, 14);
            this.lbl_InputDate.TabIndex = 6;
            this.lbl_InputDate.Text = "撤管时间选择:";
            // 
            // lbl_To
            // 
            this.lbl_To.AutoSize = true;
            this.lbl_To.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_To.Location = new System.Drawing.Point(321, 84);
            this.lbl_To.Name = "lbl_To";
            this.lbl_To.Size = new System.Drawing.Size(21, 14);
            this.lbl_To.TabIndex = 5;
            this.lbl_To.Text = "到";
            // 
            // dTPDateFrom
            // 
            this.dTPDateFrom.Location = new System.Drawing.Point(188, 81);
            this.dTPDateFrom.Name = "dTPDateFrom";
            this.dTPDateFrom.Size = new System.Drawing.Size(128, 21);
            this.dTPDateFrom.TabIndex = 4;
            this.dTPDateFrom.ValueChanged += new System.EventHandler(this.dTPDateFrom_ValueChanged);
            // 
            // dTPDateTo
            // 
            this.dTPDateTo.Location = new System.Drawing.Point(344, 81);
            this.dTPDateTo.Name = "dTPDateTo";
            this.dTPDateTo.Size = new System.Drawing.Size(125, 21);
            this.dTPDateTo.TabIndex = 3;
            this.dTPDateTo.ValueChanged += new System.EventHandler(this.dTPDateTo_ValueChanged);
            // 
            // btn_Export
            // 
            this.btn_Export.Location = new System.Drawing.Point(396, 136);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(127, 25);
            this.btn_Export.TabIndex = 2;
            this.btn_Export.Text = "导出Excel(&E)";
            this.btn_Export.UseVisualStyleBackColor = true;
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdBtn_PipeLenMons);
            this.groupBox2.Location = new System.Drawing.Point(3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(520, 55);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "统计内容";
            // 
            // btnPrintRoute
            // 
            this.btnPrintRoute.Location = new System.Drawing.Point(240, 136);
            this.btnPrintRoute.Name = "btnPrintRoute";
            this.btnPrintRoute.Size = new System.Drawing.Size(127, 25);
            this.btnPrintRoute.TabIndex = 9;
            this.btnPrintRoute.Text = "打印路由本";
            this.btnPrintRoute.UseVisualStyleBackColor = true;
            this.btnPrintRoute.Click += new System.EventHandler(this.btnPrintRoute_Click);
            // 
            // FrmStatPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 163);
            this.Controls.Add(this.btnPrintRoute);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_Export);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmStatPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "撤管统计";
            this.Load += new System.EventHandler(this.FrmStatPanel_Load);
            this.Shown += new System.EventHandler(this.FrmStatPanel_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rdBtn_PipeLenMons;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboBuildDt;
        private System.Windows.Forms.Label lbl_InputDate;
        private System.Windows.Forms.Label lbl_To;
        private System.Windows.Forms.DateTimePicker dTPDateFrom;
        private System.Windows.Forms.DateTimePicker dTPDateTo;
        private System.Windows.Forms.Button btn_Export;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnPrintRoute;
    }
}