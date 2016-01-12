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
    public partial class FrmSqlSave : Form
    {
        public FrmSqlSave()
        {
            InitializeComponent();
        }
        lsConDataBll ConData = new lsConDataBll();
        private string sLayerE, sSql, sLayer, sAdmin, sFrmName;
        public void intSQL(string LayerE, string sql,string Layer,string Admin,string FrmName)
        {
            sLayerE=LayerE;
            sSql=sql;
            sLayer=Layer;
            sAdmin = Admin;
            sFrmName = FrmName;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbRecord.Text.Trim()))
            {
                MessageBox.Show("描述不可以为空", "提示!");
            }
            else
            {
                try
                {
                    if (ConData.frmSqlInt(sFrmName,sLayer, tbRecord.Text)>0)
                    {
                        MessageBox.Show("描述存在，请重新输入", "提示!");
                        return;
                    }
                    ConData.frmSqlInsert(sLayer, sSql, sAdmin, sFrmName, tbRecord.Text.Trim(), tbBZ.Text);
                    MessageBox.Show("保存成功", "提示!");
                    this.Close();
                }
                catch (Exception)
                {

                    MessageBox.Show("条件错误，不可以保存");
                    return;
                }

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
