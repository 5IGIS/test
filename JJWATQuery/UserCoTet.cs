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
                textBox1.Enabled = true ;
            else
                textBox1.Enabled = false;
        }
    }
}
