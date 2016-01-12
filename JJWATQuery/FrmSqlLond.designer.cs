namespace JJWATQuery
{
    partial class FrmSqlLond
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSqlLond));
            this.tbBZ = new System.Windows.Forms.TextBox();
            this.lblBZ = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblRecord = new System.Windows.Forms.Label();
            this.cbMS = new System.Windows.Forms.ComboBox();
            this.tbSX = new System.Windows.Forms.TextBox();
            this.lblSQL = new System.Windows.Forms.Label();
            this.tbSQL = new System.Windows.Forms.TextBox();
            this.btnSX = new System.Windows.Forms.Button();
            this.lbSX = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbBZ
            // 
            this.tbBZ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBZ.Enabled = false;
            this.tbBZ.Location = new System.Drawing.Point(59, 167);
            this.tbBZ.Multiline = true;
            this.tbBZ.Name = "tbBZ";
            this.tbBZ.Size = new System.Drawing.Size(197, 87);
            this.tbBZ.TabIndex = 45;
            // 
            // lblBZ
            // 
            this.lblBZ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBZ.AutoSize = true;
            this.lblBZ.Location = new System.Drawing.Point(12, 167);
            this.lblBZ.Name = "lblBZ";
            this.lblBZ.Size = new System.Drawing.Size(41, 12);
            this.lblBZ.TabIndex = 44;
            this.lblBZ.Text = "备注：";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(200, 265);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 23);
            this.btnClose.TabIndex = 43;
            this.btnClose.Text = "退出(&Q)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(135, 265);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(59, 23);
            this.btnOK.TabIndex = 42;
            this.btnOK.Text = "确定(&Y)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblRecord
            // 
            this.lblRecord.AutoSize = true;
            this.lblRecord.Location = new System.Drawing.Point(12, 10);
            this.lblRecord.Name = "lblRecord";
            this.lblRecord.Size = new System.Drawing.Size(41, 12);
            this.lblRecord.TabIndex = 40;
            this.lblRecord.Text = "描述：";
            // 
            // cbMS
            // 
            this.cbMS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbMS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMS.FormattingEnabled = true;
            this.cbMS.Location = new System.Drawing.Point(59, 7);
            this.cbMS.Name = "cbMS";
            this.cbMS.Size = new System.Drawing.Size(197, 20);
            this.cbMS.TabIndex = 46;
            this.cbMS.SelectedIndexChanged += new System.EventHandler(this.cbMS_SelectedIndexChanged);
            // 
            // tbSX
            // 
            this.tbSX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSX.Location = new System.Drawing.Point(59, 33);
            this.tbSX.Name = "tbSX";
            this.tbSX.Size = new System.Drawing.Size(135, 21);
            this.tbSX.TabIndex = 47;
            // 
            // lblSQL
            // 
            this.lblSQL.AutoSize = true;
            this.lblSQL.Location = new System.Drawing.Point(12, 61);
            this.lblSQL.Name = "lblSQL";
            this.lblSQL.Size = new System.Drawing.Size(41, 12);
            this.lblSQL.TabIndex = 48;
            this.lblSQL.Text = "条件：";
            // 
            // tbSQL
            // 
            this.tbSQL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSQL.Enabled = false;
            this.tbSQL.Location = new System.Drawing.Point(59, 62);
            this.tbSQL.Multiline = true;
            this.tbSQL.Name = "tbSQL";
            this.tbSQL.Size = new System.Drawing.Size(197, 100);
            this.tbSQL.TabIndex = 49;
            // 
            // btnSX
            // 
            this.btnSX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSX.Location = new System.Drawing.Point(200, 31);
            this.btnSX.Name = "btnSX";
            this.btnSX.Size = new System.Drawing.Size(56, 23);
            this.btnSX.TabIndex = 51;
            this.btnSX.Text = "筛选(&S)";
            this.btnSX.UseVisualStyleBackColor = true;
            this.btnSX.Click += new System.EventHandler(this.btnSX_Click);
            // 
            // lbSX
            // 
            this.lbSX.AutoSize = true;
            this.lbSX.Location = new System.Drawing.Point(12, 36);
            this.lbSX.Name = "lbSX";
            this.lbSX.Size = new System.Drawing.Size(41, 12);
            this.lbSX.TabIndex = 50;
            this.lbSX.Text = "筛选：";
            // 
            // FrmSqlLond
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 296);
            this.Controls.Add(this.btnSX);
            this.Controls.Add(this.lbSX);
            this.Controls.Add(this.tbSQL);
            this.Controls.Add(this.lblSQL);
            this.Controls.Add(this.tbSX);
            this.Controls.Add(this.cbMS);
            this.Controls.Add(this.tbBZ);
            this.Controls.Add(this.lblBZ);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblRecord);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSqlLond";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "读取";
            this.Shown += new System.EventHandler(this.FrmSqlLond_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbBZ;
        private System.Windows.Forms.Label lblBZ;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblRecord;
        private System.Windows.Forms.ComboBox cbMS;
        private System.Windows.Forms.TextBox tbSX;
        private System.Windows.Forms.Label lblSQL;
        private System.Windows.Forms.TextBox tbSQL;
        private System.Windows.Forms.Button btnSX;
        private System.Windows.Forms.Label lbSX;
    }
}