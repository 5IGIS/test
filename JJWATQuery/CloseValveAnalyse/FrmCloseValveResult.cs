using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Geodatabase;
using AOBaseLibC;
using JJWATBaseLibC;
using System.Data.OleDb;
using System.Collections;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using AOBaseLibC.AFGeodatabase;

namespace JJWATQuery
{
    public partial class FrmQueryResult : Form
    {
        
        private IApplication m_pApp;        
        private string m_strMin = string.Empty;
        private string m_strMax = string.Empty;        
        private CDBCon m_objDBCON = null;
        private SysParameters m_CurParameter = null;                    
        private IMap m_pMap;
        private IMxDocument m_pMxdoc;
        private AFMap objMap = new AFMap();        
        private AOBaseLibC.AFGeodatabase.AFFeature objFeature;
        private AOBaseLibC.AFGeodatabase.AFFeatureLayer objFeatureLayer;
        List<string> m_strDisIDs = new List<string>();
        private List<AOBaseLibC.AFGeodatabase.AFFeatureLayer> m_objFeatureLayers = new List<AOBaseLibC.AFGeodatabase.AFFeatureLayer>();
        private IFeatureSelection m_pFeatureSelection;
        private Dictionary<string, string> DicValveCountAndClsidCount;
        private Dictionary<string, IList<int>> dicFeatures4Located = new Dictionary<string, IList<int>>();//layername&oids                 
        private const string mc_strNameOfRootNode = "关阀方案";
        private const string mc_TableValve = "VALVE";//阀门
        private const string mc_TablePIPESECTIONMAIN = "PIPESECTIONMAIN";//配水干线
        private const string mc_TablePIPESECTIONUSER = "PIPESECTIONUSER";//配水支线
        private const string mc_TablePIPESECTIONSOURCE = "PIPESECTIONSOURCE";//输水管线      
        private AOBaseLibC.AFCommon.AFConst.DataSourceType m_DataSourceType;//数据库类型，从parameter来 唐
        private IList<ShutdownValveScheme> lstQueryResult;     
        private strctData4ValveQuery dataFromQuery = new strctData4ValveQuery();
        private strctData4ValveQuery m_obj_DataStrcut = new strctData4ValveQuery();//用于接受数据结构
        private Dictionary<string, IList<ShutdownValveScheme>> m_dicTableSearchHistory = new Dictionary<string, IList<ShutdownValveScheme>>();
        private Dictionary<string, IList<ShutdownValveScheme>> m_dicTableSearchHistoryDetail = new Dictionary<string, IList<ShutdownValveScheme>>();
        List<string> lstDicResultsKeys = new List<string>();
      



        public FrmQueryResult()
        {
            InitializeComponent();
            //程序主入口;最后会用到
            //m_DataSourceType = m_CurParameter.GWDataSourceType;
            m_DataSourceType = AOBaseLibC.AFCommon.AFConst.DataSourceType.AFisOracle;
        }

        //zhuliang
        public void InitResultForm(strctData4ValveQuery DataStructedFromClick, IApplication Application)
        {
            m_obj_DataStrcut = DataStructedFromClick;
            m_pApp = Application;
            m_objDBCON = CDBCon.GetInstance();
            m_CurParameter = SysParameters.GetInstance();
        }

        public void InitForm(strctData4ValveQuery FrmData,IApplication Application)
        {          
                dataFromQuery = FrmData;
                m_pApp = Application;
                m_objDBCON = CDBCon.GetInstance();
                m_CurParameter = SysParameters.GetInstance();
                DicValveCountAndClsidCount = new Dictionary<string, string>();
                                  
        }
        /// <summary>
        /// 测试桩程序
        /// </summary>
      

