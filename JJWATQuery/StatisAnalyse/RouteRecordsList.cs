using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using AOBaseLibC;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using JJWATBaseLibC;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;


namespace JJWATQuery
{
    public partial class RouteRecordsList : Form
    {
        private IApplication m_Application;

        private TuFuHaoModels models;
        private IMxDocument pMxdoc;
        private static AFMap m_objMap = new AFMap();
        private IFeatureLayer m_pFLayer;

        private const string mc_strNameofTFH2000 = "网格2000";
        private const string mc_strNameofTFH10000 = "网格10000";
        //private const string mc_strExcelTemplate = @"C:\Users\Administrator\AppData\Roaming\Microsoft\Excel\XLSTART\PERSONAL.XLSB";
        private const string mc_strExcelTemplate = @"d:\JJWATER\DLLS\PERSONAL.XLSB";

        private CDBCon m_objDBCon = null;
        private SysParameters m_CurParameter;
        private string m_strCurScale;//2000,10000
        private string m_strExprtFileName = string.Empty;
        private string m_strTableName = string.Empty;//当前搜索的表名；

        private OleDbDataReader adoReader = null;
        private DataTable m_Table;
        

        public RouteRecordsList()
        {
            InitializeComponent();            
        }

        internal bool Init(IApplication app)
        {
            m_Application = app;
            pMxdoc = (m_Application.Document) as IMxDocument;
            m_objMap.Map = pMxdoc.FocusMap;
            m_CurParameter = SysParameters.GetInstance();
            m_objDBCon = CDBCon.GetInstance();
            m_strCurScale = "2000";

            var objFeatureLayer = m_objMap.GetLayerByName(mc_strNameofTFH2000);
            if (objFeatureLayer != null)
            {
                m_pFLayer = objFeatureLayer.FeatureLayer;
            }
            else
            {
                objFeatureLayer = m_objMap.GetLayerByName(mc_strNameofTFH10000);
                if (objFeatureLayer != null)
                {
                    m_pFLayer = objFeatureLayer.FeatureLayer;
                }
                else
                {
                    MessageBox.Show("确认已加载网格图层！","操作提示");
                    return false;
                }
            }
            return true;
        }

        private void RouteRecordsList_Load(object sender, EventArgs e)
        {
            GetsAllTufuhao();
            this.cmbValveType.Visible=(rdobtnValve.Checked==true);
            if (rdobtnValve.Checked == true)
            {
                cmbValveType.Text = "路闸";
            }
        }

        private void GetsAllTufuhao()
        {
            models = new TuFuHaoModels();

            if (rdoBtn2000.Checked == false)
            {
                m_strCurScale = "10000";
            }
            else
            {
                m_strCurScale = "2000";
            }
            string strSQL = @"select cname from " + m_CurParameter.GWUserName + ".wg" + m_strCurScale + " order by cname";
            adoReader = null;
            m_objDBCon.ExecuteSQLReturn(strSQL, ref adoReader);
            if (adoReader.HasRows)
            {
                while (adoReader.Read())
                {
                    TuFuHaoModel mode = new TuFuHaoModel()
                    {
                        Tfh = adoReader["cname"].ToString()
                    };
                    models.Add(mode);
                }
            }
        }


