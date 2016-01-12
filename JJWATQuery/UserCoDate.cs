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
    public partial class UserCoDate : UserControl
    {
        public UserCoDate()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                dTimeB.Enabled = true;
                dTimeE.Enabled = true;
            }
            else
            {
                dTimeB.Enabled = false;
                dTimeE.Enabled = false;
            }
        }
    }
}