        private void FrmQueryResult_Load(object sender, EventArgs e)
        {
            try
            {
                #region 获取查询窗口传的数据并加载到结果窗口               
                if (dataFromQuery.lstCheckLists.Count== 0)
                {
                    this.Close();
                    return; 
                }

                TreeNode nodes = new TreeNode();
                TreeNode node = null;
                bool bIsFirst = true;

                if (bIsFirst == true)
                {
                    ColumnHeader colh = new ColumnHeader();
                    nodes.Text = mc_strNameOfRootNode;
                    bIsFirst = false;
                }
                
                for (int i = 0; i < dataFromQuery.lstCheckLists.Count; i++)
                {                    
                    node = new TreeNode();
                    node.Text = dataFromQuery.lstCheckLists[i];
                    nodes.Nodes.Add(node);
                }
                trvQueryResult.Nodes.Add(nodes);
                trvQueryResult.ExpandAll();
                #endregion

              }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.ToString(),"错误",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

        }

      
        //点击左侧的结果Tree节点
        private void trvQueryResult_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {   
                if (e.Node.Text == "" || e.Node.Text == mc_strNameOfRootNode)
                {
                    return;
                }
                if (e.Button == MouseButtons.Left)
                {
                   //切换到数据页面
                    tabControl1.SelectTab(tabPage1);
                    objFeature = new AOBaseLibC.AFGeodatabase.AFFeature();
                    m_pMxdoc = m_pApp.Document as IMxDocument;
                    m_pMap = m_pMxdoc.FocusMap;
                    objMap.Map = m_pMap; 

                    m_objFeatureLayers.Clear();
                    //若放在文件头初始化，则查询历史中的值会动态变化为当前值
                    lstQueryResult = new List<ShutdownValveScheme>();

                    dataGrid_QueryResult.DataSource = null;
                    dataGrid_QueryResult.Refresh();

                    dataGrid_Detail4clsid.DataSource = null;
                    dataGrid_Detail4clsid.Refresh();

                    string skey = string.Empty;

                    if (e.Node.Tag != null)
                        skey = e.Node.Parent.Text;
                    else
                        skey = e.Node.Text;

                    skey = skey.Substring(0, skey.Length);
                    this.Cursor = Cursors.WaitCursor;

                    
                    string strSQLQueryclsid = string.Empty;
                    string strSQLQueryclsidHeader = string.Empty;

                    //保存查询结果到的历史记录中：历史记录为空时。当前查询结果不在历史记录中。当前结果在记录中。
                    //将查询的结果写入“缓存”m_dicTableSearchHistory中，为了下次显示数据迅速
                    if (m_dicTableSearchHistory==null || m_dicTableSearchHistory.Count < 1||!m_dicTableSearchHistory.ContainsKey(skey))
                    {
                       
                        progressBarSearching.Visible = true;
                        #region 执行查询操作
                        //全市查询
                        if (dataFromQuery.bIsAll == true)
                        {
                            strSQLQueryclsid = string.Format("select distinct (t.clsid) from {0}.closevalve t where t.valvecount={1} order by t.clsid", m_CurParameter.GWUserName, skey);                          
                            OleDbDataReader adoReader = null;
                            m_objDBCON.ExecuteSQLReturn(strSQLQueryclsid, ref adoReader);
                                                                            
                            //查出条数为了进度条做准备
                            OleDbDataReader adoReaderCount = null;
                            string  strSQLQueryclsidCount = string.Format("select count(*) as IdCount from (select distinct (t.clsid) from {0}.closevalve t where t.valvecount={1} order by t.clsid)", m_CurParameter.GWUserName, skey);
                            m_objDBCON.ExecuteSQLReturn(strSQLQueryclsidCount, ref adoReaderCount);
                            int maxNum = 0;
                            if (adoReaderCount != null)
                            {
                                adoReaderCount.Read();
                                maxNum = Convert.ToInt32(adoReaderCount[0].ToString());
                                progressBarSearching.Maximum = maxNum;
                            }
                            adoReaderCount.Dispose();
                            int icount = 1;
                            //解决方案号存到结果集lstQueryResult中
                            while(adoReader.Read())
                            {
                                ShutdownValveScheme vss = new ShutdownValveScheme();
                                vss.strNo = icount.ToString();
                                vss.strClsID = adoReader["clsid"].ToString();
                                lstQueryResult.Add(vss);                           
                                progressBarSearching.Value=icount;
                                if (icount < maxNum)
                                {
                                    icount++;
                                }
                                
                            }

                        }
                        //选择区域
                        else
                        {
                            int icount = 1;                          
                            progressBarSearching.Maximum = dataFromQuery.lstValveIDs.Count;

                                for (int k = 0; k < dataFromQuery.lstValveIDs.Count; k++)
                                {
                                    
                                    strSQLQueryclsid = "select distinct (t.clsid) from " + m_CurParameter.GWUserName + ".closevalve t where t.valvecount=" + skey + " and t.valveid like '%;" + dataFromQuery.lstValveIDs[k] + ";%' order by t.clsid";
                                    strSQLQueryclsidHeader = "select distinct (t.clsid) from " + m_CurParameter.GWUserName + ".closevalve t where t.valvecount=" + skey + " and t.valveid like '" + dataFromQuery.lstValveIDs[k] + ";%' order by t.clsid";
                                     string strSQLQueryclsidCount1 = string.Format("select Count(*) from (select distinct (t.clsid) from {0}.closevalve t where t.valvecount={1} and t.valveid like '%;{2};%' order by t.clsid)", m_CurParameter.GWUserName, skey, dataFromQuery.lstValveIDs[k]);
                                     string strSQLQueryclsidCount2 = string.Format("select Count(*) from (select distinct (t.clsid) from {0}.closevalve t where t.valvecount={1} and t.valveid like '{2};%' order by t.clsid)", m_CurParameter.GWUserName, skey, dataFromQuery.lstValveIDs[k]);

                              

                                   //查询的数据放入到结果集lstQueryResult中
                                    OleDbDataReader adoReader = null;
                                    m_objDBCON.ExecuteSQLReturn(strSQLQueryclsid, ref adoReader);
                                   
                                    while (adoReader.Read())
                                    {
                                        try
                                        {
                                            ShutdownValveScheme vss = new ShutdownValveScheme();
                                            vss.strNo = icount.ToString();
                                            vss.strClsID = adoReader["clsid"].ToString().ToString();
                                            StringClsIDComparer scomparer = new StringClsIDComparer();
                                            if (!lstQueryResult.Contains(vss, scomparer))
                                            {
                                                lstQueryResult.Add(vss);
                                                icount++;
                                            }                                          

                                        }
                                        catch
                                        {
                                            MessageBox.Show(dataFromQuery.lstValveIDs[k].ToString());

                                        }

                                    }                                   

                                    //第二次查询                                   
                                    m_objDBCON.ExecuteSQLReturn(strSQLQueryclsidHeader, ref adoReader);
                                    while (adoReader.Read())
                                    {
                                        ShutdownValveScheme vss = new ShutdownValveScheme();
                                        vss.strNo = icount.ToString();
                                        vss.strClsID = adoReader["clsid"].ToString().ToString();
                                        StringClsIDComparer scomparer = new StringClsIDComparer();
                                        if (!lstQueryResult.Contains(vss, scomparer))
                                        {
                                            lstQueryResult.Add(vss);
                                            icount++;
                                        }
                                     
                                    }
                                    adoReader.Dispose();
                                    progressBarSearching.Value =k;
                                                         
                            }
                          
                        }
                        #endregion
                        progressBarSearching.Value = 0;
                        progressBarSearching.Visible = false;
                      
                        //将结果 关阀个数，方案号写入查询历史记录中；                        
                        m_dicTableSearchHistory.Add(skey, lstQueryResult);
                    }                    
                    else
                    {
                        lstQueryResult = m_dicTableSearchHistory[skey];
                    }
                    
                    

                    //结果显示到界面的控件上。加载的数据集要有数据，否则会报错
                    if (lstQueryResult != null && lstQueryResult.Count > 0)                  
                    {
                        dataGrid_QueryResult.DataSource = lstQueryResult;
                        dataGrid_QueryResult.Columns.Clear();
                        dataGrid_QueryResult.TopLeftHeaderCell.Value = "序号";
                        //displayCol(dataGrid_QueryResult, "字段1", "显示字段");//添加其他字段
                        displayCol(dataGrid_QueryResult, "strClsID", "关阀方案号");
                        //序号赋值
                        for (int i = 0; i < dataGrid_QueryResult.Rows.Count; i++)
                        {
                            DataGridViewRow row = this.dataGrid_QueryResult.Rows[i];
                            row.HeaderCell.Value = lstQueryResult[i].strNo;
                        }
                        dataGrid_QueryResult.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;

                        dataGrid_QueryResult.ClearSelection();
                    }
                    lbl_DataViewCLSID.Text = skey+" 阀方案：共查到 " + lstQueryResult.Count + " 条记录";
                    
                }                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        #region 添加列
        /// <summary>
        /// 添加列
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="dataPropertyName"></param>
        /// <param name="headerText"></param>
        public static void displayCol(DataGridView dgv, String dataPropertyName, String headerText)
        {
            dgv.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn obj = new DataGridViewTextBoxColumn();
            obj.DataPropertyName = dataPropertyName;
            obj.HeaderText = headerText;
            obj.Name = dataPropertyName;
            obj.Resizable = DataGridViewTriState.True;
            obj.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns.AddRange(new DataGridViewColumn[] { obj });
        }
        #endregion
       
       

      

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layerName"></param>
        /// <param name="oid"></param>
        private void AddSelection(string layerName,string oid)
        {
            objFeature = getFeature(layerName, oid);
            m_pFeatureSelection = objFeatureLayer.FeatureLayer as IFeatureSelection;
            m_pFeatureSelection.Add(objFeature.Feature);
        }

        /// <summary>
        /// 定位管网设备，有阀门的话，定位阀门，没有阀门则定位管线
        /// </summary>
        /// <param name="layername">图层</param>
        /// <param name="oid">OID</param>
        private bool GetFirstValvePosition(string layername,string oid)
        {
            objFeature = getFeature(layername, oid);
            if (objFeature.Feature == null)
            {
                string msgLayer = string.Empty;
                switch (layername.ToUpper())
                {
                    case mc_TablePIPESECTIONMAIN:
                        msgLayer = "配水干线";
                        break;
                    case mc_TablePIPESECTIONUSER:
                        msgLayer = "配水支线";
                        break;
                    case mc_TablePIPESECTIONSOURCE:
                        msgLayer = "输水管线";
                        break;
                }
                MessageBox.Show("设备[" + msgLayer + ":"+oid+"]定位失败");
                return false;
            }
            IPoint po = new PointClass();
            IPolyline plt =new PolylineClass();
            IGeometryCollection gc = (IGeometryCollection)plt;            

            switch (layername.Trim().ToUpper())
            { 
                case mc_TableValve:
                    po = objFeature.Feature.ShapeCopy as IPoint;
                    break;
                case mc_TablePIPESECTIONMAIN:
                case mc_TablePIPESECTIONUSER:
                case mc_TablePIPESECTIONSOURCE:
                    gc.AddGeometryCollection((IGeometryCollection)objFeature.Feature.ShapeCopy);
                    IPolyline pl = (IPolyline)gc;
                    po = pl.FromPoint as IPoint;//管线的From点；
                    break;
                default:
                    {
                        MessageBox.Show("不能定位该设备！", "提示！");
                        return false;
                    }
            }            
            //po = objFeature.Feature.ShapeCopy as IPoint;
            if (po != null)
            {
                IPoint p = po;
                IEnvelope elp = p.Envelope;
                elp.Width = 30;
                elp.Height = 30;
                m_pMxdoc.ActiveView.Extent.CenterAt(p);
                m_pMxdoc.ActiveView.Extent = elp.Envelope;
                m_pMxdoc.ActivatedView.FocusMap.MapScale = 1000;
                m_pMxdoc.ActiveView.Refresh();
            }
            else
            { 
                //2015-3-10 管线from点定位失败，则定位阀门；
                objFeature = getFeature(mc_TableValve, oid);
                
            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(po);
            return true;
        }

        private AOBaseLibC.AFGeodatabase.AFFeature getFeature(string layerName, string OID)
        {
            bool isReapt = false;
            objFeatureLayer = objMap.GetLayerByName(layerName);
            foreach (AOBaseLibC.AFGeodatabase.AFFeatureLayer layer in m_objFeatureLayers)
            {
                if (layer.LayerTableName == objFeatureLayer.LayerTableName)
                {
                    isReapt = true;
                    break;
                }
            }
            if (isReapt == false)
            {
                m_objFeatureLayers.Add(objFeatureLayer);
            }
            IQueryFilter pQFilter = new QueryFilter();
            pQFilter.WhereClause = "OBJECTID=" + OID;
            IFeatureCursor pFCursor = objFeatureLayer.FeatureLayer.FeatureClass.Search(pQFilter, true);
            objFeature = new AOBaseLibC.AFGeodatabase.AFFeature();
            objFeature.Feature = pFCursor.NextFeature();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFCursor);
            return objFeature;
        }
       
        private void ClearSelection()
        {
            AOBaseLibC.AFCommon.AFCollection objLayers = objMap.GetLayers(AOBaseLibC.AFCommon.AFConst.AFLayterType.AFGeoFeatureLayer);
            AOBaseLibC.AFGeodatabase.AFFeatureLayer objLayer;
            foreach (DictionaryEntry layer in objLayers)
            {
                objLayer = layer.Value as AOBaseLibC.AFGeodatabase.AFFeatureLayer;
                m_pFeatureSelection = objLayer.FeatureLayer as IFeatureSelection;
                m_pFeatureSelection.Clear();
            }
        }

        //点击右上DataGridView行中记录时发生
        private void dataGrid_QueryResult_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowNum = e.RowIndex;
            if (rowNum < 0)
            {
                return;
            
            }                   
            string CLSID = dataGrid_QueryResult.Rows[rowNum].Cells[0].Value.ToString();
            string SqlSearch = string.Format("select * from {0}.closevalve t where t.CLSID={1}", m_CurParameter.GWUserName, CLSID);
            OleDbDataReader DateReader=null;
            m_objDBCON.ExecuteSQLReturn(SqlSearch, ref DateReader);
            DataTable dt = new DataTable();
            dt.Load(DateReader);
            dataGrid_Detail4clsid.DataSource = dt;        
            this.dataGrid_Detail4clsid.Columns[0].HeaderText = "方案号";
            this.dataGrid_Detail4clsid.Columns[1].HeaderText = "管线名";
            this.dataGrid_Detail4clsid.Columns[2].HeaderText = "管线号";
            this.dataGrid_Detail4clsid.Columns[3].HeaderText = "阀门号";
            this.dataGrid_Detail4clsid.Columns[4].HeaderText = "关阀数";
          //高亮显示用
            //IList<int> IListModel = new List<int>();
            //IListModel.Add(Convert.ToInt32(CLSID));
            //dicFeatures4Located.Add("valve", IListModel);
        }
        /// <summary>
        /// 将查询到的管线按类别存储到对应的用于定位的集合中
        /// </summary>
        /// <param name="lstPipesectionMainOids4Located">配水干线集合</param>
        /// <param name="lstPipesectionUserOids4Located">配水支线集合</param>
        /// <param name="lstPipesectionSourceOids4Located">输水管线集合</param>
        /// <param name="lstPipesectionValveOids4Located">阀门集合，有阀门id则定位阀门，否则定位管线</param>
        /// <param name="vss"></param>
        private static void Add2LocateDataSet(IList<int> lstPipesectionMainOids4Located, IList<int> lstPipesectionUserOids4Located, IList<int> lstPipesectionSourceOids4Located, IList<int> lstPipesectionValveOids4Located, ShutdownValveScheme vss)
        {
            switch (vss.strLineName.ToUpper())
            {
                case mc_TablePIPESECTIONMAIN:
                    if (!lstPipesectionMainOids4Located.Contains(vss.lineID))
                        lstPipesectionMainOids4Located.Add(vss.lineID);
                    break;
                case mc_TablePIPESECTIONUSER:
                    if (!lstPipesectionUserOids4Located.Contains(vss.lineID))
                        lstPipesectionUserOids4Located.Add(vss.lineID);
                    break;
                case mc_TablePIPESECTIONSOURCE:
                    if (!lstPipesectionSourceOids4Located.Contains(vss.lineID))
                        lstPipesectionSourceOids4Located.Add(vss.lineID);
                    break;
                case mc_TableValve:
                    if (!lstPipesectionValveOids4Located.Contains(vss.lineID))
                        lstPipesectionValveOids4Located.Add(vss.lineID);
                    break;
            }
        }

