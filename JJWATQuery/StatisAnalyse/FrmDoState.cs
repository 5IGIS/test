using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AOBaseLibC;
using AOBaseLibC.AFGeodatabase;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using JJWATBaseLibC;
using Utilitys;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;


namespace JJWATQuery
{
    public partial class FrmDoState : Form
    {
        public CDBCon m_objDBCON = null;
        public SysParameters m_CurParameter = null;
        private const string mc_KeyofWaterType = "PL_WATERTYPE";
        private const string mc_strValveLayerName = "阀门";
        private const string mc_strHydrantLayerName = "消火栓";
        private const string mc_FieldNameDiameter = "DIAMETER";
        private const string mc_FieldNameMaterial = "MATERIAL";
        private const string mc_FieldNameVstate = "VSTATE";//阀门性质
        private const string mc_FieldNameShapeLength = "SHAPE_LENGTH";//管线长度
        private const string mc_strPipesectionMainName = "配水干线";
        private const string mc_strPipesectionUserName = "配水支线";
        private const string mc_strPipesectionSourceName = "输水管线";
        private const string mc_strHydrant = "消火栓";
        private const string mc_StatusTempPipe = "'临时'";//临时管线修改为临时，且存储于status字段（原来为datatype）
        private const string mc_StatusDisusedPipe = "'废弃'";
        private const string mc_StatusCompletionPipe = "'竣工'";
        private const string mc_TableValveState = "VALVESTATE";//配水管线闸门统计
        private const string mc_TablePIPELINEMAINLENSTATE = "PIPELINEMAINLENSTATE";//市区输水管线长度统计
        private const string mc_TableNEWLINELENMONSTATE = "NEWLINELENMONSTATE";//新增管线长度统计月报
        private const string mc_TableNEWHYDRANTMONSTATE = "NEWHYDRANTMONSTATE";//XXXX年X月新安消火栓统计表
        private const string mc_RoadValve = "路闸";//阀门性质
        private const string mc_UserValve = "户闸";//阀门性质        
        private const string mc_TableItem = "口径";
        private const string mc_TableItemSum = "合计";
        private const string mc_TableSubtotal = "小计";
        private const string mc_TableTotal = "总计";

        private const string mc_TableItemMaterial = "材质";
        private const string mc_TableItemDiameter = "管径";
        private const string mc_TableItemUnit = "单位";
        private const string mc_TableItemLength = "长度";
        private const string mc_TableItemOriginalValue = "原值";
        private const string mc_TableItemDepreciationMonth = "月折旧额";
        private const string mc_TableItemRemark = "备注";

        private const string mc_strNameReport_Valve = "配水管线闸门统计";
        private const string mc_strNameReport_Mains = "市区配水管网长度统计";
        private const string mc_strNameReport_NewMains = "新增管线长度统计月报";
        private const string mc_strNameReport_NewHydrant = "新安装消火栓统计表";
        private const string mc_strNameReport_NewUser = "新增配水管线长度统计月报";
        private const string mc_strNameReport_NewSource = "新增输水管线长度统计月报";
        private const string mc_strNameReport_Pipes = "撤除管线长度统计月报";
        private const string mc_strNameofJJWaterltd = "北京市自来水公司";
        private const string mc_strNameReport_PipeInvestAddTotal = "管网固定资产增加汇总表";
        private const string mc_strNameReport_PipeInvestAddList = "管网固定资产增加明细表";
        private const string mc_strNameReport_GWAnnual = "管网管线长度统计年报";
        private const string mc_strWaterType_All = "所有";
        private const string mc_strInvestDomainName = "投资方式";//PL_WORKCHAR
        private const string mc_Fn_WorkChar = "WORKCHAR";
        private const string mc_Fn_Drawprice = "DRAWPRICE";
        private string mc_strUnknownDiameter = "其他管径";
        private string mc_strUnknownValveType = "未知闸型";
        private string mc_strUnknownMaterial = "其他材质";//根据年报，统计除钢管、球墨铸铁、铸铁、塑料管、水泥管之外的算作其他管。20150703
        private const string mc_strDataTypeDigFound = "'刨找'";
        private const string mc_strPipeCharDistrict = "'小区'";//工程性质为小区：刨找

        private const string mc_WithinSymbol = "介于";
        private const string mc_EqualSymbol = "等于";
        private const string mc_MoreSymbol = "大于";
        private const string mc_MoreEqualSymbol = "大于等于";
        private const string mc_SmallSymbol = "小于";
        private const string mc_SmallEqualSymbol = "小于等于";
        private const string mc_StrQueryBFFieldNameRemovedt = "bfdate";
        private const string mc_StrQueryGWFieldNameCheckdt = "checkedate";
        private const string mc_StrAnnualReportStatusDate = "WRITEDATE";


        private string m_strExcelTemplate = string.Empty;

        Dictionary<string, List<string>> m_dicDomainNameList = new Dictionary<string, List<string>>();
        //private string strSqlSelectWT = "Select * from JJWATER.PL_WATERTYPE";        //以后会有配置表，目前读取domain值,修改为Enum WaterType枚举
        private string m_strConnAppDB = string.Empty;//本地appDB路径
        private string m_strTitleOfExportExcelFileName = string.Empty;
        private UtilitysMgs m_objMgs;
        public enum DataFieldType { DString = 1, DNumber = 2 }

        OleDbConnection m_OldbConn = new OleDbConnection();
        private List<string> m_lstReportParameters = new List<string>();
        private List<string> m_lstColumns = new List<string>();
        private List<string> m_lstRow = new List<string>();
        private List<string> m_lstDBResults = new List<string>();
        private List<string> m_lstInvest = new List<string>();
        Dictionary<string, string> m_dicColumnCodeDesc = new Dictionary<string, string>();
        Dictionary<string, string> m_dicRowCodeDesc = new Dictionary<string, string>();
        private bool m_blISCAL = false;//重算标识；用于导出固定资产汇总或明细前执行，二者任意执行一次即可

        private AFMap m_objMap = new AFMap();
        IFeatureLayer m_pFeatureLayer = null;
        private string m_strDefaultExcelFilePath = string.Empty;


        private string m_strRet = string.Empty;

        public string InvestSelected = string.Empty;

        public string NewBuildDtSimple = string.Empty;
        public string RemoveDtSimple = string.Empty;
        public string AnnualDtSimple = string.Empty;

        public string BuildDataTitle = string.Empty;
        //为统一风格而修改：新增统计和撤除统计均带有时间条件选择2015-05-11
        public string NewDtComplex = string.Empty; //新增统计时间条件
        public string RemoveDtComplex = string.Empty;//撤除统计时间条件
        public string AnnualDtComplex = string.Empty;//年报统计时间条件




        public FrmDoState()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体初始化，若初始化失败，则返回false；需重新运行程序
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public bool Init(IApplication app)
        {
            try
            {
                IApplication pApp;
                IMxDocument pMxDoc;
                IMap pMap;
                string configPath = string.Empty;

                pApp = app;
                pMxDoc = pApp.Document as IMxDocument;
                pMap = pMxDoc.FocusMap;
                m_objMap.Map = pMap;
                m_objMgs = new UtilitysMgs(pApp);

                configPath = System.Reflection.Assembly.GetExecutingAssembly().Location.ToString(CultureInfo.InvariantCulture);
                configPath = System.IO.Path.GetDirectoryName(configPath);
                m_strDefaultExcelFilePath = configPath;
                m_strExcelTemplate = configPath + @"\PERSONAL.XLSB";
                configPath = configPath + @"\QueryReport.mdb";
                m_strConnAppDB = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + configPath + ";Persist Security Info=False";

                //填充关系下拉框
                cboRemoveDt.Items.Add(mc_EqualSymbol);
                cboRemoveDt.Items.Add(mc_MoreSymbol);
                cboRemoveDt.Items.Add(mc_MoreEqualSymbol);
                cboRemoveDt.Items.Add(mc_SmallSymbol);
                cboRemoveDt.Items.Add(mc_SmallEqualSymbol);
                cboRemoveDt.Items.Add(mc_WithinSymbol);

                cboRemoveDt.Text = mc_WithinSymbol;


                cboCheckDt.Items.Add(mc_EqualSymbol);
                cboCheckDt.Items.Add(mc_MoreSymbol);
                cboCheckDt.Items.Add(mc_MoreEqualSymbol);
                cboCheckDt.Items.Add(mc_SmallSymbol);
                cboCheckDt.Items.Add(mc_SmallEqualSymbol);
                cboCheckDt.Items.Add(mc_WithinSymbol);

                cboCheckDt.Text = mc_WithinSymbol;


                cboCheckDt4.Items.Add(mc_EqualSymbol);
                cboCheckDt4.Items.Add(mc_MoreSymbol);
                cboCheckDt4.Items.Add(mc_MoreEqualSymbol);
                cboCheckDt4.Items.Add(mc_SmallSymbol);
                cboCheckDt4.Items.Add(mc_SmallEqualSymbol);
                cboCheckDt4.Items.Add(mc_WithinSymbol);

                cboCheckDt4.Text = mc_WithinSymbol;



                m_CurParameter = SysParameters.GetInstance();
                if (GetDomaisFromMap(mc_strInvestDomainName))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误！");
                return false;
            }

        }
        /// <summary>
        /// 水质类型
        /// </summary>
        public enum WaterType
        {
            所有,
            净水,
            中水
        }

        private void FrmReport_Load(object sender, EventArgs e)
        {
            List<string> lstStrWaterTypes = new List<string>();
            try
            {
                //m_objDBCON = CDBCon.GetInstance();
                //SysParameters m_CurParameter = SysParameters.GetInstance();
                //if (m_objMap.Map.LayerCount > 0)
                //{
                //获取当前地图图层，查找数据所在数据库的所有属性域值，读取出水质类型，并加载到下拉框中
                //List<AFFeatureLayer> lstLayers = m_objMap.GetLayerByWorkpace(m_CurParameter.GWDataSetName);  
                //2014-12-22 修改为获取枚举值  watertype
                cboWaterType.Items.Clear();
                Array watertypes = Enum.GetValues(typeof(WaterType));
                foreach (WaterType wt in watertypes)
                {
                    cboWaterType.Items.Add(wt.ToString());
                }

                cboWaterType.Text = mc_strWaterType_All;
                //撤除界面
                lbl_To.Enabled = true;
                lbl_To.Visible = (cboRemoveDt.Text == mc_WithinSymbol);
                dTPDateTo.Enabled = true;
                dTPDateTo.Visible = (cboRemoveDt.Text == mc_WithinSymbol);

                //新增界面
                lblTo.Enabled = true;
                lblTo.Visible = (cboCheckDt.Text == mc_WithinSymbol);
                dtpEndStateDate.Enabled = true;
                dtpEndStateDate.Visible = (cboRemoveDt.Text == mc_WithinSymbol);
                //年报界面
                lblTo4.Enabled = true;
                lblTo4.Visible = (cboCheckDt4.Text == mc_WithinSymbol);
                dtpEndStateDate4.Enabled = true;
                dtpEndStateDate4.Visible = (cboRemoveDt.Text == mc_WithinSymbol);

                //if (lstLayers.Count > 0)
                //{
                //    m_dicDomainNameList = m_objMgs.GetAllDomains(m_objMap.GetLayerByName(lstLayers[0].FeatureLayer.FeatureClass.AliasName).FetLayerDataset.Workspace);
                //    if (m_dicDomainNameList.ContainsKey(mc_KeyofWaterType))
                //    {
                //        lstStrWaterTypes = m_dicDomainNameList[mc_KeyofWaterType];
                //        foreach (var s in lstStrWaterTypes)
                //        {
                //            cboWaterType.Items.Add(s);
                //        }
                //        cboWaterType.Items.Add(mc_strWaterType_All);
                //        cboWaterType.SelectedIndex = 0;
                //    }
                //    else
                //    {
                //        MessageBox.Show("数据库中没有水质类型属性域值", "提示！");
                //    }

                //}
                //else
                //{
                //    MessageBox.Show("加载的数据不可用！", "提示");
                //    return;
                //    //}
                //}
                //else
                //{
                //    MessageBox.Show("当前地图没有加载数据", "提示！");
                //    return;
                //}                    


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误！");
                return;
            }

        }

        public bool GetDomaisFromMap(string strNameofDomain)
        {
            if (m_objDBCON == null)
                m_objDBCON = CDBCon.GetInstance();
            if (m_CurParameter == null)
                m_CurParameter = SysParameters.GetInstance();
            if (strNameofDomain == null && strNameofDomain.Trim() == "")
            {
                MessageBox.Show("传入阈名" + strNameofDomain + "不能为空!");
                return false;
            }

            if (m_objMap.Map.LayerCount > 0)
            {
                //获取当前地图图层，查找数据所在数据库的所有属性域值，读取出投资方式，赋给变量m_lstInvest
                List<AFFeatureLayer> lstLayers = m_objMap.GetLayerByWorkpace(m_CurParameter.GWDataSetName);
                m_lstInvest.Clear();
                if (lstLayers.Count > 0)
                {
                    m_dicDomainNameList = m_objMgs.GetAllDomains();
                    if (m_dicDomainNameList.ContainsKey(strNameofDomain))
                    {
                        List<string> lstStrWaterTypes = m_dicDomainNameList[strNameofDomain];
                        foreach (var s in lstStrWaterTypes)
                        {
                            if (!m_lstInvest.Contains(s.ToString()))
                                m_lstInvest.Add(s);
                        }
                        //cboWaterType.SelectedIndex = 0;
                        //cboWaterType.Text = mc_strWaterType_All;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("数据库中没有" + strNameofDomain + "属性域值", "提示！");
                        return false;
                    }

                }
                else
                {
                    MessageBox.Show("无法获取数据库属性域！", "提示");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("当前地图没有加载数据", "提示！");
                return false;
            }

        }

        //private void btnState_Click(object sender, EventArgs e)
        //{
        //    if (optValvestat.Checked)
        //    {
        //        //配水管线闸门统计
        //        ValveState();
        //    }
        //    else if (optPipeLenStat.Checked)
        //    {
        //        //市区配水管线长度统计
        //        PipeLineMainState();
        //    }
        //    else if (optNewPipeLenMonthStat.Checked)
        //    {
        //        //新增管线长度统计月报
        //        NewLineMonthState(1);
        //    }
        //    else if (optNewHydrantMonthStat.Checked)
        //    {
        //        //新安消火栓个数统计月报
        //        NewHyDrantMonthState();
        //    }
        //    else if (optNewWTPipeLenMonthStat.Checked)
        //    {
        //        //新增配水管线长度统计月报
        //        NewLineMonthState(2);
        //    }
        //    else if (optNewSWTPipeLenMonthStat.Checked)
        //    {
        //        //新增输水管线长度统计月报                
        //        NewLineMonthState(3);
        //    }
        //    else
        //    {

        //    }
        //}

        /// <summary>
        /// xxxx年xx月新安消火栓统计报表
        /// </summary>
        //private void NewHyDrantMonthState()
        //{
        //    string strSQL = string.Empty;
        //    OleDbConnection connAppDB = new OleDbConnection(m_strConnAppDB);
        //    OleDbDataReader adoReader = null;
        //    OleDbDataReader adoAppDBReader = null;
        //    OleDbCommand cmd = connAppDB.CreateCommand();

        //    string strBegDate = string.Empty;
        //    string strEndDate = string.Empty;

        //    double dblCount = 0.0;
        //    Boolean blnTONGJI = false;
        //    string strWhere = string.Empty;

        //    m_lstReportParameters = new List<string>();

        //    try
        //    {
        //        strWhere = GetWhere(mc_strHydrantLayerName, ref blnTONGJI);

        //        if (blnTONGJI && strWhere != null)
        //        {
        //            this.Cursor = Cursors.WaitCursor;
        //            connAppDB.Open();
        //            strSQL = "Delete * from " + mc_TableNEWHYDRANTMONSTATE;
        //            cmd.CommandText = strSQL;
        //            cmd.ExecuteNonQuery();

        //            DateTime dtBegDate = Convert.ToDateTime(dtpBegStateDate.Text);
        //            DateTime dtEndDate = Convert.ToDateTime(dtpEndStateDate.Text);

        //            strBegDate = dtBegDate.ToString("yyyyMMdd");
        //            strEndDate = dtEndDate.ToString("yyyyMMdd");

        //            if (cboWaterType.Text == mc_strWaterType_All)
        //            {
        //                strSQL = "select addr,username,userno,diameter,count(*) Vcount from hydrant where trim(WRITEDATE)>='" + strBegDate + "' and trim(WRITEDATE)<='" + strEndDate + "' " + strWhere + " and trim(DataType)<>'临时管线' group by addr,username,userno,diameter";
        //            }
        //            else
        //            {
        //                strSQL = "select addr,username,userno,diameter,count(*) Vcount from hydrant where trim(WRITEDATE)>='" + strBegDate + "' and trim(WRITEDATE)<='" + strEndDate + "' " + strWhere + " and trim(DataType)<>'临时管线' and sztype='" + cboWaterType.Text + "' group by addr,username,userno,diameter";
        //            }

        //            Get_sqlDBReader(strSQL, ref adoReader);
        //            if (adoReader != null)
        //            {
        //                while (adoReader.Read())
        //                {
        //                    strSQL = "Insert into " + mc_TableNEWHYDRANTMONSTATE + " values('" +
        //                        adoReader["addr"].ToString() + "','" +
        //                        adoReader["username"].ToString() + "','" +
        //                        adoReader["userno"].ToString() + "','" +
        //                        adoReader["diameter"].ToString() + "','" +
        //                        adoReader["Vcount"].ToString() + "')";
        //                    cmd = connAppDB.CreateCommand();
        //                    cmd.CommandText = strSQL;
        //                    cmd.ExecuteNonQuery();
        //                }
        //                strSQL = "select sum(HYCOUNT) from " + mc_TableNEWHYDRANTMONSTATE;
        //                cmd = connAppDB.CreateCommand();
        //                cmd.CommandText = strSQL;
        //                adoAppDBReader = cmd.ExecuteReader();
        //                while (adoAppDBReader.Read())
        //                {
        //                    dblCount = Convert.ToDouble(CNullValue(adoAppDBReader[0].ToString(), DataFieldType.DNumber));
        //                }


        //            }

        //            FrmReports frmRlt = new FrmReports();

        //            m_lstReportParameters.Add(mc_strNameReport_NewHydrant);
        //            m_lstReportParameters.Add(dtBegDate.ToString("yyyy") + "年" + dtBegDate.ToString("MM") + "月");
        //            m_lstReportParameters.Add(dtEndDate.ToString("yyyy") + "年" + dtEndDate.ToString("MM") + "月");
        //            m_lstReportParameters.Add(dblCount.ToString());
        //            frmRlt.ProductReport(m_lstReportParameters);
        //            frmRlt.Show();
        //            this.Cursor = Cursors.Default;

        //        }//if(TONGJI) END
        //        else
        //        {
        //            MessageBox.Show("所选统计类型记录为空", "提示！");
        //            this.Cursor = Cursors.Default;
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "错误！");
        //        return;
        //    }
        //    finally
        //    {
        //        if (adoReader != null && adoReader.IsClosed == false)
        //        {
        //            adoReader.Close();
        //        }
        //        if (adoAppDBReader != null && adoAppDBReader.IsClosed == false)
        //        {
        //            adoAppDBReader.Close();
        //        }
        //        if (connAppDB != null && connAppDB.State == ConnectionState.Open)
        //        {
        //            connAppDB.Close();
        //        }
        //        if (m_OldbConn != null && m_OldbConn.State == ConnectionState.Open)
        //        {
        //            m_OldbConn.Close();
        //        }
        //        this.Cursor = Cursors.Default;

        //    }



        //}

        /// <summary>
        /// 新增管线长度统计月报报表
        /// </summary>
        //private void NewLineMonthState(int intType)
        //{
        //    string strSQL = string.Empty;
        //    OleDbConnection connAppDB = new OleDbConnection(m_strConnAppDB);
        //    OleDbDataReader adoReader = null;
        //    OleDbDataReader adoAppDBReader = null;
        //    OleDbCommand cmd = connAppDB.CreateCommand();

        //    string strBegDate = string.Empty;
        //    string strEndDate = string.Empty;
        //    string strDiameter = string.Empty;
        //    string strMaterial = string.Empty;
        //    string strOldDiameter = string.Empty;
        //    string strOldMaterial = string.Empty;


        //    double dblSum = 0.0;//合计
        //    double dblGangSum = 0.0;//钢管
        //    double dblQiuTieSum = 0.0;//球墨铸铁管
        //    double dblZhuTieSum = 0.0;//铸铁
        //    double dblDuXinSum = 0.0;//镀锌
        //    double dblGangSuSum = 0.0;//钢塑
        //    double dblYuYingLiSum = 0.0;//预应力
        //    double dblShiMianSum = 0.0;//石棉
        //    double dblBoLiGangSum = 0.0;//玻璃钢
        //    double dblSuLiaoSum = 0.0;//塑料
        //    double dblOtherSum = 0.0;//其他材料


        //    Boolean blnWriteTable = false;
        //    Boolean blnTONGJI1 = false;
        //    Boolean blnTONGJI2 = false;
        //    Boolean blnTONGJI3 = false;

        //    string strWherePipesectionMain = string.Empty;
        //    string strWherePipesectionUser = string.Empty;
        //    string strWherePipesectionSource = string.Empty;

        //    List<string> lstMaterial = new List<string>();
        //    List<string> lstSum = new List<string>();
        //    List<string> lstRow = new List<string>();

        //    List<double> lstdblRowValue = new List<double>();
        //    double dblLen = 0.0;
        //    Dictionary<string, int> dicMaterial = new Dictionary<string, int>();
        //    Dictionary<string, int> dicDiameter = new Dictionary<string, int>();

        //    int intRow = 0;
        //    int intCol = 0;
        //    long i = 0;
        //    long j = 0;

        //    m_lstReportParameters = new List<string>();

        //    try
        //    {

        //        strWherePipesectionMain = GetWhere(mc_strPipesectionMainName, ref blnTONGJI1);
        //        strWherePipesectionUser = GetWhere(mc_strPipesectionUserName, ref blnTONGJI2);
        //        strWherePipesectionSource = GetWhere(mc_strPipesectionSourceName, ref blnTONGJI3);

        //        if (strWherePipesectionMain.Trim() != "")
        //        {
        //            strWherePipesectionMain = " and " + strWherePipesectionMain;
        //        }
        //        if (strWherePipesectionUser.Trim() != "")
        //        {
        //            strWherePipesectionUser = " and " + strWherePipesectionUser;
        //        }
        //        if (strWherePipesectionSource.Trim() != "")
        //        {
        //            strWherePipesectionSource = " and " + strWherePipesectionSource;
        //        }

        //        if (blnTONGJI1 && blnTONGJI2 && blnTONGJI3)
        //        {
        //            this.Cursor = Cursors.WaitCursor;

        //            strSQL = "Delete * from " + mc_TableNEWLINELENMONSTATE;
        //            connAppDB.Open();
        //            cmd.CommandText = strSQL;
        //            cmd.ExecuteNonQuery();
        //            connAppDB.Close();

        //            DateTime dtBegDate = Convert.ToDateTime(dtpBegStateDate.Text);
        //            DateTime dtEndDate = Convert.ToDateTime(dtpEndStateDate.Text);

        //            strBegDate = dtBegDate.ToString("yyyyMMdd");//20140806 大小写有区别
        //            strEndDate = dtEndDate.ToString("yyyyMMdd");//20010101