        private void btnTracing_Click(object sender, EventArgs e)
        {            
            
            TuFuHaoModel InputTfh = new TuFuHaoModel() { Tfh = txtBtfh.Text };
            THFComparer scomparer = new THFComparer();
            m_Table = new DataTable();
            DataTable objTableZeroValve = new DataTable();
            DataTable objTableNoZeroValve = new DataTable();
            bool bWriteColumnsHead = false;


            #region 构建sql语句[全部]:测压还没有确定表名
            string strValveType = string.Empty;
            string tfh = InputTfh.Tfh;
            string strSQL = string.Empty;

            if (rdobtnValve.Checked == true)
            {
                //闸门
                m_strExprtFileName = InputTfh.Tfh + " " + cmbValveType.Text;
                m_strTableName = "valve";

                if (cmbValveType.Text == "路闸")
                    strValveType = "0";
                else
                    strValveType = "1";               

            strSQL =              
                    "select t.oldno as 编号,t.addr as 地址,t.diameter as 管径 from "+
                    m_CurParameter.GWUserName + "." + m_strTableName + " t " +
                    " where t.oldno like '{0}-" + strValveType + "-%'  " +
                    " order by cast(substr(t.oldno, instr(t.oldno,'-',1,3)+1) as decimal)"
                    ;         

            }
            else if (rdobtnHydrant.Checked == true)
            {
                //消火栓
                m_strExprtFileName = InputTfh.Tfh + " " + rdobtnHydrant.Text;
                m_strTableName = "hydrant";
               
                strSQL = 
                            "select t.oldno as 编号,t.addr as 地址,t.diameter as 管径 from " +
                            m_CurParameter.GWUserName + "." + m_strTableName + " t " +
                            " where t.oldno like '{0}-%'  " +
                            " order by cast(substr(t.oldno, instr(t.oldno,'-',1,2)+1) as decimal";
            }
            else if (rdoBtnGasValve.Checked == true)
            {
                //排气阀门
                m_strExprtFileName = InputTfh.Tfh + " " + rdoBtnGasValve.Text;
                m_strTableName = "gasvalve";

               

                strSQL = 
                           "select t.oldno as 编号,t.addr as 地址,t.diameter as 管径 from " +
                           m_CurParameter.GWUserName + "." + m_strTableName + " t " +
                           " where t.oldno like '{0}-%'  " +
                           " order by cast(substr(t.oldno, instr(t.oldno,'-',1,2)+1) as decimal)";

            }
            else if (rdobtnBaseWell.Checked == true)
            {
                //检查井
                m_strExprtFileName = InputTfh.Tfh + " " + rdobtnBaseWell.Text;
                m_strTableName = "basewell";                

                strSQL = 
                           "select t.oldno as 编号,t.addr as 地址,t.diameter as 管径 from " +
                           m_CurParameter.GWUserName + "." + m_strTableName + " t " +
                           " where t.oldno like '{0}-%'  " +
                           " order by cast(substr(t.oldno, instr(t.oldno,'-',1,2)+1) as decimal)";

            }
            else if (rdoBtnFluxMeasure.Checked == true)
            {
                //测流
                m_strExprtFileName = InputTfh.Tfh + " " + rdoBtnFluxMeasure.Text;
                m_strTableName = "fluxmeasure";              

                strSQL = 
                           "select t.oldno as 编号,t.addr as 地址,t.diameter as 管径 from " +
                           m_CurParameter.GWUserName + "." + m_strTableName + " t " +
                           " where t.oldno like '{0}-%'  " +
                           " order by cast(substr(t.oldno, instr(t.oldno,'-',1,1)+1) as decimal)";
            }
            else if (rdobtnMudValve.Checked == true)
            {
                //排泥
                m_strExprtFileName = InputTfh.Tfh + " " + rdobtnMudValve.Text;
                m_strTableName = "mudvalve";               

                strSQL = 
                           "select t.oldno as 编号,t.addr as 地址,t.diameter as 管径 from " +
                           m_CurParameter.GWUserName + "." + m_strTableName + " t " +
                           " where t.oldno like '{0}-%'  " +
                           " order by cast(substr(t.oldno, instr(t.oldno,'-',1,2)+1) as decimal)";
            }
            else if (radioButton7.Checked == true)
            {
                //测压:待定
                m_strExprtFileName = InputTfh.Tfh + " " + radioButton7.Text;
                m_strTableName = "PREMEASURE";                

                strSQL = 
                           "select t.oldno as 编号,t.addr as 地址,t.diameter as 管径 from " +
                           m_CurParameter.GWUserName + "." + m_strTableName + " t " +
                           " where t.oldno like '{0}-%'  " +
                           " order by cast(substr(t.oldno, instr(t.oldno,'-',1,2)+1) as decimal)";
            }
            #endregion


            if (chkBoxAll.Checked == true)
            {
                #region 选中全部                
                SaveFileDialog saveFileDialog = new SaveFileDialog();

                object missing = Missing.Value;
                

                int iWorkSheet=1;
                string strFilePath = string.Empty;
                string strFullName = string.Empty;

                    foreach (var model in models)
                    {
                        string sql = string.Format(strSQL, model.Tfh);
                        
                        string strFileName = string.Empty;
                        m_strExprtFileName = model.Tfh + " " + cmbValveType.Text;
                        try
                        {
                            Microsoft.Office.Interop.Excel.Application m_objExcel = new Microsoft.Office.Interop.Excel.Application();
                            Microsoft.Office.Interop.Excel.Workbooks m_objWorkBooks = m_objExcel.Workbooks;
                            Microsoft.Office.Interop.Excel.Workbook m_objBook = m_objWorkBooks.Add(true);
                            Microsoft.Office.Interop.Excel.Sheets m_objWorkSheets = m_objBook.Sheets;
                            Microsoft.Office.Interop.Excel.Worksheet m_objWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)m_objWorkSheets[1];

                            adoReader = null;
                            m_objDBCon.ExecuteSQLReturn(sql, ref adoReader);
                            if (adoReader.HasRows)
                            {
                                this.Cursor = Cursors.WaitCursor;
                                m_Table.Load(adoReader);

                                objTableZeroValve = m_Table.Clone();
                                objTableNoZeroValve = m_Table.Clone();

                                #region 导出到EXCEL

                                saveFileDialog.Filter = "Execl  files  (*.xls)|*.xls";
                                saveFileDialog.FilterIndex = 0;                               
                                saveFileDialog.RestoreDirectory = true;
                                saveFileDialog.CreatePrompt = true;
                                saveFileDialog.Title = "导出Excel文件到";
                                DateTime objTimeNow = DateTime.Now;
                                saveFileDialog.FileName = m_strExprtFileName + "_" + objTimeNow.Year.ToString().PadLeft(2) +
                                                                            objTimeNow.Month.ToString().PadLeft(2, '0') +
                                                                            objTimeNow.Day.ToString().PadLeft(2, '0') + "-" +
                                                                            objTimeNow.Hour.ToString().PadLeft(2, '0') +
                                                                            objTimeNow.Minute.ToString().PadLeft(2, '0') +
                                                                            objTimeNow.Second.ToString().PadLeft(2, '0');

                                

                                if (iWorkSheet == 1)
                                {
                                    saveFileDialog.CreatePrompt = false;
                                    strFileName = saveFileDialog.FileName+".xls";
                                    if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                                        return;
                                    strFilePath = saveFileDialog.FileName.Replace(strFileName, "");
                                    strFullName = saveFileDialog.FileName;
                                }
                                else
                                {
                                    strFullName = strFilePath + saveFileDialog.FileName + ".xls";
                                    System.IO.FileStream fs = new FileStream(strFullName, FileMode.Create);
                                    fs.SetLength(0);
                                    fs.Close();
                                    fs.Dispose();

                                }                                

                                string strBHLast = string.Empty;

                                for (int i = 0; i < m_Table.Rows.Count; i++)
                                {
                                    //编号末段不连续，则空一行；
                                    //编号末段0开头
                                    //134-18-0-01 0闸，需放到最后；
                                    string strBHNow = GetLastNo(m_Table.Rows[i][0].ToString().Trim());

                                    if (strBHNow == "")
                                    {
                                        continue;
                                    }
                                    if (GetLastNo(strBHNow).Substring(0, 1) == "0")
                                    {
                                        //是0闸
                                        objTableZeroValve.Rows.Add(m_Table.Rows[i].ItemArray);
                                    }
                                    else if (strBHLast != "" && GetLastNo(strBHLast).Substring(0, 1) != "0" && Convert.ToInt32(GetLastNo(strBHNow)) - Convert.ToInt32(strBHLast) != 1)
                                    {
                                        //非0闸但不连续,输出n-1个空行，并输出当前记录
                                        int icount = Convert.ToInt32(GetLastNo(strBHNow)) - Convert.ToInt32(strBHLast);
                                        for (int j = 1; j < icount; j++)
                                        {
                                            objTableNoZeroValve.Rows.Add();//m_Table.Rows[i].ItemArray
                                        }
                                        objTableNoZeroValve.Rows.Add(m_Table.Rows[i].ItemArray);
                                    }
                                    else
                                    {
                                        //正常情况
                                        objTableNoZeroValve.Rows.Add(m_Table.Rows[i].ItemArray);
                                    }
                                    strBHLast = GetLastNo(m_Table.Rows[i][0].ToString().Trim());

                                }
                                //输出结果
                                int index = 0;
                                if (objTableNoZeroValve.Rows.Count > 0)
                                {
                                    index = (objTableNoZeroValve.Columns.Count + 1) % 2 == 0 ? (objTableNoZeroValve.Columns.Count + 1) / 2 : Convert.ToInt32((objTableNoZeroValve.Columns.Count + 1) / 2) + 1;
                                    m_objExcel.Cells[1, index] = m_strExprtFileName;

                                }

                                DataColumn dcols = m_Table.Columns[0];
                                if (bWriteColumnsHead == false)
                                {
                                    int jj = 0;
                                    foreach (DataColumn dc in objTableNoZeroValve.Columns)
                                    {
                                        m_objExcel.Cells[2, jj + 1] = dc.ColumnName;
                                        jj++;
                                    }
                                    bWriteColumnsHead = true;
                                }

                                for (int i = 0; i < objTableNoZeroValve.Rows.Count; i++)
                                {
                                    //m_objExcel.Cells[i + 3, 1] = objTableNoZeroValve.Rows[i].HeaderCell.Value;
                                    for (int j = 0; j < objTableNoZeroValve.Columns.Count; j++)
                                    {
                                        m_objExcel.Cells[i + 3, j + 1] = objTableNoZeroValve.Rows[i][objTableNoZeroValve.Columns[j]].ToString();
                                    }
                                }

                                //是否存在0闸？
                                if (objTableZeroValve.Rows.Count > 0)
                                {
                                    for (int i = 0; i < objTableZeroValve.Rows.Count; i++)
                                    {
                                        for (int j = 0; j < objTableZeroValve.Columns.Count; j++)
                                        {

                                            m_objExcel.Cells[i + objTableNoZeroValve.Rows.Count, j + 1] = objTableZeroValve.Rows[i][objTableZeroValve.Columns[j]].ToString();
                                        }
                                        
                                    }
                                }

                                //题目标黑                            
                                Microsoft.Office.Interop.Excel.Range rangeTitle = m_objWorkSheet.get_Range(m_objExcel.Cells[1, index], m_objExcel.Cells[1, index]);
                                rangeTitle.Font.Size = 14;
                                rangeTitle.Font.Bold = true;

                                //合并单元格
                                rangeTitle.Merge(rangeTitle.MergeCells);
                                m_objWorkSheet.get_Range("A1", m_objExcel.Cells[1, objTableNoZeroValve.Columns.Count]).Merge(m_objWorkSheet.get_Range("A1", m_objExcel.Cells[1, objTableNoZeroValve.Columns.Count]).MergeCells);
                                Microsoft.Office.Interop.Excel.Range rangeContent = 
                                    m_objWorkSheet.get_Range( 
                                                            m_objExcel.Cells[1, 1], 
                                                            m_objExcel.Cells[objTableNoZeroValve.Rows.Count + objTableZeroValve.Rows.Count-1,
                                                                             objTableNoZeroValve.Columns.Count]
                                                             );
                                rangeContent.EntireColumn.AutoFit();
                                rangeContent.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                m_objExcel.DisplayAlerts = false;
                                m_objExcel.AlertBeforeOverwriting = false;

                                //自动调整列宽
                                rangeContent.EntireColumn.AutoFit();
                                rangeContent.Borders.LineStyle = 1;
                                rangeContent.BorderAround(
                                    Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                                    Microsoft.Office.Interop.Excel.XlBorderWeight.xlThick,
                                    Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                                    System.Drawing.Color.Black.ToArgb()
                                    );


                                //byte[] s = System.Text.Encoding.GetEncoding("gb2312").GetBytes(sw.ToString());
                                //FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write);
                                //fs.Write(s, 0, s.Length);
                                //fs.Close();
                                m_objBook.SaveAs(
                                        strFullName, 
                                        missing, 
                                        missing, 
                                        missing, 
                                        missing, 
                                        missing, 
                                        Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                                        missing,
                                        missing,
                                        missing, 
                                        missing, 
                                        missing
                                        );
                                m_objBook.Close(missing, missing, missing);
                                m_objExcel.Workbooks.Close();
                                m_objWorkBooks.Close();
                                m_objExcel.Quit();
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_objBook);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_objExcel);
                                GC.Collect();
                                m_objBook = null;
                                m_objExcel = null;

                                bWriteColumnsHead = false;
                                #endregion

                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "错误提示");
                            return;
                        }
                        finally
                        {
                            this.Cursor = Cursors.Default;
                            bWriteColumnsHead = false;                           
                            m_Table.Clear();
                        }
                        iWorkSheet++;

                       

                    }//end foreach

                

