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
            this.txtB_Remark = new System.Windows.Forms.TextBox();
            this.lblQSTJ = new System.Windows.Forms.Label();
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
            this.optNewSWTPipeLenMonthStat = new System.Windows.Forms.RadioButton();
            this.optNewHydrantMonthStat = new System.Windows.Forms.RadioButton();
            this.optNewWTPipeLenMonthStat = new System.Windows.Forms.RadioButton();
            this.optNewPipeLenMonthStat = new System.Windows.Forms.RadioButton();
            this.optPipeLenStat = new System.Windows.Forms.RadioButton();
            this.optValvestat = new System.Windows.Forms.RadioButton();
            this.cboWaterType = new System.Windows.Forms.ComboBox();
            this.lblWaterType = new System.Windows.Forms.Label();
            this.btnState = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnExprtExcel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.cboCheckDt4 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpBegStateDate4 = new System.Windows.Forms.DateTimePicker();
            this.dtpEndStateDate4 = new System.Windows.Forms.DateTimePicker();
            this.lblTo4 = new System.Windows.Forms.Label();
            this.rdBtn_GWAnnulReport = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtB_Remark);
            this.groupBox1.Controls.Add(this.lblQSTJ);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(479, 386);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "备忘";
            // 
            // txtB_Remark
            // 
            this.txtB_Remark.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtB_Remark.Location = new System.Drawing.Point(13, 49);
            this.txtB_Remark.Multiline = true;
            this.txtB_Remark.Name = "txtB_Remark";
            this.txtB_Remark.ReadOnly = true;
            this.txtB_Remark.Size = new System.Drawing.Size(454, 315);
            this.txtB_Remark.TabIndex = 9;
            this.txtB_Remark.TabStop = false;
            this.txtB_Remark.Text = resources.GetString("txtB_Remark.Text");
            // 
            // lblQSTJ
            // 
            this.lblQSTJ.AutoSize = true;
            this.lblQSTJ.Location = new System.Drawing.Point(10, 23);
            this.lblQSTJ.Name = "lblQSTJ";
            this.lblQSTJ.Size = new System.Drawing.Size(119, 12);
            this.lblQSTJ.TabIndex = 0;
            this.lblQSTJ.Text = "关于统计条件的说明:";
            // 
            // cboCheckDt
            // 
            this.cboCheckDt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCheckDt.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboCheckDt.FormattingEnabled = true;
            this.cboCheckDt.Location = new System.Drawing.Point(21, 142);
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
            this.cboRemoveDt.Location = new System.Drawing.Point(21, 142);
            this.cboRemoveDt.Name = "cboRemoveDt";
            this.cboRemoveDt.Size = new System.Drawing.Size(64, 20);
            this.cboRemoveDt.TabIndex = 21;
            this.cboRemoveDt.SelectedIndexChanged += new System.EventHandler(this.cboBuildDt_SelectedIndexChanged);
            // 
            // lbl_InputDate
            // 
            this.lbl_InputDate.AutoSize = true;
            this.lbl_InputDate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_InputDate.Location = new System.Drawing.Point(20, 114);
            this.lbl_InputDate.Name = "lbl_InputDate";
            this.lbl_InputDate.Size = new System.Drawing.Size(59, 12);
            this.lbl_InputDate.TabIndex = 20;
            this.lbl_InputDate.Text = "撤管时间:";
            // 
            // lbl_To
            // 
            this.lbl_To.AutoSize = true;
            this.lbl_To.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_To.Location = new System.Drawing.Point(223, 146);
            this.lbl_To.Name = "lbl_To";
            this.lbl_To.Size = new System.Drawing.Size(17, 12);
            this.lbl_To.TabIndex = 19;
            this.lbl_To.Text = "至";
            // 
            // dTPDateFrom
            // 
            this.dTPDateFrom.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dTPDateFrom.Location = new System.Drawing.Point(92, 142);
            this.dTPDateFrom.Name = "dTPDateFrom";
            this.dTPDateFrom.Size = new System.Drawing.Size(128, 21);
            this.dTPDateFrom.TabIndex = 18;
            this.dTPDateFrom.ValueChanged += new System.EventHandler(this.dTPDateFrom_ValueChanged);
            // 
            // dTPDateTo
            // 
            this.dTPDateTo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dTPDateTo.Location = new System.Drawing.Point(243, 142);
            this.dTPDateTo.Name = "dTPDateTo";
            this.dTPDateTo.Size = new System.Drawing.Size(125, 21);
            this.dTPDateTo.TabIndex = 17;
            this.dTPDateTo.ValueChanged += new System.EventHandler(this.dTPDateTo_ValueChanged);
            // 
            // rdBtn_PipeLenMons
            // 
            this.rdBtn_PipeLenMons.AutoSize = true;
            this.rdBtn_PipeLenMons.Checked = true;
            this.rdBtn_PipeLenMons.Font = new System.Drawing.Font("宋体", 9F);
            this.rdBtn_PipeLenMons.Location = new System.Drawing.Point(19, 28);
            this.rdBtn_PipeLenMons.Name = "rdBtn_PipeLenMons";
            this.rdBtn_PipeLenMons.Size = new System.Drawing.Size(143, 16);
            this.rdBtn_PipeLenMons.TabIndex = 16;
            this.rdBtn_PipeLenMons.TabStop = true;
            this.rdBtn_PipeLenMons.Text = "撤除管线长度统计月报";
            this.rdBtn_PipeLenMons.UseVisualStyleBackColor = true;
            // 
            // optPipeInvestAddDetail
            // 
            this.optPipeInvestAddDetail.AutoSize = true;
            this.optPipeInvestAddDetail.Location = new System.Drawing.Point(208, 59);
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
            this.optPipeInvestAddTable.Location = new System.Drawing.Point(19, 59);
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
            this.lblTo.Location = new System.Drawing.Point(223, 146);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(17, 12);
            this.lblTo.TabIndex = 13;
            this.lblTo.Text = "至";
            // 
            // dtpEndStateDate
            // 
            this.dtpEndStateDate.Location = new System.Drawing.Point(243, 142);
            this.dtpEndStateDate.Name = "dtpEndStateDate";
            this.dtpEndStateDate.Size = new System.Drawing.Size(126, 21);
            this.dtpEndStateDate.TabIndex = 12;
            this.dtpEndStateDate.ValueChanged += new System.EventHandler(this.dtpEndStateDate_ValueChanged);
            // 
            // dtpBegStateDate
            // 
            this.dtpBegStateDate.Location = new System.Drawing.Point(92, 142);
            this.dtpBegStateDate.Name = "dtpBegStateDate";
            this.dtpBegStateDate.Size = new System.Drawing.Size(128, 21);
            this.dtpBegStateDate.TabIndex = 11;
            this.dtpBegStateDate.ValueChanged += new System.EventHandler(this.dtpBegStateDate_ValueChanged);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(20, 114);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(59, 12);
            this.lblTime.TabIndex = 10;
            this.lblTime.Text = "统计时间:";
            // 
            // optNewSWTPipeLenMonthStat
            // 
            this.optNewSWTPipeLenMonthStat.AutoSize = true;
            this.optNewSWTPipeLenMonthStat.Location = new System.Drawing.Point(208, 59);
            this.optNewSWTPipeLenMonthStat.Name = "optNewSWTPipeLenMonthStat";
            this.optNewSWTPipeLenMonthStat.Size = new System.Drawing.Size(167, 16);
            this.optNewSWTPipeLenMonthStat.TabIndex = 8;
            this.optNewSWTPipeLenMonthStat.Text = "新增输水管线长度统计月报";
            this.optNewSWTPipeLenMonthStat.UseVisualStyleBackColor = true;
            // 
            // optNewHydrantMonthStat
            // 
            this.optNewHydrantMonthStat.AutoSize = true;
            this.optNewHydrantMonthStat.Location = new System.Drawing.Point(207, 28);
            this.optNewHydrantMonthStat.Name = "optNewHydrantMonthStat";
            this.optNewHydrantMonthStat.Size = new System.Drawing.Size(179, 16);
            this.optNewHydrantMonthStat.TabIndex = 7;
            this.optNewHydrantMonthStat.Text = "XXXX年XX月新安消火栓统计表";
            this.optNewHydrantMonthStat.UseVisualStyleBackColor = true;
            // 
            // optNewWTPipeLenMonthStat
            // 
            this.optNewWTPipeLenMonthStat.AutoSize = true;
            this.optNewWTPipeLenMonthStat.Location = new System.Drawing.Point(19, 59);
            this.optNewWTPipeLenMonthStat.Name = "optNewWTPipeLenMonthStat";
            this.optNewWTPipeLenMonthStat.Size = new System.Drawing.Size(167, 16);
            this.optNewWTPipeLenMonthStat.TabIndex = 6;
            this.optNewWTPipeLenMonthStat.Text = "新增配水管线长度统计月报";
            this.optNewWTPipeLenMonthStat.UseVisualStyleBackColor = true;
            // 
            // optNewPipeLenMonthStat
            // 
            this.optNewPipeLenMonthStat.AutoSize = true;
            this.optNewPipeLenMonthStat.Checked = true;
            this.optNewPipeLenMonthStat.Location = new System.Drawing.Point(19, 28);
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
            this.optPipeLenStat.Location = new System.Drawing.Point(207, 28);
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
            this.optValvestat.Location = new System.Drawing.Point(19, 28);
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
            this.cboWaterType.Location = new System.Drawing.Point(63, 347);
            this.cboWaterType.Name = "cboWaterType";
            this.cboWaterType.Size = new System.Drawing.Size(86, 20);
            this.cboWaterType.TabIndex = 2;
            // 
            // lblWaterType
            // 
            this.lblWaterType.AutoSize = true;
            this.lblWaterType.Location = new System.Drawing.Point(7, 351);
            this.lblWaterType.Name = "lblWaterType";
            this.lblWaterType.Size = new System.Drawing.Size(53, 12);
            this.lblWaterType.TabIndex = 1;
            this.lblWaterType.Text = "水质类型";
            // 
            // btnState
            // 
            this.btnState.Enabled = false;
            this.btnState.Location = new System.Drawing.Point(646, 404);
            this.btnState.Name = "btnState";
            this.btnState.Size = new System.Drawing.Size(90, 23);
            this.btnState.TabIndex = 1;
            this.btnState.Text = "统计报表(&S)";
            this.btnState.UseVisualStyleBackColor = true;
            this.btnState.Visible = false;
            this.btnState.Click += new System.EventHandler(this.btnState_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(838, 404);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "退  出(&Q)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnExprtExcel
            // 
            this.btnExprtExcel.Location = new System.Drawing.Point(742, 404);
            this.btnExprtExcel.Name = "btnExprtExcel";
            this.btnExprtExcel.Size = new System.Drawing.Size(90, 23);
            this.btnExprtExcel.TabIndex = 3;
            this.btnExprtExcel.Text = "导出Excel(&E)";
            this.btnExprtExcel.UseVisualStyleBackColor = true;
            this.btnExprtExcel.Click += new System.EventHandler(this.btnExprtExcel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tabControl1);
            this.groupBox2.Controls.Add(this.lblWaterType);
            this.groupBox2.Controls.Add(this.cboWaterType);
            this.groupBox2.Location = new System.Drawing.Point(497, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(431, 385);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "统计内容选择";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(6, 26);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(409, 302);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.optValvestat);
            this.tabPage1.Controls.Add(this.optPipeLenStat);
            this.tabPage1.Controls.Add(this.optPipeInvestAddTable);
            this.tabPage1.Controls.Add(this.optPipeInvestAddDetail);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(401, 276);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "全市统计";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.cboCheckDt);
            this.tabPage2.Controls.Add(this.optNewHydrantMonthStat);
            this.tabPage2.Controls.Add(this.optNewPipeLenMonthStat);
            this.tabPage2.Controls.Add(this.optNewWTPipeLenMonthStat);
            this.tabPage2.Controls.Add(this.optNewSWTPipeLenMonthStat);
            this.tabPage2.Controls.Add(this.lblTime);
            this.tabPage2.Controls.Add(this.dtpBegStateDate);
            this.tabPage2.Controls.Add(this.dtpEndStateDate);
            this.tabPage2.Controls.Add(this.lblTo);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(401, 276);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "新增统计";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.cboRemoveDt);
            this.tabPage3.Controls.Add(this.rdBtn_PipeLenMons);
            this.tabPage3.Controls.Add(this.dTPDateFrom);
            this.tabPage3.Controls.Add(this.lbl_InputDate);
            this.tabPage3.Controls.Add(this.dTPDateTo);
            this.tabPage3.Controls.Add(this.lbl_To);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(401, 276);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "撤除统计";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.cboCheckDt4);
            this.tabPage4.Controls.Add(this.label2);
            this.tabPage4.Controls.Add(this.dtpBegStateDate4);
            this.tabPage4.Controls.Add(this.dtpEndStateDate4);
            this.tabPage4.Controls.Add(this.lblTo4);
            this.tabPage4.Controls.Add(this.rdBtn_GWAnnulReport);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(401, 276);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "年报";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // cboCheckDt4
            // 
            this.cboCheckDt4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCheckDt4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboCheckDt4.FormattingEnabled = true;
            this.cboCheckDt4.Location = new System.Drawing.Point(21, 142);
            this.cboCheckDt4.Name = "cboCheckDt4";
            this.cboCheckDt4.Size = new System.Drawing.Size(64, 20);
            this.cboCheckDt4.TabIndex = 27;
            this.cboCheckDt4.SelectedIndexChanged += new System.EventHandler(this.cboCheckDt4_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 23;
            this.label2.Text = "统计时间:";
            // 
            // dtpBegStateDate4
            // 
            this.dtpBegStateDate4.Location = new System.Drawing.Point(92, 142);
            this.dtpBegStateDate4.Name = "dtpBegStateDate4";
            this.dtpBegStateDate4.Size = new System.Drawing.Size(128, 21);
            this.dtpBegStateDate4.TabIndex = 24;
            this.dtpBegStateDate4.ValueChanged += new System.EventHandler(this.dtpBegStateDate4_ValueChanged);
            // 
            // dtpEndStateDate4
            // 
            this.dtpEndStateDate4.Location = new System.Drawing.Point(243, 142);
            this.dtpEndStateDate4.Name = "dtpEndStateDate4";
            this.dtpEndStateDate4.Size = new System.Drawing.Size(126, 21);
            this.dtpEndStateDate4.TabIndex = 25;
            this.dtpEndStateDate4.ValueChanged += new System.EventHandler(this.dtpEndStateDate4_ValueChanged);
            // 
            // lblTo4
            // 
            this.lblTo4.AutoSize = true;
            this.lblTo4.Location = new System.Drawing.Point(223, 146);
            this.lblTo4.Name = "lblTo4";
            this.lblTo4.Size = new System.Drawing.Size(17, 12);
            this.lblTo4.TabIndex = 26;
            this.lblTo4.Text = "至";
            // 
            // rdBtn_GWAnnulReport
            // 
            this.rdBtn_GWAnnulReport.AutoSize = true;
            this.rdBtn_GWAnnulReport.Checked = true;
            this.rdBtn_GWAnnulReport.Location = new System.Drawing.Point(19, 28);
            this.rdBtn_GWAnnulReport.Name = "rdBtn_GWAnnulReport";
            this.rdBtn_GWAnnulReport.Size = new System.Drawing.Size(143, 16);
            this.rdBtn_GWAnnulReport.TabIndex = 0;
            this.rdBtn_GWAnnulReport.TabStop = true;
            this.rdBtn_GWAnnulReport.Text = "管网管线长度统计年报";
            this.rdBtn_GWAnnulReport.UseVisualStyleBackColor = true;
            // 
            // FrmDoState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 436);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnExprtExcel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnState);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmDoState";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "统计报表";
            this.Load += new System.EventHandler(this.FrmReport_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpEndStateDate;
        private System.Windows.Forms.DateTimePicker dtpBegStateDate;
        private System.Windows.Forms.Label lblTime;
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
        private System.Windows.Forms.RadioButton rdBtn_PipeLenMons;
        private System.Windows.Forms.ComboBox cboRemoveDt;
        private System.Windows.Forms.Label lbl_InputDate;
        private System.Windows.Forms.Label lbl_To;
        private System.Windows.Forms.DateTimePicker dTPDateFrom;
        private System.Windows.Forms.DateTimePicker dTPDateTo;
        private System.Windows.Forms.ComboBox cboCheckDt;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.RadioButton rdBtn_GWAnnulReport;
        private System.Windows.Forms.ComboBox cboCheckDt4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpBegStateDate4;
        private System.Windows.Forms.DateTimePicker dtpEndStateDate4;
        private System.Windows.Forms.Label lblTo4;
        private System.Windows.Forms.TextBox txtB_Remark;
    }
}