        //            #region 构造查询Oracle的strSQL语句
        //            switch (intType)
        //            {
        //                case 1:
        //                    if (cboWaterType.Text == mc_strWaterType_All)
        //                    {
        //                        //所有水质类型
        //                        strSQL = "select diameter,material,sum(cd) shape_length from (select diameter,material,sde.st_length(shape) cd from pipesectionmain " +
        //                            "where trim(writedate)>= '" + strBegDate + "' and trim(writedate)<='" + strEndDate + "' " + strWherePipesectionMain + " and trim(DataType)<> '废弃管线' and trim(DataType)<> '临时管线' union all " +
        //                            "select diameter,material,sde.st_length(shape) cd from pipesectionuser where trim(writedate)>= '" + strBegDate + "' and trim(writedate)<= '" + strEndDate + "' " + strWherePipesectionUser + " and trim(DataType)<>'废气管线' and trim(DataType)<>'临时管线' union all " +
        //                            "select diameter,material,sde.st_length(shape) cd from pipesectionsource where trim(writedate)>= '" + strBegDate + "' and trim(writedate)<= '" + strEndDate + "' " + strWherePipesectionSource + " and trim(DataType)<>'废气管线' and trim(DataType)<>'临时管线' " +
        //                            " ) group by diameter,material order by cast((case when instr(" + mc_FieldNameDiameter + " ,'X') > 1 then substr(" + mc_FieldNameDiameter + ",1,instr(" + mc_FieldNameDiameter + ",'X')-1) else diameter end ) as int) asc," + mc_FieldNameDiameter;

        //                    }
        //                    else
        //                    {
        //                        //选中水质类型
        //                        strSQL = "select diameter,material,sum(cd) shape_length from (select diameter,material,sde.st_length(shape) cd from pipesectionmain " +
        //                            "where trim(writedate)>= '" + strBegDate + "' and trim(writedate)<='" + strEndDate + "' " + strWherePipesectionMain + " and trim(DataType)<> '废弃管线' and trim(DataType)<> '临时管线' and sztype='" + cboWaterType.Text + "' union all " +
        //                            "select diameter,material,sde.st_length(shape) cd from pipesectionuser where trim(writedate)>= '" + strBegDate + "' and trim(writedate)<= '" + strEndDate + "' " + strWherePipesectionUser + " and trim(DataType)<>'废气管线' and trim(DataType)<>'临时管线' and sztype='" + cboWaterType.Text + "' union all " +
        //                            "select diameter,material,sde.st_length(shape) cd from pipesectionsource where trim(writedate)>= '" + strBegDate + "' and trim(writedate)<= '" + strEndDate + "' " + strWherePipesectionSource + " and trim(DataType)<>'废气管线' and trim(DataType)<>'临时管线' and sztype='" + cboWaterType.Text + "'" +
        //                            " ) group by diameter,material order by cast((case when instr(" + mc_FieldNameDiameter + " ,'X') > 1 then substr(" + mc_FieldNameDiameter + ",1,instr(" + mc_FieldNameDiameter + ",'X')-1) else diameter end ) as int) asc," + mc_FieldNameDiameter;
        //                    }
        //                    break;
        //                case 2:
        //                    if (cboWaterType.Text == mc_strWaterType_All)
        //                    {
        //                        //所有水质类型,配水支线
        //                        strSQL = "select diameter,material,sum(cd) shape_length from (select diameter,material,sde.st_length(shape) cd from pipesectionmain " +
        //                            "where trim(writedate)>= '" + strBegDate + "' and trim(writedate)<='" + strEndDate + "' " + strWherePipesectionMain + " and trim(DataType)<> '废弃管线' and trim(DataType)<> '临时管线' union all " +
        //                            "select diameter,material,sde.st_length(shape) cd from pipesectionuser where trim(writedate)>= '" + strBegDate + "' and trim(writedate)<= '" + strEndDate + "' " + strWherePipesectionUser + " and trim(DataType)<>'废气管线' and trim(DataType)<>'临时管线' " +
        //                            " ) group by diameter,material order by cast((case when instr(" + mc_FieldNameDiameter + " ,'X') > 1 then substr(" + mc_FieldNameDiameter + ",1,instr(" + mc_FieldNameDiameter + ",'X')-1) else diameter end ) as int) asc," + mc_FieldNameDiameter;
        //                    }
        //                    else
        //                    {
        //                        //选中水质类型，配水支线
        //                        strSQL = "select diameter,material,sum(cd) shape_length from (select diameter,material,sde.st_length(shape) cd from pipesectionmain " +
        //                            "where trim(writedate)>= '" + strBegDate + "' and trim(writedate)<='" + strEndDate + "' " + strWherePipesectionMain + " and trim(DataType)<> '废弃管线' and trim(DataType)<> '临时管线' and sztype='" + cboWaterType.Text + "' union all " +
        //                            "select diameter,material,sde.st_length(shape) cd from pipesectionuser where trim(writedate)>= '" + strBegDate + "' and trim(writedate)<= '" + strEndDate + "' " + strWherePipesectionUser + " and trim(DataType)<>'废气管线' and trim(DataType)<>'临时管线' and sztype='" + cboWaterType.Text + "' " +
        //                            " ) group by diameter,material order by cast((case when instr(" + mc_FieldNameDiameter + " ,'X') > 1 then substr(" + mc_FieldNameDiameter + ",1,instr(" + mc_FieldNameDiameter + ",'X')-1) else diameter end ) as int) asc," + mc_FieldNameDiameter;
        //                    }
        //                    break;
        //                case 3:
        //                    if (cboWaterType.Text == mc_strWaterType_All)
        //                    {
        //                        //所有水质类型,输水管线
        //                        strSQL = "select diameter,material,sum(cd) shape_length from (select diameter,material,sde.st_length(shape) cd from pipesectionsource where trim(writedate)>= '" + strBegDate + "' and trim(writedate)<= '" + strEndDate + "' " + strWherePipesectionSource + " and trim(DataType)<>'废气管线' and trim(DataType)<>'临时管线' " +
        //                            " ) group by diameter,material order by cast((case when instr(" + mc_FieldNameDiameter + " ,'X') > 1 then substr(" + mc_FieldNameDiameter + ",1,instr(" + mc_FieldNameDiameter + ",'X')-1) else diameter end ) as int) asc," + mc_FieldNameDiameter;

        //                    }
        //                    else
        //                    {
        //                        //选中水质类型，输水管线
        //                        strSQL = "select diameter,material,sum(cd) shape_length from (select diameter,material,sde.st_length(shape) cd from pipesectionsource where trim(writedate)>= '" + strBegDate + "' and trim(writedate)<= '" + strEndDate + "' " + strWherePipesectionSource + " and trim(DataType)<>'废气管线' and trim(DataType)<>'临时管线' and sztype='" + cboWaterType.Text + "' " +
        //                            " ) group by diameter,material order by cast((case when instr(" + mc_FieldNameDiameter + " ,'X') > 1 then substr(" + mc_FieldNameDiameter + ",1,instr(" + mc_FieldNameDiameter + ",'X')-1) else diameter end ) as int) asc," + mc_FieldNameDiameter;
        //                    }
        //                    break;
        //                case 4:
        //                    if (cboWaterType.Text == mc_strWaterType_All)
        //                    {
        //                        //所有水质类型,和1比不区分时间
        //                        strSQL = "select diameter,material,sum(cd) shape_length from (select diameter,material,sde.st_length(shape) cd from pipesectionmain " +
        //                            "where trim(DataType)<> '废弃管线' and trim(DataType)<> '临时管线' union all " +
        //                            "select diameter,material,sde.st_length(shape) cd from pipesectionuser where trim(DataType)<>'废气管线' and trim(DataType)<>'临时管线' union all " +
        //                            "select diameter,material,sde.st_length(shape) cd from pipesectionsource where trim(DataType)<>'废气管线' and trim(DataType)<>'临时管线' " +
        //                            " ) group by diameter,material order by cast((case when instr(" + mc_FieldNameDiameter + " ,'X') > 1 then substr(" + mc_FieldNameDiameter + ",1,instr(" + mc_FieldNameDiameter + ",'X')-1) else diameter end ) as int) asc," + mc_FieldNameDiameter;

        //                    }
        //                    else
        //                    {
        //                        //选中水质类型
        //                        strSQL = "select diameter,material,sum(cd) shape_length from (select diameter,material,sde.st_length(shape) cd from pipesectionmain " +
        //                            "where trim(DataType)<> '废弃管线' and trim(DataType)<> '临时管线' and sztype='" + cboWaterType.Text + "' union all " +
        //                            "select diameter,material,sde.st_length(shape) cd from pipesectionuser where  trim(DataType)<>'废气管线' and trim(DataType)<>'临时管线' and sztype='" + cboWaterType.Text + "' union all " +
        //                            "select diameter,material,sde.st_length(shape) cd from pipesectionsource where  trim(DataType)<>'废气管线' and trim(DataType)<>'临时管线' and sztype='" + cboWaterType.Text + "' " +
        //                            " ) group by diameter,material order by cast((case when instr(" + mc_FieldNameDiameter + " ,'X') > 1 then substr(" + mc_FieldNameDiameter + ",1,instr(" + mc_FieldNameDiameter + ",'X')-1) else diameter end ) as int) asc," + mc_FieldNameDiameter;
        //                    }
        //                    break;
        //                default:
        //                    MessageBox.Show("输入参数错误", "提示");
        //                    return;
        //            }
        //            #endregion

        //            Get_sqlDBReader(strSQL, ref adoReader);

        //            if (adoReader != null)
        //            {
        //                intRow = 0;
        //                intCol = 0;
        //                int ii = 0;
        //                while (adoReader.Read())
        //                {
        //                    ii++;
        //                    if (ii <= 1)
        //                    {
        //                        strOldDiameter = CNullValue(adoReader[mc_FieldNameDiameter].ToString(), DataFieldType.DNumber);

        //                    }
        //                    strDiameter = CNullValue(adoReader[mc_FieldNameDiameter].ToString(), DataFieldType.DNumber);

        //                    if (strOldDiameter != strDiameter)
        //                    {
        //                        strSQL = "Insert into " + mc_TableNEWLINELENMONSTATE + " (DIAMETER,GANGGUAN,ZHUTIEGUAN,QIUMOGUAN,DUXINGUAN,YUYINGLI,SHIMIANGUAN,SULIAO,GANGSUOFUHE,BOLIGANG,LITTLESTATE) values('" +
        //                            strOldDiameter + "','" +
        //                            Math.Round(dblGangSum, 2) + "','" +
        //                            Math.Round(dblZhuTieSum, 2) + "','" +
        //                            Math.Round(dblQiuTieSum, 2) + "','" +
        //                            Math.Round(dblDuXinSum, 2) + "','" +
        //                            Math.Round(dblYuYingLiSum, 2) + "','" +
        //                            Math.Round(dblShiMianSum, 2) + "','" +
        //                            Math.Round(dblSuLiaoSum, 2) + "','" +
        //                            Math.Round(dblGangSuSum, 2) + "','" +
        //                            Math.Round(dblBoLiGangSum, 2) + "','" +
        //                            Math.Round(dblGangSum + dblZhuTieSum + dblQiuTieSum + dblDuXinSum + dblGangSuSum + dblYuYingLiSum + dblShiMianSum + dblSuLiaoSum + dblBoLiGangSum, 2) +
        //                            "') ";

        //                        if (connAppDB.State == ConnectionState.Closed)
        //                        {
        //                            connAppDB.Open();
        //                        }
        //                        cmd = connAppDB.CreateCommand();
        //                        cmd.CommandText = strSQL;
        //                        cmd.ExecuteNonQuery();
        //                        dblGangSum = 0; dblQiuTieSum = 0; dblZhuTieSum = 0; dblDuXinSum = 0; dblGangSuSum = 0; dblYuYingLiSum = 0; dblShiMianSum = 0; dblSuLiaoSum = 0; dblBoLiGangSum = 0;
        //                    }
        //                    switch (CNullValue(adoReader[mc_FieldNameMaterial].ToString(), DataFieldType.DString))
        //                    {
        //                        case "铸铁管":
        //                        case "铸铁":
        //                            dblZhuTieSum = dblZhuTieSum + Convert.ToDouble(adoReader[mc_FieldNameShapeLength].ToString());
        //                            break;
        //                        case "球墨":
        //                        case "球墨铸铁":
        //                        case "球墨铸铁管":
        //                            dblQiuTieSum = dblQiuTieSum + Convert.ToDouble(adoReader[mc_FieldNameShapeLength].ToString());
        //                            break;
        //                        case "钢":
        //                        case "钢管":
        //                            dblGangSum = dblGangSum + Convert.ToDouble(adoReader[mc_FieldNameShapeLength].ToString());
        //                            break;
        //                        case "石棉水泥":
        //                        case "石棉":
        //                        case "石棉水泥管":
        //                        case "石棉管":
        //                            dblShiMianSum = dblShiMianSum + Convert.ToDouble(adoReader[mc_FieldNameShapeLength].ToString());
        //                            break;
        //                        case "预应力":
        //                        case "预应力管":
        //                            dblYuYingLiSum = dblYuYingLiSum + Convert.ToDouble(adoReader[mc_FieldNameShapeLength].ToString());
        //                            break;
        //                        case "钢塑复合管":
        //                        case "钢塑复合":
        //                        case "塑钢管":
        //                        case "塑钢":
        //                            dblGangSuSum = dblGangSuSum + Convert.ToDouble(adoReader[mc_FieldNameShapeLength].ToString());
        //                            break;
        //                        case "塑料管":
        //                        case "塑料":
        //                            dblSuLiaoSum = dblSuLiaoSum + Convert.ToDouble(adoReader[mc_FieldNameShapeLength].ToString());
        //                            break;
        //                        case "镀锌管":
        //                        case "镀锌":
        //                            dblDuXinSum = dblDuXinSum + Convert.ToDouble(adoReader[mc_FieldNameShapeLength].ToString());
        //                            break;
        //                        case "玻璃钢管":
        //                        case "玻璃钢":
        //                            dblBoLiGangSum = dblBoLiGangSum + Convert.ToDouble(adoReader[mc_FieldNameShapeLength].ToString());
        //                            break;
        //                        default:
        //                            dblOtherSum = dblOtherSum + Convert.ToDouble(adoReader[mc_FieldNameShapeLength].ToString());
        //                            break;
        //                    }

        //                    strOldDiameter = strDiameter;
        //                }

        //                strSQL = "Insert into " + mc_TableNEWLINELENMONSTATE + " (DIAMETER,GANGGUAN,ZHUTIEGUAN,QIUMOGUAN,DUXINGUAN,YUYINGLI,SHIMIANGUAN,SULIAO,GANGSUOFUHE,BOLIGANG,LITTLESTATE) values('" +
        //                            strOldDiameter + "','" +
        //                            Math.Round(dblGangSum, 2) + "','" +
        //                            Math.Round(dblZhuTieSum, 2) + "','" +
        //                            Math.Round(dblQiuTieSum, 2) + "','" +
        //                            Math.Round(dblDuXinSum, 2) + "','" +
        //                            Math.Round(dblYuYingLiSum, 2) + "','" +
        //                            Math.Round(dblShiMianSum, 2) + "','" +
        //                            Math.Round(dblSuLiaoSum, 2) + "','" +
        //                            Math.Round(dblGangSuSum, 2) + "','" +
        //                            Math.Round(dblBoLiGangSum, 2) + "','" +
        //                            Math.Round(dblGangSum + dblZhuTieSum + dblQiuTieSum + dblDuXinSum + dblGangSuSum + dblYuYingLiSum + dblShiMianSum + dblSuLiaoSum + dblBoLiGangSum, 2) +
        //                            "') ";

        //                if (connAppDB.State == ConnectionState.Closed)
        //                {
        //                    connAppDB.Open();
        //                }
        //                cmd = connAppDB.CreateCommand();
        //                cmd.CommandText = strSQL;
        //                cmd.ExecuteNonQuery();


        //                strSQL = "select sum(GANGGUAN),sum(QIUMOGUAN),sum(ZHUTIEGUAN),sum(DUXINGUAN),sum(GANGSUOFUHE),sum(YUYINGLI),sum(SHIMIANGUAN),sum(SULIAO),sum(BOLIGANG) from " + mc_TableNEWLINELENMONSTATE;
        //                cmd = connAppDB.CreateCommand();
        //                cmd.CommandText = strSQL;
        //                adoAppDBReader = cmd.ExecuteReader();

        //                while (adoAppDBReader.Read())
        //                {
        //                    dblGangSum = Convert.ToDouble(adoAppDBReader[0].ToString());
        //                    dblQiuTieSum = Convert.ToDouble(adoAppDBReader[1].ToString());
        //                    dblZhuTieSum = Convert.ToDouble(adoAppDBReader[2].ToString());
        //                    dblDuXinSum = Convert.ToDouble(adoAppDBReader[3].ToString());
        //                    dblGangSuSum = Convert.ToDouble(adoAppDBReader[4].ToString());
        //                    dblYuYingLiSum = Convert.ToDouble(adoAppDBReader[5].ToString());
        //                    dblShiMianSum = Convert.ToDouble(adoAppDBReader[6].ToString());
        //                    dblSuLiaoSum = Convert.ToDouble(adoAppDBReader[7].ToString());
        //                    dblBoLiGangSum = Convert.ToDouble(adoAppDBReader[8].ToString());

        //                }
        //                //插入合计
        //                strSQL = "Insert into " + mc_TableNEWLINELENMONSTATE + " (DIAMETER,GANGGUAN,ZHUTIEGUAN,QIUMOGUAN,DUXINGUAN,YUYINGLI,SHIMIANGUAN,SULIAO,GANGSUOFUHE,BOLIGANG,LITTLESTATE) values(" +
        //                            "'小计','" +
        //                            Math.Round(dblGangSum, 2) + "','" +
        //                            Math.Round(dblZhuTieSum, 2) + "','" +
        //                            Math.Round(dblQiuTieSum, 2) + "','" +
        //                            Math.Round(dblDuXinSum, 2) + "','" +
        //                            Math.Round(dblYuYingLiSum, 2) + "','" +
        //                            Math.Round(dblShiMianSum, 2) + "','" +
        //                            Math.Round(dblSuLiaoSum, 2) + "','" +
        //                            Math.Round(dblGangSuSum, 2) + "','" +
        //                            Math.Round(dblBoLiGangSum, 2) + "','" +
        //                            Math.Round(dblGangSum + dblZhuTieSum + dblQiuTieSum + dblDuXinSum + dblGangSuSum + dblYuYingLiSum + dblShiMianSum + dblSuLiaoSum + dblBoLiGangSum, 2) +
        //                            "') ";

        //                cmd = connAppDB.CreateCommand();
        //                cmd.CommandText = strSQL;
        //                cmd.ExecuteNonQuery();
        //                connAppDB.Close();
        //                if (dblOtherSum > 1)
        //                {
        //                    //MessageBox.Show("存在其他材料管线长度：" + dblOtherSum, "提示");
        //                }
        //                FrmReports frmRlt = new FrmReports();
        //                switch (intType)
        //                {
        //                    case 1:
        //                        m_lstReportParameters.Add(mc_strNameReport_NewMains);
        //                        m_lstReportParameters.Add(dtBegDate.ToString("yyyy") + "年" + dtBegDate.ToString("MM") + "月");
        //                        m_lstReportParameters.Add(dtEndDate.ToString("yyyy") + "年" + dtEndDate.ToString("MM") + "月");
        //                        m_lstReportParameters.Add(cboWaterType.Text);
        //                        frmRlt.ProductReport(m_lstReportParameters);
        //                        break;
        //                    case 2:
        //                        m_lstReportParameters.Add(mc_strNameReport_NewUser);
        //                        m_lstReportParameters.Add(dtBegDate.ToString("yyyy") + "年" + dtBegDate.ToString("MM") + "月");
        //                        m_lstReportParameters.Add(dtEndDate.ToString("yyyy") + "年" + dtEndDate.ToString("MM") + "月");
        //                        m_lstReportParameters.Add(cboWaterType.Text);
        //                        frmRlt.ProductReport(m_lstReportParameters);
        //                        break;
        //                    case 3:
        //                        m_lstReportParameters.Add(mc_strNameReport_NewSource);
        //                        m_lstReportParameters.Add(dtBegDate.ToString("yyyy") + "年" + dtBegDate.ToString("MM") + "月");
        //                        m_lstReportParameters.Add(dtEndDate.ToString("yyyy") + "年" + dtEndDate.ToString("MM") + "月");
        //                        m_lstReportParameters.Add(cboWaterType.Text);
        //                        frmRlt.ProductReport(m_lstReportParameters);
        //                        break;
        //                }

        //                frmRlt.Show();
        //                this.Cursor = Cursors.Default;


        //            }
        //            else
        //            {
        //                MessageBox.Show("所选统计类型记录为空", "提示！");
        //                this.Cursor = Cursors.Default;
        //                return;
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "提示");
        //        return;
        //    }
        //    finally
        //    {
        //        if (adoReader.IsClosed == false)
        //        {
        //            adoReader.Close();
        //        }
        //        if (adoAppDBReader.IsClosed == false)
        //        {
        //            adoAppDBReader.Close();
        //        }
        //        if (connAppDB.State == ConnectionState.Open)
        //        {
        //            connAppDB.Close();
        //        }
        //        if (m_OldbConn.State == ConnectionState.Open)
        //        {
        //            m_OldbConn.Close();
        //        }
        //        this.Cursor = Cursors.Default;
        //    }


        //}

        /// <summary>
        /// 市区配水管网长度统计报表
        /// </summary>
        //private void PipeLineMainState()
        //{
        //    string strSQL = string.Empty;
        //    string strDiameter = string.Empty;
        //    string strOldDiameter = string.Empty;
        //    string strWherePipesectionMain = string.Empty;
        //    string strWherePipesectionUser = string.Empty;
        //    string strWherePipesectionSource = string.Empty;

        //    double dblSum = 0.0;

        //    double dblGangSum = 0.0;//钢
        //    double dblQiuTieSum = 0.0;//球墨铸铁
        //    double dblZhuTieSum = 0.0;//铸铁
        //    double dblDuXinSum = 0.0;//镀锌
        //    double dblGangSuSum = 0.0;//钢塑
        //    double dblYuYingLiSum = 0.0;//预应力
        //    double dblShiMianSum = 0.0;//石棉
        //    double dblBoLiGangSum = 0.0;//玻璃钢
        //    double dblSuLiaoSum = 0.0;//塑料
        //    double dblOtherSum = 0.0;//其他材料

        //    OleDbConnection connAppDB = new OleDbConnection(m_strConnAppDB);
        //    OleDbCommand cmd = connAppDB.CreateCommand();

        //    OleDbDataReader adoReader = null;
        //    OleDbDataReader adoAppDBReader = null;
        //    m_lstReportParameters = new List<string>();

        //    try
        //    {
        //        this.Cursor = Cursors.WaitCursor;
        //        //删除app Mdb数据
        //        strSQL = "Delete * from " + mc_TablePIPELINEMAINLENSTATE;

        //        connAppDB.Open();
        //        cmd.CommandText = strSQL;
        //        cmd.ExecuteNonQuery();
        //        connAppDB.Close();
        //        //
        //        if (cboWaterType.Text == mc_strWaterType_All)
        //        {
        //            strSQL = "select diameter,material,sum(cd) shape_length from (select diameter,material,SDE.ST_LENGTH(SHAPE) cd from pipesectionmain " +
        //                "where trim(DataType) <> '废弃管线' and trim(DataType) <> '临时管线' " + " union all " +
        //                "select diameter,material,SDE.ST_LENGTH(SHAPE) cd   from pipesectionuser where trim(DataType) <> '废弃管线' and trim(DataType) <> '临时管线' " + " union all " +
        //                "select diameter,material,SDE.ST_LENGTH(SHAPE) cd from pipesectionsource where trim(DataType) <> '废弃管线' and trim(DataType) <> '临时管线'" + ") group by diameter,material order by cast((case when instr(" + mc_FieldNameDiameter + " ,'X') > 1 then substr(" + mc_FieldNameDiameter + ",1,instr(" + mc_FieldNameDiameter + ",'X')-1) else diameter end ) as int) asc," + mc_FieldNameDiameter;
        //        }
        //        else
        //        {
        //            strSQL = "select diameter,material,sum(cd) shape_length from (select diameter,material,SDE.ST_LENGTH(SHAPE) cd from pipesectionmain " +
        //                "where trim(DataType) <> '废弃管线' and trim(DataType) <> '临时管线' and sztype='" + cboWaterType.Text + "'" + " union all " +
        //                "select diameter,material,SDE.ST_LENGTH(SHAPE) cd from pipesectionuser where trim(DataType) <> '废弃管线' and trim(DataType) <> '临时管线' and sztype='" + cboWaterType.Text + "'" + " union all " +
        //                "select diameter,material,SDE.ST_LENGTH(SHAPE) cd from pipesectionsource where trim(DataType) <> '废弃管线' and trim(DataType) <> '临时管线'and sztype='" + cboWaterType.Text + "'" + ") group by diameter,material order by cast((case when instr(" + mc_FieldNameDiameter + " ,'X') > 1 then substr(" + mc_FieldNameDiameter + ",1,instr(" + mc_FieldNameDiameter + ",'X')-1) else diameter end ) as int) asc," + mc_FieldNameDiameter;
        //        }
        //        //返回null
        //        // m_objDBCON.ExecuteSQLReturn(strSQL,ref adoReader);
        //        Get_sqlDBReader(strSQL, ref adoReader);


