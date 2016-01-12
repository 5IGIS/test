using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace JJWATQuery
{
    public class ClsOutExcel
    {
        static ProgressShow ps;
        public delegate void ProgressValueChangedHandle();
        public static event ProgressValueChangedHandle ProgressValueChanged;

        public void OutExcel(OleDbDataReader dbReader, int rowNum)
        {
            try
            {
                
                
                if (rowNum == 0)
                {
                    MessageBox.Show("数据库中没有数据！");
                    return;
                }
                
                System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
                sfd.Filter = ("Excel 文件(*.xls)|*.xls");
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string filename = sfd.FileName;  
                    if (System.IO.File.Exists(filename))  
                    {  
                        System.IO.File.Delete(filename);
                    }

                    Excel.Application m_Excel = new Excel.Application();
                    m_Excel.SheetsInNewWorkbook = 1;
                    Excel._Workbook m_Book = (Excel._Workbook)(m_Excel.Workbooks.Add(Missing.Value));
                    Excel._Worksheet m_Sheet = (Excel._Worksheet)(m_Excel.Worksheets.Add(Missing.Value));
                    m_Sheet.Name = "关阀方案";

                    ps = new ProgressShow();
                    ps.progressBar1.Maximum = rowNum;
                    //ps.progressBar1.Maximum = 500;
                    ps.progressBar1.Value = 0;
                    ps.Show();
                    ProgressValueChanged += new ProgressValueChangedHandle(co_ProgShow2Completed);

                    DataToSheet(dbReader, m_Book, m_Sheet, m_Excel, rowNum);

                    m_Book.SaveAs(filename, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                    m_Book.Close(false, Missing.Value, Missing.Value);
                    m_Excel.Quit();
                 
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(m_Book);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(m_Excel);
                    m_Book = null;
                    m_Sheet = null;
                    m_Excel = null;

                    ps.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void DataToSheet(OleDbDataReader dbReader, Excel._Workbook m_Book, Excel._Worksheet m_Sheet,Excel.Application m_Excel,int rowNum)
        {
            //Excel.Range r;//声明Range对象
            //r = m_Sheet.Range[m_Sheet.Cells[2, 1], m_Sheet.Cells[2, 1]];//为对象赋值

            int columnNum = dbReader.FieldCount;
            int index = columnNum % 2 == 0 ? columnNum / 2 : Convert.ToInt32(columnNum / 2) + 1;
            for (int i = 0; i < columnNum; i++)
            {
                m_Sheet.Cells[1, i + 1] = dbReader.GetName(i).ToString();
                //m_Excel.Cells[1, i + 1] = dbReader.GetName(i).ToString();
            }
            int intRow=0;
            string strColName;
            while(dbReader.Read())
            {
                for (int j = 0; j < columnNum; j++)
                {
                    strColName=dbReader.GetName(j).ToString();
                    m_Sheet.Cells[intRow + 2, j + 1] = dbReader[strColName].ToString();
                    //m_Excel.Cells[intRow + 2, j + 1] = dbReader[strColName].ToString();
                }
                intRow++;
                ProgressValueChanged();
                //if (intRow == 500)
                //{
                //    return;
                //}
            }
            Microsoft.Office.Interop.Excel.Range rangeContent = m_Sheet.Columns;
            rangeContent.AutoFit();
            //rangeContent.ColumnWidth = 13;
        }

        void co_ProgShow2Completed()
        {
            if (ps.progressBar1.Value < ps.progressBar1.Maximum)
            {
                ps.progressBar1.Value++;
                ps.lblNum.Text = ps.progressBar1.Value.ToString() + "/" + ps.progressBar1.Maximum.ToString();

                System.Windows.Forms.Application.DoEvents();
            }
        }

        
    }
}