        private void FrmQueryResult_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClearSelection();
            objMap.RefreshMap();
        }
        /// <summary>
        /// 需定位右上显示的整个选中的方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Locate_Click(object sender, EventArgs e)
        {
            if (dataGrid_Detail4clsid.CurrentCell == null)
            {
                MessageBox.Show("请选择一条数据！","提示");
                return;
            
            }
            int rownumber = dataGrid_Detail4clsid.CurrentCell.RowIndex; 
            string strLocLayername=string.Empty;
            string strLocLineID = string.Empty;

            try
            {
                ClearSelection();

                strLocLayername = dataGrid_Detail4clsid.Rows[rownumber].Cells[1].Value.ToString();
                strLocLineID = dataGrid_Detail4clsid.Rows[rownumber].Cells[2].Value.ToString();

                //获取阀门要素,使其高亮显示
                // List<string> ValveIds = new List<string>();
                //try
                //{
                //    string[] ValveIds = dataGrid_Detail4clsid.Rows[rownumber].Cells[3].Value.ToString().Split(';');
                //    AFFeature objFeature;
                //    m_pFeatureSelection = objFeatureLayer.FeatureLayer as IFeatureSelection;
                //    foreach (string StrID in ValveIds)
                //    {
                //        try
                //        {
                //            objFeature = getFeature("Valve", StrID);
                //            m_pFeatureSelection.Add(objFeature.Feature);
                //        }
                //        catch {

                //            continue;
                //        }
                       
                //    }
                                                                                                                    
                //}
               
               
                    //没有阀门的情况
                 //有阀门的话，定位阀门，没有阀门则定位管线
                    //实际上strLocLayername只能获取到管线层，不会获取到阀门层。
                    if (strLocLayername.Trim() != "" && strLocLineID.Trim() != "")
                    {
                        if (GetFirstValvePosition(strLocLayername, strLocLineID))
                        {
                                           
                           m_pFeatureSelection = objFeatureLayer.FeatureLayer as IFeatureSelection;
                            m_pFeatureSelection.Add(objFeature.Feature);

                            
                        }
                    }

               
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序遇到错误，查询失败！");
                return;
            }
           
            objMap.RefreshMap();
        }
                
