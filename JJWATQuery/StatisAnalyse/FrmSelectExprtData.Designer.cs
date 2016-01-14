namespace JJWATQuery.StatisAnalyse
{
    partial class FrmSelectExprtData
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbToData = new System.Windows.Forms.ComboBox();
            this.lbl_To = new System.Windows.Forms.Label();
            this.cmbFromData = new System.Windows.Forms.ComboBox();
            this.cmbBoxCond = new System.Windows.Forms.ComboBox();
            this.rdBtn_Selecting = new System.Windows.Forms.RadioButton();
            this.rdBtn_All = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbToData);
            this.groupBox1.Controls.Add(this.lbl_To);
            this.groupBox1.Controls.Add(this.cmbFromData);
            this.groupBox1.Controls.Add(this.cmbBoxCond);
            this.groupBox1.Controls.Add(this.rdBtn_Selecting);
            this.groupBox1.Controls.Add(this.rdBtn_All);
            this.groupBox1.Location = new System.Drawing.Point(7, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(385, 89);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cmbToData
            // 
            this.cmbToData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbToData.FormattingEnabled = true;
            this.cmbToData.Location = new System.Drawing.Point(312, 51);
            this.cmbToData.Name = "cmbToData";
            this.cmbToData.Size = new System.Drawing.Size(61, 20);
            this.cmbToData.TabIndex = 5;
            this.cmbToData.SelectedIndexChanged += new System.EventHandler(this.cmbToData_SelectedIndexChanged);
            // 
            // lbl_To
            // 
            this.lbl_To.AutoSize = true;
            this.lbl_To.Location = new System.Drawing.Point(291, 54);
            this.lbl_To.Name = "lbl_To";
            this.lbl_To.Size = new System.Drawing.Size(17, 12);
            this.lbl_To.TabIndex = 4;
            this.lbl_To.Text = "至";
            // 
            // cmbFromData
            // 
            this.cmbFromData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFromData.FormattingEnabled = true;
            this.cmbFromData.Location = new System.Drawing.Point(227, 51);
            this.cmbFromData.Name = "cmbFromData";
            this.cmbFromData.Size = new System.Drawing.Size(59, 20);
            this.cmbFromData.TabIndex = 3;
            this.cmbFromData.SelectedIndexChanged += new System.EventHandler(this.cmbFromData_SelectedIndexChanged);
            // 
            // cmbBoxCond
            // 
            this.cmbBoxCond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxCond.FormattingEnabled = true;
            this.cmbBoxCond.Location = new System.Drawing.Point(141, 51);
            this.cmbBoxCond.Name = "cmbBoxCond";
            this.cmbBoxCond.Size = new System.Drawing.Size(79, 20);
            this.cmbBoxCond.TabIndex = 2;
            this.cmbBoxCond.SelectedIndexChanged += new System.EventHandler(this.cmbBoxCond_SelectedIndexChanged);
            // 
            // rdBtn_Selecting
            // 
            this.rdBtn_Selecting.AutoSize = true;
            this.rdBtn_Selecting.Location = new System.Drawing.Point(23, 52);
            this.rdBtn_Selecting.Name = "rdBtn_Selecting";
            this.rdBtn_Selecting.Size = new System.Drawing.Size(119, 16);
            this.rdBtn_Selecting.TabIndex = 1;
            this.rdBtn_Selecting.Text = "只导出个数或长度";
            this.rdBtn_Selecting.UseVisualStyleBackColor = true;
            this.rdBtn_Selecting.CheckedChanged += new System.EventHandler(this.rdBtn_Selecting_CheckedChanged);
            // 
            // rdBtn_All
            // 
            this.rdBtn_All.AutoSize = true;
            this.rdBtn_All.Checked = true;
            this.rdBtn_All.Location = new System.Drawing.Point(23, 27);
            this.rdBtn_All.Name = "rdBtn_All";
            this.rdBtn_All.Size = new System.Drawing.Size(71, 16);
            this.rdBtn_All.TabIndex = 0;
            this.rdBtn_All.TabStop = true;
            this.rdBtn_All.Text = "所有数据";
            this.rdBtn_All.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(209, 98);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 24);
            this.button1.TabIndex = 1;
            this.button1.Text = "确定(&O)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(308, 98);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 24);
            this.button2.TabIndex = 2;
            this.button2.Text = "取消(&C)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FrmSelectExprtData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 129);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSelectExprtData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "导出数据";
            this.Load += new System.EventHandler(this.FrmSelectExprtData_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbFromData;
        private System.Windows.Forms.ComboBox cmbBoxCond;
        private System.Windows.Forms.RadioButton rdBtn_Selecting;
        private System.Windows.Forms.RadioButton rdBtn_All;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lbl_To;
        private System.Windows.Forms.ComboBox cmbToData;
    }
}