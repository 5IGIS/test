using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JJWATBaseLibC;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using ESRI.ArcGIS.Framework;

namespace JJWATQuery
{
    public partial class FrmStatPanel : Form
    {
        private List<string> m_lstInvestmentStyle = new List<string>();

        //查询条件关系中文定义
        private const string mc_FieldNameShapeLength = "SHAPE_LENGTH";//管线长度
        private const string mc_EqualSymbol = "等于";
        private const string mc_MoreSymbol = "大于";
        private const string mc_MoreEqualSymbol = "大于等于";
        private const string mc_strNameReport_Pipes = "撤除管线长度统计月报";
        private const string mc_SmallSymbol = "小于";
        private const string mc_SmallEqualSymbol = "小于等于";
        private const string mc_StrQueryFieldNameBuilddt = "bfdate";
        private const string mc_TableItem = "管径";
        private const string mc_TableItemSum = "小计";
        private const string mc_TableSubtotal = "合计";
        private const string mc_WithinSymbol = "介于";

        private string m_strRet = string.Empty;

        public string InvestSelected = string.Empty;
        public string BuildDtComplex = string.Empty;
        public string BuildDtSimple = string.Empty;
        public string BuildDataTitle = string.Empty;

        private List<string> m_lstColumns = new List<string>();
        private List<string> m_lstRow = new List<string>();
        private List<string> m_lstDBResults = new List<string>();

       // private string m_strConnstr = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=E:\\20150109-JJBW\\jjwater.mdb";
        private const string mc_strExcelTemplate = @"C:\Users\Administrator\AppData\Roaming\Microsoft\Excel\XLSTART\PERSONAL.XLSB";
        private LocalCDBCon m_objLdbc = new LocalCDBCon();
        private CDBCon m_objDBCon = null;

        private Dictionary<string, string> m_dicColumnCodeDesc = new Dictionary<string, string>();
        private Dictionary<string, string> m_dicRowCodeDesc = new Dictionary<string, string>();
        private IApplication m_application;

        public FrmStatPanel()
        {
            InitializeComponent();
        }

        internal bool Init(IApplication app)
        {
            try
            {
                m_application = app;
                //填充关系下拉框
                cboBuildDt.Items.Add(mc_EqualSymbol);
                cboBuildDt.Items.Add(mc_MoreSymbol);
                cboBuildDt.Items.Add(mc_MoreEqualSymbol);
                cboBuildDt.Items.Add(mc_SmallSymbol);
                cboBuildDt.Items.Add(mc_SmallEqualSymbol);
                cboBuildDt.Items.Add(mc_WithinSymbol);

                cboBuildDt.Text = mc_WithinSymbol;
                //m_objLdbc.OpenConnection(m_strConnstr);
                //m_objLdbc.DBConnTest();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误!");
                return false;
            }
            return true;

        }

        private void FrmStatPanel_Load(object sender, EventArgs e)
        {

        }

        //组合日期条件
        private string CombWhereDate(string strRelation, string strField, string strStartNum, string strEndNum)
        {
            //将日期型转为字符型数据进行查询                      

            
            string strRetTitle = string.Empty;

            switch (strRelation)
            {
                case mc_EqualSymbol:
                    m_strRet = "(" + strField + " >=to_date('" + strStartNum + "000000','yyyyMMddhh24:mi:ss')" + " and " + strField + " <=to_date('" + strStartNum + "235959','yyyyMMddhh24:mi:ss'))";
                    //strRetTitle = formatDataCN(strStartNum);
                    break;
                case mc_MoreSymbol:
                    m_strRet = "(" + strField + ">to_date('" + strStartNum + "235959','yyyyMMddhh24:mi:ss'))";
                    //strRetTitle = "" + formatDataCN(strStartNum) + "以后";
                    break;
                case mc_MoreEqualSymbol:
                    m_strRet = "(" + strField + ">=to_date('" + strStartNum + "000000','yyyyMMddhh24:mi:ss'))";
                    //strRetTitle = "从" + formatDataCN(strStartNum) + "起";
                    break;
                case mc_SmallSymbol:
                    m_strRet = "(" + strField + "<to_date('" + strStartNum + "000000','yyyyMMddhh24:mi:ss'))";
                    //strRetTitle = "" + formatDataCN(strStartNum) + "之前";
                    break;
                case mc_SmallEqualSymbol:
                    m_strRet = "(" + strField + "<=to_date('" + strStartNum + "235959','yyyyMMddhh24:mi:ss'))";
                    //strRetTitle = "从" + formatDataCN(strStartNum) + "起之前";
                    break;
                case mc_WithinSymbol:
                    m_strRet = "(" + strField + " >=to_date('" + strStartNum + "000000','yyyyMMddhh24:mi:ss')" + " and " + strField + " <=to_date('" + strEndNum + "235959','yyyyMMddhh24:mi:ss'))";
                    //strRetTitle = formatDataCN(strEndNum);
                    //strRetTitle = strRetTitle.Substring(0, strRetTitle.Length - 3);
                    break;
            }
            //BuildDataTitle = strRetTitle;
            return m_strRet;
           
        }

