using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utilitys;

namespace JJWATQuery
{
    public partial class FrmSqlDel : Form
    {
        public FrmSqlDel()
        {
            InitializeComponent();
        }
        lsConDataBll ConData = new lsConDataBll();
        string sLayer;
        private List<ClsDataSql> listData = new List<ClsDataSql>();
        public void intSQL(string Layer)
        {
            sLayer = Layer;
        }
        private void FrmSqlDel_Shown(object sender, EventArgs e)
        {
            SelectWhere(0);
        }

        private void SelectWhere(int i)
        {
            listData = ConData.frmSqlSelectWhere(sLayer);
            if (i == 0)
            {
                if (listData.Count > 0)
                {
                    dgvData.DataSource = listData;
                }
                else
                {
                    MessageBox.Show("图层未设置过查询条件！");
                    this.Close();
                }
            }
            else
            {
                if (listData.Count > 0)
                {
                    dgvData.DataSource = listData;
                }
                else
                {
                    dgvData.DataSource = "";
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            if (dgvData.SelectedRows.Count > 0)
            {
                DialogResult dr = MessageBox.Show("您确定要删除吗？", "确认", MessageBoxButtons.YesNo);
                if (dr==DialogResult.Yes)
                {
                    string sName = "";
                    for (int i = 0; i < dgvData.SelectedRows.Count; i++)
                    {
                        sName = sName + "'" + dgvData.SelectedRows[i].Cells[0].Value + "',";
                    }
                    sName = sName.Substring(0, sName.Length - 1);
                    try
                    {
                        ConData.frmSqlDel(sLayer, sName);
                        MessageBox.Show("删除成功！");
                        SelectWhere(1);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择要删除的记录！");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
