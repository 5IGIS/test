using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JJWATQuery
{
    public partial class UserCoGroupList : UserControl
    { 
        
        string stypename;
        FrmSelect fSelect;
        public string Stypename
        {
            get { return stypename; }
            set { stypename = value; }
        }
        List<string> listname;

        public List<string> Listname
        {
            get { return listname; }
            set { listname = value; }
        }
        public UserCoGroupList()
        {
            InitializeComponent();
        }
        private void UserCoGroupList_Load(object sender, EventArgs e)
        {
            btnSelect.Enabled = false;

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (fSelect == null || fSelect.IsDisposed)
            {
                fSelect = new FrmSelect();
                fSelect.gbSelect.Text = Stypename;
                fSelect.tbSelect = tbSelect;
                fSelect.lType = listname;
                fSelect.ShowDialog();
            }
            else
            {
                fSelect.gbSelect.Text = Stypename;
                fSelect.lType = listname;
                fSelect.Visible = true;
            }
        }

        private void cbType_CheckedChanged(object sender, EventArgs e)
        {
            if (cbType.Checked == true)
                btnSelect.Enabled = true;
            else
                btnSelect.Enabled = false;
        }
    }
}