                if (MessageBox.Show("路由本制作完毕,点击确定打开", "操作提示", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    string strMacroName = "PERSONAL.XLSB!路由本";
                    OpenFolderAndSelectFile(saveFileDialog.FileName, mc_strExcelTemplate, strMacroName);
                }
                this.Cursor = Cursors.Default;

                #endregion
            }
            else
            {
                #region 非全部
                if (!models.Contains(InputTfh, scomparer))
                {
                    MessageBox.Show("当前网格" + m_strCurScale + "中没有该图幅！", "提示");
                    txtBtfh.Text = "";
                    return;
                }
                else
                {

                    #region 构建sql语句[单独]:测压还没有确定表名                    

                    if (rdobtnValve.Checked == true)
                    {
                        //闸门
                        m_strExprtFileName = InputTfh.Tfh + " " + cmbValveType.Text;
                        m_strTableName = "valve";

                        if (cmbValveType.Text == "路闸")
                            strValveType = "0";
                        else
                            strValveType = "1";

                        strSQL = @"select t.oldno as 编号,t.addr as 地址,t.diameter as 管径 from " +
                                    m_CurParameter.GWUserName + "." + m_strTableName + " t " +
                                    " where t.oldno like '" + tfh + "-" + strValveType + "-%'  " +
                                    " order by cast(substr(t.oldno, instr(t.oldno,'-',1,3)+1) as decimal)";
                    }
                    else if (rdobtnHydrant.Checked == true)
                    {
                        //消火栓
                        m_strExprtFileName = InputTfh.Tfh + " " + rdobtnHydrant.Text;
                        m_strTableName = "hydrant";

                        strSQL = @"select t.oldno as 编号,t.addr as 地址,t.diameter as 管径 from " +
                                    m_CurParameter.GWUserName + "." + m_strTableName + " t " +
                                    " where t.oldno like '" + tfh + "-%'  " +
                                    " order by cast(substr(t.oldno, instr(t.oldno,'-',1,2)+1) as decimal)";
                       
                    }
                    else if (rdoBtnGasValve.Checked == true)
                    {
                        //排气阀门
                        m_strExprtFileName = InputTfh.Tfh + " " + rdoBtnGasValve.Text;
                        m_strTableName = "gasvalve";

                        strSQL = @"select t.oldno as 编号,t.addr as 地址,t.diameter as 管径 from " +
                                   m_CurParameter.GWUserName + "." + m_strTableName + " t " +
                                   " where t.oldno like '" + tfh + "-%'  " +
                                   " order by cast(substr(t.oldno, instr(t.oldno,'-',1,2)+1) as decimal)";                       

                    }
                    else if (rdobtnBaseWell.Checked == true)
                    {
                        //检查井
                        m_strExprtFileName = InputTfh.Tfh + " " + rdobtnBaseWell.Text;
                        m_strTableName = "basewell";

                        strSQL = @"select t.oldno as 编号,t.addr as 地址,t.diameter as 管径 from " +
                                   m_CurParameter.GWUserName + "." + m_strTableName + " t " +
                                   " where t.oldno like '" + tfh + "-%'  " +
                                   " order by cast(substr(t.oldno, instr(t.oldno,'-',1,2)+1) as decimal)";                       

                    }
                    else if (rdoBtnFluxMeasure.Checked == true)
                    {
                        //测流
                        m_strExprtFileName = InputTfh.Tfh + " " + rdoBtnFluxMeasure.Text;
                        m_strTableName = "fluxmeasure";

                        strSQL = @"select t.oldno as 编号,t.addr as 地址,t.diameter as 管径 from " +
                                   m_CurParameter.GWUserName + "." + m_strTableName + " t " +
                                   " where t.oldno like '" + tfh + "-%'  " +
                                   " order by cast(substr(t.oldno, instr(t.oldno,'-',1,1)+1) as decimal)";
                       
                    }
                    else if (rdobtnMudValve.Checked == true)
                    {
                        //排泥
                        m_strExprtFileName = InputTfh.Tfh + " " + rdobtnMudValve.Text;
                        m_strTableName = "mudvalve";

                        strSQL = @"select t.oldno as 编号,t.addr as 地址,t.diameter as 管径 from " +
                                   m_CurParameter.GWUserName + "." + m_strTableName + " t " +
                                   " where t.oldno like '" + tfh + "-%'  " +
                                   " order by cast(substr(t.oldno, instr(t.oldno,'-',1,2)+1) as decimal)";
                       
                    }
                    else if (radioButton7.Checked == true)
                    {
                        //测压:待定
                        m_strExprtFileName = InputTfh.Tfh + " " + radioButton7.Text;
                        m_strTableName = "PREMEASURE";

                        strSQL = @"select t.oldno as 编号,t.addr as 地址,t.diameter as 管径 from " +
                                   m_CurParameter.GWUserName + "." + m_strTableName + " t " +
                                   " where t.oldno like '" + tfh + "-%'  " +
                                   " order by cast(substr(t.oldno, instr(t.oldno,'-',1,2)+1) as decimal)";                       

                    }
                    #endregion
                    try
                    {
                        adoReader = null;
                        m_objDBCon.ExecuteSQLReturn(strSQL, ref adoReader);
                        if (adoReader.HasRows)
                        {
                            this.Cursor = Cursors.WaitCursor;
                            m_Table.Load(adoReader);
                        }

                        objTableZeroValve = m_Table.Clone();

                        #region 导出到EXCEL
                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.Filter = "Execl  files  (*.xls)|*.xls";
                        saveFileDialog.FilterIndex = 0;
                        saveFileDialog.RestoreDirectory = true;
                        saveFileDialog.CreatePrompt = true;
                        saveFileDialog.Title = "导出Excel文件到";
                        DateTime objTimeNow = DateTime.Now;
                        saveFileDialog.FileName = m_strExprtFileName + "_" + objTimeNow.Year.ToString().PadLeft(2) +
                                                                    objTimeNow.Month.ToString().PadLeft(2, '0') +
                                                                    objTimeNow.Day.ToString().PadLeft(2, '0') + "-" +
                                                                    objTimeNow.Hour.ToString().PadLeft(2, '0') +
                                                                    objTimeNow.Minute.ToString().PadLeft(2, '0') +
                                                                    objTimeNow.Second.ToString().PadLeft(2, '0');


                        if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                            return;

                        StringWriter sw = new StringWriter();
                        sw.WriteLine(m_strExprtFileName);
                        string strBHLast = string.Empty;

                        for (int i = 0; i < m_Table.Rows.Count; i++)
                        {
                            //编号末段不连续，则空一行；
                            //编号末段0开头
                            //134-18-0-01 0闸，需放到最后；
                            string strBHNow = GetLastNo(m_Table.Rows[i][0].ToString().Trim());

                            if (strBHNow == "")
                            {
                                continue;
                            }
                            if (GetLastNo(strBHNow).Substring(0, 1) == "0")
                            {
                                //是0闸
                                objTableZeroValve.Rows.Add(m_Table.Rows[i].ItemArray);
                            }
                            else if (strBHLast != "" && GetLastNo(strBHLast).Substring(0, 1) != "0" && Convert.ToInt32(GetLastNo(strBHNow)) - Convert.ToInt32(strBHLast) != 1)
                            {
                                //非0闸但不连续,输出n-1个空行，并输出当前记录
                                int icount = Convert.ToInt32(GetLastNo(strBHNow)) - Convert.ToInt32(strBHLast);
                                for (int j = 1; j < icount; j++)
                                {
                                    sw.Write("\n");
                                }
                                for (int j = 0; j < m_Table.Columns.Count; j++)
                                {
                                    DataColumn dcols = m_Table.Columns[j];
                                    //写入列头
                                    if (bWriteColumnsHead == false)
                                    {
                                        foreach (DataColumn dc in m_Table.Columns)
                                        {
                                            sw.Write(string.Format("{0}\t", dc.ColumnName));
                                            bWriteColumnsHead = true;
                                        }
                                        sw.Write("\n");
                                    }
                                    sw.Write(m_Table.Rows[i][j].ToString().Trim() + "\t");
                                }
                                sw.Write("\n");
                            }
                            else
                            {
                                //正常情况
                                for (int j = 0; j < m_Table.Columns.Count; j++)
                                {
                                    DataColumn dcols = m_Table.Columns[j];
                                    //写入列头
                                    if (bWriteColumnsHead==false)
                                    {
                                        foreach (DataColumn dc in m_Table.Columns)
                                        {
                                            sw.Write(string.Format("{0}\t", dc.ColumnName));
                                        }
                                        bWriteColumnsHead = true;
                                        sw.Write("\n");
                                    }
                                    sw.Write(m_Table.Rows[i][j].ToString().Trim() + "\t");
                                }
                                sw.Write("\n");
                            }
                            strBHLast = GetLastNo(m_Table.Rows[i][0].ToString().Trim());

                        }

                        //是否存在0闸？
                        if (objTableZeroValve.Rows.Count > 0)
                        {
                            for (int i = 0; i < objTableZeroValve.Rows.Count; i++)
                            {
                                for (int j = 0; j < objTableZeroValve.Columns.Count; j++)
                                {
                                    //仅有0闸，需要写列头
                                    if (bWriteColumnsHead == false)
                                    {
                                        DataColumn dcols = m_Table.Columns[j];                                    
                                        if (i == 0 && j == 0)
                                        {
                                            foreach (DataColumn dc in m_Table.Columns)
                                            {
                                                sw.Write(string.Format("{0}\t", dc.ColumnName));
                                            }
                                            sw.Write("\n");
                                        }
                                        bWriteColumnsHead = true;
                                    }

                                    sw.Write(objTableZeroValve.Rows[i][j].ToString().Trim() + "\t");
                                }
                                sw.Write("\n");
                            }
                        }

                        byte[] s = System.Text.Encoding.GetEncoding("gb2312").GetBytes(sw.ToString());
                        FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write);
                        fs.Write(s, 0, s.Length);
                        fs.Close();
                        if (MessageBox.Show("路由本制作完毕,点击确定打开", "操作提示",MessageBoxButtons.OK)==DialogResult.OK)
                        {
                            string strMacroName = "PERSONAL.XLSB!路由本";
                            OpenFolderAndSelectFile(saveFileDialog.FileName, mc_strExcelTemplate, strMacroName); 
                        }
                        this.Cursor = Cursors.Default;
                        bWriteColumnsHead = false;
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "错误提示");
                        return;
                    }
                    finally
                    {
                        bWriteColumnsHead = false;
                        this.Cursor = Cursors.Default;
                    }

                }
                #endregion
            }
            
            
        }


        /// <summary>
        /// 获取编号最后一段
        /// </summary>
        /// <param name="OLDNO">设备编号</param>
        /// <returns></returns>
        private string GetLastNo(string OLDNO)
        {
            if (OLDNO.Trim() != "")
            {
                string[] sArray = OLDNO.Split('-');
                return sArray[sArray.Length - 1];
            }
            else
            {
                return string.Empty;
            }
        }
        
        private void rdobtnValve_CheckedChanged(object sender, EventArgs e)
        {
            if (rdobtnValve.Checked == true)
            {
                 rdoBtn2000.Checked = true;
                rdoBtn10000.Checked = false;
            }
            this.cmbValveType.Visible = (rdobtnValve.Checked == true);
        }

        private void rdobtnHydrant_CheckedChanged(object sender, EventArgs e)
        {
            if (rdobtnHydrant.Checked == true)
            {
                 rdoBtn2000.Checked = true;
                rdoBtn10000.Checked = false;
            }
        }

        private void rdoBtnGasValve_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoBtnGasValve.Checked == true)
            {
                 rdoBtn2000.Checked = true;
                rdoBtn10000.Checked = false;
            }
        }

        private void rdobtnBaseWell_CheckedChanged(object sender, EventArgs e)
        {
            //检查井编号2000
            if (rdobtnBaseWell.Checked == true)
            {
                rdoBtn2000.Checked = true;
                rdoBtn10000.Checked = false;
                
            }
        }

        private void rdoBtnFluxMeasure_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoBtnFluxMeasure.Checked == true)
            {
                rdoBtn2000.Checked = false;
                rdoBtn10000.Checked = true;

            }
        }

        private void rdobtnMudValve_CheckedChanged(object sender, EventArgs e)
        {
            if (rdobtnMudValve.Checked == true)
            {
                rdoBtn2000.Checked = false;
                rdoBtn10000.Checked = true;

            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked == true)
            {
                rdoBtn2000.Checked = false;
                rdoBtn10000.Checked = true;

            }
        }

        private void txtBtfh_TextChanged(object sender, EventArgs e)
        {
            string strtfh = txtBtfh.Text;
            if (string.IsNullOrEmpty(strtfh))
            { return; }
            var item = models.Where(a => a.Tfh.Contains(strtfh)).GroupBy(a => a.Tfh).Take(5);
            tfhList.Items.Clear();
            foreach (var m in item)
            {
                tfhList.Items.Add(m.Key);
            }
        }

        private void tfhList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tfhList.SelectedIndex > -1)
                txtBtfh.Text = tfhList.SelectedItem.ToString();
            else
                return;
        }

        private void rdoBtn2000_CheckedChanged(object sender, EventArgs e)
        {
            GetsAllTufuhao();
        }

        private void rdoBtn10000_CheckedChanged(object sender, EventArgs e)
        {
            GetsAllTufuhao();
        }

        private void OpenFolderAndSelectFile(string fileFullName, string strTemplateFileName, string strMacro4Run)
        {
            // 打开文件所在的目录
            //System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
            //psi.Arguments = "/e,/select," + fileFullName;
            //System.Diagnostics.Process.Start(psi);
            //System.Diagnostics.Process.Start(fileFullName);
            
            //判断模板是否存在
            //if (!File.Exists(strTemplateFileName))
            //{
            //    MessageBox.Show("模板文件不存在，请联系技术人员!");
            //    return;
            //}
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

        private void chkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxAll.Checked == true)
            {
                txtBtfh.ReadOnly = true;
                txtBtfh.Text = "";
                tfhList.Items.Clear();                
            }
            else
            {
                txtBtfh.ReadOnly = false;
            }
        }

        /// <summary>
        /// 转出Excel文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //SaveFileDialog saveFileDialog = new SaveFileDialog();
                //saveFileDialog.Filter = "Execl files(*.xlsx)|*.xls";
                //saveFileDialog.FilterIndex = 0;
                //saveFileDialog.RestoreDirectory = true;
                //saveFileDialog.CreatePrompt = true;
                //saveFileDialog.Title = "导出Excel文件到";
                //if (saveFileDialog.ShowDialog() == DialogResult.OK)
                //{
                //    this.Cursor = Cursors.WaitCursor;
                //    object missing = Missing.Value;
                //    Microsoft.Office.Interop.Excel.Application m_objExcel = new Microsoft.Office.Interop.Excel.Application();
                //    Microsoft.Office.Interop.Excel.Workbooks m_objWorkBooks = m_objExcel.Workbooks;
                //    Microsoft.Office.Interop.Excel.Workbook m_objBook = m_objWorkBooks.Add(true);
                //    Microsoft.Office.Interop.Excel.Sheets m_objWorkSheets = m_objBook.Sheets; ;
                //    Microsoft.Office.Interop.Excel.Worksheet m_objWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)m_objWorkSheets[1];
                //    int index = (dataGridView1.Columns.Count + 1) % 2 == 0 ? (dataGridView1.Columns.Count + 1) / 2 : Convert.ToInt32((dataGridView1.Columns.Count + 1) / 2) + 1;
                //    m_objExcel.Cells[1, index] = "行政区统计报表";
                //    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                //    {
                //        m_objExcel.Cells[2, i + 2] = dataGridView1.Columns[i].HeaderText;
                //    }
                //    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                //    {
                //        m_objExcel.Cells[i + 3, 1] = dataGridView1.Rows[i].HeaderCell.Value;
                //        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                //        {
                //            m_objExcel.Cells[i + 3, j + 2] = dataGridView1.Rows[i].Cells[j].Value;
                //        }
                //    }
                //    Microsoft.Office.Interop.Excel.Range rangeTitle = m_objWorkSheet.get_Range(m_objExcel.Cells[1, index], m_objExcel.Cells[1, index]);
                //    rangeTitle.Font.Size = 14;
                //    rangeTitle.Font.Bold = true;
                //    //合并单元格
                //    rangeTitle.Merge(rangeTitle.MergeCells);
                //    m_objWorkSheet.get_Range("A1", m_objExcel.Cells[1, dataGridView1.Columns.Count + 1]).Merge(m_objWorkSheet.get_Range("A1", m_objExcel.Cells[1, dataGridView1.Columns.Count + 1]).MergeCells);
                //    Microsoft.Office.Interop.Excel.Range rangeContent = m_objWorkSheet.get_Range(m_objExcel.Cells[1, 1], m_objExcel.Cells[2 + dataGridView1.Rows.Count, dataGridView1.Columns.Count + 1]);
                //    rangeContent.EntireColumn.AutoFit();
                //    rangeContent.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                //    m_objExcel.DisplayAlerts = false;
                //    m_objExcel.AlertBeforeOverwriting = false;
                //    //自动调整列宽
                //    rangeContent.EntireColumn.AutoFit();
                //    rangeContent.Borders.LineStyle = 1;
                //    rangeContent.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThick, XlColorIndex.xlColorIndexAutomatic, System.Drawing.Color.Black.ToArgb());
                //    m_objBook.SaveAs(saveFileDialog.FileName, missing, missing, missing, missing, missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                //            missing, missing, missing, missing, missing);
                //    m_objBook.Close(missing, missing, missing);
                //    m_objExcel.Workbooks.Close();
                //    m_objWorkBooks.Close();
                //    m_objExcel.Quit();
                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(m_objBook);
                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(m_objExcel);
                //    GC.Collect();
                //    m_objBook = null;
                //    m_objExcel = null;
                //    this.Cursor = Cursors.Default;
                //    MessageBox.Show("转出Excel成功！！！");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