        //修改条件
        //private void cboBuildDt_Click(object sender, EventArgs e)
        //{
        //    lbl_To.Visible = (cboBuildDt.Text == mc_WithinSymbol);
        //    dTPDateTo.Enabled = true;
        //    lbl_To.Enabled = true;
        //    dTPDateTo.Visible = lbl_To.Visible;
        //    BuildDtComplex = CombWhereDate(cboBuildDt.Text,
        //                                   mc_StrQueryFieldNameBuilddt,
        //                                   DateToChar(dTPDateFrom.Value),
        //                                   DateToChar(dTPDateTo.Value)
        //                                    );
        //}
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

        ////格式化日期20150105 => 2015年01月05日
        //private string formatDataCN(string strDate)
        //{
        //    if (strDate.Trim() == "")
        //    {
        //        return " ";
        //    }
        //    else
        //    {
        //        string strDay = string.Empty;
        //        string strYear = string.Empty;
        //        string strMonth = string.Empty;

        //        strYear = strDate.Substring(0, 4);
        //        strMonth = strDate.Substring(4, 2);
        //        strDay = strDate.Substring(6, 2);

        //        return strYear + "年" + strMonth + "月" + strDay + "日";
        //    }

        //}

        //将日期型数据 年月日--改成字符型数据进行查询统计
        private string DateToChar(DateTime dateTime)
        {
            //DateTime SysDate=new DateTime();
            string StrYear = string.Empty;
            string StrMonth = string.Empty;
            string StrDay = string.Empty;

            StrYear = dateTime.Year.ToString();
            StrMonth = dateTime.Month.ToString();
            StrDay = dateTime.Day.ToString();
            if (StrMonth.Trim().Length == 1)
                StrMonth = "0" + StrMonth;
            if (StrDay.Trim().Length == 1)
                StrDay = "0" + StrDay;
            return StrYear + StrMonth + StrDay;
        }

        //撤除日期 to
        private void dTPDateTo_ValueChanged(object sender, EventArgs e)
        {
            BuildDtSimple = "";
            BuildDtComplex = CombWhereDate(cboBuildDt.Text, mc_StrQueryFieldNameBuilddt, DateToChar(dTPDateFrom.Value), DateToChar(dTPDateTo.Value));
        }

