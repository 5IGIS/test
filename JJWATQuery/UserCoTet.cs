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
    public partial class UserCoTet : UserControl
    {
        public UserCoTet()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.Enabled = true;
                if (checkBox1.Text != "档案号" || checkBox1.Text != "户号")
                {
                    cbPreciseQuery.Enabled = true;
                }
            }
            else
            {
                textBox1.Enabled = false;
                cbPreciseQuery.Enabled = false;
            }
        }
    }
}
