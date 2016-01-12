using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Data.OleDb;
using JJWATBaseLibC;

namespace JJWATQuery
{
    public partial class Frm_LoadOutEXCLE : Form
    {
        public strctData4ValveQuery m_obj_DataStrcut = new strctData4ValveQuery();//用于接受数据结构
        public Dictionary<string, IList<ClsResultSet>> DicHistory = new Dictionary<string, IList<ClsResultSet>>();
        private List<string> LsitCheckedIterms = new List<string>();
        private CDBCon m_objDBCON = CDBCon.GetInstance();
        public SysParameters m_CurParameter = null;
        public Frm_LoadOutEXCLE()
        {
            InitializeComponent();
        }


        private void GetCheckedItem()
        {
            //得到全部被选中的值
            LsitCheckedIterms.Clear();
            for (int j = 0; j < checkedListBox1.Items.Count; j++)
            {
                if (checkedListBox1.GetItemChecked(j))
                {
                    LsitCheckedIterms.Add(checkedListBox1.GetItemText(checkedListBox1.Items[j]));
                }
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Execl files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "导出距阀设备到Excel文件";
            DateTime objTimeNow = DateTime.Now;
            saveFileDialog.FileName = "距阀设备 " + objTimeNow.Year.ToString().PadLeft(2) + objTimeNow.Month.ToString().PadLeft(2, '0') + objTimeNow.Day.ToString().PadLeft(2, '0') + "-" + objTimeNow.Hour.ToString().PadLeft(2, '0') + objTimeNow.Minute.ToString().PadLeft(2, '0') + objTimeNow.Second.ToString().PadLeft(2, '0');
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName.IndexOf(":") < 0) return; //被点了"取消"
            Stream myStream = saveFileDialog.OpenFile();
            StreamWriter MySw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(0));
            GetCheckedItem();

            using (StreamWriter sw = MySw)
            {
                //加载第一行，表头
                string FirstRow = "";
                FirstRow += "距离序号\t设备图层\t阀门ID\t终止设备ID\t距离信息";
                MySw.WriteLine(FirstRow);
                //加载其余行，内容
                for (int i = 0; i < LsitCheckedIterms.Count; i++)
                {
                    //缓存中存在
                    string Key = LsitCheckedIterms[i];
                    if (DicHistory.Keys.Contains(LsitCheckedIterms[i]))
                    {
                       
                        for (int j = 0; j < DicHistory[Key].Count; j++)
                        {
                            string RowSth = null;
                            RowSth = string.Format("{0}\t{1}\t{2}\t{3}\t{4}", DicHistory[Key][j].DisId, DicHistory[Key][j].LayerName, DicHistory[Key][j].ValveID, DicHistory[Key][j].StopID, DicHistory[Key][j].Distance);
                            sw.WriteLine(RowSth);
                        }

                    }
                    //缓存中不存在的要访问数据库加载
                    else
                    {

                        if (m_obj_DataStrcut.bIsAll)
                        {
                            progressBar1.Visible = true;
                           
                            int CountNum = 0;
                            int NowValue = 0;
                            string strSQLQueryEquipment = string.Format("select DISID ,LAYERNAME, VALVEID,STOPID, DISTANCE ,VPATH from {0}.closevalvedistance where LAYERNAME='{1}' and DISTANCE between {2} and {3}", m_CurParameter.GWUserName, Key, m_obj_DataStrcut.dMinDistance, m_obj_DataStrcut.dMaxDistance);
                            string strSQLCount = string.Format("select count(*) from {0}.closevalvedistance where  LAYERNAME='{1}' and DISTANCE between {2} and {3}", m_CurParameter.GWUserName, Key, m_obj_DataStrcut.dMinDistance, m_obj_DataStrcut.dMaxDistance);
                            OleDbDataReader adoReader = null;
                            //获取符合条件的数据的条数用于进度条显示处理
                            m_objDBCON.ExecuteSQLReturn(strSQLCount, ref adoReader);
                            while (adoReader.Read())
                            {
                                CountNum = Convert.ToInt32(adoReader[0].ToString());
                                break;
                            }
                            progressBar1.Maximum = CountNum;                           
                            NowValue = 0;
                            adoReader = null;
                            m_objDBCON.ExecuteSQLReturn(strSQLQueryEquipment, ref adoReader);
                            IList<ClsResultSet> lis = new List<ClsResultSet>();
                            while (adoReader.Read())
                            {
                                string rowSth = null;                           
                                rowSth = string.Format("{0}\t{1}\t{2}\t{3}\t{4}", adoReader["DISID"].ToString(), adoReader["LAYERNAME"].ToString(), adoReader["VALVEID"].ToString(), adoReader["STOPID"].ToString(), adoReader["DISTANCE"].ToString());
                                sw.WriteLine(rowSth);
                                progressBar1.Value = NowValue++;
                            }
                            adoReader.Dispose();
                            progressBar1.Visible = false;
                           
                        }

                        else
                        {
                            progressBar1.Visible = true;
                            progressBar1.Maximum = m_obj_DataStrcut.lstStrValveIDs.Count;
                            for (int k = 0; k < m_obj_DataStrcut.lstStrValveIDs.Count; k++)
                            {
                                progressBar1.Value = k;
                                string strSqlRegion = string.Format("select DISID ,LAYERNAME, VALVEID,STOPID, DISTANCE ,VPATH from {0}.closevalvedistance where LAYERNAME='{1}' and VALVEID={4} and DISTANCE between {2} and {3}", m_CurParameter.GWUserName, Key, m_obj_DataStrcut.dMinDistance, m_obj_DataStrcut.dMaxDistance, m_obj_DataStrcut.lstStrValveIDs[k]);
                                OleDbDataReader adoReaderReginal = null;
                                m_objDBCON.ExecuteSQLReturn(strSqlRegion, ref adoReaderReginal);

                                while (adoReaderReginal.Read())
                                {
                                    string AnotherRowSth = null;
                                    AnotherRowSth = string.Format("{0}\t{1}\t{2}\t{3}\t{4}", adoReaderReginal["DISID"].ToString(), adoReaderReginal["LAYERNAME"].ToString(), adoReaderReginal["VALVEID"].ToString(), adoReaderReginal["STOPID"].ToString(), adoReaderReginal["DISTANCE"].ToString());
                                    sw.WriteLine(AnotherRowSth);

                                }

                            }
                            progressBar1.Visible = false;


                        }

                    }


                   
                }
                
                MessageBox.Show("转出Excel成功！", "提示");
            }
        }
          


        
                  
        
         
          
           
      

        private void Frm_LoadOutEXCLE_Load(object sender, EventArgs e)
        {

            checkedListBox1.Items.Clear();
            if (m_obj_DataStrcut.Pipelinequipment.Count == 0)
            {
                MessageBox.Show("遇见未知错误请联系开发人员", "提示");
                return;

            }

            else
            {
                foreach (string str in m_obj_DataStrcut.Pipelinequipment)
                {
                    if (!checkedListBox1.Items.Contains(str))
                    {
                        checkedListBox1.Items.Add(str);
                    }
                }
            
            
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAll.Checked)
            {
                checkNotAll.Checked = false;
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, true);

                }
            }
        }

        private void checkNotAll_CheckedChanged(object sender, EventArgs e)
        {
            if (checkNotAll.Checked == true)
            {
                checkAll.Checked = false;
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, false);

                }

            }
        }
    }
}