        //        if (adoReader != null)
        //        {
        //            int i = 0;
        //            while (adoReader.Read())
        //            {
        //                i++;
        //                if (i <= 1)
        //                {
        //                    strOldDiameter = CNullValue(adoReader[mc_FieldNameDiameter].ToString(), DataFieldType.DNumber);
        //                }
        //                strDiameter = CNullValue(adoReader[mc_FieldNameDiameter].ToString(), DataFieldType.DNumber);
        //                if (strOldDiameter != strDiameter)
        //                {
        //                    strSQL = "Insert into " + mc_TablePIPELINEMAINLENSTATE
        //                        + "(DIAMETER,ALLLENGTH,GANGGUAN,QIUTIEGUAN,ZHUTIEGUAN,DUXINGUAN,GANGFUHEGUAN,YUYINGLI,SHIMIANGUAN,SULIAO,BOLIGANG) values('" + 
        //                        strOldDiameter + "','" +
        //                        Math.Round(dblGangSum + dblQiuTieSum + dblZhuTieSum + dblDuXinSum + dblGangSuSum + dblYuYingLiSum + dblShiMianSum + dblSuLiaoSum + dblBoLiGangSum, 2) + "','" +
        //                        Math.Round(dblGangSum, 2) + "','" +
        //                        Math.Round(dblQiuTieSum, 2) + "','" +
        //                        Math.Round(dblZhuTieSum, 2) + "','" +
        //                        Math.Round(dblDuXinSum, 2) + "','" +
        //                        Math.Round(dblGangSuSum, 2) + "','" +
        //                        Math.Round(dblYuYingLiSum, 2) + "','" +
        //                        Math.Round(dblShiMianSum, 2) + "','" +
        //                        Math.Round(dblSuLiaoSum, 2) + "','" +
        //                        Math.Round(dblBoLiGangSum, 2) + "')";

        //                    if (connAppDB.State == ConnectionState.Closed)
        //                    {
        //                        connAppDB.Open();
        //                    }
        //                    cmd.CommandText = strSQL;
        //                    cmd.ExecuteNonQuery();
        //                    dblGangSum = 0; dblQiuTieSum = 0; dblZhuTieSum = 0; dblDuXinSum = 0; dblGangSuSum = 0; dblYuYingLiSum = 0; dblShiMianSum = 0; dblSuLiaoSum = 0; dblBoLiGangSum = 0;

        //                }
        //                switch (CNullValue(adoReader[mc_FieldNameMaterial].ToString(), DataFieldType.DString))
        //                {
        //                    case "铸铁管":
        //                    case "铸铁":
        //                        dblZhuTieSum = dblZhuTieSum + Convert.ToDouble(adoReader[mc_FieldNameShapeLength].ToString());
        //                        break;
        //                    case "球墨":
        //                    case "球墨铸铁":
        //                    case "球墨铸铁管":
        //                        dblQiuTieSum = dblQiuTieSum + Convert.ToDouble(adoReader[mc_FieldNameShapeLength].ToString());
        //                        break;
        //                    case "钢管":
        //                    case "钢":
        //                        dblGangSum = dblGangSum + Convert.ToDouble(adoReader[mc_FieldNameShapeLength].ToString());
        //                        break;
        //                    case "石棉水泥":
        //                    case "石棉":
        //                    case "石棉水泥管":
        //                    case "石棉管":
        //                        dblShiMianSum = dblShiMianSum + Convert.ToDouble(adoReader[mc_FieldNameShapeLength].ToString());
        //                        break;
        //                    case "预应力":
        //                    case "预应力管":
        //                        dblYuYingLiSum = dblYuYingLiSum + Convert.ToDouble(adoReader[mc_FieldNameShapeLength].ToString());
        //                        break;
        //                    case "钢塑复合管":
        //                    case "钢塑复合":
        //                    case "塑钢管":
        //                    case "塑钢":
        //                        dblGangSuSum = dblGangSuSum + Convert.ToDouble(adoReader[mc_FieldNameShapeLength].ToString());
        //                        break;
        //                    case "塑料管":
        //                    case "塑料":
        //                        dblSuLiaoSum = dblSuLiaoSum + Convert.ToDouble(adoReader[mc_FieldNameShapeLength].ToString());
        //                        break;
        //                    case "镀锌管":
        //                    case "镀锌":
        //                        dblDuXinSum = dblDuXinSum + Convert.ToDouble(adoReader[mc_FieldNameShapeLength].ToString());
        //                        break;
        //                    case "玻璃钢管":
        //                    case "玻璃钢":
        //                        dblBoLiGangSum = dblBoLiGangSum + Convert.ToDouble(adoReader[mc_FieldNameShapeLength].ToString());
        //                        break;
        //                    default:
        //                        dblOtherSum = dblOtherSum + Convert.ToDouble(adoReader[mc_FieldNameShapeLength].ToString());
        //                        break;
        //                }
        //                strOldDiameter = strDiameter;

        //            }

        //            strSQL = "Insert into " + mc_TablePIPELINEMAINLENSTATE + " (DIAMETER,ALLLENGTH,GANGGUAN,QIUTIEGUAN,ZHUTIEGUAN,DUXINGUAN,GANGFUHEGUAN,YUYINGLI,SHIMIANGUAN,SULIAO,BOLIGANG) values('" +
        //                strDiameter + "','" + Math.Round(dblGangSum + dblQiuTieSum + dblZhuTieSum + dblDuXinSum + dblGangSuSum + dblYuYingLiSum + dblShiMianSum + dblSuLiaoSum + dblBoLiGangSum, 2) + "','" +
        //                        Math.Round(dblGangSum, 2) + "','" +
        //                        Math.Round(dblQiuTieSum, 2) + "','" +
        //                        Math.Round(dblZhuTieSum, 2) + "','" +
        //                        Math.Round(dblDuXinSum, 2) + "','" +
        //                        Math.Round(dblGangSuSum, 2) + "','" +
        //                        Math.Round(dblYuYingLiSum, 2) + "','" +
        //                        Math.Round(dblShiMianSum, 2) + "','" +
        //                        Math.Round(dblSuLiaoSum, 2) + "','" +
        //                        Math.Round(dblBoLiGangSum, 2) + "')";

        //            if (connAppDB.State == ConnectionState.Closed)
        //            {
        //                connAppDB.Open();
        //            }

        //            cmd.CommandText = strSQL;
        //            cmd.ExecuteNonQuery();
        //            strSQL = "select sum(GANGGUAN),sum(QIUTIEGUAN),sum(ZHUTIEGUAN),sum(DUXINGUAN),sum(GANGFUHEGUAN),sum(YUYINGLI),sum(SHIMIANGUAN),sum(SULIAO),sum(BOLIGANG) from " + mc_TablePIPELINEMAINLENSTATE;
        //            cmd.CommandText = strSQL;
        //            adoAppDBReader = cmd.ExecuteReader();

        //            while (adoAppDBReader.Read())
        //            {
        //                dblGangSum = Convert.ToDouble(adoAppDBReader[0].ToString());
        //                dblQiuTieSum = Convert.ToDouble(adoAppDBReader[1].ToString());
        //                dblZhuTieSum = Convert.ToDouble(adoAppDBReader[2].ToString());
        //                dblDuXinSum = Convert.ToDouble(adoAppDBReader[3].ToString());
        //                dblGangSuSum = Convert.ToDouble(adoAppDBReader[4].ToString());
        //                dblYuYingLiSum = Convert.ToDouble(adoAppDBReader[5].ToString());
        //                dblShiMianSum = Convert.ToDouble(adoAppDBReader[6].ToString());
        //                dblSuLiaoSum = Convert.ToDouble(adoAppDBReader[7].ToString());
        //                dblBoLiGangSum = Convert.ToDouble(adoAppDBReader[8].ToString());

        //            }
        //            connAppDB.Close();
        //            strSQL = "Insert into " + mc_TablePIPELINEMAINLENSTATE + "(DIAMETER,ALLLENGTH,GANGGUAN,QIUTIEGUAN,ZHUTIEGUAN,DUXINGUAN,GANGFUHEGUAN,YUYINGLI,SHIMIANGUAN,SULIAO,BOLIGANG) values('总计(M)','" +
        //                Math.Round(dblGangSum + dblQiuTieSum + dblZhuTieSum + dblDuXinSum + dblGangSuSum + dblYuYingLiSum + dblShiMianSum + dblSuLiaoSum + dblBoLiGangSum, 2) + "','" +
        //                Math.Round(dblGangSum, 2) + "','" +
        //                Math.Round(dblQiuTieSum, 2) + "','" +
        //                Math.Round(dblZhuTieSum, 2) + "','" +
        //                Math.Round(dblDuXinSum, 2) + "','" +
        //                Math.Round(dblGangSuSum, 2) + "','" +
        //                Math.Round(dblYuYingLiSum, 2) + "','" +
        //                Math.Round(dblShiMianSum, 2) + "','" +
        //                Math.Round(dblSuLiaoSum, 2) + "','" +
        //                Math.Round(dblBoLiGangSum, 2) + "')";

        //            if (connAppDB.State == ConnectionState.Closed)
        //            {
        //                connAppDB.Open();
        //            }
        //            cmd.CommandText = strSQL;
        //            cmd.ExecuteNonQuery();