        //撤除日期 from
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
                //BuildDataTitle = formatDataCN(strDt).Substring(1, 8);
            }
        }

        private void FrmStatPanel_Shown(object sender, EventArgs e)
        {
            lbl_To.Enabled = true;
            lbl_To.Visible = (cboBuildDt.Text == mc_WithinSymbol);
            dTPDateTo.Enabled = true;
            dTPDateTo.Visible = lbl_To.Visible;
        }

        //导出统计结果
        private void btn_Export_Click(object sender, EventArgs e)
        {

            if (rdBtn_PipeLenMons.Checked)
            {
                RemovedPipeStat();
            }
            
            
        }
        //撤除管线统计
        private void RemovedPipeStat()
        {
            string strSQL = string.Empty, strPipeSectionMain = string.Empty, strPipeSectionUser = string.Empty, strPipeSectionSource = string.Empty;

            DataTable dDataTableA = new DataTable();
            DataTable dDataTableB = new DataTable();

            OleDbDataReader adoReader = null;

            strPipeSectionMain = string.Format("(select * from jjwater.bfpipesectionmain a,jjwater.f{0} b where a.shape=b.fid)",
            getLayerID("BFPIPESECTIONMAIN"));
            strPipeSectionUser = string.Format("(select * from jjwater.bfpipesectionuser a,jjwater.f{0} b where a.shape=b.fid)",
                getLayerID("BFPIPESECTIONUSER"));
            strPipeSectionSource = string.Format("(select * from jjwater.bfpipesectionsource a,jjwater.f{0} b where a.shape=b.fid)",
                getLayerID("BFPIPESECTIONSOURCE"));


            strSQL = "select diameter,material,sum(cd) shape_length from ((select diameter,material,len cd from " +
            strPipeSectionMain + " where " +
            " writedate is not null and writedate>to_date('194910011500','yyyyMMddhh24:mi:ss') " +
            " and "+BuildDtComplex+
            " ) union all" +
            " (select diameter,material,len cd from " +
            strPipeSectionUser + " where " +
            " writedate is not null and writedate>to_date('194910011500','yyyyMMddhh24:mi:ss')" +
            " and " + BuildDtComplex +
            " ) union all" +
            "(select diameter,material,len cd from " +
            strPipeSectionSource + " where " +
            " writedate is not null and writedate>to_date('194910011500','yyyyMMddhh24:mi:ss')" +
            " and " + BuildDtComplex +
            " )) group by diameter,material order by diameter";

            try
            {
                if (m_objDBCon == null)
                    m_objDBCon = CDBCon.GetInstance();
                m_objDBCon.ExecuteSQLReturn(strSQL, ref adoReader);
                if (adoReader.HasRows)
                {
                    this.Cursor = Cursors.WaitCursor;

                    string strDBresult = string.Empty;
                    string strDiameter = string.Empty;
                    string strMaterial = string.Empty;
                    string strNewRecord = string.Empty;
                    string strSameRecord = string.Empty;
                    double dblTempLength = 0;

                    while (adoReader.Read())
                    {
                        strDBresult = "";
                        dblTempLength = 0;
                        strDiameter = adoReader["diameter"].ToString().Trim();
                        strMaterial = adoReader["material"].ToString().Trim();

                        if (!m_lstRow.Contains(strDiameter))
                        {
                            m_lstRow.Add(strDiameter);
                        }
                        if (!m_lstColumns.Contains(strMaterial))
                        {
                            m_lstColumns.Add(strMaterial);
                        }

                        foreach (string r in m_lstDBResults)
                        {
                            string[] s = r.Split(',');
                            if ((strDiameter + "," + strMaterial) == (s[0] + "," + s[1]))
                            {
                                strSameRecord = r;
                                dblTempLength = Convert.ToDouble(s[2]) + Convert.ToDouble(adoReader[mc_FieldNameShapeLength]);
                                strNewRecord = s[0] + "," + s[1] + "," + dblTempLength.ToString();
                                break;
                            }
                        }

                        if (strSameRecord != "")
                        {
                            m_lstDBResults.Remove(strSameRecord);
                            m_lstDBResults.Add(strNewRecord);

                            strSameRecord = "";
                            strNewRecord = "";
                        }
                        else
                        {
                            strDBresult = strDiameter + "," + strMaterial + "," + adoReader[mc_FieldNameShapeLength].ToString();
                            m_lstDBResults.Add(strDBresult);
                        }
                    }
                    //为数据表创建列
                    for (int i = 0; i < m_lstColumns.Count; i++)
                    {
                        DataColumn dc = null;
                        if (i == 0)
                        {
                            dc = dDataTableA.Columns.Add(mc_TableItem, Type.GetType("System.String"));
                        }
                        dc = dDataTableA.Columns.Add(m_lstColumns[i].ToString(), Type.GetType("System.Decimal"));
                        if (i == m_lstColumns.Count - 1)
                        {
                            dc = dDataTableA.Columns.Add(mc_TableItemSum, Type.GetType("System.Decimal"));
                        }
                    }
                    m_lstColumns.Add(mc_TableItemSum);
                    //数据表添加行头
                    for (int j = 0; j < m_lstRow.Count; j++)
                    {
                        DataRow dr = dDataTableA.NewRow();
                        dr[mc_TableItem] = m_lstRow[j].ToString();
                        dDataTableA.Rows.Add(dr);
                    }

                    //数据表赋初值为0
                    foreach (DataRow dr in dDataTableA.Rows)
                    {
                        for (int k = 1; k < dr.ItemArray.Length; k++)
                        {
                            dr[k] = 0;
                        }
                    }

                    int count = 0;
                    string strCodeofColumn = string.Empty;
                    string strCodeofRow = string.Empty;
                    string strOldColumnItem = string.Empty;
                    string strOldRowItem = string.Empty;

                    foreach (DataRow dr in dDataTableA.Rows)
                    {
                        count++;
                        double dblHeji = 0;
                        for (int k = 1; k < dr.ItemArray.Length; k++)
                        {
                            //code作为查询条件，description和code的替换
                            string strSqlQueryDBvalue = string.Empty;
                            if (m_dicColumnCodeDesc.ContainsValue(m_lstColumns[k - 1]))
                            {
                                if (strOldColumnItem != m_lstColumns[k - 1])
                                {
                                    foreach (KeyValuePair<string, string> kvp in m_dicColumnCodeDesc)
                                    {
                                        if (kvp.Value.Equals(m_lstColumns[k - 1]))
                                        {
                                            strCodeofColumn = kvp.Key;
                                            break;
                                        }
                                    }
                                    strOldColumnItem = m_lstColumns[k - 1];
                                }
                            }
                            else
                            {
                                strCodeofColumn = m_lstColumns[k - 1];
                            }

                            if (m_dicRowCodeDesc.ContainsValue(dr[mc_TableItem].ToString()))
                            {
                                if (strOldRowItem != dr[mc_TableItem].ToString())
                                {
                                    foreach (KeyValuePair<string, string> kvp in m_dicRowCodeDesc)
                                    {
                                        if (kvp.Value.Equals(dr[mc_TableItem].ToString()))
                                        {
                                            strCodeofRow = kvp.Key;
                                            break;
                                        }
                                    }
                                    strOldRowItem = dr[mc_TableItem].ToString();
                                }
                            }
                            else
                            {
                                strCodeofRow = dr[mc_TableItem].ToString();
                            }

                            //判断并赋值
                            foreach (string r in m_lstDBResults)
                            {
                                string[] s = r.Split(',');
                                if (strCodeofRow + "," + strCodeofColumn == s[0] + "," + s[1])
                                {
                                    dr[k] = Math.Round(Convert.ToDecimal(s[2]), 2);
                                    break;
                                }
                            }

                            if (k == dr.ItemArray.Length - 1)
                            {
                                dr[k] = dblHeji;
                            }
                            else
                            {
                                dblHeji = dblHeji + Convert.ToDouble(dr[k]);
                            }

                        }//end for (int k = 1; k < dr.ItemArray.Length; k++)

                    }//end foreach (DataRow dr in dDataTableA.Rows)

                }
                else
                {
                    MessageBox.Show("统计内容记录为空，统计终止", "提示!");
                    this.Cursor = Cursors.Default;
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误!");
                this.Cursor = Cursors.Default;
                return;
            }

            #region Export to Excel file

            DateTime now = DateTime.Now;
            SaveFileDialog objSave = new SaveFileDialog();
            objSave.Filter = "电子表格(*.xls)|*.xls|所有文件(*.*)|*.*";
            objSave.FilterIndex = 0;
            objSave.RestoreDirectory = true;
            objSave.AddExtension = true;
            objSave.CreatePrompt = true;
            objSave.Title = "导出" + mc_strNameReport_Pipes;
            objSave.FileName = mc_strNameReport_Pipes + now.Year.ToString().PadLeft(2, '0') + now.Month.ToString().PadLeft(2, '0') + now.Day.ToString().PadLeft(2, '0')
                + now.Hour.ToString().PadLeft(2, '0') + now.Minute.ToString().PadLeft(2, '0') + now.Second.ToString().PadLeft(2, '0') + ".xls";
            try
            {
                if (objSave.ShowDialog() == DialogResult.OK)
                {
                    StringWriter sw = new StringWriter();
                    sw.WriteLine("撤除管线长度统计月报");
                    sw.WriteLine("");
                    sw.WriteLine("\t\t\t\t\t\t 填报日期: \t\t 单位：m");
                    string[] strColumnNames = new string[dDataTableA.Columns.Count];
                    object[] objLastRowofTable = new object[dDataTableA.Columns.Count];

                    for (int i = 0; i < dDataTableA.Rows.Count; i++)
                    {
                        for (int j = 0; j < dDataTableA.Columns.Count; j++)
                        {
                            DataColumn dcols = dDataTableA.Columns[j];
                            if (i == 0 && j == 0)
                            {
                                int k = 0;
                                foreach (DataColumn dc in dDataTableA.Columns)
                                {
                                    sw.Write(string.Format("{0}\t", dc.ColumnName));
                                    strColumnNames[k] = dc.ColumnName;
                                    k++;
                                }
                                sw.Write("\r\n");
                            }

                            if ((i == dDataTableA.Rows.Count - 1) && (j == dDataTableA.Columns.Count - 1))
                            {
                                int k = 1;
                                foreach (var c in strColumnNames)
                                {
                                    if (c != mc_TableItem)
                                    {
                                        Decimal sum = dDataTableA.AsEnumerable().Sum(a => a.Field<Decimal>(c));
                                        objLastRowofTable[k] = sum;
                                        k++;
                                    }
                                    else
                                    {
                                        objLastRowofTable[0] = mc_TableSubtotal;
                                    }
                                }
                            }

                            if (dcols.ColumnName != mc_TableItem)
                            {
                                sw.Write(dDataTableA.Rows[i][j].ToString().Trim() + "\t");
                            }
                            else
                            {
                                string sValue = dDataTableA.Rows[i][j].ToString();
                                sw.Write(sValue + "\t");
                            }
                        }
                        sw.Write("\r\n");
                    }

                    foreach (var o in objLastRowofTable)
                    {
                        sw.Write(o.ToString() + "\t");
                    }

                    sw.WriteLine("\r\n 主管领导:\t\t财务负责人:\t\t审核人:\t\t制表人:");

                    byte[] s = System.Text.Encoding.GetEncoding("gb2312").GetBytes(sw.ToString());
                    FileStream fs = new FileStream(objSave.FileName, FileMode.Create, FileAccess.Write);
                    fs.Write(s, 0, s.Length);
                    fs.Close();
                    if (MessageBox.Show("导出完毕,点击【确定】打开！", "提示", MessageBoxButtons.OK) == DialogResult.OK)
                    {
                        string strMacroName = "PERSONAL.XLSB!报废管线统计表";
                        OpenFolderAndSelectFile(objSave.FileName, mc_strExcelTemplate, strMacroName);
                    }

                }
                else
                {
                    this.Cursor = Cursors.Default;
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "提示");
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                m_lstRow.Clear();
                m_lstColumns.Clear();
                //m_pFeatureLayer = null;
                m_dicColumnCodeDesc.Clear();
                m_dicRowCodeDesc.Clear();
                m_lstDBResults.Clear();
            }
            #endregion
        }

        private string getLayerID(string strLayerName)
        {
            try
            {
                string LayerID, strSQL;
                OleDbDataReader adoReader = null;
                strSQL = string.Format("select layer_id from sde.layers where Table_name='{0}'", strLayerName);
                if (m_objDBCon == null)
                    m_objDBCon = CDBCon.GetInstance();
                m_objDBCon.ExecuteSQLReturn(strSQL, ref adoReader);
                if (adoReader.Read())
                {
                    return adoReader.GetValue(0).ToString();
                }
                else
                {
                    return "0";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误！");
                return "0";
            }
        }

        private void OpenFolderAndSelectFile(string fileFullName, string strTemplateFileName, string strMacro4Run)
        {
            if (!File.Exists(strTemplateFileName))
            {
                MessageBox.Show("模板文件不存在，请联系技术人员!");
                return;
            }
            Excel.Application ExcelApp = new Excel.Application();
            object missing = Missing.Value;

            try
            {
                //打开文件
                ExcelApp.Application.Workbooks.Open(fileFullName,
                missing,
                missing,
                missing,
                missing,
                missing,
                missing,
                missing,
                missing,
                missing,
                missing,
                missing,
                missing);

                ExcelApp.Visible = true;
                ExcelApp.DisplayAlerts = false;
                ExcelApp.AlertBeforeOverwriting = false;

                ExcelApp.Run(strMacro4Run, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        //打印路由本
        private void btnPrintRoute_Click(object sender, EventArgs e)
        {
            RouteRecordsList rrl = new RouteRecordsList();
            rrl.Init(m_application);
            this.TopMost = false;
            this.Visible = false;
            rrl.Show();
        }




    }
}
