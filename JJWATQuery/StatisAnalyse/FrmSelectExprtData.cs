using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JJWATQuery.StatisAnalyse
{
    public partial class FrmSelectExprtData : Form
    {
        public string SelectDataCondition = string.Empty;

        //查询条件关系中文定义
        private const string mc_EqualSymbol = "等于";
        private const string mc_MoreSymbol = "大于";
        private const string mc_MoreEqualSymbol = "大于等于";
        private const string mc_SmallSymbol = "小于";
        private const string mc_SmallEqualSymbol = "小于等于";
        private const string mc_WithinSymbol = "介于";

        public FrmSelectExprtData()
        {
            InitializeComponent();
        }

        internal void Init(string selectedLayerName, List<string> strLastData2Select)
        {
            string strBuildDateCondition = string.Empty;
            this.Text = "选择导出数据:" + selectedLayerName;
            
            //填充关系下拉框
            cmbBoxCond.Items.Add(mc_EqualSymbol);
            cmbBoxCond.Items.Add(mc_MoreSymbol);
            cmbBoxCond.Items.Add(mc_MoreEqualSymbol);
            cmbBoxCond.Items.Add(mc_SmallSymbol);
            cmbBoxCond.Items.Add(mc_SmallEqualSymbol);
            cmbBoxCond.Items.Add(mc_WithinSymbol);

            cmbBoxCond.Text = mc_WithinSymbol;
            strLastData2Select.Sort();
            foreach (var o in strLastData2Select)
            {
                if(!cmbFromData.Items.Contains(o.ToString(CultureInfo.InvariantCulture)))
                {
                    cmbFromData.Items.Add(o.ToString(CultureInfo.InvariantCulture));
                    cmbToData.Items.Add(o.ToString(CultureInfo.InvariantCulture));
                }
            }

        }

        private void FrmSelectExprtData_Load(object sender, EventArgs e)
        {
            SelectDataCondition = string.Empty;
            cmbBoxCond.Enabled = rdBtn_Selecting.Checked;
            cmbFromData.Enabled = rdBtn_Selecting.Checked;
            lbl_To.Enabled = rdBtn_Selecting.Checked;
            cmbToData.Enabled = rdBtn_Selecting.Checked;

            lbl_To.Visible = (cmbBoxCond.Text == mc_WithinSymbol);
            cmbToData.Visible = lbl_To.Visible;
        }

        private void rdBtn_Selecting_CheckedChanged(object sender, EventArgs e)
        {
            cmbBoxCond.Enabled = rdBtn_Selecting.Checked;
            cmbFromData.Enabled = rdBtn_Selecting.Checked;
            lbl_To.Enabled = rdBtn_Selecting.Checked;
            cmbToData.Enabled = rdBtn_Selecting.Checked;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(rdBtn_Selecting.Checked==true)
            {
                if (cmbBoxCond.Text == mc_WithinSymbol)
                {
                    if (cmbFromData.Text == "" || cmbToData.Text == "")
                    {
                        MessageBox.Show("范围值无效！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        return;
                    }

                }
                else
                {
                    if (cmbFromData.Text == "")
                    {
                        MessageBox.Show("请选择一个有效值！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            
            SelectDataCondition = rdBtn_All.Checked ? "JJGISALLDATA" : SelectDataCondition;

            this.DialogResult = DialogResult.OK;
        }

        private string CombSelectData(string strRelation, string strStartNum, string strEndNum)
        {
            string strRet = string.Empty;

            switch (strRelation)
            {
                case mc_EqualSymbol:
                    strRet = "等于之" + strStartNum;
                    
                    break;
                case mc_MoreSymbol:
                    strRet = "大于之" + strStartNum;
                    
                    break;
                case mc_MoreEqualSymbol:
                    strRet = "不小于" + strStartNum;
                    
                    break;
                case mc_SmallSymbol:
                    strRet = "小于之" + strStartNum;
                    
                    break;
                case mc_SmallEqualSymbol:
                    strRet = "不大于" + strStartNum;
                    
                    break;
                case mc_WithinSymbol:
                    strRet = "介于之" + strStartNum + "和" + strEndNum;
                    break;
            }
            return strRet;
        }

        private void cmbBoxCond_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_To.Visible = (cmbBoxCond.Text == mc_WithinSymbol);
            cmbToData.Enabled = true;
            lbl_To.Enabled = true;
            cmbToData.Visible = lbl_To.Visible;
            SelectDataCondition = CombSelectData(cmbBoxCond.Text, cmbFromData.Text, cmbToData.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult=DialogResult.Cancel;
            return;
        }

        private void cmbToData_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectDataCondition = CombSelectData(cmbBoxCond.Text, cmbFromData.Text, cmbToData.Text);
        }

        private void cmbFromData_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectDataCondition = CombSelectData(cmbBoxCond.Text, cmbFromData.Text, cmbToData.Text);
            cmbToData.Items.Clear();
            foreach (var o in cmbFromData.Items)
            {
                if(o.ToString()!=""&&System.Convert.ToDouble(o.ToString())>Convert.ToDouble(cmbFromData.Text))
                {
                    cmbToData.Items.Add(o.ToString());
                }
            }
            if (cmbBoxCond.Text == mc_WithinSymbol && cmbToData.Items.Count < 1)
            {
                MessageBox.Show("介于条件，当前不能为最大值", "提示");
                cmbFromData.Text = "";
                return;
            }

        }

       




        
    }
}
