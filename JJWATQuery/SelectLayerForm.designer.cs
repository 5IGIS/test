namespace JJWATQuery
{
    partial class SelectLayerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectLayerForm));
            this.cmdCenter = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.tabTables = new System.Windows.Forms.TabControl();
            this.check_All = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cmdCenter
            // 
            this.cmdCenter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCenter.Location = new System.Drawing.Point(176, 339);
            this.cmdCenter.Name = "cmdCenter";
            this.cmdCenter.Size = new System.Drawing.Size(58, 21);
            this.cmdCenter.TabIndex = 6;
            this.cmdCenter.Text = "退出(&Q)";
            this.cmdCenter.UseVisualStyleBackColor = true;
            this.cmdCenter.Click += new System.EventHandler(this.cmdCenter_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.Location = new System.Drawing.Point(115, 339);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(58, 21);
            this.cmdOK.TabIndex = 5;
            this.cmdOK.Text = "确定(&Y)";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // tabTables
            // 
            this.tabTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabTables.Location = new System.Drawing.Point(1, 0);
            this.tabTables.Name = "tabTables";
            this.tabTables.SelectedIndex = 0;
            this.tabTables.Size = new System.Drawing.Size(235, 337);
            this.tabTables.TabIndex = 4;
            // 
            // check_All
            // 
            this.check_All.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.check_All.AutoSize = true;
            this.check_All.Location = new System.Drawing.Point(8, 342);
            this.check_All.Name = "check_All";
            this.check_All.Size = new System.Drawing.Size(48, 16);
            this.check_All.TabIndex = 7;
            this.check_All.Text = "全选";
            this.check_All.UseVisualStyleBackColor = true;
            this.check_All.Click += new System.EventHandler(this.check_All_Click);
            // 
            // SelectLayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(236, 363);
            this.Controls.Add(this.check_All);
            this.Controls.Add(this.cmdCenter);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.tabTables);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectLayerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "配置添加选择集图层";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.UtilitysFields_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdCenter;
        private System.Windows.Forms.Button cmdOK;
        public System.Windows.Forms.TabControl tabTables;
        private System.Windows.Forms.CheckBox check_All;
    }
}