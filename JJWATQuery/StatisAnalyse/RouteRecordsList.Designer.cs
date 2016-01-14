namespace JJWATQuery
{
    partial class RouteRecordsList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RouteRecordsList));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbValveType = new System.Windows.Forms.ComboBox();
            this.rdoBtnFluxMeasure = new System.Windows.Forms.RadioButton();
            this.rdobtnValve = new System.Windows.Forms.RadioButton();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.rdoBtnGasValve = new System.Windows.Forms.RadioButton();
            this.rdobtnBaseWell = new System.Windows.Forms.RadioButton();
            this.rdobtnMudValve = new System.Windows.Forms.RadioButton();
            this.rdobtnHydrant = new System.Windows.Forms.RadioButton();
            this.btnTracing = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tfhList = new System.Windows.Forms.ListBox();
            this.chkBoxAll = new System.Windows.Forms.CheckBox();
            this.lbl_InputTFH = new System.Windows.Forms.Label();
            this.txtBtfh = new System.Windows.Forms.TextBox();
            this.rdoBtn10000 = new System.Windows.Forms.RadioButton();
            this.rdoBtn2000 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbValveType);
            this.groupBox1.Controls.Add(this.rdoBtnFluxMeasure);
            this.groupBox1.Controls.Add(this.rdobtnValve);
            this.groupBox1.Controls.Add(this.radioButton7);
            this.groupBox1.Controls.Add(this.rdoBtnGasValve);
            this.groupBox1.Controls.Add(this.rdobtnBaseWell);
            this.groupBox1.Controls.Add(this.rdobtnMudValve);
            this.groupBox1.Controls.Add(this.rdobtnHydrant);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(427, 139);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择记录";
            // 
            // cmbValveType
            // 
            this.cmbValveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbValveType.FormattingEnabled = true;
            this.cmbValveType.Items.AddRange(new object[] {
            "路闸",
            "户闸"});
            this.cmbValveType.Location = new System.Drawing.Point(96, 30);
            this.cmbValveType.Name = "cmbValveType";
            this.cmbValveType.Size = new System.Drawing.Size(60, 22);
            this.cmbValveType.TabIndex = 7;
            // 
            // rdoBtnFluxMeasure
            // 
            this.rdoBtnFluxMeasure.AutoSize = true;
            this.rdoBtnFluxMeasure.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoBtnFluxMeasure.Location = new System.Drawing.Point(192, 67);
            this.rdoBtnFluxMeasure.Name = "rdoBtnFluxMeasure";
            this.rdoBtnFluxMeasure.Size = new System.Drawing.Size(70, 18);
            this.rdoBtnFluxMeasure.TabIndex = 4;
            this.rdoBtnFluxMeasure.Text = "测流点";
            this.rdoBtnFluxMeasure.UseVisualStyleBackColor = true;
            this.rdoBtnFluxMeasure.CheckedChanged += new System.EventHandler(this.rdoBtnFluxMeasure_CheckedChanged);
            // 
            // rdobtnValve
            // 
            this.rdobtnValve.AutoSize = true;
            this.rdobtnValve.Checked = true;
            this.rdobtnValve.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdobtnValve.Location = new System.Drawing.Point(25, 30);
            this.rdobtnValve.Name = "rdobtnValve";
            this.rdobtnValve.Size = new System.Drawing.Size(71, 18);
            this.rdobtnValve.TabIndex = 0;
            this.rdobtnValve.TabStop = true;
            this.rdobtnValve.Text = "闸  门";
            this.rdobtnValve.UseVisualStyleBackColor = true;
            this.rdobtnValve.CheckedChanged += new System.EventHandler(this.rdobtnValve_CheckedChanged);
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton7.Location = new System.Drawing.Point(324, 30);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(70, 18);
            this.radioButton7.TabIndex = 6;
            this.radioButton7.Text = "测压点";
            this.radioButton7.UseVisualStyleBackColor = true;
            this.radioButton7.CheckedChanged += new System.EventHandler(this.radioButton7_CheckedChanged);
            // 
            // rdoBtnGasValve
            // 
            this.rdoBtnGasValve.AutoSize = true;
            this.rdoBtnGasValve.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoBtnGasValve.Location = new System.Drawing.Point(25, 102);
            this.rdoBtnGasValve.Name = "rdoBtnGasValve";
            this.rdoBtnGasValve.Size = new System.Drawing.Size(85, 18);
            this.rdoBtnGasValve.TabIndex = 2;
            this.rdoBtnGasValve.Text = "排气阀门";
            this.rdoBtnGasValve.UseVisualStyleBackColor = true;
            this.rdoBtnGasValve.CheckedChanged += new System.EventHandler(this.rdoBtnGasValve_CheckedChanged);
            // 
            // rdobtnBaseWell
            // 
            this.rdobtnBaseWell.AutoSize = true;
            this.rdobtnBaseWell.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdobtnBaseWell.Location = new System.Drawing.Point(192, 31);
            this.rdobtnBaseWell.Name = "rdobtnBaseWell";
            this.rdobtnBaseWell.Size = new System.Drawing.Size(70, 18);
            this.rdobtnBaseWell.TabIndex = 3;
            this.rdobtnBaseWell.Text = "检查井";
            this.rdobtnBaseWell.UseVisualStyleBackColor = true;
            this.rdobtnBaseWell.CheckedChanged += new System.EventHandler(this.rdobtnBaseWell_CheckedChanged);
            // 
            // rdobtnMudValve
            // 
            this.rdobtnMudValve.AutoSize = true;
            this.rdobtnMudValve.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdobtnMudValve.Location = new System.Drawing.Point(192, 103);
            this.rdobtnMudValve.Name = "rdobtnMudValve";
            this.rdobtnMudValve.Size = new System.Drawing.Size(85, 18);
            this.rdobtnMudValve.TabIndex = 5;
            this.rdobtnMudValve.Text = "排泥阀门";
            this.rdobtnMudValve.UseVisualStyleBackColor = true;
            this.rdobtnMudValve.CheckedChanged += new System.EventHandler(this.rdobtnMudValve_CheckedChanged);
            // 
            // rdobtnHydrant
            // 
            this.rdobtnHydrant.AutoSize = true;
            this.rdobtnHydrant.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdobtnHydrant.Location = new System.Drawing.Point(25, 66);
            this.rdobtnHydrant.Name = "rdobtnHydrant";
            this.rdobtnHydrant.Size = new System.Drawing.Size(70, 18);
            this.rdobtnHydrant.TabIndex = 1;
            this.rdobtnHydrant.Text = "消火栓";
            this.rdobtnHydrant.UseVisualStyleBackColor = true;
            this.rdobtnHydrant.CheckedChanged += new System.EventHandler(this.rdobtnHydrant_CheckedChanged);
            // 
            // btnTracing
            // 
            this.btnTracing.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTracing.Location = new System.Drawing.Point(191, 8);
            this.btnTracing.Name = "btnTracing";
            this.btnTracing.Size = new System.Drawing.Size(102, 25);
            this.btnTracing.TabIndex = 7;
            this.btnTracing.Text = "打印路由(&T)";
            this.btnTracing.UseVisualStyleBackColor = true;
            this.btnTracing.Click += new System.EventHandler(this.btnTracing_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(427, 309);
            this.splitContainer1.SplitterDistance = 139;
            this.splitContainer1.TabIndex = 7;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tfhList);
            this.groupBox2.Controls.Add(this.chkBoxAll);
            this.groupBox2.Controls.Add(this.lbl_InputTFH);
            this.groupBox2.Controls.Add(this.txtBtfh);
            this.groupBox2.Controls.Add(this.rdoBtn10000);
            this.groupBox2.Controls.Add(this.rdoBtn2000);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(427, 166);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "图幅选择";
            // 
            // tfhList
            // 
            this.tfhList.BackColor = System.Drawing.SystemColors.Control;
            this.tfhList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tfhList.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tfhList.FormattingEnabled = true;
            this.tfhList.ItemHeight = 14;
            this.tfhList.Items.AddRange(new object[] {
            ""});
            this.tfhList.Location = new System.Drawing.Point(114, 81);
            this.tfhList.Name = "tfhList";
            this.tfhList.Size = new System.Drawing.Size(156, 70);
            this.tfhList.TabIndex = 7;
            this.tfhList.SelectedIndexChanged += new System.EventHandler(this.tfhList_SelectedIndexChanged);
            // 
            // chkBoxAll
            // 
            this.chkBoxAll.AutoSize = true;
            this.chkBoxAll.Location = new System.Drawing.Point(276, 61);
            this.chkBoxAll.Name = "chkBoxAll";
            this.chkBoxAll.Size = new System.Drawing.Size(56, 18);
            this.chkBoxAll.TabIndex = 6;
            this.chkBoxAll.Text = "全部";
            this.chkBoxAll.UseVisualStyleBackColor = true;
            this.chkBoxAll.CheckedChanged += new System.EventHandler(this.chkBoxAll_CheckedChanged);
            // 
            // lbl_InputTFH
            // 
            this.lbl_InputTFH.AutoSize = true;
            this.lbl_InputTFH.Location = new System.Drawing.Point(48, 62);
            this.lbl_InputTFH.Name = "lbl_InputTFH";
            this.lbl_InputTFH.Size = new System.Drawing.Size(60, 14);
            this.lbl_InputTFH.TabIndex = 5;
            this.lbl_InputTFH.Text = "图幅号:";
            // 
            // txtBtfh
            // 
            this.txtBtfh.Location = new System.Drawing.Point(113, 59);
            this.txtBtfh.Name = "txtBtfh";
            this.txtBtfh.Size = new System.Drawing.Size(157, 23);
            this.txtBtfh.TabIndex = 2;
            this.txtBtfh.TextChanged += new System.EventHandler(this.txtBtfh_TextChanged);
            // 
            // rdoBtn10000
            // 
            this.rdoBtn10000.AutoSize = true;
            this.rdoBtn10000.Enabled = false;
            this.rdoBtn10000.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoBtn10000.Location = new System.Drawing.Point(193, 21);
            this.rdoBtn10000.Name = "rdoBtn10000";
            this.rdoBtn10000.Size = new System.Drawing.Size(71, 20);
            this.rdoBtn10000.TabIndex = 1;
            this.rdoBtn10000.Text = "10000";
            this.rdoBtn10000.UseVisualStyleBackColor = true;
            this.rdoBtn10000.CheckedChanged += new System.EventHandler(this.rdoBtn10000_CheckedChanged);
            // 
            // rdoBtn2000
            // 
            this.rdoBtn2000.AutoSize = true;
            this.rdoBtn2000.Checked = true;
            this.rdoBtn2000.Enabled = false;
            this.rdoBtn2000.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoBtn2000.Location = new System.Drawing.Point(26, 24);
            this.rdoBtn2000.Name = "rdoBtn2000";
            this.rdoBtn2000.Size = new System.Drawing.Size(62, 20);
            this.rdoBtn2000.TabIndex = 0;
            this.rdoBtn2000.TabStop = true;
            this.rdoBtn2000.Text = "2000";
            this.rdoBtn2000.UseVisualStyleBackColor = true;
            this.rdoBtn2000.CheckedChanged += new System.EventHandler(this.rdoBtn2000_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnTracing);
            this.panel1.Location = new System.Drawing.Point(0, 315);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(427, 39);
            this.panel1.TabIndex = 8;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Location = new System.Drawing.Point(330, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 25);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "退出(&Q)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // RouteRecordsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 358);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "RouteRecordsList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "打印路由";
            this.Load += new System.EventHandler(this.RouteRecordsList_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.RadioButton rdoBtnFluxMeasure;
        private System.Windows.Forms.RadioButton rdobtnMudValve;
        private System.Windows.Forms.RadioButton rdoBtnGasValve;
        private System.Windows.Forms.RadioButton rdobtnBaseWell;
        private System.Windows.Forms.RadioButton rdobtnHydrant;
        private System.Windows.Forms.RadioButton rdobtnValve;
        private System.Windows.Forms.Button btnTracing;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtBtfh;
        private System.Windows.Forms.RadioButton rdoBtn10000;
        private System.Windows.Forms.RadioButton rdoBtn2000;
        private System.Windows.Forms.CheckBox chkBoxAll;
        private System.Windows.Forms.Label lbl_InputTFH;
        private System.Windows.Forms.ListBox tfhList;
        private System.Windows.Forms.ComboBox cmbValveType;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
    }
}