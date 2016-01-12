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
    public partial class FrmSqlLond : Form
    {
        public FrmSqlLond()
        {
            InitializeComponent();
        }
        private string sLayerE, sLayer, sAdmin, sFrmName, sSqlSign;
        lsConDataBll ConData = new lsConDataBll();
        private Dictionary<string, string> dicSQL = new Dictionary<string, string>();
        TextBox ftbsql = new TextBox();
        public void intSQL(string LayerE, string Layer, string Admin, string FrmName, string SqlSign,TextBox tbSql)
        {
            sLayerE = LayerE;
            sLayer = Layer;
            sAdmin = Admin;
            sFrmName = FrmName;
            sSqlSign = SqlSign;
            ftbsql = tbSql;
        }

        private void FrmSqlLond_Shown(object sender, EventArgs e)
        {
            using (DataTable dTable = ConData.frmSqlSelectWhere(sFrmName, sLayer, cbMS.Text, sSqlSign))
            {
                cbMS.Items.Clear();
                dicSQL.Clear();
                if (dTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dTable.Rows.Count; i++)
                    {
                        dicSQL.Add(dTable.Rows[i][0].ToString(), dTable.Rows[i][1].ToString() + "|" + dTable.Rows[i][2].ToString());
                        cbMS.Items.Add(dTable.Rows[i][0].ToString());
                    }
                    cbMS.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("图层未设置过查询条件！");
                    this.Close();
                }
            }
        }
        private void set_cbSelectValue()
        {
            using (DataTable dTable = ConData.frmSqlSelectWhere(sFrmName, sLayer, tbSX.Text, sSqlSign))
            {
                cbMS.Items.Clear();
                dicSQL.Clear();
                if (dTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dTable.Rows.Count; i++)
                    {
                        dicSQL.Add(dTable.Rows[i][0].ToString(), dTable.Rows[i][1].ToString() + "|" + dTable.Rows[i][2].ToString());
                        cbMS.Items.Add(dTable.Rows[i][0].ToString());
                    }
                    cbMS.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("未找到符合条件的信息");
                }
            }
        }
        private void cbMS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dicSQL.Count>0)
            {
                string[] sName = dicSQL[cbMS.SelectedItem.ToString()].Split('|');
                tbSQL.Text = sName[0].ToString();
                tbBZ.Text = sName[1].ToString();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ftbsql.Text = tbSQL.Text;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSX_Click(object sender, EventArgs e)
        {
            tbSQL.Clear();
            tbBZ.Clear();
            set_cbSelectValue();
        }
    }
}
