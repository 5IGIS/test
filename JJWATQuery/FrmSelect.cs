using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JJWATQuery
{
    public partial class FrmSelect : Form
    {
        public FrmSelect()
        {
            InitializeComponent();
        }
        public TextBox tbSelect;
        public List<string> lType = new List<string>();

        private void frmSelect_Shown(object sender, EventArgs e)
        {

        }

        private void frmSelect_VisibleChanged(object sender, EventArgs e)
        {
            clbTypes.Items.Clear();
            for (int i = 0; i < lType.Count; i++)
            {
                clbTypes.Items.Add(lType[i]);
            }
            cbAll.Checked = false;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            string sWhere = "";
            for (int i = 0; i < clbTypes.CheckedItems.Count; i++)
            {
                sWhere = sWhere + "'"+clbTypes.CheckedItems[i].ToString() +"'"+ ",";
            }
            if (string.IsNullOrEmpty(sWhere)==false)
            {
                sWhere = sWhere.Substring(0, sWhere.Length - 1);
                
            }
            tbSelect.Text = sWhere;
            this.Visible = false;
        }
        private void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAll.Checked == true)
            {
                for (int i = 0; i < clbTypes.Items.Count; i++)
                {
                    clbTypes.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < clbTypes.Items.Count; i++)
                {
                    clbTypes.SetItemChecked(i, false);
                }
            }
        }
    }
}
