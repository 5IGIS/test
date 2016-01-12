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
    public partial class SelectLayerForm : Form
    {
        TreeNode m_Nodes;
        public SelectLayerForm(TreeNode node)
        {
            m_Nodes = node;
            InitializeComponent();
        }

        public CheckedListBox checkLayers = new CheckedListBox();

        private void cmdCenter_Click(object sender, EventArgs e)
        {
            UtilitysResultForm.isClose = true;
            this.Close();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (checkLayers.CheckedItems.Count == 0)
            {
                MessageBox.Show("请选择需要添加选择集的图层！！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            UtilitysResultForm.isClose = false;
            this.Visible = false;
            this.Close();
        }

        private void UtilitysFields_Load(object sender, EventArgs e)
        {
            string[] strLayerNum;
            tabTables.TabPages.Add("图层名列表");
            checkLayers.Dock = DockStyle.Fill;
            checkLayers.CheckOnClick = true;
            tabTables.TabPages[0].Controls.Add(checkLayers);
            for (int i = 0; i < m_Nodes.Nodes.Count; i++)
            {
                strLayerNum = m_Nodes.Nodes[i].Text.Split('：');
                checkLayers.Items.Add(strLayerNum[0]);
            }
        }

        private void check_All_Click(object sender, EventArgs e)
        {
            if (check_All.CheckState == CheckState.Checked)
            {
                //check_All.Text = "全不选";
                for (int i = 0; i < checkLayers.Items.Count; i++)
                {
                    checkLayers.SetItemCheckState(i, CheckState.Checked);
                }
                check_All.CheckState = CheckState.Checked;
            }
            else
            {
                //check_All.Text = "全选";
                for (int i = 0; i < checkLayers.Items.Count; i++)
                {
                    checkLayers.SetItemCheckState(i, CheckState.Unchecked);
                }
                check_All.CheckState = CheckState.Unchecked;
            }
        }
    }
}