        //            connAppDB.Close();
        //            if (dblOtherSum > 1)
        //            {
        //                //MessageBox.Show("存在其他材料管线，长度：" + dblOtherSum, "提示");
        //            }
        //            FrmReports frmRlt = new FrmReports();
        //            m_lstReportParameters.Add(mc_strNameReport_Mains);
        //            frmRlt.ProductReport(m_lstReportParameters);
        //            frmRlt.Show();
        //            this.Cursor = Cursors.Default;
        //        }
        //        else
        //        {
        //            MessageBox.Show("所选统计类型记录为空", "提示！");
        //            this.Cursor = Cursors.Default;
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "提示");
        //        this.Cursor = Cursors.Default;

        //    }
        //    finally
        //    {
        //        if (adoReader.IsClosed == false)
        //        {
        //            adoReader.Close();
        //        }
        //        if (adoAppDBReader.IsClosed == false)
        //        {
        //            adoAppDBReader.Close();
        //        }
        //        if (connAppDB.State == ConnectionState.Open)
        //        {
        //            connAppDB.Close();
        //        }
        //        if (m_OldbConn.State == ConnectionState.Open)
        //        {
        //            m_OldbConn.Close();
        //        }
        //        this.Cursor = Cursors.Default;
        //    }

        //}

        /// <summary>
        /// 配水管线闸门统计报表
        /// </summary>
        //private void ValveState()
        //{
        //    string strSQL = string.Empty, strWhere = string.Empty, strOldDiameter = string.Empty, strDiameter = string.Empty;
        //    double dblAllCount = 0, dblAllRoadCount = 0, dblAllUserCount = 0;
        //    double dblUserCount = 0, dblRoadCount = 0;
        //    Boolean blnTONGJI = false;
        //    OleDbConnection con = new OleDbConnection(m_strConnAppDB);
        //    m_lstReportParameters = new List<string>();
        //    try
        //    {
        //        OleDbDataReader adoReader = null;
        //        //OleDbDataReader adoAppDBReader = null;
        //        if (m_objMap.Map.LayerCount < 1)
        //        {
        //            MessageBox.Show("当前地图没有加载数据", "提示！");
        //            return;
        //        }
        //        if (m_objMap.GetLayerByName(mc_strValveLayerName) == null)
        //        {
        //            MessageBox.Show("未加载闸门数据", "提示！");
        //            return;
        //        }
        //        IFeatureLayer pFeatureLayer = m_objMap.GetLayerByName(mc_strValveLayerName).FeatureLayer;
        //        IDataset pSelectedLayerDataset = (IDataset)pFeatureLayer;
        //        //m_sqlSearchTable = pSelectedLayerDataset.BrowseName;
        //        strWhere = GetWhere(mc_strValveLayerName, ref blnTONGJI);

        //        if (blnTONGJI)
        //        {
        //            this.Cursor = Cursors.WaitCursor;
        //            if (cboWaterType.Text == mc_strWaterType_All)
        //            {
        //                //strSQL = "select " + mc_FieldNameDiameter + "," + mc_FieldNameVstate + ", count(*) Vcount from " + m_sqlSearchTable + " where trim(diameter) is not null " + strWhere + " and trim(datatype) <> " + mc_DataTypeTempPipe + " group by " + mc_FieldNameDiameter + "," + mc_FieldNameVstate + " order by cast((case when instr(" + mc_FieldNameDiameter+" ,'X') > 1 then substr("+mc_FieldNameDiameter+",1,instr("+mc_FieldNameDiameter+",'X')-1) else diameter end ) as int) asc,"+mc_FieldNameDiameter;
        //                strSQL = "select " + mc_FieldNameDiameter + "," + mc_FieldNameVstate + ", count(*) Vcount from " + m_CurParameter.GWUserName + ".valve where trim(diameter) is not null " + strWhere + " and trim(datatype) <> " + mc_DataTypeTempPipe + " group by " + mc_FieldNameDiameter + "," + mc_FieldNameVstate + " order by cast((case when instr(" + mc_FieldNameDiameter + " ,'X') > 1 then substr(" + mc_FieldNameDiameter + ",1,instr(" + mc_FieldNameDiameter + ",'X')-1) else diameter end ) as int) asc," + mc_FieldNameDiameter;
        //            }
        //            else
        //            {
        //                strSQL = "select " + mc_FieldNameDiameter + "," + mc_FieldNameVstate + ", count(*) Vcount from " + m_CurParameter.GWUserName + ".valve where trim(diameter) is not null " + strWhere + " and trim(datatype) <> " + mc_DataTypeTempPipe + " and SZTYPE ='" + cboWaterType.Text + "' group by " + mc_FieldNameDiameter + "," + mc_FieldNameVstate + " order by cast((case when instr(" + mc_FieldNameDiameter + " ,'X') > 1 then substr(" + mc_FieldNameDiameter + ",1,instr(" + mc_FieldNameDiameter + ",'X')-1) else diameter end ) as int) asc," + mc_FieldNameDiameter;
        //            }
        //            m_objDBCON.ExecuteSQLReturn(strSQL, ref adoReader);
        //        }

        //        if (adoReader != null)
        //        {
        //            //删除本地mdb中的valvestate表中的数据（不包括列头）
        //            strSQL = "Delete * from " + mc_TableValveState;
        //            //strSQL = "SELECT DIAMETER FROM " + mc_TableValveState; 
        //            con.Open();
        //            OleDbCommand cmd = con.CreateCommand();
        //            cmd.CommandText = strSQL;
        //            cmd.ExecuteNonQuery();

        //            //adoAppReader = cmd.ExecuteReader();
        //            //if (adoReader.Read()!=false)
        //            //{                            
        //            //}      

        //            int i = 0;
        //            while (adoReader.Read())
        //            {
        //                i++;
        //                if (i <= 1)
        //                {
        //                    strOldDiameter = CNullValue(adoReader[mc_FieldNameDiameter].ToString(), DataFieldType.DString);
        //                }
        //                strDiameter = CNullValue(adoReader[mc_FieldNameDiameter].ToString(), DataFieldType.DString);
        //                if (strOldDiameter != strDiameter)
        //                {
        //                    strSQL = "Insert into " + mc_TableValveState + " values ('" + strOldDiameter + "','" + (dblRoadCount + dblUserCount) + "','" + dblRoadCount + "','" + dblUserCount + "')";
        //                    cmd.CommandText = strSQL;
        //                    cmd.ExecuteNonQuery();
        //                    dblRoadCount = 0;
        //                    dblUserCount = 0;
        //                }
        //                switch (CNullValue(adoReader[mc_FieldNameVstate].ToString(), DataFieldType.DString))
        //                {
        //                    case mc_RoadValve:
        //                        dblRoadCount = dblRoadCount + Convert.ToDouble(adoReader["VCount"]);
        //                        dblAllRoadCount = dblAllRoadCount + Convert.ToDouble(adoReader["VCount"]);
        //                        dblAllCount = dblAllCount + Convert.ToDouble(adoReader["VCount"]);
        //                        break;
        //                    case mc_UserValve:
        //                        dblUserCount = dblUserCount + Convert.ToDouble(adoReader["VCount"]);
        //                        dblAllUserCount = dblAllUserCount + Convert.ToDouble(adoReader["VCount"]);
        //                        dblAllCount = dblAllCount + Convert.ToDouble(adoReader["VCount"]);
        //                        break;
        //                    default:
        //                        dblUserCount = dblUserCount + Convert.ToDouble(adoReader["VCount"]);
        //                        dblAllUserCount = dblAllUserCount + Convert.ToDouble(adoReader["VCount"]);
        //                        dblAllCount = dblAllCount + Convert.ToDouble(adoReader["VCount"]);
        //                        break;
        //                }
        //                strOldDiameter = strDiameter;
        //            }
        //            strSQL = "Insert into " + mc_TableValveState + " values('总计','" + dblAllCount + "','" + dblAllRoadCount + "','" + dblAllUserCount + "')";
        //            cmd.CommandText = strSQL;
        //            cmd.ExecuteNonQuery();
        //            adoReader.Close();
        //            con.Close();
        //            FrmReports frmRlt = new FrmReports();
        //            m_lstReportParameters.Add(mc_strNameReport_Valve);
        //            frmRlt.ProductReport(m_lstReportParameters);
        //            frmRlt.Show();

        //            this.Cursor = Cursors.Default;
        //        }
        //        else
        //        {
        //            MessageBox.Show("没有查到数据，用户密码是否过期？", "提示！");
        //            this.Cursor = Cursors.Default;
        //            adoReader.Close();
        //            con.Close();
        //            return;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message.ToString(), "错误!");
        //        this.Cursor = Cursors.Default;
        //        con.Close();
        //        return;
        //    }

        //}

        private string GetWhere(string m_cstrValveLayerName, ref bool blnTONGJI)
        {
            IFeatureLayerDefinition flDef;
            IFeatureLayer pFeatureLayer;
            string strDefinition = string.Empty, strReturnWhere = string.Empty;

            IQueryFilter pQueryFilter = new QueryFilterClass();
            try
            {
                pFeatureLayer = m_objMap.GetLayerByName(m_cstrValveLayerName).FeatureLayer;

                flDef = pFeatureLayer as IFeatureLayerDefinition;
                strDefinition = flDef.DefinitionExpression;
                pQueryFilter.WhereClause = strDefinition;
                if (pFeatureLayer.FeatureClass.FeatureCount(pQueryFilter) >= 0)
                {
                    blnTONGJI = true;
                    if (strDefinition.Trim() != "")
                    {
                        strReturnWhere = "and " + strDefinition;
                    }
                    pQueryFilter = null;
                }
                else
                {
                    MsgBox.Show("图层" + pFeatureLayer.FeatureClass.AliasName + "中没有数据，不能进行统计！");
                    blnTONGJI = false;
                    strReturnWhere = string.Empty;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("地图中未发现图层：" + m_cstrValveLayerName, "获取查询语句条件模块发生错误");
                return null;
            }

            return strReturnWhere;
        }

        public string CNullValue(string Value, DataFieldType strType)
        {
            string CNullValue = string.Empty;
            if (String.IsNullOrEmpty(Value))
            {
                switch (strType)
                {
                    case DataFieldType.DString:
                        CNullValue = "";
                        break;
                    case DataFieldType.DNumber:
                        CNullValue = "0";
                        break;
                    default:
                        CNullValue = null;
                        break;
                }
            }
            else
            {
                CNullValue = Value.ToString().Trim();
            }
            CNullValue = CNullValue.Replace("'", "");
            return CNullValue;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public OleDbDataReader Get_sqlDBReader(string sql, ref OleDbDataReader aReader)
        {
            try
            {
                ////方法一方法二失效原来账户过期，或者没带表前缀
                //string constr = "Provider=OraOLEDB.Oracle;Data Source=jjwater;Persist Security Info=True;User ID=jjwater;Password=jjwater;Unicode=True";
                //m_OldbConn = new OleDbConnection(constr);
                //m_OldbConn.Open();
                //using (var cmd = new OleDbCommand(sql, m_OldbConn))
                //{
                //    return aReader = cmd.ExecuteReader();
                //}

                //方法二
                m_objDBCON = CDBCon.GetInstance();
                SysParameters m_CurParameter = SysParameters.GetInstance();
                OleDbDataReader adoReader = null;
                m_objDBCON.ExecuteSQLReturn(sql, ref adoReader);
                return adoReader;
            }
            catch (Exception ex)
            {
                //m_objDBCON.CloseCon();
                MsgBox.Show(ex.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 导出统计结果到Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExprtExcel_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab == this.tabPage1)
            {
                if (optValvestat.Checked)
                {
                    //配水管线闸门统计
                    ValveStateReport2Excel();
                }
                else if (optPipeLenStat.Checked)
                {
                    //市区配水管线长度统计
                    MainsReport2Excel();
                }
                else if (optPipeInvestAddTable.Checked)
                {
                    //固定资产增加汇总表
                    PipeInvestAddTotalTable2Excel();
                }
                else if (optPipeInvestAddDetail.Checked)
                {
                    //固定资产增加明细表
                    PipeInvestAddTableDetail2Excel();
                }
            }
            else if (this.tabControl1.SelectedTab == this.tabPage2)
            {
                if (optNewPipeLenMonthStat.Checked)
                {
                    //新增管线长度统计月报
                    NewLineLenMonthReport2Excel(1);
                }
                else if (optNewHydrantMonthStat.Checked)
                {
                    //新安消火栓个数统计月报
                    HydrantMonthReport2Excel();
                }
                else if (optNewWTPipeLenMonthStat.Checked)
                {
                    //新增配水管线长度统计月报
                    NewLineLenMonthReport2Excel(2);
                }
                else if (optNewSWTPipeLenMonthStat.Checked)
                {
                    //新增输水管线长度统计月报                
                    NewLineLenMonthReport2Excel(3);
                }
            }
            else if (this.tabControl1.SelectedTab == this.tabPage3)
            {
                if (rdBtn_PipeLenMons.Checked)
                {
                    //撤管长度统计
                    RemovedPipeStat();
                }
            }
            else if (this.tabControl1.SelectedTab == this.tabPage4)
            {
                if (rdBtn_GWAnnulReport.Checked)
                {
                    //管网统计年报
                    GWAnnualStatReport();
                }
            }
        }
        /// <summary>
        /// 管网统计年报
        /// </summary>
        private void GWAnnualStatReport()
        {
            string strSQL = string.Empty, strPipeSectionMain = string.Empty, strPipeSectionUser = string.Empty, strPipeSectionSource = string.Empty;
            string strBegDate = string.Empty;
            string strEndDate = string.Empty;

            DataTable dDataTableA = new DataTable();
            DataTable dDataTableB = new DataTable();

            OleDbDataReader adoReader = null;

            strPipeSectionMain = string.Format("(select * from jjwater.pipesectionmain a,jjwater.f{0} b where a.shape=b.fid)",
                getLayerID("PIPESECTIONMAIN"));
            strPipeSectionUser = string.Format("(select * from jjwater.pipesectionuser a,jjwater.f{0} b where a.shape=b.fid)",
                getLayerID("PIPESECTIONUSER"));

            strPipeSectionSource = string.Format("(select * from jjwater.pipesectionsource a,jjwater.f{0} b where a.shape=b.fid)",
                getLayerID("PIPESECTIONSOURCE"));

            m_pFeatureLayer = m_objMap.GetLayerByName(mc_strPipesectionMainName).FeatureLayer;
            m_dicColumnCodeDesc = GetSubtypes(m_pFeatureLayer.FeatureClass, mc_FieldNameMaterial);
            m_dicRowCodeDesc = GetSubtypes(m_pFeatureLayer.FeatureClass, mc_FieldNameDiameter);

            DateTime dtBegDate = Convert.ToDateTime(dtpBegStateDate4.Text);
            DateTime dtEndDate = Convert.ToDateTime(dtpEndStateDate4.Text);

            strBegDate = dtBegDate.ToString("yyyyMMdd");//20140806 大小写有区别
            strEndDate = dtEndDate.ToString("yyyyMMdd");//20010101

            Boolean blnTONGJI1 = false;
            Boolean blnTONGJI2 = false;
            Boolean blnTONGJI3 = false;

            string strWherePipesectionMain = GetWhere(mc_strPipesectionMainName, ref blnTONGJI1);
            string strWherePipesectionUser = GetWhere(mc_strPipesectionUserName, ref blnTONGJI2);

            string strWherePipesectionSource = GetWhere(mc_strPipesectionSourceName, ref blnTONGJI3);

            if (strWherePipesectionMain.Trim() != "")
            {
                strWherePipesectionMain = " and " + strWherePipesectionMain;
            }
            if (strWherePipesectionUser.Trim() != "")
            {
                strWherePipesectionUser = " and " + strWherePipesectionUser;
            }
            if (strWherePipesectionSource.Trim() != "")
            {
                strWherePipesectionSource = " and " + strWherePipesectionSource;
            }

            if (blnTONGJI1 && blnTONGJI2 && blnTONGJI3)
            {
                //判断时间条件类型，小于或者小于等于按要求使用录入时间，其他使用审核时间。
                if (cboCheckDt4.Text == mc_SmallEqualSymbol || cboCheckDt4.Text == mc_SmallSymbol)
                {
                    //X:供水；Y:输水
                    strSQL =
                        "select diameter,material,sum(cd) shape_length, flag from ((select diameter,material,len cd ,'x' as flag from " +
                        strPipeSectionMain + " where " +
                        AnnualDtComplex +
                        //"where STATUS IS NULL or (STATUS <> " + mc_StatusDisusedPipe + " and STATUS <> " + mc_StatusTempPipe + 
                        //"where STATUS=" + mc_StatusCompletionPipe + 
                        //2015-03-13 周:涉及到时间的统计均不统计1900.有问题时他们都会把录入日期改为1900；日期为空不统计
                        //2015-06-18 贾：之前统计用录入时间同时统计为空的
                        //" and WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')" +
                        //")) union all " +
                        ") union all " +
                        "(select diameter,material,len cd , 'x' as flag from " +
                        strPipeSectionUser + " where " +
                        AnnualDtComplex +
                        //"where STATUS=" + mc_StatusCompletionPipe +
                        //2015-03-13 周:涉及到时间的统计均不统计1900.有问题时他们都会把录入日期改为1900；日期为空不统计
                        //2015-06-18 贾：之前统计用录入时间同时统计为空的
                        //        " and  WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')" +
                        ") union all" +
                        " (select diameter,material,len cd ,'y' as flag from " +
                        strPipeSectionSource + " " +
                        ")) group by diameter,material,flag order by  " + mc_FieldNameDiameter;

                }
                else
                {
                    strSQL =
                        "select diameter,material,sum(cd) shape_length, flag from ((select diameter,material,len cd, 'x' as flag from " +
                        strPipeSectionMain + " where " +
                        AnnualDtComplex +
                        //"where STATUS IS NULL or (STATUS <> " + mc_StatusDisusedPipe + " and STATUS <> " + mc_StatusTempPipe + 
                        //"where STATUS=" + mc_StatusCompletionPipe + 
                        //2015-03-13 周:涉及到时间的统计均不统计1900.有问题时他们都会把录入日期改为1900；日期为空不统计
                        " and WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')" +
                        ") union all " +
                        "(select diameter,material,len cd,'x' as flag from " +
                        strPipeSectionUser + " where " +
                        AnnualDtComplex +
                        " and WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')" +
                        //"where STATUS=" + mc_StatusCompletionPipe +
                        //2015-03-13 周:涉及到时间的统计均不统计1900.有问题时他们都会把录入日期改为1900；日期为空不统计
                         ") union all" +
                        " (select diameter,material,len cd ,'y' as flag from " +
                        strPipeSectionSource + " " + "where" +
                        AnnualDtComplex +
                                " and  WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')" +
                        ")) group by diameter,material,flag order by  " + mc_FieldNameDiameter;

                }
                try
                {
                    if (m_objDBCON == null)
                        m_objDBCON = CDBCon.GetInstance();
                    m_objDBCON.ExecuteSQLReturn(strSQL, ref adoReader);
                    //Get_sqlDBReader(strSQL, ref adoReader);
                    if (adoReader.HasRows)
                    {
                        string strDBresult = string.Empty;
                        string strDiameter = string.Empty;
                        string strMaterial = string.Empty;
                        string strNewRecord = string.Empty;
                        string strSameRecord = string.Empty;
                        double dblTempLength = 0;

                        string strFlag = string.Empty;


                        while (adoReader.Read())
                        {
                            strDBresult = "";
                            dblTempLength = 0;
                            strDiameter = adoReader[mc_FieldNameDiameter].ToString().Trim();
                            strMaterial = adoReader[mc_FieldNameMaterial].ToString().Trim();
                            strFlag = adoReader["flag"].ToString().ToUpper();


                            if (adoReader[mc_FieldNameDiameter].ToString().Trim() == "")
                            {
                                strDiameter = mc_strUnknownDiameter;
                            }
                            //其他材质管：除了钢、球墨铸铁、铸铁、塑料水泥外，都是其他。条件需要再添加
                            if (adoReader[mc_FieldNameMaterial].ToString().Trim() == "")
                            {
                                strMaterial = mc_strUnknownMaterial;
                            }

                            if (!m_lstRow.Contains(strDiameter))
                            {
                                m_lstRow.Add(strDiameter);
                            }
                            if (!m_lstColumns.Contains(strMaterial))
                            {
                                m_lstColumns.Add(strMaterial);
                            }


                            //存在数据不规范的情况，如重复记录，多n个空格，为“”，需要再判断，计算；

                            foreach (string r in m_lstDBResults)
                            {
                                string[] s = r.Split(',');
                                if ((strDiameter + "," + strMaterial + "," + strFlag) == (s[0] + "," + s[1] + "," + s[3]))
                                {
                                    strSameRecord = r;
                                    dblTempLength = Convert.ToDouble(s[2]) + Convert.ToDouble(adoReader[mc_FieldNameShapeLength]);
                                    strNewRecord = s[0] + "," + s[1] + "," + dblTempLength.ToString() + "," + strFlag;
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
                                strDBresult = strDiameter + "," + strMaterial + "," + adoReader[mc_FieldNameShapeLength].ToString() + "," + strFlag;
                                m_lstDBResults.Add(strDBresult);
                            }
                        }
                        //为数据表创建列
                        for (int i = 0; i < m_lstColumns.Count; i++)
                        {
                            //供水统计信息
                            if (i == 0)
                            {
                                dDataTableA.Columns.Add(mc_TableItem, Type.GetType("System.String"));
                                dDataTableA.Columns.Add(mc_TableTotal, Type.GetType("System.Decimal"));
                                dDataTableA.Columns.Add(mc_TableItemSum, Type.GetType("System.Decimal"));
                            }
                            dDataTableA.Columns.Add(m_lstColumns[i] + "X", Type.GetType("System.Decimal"));
                            if (i == m_lstColumns.Count - 1)
                            {
                                //添加取水统计
                                dDataTableA.Columns.Add(mc_TableItemSum + "Y", Type.GetType("System.Decimal"));
                                for (int j = 0; j < m_lstColumns.Count; j++)
                                {
                                    dDataTableA.Columns.Add(m_lstColumns[j] + "Y", Type.GetType("System.Decimal"));
                                }
                            }
                        }
                        // m_lstColumns.Add(mc_TableItemSum);
                        //数据表添加行头
                        for (int j = 0; j < m_lstRow.Count; j++)
                        {
                            DataRow dr = dDataTableA.NewRow();
                            dr[mc_TableItem] = "DN" + m_lstRow[j] + "mm";
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

                        int count = 0, IndexofHejiY = 0;
                        string strCodeofColumn = string.Empty;
                        string strCodeofRow = string.Empty;
                        string strOldColumnItem = string.Empty;
                        string strOldRowItem = string.Empty;

                        foreach (DataRow dr in dDataTableA.Rows)
                        {
                            count++;
                            double dblHejiX = 0, dblHejiY = 0;
                            int indexOfColumnsMaterial = 0;
                            //dr.ItemArray[0]～dr.ItemArray[29]
                            for (int k = 3; k < dr.ItemArray.Length; k++)
                            {
                                //code作为查询条件，description和code的替换
                                string strSqlQueryDBvalue = string.Empty;
                                //逐列判别属于哪个材质：2015-7-3
                                //为输水合计
                                if (k == m_lstColumns.Count + 3)
                                {
                                    //输水合计序号
                                    IndexofHejiY = k;
                                    //供水合计
                                    dr[2] = dblHejiX;
                                    indexOfColumnsMaterial = 0;
                                    continue;
                                }


                                if (k > m_lstColumns.Count + 3 - 1)//口径、总计、合计，0开始计算
                                {
                                    indexOfColumnsMaterial = k - m_lstColumns.Count - 4;
                                }
                                else
                                {
                                    indexOfColumnsMaterial = k - 3;
                                }

                                #region 若存在子类型，需要用子类型的码值查询
                                if (m_dicColumnCodeDesc.ContainsValue(m_lstColumns[indexOfColumnsMaterial]))
                                {
                                    if (strOldColumnItem != m_lstColumns[indexOfColumnsMaterial])
                                    {
                                        foreach (KeyValuePair<string, string> kvp in m_dicColumnCodeDesc)
                                        {
                                            if (kvp.Value.Equals(m_lstColumns[indexOfColumnsMaterial]))
                                            {
                                                strCodeofColumn = kvp.Key;
                                                break;
                                            }
                                        }
                                        strOldColumnItem = m_lstColumns[indexOfColumnsMaterial];
                                    }
                                }
                                else
                                {
                                    strCodeofColumn = m_lstColumns[indexOfColumnsMaterial];
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

                                #endregion

                                //判断并赋值,dr[k]:该行（管径）的合计，直到最后一个列材质结束才罢休
                                foreach (string r in m_lstDBResults)
                                {
                                    string[] s = r.Split(',');
                                    if (k < m_lstColumns.Count + 3 + 1)
                                    {
                                        if (strCodeofRow + "," + strCodeofColumn + "," + "X" == "DN" + s[0] + "mm" + "," + s[1] + "," + s[3].ToUpper())
                                        {
                                            dr[k] = Math.Round(Convert.ToDecimal(s[2]), 2);
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (strCodeofRow + "," + strCodeofColumn + "," + "Y" == "DN" + s[0] + "mm" + "," + s[1] + "," + s[3].ToUpper())
                                        {
                                            //dr[indexOfColumnsMaterial + m_lstColumns.Count+1] = Math.Round(Convert.ToDecimal(s[2]), 3);
                                            dr[k] = Math.Round(Convert.ToDecimal(s[2]), 2);
                                            break;
                                        }
                                    }


                                }

                                if (k == dr.ItemArray.Length - 1)
                                {
                                    //输水合计
                                    dr[IndexofHejiY] = dblHejiY + Convert.ToDouble(dr[indexOfColumnsMaterial + m_lstColumns.Count + 3 + 1]);
                                    //总计
                                    dr[1] = System.Convert.ToDouble(dr[IndexofHejiY]) + System.Convert.ToDouble(dr[2]);
                                }
                                else
                                {
                                    dblHejiX = dblHejiX + Convert.ToDouble(dr[k]);
                                    dblHejiY = dblHejiY + Convert.ToDouble(dr[indexOfColumnsMaterial + m_lstColumns.Count + 3 + 1]);//indexOfColumnsMaterial + m_lstColumns.Count + 3 + 1=k
                                }


                            }//end for (int k = 1; k < dr.ItemArray.Length; k++)

                        }//end foreach (DataRow dr in dDataTableA.Rows)

                    }
                    else
                    {
                        MessageBox.Show("所选统计类型记录为空,导出终止！", "提示！");
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误");
                    m_lstDBResults.Clear();
                    return;
                }


            }

            DateTime now = DateTime.Now;
            SaveFileDialog objSave = new SaveFileDialog();
            objSave.Filter = "电子表格(*.xls)|*.xls|所有文件(*.*)|*.*";
            objSave.FilterIndex = 0;
            objSave.RestoreDirectory = true;
            objSave.AddExtension = true;
            objSave.CreatePrompt = true;
            objSave.Title = "导出" + mc_strNameReport_GWAnnual;
            objSave.FileName = mc_strNameReport_GWAnnual + now.Year.ToString().PadLeft(2, '0') + now.Month.ToString().PadLeft(2, '0') + now.Day.ToString().PadLeft(2, '0')
                + now.Hour.ToString().PadLeft(2, '0') + now.Minute.ToString().PadLeft(2, '0') + now.Second.ToString().PadLeft(2, '0') + ".xls";
            try
            {
                if (objSave.ShowDialog() == DialogResult.OK)
                {
                    StringWriter sw = new StringWriter();
                    sw.WriteLine("管 网 管 线 长 度 统 计 年 报");
                    sw.WriteLine("");
                    sw.WriteLine("\u0020\u0020 填报单位：管网管理分公司 \t \t \t \t {0} \t \t \t \t", dtBegDate.Year + "年");
                    sw.WriteLine("");
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
                                        objLastRowofTable[0] = mc_TableItemSum; //合计  //mc_TableSubtotal 小计
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
                    //sw.Write("\r\n 单位负责人:\t\t\t\t\t\t填表人:\t\t\t\t\t\t报出日期：\t");
                    byte[] s = System.Text.Encoding.GetEncoding("gb2312").GetBytes(sw.ToString());
                    FileStream fs = new FileStream(objSave.FileName, FileMode.Create, FileAccess.Write);
                    fs.Write(s, 0, s.Length);
                    fs.Close();
                    if (MessageBox.Show("导出完毕,点击【确定】打开！", "提示", MessageBoxButtons.OK) == DialogResult.OK)
                    {
                        string strMacroName = "PERSONAL.XLSB!管网管线长度统计年报";
                        OpenFolderAndSelectFile(objSave.FileName, m_strExcelTemplate, strMacroName);
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
                m_pFeatureLayer = null;
                m_dicColumnCodeDesc.Clear();
                m_dicRowCodeDesc.Clear();
                m_lstDBResults.Clear();
            }

        }



        /// <summary>
        /// 撤除管线统计
        /// </summary>
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
            " and " + RemoveDtComplex +
            " ) union all" +
            " (select diameter,material,len cd from " +
            strPipeSectionUser + " where " +
            " writedate is not null and writedate>to_date('194910011500','yyyyMMddhh24:mi:ss')" +
            " and " + RemoveDtComplex +
            " ) union all" +
            "(select diameter,material,len cd from " +
            strPipeSectionSource + " where " +
            " writedate is not null and writedate>to_date('194910011500','yyyyMMddhh24:mi:ss')" +
            " and " + RemoveDtComplex +
            " )) group by diameter,material order by diameter";

            try
            {
                if (m_objDBCON == null)
                    m_objDBCON = CDBCon.GetInstance();
                m_objDBCON.ExecuteSQLReturn(strSQL, ref adoReader);
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
                    sw.WriteLine("\t\t\t\t\t\t填报日期:\t\t单位：m");
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
                    sw.WriteLine("\r\n");
                    sw.WriteLine("\r\n 主管领导:\t\t财务负责人:\t\t审核人:\t\t制表人:");

                    byte[] s = System.Text.Encoding.GetEncoding("gb2312").GetBytes(sw.ToString());
                    FileStream fs = new FileStream(objSave.FileName, FileMode.Create, FileAccess.Write);
                    fs.Write(s, 0, s.Length);
                    fs.Close();
                    if (MessageBox.Show("导出完毕,点击【确定】打开！", "提示", MessageBoxButtons.OK) == DialogResult.OK)
                    {
                        string strMacroName = "PERSONAL.XLSB!报废管线统计表";
                        OpenFolderAndSelectFile(objSave.FileName, m_strExcelTemplate, strMacroName);
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

        /// <summary>
        /// 2015-01-06
        /// 固定资产增加汇总表
        /// </summary>
        private void PipeInvestAddTotalTable2Excel()
        {
            string TN_LEN_PMain = string.Empty;
            string TN_LEN_PUser = string.Empty;
            string TN_LEN_PSource = string.Empty;

            try
            {
                if (!m_blISCAL)
                {
                    if (MessageBox.Show("统计前是否重新计算原值？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (ReCalculateDrawprice() == 0)
                        {
                            m_blISCAL = true;
                        }
                        else
                        {
                            m_blISCAL = false;
                            return;
                        }
                    }
                }

                DataTable dDataTableA = new DataTable();

                string m_Vn_PipeMain = string.Empty;
                string m_Vn_PipeUser = string.Empty;
                string m_Vn_PipeSource = string.Empty;

                if (m_objMap.GetLayerByName(mc_strPipesectionMainName) == null || m_objMap.GetLayerByName(mc_strPipesectionUserName) == null || m_objMap.GetLayerByName(mc_strPipesectionSourceName) == null)
                {
                    m_Vn_PipeMain = "PIPESECTIONMAIN";
                    m_Vn_PipeUser = "PIPESECTIONUSER";
                    m_Vn_PipeSource = "PIPESECTIONSOURCE";

                }
                else
                {
                    m_Vn_PipeMain = m_objMap.GetLayerByName(mc_strPipesectionMainName).LayerTableName;
                    m_Vn_PipeUser = m_objMap.GetLayerByName(mc_strPipesectionUserName).LayerTableName;
                    m_Vn_PipeSource = m_objMap.GetLayerByName(mc_strPipesectionSourceName).LayerTableName;

                }


                if (m_CurParameter == null)
                    m_CurParameter = SysParameters.GetInstance();

                string m_PrifixName = m_CurParameter.GWUserName;

                TN_LEN_PMain = m_PrifixName + ".f" + getLayerID(m_Vn_PipeMain);
                TN_LEN_PUser = m_PrifixName + ".f" + getLayerID(m_Vn_PipeUser);
                TN_LEN_PSource = m_PrifixName + ".f" + getLayerID(m_Vn_PipeSource);

                string strSelInvest = string.Empty;
                string strBuildDateCondition = string.Empty;
                string strBuilDateTitle = string.Empty;

                FrmSelInvest frmInvest = new FrmSelInvest();
                frmInvest.Init(m_lstInvest);
                if (frmInvest.ShowDialog() == DialogResult.OK)
                {

                    strSelInvest = frmInvest.cboInvestStyle.Text;
                    //20150206 固定资产不统计录入时间为19000101或者为空的的记录
                    //strBuildDateCondition = frmInvest.BuildDtComplex;
                    strBuildDateCondition = frmInvest.BuildDtComplex + " and WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')";

                    strBuilDateTitle = frmInvest.BuildDataTitle;
                    frmInvest.Close();
                }
                if (strSelInvest.Trim() == "")
                {
                    return;
                }
                string strTemp = string.Empty, strFields = string.Empty, strSQL1 = string.Empty, strSQL2 = string.Empty, strSQL3 = string.Empty, strSQL = string.Empty;

                this.Cursor = Cursors.WaitCursor;

                if (cboWaterType.Text == mc_strWaterType_All)
                {
                    //Main
                    strSQL1 = "select a.MATERIAL as 材质,a.DIAMETER as 管径,'米' as 单位,sum(L.LEN) as 长度,sum(a.DRAWPRICE) as 原值," +
                        "sum(a.DRAWPRICE)*0.04 as 月折旧额,'' as 备注" +
                        " from " + m_PrifixName + "." + m_Vn_PipeMain + " a, " + TN_LEN_PMain + " L " +
                        " where A.Shape=L.FID " +
                        " and trim(" + mc_Fn_WorkChar + ")='" + strSelInvest + "' and " + mc_Fn_Drawprice + "<>0 and not " + mc_Fn_Drawprice + " is null " +
                        " and " + strBuildDateCondition +
                        //2015-01-19 状态由<>废弃 \临时 改为：状态=竣工
                        //" and a.STATUS is null or (trim(a.STATUS)<>" + mc_StatusDisusedPipe + " and trim(a.STATUS)<>" + mc_StatusTempPipe + ") " +
                        //" and trim(a.STATUS)=" + mc_StatusCompletionPipe + //临时去掉20150122
                        //2015-03-03 不统计数据类型为刨找的
                        //2015-03-13 周婧 仅统计录入日期不为空，且大于1900，刨找先不区分。
                        //" and (A.DATATYPE is NULL or trim(A.DATATYPE)<>" + mc_strDataTypeDigFound + ")" +
                        " group by a.MATERIAL,a.DIAMETER" +
                        " ";
                    //User
                    strSQL2 = "select a.MATERIAL as 材质,a.DIAMETER as 管径,'米' as 单位,sum(L.LEN) as 长度,sum(a.DRAWPRICE) as 原值," +
                        "sum(a.DRAWPRICE)*0.04 as 月折旧额,'' as 备注" +
                        " from " + m_PrifixName + "." + m_Vn_PipeUser + " a, " + TN_LEN_PUser + " L " +
                        " where A.Shape=L.FID " +
                        " and trim(" + mc_Fn_WorkChar + ")='" + strSelInvest + "' and " + mc_Fn_Drawprice + "<>0 and not " + mc_Fn_Drawprice + " is null " +
                        " and " + strBuildDateCondition +
                        //" and trim(a.STATUS)=" + mc_StatusCompletionPipe +

                        //2015-03-03 不统计数据类型为刨找的
                        //" and (A.DATATYPE is NULL or trim(A.DATATYPE)<>" + mc_strDataTypeDigFound + ")" +
                        " group by a.MATERIAL,a.DIAMETER" +
                        "";
                    //source
                    strSQL3 = "select a.MATERIAL as 材质,a.DIAMETER as 管径,'米' as 单位,sum(L.LEN) as 长度,sum(a.DRAWPRICE) as 原值," +
                        "sum(a.DRAWPRICE)*0.04 as 月折旧额,'' as 备注" +
                        " from " + m_PrifixName + "." + m_Vn_PipeSource + " a, " + TN_LEN_PSource + " L " +
                        " where A.Shape=L.FID " +
                        " and trim(" + mc_Fn_WorkChar + ")='" + strSelInvest + "' and " + mc_Fn_Drawprice + "<>0 and not " + mc_Fn_Drawprice + " is null " +
                        " and " + strBuildDateCondition +
                        //" and trim(a.STATUS)=" + mc_StatusCompletionPipe +

                        //2015-03-03 不统计数据类型为刨找的
                        //" and (A.DATATYPE is NULL or trim(A.DATATYPE)<>" + mc_strDataTypeDigFound + ")" +
                        " group by a.MATERIAL,a.DIAMETER" +
                        "";
                }
                else
                {
                    //Main
                    strSQL1 = "select a.MATERIAL as 材质,a.DIAMETER as 管径,'米' as 单位,sum(L.LEN) as 长度,sum(a.DRAWPRICE) as 原值," +
                        "sum(a.DRAWPRICE)*0.04 as 月折旧额,'' as 备注" +
                        " from " + m_PrifixName + "." + m_Vn_PipeMain + " a, " + TN_LEN_PMain + " L " +
                        " where A.Shape=L.FID " +
                        " and trim(" + mc_Fn_WorkChar + ")='" + strSelInvest + "' and " + mc_Fn_Drawprice + "<>0 and not " + mc_Fn_Drawprice + " is null " +
                        " and " + strBuildDateCondition +
                        //" and a.STATUS is null or (trim(a.STATUS)<>" + mc_StatusDisusedPipe + " and trim(a.STATUS)<>" + mc_StatusTempPipe + ") " + " and a.WATERTYPE='" + cboWaterType.Text + "' " +
                        //" and trim(a.STATUS)=" + mc_StatusCompletionPipe + //临时去掉20150122
                        //2015-03-03 不统计数据类型为刨找的
                        //" and (A.DATATYPE is NULL or trim(A.DATATYPE)<>" + mc_strDataTypeDigFound + ")" +
                        " and a.WATERTYPE='" + cboWaterType.Text + "' " +
                        " group by a.MATERIAL,a.DIAMETER" +
                        "";
                    //User
                    strSQL2 = "select a.MATERIAL as 材质,a.DIAMETER as 管径,'米' as 单位,sum(L.LEN) as 长度,sum(a.DRAWPRICE) as 原值," +
                        "sum(a.DRAWPRICE)*0.04 as 月折旧额,'' as 备注" +
                        " from " + m_PrifixName + "." + m_Vn_PipeUser + " a, " + TN_LEN_PUser + " L " +
                        " where A.Shape=L.FID " +
                        " and trim(" + mc_Fn_WorkChar + ")='" + strSelInvest + "' and " + mc_Fn_Drawprice + "<>0 and not " + mc_Fn_Drawprice + " is null " +
                        " and " + strBuildDateCondition +
                        //" and trim(a.STATUS)=" + mc_StatusCompletionPipe + 
                        //2015-03-03 不统计数据类型为刨找的
                        //" and (A.DATATYPE is NULL or trim(A.DATATYPE)<>" + mc_strDataTypeDigFound + ")" +
                         " and a.WATERTYPE='" + cboWaterType.Text + "' " +
                        " group by a.MATERIAL,a.DIAMETER" +
                        "";
                    //source
                    strSQL3 = "select a.MATERIAL as 材质,a.DIAMETER as 管径,'米' as 单位,sum(L.LEN) as 长度,sum(a.DRAWPRICE) as 原值," +
                        "sum(a.DRAWPRICE)*0.04 as 月折旧额,'' as 备注" +
                        " from " + m_PrifixName + "." + m_Vn_PipeSource + " a, " + TN_LEN_PSource + " L " +
                        " where A.Shape=L.FID " +
                        " and trim(" + mc_Fn_WorkChar + ")='" + strSelInvest + "' and " + mc_Fn_Drawprice + "<>0 and not " + mc_Fn_Drawprice + " is null " +
                        " and " + strBuildDateCondition +
                        //" and trim(a.STATUS)=" + mc_StatusCompletionPipe + 
                        //2015-03-03 不统计数据类型为刨找的                    
                        //" and (A.DATATYPE is NULL or trim(A.DATATYPE)<>" + mc_strDataTypeDigFound + ")" +
                         " and a.WATERTYPE='" + cboWaterType.Text + "' " +
                        " group by a.MATERIAL,a.DIAMETER" +
                        "";
                }

                //strSQL = " select distinct 材质,管径,round(sum(长度),3) as 长度,round(sum(原值),2) as 原值,round(sum(月折旧额),2) as 月折旧额,'' as 备注 from (" + strSQL1 + " Union " + strSQL2 + " " + " Union " + strSQL3 + ") group by 材质,管径 order by 材质,管径";
                //2015-03-04 要求按照档号\口径\材质的顺序统计结果;0313zhou 长度为3，原值为2；
                strSQL = " select distinct 材质,管径,round(sum(长度),3) as 长度,round(sum(原值),2) as 原值,round(sum(月折旧额),4) as 月折旧额,'' as 备注 from (" + strSQL1 + " Union " + strSQL2 + " " + " Union " + strSQL3 + ") group by 材质,管径 order by 材质,管径";
                dDataTableA = Get_QuerySheet(strSQL);

                if (dDataTableA.Rows.Count < 1)
                {
                    MessageBox.Show("所选统计类型记录为空，导出终止！", "提示！");
                    this.Cursor = Cursors.Default;
                    return;
                }
                DateTime now = DateTime.Now;
                SaveFileDialog objSave = new SaveFileDialog();
                objSave.Filter = "电子表格(*.xls)|*.xls|所有文件(*.*)|*.*";
                objSave.FilterIndex = 0;
                objSave.RestoreDirectory = true;
                objSave.AddExtension = true;
                objSave.CreatePrompt = true;
                objSave.Title = "导出" + mc_strNameReport_PipeInvestAddTotal;
                objSave.FileName = mc_strNameReport_PipeInvestAddTotal + now.Year.ToString().PadLeft(2, '0') + now.Month.ToString().PadLeft(2, '0') + now.Day.ToString().PadLeft(2, '0')
                    + now.Hour.ToString().PadLeft(2, '0') + now.Minute.ToString().PadLeft(2, '0') + now.Second.ToString().PadLeft(2, '0') + ".xls";

                if (objSave.ShowDialog() == DialogResult.OK)
                {
                    double dbl_YZJESum = 0;
                    double dbl_ChangDSum = 0;
                    double dbl_YuanZSum = 0;
                    StringWriter sw = new StringWriter();
                    sw.WriteLine("{0}(水质类型：{1}){2}", mc_strNameofJJWaterltd, cboWaterType.Text, mc_strNameReport_PipeInvestAddTotal);
                    sw.WriteLine("");
                    sw.WriteLine("投资方式：{0}", strSelInvest);
                    sw.WriteLine("统计区间：{0}", strBuilDateTitle);
                    sw.WriteLine("");

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
                            sw.Write(dDataTableA.Rows[i][j].ToString().Trim() + "\t");
                        }
                        //累加月折旧额
                        if (dDataTableA.Rows[i][dDataTableA.Columns.Count - 2] != null && !Convert.IsDBNull(dDataTableA.Rows[i][dDataTableA.Columns.Count - 2]) && dDataTableA.Rows[i][dDataTableA.Columns.Count - 2].ToString().Trim() != "")
                            dbl_YZJESum = Convert.ToDouble(dDataTableA.Rows[i][dDataTableA.Columns.Count - 2]) + dbl_YZJESum;
                        //累加原值
                        if (dDataTableA.Rows[i][dDataTableA.Columns.Count - 3] != null && !Convert.IsDBNull(dDataTableA.Rows[i][dDataTableA.Columns.Count - 3]) && dDataTableA.Rows[i][dDataTableA.Columns.Count - 3].ToString().Trim() != "")
                            dbl_YuanZSum = Convert.ToDouble(dDataTableA.Rows[i][dDataTableA.Columns.Count - 3]) + dbl_YuanZSum;
                        //累加长度
                        if (dDataTableA.Rows[i][dDataTableA.Columns.Count - 4] != null && !Convert.IsDBNull(dDataTableA.Rows[i][dDataTableA.Columns.Count - 4]) && dDataTableA.Rows[i][dDataTableA.Columns.Count - 4].ToString().Trim() != "")
                            dbl_ChangDSum = Convert.ToDouble(dDataTableA.Rows[i][dDataTableA.Columns.Count - 4]) + dbl_ChangDSum;

                        sw.Write("\r\n");
                    }
                    sw.WriteLine("月折旧额：{0} 元", Math.Round(dbl_YZJESum, 2));
                    sw.WriteLine("长度总计：{0} 米", Math.Round(dbl_ChangDSum, 3));//要求改成2位：2015-3-4；要求改为3位：2015-03-13；
                    sw.WriteLine("原值总计：{0} 元", Math.Round(dbl_YuanZSum, 2));
                    sw.Write("\r\n");
                    sw.WriteLine("主管领导:\t审核人:\t制表人:");
                    byte[] s = System.Text.Encoding.GetEncoding("gb2312").GetBytes(sw.ToString());
                    FileStream fs = new FileStream(objSave.FileName, FileMode.Create, FileAccess.Write);
                    fs.Write(s, 0, s.Length);
                    fs.Close();
                    this.Cursor = Cursors.Default;
                    if (MessageBox.Show("导出完毕,点击【确定】打开！", "提示", MessageBoxButtons.OK) == DialogResult.OK)
                    {
                        string strMacroName = "PERSONAL.XLSB!固定资产增加汇总表";
                        OpenFolderAndSelectFile(objSave.FileName, m_strExcelTemplate, strMacroName);
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
        }
        /// <summary>
        /// 重算原值；成功为0，失败为1；两个统计仅重算一次。
        /// </summary>
        /// <returns></returns>
        private int ReCalculateDrawprice()
        {
            //原值重算,重算哪些？
            //重算pipeproject中已经算过的；
            CalculateProjectCost cpc = new CalculateProjectCost();
            string strPipefileInPipeProject = string.Format("select t.pipefile from jjparameter.PIPEPROJECT t where t.ISCAL=1");
            OleDbDataReader adoReader = null;
            try
            {
                m_objDBCON.ExecuteSQLReturn(strPipefileInPipeProject, ref adoReader);
                if (adoReader != null)
                {
                    while (adoReader.Read())
                    {
                        if (adoReader.GetValue(0) != null && adoReader.GetValue(0).ToString().Trim() != "")
                            if (cpc.CalculateProjectCosting(adoReader.GetValue(0).ToString().Trim()) == 0)
                            {
                                //MessageBox.Show(adoReader.GetValue(0).ToString().Trim());
                            }
                            else
                            {
                                MessageBox.Show("非常抱歉，重算过程中遇到错误，请联系技术人员！", "错误！");
                                return 1;
                            }
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("非常抱歉，重算过程中遇到错误，请联系技术人员！\n" + ex.Message, "错误！");
                return 1;
            }
        }
        //固定资产增加明细表
        //2015-01-07

        private void PipeInvestAddTableDetail2Excel()
        {
            string TN_LEN_PMain = string.Empty;
            string TN_LEN_PUser = string.Empty;
            string TN_LEN_PSource = string.Empty;
            DataTable dDataTableM = new DataTable();
            DataTable dDataTableU = new DataTable();
            DataTable dDataTableS = new DataTable();
            DataTable dDataTableTotal = new DataTable();
            if (!m_blISCAL)
            {
                if (MessageBox.Show("统计前是否重新计算原值？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (ReCalculateDrawprice() == 0)
                    {
                        m_blISCAL = true;
                    }
                    else
                    {
                        m_blISCAL = false;
                        return;
                    }
                }
            }
            string m_Vn_Hydrant = string.Empty;
            string m_Vn_PipeMain = string.Empty;
            string m_Vn_PipeUser = string.Empty;
            string m_Vn_PipeSource = string.Empty;

            if (m_objMap.GetLayerByName(mc_strPipesectionMainName) == null || m_objMap.GetLayerByName(mc_strPipesectionUserName) == null || m_objMap.GetLayerByName(mc_strPipesectionSourceName) == null)
            {
                m_Vn_PipeMain = "PIPESECTIONMAIN";
                m_Vn_PipeUser = "PIPESECTIONUSER";
                m_Vn_PipeSource = "PIPESECTIONSOURCE";
                m_Vn_Hydrant = "HYDRANT";
            }
            else
            {
                //当前地图中若有找不到数据源的图层,则会出错!
                m_Vn_PipeMain = m_objMap.GetLayerByName(mc_strPipesectionMainName).LayerTableName;
                m_Vn_PipeUser = m_objMap.GetLayerByName(mc_strPipesectionUserName).LayerTableName;
                m_Vn_PipeSource = m_objMap.GetLayerByName(mc_strPipesectionSourceName).LayerTableName;
                m_Vn_Hydrant = m_objMap.GetLayerByName(mc_strHydrant).LayerTableName;

            }

            if (m_CurParameter == null)
                m_CurParameter = SysParameters.GetInstance();
            string m_PrifixName = m_CurParameter.GWUserName;

            TN_LEN_PMain = m_PrifixName + ".f" + getLayerID(m_Vn_PipeMain);
            TN_LEN_PUser = m_PrifixName + ".f" + getLayerID(m_Vn_PipeUser);
            TN_LEN_PSource = m_PrifixName + ".f" + getLayerID(m_Vn_PipeSource);

            string strSelInvest = string.Empty;
            string strBuildDateCondition = string.Empty;
            string strBuilDateTitle = string.Empty;
            FrmSelInvest frmInvest = new FrmSelInvest();
            frmInvest.Init(m_lstInvest);
            if (frmInvest.ShowDialog() == DialogResult.OK)
            {

                strSelInvest = frmInvest.cboInvestStyle.Text;

                //20150206 固定资产不统计录入时间为19000101或者为空的的记录
                //strBuildDateCondition = frmInvest.BuildDtComplex;                  
                strBuildDateCondition = frmInvest.BuildDtComplex + " and WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')";

                strBuilDateTitle = frmInvest.BuildDataTitle;
                frmInvest.Close();
            }
            if (strSelInvest.Trim() == "")
            {
                return;
            }
            string strTemp = string.Empty, strFields = string.Empty, strSQL1 = string.Empty, strSQL2 = string.Empty, strSQL3 = string.Empty, strSQL = string.Empty;
            this.Cursor = Cursors.WaitCursor;
            if (cboWaterType.Text == mc_strWaterType_All)
            {
                //MAIN
                strSQL1 = "select a.MATERIAL as 材质,a.DIAMETER as 管径,sum(L.LEN) as 长度_米,sum(a.DRAWPRICE) as 原值," +
                    "a.ADDR as 地址,a.PIPEFILE as 档号,a.WORKNO as 工号,a.USERNAME as 户名,a.ASSEMBLER as 施工单位," +
                    "( select count(b.OBJECTID) from " + m_PrifixName + "." + m_Vn_Hydrant + " b " +
                    "where trim(b.PIPEFILE)=trim(a.PIPEFILE) and trim(b.MATERIAL)=trim(a.MATERIAL) and trim(b.DIAMETER)=trim(a.DIAMETER) " +
                    ") as 消火栓" +
                    " from " + m_PrifixName + "." + m_Vn_PipeMain + " a, " + TN_LEN_PMain + " L " +
                    " where A.Shape=L.FID " +
                    " and trim(" + mc_Fn_WorkChar + ")='" + strSelInvest + "' and " + mc_Fn_Drawprice + "<>0.0 and not " + mc_Fn_Drawprice + " is null " +
                    " and " + strBuildDateCondition +
                    //" and a.STATUS is null or (trim(a.STATUS)<>" + mc_StatusDisusedPipe + " and trim(a.STATUS)<>" + mc_StatusTempPipe + ") " +
                    //" and trim(a.STATUS)=" + mc_StatusCompletionPipe +
                    //2015-03-03 不统计数据类型为刨找的                    
                    //" and (A.DATATYPE is NULL or trim(A.DATATYPE)<>" + mc_strDataTypeDigFound + ")" +

                    " group by a.MATERIAL,a.DIAMETER,a.ADDR,a.PIPEFILE,a.WORKNO,a.USERNAME,a.ASSEMBLER" +
                    " ";
                //" order by a.MATERIAL,a.DIAMETER";
                //USER
                strSQL2 = "select a.MATERIAL as 材质,a.DIAMETER as 管径,sum(L.LEN) as 长度_米,sum(a.DRAWPRICE) as 原值," +
                    "a.ADDR as 地址,a.PIPEFILE as 档号,a.WORKNO as 工号,a.USERNAME as 户名,a.ASSEMBLER as 施工单位," +
                    "( select count(b.OBJECTID) from " + m_PrifixName + "." + m_Vn_Hydrant + " b " +
                    "where trim(b.PIPEFILE)=trim(a.PIPEFILE) and trim(b.MATERIAL)=trim(a.MATERIAL) and trim(b.DIAMETER)=trim(a.DIAMETER) " +
                    ") as 消火栓" +
                    " from " + m_PrifixName + "." + m_Vn_PipeUser + " a, " + TN_LEN_PUser + " L " +
                    " where A.Shape=L.FID " +
                    " and trim(" + mc_Fn_WorkChar + ")='" + strSelInvest + "' and " + mc_Fn_Drawprice + "<>0.0 and not " + mc_Fn_Drawprice + " is null " +
                    " and " + strBuildDateCondition +
                    //" and trim(a.STATUS)=" + mc_StatusCompletionPipe +
                    //2015-03-03 不统计数据类型为刨找的                    
                    //" and (A.DATATYPE is NULL or trim(A.DATATYPE)<>" + mc_strDataTypeDigFound + ")" +

                    " group by a.MATERIAL,a.DIAMETER,a.ADDR,a.PIPEFILE,a.WORKNO,a.USERNAME,a.ASSEMBLER" +
                    " ";
                //" order by a.MATERIAL,a.DIAMETER";
                //SOURCE
                strSQL3 = "select a.MATERIAL as 材质,a.DIAMETER as 管径,sum(L.LEN) as 长度_米,sum(a.DRAWPRICE) as 原值," +
                    "a.ADDR as 地址,a.PIPEFILE as 档号,a.WORKNO as 工号,a.USERNAME as 户名,a.ASSEMBLER as 施工单位," +
                    "( select count(b.OBJECTID) from " + m_PrifixName + "." + m_Vn_Hydrant + " b " +
                    "where trim(b.PIPEFILE)=trim(a.PIPEFILE) and trim(b.MATERIAL)=trim(a.MATERIAL) and trim(b.DIAMETER)=trim(a.DIAMETER) " +
                    ") as 消火栓" +
                    " from " + m_PrifixName + "." + m_Vn_PipeSource + " a, " + TN_LEN_PSource + " L " +
                    " where A.Shape=L.FID " +
                    " and trim(" + mc_Fn_WorkChar + ")='" + strSelInvest + "' and " + mc_Fn_Drawprice + "<>0.0 and not " + mc_Fn_Drawprice + " is null " +
                    " and " + strBuildDateCondition +
                    // " and trim(a.STATUS)=" + mc_StatusCompletionPipe +
                    //2015-03-03 不统计数据类型为刨找的                    
                    //" and (A.DATATYPE is NULL or trim(A.DATATYPE)<>" + mc_strDataTypeDigFound + ")" +
                    " group by a.MATERIAL,a.DIAMETER,a.ADDR,a.PIPEFILE,a.WORKNO,a.USERNAME,a.ASSEMBLER" +
                    // " order by a.MATERIAL,a.DIAMETER";
                   " ";
            }
            else
            {
                //main
                strSQL1 = "select a.MATERIAL as 材质,a.DIAMETER as 管径,sum(L.LEN) as 长度_米,sum(a.DRAWPRICE) as 原值," +
                    "a.ADDR as 地址,a.PIPEFILE as 档号,a.WORKNO as 工号,a.USERNAME as 户名,a.ASSEMBLER as 施工单位," +
                    "( select count(b.OBJECTID) from " + m_PrifixName + "." + m_Vn_Hydrant + " b " +
                    "where trim(b.PIPEFILE)=trim(a.PIPEFILE) and trim(b.MATERIAL)=trim(a.MATERIAL) and trim(b.DIAMETER)=trim(a.DIAMETER) " +
                    ") as 消火栓" +
                    " from " + m_PrifixName + "." + m_Vn_PipeMain + " a, " + TN_LEN_PMain + " L " +
                    " where A.Shape=L.FID " +
                    " and trim(" + mc_Fn_WorkChar + ")='" + strSelInvest + "' and " + mc_Fn_Drawprice + "<>0.0 and not " + mc_Fn_Drawprice + " is null " +
                    " and " + strBuildDateCondition +
                    //" and a.STATUS is null or (trim(a.STATUS)<>" + mc_StatusDisusedPipe + " and trim(a.STATUS)<>" + mc_StatusTempPipe + ") and a.WATERTYPE='"+cboWaterType.Text +"'"+
                    //" and trim(a.STATUS)=" + mc_StatusCompletionPipe + 
                    //2015-03-03 不统计数据类型为刨找的                    
                    //" and (A.DATATYPE is NULL or trim(A.DATATYPE)<>" + mc_strDataTypeDigFound + ")" +
                    " and a.WATERTYPE='" + cboWaterType.Text + "' " +
                    " group by a.MATERIAL,a.DIAMETER,a.ADDR,a.PIPEFILE,a.WORKNO,a.USERNAME,a.ASSEMBLER" +
                    //" order by a.MATERIAL,a.DIAMETER";
                    " ";
                //USER
                strSQL2 = "select a.MATERIAL as 材质,a.DIAMETER as 管径,sum(L.LEN) as 长度_米,sum(a.DRAWPRICE) as 原值," +
                    "a.ADDR as 地址,a.PIPEFILE as 档号,a.WORKNO as 工号,a.USERNAME as 户名,a.ASSEMBLER as 施工单位," +
                    "( select count(b.OBJECTID) from " + m_PrifixName + "." + m_Vn_Hydrant + " b " +
                    "where trim(b.PIPEFILE)=trim(a.PIPEFILE) and trim(b.MATERIAL)=trim(a.MATERIAL) and trim(b.DIAMETER)=trim(a.DIAMETER) " +
                    ") as 消火栓" +
                    " from " + m_PrifixName + "." + m_Vn_PipeUser + " a, " + TN_LEN_PUser + " L " +
                    " where A.Shape=L.FID " +
                    " and trim(" + mc_Fn_WorkChar + ")='" + strSelInvest + "' and " + mc_Fn_Drawprice + "<>0.0 and not " + mc_Fn_Drawprice + " is null " +
                    " and " + strBuildDateCondition +
                    //" and trim(a.STATUS)=" + mc_StatusCompletionPipe +
                    //2015-03-03 不统计数据类型为刨找的                    
                    //" and (A.DATATYPE is NULL or trim(A.DATATYPE)<>" + mc_strDataTypeDigFound + ")" +
                    " and a.WATERTYPE='" + cboWaterType.Text + "' " +
                    " group by a.MATERIAL,a.DIAMETER,a.ADDR,a.PIPEFILE,a.WORKNO,a.USERNAME,a.ASSEMBLER" +
                    //" order by a.MATERIAL,a.DIAMETER";
                    " ";
                //SOURCE
                strSQL3 = "select a.MATERIAL as 材质,a.DIAMETER as 管径,sum(L.LEN) as 长度_米,sum(a.DRAWPRICE) as 原值," +
                    "a.ADDR as 地址,a.PIPEFILE as 档号,a.WORKNO as 工号,a.USERNAME as 户名,a.ASSEMBLER as 施工单位," +
                    "( select count(b.OBJECTID) from " + m_PrifixName + "." + m_Vn_Hydrant + " b " +
                    "where trim(b.PIPEFILE)=trim(a.PIPEFILE) and trim(b.MATERIAL)=trim(a.MATERIAL) and trim(b.DIAMETER)=trim(a.DIAMETER) " +
                    ") as 消火栓" +
                    " from " + m_PrifixName + "." + m_Vn_PipeSource + " a, " + TN_LEN_PSource + " L " +
                    " where A.Shape=L.FID " +
                    " and trim(" + mc_Fn_WorkChar + ")='" + strSelInvest + "' and " + mc_Fn_Drawprice + "<>0.0 and not " + mc_Fn_Drawprice + " is null " +
                    " and " + strBuildDateCondition +
                    //" and trim(a.STATUS)=" + mc_StatusCompletionPipe +
                    //2015-03-03 不统计数据类型为刨找的                    
                    //" and (A.DATATYPE is NULL or trim(A.DATATYPE)<>" + mc_strDataTypeDigFound + ")" +
                    " and a.WATERTYPE='" + cboWaterType.Text + "' " +
                    " group by a.MATERIAL,a.DIAMETER,a.ADDR,a.PIPEFILE,a.WORKNO,a.USERNAME,a.ASSEMBLER" +
                    //" order by a.MATERIAL,a.DIAMETER";
                    " ";
            }

            //strSQL = strSQL1;            
            //dDataTableM = Get_QuerySheet(strSQL);
            //strSQL = strSQL2;
            //dDataTableU = Get_QuerySheet(strSQL);
            //strSQL = strSQL3;
            //dDataTableS = Get_QuerySheet(strSQL);            

            strSQL = "select 材质,管径,round(长度_米,3) 长度_米,round(原值,2) 原值,地址,档号,工号,户名,施工单位,消火栓 from( " +
                strSQL1 + " union " +
                strSQL2 + " union " +
                strSQL3 +
                ") group by  材质,管径,长度_米,原值,地址,档号,工号,户名,施工单位,消火栓 " +
                " order by 档号,材质,管径";//按要求统计结果顺序调整为档号、材质、管径；原为档号，管径、材质2015-03-04；长度改为3,2015-3-13；

            dDataTableTotal = Get_QuerySheet(strSQL);

            //if (dDataTableM.Rows.Count < 1 && dDataTableU.Rows.Count < 1 && dDataTableS.Rows.Count < 1)
            if (dDataTableTotal.Rows.Count < 1)
            {
                MessageBox.Show("所选统计类型记录为空，导出终止！", "提示！");
                this.Cursor = Cursors.Default;
                return;
            }
            ////赋值表结构,先取最大行记录的
            //string falg = string.Empty;           
            //dDataTableTotal = (dDataTableM.Rows.Count > dDataTableU.Rows.Count ? 
            //    (dDataTableM.Rows.Count > dDataTableS.Rows.Count ? dDataTableM.Copy(): dDataTableS.Copy()) : 
            //    (dDataTableU.Rows.Count > dDataTableS.Rows.Count ? dDataTableU.Copy(): dDataTableS.Copy()));

            //falg = (dDataTableM.Rows.Count > dDataTableU.Rows.Count ?
            //   (dDataTableM.Rows.Count > dDataTableS.Rows.Count ? "M" : "S") :
            //   (dDataTableU.Rows.Count > dDataTableS.Rows.Count ? "U" : "S"));
            //// 增加另外两个表记录
            //if (falg =="M")
            //{ 
            //    foreach (DataRow dr in dDataTableU.Rows)
            //    {
            //        dDataTableTotal.Rows.Add(dr.ItemArray);
            //    }
            //    foreach (DataRow dr in dDataTableS.Rows)
            //    {
            //        dDataTableTotal.Rows.Add(dr.ItemArray);
            //    }
            //}
            //else if (falg == "U")
            //{                
            //    foreach (DataRow dr in dDataTableM.Rows)
            //    {
            //        dDataTableTotal.Rows.Add(dr.ItemArray);
            //    }
            //    foreach (DataRow dr in dDataTableS.Rows)
            //    {
            //        dDataTableTotal.Rows.Add(dr.ItemArray);
            //    }
            //}
            //else
            //{                
            //    foreach (DataRow dr in dDataTableU.Rows)
            //    {
            //        dDataTableTotal.Rows.Add(dr.ItemArray);
            //    }
            //    foreach (DataRow dr in dDataTableM.Rows)
            //    {
            //        dDataTableTotal.Rows.Add(dr.ItemArray);
            //    }
            //}

            DateTime now = DateTime.Now;
            SaveFileDialog objSave = new SaveFileDialog();
            objSave.Filter = "电子表格(*.xls)|*.xls|所有文件(*.*)|*.*";
            objSave.FilterIndex = 0;
            objSave.RestoreDirectory = true;
            objSave.AddExtension = true;
            objSave.CreatePrompt = true;
            objSave.Title = "导出" + mc_strNameReport_PipeInvestAddList;
            objSave.FileName = mc_strNameReport_PipeInvestAddList + now.Year.ToString().PadLeft(2, '0') + now.Month.ToString().PadLeft(2, '0') + now.Day.ToString().PadLeft(2, '0')
                + now.Hour.ToString().PadLeft(2, '0') + now.Minute.ToString().PadLeft(2, '0') + now.Second.ToString().PadLeft(2, '0') + ".xls";
            try
            {
                if (objSave.ShowDialog() == DialogResult.OK)
                {
                    double dbl_YZJESum = 0;
                    double dbl_ChangDSum = 0;
                    double dbl_YuanZSum = 0;
                    StringWriter sw = new StringWriter();
                    sw.WriteLine("{0}(水质类型：{1}){2}", mc_strNameofJJWaterltd, cboWaterType.Text, mc_strNameReport_PipeInvestAddList);
                    sw.WriteLine("投资方式：{0}", strSelInvest);
                    sw.WriteLine("统计区间：{0}", strBuilDateTitle);
                    sw.WriteLine("");

                    string[] strColumnNames = new string[dDataTableTotal.Columns.Count];
                    object[] objLastRowofTable = new object[dDataTableTotal.Columns.Count];


                    string strCompMaterial = string.Empty;
                    int intCompDiameter = 0;
                    string strComPipefile = string.Empty;
                    string strComWorkNo = string.Empty;

                    for (int i = 0; i < dDataTableTotal.Rows.Count; i++)
                    {

                        string dcHydrantName = string.Empty;

                        for (int j = 0; j < dDataTableTotal.Columns.Count; j++)
                        {
                            DataColumn dcols = dDataTableTotal.Columns[j];
                            if (i == 0 && j == 0)
                            {
                                int k = 0;
                                foreach (DataColumn dc in dDataTableTotal.Columns)
                                {
                                    sw.Write(string.Format("{0}\t", dc.ColumnName));
                                    strColumnNames[k] = dc.ColumnName;
                                    k++;
                                }
                                sw.Write("\r\n");
                            }

                            dcHydrantName = dDataTableTotal.Columns[j].ColumnName;
                            if (i == 0)
                            {
                                sw.Write(dDataTableTotal.Rows[i][j].ToString().Trim() + "\t");
                            }
                            //存在重复的情况,统计时消火栓仅统计一次:
                            //材质，管径，档号，工号；和上一次记录比较
                            if (dcHydrantName == "消火栓" &&
                                strCompMaterial == dDataTableTotal.Rows[i][0].ToString().Trim() &&
                                intCompDiameter == Convert.ToInt32(dDataTableTotal.Rows[i][1].ToString().Trim()) &&
                                strComPipefile == dDataTableTotal.Rows[i][5].ToString().Trim() &&
                                 strComWorkNo == dDataTableTotal.Rows[i][6].ToString().Trim() &&
                                i > 0)
                            {
                                sw.Write("0" + "\t");
                            }
                            else if (i > 0)
                            {
                                sw.Write(dDataTableTotal.Rows[i][j].ToString().Trim() + "\t");

                            }
                        }

                        strCompMaterial = dDataTableTotal.Rows[i][0].ToString().Trim();
                        intCompDiameter = Convert.ToInt32(dDataTableTotal.Rows[i][1].ToString().Trim());
                        strComPipefile = dDataTableTotal.Rows[i][5].ToString().Trim();
                        strComWorkNo = dDataTableTotal.Rows[i][6].ToString().Trim();


                        //累加月折旧额
                        //if (dDataTableTotal.Rows[i][dDataTableTotal.Columns.Count - 2] != null && !Convert.IsDBNull(dDataTableTotal.Rows[i][dDataTableTotal.Columns.Count - 2]) && dDataTableTotal.Rows[i][dDataTableTotal.Columns.Count - 2].ToString().Trim() != "")
                        //    dbl_YZJESum = Convert.ToDouble(dDataTableTotal.Rows[i][dDataTableTotal.Columns.Count - 2]) + dbl_YZJESum;
                        //累加原值
                        if (dDataTableTotal.Rows[i][dDataTableTotal.Columns.Count - 7] != null && !Convert.IsDBNull(dDataTableTotal.Rows[i][dDataTableTotal.Columns.Count - 7]) && dDataTableTotal.Rows[i][dDataTableTotal.Columns.Count - 7].ToString().Trim() != "")
                            dbl_YuanZSum = Convert.ToDouble(dDataTableTotal.Rows[i][dDataTableTotal.Columns.Count - 7]) + dbl_YuanZSum;
                        //累加长度
                        if (dDataTableTotal.Rows[i][dDataTableTotal.Columns.Count - 8] != null && !Convert.IsDBNull(dDataTableTotal.Rows[i][dDataTableTotal.Columns.Count - 8]) && dDataTableTotal.Rows[i][dDataTableTotal.Columns.Count - 8].ToString().Trim() != "")
                            dbl_ChangDSum = Convert.ToDouble(dDataTableTotal.Rows[i][dDataTableTotal.Columns.Count - 8]) + dbl_ChangDSum;

                        sw.Write("\r\n");
                    }
                    //sw.WriteLine("月折旧额：{0} 元", Math.Round(dbl_YZJESum, 2));
                    sw.WriteLine("\r\n");
                    sw.WriteLine("长度合计：{0} 米 \t\t 原值合计：{1} 元", Math.Round(dbl_ChangDSum, 3), Math.Round(dbl_YuanZSum, 2));//长度2改为3，2015-03-13；zhou


                    byte[] s = System.Text.Encoding.GetEncoding("gb2312").GetBytes(sw.ToString());
                    FileStream fs = new FileStream(objSave.FileName, FileMode.Create, FileAccess.Write);
                    fs.Write(s, 0, s.Length);

                    fs.Close();
                    this.Cursor = Cursors.Default;
                    if (MessageBox.Show("导出完毕,点击【确定】打开！", "提示", MessageBoxButtons.OK) == DialogResult.OK)
                    {
                        string strMacroName = "PERSONAL.XLSB!固定资产增加明细表";
                        OpenFolderAndSelectFile(objSave.FileName, m_strExcelTemplate, strMacroName);
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

        }
        //新安消火栓统计
        private void HydrantMonthReport2Excel()
        {

            string strSQL = string.Empty;

            DataTable dDataTableA = new DataTable();

            Boolean blnTONGJI = false;

            DateTime dtBegDate = Convert.ToDateTime(dtpBegStateDate.Text);
            DateTime dtEndDate = Convert.ToDateTime(dtpEndStateDate.Text);
            //修改时间格式：2015年2月2日  
            string strBegDate = dtBegDate.ToString("yyyyMMdd");
            string strEndDate = dtEndDate.ToString("yyyyMMdd");

            string strBegDateInTitle = dtBegDate.ToString("yyyy") + "年" + dtBegDate.ToString("MM") + "月";
            string strEndDateInTitle = dtEndDate.ToString("yyyy") + "年" + dtEndDate.ToString("MM") + "月";

            string strWhere = GetWhere(mc_strValveLayerName, ref blnTONGJI);

            if (blnTONGJI)
            {
                this.Cursor = Cursors.WaitCursor;
                m_lstDBResults = new List<string>();
                if (cboWaterType.Text == mc_strWaterType_All)
                {
                    //状态改为只统计竣工；
                    strSQL = "select addr 地址,username 单位名称,userno 户号,diameter 口径,count(*) 消火栓个数 from " + m_CurParameter.GWUserName +
                        ".hydrant " + " where " +
                        //"checkedate >= to_date('" + strBegDate + "000000','yyyyMMddhh24:mi:ss') and checkedate<= to_date('" + strEndDate + "235959','yyyyMMddhh24:mi:ss') " +
                        NewDtComplex +
                        strWhere +
                        //2015-03-13 周:涉及到时间的统计均不统计1900.有问题时他们都会把录入日期改为1900；日期为空不统计
                                " and WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')" +
                        //" and (STATUS IS NULL or (STATUS <> " + mc_StatusTempPipe + "and STATUS<>" + mc_StatusDisusedPipe + ")) " + 
                        //" and STATUS = "+mc_StatusCompletionPipe +
                        " group by addr,username,userno,diameter";
                    m_strTitleOfExportExcelFileName = mc_strNameReport_NewHydrant;
                }
                else
                {
                    strSQL = "select addr 地址,username 单位名称,userno 户号,diameter 口径,count(*) 消火栓个数 from " + m_CurParameter.GWUserName +
                        ".hydrant " + " where " +
                        //" checkedate >= to_date('" + strBegDate + "000000','yyyyMMddhh24:mi:ss') and checkedate<= to_date('" + strEndDate + "235959','yyyyMMddhh24:mi:ss') " + 
                        NewDtComplex +
                        strWhere +
                        //2015-03-13 周:涉及到时间的统计均不统计1900.有问题时他们都会把录入日期改为1900；日期为空不统计
                                " and WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')" +
                        //" and (STATUS IS NULL or (STATUS <> " + mc_StatusTempPipe + "and STATUS<>" + mc_StatusDisusedPipe+" )) and WATERTYPE='" + cboWaterType.Text + "'"+
                        //" and STATUS=" + mc_StatusCompletionPipe + 
                        " and WATERTYPE='" + cboWaterType.Text + "' " +
                        " group by addr,username,userno,diameter";
                    m_strTitleOfExportExcelFileName = mc_strNameReport_NewHydrant;
                }

                try
                {
                    dDataTableA = Get_QuerySheet(strSQL);
                    if (dDataTableA.Rows.Count < 1)
                    {
                        MessageBox.Show("所选统计类型记录为空,导出终止！", "提示！");
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误");
                    this.Cursor = Cursors.Default;
                    m_lstDBResults.Clear();
                    return;
                }

            }

            DateTime now = DateTime.Now;
            SaveFileDialog objSave = new SaveFileDialog();
            objSave.Filter = "电子表格(*.xls)|*.xls|所有文件(*.*)|*.*";
            objSave.FilterIndex = 0;
            objSave.RestoreDirectory = true;
            objSave.AddExtension = true;
            objSave.CreatePrompt = true;
            objSave.Title = "导出" + m_strTitleOfExportExcelFileName;
            objSave.FileName = m_strTitleOfExportExcelFileName + now.Year.ToString().PadLeft(2, '0') + now.Month.ToString().PadLeft(2, '0') + now.Day.ToString().PadLeft(2, '0')
                + now.Hour.ToString().PadLeft(2, '0') + now.Minute.ToString().PadLeft(2, '0') + now.Second.ToString().PadLeft(2, '0') + ".xls";
            try
            {
                if (objSave.ShowDialog() == DialogResult.OK)
                {
                    double dblSum = 0;
                    StringWriter sw = new StringWriter();
                    sw.WriteLine("{0}至{1}{2}(水质类型:{3})", strBegDateInTitle, strEndDateInTitle, m_strTitleOfExportExcelFileName, cboWaterType.Text);
                    sw.WriteLine("");

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
                            sw.Write(dDataTableA.Rows[i][j].ToString().Trim() + "\t");
                        }
                        dblSum = Convert.ToDouble(dDataTableA.Rows[i][dDataTableA.Columns.Count - 1]) + dblSum;
                        sw.Write("\r\n");
                    }
                    sw.WriteLine("总计：{0} 个", dblSum);
                    byte[] s = System.Text.Encoding.GetEncoding("gb2312").GetBytes(sw.ToString());
                    FileStream fs = new FileStream(objSave.FileName, FileMode.Create, FileAccess.Write);
                    fs.Write(s, 0, s.Length);
                    fs.Close();
                    if (MessageBox.Show("导出完毕,点击【确定】打开！", "提示", MessageBoxButtons.OK) == DialogResult.OK)
                    {
                        string strMacroName = "PERSONAL.XLSB!新安消火栓个数统计月报";
                        OpenFolderAndSelectFile(objSave.FileName, m_strExcelTemplate, strMacroName);
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
                m_pFeatureLayer = null;
                m_dicColumnCodeDesc.Clear();
                m_dicRowCodeDesc.Clear();
                m_lstDBResults.Clear();
            }

        }

        private void ValveStateReport2Excel()
        {
            string strSQL = string.Empty;

            DataTable dDataTableA = new DataTable();
            DataTable dDataTableB = new DataTable();

            OleDbDataReader adoReader = null;
            Boolean blnTONGJI = false;


            string cstrVavleCount_en = "VCount";
            string cstrValveCount_cn = "总数(座)";

            try
            {
                IFeatureLayer pFeatureLayer = m_objMap.GetLayerByName(mc_strValveLayerName).FeatureLayer;
                IDataset pSelectedLayerDataset = (IDataset)pFeatureLayer;
                m_dicColumnCodeDesc = GetSubtypes(pFeatureLayer.FeatureClass, mc_FieldNameMaterial);
                m_dicRowCodeDesc = GetSubtypes(pFeatureLayer.FeatureClass, mc_FieldNameDiameter);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
                return;
            }

            string strWhere = GetWhere(mc_strValveLayerName, ref blnTONGJI);

            if (blnTONGJI)
            {
                this.Cursor = Cursors.WaitCursor;
                if (cboWaterType.Text == mc_strWaterType_All)
                {
                    //STATUS只筛选竣工 20150119
                    //SZTYPE 修改为 WATERTYPE 20141421
                    //strSQL = "select " + mc_FieldNameDiameter + "," + mc_FieldNameVstate + ", count(*) Vcount from " + m_CurParameter.GWUserName + ".valve where trim(DIAMETER) is not null " + strWhere + " and (STATUS IS NULL or (STATUS IS NOT NULL and trim(STATUS) <> " + mc_StatusTempPipe + ")) group by " + mc_FieldNameDiameter + "," + mc_FieldNameVstate + " order by " + mc_FieldNameDiameter;
                    strSQL = "select " + mc_FieldNameDiameter + "," + mc_FieldNameVstate + ", count(*) Vcount from " + m_CurParameter.GWUserName + ".valve " +

                        //" where STATUS IS NULL or (STATUS <> " + mc_StatusTempPipe +"and STATUS<> "+mc_StatusDisusedPipe+") "+
                        //" STATUS=" + mc_StatusCompletionPipe +

                        //2015-03-13 周:涉及到时间的统计均不统计1900.有问题时他们都会把录入日期改为1900；日期为空不统计
                                " where WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')" +

                        " group by " + mc_FieldNameDiameter + "," + mc_FieldNameVstate + " order by " + mc_FieldNameDiameter;
                    m_strTitleOfExportExcelFileName = mc_strNameReport_Valve;
                }
                else
                {
                    //strSQL = "select " + mc_FieldNameDiameter + "," + mc_FieldNameVstate + ", count(*) Vcount from " + m_CurParameter.GWUserName + ".valve where trim(DIAMETER) is not null " + strWhere + " and (STATUS IS NULL or (STATUS IS NOT NULL and trim(STATUS) <> " + mc_StatusTempPipe + ")) and WATERTYPE ='" + cboWaterType.Text + "' group by " + mc_FieldNameDiameter + "," + mc_FieldNameVstate + " order by " + mc_FieldNameDiameter;
                    strSQL = "select " + mc_FieldNameDiameter + "," + mc_FieldNameVstate + ", count(*) Vcount from " + m_CurParameter.GWUserName + ".valve" +
                        " where" +
                        //" STATUS IS NULL or (STATUS <> " + mc_StatusTempPipe + " and STATUS <> "+mc_StatusDisusedPipe+"  and WATERTYPE ='" + cboWaterType.Text + "')"+
                        //" STATUS="+mc_StatusCompletionPipe+
                        " WATERTYPE='" + cboWaterType.Text + "' " +
                        //2015-03-13 周:涉及到时间的统计均不统计1900.有问题时他们都会把录入日期改为1900；日期为空不统计
                                " and WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')" +
                        " group by " + mc_FieldNameDiameter + "," + mc_FieldNameVstate + " order by " + mc_FieldNameDiameter;
                    m_strTitleOfExportExcelFileName = mc_strNameReport_Valve;
                }

                try
                {
                    if (m_objDBCON == null)
                        m_objDBCON = CDBCon.GetInstance();
                    //Get_sqlDBReader(strSQL, ref adoReader);
                    m_objDBCON.ExecuteSQLReturn(strSQL, ref adoReader);
                    //if (adoReader != null)
                    if (adoReader.HasRows)
                    {
                        string strDBresult = string.Empty;
                        string strDiameter = string.Empty;
                        string strVstate = string.Empty;
                        string strNewRecord = string.Empty;
                        string strSameRecord = string.Empty;
                        double dblTempSum = 0;

                        while (adoReader.Read())
                        {
                            strDBresult = "";
                            dblTempSum = 0;
                            strDiameter = adoReader[mc_FieldNameDiameter].ToString().Trim();
                            strVstate = adoReader[mc_FieldNameVstate].ToString().Trim();

                            if (adoReader[mc_FieldNameDiameter].ToString().Trim() == "")
                            {
                                strDiameter = mc_strUnknownDiameter;
                                //20150206：不统计未知材质和管径
                                continue;
                            }
                            if (adoReader[mc_FieldNameVstate].ToString().Trim() == "")
                            {
                                strVstate = mc_strUnknownValveType;
                                //20150206：不统计未知材质和管径
                                continue;
                            }
                            if (!m_lstRow.Contains(strDiameter))
                            {
                                m_lstRow.Add(strDiameter);
                            }
                            if (!m_lstColumns.Contains(strVstate))
                            {
                                m_lstColumns.Add(strVstate);
                            }
                            //存在数据不规范的情况，如重复记录，多n个空格，为“”，需要再判断，计算；
                            foreach (string r in m_lstDBResults)
                            {
                                string[] s = r.Split(',');
                                if ((strDiameter + "," + strVstate) == (s[0] + "," + s[1]))
                                {
                                    strSameRecord = r;
                                    dblTempSum = Convert.ToDouble(s[2]) + Convert.ToDouble(adoReader[cstrVavleCount_en]);
                                    strNewRecord = s[0] + "," + s[1] + "," + dblTempSum.ToString();
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
                                strDBresult = strDiameter + "," + strVstate + "," + adoReader[cstrVavleCount_en].ToString();
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
                                dc = dDataTableA.Columns.Add(cstrValveCount_cn, Type.GetType("System.Decimal"));
                            }
                        }
                        m_lstColumns.Add(cstrValveCount_cn);
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
                                        dr[k] = Math.Round(Convert.ToDecimal(s[2]), 3);
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
                        MessageBox.Show("所选统计类型记录为空,导出终止！", "提示！");
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误");
                    m_lstDBResults.Clear();
                    return;
                }

            }

            DateTime now = DateTime.Now;
            SaveFileDialog objSave = new SaveFileDialog();
            objSave.Filter = "电子表格(*.xls)|*.xls|所有文件(*.*)|*.*";
            objSave.FilterIndex = 0;
            objSave.RestoreDirectory = true;
            objSave.AddExtension = true;
            objSave.CreatePrompt = true;
            objSave.Title = "导出" + mc_strNameReport_Valve;
            objSave.FileName = mc_strNameReport_Valve + now.Year.ToString().PadLeft(2, '0') + now.Month.ToString().PadLeft(2, '0') + now.Day.ToString().PadLeft(2, '0')
                + now.Hour.ToString().PadLeft(2, '0') + now.Minute.ToString().PadLeft(2, '0') + now.Second.ToString().PadLeft(2, '0') + ".xls";
            try
            {
                if (objSave.ShowDialog() == DialogResult.OK)
                {
                    StringWriter sw = new StringWriter();
                    sw.WriteLine("\t {0}(水质类型:{1})", m_strTitleOfExportExcelFileName, cboWaterType.Text);
                    sw.WriteLine("");
                    sw.WriteLine("填报单位：\t 填报日期:\t 单位：座");
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

                    byte[] s = System.Text.Encoding.GetEncoding("gb2312").GetBytes(sw.ToString());
                    FileStream fs = new FileStream(objSave.FileName, FileMode.Create, FileAccess.Write);
                    fs.Write(s, 0, s.Length);
                    fs.Close();
                    if (MessageBox.Show("导出完毕,点击【确定】打开！", "提示", MessageBoxButtons.OK) == DialogResult.OK)
                    {
                        string strMacroName = "PERSONAL.XLSB!配水管线闸门统计";
                        OpenFolderAndSelectFile(objSave.FileName, m_strExcelTemplate, strMacroName);
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
                m_pFeatureLayer = null;
                m_dicColumnCodeDesc.Clear();
                m_dicRowCodeDesc.Clear();
                m_lstDBResults.Clear();
            }
        }

        /// <summary>
        /// 导出新增管线统计月报到电子表格
        /// </summary>
        /// <param name="type">1、新增管线统计月报；2、新增配水管线统计月报；3、新增输水管线长度统计月报</param>
        private void NewLineLenMonthReport2Excel(int intType)
        {
            string strSQL = string.Empty, strPipeSectionMain = string.Empty, strPipeSectionUser = string.Empty, strPipeSectionSource = string.Empty;
            string strBegDate = string.Empty;
            string strEndDate = string.Empty;

            Boolean blnTONGJI1 = false;
            Boolean blnTONGJI2 = false;
            Boolean blnTONGJI3 = false;

            DataTable dDataTableA = new DataTable();
            DataTable dDataTableB = new DataTable();

            OleDbDataReader adoReader = null;

            if (cboWaterType.Text == mc_strWaterType_All)
            {
                //strPipeSectionMain = string.Format("(select * from jjwater.pipesectionmain a,jjwater.f{0} b where a.shape=b.fid)",
                //getLayerID(m_objMap.GetLayerByName(mc_strPipesectionMainName).LayerTableName));
                //strPipeSectionUser = string.Format("(select * from jjwater.pipesectionuser a,jjwater.f{0} b where a.shape=b.fid)",
                //    getLayerID(m_objMap.GetLayerByName(mc_strPipesectionUserName).LayerTableName));
                //strPipeSectionSource = string.Format("(select * from jjwater.pipesectionsource a,jjwater.f{0} b where a.shape=b.fid)",
                //    getLayerID(m_objMap.GetLayerByName(mc_strPipesectionSourceName).LayerTableName));

                strPipeSectionMain = string.Format("(select * from jjwater.pipesectionmain a,jjwater.f{0} b where a.shape=b.fid)",
                getLayerID("PIPESECTIONMAIN"));
                strPipeSectionUser = string.Format("(select * from jjwater.pipesectionuser a,jjwater.f{0} b where a.shape=b.fid)",
                    getLayerID("PIPESECTIONUSER"));
                strPipeSectionSource = string.Format("(select * from jjwater.pipesectionsource a,jjwater.f{0} b where a.shape=b.fid)",
                    getLayerID("PIPESECTIONSOURCE"));

            }
            else
            {
                //strPipeSectionMain = string.Format("(select * from jjwater.pipesectionmain a,jjwater.f{0} b where a.shape=b.fid and a.watertype='{1}')",
                //getLayerID(m_objMap.GetLayerByName(mc_strPipesectionMainName).LayerTableName),cboWaterType.Text);
                //strPipeSectionUser = string.Format("(select * from jjwater.pipesectionuser a,jjwater.f{0} b where a.shape=b.fid and a.watertype='{1}')",
                //    getLayerID(m_objMap.GetLayerByName(mc_strPipesectionUserName).LayerTableName),cboWaterType.Text);
                //strPipeSectionSource = string.Format("(select * from jjwater.pipesectionsource a,jjwater.f{0} b where a.shape=b.fid and a.watertype='{1}')",
                //    getLayerID(m_objMap.GetLayerByName(mc_strPipesectionSourceName).LayerTableName), cboWaterType.Text);

                strPipeSectionMain = string.Format("(select * from jjwater.pipesectionmain a,jjwater.f{0} b where a.shape=b.fid and a.watertype='{1}')",
                getLayerID("PIPESECTIONMAIN"), cboWaterType.Text);
                strPipeSectionUser = string.Format("(select * from jjwater.pipesectionuser a,jjwater.f{0} b where a.shape=b.fid and a.watertype='{1}')",
                    getLayerID("PIPESECTIONUSER"), cboWaterType.Text);
                strPipeSectionSource = string.Format("(select * from jjwater.pipesectionsource a,jjwater.f{0} b where a.shape=b.fid and a.watertype='{1}')",
                    getLayerID("PIPESECTIONSOURCE"), cboWaterType.Text);

            }



            m_pFeatureLayer = m_objMap.GetLayerByName(mc_strPipesectionMainName).FeatureLayer;
            m_dicColumnCodeDesc = GetSubtypes(m_pFeatureLayer.FeatureClass, mc_FieldNameMaterial);
            m_dicRowCodeDesc = GetSubtypes(m_pFeatureLayer.FeatureClass, mc_FieldNameDiameter);

            DateTime dtBegDate = Convert.ToDateTime(dtpBegStateDate.Text);
            DateTime dtEndDate = Convert.ToDateTime(dtpEndStateDate.Text);

            strBegDate = dtBegDate.ToString("yyyyMMdd");//20140806 大小写有区别
            strEndDate = dtEndDate.ToString("yyyyMMdd");//20010101

            string strWherePipesectionMain = GetWhere(mc_strPipesectionMainName, ref blnTONGJI1);
            string strWherePipesectionUser = GetWhere(mc_strPipesectionUserName, ref blnTONGJI2);
            string strWherePipesectionSource = GetWhere(mc_strPipesectionSourceName, ref blnTONGJI3);

            if (strWherePipesectionMain.Trim() != "")
            {
                strWherePipesectionMain = " and " + strWherePipesectionMain;
            }
            if (strWherePipesectionUser.Trim() != "")
            {
                strWherePipesectionUser = " and " + strWherePipesectionUser;
            }
            if (strWherePipesectionSource.Trim() != "")
            {
                strWherePipesectionSource = " and " + strWherePipesectionSource;
            }

            if (blnTONGJI1 && blnTONGJI2 && blnTONGJI3)
            {
                #region 构造查询Oracle的strSQL语句
                switch (intType)
                {
                    case 1:
                        if (cboWaterType.Text == mc_strWaterType_All)
                        {
                            //所有水质类型
                            // " {0}>=to_date('{1} 00:00:00','YYYY-MM-DD hh24:mi:ss')  and {0}<=to_date('{2} 23:59:59','YYYY-MM-DD hh24:mi:ss')",
                            strSQL =
                                "select diameter,material,sum(cd) shape_length from ((select diameter,material,len cd from " +
                                strPipeSectionMain + " where " +
                                //"where checkedate >= to_date('" + strBegDate + "000000','yyyyMMddhh24:mi:ss') and checkedate<= to_date('" + strEndDate + "235959','yyyyMMddhh24:mi:ss') " +
                                NewDtComplex +
                                strWherePipesectionMain +
                                //2015-03-13 周:涉及到时间的统计均不统计1900.有问题时他们都会把录入日期改为1900；日期为空不统计
                                " and WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')" +
                                //" and STATUS IS NULL or (STATUS <> " + mc_StatusDisusedPipe + " and STATUS <> " + mc_StatusTempPipe + 
                                //" and STATUS ="+ mc_StatusCompletionPipe +
                                //")) union all " +
                                ") union all " +
                                "(select diameter,material,len cd from " +
                                strPipeSectionUser + " where " +
                                //"where checkedate >= to_date('" + strBegDate + "000000','yyyyMMddhh24:mi:ss') and checkedate<= to_date('" + strEndDate + "235959','yyyyMMddhh24:mi:ss') " + 
                                NewDtComplex +
                                strWherePipesectionUser +
                                //2015-03-13 周:涉及到时间的统计均不统计1900.有问题时他们都会把录入日期改为1900；日期为空不统计
                                " and WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')" +
                                //" and STATUS =" + mc_StatusCompletionPipe +
                                ") union all " +
                                "(select diameter,material,len cd from " +
                                strPipeSectionSource + " where " +
                                //"where checkedate >= to_date('" + strBegDate + "000000','yyyyMMddhh24:mi:ss') and checkedate<= to_date('" + strEndDate + "235959','yyyyMMddhh24:mi:ss') " + 
                                NewDtComplex +
                                strWherePipesectionSource +
                                //2015-03-13 周:涉及到时间的统计均不统计1900.有问题时他们都会把录入日期改为1900；日期为空不统计
                                " and WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')" +
                                //" and STATUS =" + mc_StatusCompletionPipe +
                                ")) group by diameter,material order by  " + mc_FieldNameDiameter;

                            m_strTitleOfExportExcelFileName = mc_strNameReport_NewMains;
                        }
                        else
                        {
                            //选中水质类型
                            strSQL =
                                "select diameter,material,sum(cd) shape_length from ((select diameter,material,len cd from " +
                                strPipeSectionMain + " where " +
                                //"where checkedate >= to_date('" + strBegDate + "000000','yyyyMMddhh24:mi:ss') and checkedate<= to_date('" + strEndDate + "235959','yyyyMMddhh24:mi:ss') " + 
                                NewDtComplex +
                                strWherePipesectionMain +
                                //2015-03-13 周:涉及到时间的统计均不统计1900.有问题时他们都会把录入日期改为1900；日期为空不统计
                                " and WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')" +
                                //" and STATUS IS NULL or (STATUS <> " + mc_StatusDisusedPipe + " and STATUS <> " + mc_StatusTempPipe + " and WATERTYPE='" + cboWaterType.Text + 
                                // " and STATUS ="+mc_StatusCompletionPipe +
                                " and WATERTYPE='" + cboWaterType.Text +
                                //"')) union all " +
                                "') union all " +
                                "(select diameter,material,len cd from " +
                                strPipeSectionUser + " where " +
                                //"where checkedate >= to_date('" + strBegDate + "000000','yyyyMMddhh24:mi:ss') and checkedate<= to_date('" + strEndDate + "235959','yyyyMMddhh24:mi:ss') " + 
                                NewDtComplex +
                                strWherePipesectionUser +
                                //2015-03-13 周:涉及到时间的统计均不统计1900.有问题时他们都会把录入日期改为1900；日期为空不统计
                                " and WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')" +
                                //" and STATUS =" + mc_StatusCompletionPipe + 
                                " and WATERTYPE='" + cboWaterType.Text +
                                "') union all " +
                                "(select diameter,material,len cd from " +
                                strPipeSectionSource + " where " +
                                //"where checkedate >= to_date('" + strBegDate + "000000','yyyyMMddhh24:mi:ss') and checkedate<= to_date('" + strEndDate + "235959','yyyyMMddhh24:mi:ss')  " + 
                                NewDtComplex +
                                strWherePipesectionSource +
                                //2015-03-13 周:涉及到时间的统计均不统计1900.有问题时他们都会把录入日期改为1900；日期为空不统计
                                " and WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')" +
                                //" and STATUS =" + mc_StatusCompletionPipe + 
                                " and WATERTYPE='" + cboWaterType.Text +
                                "')) group by diameter,material order by  " + mc_FieldNameDiameter;

                            m_strTitleOfExportExcelFileName = mc_strNameReport_NewMains;
                        }
                        break;
                    case 2:
                        if (cboWaterType.Text == mc_strWaterType_All)
                        {
                            //所有水质类型,配水管线
                            strSQL =
                                "select diameter,material,sum(cd) shape_length from ((select diameter,material,len cd from " +
                                strPipeSectionMain + " where " +
                                //"where checkedate >= to_date('" + strBegDate + "000000','yyyyMMddhh24:mi:ss') and checkedate<= to_date('" + strEndDate + "235959','yyyyMMddhh24:mi:ss') " + 
                                NewDtComplex +
                                strWherePipesectionMain +
                                //2015-03-13 周:涉及到时间的统计均不统计1900.有问题时他们都会把录入日期改为1900；日期为空不统计
                                " and WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')" +
                                //"and STATUS IS NULL or (STATUS <> " + mc_StatusDisusedPipe + " and STATUS <> " + mc_StatusTempPipe+
                                //" and STATUS ="+mc_StatusCompletionPipe+
                                //")) union all "+ 
                                 ") union all" +
                                 "(select diameter,material,len cd from " +
                                 strPipeSectionUser + " where " +
                                //"where checkedate >= to_date('" + strBegDate + "000000','yyyyMMddhh24:mi:ss') and checkedate<= to_date('" + strEndDate + "235959','yyyyMMddhh24:mi:ss') " + strWherePipesectionUser +
                                   NewDtComplex +
                                   strWherePipesectionUser +
                                //2015-03-13 周:涉及到时间的统计均不统计1900.有问题时他们都会把录入日期改为1900；日期为空不统计
                                " and WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')" +
                                //" and STATUS =" + mc_StatusCompletionPipe +
                                 "))group by diameter,material order by " + mc_FieldNameDiameter;
                            m_strTitleOfExportExcelFileName = mc_strNameReport_NewUser;
                        }
                        else
                        {
                            //选中水质类型，配水管线
                            strSQL =
                                "select diameter,material,sum(cd) shape_length from ((select diameter,material,len cd from " +
                                strPipeSectionMain + " where " +
                                //"where checkedate >= to_date('" + strBegDate + "000000','yyyyMMddhh24:mi:ss') and checkedate<= to_date('" + strEndDate + "235959','yyyyMMddhh24:mi:ss') " + strWherePipesectionMain +
                                NewDtComplex +
                                strWherePipesectionMain +
                                //2015-03-13 周:涉及到时间的统计均不统计1900.有问题时他们都会把录入日期改为1900；日期为空不统计
                                " and WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')" +
                                //"and STATUS IS NULL or (STATUS <> " + mc_StatusDisusedPipe + " and STATUS <> " + mc_StatusTempPipe + 
                                //" and STATUS ="+mc_StatusCompletionPipe+
                                //") and WATERTYPE='" + cboWaterType.Text + "') union all " +
                                 " and WATERTYPE='" + cboWaterType.Text + "') union all " +
                                 "(select diameter,material,len cd from " +
                                 strPipeSectionUser + " where " +
                                //"where checkedate >= to_date('" + strBegDate + "000000','yyyyMMddhh24:mi:ss') and checkedate<= to_date('" + strEndDate + "235959','yyyyMMddhh24:mi:ss') " + strWherePipesectionUser +
                                 NewDtComplex +
                                 strWherePipesectionUser +
                                //2015-03-13 周:涉及到时间的统计均不统计1900.有问题时他们都会把录入日期改为1900；日期为空不统计
                                " and WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')" +
                                //" and STATUS =" + mc_StatusCompletionPipe +
                                 " and WATERTYPE='" + cboWaterType.Text + "'))group by diameter,material order by " + mc_FieldNameDiameter;

                            m_strTitleOfExportExcelFileName = mc_strNameReport_NewUser;
                        }
                        break;
                    case 3:
                        if (cboWaterType.Text == mc_strWaterType_All)
                        {
                            //所有水质类型,输水管线
                            strSQL = "select diameter,material,sum(cd) shape_length from ((select diameter,material,len cd from " +
                                strPipeSectionSource + " where " +
                                //"where checkedate >= to_date('" + strBegDate + "000000','yyyyMMddhh24:mi:ss') and checkedate<= to_date('" + strEndDate + "235959','yyyyMMddhh24:mi:ss') " + strWherePipesectionSource +
                                NewDtComplex +
                                strWherePipesectionSource +
                                //2015-03-13 周:涉及到时间的统计均不统计1900.有问题时他们都会把录入日期改为1900；日期为空不统计
                                " and WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')" +
                                //" and STATUS is NULL or (STATUS <> " + mc_StatusDisusedPipe + " and trim(STATUS) <> " + mc_StatusTempPipe + 
                                //" and STATUS ="+mc_StatusCompletionPipe+
                                //"))) group by diameter,material order by  " + mc_FieldNameDiameter;
                                ")) group by diameter,material order by  " + mc_FieldNameDiameter;

                            m_strTitleOfExportExcelFileName = mc_strNameReport_NewSource;
                        }
                        else
                        {
                            //选中水质类型，输水管线
                            strSQL = "select diameter,material,sum(cd) shape_length from ((select diameter,material,len cd from " +
                                strPipeSectionSource + " where " +
                                //"where checkedate >= to_date('" + strBegDate + "000000','yyyyMMddhh24:mi:ss') and checkedate<= to_date('" + strEndDate + "235959','yyyyMMddhh24:mi:ss') " + strWherePipesectionSource +
                                NewDtComplex +
                                strWherePipesectionSource +
                                //2015-03-13 周:涉及到时间的统计均不统计1900.有问题时他们都会把录入日期改为1900；日期为空不统计
                                " and WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')" +
                                //" and STATUS IS NULL or (STATUS <> " + mc_StatusDisusedPipe + " and STATUS <> " + mc_StatusTempPipe + 
                                //" and STATUS ="+mc_StatusCompletionPipe+
                                //") and WATERTYPE='" + cboWaterType.Text + "' " +
                                " and WATERTYPE='" + cboWaterType.Text + "' " +
                                " )) group by diameter,material order by  " + mc_FieldNameDiameter;
                            m_strTitleOfExportExcelFileName = mc_strNameReport_NewSource;
                        }
                        break;
                    case 4:
                        if (cboWaterType.Text == mc_strWaterType_All)
                        {
                            //所有水质类型,和1比不区分时间
                            //strSQL =
                            //    "select diameter,material,sum(cd) shape_length from (select diameter,material,sde.st_length(shape) cd from " + m_CurParameter.GWUserName + ".pipesectionmain " +
                            //    "where (STATUS IS NULL or (STATUS IS NOT NULL and trim(STATUS) <> " + mc_StatusdisusedPipe + " and trim(STATUS) <> " + mc_StatusTempPipe + ")) union all " +
                            //    "select diameter,material,sde.st_length(shape) cd from " + m_CurParameter.GWUserName + ".pipesectionuser where (STATUS IS NULL or (STATUS IS NOT NULL and trim(STATUS) <> " + mc_StatusdisusedPipe + " and trim(STATUS) <> " + mc_StatusTempPipe + ")) union all " +
                            //    "select diameter,material,sde.st_length(shape) cd from " + m_CurParameter.GWUserName + ".pipesectionsource where (STATUS IS NULL or (STATUS IS NOT NULL and trim(STATUS) <> " + mc_StatusdisusedPipe + " and trim(STATUS) <> " + mc_StatusTempPipe + 
                            //    ")) ) group by diameter,material order by  " + mc_FieldNameDiameter;
                            strSQL =
                                 "select diameter,material,sum(cd) shape_length from ((select diameter,material,len cd from " +
                                strPipeSectionMain + " " +
                                //"where STATUS IS NULL or (STATUS <> " + mc_StatusDisusedPipe + " and STATUS <> " + mc_StatusTempPipe + 
                                //" where  STATUS ="+mc_StatusCompletionPipe+                                
                                //")) union all " +
                                ") union all " +
                                "(select diameter,material,len cd from " +
                                strPipeSectionUser + " " +
                                //" where  STATUS =" + mc_StatusCompletionPipe +
                                ") union all " +
                                "(select diameter,material,len cd from " +
                                strPipeSectionSource + " " +
                                //" where  STATUS =" + mc_StatusCompletionPipe +
                                ")) group by diameter,material order by  " + mc_FieldNameDiameter;
                        }
                        else
                        {
                            //选中水质类型
                            //strSQL =
                            //    "select diameter,material,sum(cd) shape_length from (select diameter,material,sde.st_length(shape) cd from " + m_CurParameter.GWUserName + ".pipesectionmain " +
                            //    "where (STATUS IS NULL or (STATUS IS NOT NULL and trim(STATUS) <> " + mc_StatusdisusedPipe + " and trim(STATUS) <> " + mc_StatusTempPipe + ")) and WATERTYPE='" + cboWaterType.Text + "' union all " +
                            //    "select diameter,material,sde.st_length(shape) cd from " + m_CurParameter.GWUserName + ".pipesectionuser where  (STATUS IS NULL or (STATUS IS NOT NULL and trim(STATUS) <> " + mc_StatusdisusedPipe + " and trim(STATUS) <> " + mc_StatusTempPipe + ")) and WATERTYPE='" + cboWaterType.Text + "' union all " +
                            //    "select diameter,material,sde.st_length(shape) cd from " + m_CurParameter.GWUserName + ".pipesectionsource where (STATUS IS NULL or (STATUS IS NOT NULL and trim(STATUS) <> " + mc_StatusdisusedPipe + " and trim(STATUS) <> " + mc_StatusTempPipe + ")) and WATERTYPE='" + cboWaterType.Text + "' " +
                            //    " ) group by diameter,material order by  " + mc_FieldNameDiameter;
                            strSQL =
                                "select diameter,material,sum(cd) shape_length from ((select diameter,material,len cd from " +
                                strPipeSectionMain + " " +
                                //"where STATUS IS NULL or (STATUS <> " + mc_StatusDisusedPipe + " and STATUS <> " + mc_StatusTempPipe +                                 
                                //" where STATUS= "+mc_StatusCompletionPipe+
                                " where " +
                                //" and WATERTYPE= '" + cboWaterType.Text + "')) union all " +

                                " WATERTYPE= '" + cboWaterType.Text + "') union all " +
                                "(select diameter,material,len cd from " +
                                strPipeSectionUser + " " +
                                //" where STATUS= " + mc_StatusCompletionPipe +
                                " where  " +
                                " WATERTYPE= '" + cboWaterType.Text + "') union all " +
                                "(select diameter,material,len cd from " +
                                strPipeSectionSource + " " +
                                //" where STATUS= " + mc_StatusCompletionPipe +
                                " where " +
                                "  WATERTYPE='" + cboWaterType.Text +
                                "')) group by diameter,material order by  " + mc_FieldNameDiameter;
                        }
                        break;
                    default:
                        MessageBox.Show("输入参数错误", "提示");
                        return;
                }
                #endregion

                try
                {
                    if (m_objDBCON == null)
                        m_objDBCON = CDBCon.GetInstance();
                    m_objDBCON.ExecuteSQLReturn(strSQL, ref adoReader);
                    //Get_sqlDBReader(strSQL, ref adoReader);
                    //if (adoReader == null)
                    if (adoReader.HasRows)
                    {
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
                            strDiameter = adoReader[mc_FieldNameDiameter].ToString().Trim();
                            strMaterial = adoReader[mc_FieldNameMaterial].ToString().Trim();

                            if (adoReader[mc_FieldNameDiameter].ToString().Trim() == "")
                            {
                                strDiameter = mc_strUnknownDiameter;
                                //20150206不统计未知材质和管径
                                continue;
                            }
                            if (adoReader[mc_FieldNameMaterial].ToString().Trim() == "")
                            {
                                //20150206不统计未知材质和管径
                                strMaterial = mc_strUnknownMaterial;
                                continue;
                            }
                            if (!m_lstRow.Contains(strDiameter))
                            {
                                m_lstRow.Add(strDiameter);
                            }
                            if (!m_lstColumns.Contains(strMaterial))
                            {
                                m_lstColumns.Add(strMaterial);
                            }
                            //存在数据不规范的情况，如重复记录，多n个空格，为“”，需要再判断，计算；
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
                                dr[k] = 0.00;
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
                                        dr[k] = Math.Round(Convert.ToDecimal(s[2]), 2);//改为2位2015-03-04
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
                        MessageBox.Show("所选统计类型记录为空,导出终止！", "提示！");
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误");
                    m_lstDBResults.Clear();
                    return;
                }

            }

            DateTime now = DateTime.Now;
            SaveFileDialog objSave = new SaveFileDialog();
            objSave.Filter = "电子表格(*.xls)|*.xls|所有文件(*.*)|*.*";
            objSave.FilterIndex = 0;
            objSave.RestoreDirectory = true;
            objSave.AddExtension = true;
            objSave.CreatePrompt = true;
            objSave.Title = "导出" + m_strTitleOfExportExcelFileName;
            objSave.FileName = m_strTitleOfExportExcelFileName + now.Year.ToString().PadLeft(2, '0') + now.Month.ToString().PadLeft(2, '0') + now.Day.ToString().PadLeft(2, '0')
                + now.Hour.ToString().PadLeft(2, '0') + now.Minute.ToString().PadLeft(2, '0') + now.Second.ToString().PadLeft(2, '0') + ".xls";
            try
            {
                if (objSave.ShowDialog() == DialogResult.OK)
                {
                    StringWriter sw = new StringWriter();
                    sw.WriteLine("{0}(水质类型：{1})", m_strTitleOfExportExcelFileName, cboWaterType.Text);
                    sw.WriteLine("");
                    sw.WriteLine("填报单位：\t\t 填报日期: \t\t 单位：米");
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

                    byte[] s = System.Text.Encoding.GetEncoding("gb2312").GetBytes(sw.ToString());
                    FileStream fs = new FileStream(objSave.FileName, FileMode.Create, FileAccess.Write);
                    fs.Write(s, 0, s.Length);
                    fs.Close();
                    if (MessageBox.Show("导出完毕,点击【确定】打开！", "提示", MessageBoxButtons.OK) == DialogResult.OK)
                    {
                        string strMacroName = "PERSONAL.XLSB!新增管线统计月报";
                        OpenFolderAndSelectFile(objSave.FileName, m_strExcelTemplate, strMacroName);
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
                m_pFeatureLayer = null;
                m_dicColumnCodeDesc.Clear();
                m_dicRowCodeDesc.Clear();
                m_lstDBResults.Clear();
                m_strTitleOfExportExcelFileName = "";
            }
        }
        /// <summary>
        /// 通过SDE库得到存储管线长度的表名
        /// </summary>
        /// <param name="strLayerName"></param>
        /// <returns></returns>
        private string getLayerID(string strLayerName)
        {
            try
            {
                string LayerID, strSQL;
                OleDbDataReader adoReader = null;
                strSQL = string.Format("select layer_id from sde.layers where Table_name='{0}'", strLayerName);
                if (m_objDBCON == null)
                    m_objDBCON = CDBCon.GetInstance();
                m_objDBCON.ExecuteSQLReturn(strSQL, ref adoReader);
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
        /// <summary>
        /// 导出市区配水管网长度统计报表到电子表格
        /// </summary>
        private void MainsReport2Excel()
        {
            string strSQL = string.Empty, strPipeSectionMain = string.Empty, strPipeSectionUser = string.Empty, strPipeSectionSource = string.Empty;

            DataTable dDataTableA = new DataTable();
            DataTable dDataTableB = new DataTable();

            OleDbDataReader adoReader = null;

            if (cboWaterType.Text == mc_strWaterType_All)
            {
                //strPipeSectionMain = string.Format("(select * from jjwater.pipesectionmain a,jjwater.f{0} b where a.shape=b.fid)",
                //getLayerID(m_objMap.GetLayerByName(mc_strPipesectionMainName).LayerTableName));
                //strPipeSectionUser = string.Format("(select * from jjwater.pipesectionuser a,jjwater.f{0} b where a.shape=b.fid)",
                //    getLayerID(m_objMap.GetLayerByName(mc_strPipesectionUserName).LayerTableName));
                //strPipeSectionSource = string.Format("(select * from jjwater.pipesectionsource a,jjwater.f{0} b where a.shape=b.fid)",
                //    getLayerID(m_objMap.GetLayerByName(mc_strPipesectionSourceName).LayerTableName));
                strPipeSectionMain = string.Format("(select * from jjwater.pipesectionmain a,jjwater.f{0} b where a.shape=b.fid)",
                getLayerID("PIPESECTIONMAIN"));
                strPipeSectionUser = string.Format("(select * from jjwater.pipesectionuser a,jjwater.f{0} b where a.shape=b.fid)",
                    getLayerID("PIPESECTIONUSER"));
                strPipeSectionSource = string.Format("(select * from jjwater.pipesectionsource a,jjwater.f{0} b where a.shape=b.fid)",
                    getLayerID("PIPESECTIONSOURCE"));

            }
            else
            {
                //strPipeSectionMain = string.Format("(select * from jjwater.pipesectionmain a,jjwater.f{0} b where a.shape=b.fid and a.watertype='{1}')",
                //getLayerID(m_objMap.GetLayerByName(mc_strPipesectionMainName).LayerTableName), cboWaterType.Text);
                //strPipeSectionUser = string.Format("(select * from jjwater.pipesectionuser a,jjwater.f{0} b where a.shape=b.fid and a.watertype='{1}')",
                //    getLayerID(m_objMap.GetLayerByName(mc_strPipesectionUserName).LayerTableName), cboWaterType.Text);
                //strPipeSectionSource = string.Format("(select * from jjwater.pipesectionsource a,jjwater.f{0} b where a.shape=b.fid and a.watertype='{1}')",
                //    getLayerID(m_objMap.GetLayerByName(mc_strPipesectionSourceName).LayerTableName), cboWaterType.Text);

                strPipeSectionMain = string.Format("(select * from jjwater.pipesectionmain a,jjwater.f{0} b where a.shape=b.fid and a.watertype='{1}')",
                getLayerID("PIPESECTIONMAIN"), cboWaterType.Text);
                strPipeSectionUser = string.Format("(select * from jjwater.pipesectionuser a,jjwater.f{0} b where a.shape=b.fid and a.watertype='{1}')",
                    getLayerID("PIPESECTIONUSER"), cboWaterType.Text);
                strPipeSectionSource = string.Format("(select * from jjwater.pipesectionsource a,jjwater.f{0} b where a.shape=b.fid and a.watertype='{1}')",
                    getLayerID("PIPESECTIONSOURCE"), cboWaterType.Text);
            }


            //当前地图中若有找不到数据源的图层,则会出错!
            m_pFeatureLayer = m_objMap.GetLayerByName(mc_strPipesectionMainName).FeatureLayer;
            m_dicColumnCodeDesc = GetSubtypes(m_pFeatureLayer.FeatureClass, mc_FieldNameMaterial);
            m_dicRowCodeDesc = GetSubtypes(m_pFeatureLayer.FeatureClass, mc_FieldNameDiameter);


            Boolean blnTONGJI1 = false;
            Boolean blnTONGJI2 = false;
            Boolean blnTONGJI3 = false;
            string strWherePipesectionMain = GetWhere(mc_strPipesectionMainName, ref blnTONGJI1);
            string strWherePipesectionUser = GetWhere(mc_strPipesectionUserName, ref blnTONGJI2);
            string strWherePipesectionSource = GetWhere(mc_strPipesectionSourceName, ref blnTONGJI3);

            if (strWherePipesectionMain.Trim() != "")
            {
                strWherePipesectionMain = " and " + strWherePipesectionMain;
            }
            if (strWherePipesectionUser.Trim() != "")
            {
                strWherePipesectionUser = " and " + strWherePipesectionUser;
            }
            if (strWherePipesectionSource.Trim() != "")
            {
                strWherePipesectionSource = " and " + strWherePipesectionSource;
            }

            if (blnTONGJI1 && blnTONGJI2 && blnTONGJI3)
            {
                if (cboWaterType.Text == mc_strWaterType_All)
                {
                    //strSQL = "select diameter,material,sum(cd) shape_length from (select diameter,material,SDE.ST_LENGTH(SHAPE) cd from " + m_CurParameter.GWUserName + ".pipesectionmain " +
                    //            "where (STATUS IS NULL or (STATUS IS NOT NULL and trim(STATUS) <> " + mc_StatusdisusedPipe + " and trim(STATUS) <> " + mc_StatusTempPipe + ")) union all " +
                    //            "select diameter,material,SDE.ST_LENGTH(SHAPE) cd from " + m_CurParameter.GWUserName + ".pipesectionuser where (STATUS IS NULL or (STATUS IS NOT NULL and trim(STATUS) <> " + mc_StatusdisusedPipe + " and trim(STATUS) <> " + mc_StatusTempPipe + ")) union all " +
                    //            "select diameter,material,SDE.ST_LENGTH(SHAPE) cd from " + m_CurParameter.GWUserName + ".pipesectionsource where (STATUS IS NULL or (STATUS IS NOT NULL and trim(STATUS) <> " + mc_StatusdisusedPipe + " and trim(STATUS) <> " + mc_StatusTempPipe + ")) ) group by diameter,material order by  " + mc_FieldNameDiameter;
                    strSQL =
                        "select diameter,material,sum(cd) shape_length from ((select diameter,material,len cd from " +
                        strPipeSectionMain + " " +
                        //"where STATUS IS NULL or (STATUS <> " + mc_StatusDisusedPipe + " and STATUS <> " + mc_StatusTempPipe + 
                        //"where STATUS=" + mc_StatusCompletionPipe + 
                        //2015-03-13 周:涉及到时间的统计均不统计1900.有问题时他们都会把录入日期改为1900；日期为空不统计
                                " WHERE WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')" +
                        //")) union all " +
                        ") union all " +
                        "(select diameter,material,len cd from " +
                        strPipeSectionUser + " " +
                        //"where STATUS=" + mc_StatusCompletionPipe +
                        //2015-03-13 周:涉及到时间的统计均不统计1900.有问题时他们都会把录入日期改为1900；日期为空不统计
                                " WHERE WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')" +
                        ") union all " +
                        "(select diameter,material,len cd from " +
                        strPipeSectionSource + " " +
                        // "where STATUS=" + mc_StatusCompletionPipe +
                        //2015-03-13 周:涉及到时间的统计均不统计1900.有问题时他们都会把录入日期改为1900；日期为空不统计
                                " WHERE WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')" +
                        ")) group by diameter,material order by  " + mc_FieldNameDiameter;

                }
                else
                {
                    //strSQL = "select diameter,material,sum(cd) shape_length from (select diameter,material,SDE.ST_LENGTH(SHAPE) cd from " + m_CurParameter.GWUserName + ".pipesectionmain " +
                    //           "where (STATUS IS NULL or (STATUS IS NOT NULL and trim(STATUS) <> " + mc_StatusdisusedPipe + " and trim(STATUS) <> " + mc_StatusTempPipe + ")) and WATERTYPE='" + cboWaterType.Text + "'" + " union all " +
                    //           "select diameter,material,SDE.ST_LENGTH(SHAPE) cd from " + m_CurParameter.GWUserName + ".pipesectionuser where (STATUS IS NULL or (STATUS IS NOT NULL and trim(STATUS) <> " + mc_StatusdisusedPipe + " and trim(STATUS) <> " + mc_StatusTempPipe + ")) and WATERTYPE='" + cboWaterType.Text + "'" + " union all " +
                    //           "select diameter,material,SDE.ST_LENGTH(SHAPE) cd from " + m_CurParameter.GWUserName + ".pipesectionsource where (STATUS IS NULL or (STATUS IS NOT NULL and trim(STATUS) <> " + mc_StatusdisusedPipe + " and trim(STATUS) <> " + mc_StatusTempPipe + ")) and WATERTYPE='" + cboWaterType.Text + "'" + ") group by diameter,material order by  " + mc_FieldNameDiameter;
                    strSQL =
                        "select diameter,material,sum(cd) shape_length from ((select diameter,material,len cd from " +
                        strPipeSectionMain + " " +
                        //"where STATUS IS NULL or (STATUS <> " + mc_StatusDisusedPipe + " and STATUS <> " + mc_StatusTempPipe + 
                        //"where STATUS =" + mc_StatusCompletionPipe +
                        "where " +
                        //" and WATERTYPE='" + cboWaterType.Text + "')) union all " +
                        "  WATERTYPE='" + cboWaterType.Text + "'" +
                        //2015-03-13 周:涉及到时间的统计均不统计1900.有问题时他们都会把录入日期改为1900；日期为空不统计
                                " and WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')" +
                        ") union all " +
                        "(select diameter,material,len cd from " +
                        strPipeSectionUser + " " +
                        //"where STATUS =" + mc_StatusCompletionPipe +
                        "where " +
                        "  WATERTYPE='" + cboWaterType.Text + "'" +
                        //2015-03-13 周:涉及到时间的统计均不统计1900.有问题时他们都会把录入日期改为1900；日期为空不统计
                                " and WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')" +
                        ") union all " +
                        "(select diameter,material,len cd from " +
                        strPipeSectionSource + " " +
                        //"where STATUS =" + mc_StatusCompletionPipe +
                        "where " +
                        "  WATERTYPE='" + cboWaterType.Text + "'" +
                        //2015-03-13 周:涉及到时间的统计均不统计1900.有问题时他们都会把录入日期改为1900；日期为空不统计
                                " and WRITEDATE IS NOT NULL and WRITEDATE>to_date('19491001150000','yyyyMMddhh24:mi:ss')" +
                        ")) group by diameter,material order by  " + mc_FieldNameDiameter;
                }

                try
                {
                    if (m_objDBCON == null)
                        m_objDBCON = CDBCon.GetInstance();
                    m_objDBCON.ExecuteSQLReturn(strSQL, ref adoReader);
                    //Get_sqlDBReader(strSQL, ref adoReader);
                    if (adoReader.HasRows)
                    {
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
                            strDiameter = adoReader[mc_FieldNameDiameter].ToString().Trim();
                            strMaterial = adoReader[mc_FieldNameMaterial].ToString().Trim();

                            if (adoReader[mc_FieldNameDiameter].ToString().Trim() == "")
                            {
                                //20150206 不统计未知管径材质
                                strDiameter = mc_strUnknownDiameter;
                                continue;
                            }
                            if (adoReader[mc_FieldNameMaterial].ToString().Trim() == "")
                            {
                                strMaterial = mc_strUnknownMaterial;
                                continue;
                            }
                            if (!m_lstRow.Contains(strDiameter))
                            {
                                m_lstRow.Add(strDiameter);
                            }
                            if (!m_lstColumns.Contains(strMaterial))
                            {
                                m_lstColumns.Add(strMaterial);
                            }
                            //存在数据不规范的情况，如重复记录，多n个空格，为“”，需要再判断，计算；
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
                                        dr[k] = Math.Round(Convert.ToDecimal(s[2]), 3);
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
                        MessageBox.Show("所选统计类型记录为空,导出终止！", "提示！");
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误");
                    m_lstDBResults.Clear();
                    return;
                }

            }

            DateTime now = DateTime.Now;
            SaveFileDialog objSave = new SaveFileDialog();
            objSave.Filter = "电子表格(*.xls)|*.xls|所有文件(*.*)|*.*";
            objSave.FilterIndex = 0;
            objSave.RestoreDirectory = true;
            objSave.AddExtension = true;
            objSave.CreatePrompt = true;
            objSave.Title = "导出" + mc_strNameReport_Mains;
            objSave.FileName = mc_strNameReport_Mains + now.Year.ToString().PadLeft(2, '0') + now.Month.ToString().PadLeft(2, '0') + now.Day.ToString().PadLeft(2, '0')
                + now.Hour.ToString().PadLeft(2, '0') + now.Minute.ToString().PadLeft(2, '0') + now.Second.ToString().PadLeft(2, '0') + ".xls";
            try
            {
                if (objSave.ShowDialog() == DialogResult.OK)
                {
                    StringWriter sw = new StringWriter();
                    sw.WriteLine("市区配水管网(水质类型:{0})长度统计", cboWaterType.Text);
                    sw.WriteLine("");
                    sw.WriteLine("填报单位：\t \t填报日期: \t \t 单位：米");
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

                    byte[] s = System.Text.Encoding.GetEncoding("gb2312").GetBytes(sw.ToString());
                    FileStream fs = new FileStream(objSave.FileName, FileMode.Create, FileAccess.Write);
                    fs.Write(s, 0, s.Length);
                    fs.Close();
                    if (MessageBox.Show("导出完毕,点击【确定】打开！", "提示", MessageBoxButtons.OK) == DialogResult.OK)
                    {
                        string strMacroName = "PERSONAL.XLSB!市区配水管网统计月报";
                        OpenFolderAndSelectFile(objSave.FileName, m_strExcelTemplate, strMacroName);
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
                m_pFeatureLayer = null;
                m_dicColumnCodeDesc.Clear();
                m_dicRowCodeDesc.Clear();
                m_lstDBResults.Clear();
            }

        }

        public Dictionary<string, string> GetSubtypes(IFeatureClass pFClass, string FiedName)
        {
            Dictionary<string, string> dSubtypes = new Dictionary<string, string>();
            ISubtypes pSubtype = pFClass as ISubtypes;
            if (FiedName == pSubtype.SubtypeFieldName)
            {
                if (pSubtype.HasSubtype)
                {
                    IEnumSubtype pEnumSub = pSubtype.Subtypes;
                    int lSubT;
                    string sTypes = pEnumSub.Next(out lSubT);
                    while (string.IsNullOrEmpty(sTypes) == false)
                    {
                        dSubtypes.Add(lSubT.ToString(), sTypes);
                        sTypes = pEnumSub.Next(out lSubT);
                    }
                }
            }
            return dSubtypes;
        }

        public DataTable Get_QuerySheet(string sql)
        {
            try
            {
                string constr = "Provider=OraOLEDB.Oracle;Data Source=jjwater;Persist Security Info=True;User ID=jjwater;Password=jjwater;Unicode=True";
                DataTable DataTab = new DataTable();
                using (var myconnection = new OleDbConnection(constr))
                {
                    myconnection.Open();
                    using (var mycommand = new OleDbCommand(sql, myconnection))
                    {
                        using (var Oracle = new OleDbDataAdapter(mycommand))
                        {
                            Oracle.Fill(DataTab);
                        }
                    }
                }
                return DataTab;
            }
            catch (Exception e)
            {
                MsgBox.Show(e.ToString());
                throw e;
            }
        }

        //private static Microsoft.Office.Interop.Excel.Application ExcelApp;//Define a Excel Application object

        private void OpenFolderAndSelectFile(string fileFullName, string strTemplateFileName, string strMacro4Run)
        {
            // 打开文件所在的目录
            //System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
            //psi.Arguments = "/e,/select," + fileFullName;
            //System.Diagnostics.Process.Start(psi);
            //System.Diagnostics.Process.Start(fileFullName);


            //判断模板是否存在
            if (!File.Exists(strTemplateFileName))
            {
                MessageBox.Show("模板文件不存在，请联系技术人员!", "操作提示");
                return;
            }
            Excel.Application ExcelApp = new Excel.Application();
            ExcelApp.DefaultFilePath = m_strDefaultExcelFilePath;
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
                MessageBox.Show(ex.Message, "错误提示");
                return;
            }

        }

        #region 撤出管线统计时间条件选择
        private void cboBuildDt_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_To.Visible = (cboRemoveDt.Text == mc_WithinSymbol);
            dTPDateTo.Enabled = true;
            lbl_To.Enabled = true;
            dTPDateTo.Visible = lbl_To.Visible;
            RemoveDtComplex = CombWhereDate(cboRemoveDt.Text,
                                           mc_StrQueryBFFieldNameRemovedt,
                                           DateToChar(dTPDateFrom.Value),
                                           DateToChar(dTPDateTo.Value)
                                            );

        }
        //撤除时间From
        private void dTPDateFrom_ValueChanged(object sender, EventArgs e)
        {
            RemoveDtSimple = "";
            RemoveDtComplex = CombWhereDate(cboRemoveDt.Text, mc_StrQueryBFFieldNameRemovedt, DateToChar(dTPDateFrom.Value), DateToChar(dTPDateTo.Value));

            //简单查询
            if (cboRemoveDt.Text != mc_WithinSymbol)
            {
                string strDt = string.Empty;
                string strCond = string.Empty;
                strDt = dTPDateFrom.Value.ToString();
                strCond = " SUBSTR( TRIM(" + mc_StrQueryBFFieldNameRemovedt + "),1,6)=" + strDt.Substring(1, 6);
                RemoveDtSimple = strCond;
                //BuildDataTitle = formatDataCN(strDt).Substring(1, 8);
            }
        }
        //撤除时间To
        private void dTPDateTo_ValueChanged(object sender, EventArgs e)
        {
            RemoveDtSimple = "";
            RemoveDtComplex = CombWhereDate(cboRemoveDt.Text, mc_StrQueryBFFieldNameRemovedt, DateToChar(dTPDateFrom.Value), DateToChar(dTPDateTo.Value));

        }
        #endregion

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
                    if (strField == mc_StrAnnualReportStatusDate)//年报统计时 之前用录入日期，且统计为空，介于用审核日期。2015-06-30 贾主任
                    { m_strRet = "(" + strField + " is null or " + strField + "<to_date('" + strStartNum + "000000','yyyyMMddhh24:mi:ss'))"; }
                    //strRetTitle = "" + formatDataCN(strStartNum) + "之前";
                    else
                    { m_strRet = "(" + strField + "<to_date('" + strStartNum + "000000','yyyyMMddhh24:mi:ss'))"; }
                    break;
                case mc_SmallEqualSymbol:
                    if (strField == mc_StrAnnualReportStatusDate)//年报统计时 之前用录入日期，且统计为空，介于用审核日期。2015-06-30 贾主任
                    { m_strRet = "(" + strField + " is null or " + strField + "<=to_date('" + strStartNum + "235959','yyyyMMddhh24:mi:ss'))"; }
                    else
                    { m_strRet = "(" + strField + "<=to_date('" + strStartNum + "235959','yyyyMMddhh24:mi:ss'))"; }
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

        #region 新增统计月报：时间条件选择
        private void cboCheckDt_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTo.Visible = (cboCheckDt.Text == mc_WithinSymbol);
            dtpEndStateDate.Enabled = true;
            lblTo.Enabled = true;
            dtpEndStateDate.Visible = lblTo.Visible;
            NewDtComplex = CombWhereDate(cboCheckDt.Text,
                                           mc_StrQueryGWFieldNameCheckdt,
                                           DateToChar(dtpBegStateDate.Value),
                                           DateToChar(dtpEndStateDate.Value)
                                            );
        }
        //新增统计月报：From  Time变化
        private void dtpBegStateDate_ValueChanged(object sender, EventArgs e)
        {
            NewBuildDtSimple = "";
            NewDtComplex = CombWhereDate(cboCheckDt.Text,
                                            mc_StrQueryGWFieldNameCheckdt,
                                            DateToChar(dtpBegStateDate.Value),
                                            DateToChar(dtpEndStateDate.Value)
                                            );

            //简单查询
            if (cboCheckDt.Text != mc_WithinSymbol)
            {
                string strDt = string.Empty;
                string strCond = string.Empty;
                strDt = dtpBegStateDate.Value.ToString();
                strCond = " SUBSTR( TRIM(" + mc_StrQueryGWFieldNameCheckdt + "),1,6)=" + strDt.Substring(1, 6);
                NewBuildDtSimple = strCond;
                //BuildDataTitle = formatDataCN(strDt).Substring(1, 8);
            }
        }
        //新增统计月报：To Time 变化
        private void dtpEndStateDate_ValueChanged(object sender, EventArgs e)
        {
            NewBuildDtSimple = "";
            NewDtComplex = CombWhereDate(cboCheckDt.Text,
                                            mc_StrQueryGWFieldNameCheckdt,
                                            DateToChar(dtpBegStateDate.Value),
                                            DateToChar(dtpEndStateDate.Value)
                );

        }
        #endregion

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {

            if (e.TabPage == tabPage1 || e.TabPage == tabPage2)
            {
                cboWaterType.Enabled = true;
            }
            else if (e.TabPage == tabPage3 || e.TabPage == tabPage4)
            {
                cboWaterType.Enabled = false;
            }

        }

        #region 管网年报统计时间选择
        private void cboCheckDt4_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTo4.Visible = (cboCheckDt4.Text == mc_WithinSymbol);
            dtpEndStateDate4.Enabled = true;
            lblTo4.Enabled = true;
            dtpEndStateDate4.Visible = lblTo4.Visible;
            //年报统计时 之前用录入日期，且统计为空，介于用审核日期。2015-06-18 贾主任
            if (cboCheckDt4.Text == mc_SmallEqualSymbol || cboCheckDt4.Text == mc_SmallSymbol)
            {
                AnnualDtComplex = CombWhereDate(cboCheckDt4.Text,
                                           mc_StrAnnualReportStatusDate,//统计采用的时间
                                           DateToChar(dtpBegStateDate4.Value),
                                           DateToChar(dtpEndStateDate4.Value)
                                            );
            }
            else
            {
                AnnualDtComplex = CombWhereDate(cboCheckDt4.Text,
                                           mc_StrQueryGWFieldNameCheckdt,//统计采用的时间
                                           DateToChar(dtpBegStateDate4.Value),
                                           DateToChar(dtpEndStateDate4.Value)
                                            );
            }

        }
        //管网统计年报：From  Time变化
        private void dtpBegStateDate4_ValueChanged(object sender, EventArgs e)
        {
            AnnualDtSimple = "";
            //年报统计时 之前用录入日期，且统计为空，介于用审核日期。2015-06-18 贾主任
            if (cboCheckDt4.Text == mc_SmallEqualSymbol || cboCheckDt4.Text == mc_SmallSymbol)
            {
                AnnualDtComplex = CombWhereDate(cboCheckDt4.Text,
                                            mc_StrAnnualReportStatusDate,
                                            DateToChar(dtpBegStateDate4.Value),
                                            DateToChar(dtpEndStateDate4.Value)
                                            );
            }
            else
            {
                //其他的用审核日期
                AnnualDtComplex = CombWhereDate(cboCheckDt4.Text,
                                            mc_StrQueryGWFieldNameCheckdt,
                                            DateToChar(dtpBegStateDate4.Value),
                                            DateToChar(dtpEndStateDate4.Value)
                                            );
            }


            //简单查询
            if (cboCheckDt4.Text != mc_WithinSymbol)
            {
                string strDt = string.Empty;
                string strCond = string.Empty;
                strDt = dtpBegStateDate4.Value.ToString();
                strCond = " SUBSTR( TRIM(" + mc_StrQueryGWFieldNameCheckdt + "),1,6)=" + strDt.Substring(1, 6);
                AnnualDtSimple = strCond;
                //BuildDataTitle = formatDataCN(strDt).Substring(1, 8);
            }
        }


        //管网统计年报：To Time变化
        private void dtpEndStateDate4_ValueChanged(object sender, EventArgs e)
        {
            AnnualDtSimple = "";


            AnnualDtComplex = CombWhereDate(cboCheckDt4.Text,
                                            mc_StrQueryGWFieldNameCheckdt,
                                            DateToChar(dtpBegStateDate4.Value),
                                            DateToChar(dtpEndStateDate4.Value)
                );
        }

        #endregion

        private void btnState_Click(object sender, EventArgs e)
        {

        }











    }
}
