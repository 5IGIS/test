using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Utilitys;

namespace JJWATQuery
{
    public partial class UtilitysFields : Form
    {
        public string tableName;
        IFeatureClass pFClass;
        public UtilitysFields(IFeatureClass fcs)
        {
            pFClass = fcs;
            InitializeComponent();
        }

        public CheckedListBox checkFileds = new CheckedListBox();

        private void cmdCenter_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (checkFileds.CheckedItems.Count == 0)
            {
                MessageBox.Show("请选择需要导出的字段！！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            UtilitysResultForm.isClose = false;
            this.Visible = false;
        }

        private void UtilitysFields_Load(object sender, EventArgs e)
        {
            IField pField;
            tabTables.TabPages.Add(pFClass.AliasName);
            checkFileds.Dock = DockStyle.Fill;
            checkFileds.CheckOnClick = true;
            tabTables.TabPages[0].Controls.Add(checkFileds);
            System.Data.DataTable dt = SDTBLL.QueryAllFields(tableName, UtilitysResultForm.mgs.get_LoginUserName());
            for (int i = 0; i < pFClass.Fields.FieldCount; i++)
            {
                pField = pFClass.Fields.get_Field(i);
                if (pField.Name == "OBJECTID")
                {
                    checkFileds.Items.Add(pField.Name);
                    continue;
                }
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    if (dt.Rows[k]["FIELDNAME"].ToString() == pField.Name)
                    {
                        if (pField.Name != "SHAPE")
                            checkFileds.Items.Add(pField.AliasName);
                    }
                }
            }
        }

        private void UtilitysFields_FormClosed(object sender, FormClosedEventArgs e)
        {
            UtilitysResultForm.isClose = true;
        }

        private void check_All_Click(object sender, EventArgs e)
        {
            if (check_All.CheckState == CheckState.Checked)
            {
                //check_All.Text = "全不选";
                for (int i = 0; i < checkFileds.Items.Count; i++)
                {
                    checkFileds.SetItemCheckState(i, CheckState.Checked);
                }
                check_All.CheckState = CheckState.Checked;
            }
            else
            {
                //check_All.Text = "全选";
                for (int i = 0; i < checkFileds.Items.Count; i++)
                {
                    checkFileds.SetItemCheckState(i, CheckState.Unchecked);
                }
                check_All.CheckState = CheckState.Unchecked;
            }
        }
    }
}