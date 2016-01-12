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
    /// <summary>
    ///'* 类名：frmSelInvest
    ///'* 功能：北京市自来水公司管网固定资产报表 条件设置窗体。
    ///'* 描述：
    ///'* 开发：OY
    ///'* 时间：2005/03/18
    ///'* 再版：2015/02/02
    ///'* Developer:wchm
    /// </summary>
    public partial class FrmSelInvest : Form
    {

        private List<string> m_lstInvestmentStyle = new List<string>();       

        //查询条件关系中文定义
        private const string mc_EqualSymbol = "等于";
        private const string mc_MoreSymbol = "大于";
        private const string mc_MoreEqualSymbol = "大于等于";
        private const string mc_SmallSymbol = "小于";
        private const string mc_SmallEqualSymbol = "小于等于";
        private const string mc_WithinSymbol = "介于";
        //2015-01-12 录入日期 修改为 检核日期:WRITEDATE-->CHECKEDATE
        private const string mc_StrQueryFieldNameBuilddt = "CHECKEDATE";


        public string InvestSelected = string.Empty;
        public string BuildDtComplex = string.Empty;//复杂
        public string BuildDtSimple = string.Empty;//简单
        public string BuildDataTitle = string.Empty;
        
        public FrmSelInvest()
        {
            InitializeComponent();            
        }
        
        internal bool Init(List<string> lstInvestStyle)
        {
            try
            {
                if (lstInvestStyle.Count < 1)
                {
                    MessageBox.Show("读取Geodatabase时遇到错误，操作终止！", "提示！");
                    return false;
                }
                else
                {
                    //投资方式数据源赋值
                    m_lstInvestmentStyle.Clear();
                    m_lstInvestmentStyle = lstInvestStyle;
                    
                    //填充关系下拉框
                    cboBuildDt.Items.Add(mc_EqualSymbol);
                    cboBuildDt.Items.Add(mc_MoreSymbol);
                    cboBuildDt.Items.Add(mc_MoreEqualSymbol);
                    cboBuildDt.Items.Add(mc_SmallSymbol);
                    cboBuildDt.Items.Add(mc_SmallEqualSymbol);
                    cboBuildDt.Items.Add(mc_WithinSymbol);

                    cboBuildDt.Text = mc_WithinSymbol;
                    //MessageBox.Show("Init!");
                    this.DialogResult = DialogResult.Cancel;                                        
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误！");
                return false;
            }
        }

        private void FrmSelInvest_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("Load!");
            cboInvestStyle.Items.Clear();
            for (int i = 0; i < m_lstInvestmentStyle.Count; i++)
            {
                cboInvestStyle.Items.Add(m_lstInvestmentStyle[i].ToString());
            }
            if (cboInvestStyle.Items.Count > 0)
                cboInvestStyle.SelectedIndex = 0;            
        }
        
        //组合日期条件
        private string CombWhereDate(string strRelation, string strField, string strStartNum, string strEndNum)
        {
            //将日期型转为字符型数据进行查询                      

            string strRet = string.Empty;
            string strRetTitle = string.Empty;

            switch (strRelation)
            {
                case mc_EqualSymbol:
                    //where writedate >= to_date('" + strBegDate + "','yyyyMMdd') 
                    //strRet = "(" + strField + "=to_date('" + strStartNum + "','yyyyMMdd'))";
                    strRet = "(" + strField + " >=to_date('" + strStartNum + "000000','yyyyMMddhh24:mi:ss')" + " and " + strField + " <=to_date('" + strStartNum + "235959','yyyyMMddhh24:mi:ss'))";
                    strRetTitle = formatDataCN(strStartNum);
                    break;
                case mc_MoreSymbol:
                    //strRet = "(" + strField + ">to_date('" + strStartNum + "','yyyyMMdd'))";
                    strRet = "(" + strField + ">to_date('" + strStartNum + "235959','yyyyMMddhh24:mi:ss'))";
                    strRetTitle = ""+formatDataCN(strStartNum)+"以后";
                    break;
                case mc_MoreEqualSymbol:
                    strRet = "(" + strField + ">=to_date('" + strStartNum + "000000','yyyyMMddhh24:mi:ss'))";
                    strRetTitle = "从"+formatDataCN(strStartNum)+"起";
                    break;
                case mc_SmallSymbol:
                    strRet = "(" + strField + "<to_date('" + strStartNum + "000000','yyyyMMddhh24:mi:ss'))";
                    strRetTitle = "" + formatDataCN(strStartNum) + "之前";
                    break;
                case mc_SmallEqualSymbol:
                    strRet = "(" + strField + "<=to_date('" + strStartNum + "235959','yyyyMMddhh24:mi:ss'))";
                    strRetTitle = "从" + formatDataCN(strStartNum) + "起之前";
                    break;
                case mc_WithinSymbol:
                    strRet = "(" + strField + " >=to_date('" + strStartNum + "000000','yyyyMMddhh24:mi:ss')" + " and " + strField + " <=to_date('" + strEndNum + "235959','yyyyMMddhh24:mi:ss'))";
                    strRetTitle = formatDataCN(strEndNum);
                    strRetTitle = strRetTitle.Substring(0,strRetTitle.Length-3);
                    break;
            }
            BuildDataTitle = strRetTitle;            
            return strRet;
        }
        //格式化日期20150105 => 2015年01月05日
        private string formatDataCN(string strDate)
        {
            if (strDate.Trim() == "")
            {
                return " ";
            }
            else
            {                
                string strDay = string.Empty;
                string strYear = string.Empty;
                string strMonth = string.Empty;                              
                
                strYear = strDate.Substring(0, 4);
                strMonth = strDate.Substring(4, 2);
                strDay = strDate.Substring(6, 2);

                return strYear + "年" + strMonth + "月" + strDay + "日";
            }

        }
        
        private void btn_OK_Click(object sender, EventArgs e)
        {
            InvestSelected = cboInvestStyle.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            InvestSelected = "";
            BuildDtComplex = "";
            BuildDataTitle = "";
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //选择查询条件时发生：
        private void cboBuildDt_Click(object sender, EventArgs e)
        {
            lbl_To.Visible = (cboBuildDt.Text == mc_WithinSymbol);
            dTPDateTo.Enabled = true;
            lbl_To.Enabled = true;
            dTPDateTo.Visible = lbl_To.Visible;
            BuildDtComplex = CombWhereDate(cboBuildDt.Text,
                                           mc_StrQueryFieldNameBuilddt,
                                           DateToChar(dTPDateFrom.Value),
                                           DateToChar(dTPDateTo.Value)
                                            );
            
        }

        private void cboBuildDt_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_To.Visible = (cboBuildDt.Text == mc_WithinSymbol);
            dTPDateTo.Enabled = true;
            lbl_To.Enabled = true;
            dTPDateTo.Visible = lbl_To.Visible;
            BuildDtComplex = CombWhereDate(cboBuildDt.Text,
                                           mc_StrQueryFieldNameBuilddt,
                                           DateToChar(dTPDateFrom.Value),
                                           DateToChar(dTPDateTo.Value)
                                            );
        }
        //录入日期：to
        private void dTPDateTo_ValueChanged(object sender, EventArgs e)
        {
            BuildDtSimple = "";
            BuildDtComplex = CombWhereDate(cboBuildDt.Text, mc_StrQueryFieldNameBuilddt, DateToChar(dTPDateFrom.Value), DateToChar(dTPDateTo.Value));
        }
        //录入日期：from
        private void dTPDateFrom_ValueChanged(object sender, EventArgs e)
        {
            BuildDtSimple = "";
            BuildDtComplex = CombWhereDate(cboBuildDt.Text, mc_StrQueryFieldNameBuilddt, DateToChar(dTPDateFrom.Value), DateToChar(dTPDateTo.Value));

            //简单查询
            if (cboBuildDt.Text != mc_WithinSymbol)
            {
                string strDt = string.Empty;
                string strCond = string.Empty;
                strDt = dTPDateFrom.Value.ToString();
                strCond = " SUBSTR( TRIM(" + mc_StrQueryFieldNameBuilddt + "),1,6)=" + strDt.Substring(1, 6);
                BuildDtSimple = strCond;
                BuildDataTitle = formatDataCN(strDt).Substring(1, 8);
            }
        }
        //将日期型数据 年月日--改成字符型数据进行查询统计
        private string DateToChar(DateTime dateTime)
        {
 	       //DateTime SysDate=new DateTime();
            string StrYear=string.Empty;
            string StrMonth=string.Empty;
            string StrDay=string.Empty;

            StrYear=dateTime.Year.ToString();
            StrMonth=dateTime.Month.ToString();
            StrDay=dateTime.Day.ToString();
            if(StrMonth.Trim().Length==1)
                StrMonth="0"+StrMonth;
            if(StrDay.Trim().Length==1)
                StrDay="0"+StrDay;
            return StrYear+StrMonth+StrDay;

        }

        private void FrmSelInvest_Shown(object sender, EventArgs e)
        {
            lbl_To.Enabled = true;
            lbl_To.Visible = (cboBuildDt.Text == mc_WithinSymbol);
            dTPDateTo.Enabled = true;
            dTPDateTo.Visible = lbl_To.Visible;
        }
        ////录入日期：下
        //private void dTPBuildDtSimple_ValueChanged(object sender, EventArgs e)
        //{
        //    string strDt = string.Empty;
        //    string strCond = string.Empty;
        //    strDt = dTPBuildDtSimple.Value.ToString();
        //    strCond = " SUBSTR( TRIM(" + mc_StrQueryFieldNameBuilddt + "),1,6)=" + strDt.Substring(1, 6);
        //    BuildDtSimple = strCond;
        //    BuildDataTitle = formatDataCN(strDt).Substring(1, 8);

        //}


    }
}
