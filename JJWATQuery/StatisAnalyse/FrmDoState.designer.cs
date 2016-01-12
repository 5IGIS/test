namespace JJWATQuery
{
    partial class FrmDoState
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDoState));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboCheckDt = new System.Windows.Forms.ComboBox();
            this.cboRemoveDt = new System.Windows.Forms.ComboBox();
            this.lbl_InputDate = new System.Windows.Forms.Label();
            this.lbl_To = new System.Windows.Forms.Label();
            this.dTPDateFrom = new System.Windows.Forms.DateTimePicker();
            this.dTPDateTo = new System.Windows.Forms.DateTimePicker();
            this.rdBtn_PipeLenMons = new System.Windows.Forms.RadioButton();
            this.optPipeInvestAddDetail = new System.Windows.Forms.RadioButton();
            this.optPipeInvestAddTable = new System.Windows.Forms.RadioButton();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpEndStateDate = new System.Windows.Forms.DateTimePicker();
            this.dtpBegStateDate = new System.Windows.Forms.DateTimePicker();
            this.lblTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLine = new System.Windows.Forms.Label();
            this.optNewSWTPipeLenMonthStat = new System.Windows.Forms.RadioButton();
            this.optNewHydrantMonthStat = new System.Windows.Forms.RadioButton();
            this.optNewWTPipeLenMonthStat = new System.Windows.Forms.RadioButton();
            this.optNewPipeLenMonthStat = new System.Windows.Forms.RadioButton();
            this.optPipeLenStat = new System.Windows.Forms.RadioButton();
            this.optValvestat = new System.Windows.Forms.RadioButton();
            this.cboWaterType = new System.Windows.Forms.ComboBox();
            this.lblWaterType = new System.Windows.Forms.Label();
            this.lblQSTJ = new System.Windows.Forms.Label();
            this.btnState = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnExprtExcel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboCheckDt);
            this.groupBox1.Controls.Add(this.cboRemoveDt);
            this.groupBox1.Controls.Add(this.lbl_InputDate);
            this.groupBox1.Controls.Add(this.lbl_To);
            this.groupBox1.Controls.Add(this.dTPDateFrom);
            this.groupBox1.Controls.Add(this.dTPDateTo);
            this.groupBox1.Controls.Add(this.rdBtn_PipeLenMons);
            this.groupBox1.Controls.Add(this.optPipeInvestAddDetail);
            this.groupBox1.Controls.Add(this.optPipeInvestAddTable);
            this.groupBox1.Controls.Add(this.lblTo);
            this.groupBox1.Controls.Add(this.dtpEndStateDate);
            this.groupBox1.Controls.Add(this.dtpBegStateDate);
            this.groupBox1.Controls.Add(this.lblTime);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblLine);
            this.groupBox1.Controls.Add(this.optNewSWTPipeLenMonthStat);
            this.groupBox1.Controls.Add(this.optNewHydrantMonthStat);
            this.groupBox1.Controls.Add(this.optNewWTPipeLenMonthStat);
            this.groupBox1.Controls.Add(this.optNewPipeLenMonthStat);
            this.groupBox1.Controls.Add(this.optPipeLenStat);
            this.groupBox1.Controls.Add(this.optValvestat);
            this.groupBox1.Controls.Add(this.cboWaterType);
            this.groupBox1.Controls.Add(this.lblWaterType);
            this.groupBox1.Controls.Add(this.lblQSTJ);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(492, 352);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "统计内容";
            // 
            // cboCheckDt
            // 
            this.cboCheckDt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCheckDt.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboCheckDt.FormattingEnabled = true;
            this.cboCheckDt.Location = new System.Drawing.Point(121, 142);
            this.cboCheckDt.Name = "cboCheckDt";
            this.cboCheckDt.Size = new System.Drawing.Size(64, 20);
            this.cboCheckDt.TabIndex = 22;
            this.cboCheckDt.SelectedIndexChanged += new System.EventHandler(this.cboCheckDt_SelectedIndexChanged);
            // 
            // cboRemoveDt
            // 
            this.cboRemoveDt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRemoveDt.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboRemoveDt.FormattingEnabled = true;
            this.cboRemoveDt.Location = new System.Drawing.Point(121, 299);
            this.cboRemoveDt.Name = "cboRemoveDt";
            this.cboRemoveDt.Size = new System.Drawing.Size(64, 20);
            this.cboRemoveDt.TabIndex = 21;
            this.cboRemoveDt.SelectedIndexChanged += new System.EventHandler(this.cboBuildDt_SelectedIndexChanged);
            // 
            // lbl_InputDate
            // 
            this.lbl_InputDate.AutoSize = true;
            this.lbl_InputDate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_InputDate.Location = new System.Drawing.Point(29, 302);
            this.lbl_InputDate.Name = "lbl_InputDate";
            this.lbl_InputDate.Size = new System.Drawing.Size(59, 12);
            this.lbl_InputDate.TabIndex = 20;
            this.lbl_InputDate.Text = "撤管时间:";
            // 
            // lbl_To
            // 
            this.lbl_To.AutoSize = true;
            this.lbl_To.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_To.Location = new System.Drawing.Point(325, 301);
            this.lbl_To.Name = "lbl_To";
            this.lbl_To.Size = new System.Drawing.Size(17, 12);
            this.lbl_To.TabIndex = 19;
            this.lbl_To.Text = "至";
            // 
            // dTPDateFrom
            // 
            this.dTPDateFrom.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dTPDateFrom.Location = new System.Drawing.Point(192, 298);
            this.dTPDateFrom.Name = "dTPDateFrom";
            this.dTPDateFrom.Size = new System.Drawing.Size(128, 21);
            this.dTPDateFrom.TabIndex = 18;
            this.dTPDateFrom.ValueChanged += new System.EventHandler(this.dTPDateFrom_ValueChanged);
            // 
            // dTPDateTo
            // 
            this.dTPDateTo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dTPDateTo.Location = new System.Drawing.Point(348, 298);
            this.dTPDateTo.Name = "dTPDateTo";
            this.dTPDateTo.Size = new System.Drawing.Size(125, 21);
            this.dTPDateTo.TabIndex = 17;
            this.dTPDateTo.ValueChanged += new System.EventHandler(this.dTPDateTo_ValueChanged);
            // 
            // rdBtn_PipeLenMons
            // 
            this.rdBtn_PipeLenMons.AutoSize = true;
            this.rdBtn_PipeLenMons.Font = new System.Drawing.Font("宋体", 9F);
            this.rdBtn_PipeLenMons.Location = new System.Drawing.Point(24, 270);
            this.rdBtn_PipeLenMons.Name = "rdBtn_PipeLenMons";
            this.rdBtn_PipeLenMons.Size = new System.Drawing.Size(143, 16);
            this.rdBtn_PipeLenMons.TabIndex = 16;
            this.rdBtn_PipeLenMons.Text = "撤除管线长度统计月报";
            this.rdBtn_PipeLenMons.UseVisualStyleBackColor = true;
            // 
            // optPipeInvestAddDetail
            // 
            this.optPipeInvestAddDetail.AutoSize = true;
            this.optPipeInvestAddDetail.Location = new System.Drawing.Point(238, 92);
            this.optPipeInvestAddDetail.Name = "optPipeInvestAddDetail";
            this.optPipeInvestAddDetail.Size = new System.Drawing.Size(131, 16);
            this.optPipeInvestAddDetail.TabIndex = 15;
            this.optPipeInvestAddDetail.TabStop = true;
            this.optPipeInvestAddDetail.Text = "固定资产增加明细表";
            this.optPipeInvestAddDetail.UseVisualStyleBackColor = true;
            // 
            // optPipeInvestAddTable
            // 
            this.optPipeInvestAddTable.AutoSize = true;
            this.optPipeInvestAddTable.Location = new System.Drawing.Point(25, 92);
            this.optPipeInvestAddTable.Name = "optPipeInvestAddTable";
            this.optPipeInvestAddTable.Size = new System.Drawing.Size(131, 16);
            this.optPipeInvestAddTable.TabIndex = 14;
            this.optPipeInvestAddTable.TabStop = true;
            this.optPipeInvestAddTable.Text = "固定资产增加汇总表";
            this.optPipeInvestAddTable.UseVisualStyleBackColor = true;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(324, 145);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(17, 12);
            this.lblTo.TabIndex = 13;
            this.lblTo.Text = "至";
            // 
            // dtpEndStateDate
            // 
            this.dtpEndStateDate.Location = new System.Drawing.Point(347, 142);
            this.dtpEndStateDate.Name = "dtpEndStateDate";
            this.dtpEndStateDate.Size = new System.Drawing.Size(126, 21);
            this.dtpEndStateDate.TabIndex = 12;
            this.dtpEndStateDate.ValueChanged += new System.EventHandler(this.dtpEndStateDate_ValueChanged);
            // 
            // dtpBegStateDate
            // 
            this.dtpBegStateDate.Location = new System.Drawing.Point(192, 142);
            this.dtpBegStateDate.Name = "dtpBegStateDate";
            this.dtpBegStateDate.Size = new System.Drawing.Size(126, 21);
            this.dtpBegStateDate.TabIndex = 11;
            this.dtpBegStateDate.ValueChanged += new System.EventHandler(this.dtpBegStateDate_ValueChanged);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(32, 145);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(59, 12);
            this.lblTime.TabIndex = 10;
            this.lblTime.Text = "统计时间:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 238);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(461, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "____________________________________________________________________________";
            // 
            // lblLine
            // 
            this.lblLine.AutoSize = true;
            this.lblLine.Location = new System.Drawing.Point(14, 113);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(461, 12);
            this.lblLine.TabIndex = 9;
            this.lblLine.Text = "____________________________________________________________________________";
            // 
            // optNewSWTPipeLenMonthStat
            // 
            this.optNewSWTPipeLenMonthStat.AutoSize = true;
            this.optNewSWTPipeLenMonthStat.Location = new System.Drawing.Point(237, 215);
            this.optNewSWTPipeLenMonthStat.Name = "optNewSWTPipeLenMonthStat";
            this.optNewSWTPipeLenMonthStat.Size = new System.Drawing.Size(167, 16);
            this.optNewSWTPipeLenMonthStat.TabIndex = 8;
            this.optNewSWTPipeLenMonthStat.TabStop = true;
            this.optNewSWTPipeLenMonthStat.Text = "新增输水管线长度统计月报";
            this.optNewSWTPipeLenMonthStat.UseVisualStyleBackColor = true;
            // 
            // optNewHydrantMonthStat
            // 
            this.optNewHydrantMonthStat.AutoSize = true;
            this.optNewHydrantMonthStat.Location = new System.Drawing.Point(237, 183);
            this.optNewHydrantMonthStat.Name = "optNewHydrantMonthStat";
            this.optNewHydrantMonthStat.Size = new System.Drawing.Size(179, 16);
            this.optNewHydrantMonthStat.TabIndex = 7;
            this.optNewHydrantMonthStat.TabStop = true;
            this.optNewHydrantMonthStat.Text = "XXXX年XX月新安消火栓统计表";
            this.optNewHydrantMonthStat.UseVisualStyleBackColor = true;
            // 
            // optNewWTPipeLenMonthStat
            // 
            this.optNewWTPipeLenMonthStat.AutoSize = true;
            this.optNewWTPipeLenMonthStat.Location = new System.Drawing.Point(24, 215);
            this.optNewWTPipeLenMonthStat.Name = "optNewWTPipeLenMonthStat";
            this.optNewWTPipeLenMonthStat.Size = new System.Drawing.Size(167, 16);
            this.optNewWTPipeLenMonthStat.TabIndex = 6;
            this.optNewWTPipeLenMonthStat.TabStop = true;
            this.optNewWTPipeLenMonthStat.Text = "新增配水管线长度统计月报";
            this.optNewWTPipeLenMonthStat.UseVisualStyleBackColor = true;
            // 
            // optNewPipeLenMonthStat
            // 
            this.optNewPipeLenMonthStat.AutoSize = true;
            this.optNewPipeLenMonthStat.Location = new System.Drawing.Point(24, 183);
            this.optNewPipeLenMonthStat.Name = "optNewPipeLenMonthStat";
            this.optNewPipeLenMonthStat.Size = new System.Drawing.Size(143, 16);
            this.optNewPipeLenMonthStat.TabIndex = 5;
            this.optNewPipeLenMonthStat.TabStop = true;
            this.optNewPipeLenMonthStat.Text = "新增管线长度统计月报";
            this.optNewPipeLenMonthStat.UseVisualStyleBackColor = true;
            // 
            // optPipeLenStat
            // 
            this.optPipeLenStat.AutoSize = true;
            this.optPipeLenStat.Location = new System.Drawing.Point(238, 61);
            this.optPipeLenStat.Name = "optPipeLenStat";
            this.optPipeLenStat.Size = new System.Drawing.Size(143, 16);
            this.optPipeLenStat.TabIndex = 4;
            this.optPipeLenStat.TabStop = true;
            this.optPipeLenStat.Text = "市区配水管线长度统计";
            this.optPipeLenStat.UseVisualStyleBackColor = true;
            // 
            // optValvestat
            // 
            this.optValvestat.AutoSize = true;
            this.optValvestat.Checked = true;
            this.optValvestat.Location = new System.Drawing.Point(25, 61);
            this.optValvestat.Name = "optValvestat";
            this.optValvestat.Size = new System.Drawing.Size(119, 16);
            this.optValvestat.TabIndex = 3;
            this.optValvestat.TabStop = true;
            this.optValvestat.Text = "配水管线闸门统计";
            this.optValvestat.UseVisualStyleBackColor = true;
            // 
            // cboWaterType
            // 
            this.cboWaterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWaterType.FormattingEnabled = true;
            this.cboWaterType.Location = new System.Drawing.Point(352, 24);
            this.cboWaterType.Name = "cboWaterType";
            this.cboWaterType.Size = new System.Drawing.Size(121, 20);
            this.cboWaterType.TabIndex = 2;
            // 
            // lblWaterType
            // 
            this.lblWaterType.AutoSize = true;
            this.lblWaterType.Location = new System.Drawing.Point(288, 27);
            this.lblWaterType.Name = "lblWaterType";
            this.lblWaterType.Size = new System.Drawing.Size(53, 12);
            this.lblWaterType.TabIndex = 1;
            this.lblWaterType.Text = "水质类型";
            // 
            // lblQSTJ
            // 
            this.lblQSTJ.AutoSize = true;
            this.lblQSTJ.Location = new System.Drawing.Point(27, 27);
            this.lblQSTJ.Name = "lblQSTJ";
            this.lblQSTJ.Size = new System.Drawing.Size(53, 12);
            this.lblQSTJ.TabIndex = 0;
            this.lblQSTJ.Text = "全市统计";
            // 
            // btnState
            // 
            this.btnState.Enabled = false;
            this.btnState.Location = new System.Drawing.Point(195, 375);
            this.btnState.Name = "btnState";
            this.btnState.Size = new System.Drawing.Size(90, 23);
            this.btnState.TabIndex = 1;
            this.btnState.Text = "统计报表(&S)";
            this.btnState.UseVisualStyleBackColor = true;
            this.btnState.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(413, 375);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "退  出(&Q)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnExprtExcel
            // 
            this.btnExprtExcel.Location = new System.Drawing.Point(302, 375);
            this.btnExprtExcel.Name = "btnExprtExcel";
            this.btnExprtExcel.Size = new System.Drawing.Size(90, 23);
            this.btnExprtExcel.TabIndex = 3;
            this.btnExprtExcel.Text = "导出Excel(&E)";
            this.btnExprtExcel.UseVisualStyleBackColor = true;
            this.btnExprtExcel.Click += new System.EventHandler(this.btnExprtExcel_Click);
            // 
            // FrmDoState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 405);
            this.Controls.Add(this.btnExprtExcel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnState);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmDoState";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "统计报表";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmReport_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpEndStateDate;
        private System.Windows.Forms.DateTimePicker dtpBegStateDate;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblLine;
        private System.Windows.Forms.RadioButton optNewSWTPipeLenMonthStat;
        private System.Windows.Forms.RadioButton optNewHydrantMonthStat;
        private System.Windows.Forms.RadioButton optNewWTPipeLenMonthStat;
        private System.Windows.Forms.RadioButton optNewPipeLenMonthStat;
        private System.Windows.Forms.RadioButton optPipeLenStat;
        private System.Windows.Forms.RadioButton optValvestat;
        private System.Windows.Forms.ComboBox cboWaterType;
        private System.Windows.Forms.Label lblWaterType;
        private System.Windows.Forms.Label lblQSTJ;
        private System.Windows.Forms.Button btnState;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnExprtExcel;
        private System.Windows.Forms.RadioButton optPipeInvestAddDetail;
        private System.Windows.Forms.RadioButton optPipeInvestAddTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdBtn_PipeLenMons;
        private System.Windows.Forms.ComboBox cboRemoveDt;
        private System.Windows.Forms.Label lbl_InputDate;
        private System.Windows.Forms.Label lbl_To;
        private System.Windows.Forms.DateTimePicker dTPDateFrom;
        private System.Windows.Forms.DateTimePicker dTPDateTo;
        private System.Windows.Forms.ComboBox cboCheckDt;
    }
}