        /// <summary>
        /// 数据视图界面的导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Export_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGrid_Detail4clsid.Rows.Count > 0)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Execl files (*.xls)|*.xls";
                    saveFileDialog.FilterIndex = 0;
                    saveFileDialog.RestoreDirectory = true;
                    saveFileDialog.CreatePrompt = true;
                    saveFileDialog.Title = "导出关阀方案到Excel文件";
                    DateTime objTimeNow = DateTime.Now;
                    saveFileDialog.FileName = "关阀方案 " + objTimeNow.Year.ToString().PadLeft(2) + objTimeNow.Month.ToString().PadLeft(2, '0') + objTimeNow.Day.ToString().PadLeft(2, '0') + "-" + objTimeNow.Hour.ToString().PadLeft(2, '0') + objTimeNow.Minute.ToString().PadLeft(2, '0') + objTimeNow.Second.ToString().PadLeft(2, '0');
                    saveFileDialog.ShowDialog();
                    if (saveFileDialog.FileName.IndexOf(":") < 0) return; //被点了"取消"
                    Stream myStream = saveFileDialog.OpenFile();//打开一个文件并返回一个文本流
                    StreamWriter MySw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(0));
                    using (StreamWriter sw = MySw)
                    {

                        //输出第一行,表头名
                       
                        string FirstRowSth = string.Format("方案号\t管线名\t管线号\t阀门号\t关阀数");
                        sw.WriteLine(FirstRowSth);

                        for (int i = 0; i < dataGrid_Detail4clsid.Rows.Count; i++)
                        {
                            string RowSth = null;
                            RowSth = string.Format("{0}\t{1}\t{2}\t{3}\t{4}", dataGrid_Detail4clsid.Rows[i].Cells[0].Value, dataGrid_Detail4clsid.Rows[i].Cells[1].Value, dataGrid_Detail4clsid.Rows[i].Cells[2].Value, dataGrid_Detail4clsid.Rows[i].Cells[3].Value, dataGrid_Detail4clsid.Rows[i].Cells[4].Value);
                            sw.WriteLine(RowSth);
                        }

                    }

                    MessageBox.Show("转出Excel成功！", "提示");

                }

                else
                {
                    MessageBox.Show("数据为空不可以导出！");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误信息!");
                this.Cursor = Cursors.Default;
                return;
            }

        }
        
      
       
        
       
        public void DarwMapsGFSL()
        {

            try
            {
                if (dataFromQuery.lstCheckLists.Count == 0)
                {
                    return;
                }
                string con = "Provider=MSDAORA.1;Password=jjwater;User ID=jjwater;Data Source=jjwater;Persist Security Info=True";
                LocalCDBCon objDBCon = new LocalCDBCon();
                objDBCon.OpenConnection(con);
                string strSQLNew = "";
                chart1.Series.Clear();
                Series series = new Series("关阀方案数量");
                series.ChartType = SeriesChartType.Column;
                series.ShadowOffset = 2;
                series.Legend = chart1.Legends[0].Name;
                series.XValueType = ChartValueType.Int32;
                chart1.Series.Add(series);
                chart1.Series[0].IsVisibleInLegend = true;//设置显示图例               
                ChartArea ca = chart1.ChartAreas[0];
                ca.AxisX.Interval = 1;//设置轴间距
                ca.AxisX.Title = "关阀数量";
                ca.AxisX.TitleAlignment = StringAlignment.Far;
                ca.AxisX.MajorGrid.Enabled = false;//不显示竖分割线
                ca.AxisY.Title = "关阀方案数量";//轴标题名称
                ca.AxisY.TitleAlignment = StringAlignment.Far;//轴标题位置
                ca.AxisY.TextOrientation = TextOrientation.Stacked;//轴标题文字方向  

                //全市查询
                if (dataFromQuery.bIsAll)
                {
                    for (int i = 0; i < dataFromQuery.lstCheckLists.Count; i++)
                    {                     
                        lstDicResultsKeys.Add(dataFromQuery.lstCheckLists[i].ToString());
                        strSQLNew = string.Format("select count(*) from CLOSEVALVE where valvecount={0}", dataFromQuery.lstCheckLists[i]);
                        OleDbDataReader dbReaderAll = objDBCon.ReadData(strSQLNew);
                        while (dbReaderAll.Read())
                        {
                            chart1.Series[0].Points.AddXY(lstDicResultsKeys[i], dbReaderAll[0].ToString());
                            chart1.Series[0].Points[i].Label = dbReaderAll[0].ToString();
                        }
                    }
                }
                //区域查询
                else
                {
                    //缓存为空说明是第一次加载页面
                    if (DicValveCountAndClsidCount.Count == 0)
                    {
                        if (MessageBox.Show("加载区域的图表视图会有些慢是否继续吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            progressBarSearching.Visible = true;
                            progressBarSearching.Maximum = dataFromQuery.lstCheckLists.Count * dataFromQuery.lstValveIDs.Count;
                            int value = 0;
                            for (int i = 0; i < dataFromQuery.lstCheckLists.Count; i++)
                            {                              
                             
                                int Count = 0;                                                                            
                                for (int k = 0; k < dataFromQuery.lstValveIDs.Count; k++)
                                {
                                    progressBarSearching.Value = value++;
                                    string strSQLQueryclsidCount1 = string.Format("select Count(*) from (select distinct (t.clsid) from {0}.closevalve t where t.valvecount={1} and t.valveid like '%;{2};%' order by t.clsid)", m_CurParameter.GWUserName, dataFromQuery.lstCheckLists[i], dataFromQuery.lstValveIDs[k]);
                                    string strSQLQueryclsidCount2 = string.Format("select Count(*) from (select distinct (t.clsid) from {0}.closevalve t where t.valvecount={1} and t.valveid like '{2};%' order by t.clsid)", m_CurParameter.GWUserName, dataFromQuery.lstCheckLists[i], dataFromQuery.lstValveIDs[k]);
                                    OleDbDataReader adoReader = null;

                                    m_objDBCON.ExecuteSQLReturn(strSQLQueryclsidCount1, ref adoReader);
                                    if (adoReader != null && adoReader.Read())
                                    {
                                        Count += Convert.ToInt16(adoReader[0]);

                                    }
                                    adoReader = null;
                                    m_objDBCON.ExecuteSQLReturn(strSQLQueryclsidCount2, ref adoReader);
                                    if (adoReader != null && adoReader.Read())
                                    {
                                        Count = Count + Convert.ToInt16(adoReader[0]);
                                        adoReader.Dispose();
                                    }


                                }
                                //存入缓存里面
                                if (!DicValveCountAndClsidCount.ContainsKey(dataFromQuery.lstCheckLists[i]))
                                {
                                    DicValveCountAndClsidCount.Add(dataFromQuery.lstCheckLists[i], Count.ToString());
                                }

                            }
                            progressBarSearching.Value = 0;
                            progressBarSearching.Visible = false;
                        
                            //绘制图形
                            int j = 0;
                            foreach (KeyValuePair<string, string> kv in DicValveCountAndClsidCount)
                            {

                                chart1.Series[0].Points.AddXY(kv.Key, kv.Value);
                                chart1.Series[0].Points[j++].Label = kv.Value;

                            }

                        }
                        else
                        {
                            //没有绘图要返回数据页面
                            tabControl1.SelectTab("tabPage1");
                            return;
                        }
                    }

                    //再次加载页面
                    else
                    {
                        int i = 0;
                        foreach (KeyValuePair<string,string> kv in DicValveCountAndClsidCount)
                        {
                            
                            chart1.Series[0].Points.AddXY(kv.Key, kv.Value);
                            chart1.Series[0].Points[i++].Label=kv.Value;
                        
                        }
                        
                    
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

      


        private SeriesChartType type(string sTypename)
        {
            SeriesChartType types = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            if (sTypename == "柱形")
            {
                types = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            }
            if (sTypename == "圆型")
            {
                types = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;

            }
            if (sTypename == "折线型")
            {
                types = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            }
            return types;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            for (int i = 0; i < chart1.Series.Count; i++)
            {
                chart1.Series[i].ChartType = type(comboBox1.Text);
            }
        }

      

        //图表界面导出Excel
        private void btnOutExcel_Click(object sender, EventArgs e)
        {
            try
            {
                int columnNum;
                int rowNum = 0;
                ClsOutExcel clsOE = new ClsOutExcel();
                string con = "Provider=MSDAORA.1;Password=jjwater;User ID=jjwater;Data Source=jjwater;Persist Security Info=True";
                LocalCDBCon objDBCon = new LocalCDBCon();
                objDBCon.OpenConnection(con);
                string strSQLNum = "select count(*) as c from CLOSEVALVE";
                OleDbDataReader dbReader = objDBCon.ReadData(strSQLNum);
                //dbReader只有一条数据，数据条数
                while (dbReader.Read())
                {
                    rowNum = Convert.ToInt32(dbReader["c"]);
                }

                string strSQL = "select * from CLOSEVALVE";
                string str1 = "select * from CLOSEVALVE where cl.valvecount='";
                string str2 = "' group by cl.valvecount";
              
                dbReader = objDBCon.ReadData(strSQL);

                clsOE.OutExcel(dbReader, rowNum);
                MessageBox.Show("导出完成！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
       
       

       

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
           string Text = tabControl1.SelectedTab.Name;
           if (Text == "tabPage2")
           {
               DarwMapsGFSL();
           }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }



              

        
    }
                                           

}
