using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Framework;
using JJWATBaseLibC;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.ArcMapUI;
using AOBaseLibC;
using ESRI.ArcGIS.Geodatabase;
using System.Data.OleDb;
using System.Reflection;
using System.Text.RegularExpressions;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using System.Collections;
using AOBaseLibC.AFGeodatabase;

namespace JJWATQuery
{
    public partial class FrmQueryRangeResult : Form
    {
        private IApplication m_pApp;
        private string m_strMin = string.Empty;
        private string m_strMax = string.Empty;
        private CDBCon m_objDBCON = null;
        private SysParameters m_CurParameter = null;
        private TreeNode lastClickNode;
        private IMap m_pMap;
        private IMxDocument m_pMxdoc;
        private AFMap objMap = new AFMap();
        private AOBaseLibC.AFGeodatabase.AFFeature objFeature;
        private AOBaseLibC.AFGeodatabase.AFFeatureLayer objFeatureLayer;
        List<string> m_strDisIDs = new List<string>();
        private List<AOBaseLibC.AFGeodatabase.AFFeatureLayer> m_objFeatureLayers = new List<AOBaseLibC.AFGeodatabase.AFFeatureLayer>();

        private Dictionary<string, IList<ClsResultSet>> DicHistory = new Dictionary<string, IList<ClsResultSet>>();
        private IFeatureSelection m_pFeatureSelection;

        private Dictionary<string, IList<int>> dicFeatures4Located = new Dictionary<string, IList<int>>();//layername&oids
        private Dictionary<string, string> m_dicTableUidPath = new Dictionary<string, string>();

        List<IFeatureClass> m_lstFC2Export;
        List<IFeature> m_lstFeature2Display;

        private const string mc_strNameOfRootNodeV = "关阀数量";
        private const string mc_strNameOfRootNodeE = "管线设备列表";

        private const string mc_TableValve = "VALVE";//配水干线
        private const string mc_TablePIPESECTIONMAIN = "PIPESECTIONMAIN";//配水干线
        private const string mc_TablePIPESECTIONUSER = "PIPESECTIONUSER";//配水支线
        private const string mc_TablePIPESECTIONSOURCE = "PIPESECTIONSOURCE";//输水管线

        private AOBaseLibC.AFCommon.AFConst.DataSourceType m_DataSourceType;//数据库类型，从parameter来 唐

        private IList<ShutdownValveScheme> lstQueryResult;
        private IList<ShutdownValveScheme> lstQueryClsResult;

        private strctData4ValveQuery m_obj_DataStrcut = new strctData4ValveQuery();//用于接受数据结构

        private Dictionary<string, IList<ShutdownValveScheme>> m_dicTableSearchHistory = new Dictionary<string, IList<ShutdownValveScheme>>();
        private Dictionary<string, IList<ShutdownValveScheme>> m_dicTableSearchHistoryDetail = new Dictionary<string, IList<ShutdownValveScheme>>();

        List<string> lstDicResultsKeys = new List<string>();
        //Boolean bYesNo = false;
        public static int tabForm;
        //private int inum = 10;
        //private bool m_IsSelectAll;
        //private string sMin, sMax;
       private Boolean m_bln_HasLocated = false;
        private int CountNum;
        private int NowValue = 0;
        //Boolean m_bln_HasLocated = false;
        private Frm_LoadOutEXCLE Frm_LoadOutEXCLE_model = null;
        public void InitResultForm(strctData4ValveQuery DataStructedFromClick, IApplication Application)
        {
            m_obj_DataStrcut = DataStructedFromClick;
            m_pApp = Application;
            m_objDBCON = CDBCon.GetInstance();
            m_CurParameter = SysParameters.GetInstance();
        }
        public FrmQueryRangeResult()
        {
            InitializeComponent();
        }

        private void FrmQueryRangeResult_Load(object sender, EventArgs e)
        {
            try
            {
                LoadTreeNodes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
        //加载树
        private void LoadTreeNodes()
        {

            TreeNode nodes = new TreeNode();
            if (m_obj_DataStrcut.Pipelinequipment.Count == 0)
            {
                this.Close();
                return;
            }

            for (int i = 0; i < m_obj_DataStrcut.Pipelinequipment.Count; i++)
            {
                TreeNode node = new TreeNode();
                node.Text = m_obj_DataStrcut.Pipelinequipment[i];
                nodes.Nodes.Add(node);
            }
            nodes.Text = "管线设备列表";
            trvQueryResult.Nodes.Add(nodes);
            trvQueryResult.ExpandAll();


        }

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

        private void trvQueryResult_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (e.Node.Text == "" || e.Node.Text == "管线设备列表")
                    {
                        return;
                    }

                    else
                    {
                        objFeature = new AOBaseLibC.AFGeodatabase.AFFeature();
                        m_pMxdoc = m_pApp.Document as IMxDocument;
                        m_pMap = m_pMxdoc.FocusMap;
                        objMap.Map = m_pMap;
                        m_objFeatureLayers.Clear();
                        m_dicTableUidPath.Clear();

                        string skey = e.Node.Text;
                        skey = skey.Substring(0, skey.Length);
                        //全市查询
                        dataGrid_QueryResult.DataSource = null;
                        dataGrid_Detail4clsid.DataSource = null;
                        if (m_obj_DataStrcut.bIsAll == true)
                        {

                            //缓存机制
                            if (!DicHistory.Keys.Contains(skey))
                            {
                                string strSQLQueryEquipment = string.Format("select DISID ,LAYERNAME, VALVEID,STOPID, DISTANCE ,VPATH from {0}.closevalvedistance where LAYERNAME='{1}' and DISTANCE between {2} and {3}", m_CurParameter.GWUserName, skey, m_obj_DataStrcut.dMinDistance, m_obj_DataStrcut.dMaxDistance);
                                string strSQLCount = string.Format("select count(*) from {0}.closevalvedistance where  LAYERNAME='{1}' and DISTANCE between {2} and {3}", m_CurParameter.GWUserName, skey, m_obj_DataStrcut.dMinDistance, m_obj_DataStrcut.dMaxDistance);
                                OleDbDataReader adoReader = null;
                                //获取符合条件的数据的条数用于进度条显示处理
                                m_objDBCON.ExecuteSQLReturn(strSQLCount, ref adoReader);
                                while (adoReader.Read())
                                {
                                    CountNum = Convert.ToInt32(adoReader[0].ToString());
                                    break;
                                }
                                progressBarSearching.Maximum = CountNum;
                                progressBarSearching.Visible = true;
                                NowValue = 0;
                                adoReader = null;
                                m_objDBCON.ExecuteSQLReturn(strSQLQueryEquipment, ref adoReader);
                                IList<ClsResultSet> lis = new List<ClsResultSet>();
                                while (adoReader.Read())
                                {
                                    ClsResultSet result = new ClsResultSet();
                                    result.DisId = adoReader["DISID"].ToString();
                                    result.LayerName = adoReader["LAYERNAME"].ToString();
                                    result.ValveID = adoReader["VALVEID"].ToString();
                                    result.StopID = adoReader["STOPID"].ToString();
                                    result.Distance = adoReader["DISTANCE"].ToString();
                                    result.Vpath = adoReader["VPATH"].ToString();
                                    result.UidPath = adoReader["DISID"].ToString() + adoReader["LAYERNAME"].ToString();

                                    lis.Add(result);
                                    if (!m_dicTableUidPath.ContainsKey(result.UidPath))
                                        m_dicTableUidPath.Add(result.UidPath, result.Vpath);
                                    progressBarSearching.Value = NowValue++;
                                }

                                

                                adoReader.Dispose();
                                DicHistory.Add(skey, lis);
                                progressBarSearching.Visible = false;
                            }


                            lbl_DataViewCLSID.Visible = true;
                            lbl_DataViewCLSID.Text = "共有" + DicHistory[skey].Count + "条记录";
                            if (DicHistory[skey].Count > 0)
                            {
                                dataGrid_QueryResult.DataSource = DicHistory[skey];
                            }

                            dataGrid_QueryResult.Columns.Clear();
                            dataGrid_QueryResult.TopLeftHeaderCell.Value = "";
                            displayCol(dataGrid_QueryResult, "DisID", "距离序号");
                            displayCol(dataGrid_QueryResult, "LayerName", "设备图层");
                            displayCol(dataGrid_QueryResult, "ValveID", "阀门ID");
                            displayCol(dataGrid_QueryResult, "StopID", "终止设备ID");
                            displayCol(dataGrid_QueryResult, "Distance", "距离信息");

                        }
                        //区域查询
                        else
                        {
                            //如果缓存中没有
                            if (!DicHistory.Keys.Contains(skey))
                            {
                                IList<ClsResultSet> lis = new List<ClsResultSet>();
                                progressBarSearching.Maximum = m_obj_DataStrcut.lstStrValveIDs.Count;
                                progressBarSearching.Visible = true;
                                NowValue = 0;
                                for (int k = 0; k < m_obj_DataStrcut.lstStrValveIDs.Count; k++)
                                {
                                    progressBarSearching.Value = NowValue++;
                                    string strSqlRegion = string.Format("select DISID ,LAYERNAME, VALVEID,STOPID, DISTANCE ,VPATH from {0}.closevalvedistance where LAYERNAME='{1}' and VALVEID={4} and DISTANCE between {2} and {3}", m_CurParameter.GWUserName, skey, m_obj_DataStrcut.dMinDistance, m_obj_DataStrcut.dMaxDistance, m_obj_DataStrcut.lstStrValveIDs[k]);
                                    OleDbDataReader adoReaderReginal = null;
                                    m_objDBCON.ExecuteSQLReturn(strSqlRegion, ref adoReaderReginal);

                                    while (adoReaderReginal.Read())
                                    {
                                        ClsResultSet result = new ClsResultSet();
                                        result.DisId = adoReaderReginal["DISID"].ToString();
                                        result.LayerName = adoReaderReginal["LAYERNAME"].ToString();
                                        result.ValveID = adoReaderReginal["VALVEID"].ToString();
                                        result.StopID = adoReaderReginal["STOPID"].ToString();
                                        result.Distance = adoReaderReginal["DISTANCE"].ToString();
                                        result.Vpath = adoReaderReginal["VPATH"].ToString();
                                        result.UidPath = adoReaderReginal["DISID"].ToString() + adoReaderReginal["LAYERNAME"].ToString();
                                        lis.Add(result);
                                        if (!m_dicTableUidPath.ContainsKey(result.UidPath))
                                            m_dicTableUidPath.Add(result.UidPath, result.Vpath);
                                    }

                                }

                                DicHistory.Add(skey, lis);
                                progressBarSearching.Visible = false;
                            }

                            lbl_DataViewCLSID.Visible = true;
                            lbl_DataViewCLSID.Text = "共有" + DicHistory[skey].Count + "条记录";
                            if (DicHistory[skey].Count > 0)
                            {
                                dataGrid_QueryResult.DataSource = DicHistory[skey];
                            }

                            dataGrid_QueryResult.Columns.Clear();
                            dataGrid_QueryResult.TopLeftHeaderCell.Value = "";
                            displayCol(dataGrid_QueryResult, "DisID", "距离序号");
                            displayCol(dataGrid_QueryResult, "LayerName", "设备图层");
                            displayCol(dataGrid_QueryResult, "ValveID", "阀门ID");
                            displayCol(dataGrid_QueryResult, "StopID", "终止设备ID");
                            displayCol(dataGrid_QueryResult, "Distance", "距离信息");


                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误");

            }





        }
        public class ValveAttribute
        {
            private string strAttrName;

            public string StrAttrName
            {
                get { return strAttrName; }
                set { strAttrName = value; }
            }

            private string strAttrValue;

            public string StrAttrValue
            {
                get { return strAttrValue; }
                set { strAttrValue = value; }
            }

        }
        private void dataGrid_QueryResult_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            #region 获取管网设备信息

            int rownumber = dataGrid_QueryResult.CurrentCell.RowIndex;
            List<ValveAttribute> ValveAttributes = new List<ValveAttribute>();
            if (rownumber >= 0)
            {

                IFeatureClass pFC_Valve_Selected = objMap.GetLayerByName(mc_TableValve).FeatureLayer.FeatureClass;
                int ValveID = Convert.ToInt32(dataGrid_QueryResult.Rows[rownumber].Cells[2].Value.ToString());
                if (pFC_Valve_Selected != null && ValveID > -1)
                {
                    IFeature pFeature;
                    try { pFeature = pFC_Valve_Selected.GetFeature(ValveID);
                       
                    }
                    catch
                    {
                        MessageBox.Show("查无此阀门的详细信息！", "提示");
                        return;
                    }
                    if (pFeature != null)
                    {
                        ValveAttributes.Clear();
                        IField pField;
                        IFields pFields = pFC_Valve_Selected.Fields;
                        for (int i = 0; i < pFields.FieldCount; i++)
                        {
                            pField = pFields.get_Field(i);
                            if (pField.AliasName.ToUpper() != "Shape".ToUpper())
                            {
                                ValveAttribute vab = new ValveAttribute();
                                vab.StrAttrValue = pFeature.get_Value(i).ToString();
                                vab.StrAttrName = pField.AliasName;
                                ValveAttributes.Add(vab);
                            }
                        }

                        dataGrid_Detail4clsid.DataSource = ValveAttributes;
                        dataGrid_Detail4clsid.Columns.Clear();
                        dataGrid_Detail4clsid.TopLeftHeaderCell.Value = "属性名称";
                        displayCol(dataGrid_Detail4clsid, "strAttrValue", "属性值");
                        for (int i = 0; i < dataGrid_Detail4clsid.Rows.Count; i++)
                        {
                            DataGridViewRow row = this.dataGrid_Detail4clsid.Rows[i];
                            row.HeaderCell.Value = ValveAttributes[i].StrAttrName;
                        }
                        dataGrid_Detail4clsid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
                    }

                    else
                    {
                        MessageBox.Show("未发现编号为[" + ValveID + "]阀门", "查询提示");
                        return;
                    }
                }




            #endregion
            }
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            if (Frm_LoadOutEXCLE_model == null || Frm_LoadOutEXCLE_model.IsDisposed)
            {
                Frm_LoadOutEXCLE_model = new Frm_LoadOutEXCLE();
                Frm_LoadOutEXCLE_model.m_obj_DataStrcut = this.m_obj_DataStrcut;
                Frm_LoadOutEXCLE_model.DicHistory = this.DicHistory;
                Frm_LoadOutEXCLE_model.m_CurParameter = this.m_CurParameter;
                Frm_LoadOutEXCLE_model.Show();
            }

            else
            {

                Frm_LoadOutEXCLE_model.Activate();

            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //王长明
        /// <summary>
        /// 定位管网设备，有阀门的话，定位阀门，没有阀门则定位管线
        /// </summary>
        /// <param name="layername">管线层</param>
        /// <param name="oid">OBJECTID</param>
        /// <param name="ValveID">阀门ID</param>
        private void GetFirstValvePosition(string layername, string oid, string ValveIDs)
        {
            IPoint pFromPointOfLine = new PointClass();
            IPoint pToPointOfLine = new PointClass();

            IPolyline plt = new PolylineClass();
            IGeometryCollection gc = (IGeometryCollection)plt;
            IPointCollection5 pPc = new MultipointClass();

            IGraphicsContainer cont;
            cont = m_pMxdoc.ActivatedView as IGraphicsContainer;
            cont.DeleteAllElements();

            //依次读取出阀门ID并得到阀门点集合的整体范围；

            if (ValveIDs.ToString().Trim() != "")
            {
                string[] ValveID = ValveIDs.Split(';');

                double dbl_ValvesEnvelop_Max_X = 0, dbl_ValvesEnvelop_Min_X = 0;
                double dbl_ValvesEnvelop_Max_Y = 0, dbl_ValvesEnvelop_Min_Y = 0;


                for (int i = 0; i < ValveID.Length; i++)
                {
                    if (ValveID[i].ToString().Trim() == "")
                        continue;
                    objFeature = getFeature(mc_TableValve, ValveID[i]);
                    if (objFeature.Feature == null)
                    {
                        //MessageBox.Show("未发现阀门：" + ValveID[i]);
                        continue;
                    }
                    pFromPointOfLine = objFeature.Feature.ShapeCopy as IPoint;
                    if (pFromPointOfLine != null)
                    {

                        IPoint p = pFromPointOfLine;
                        pPc.AddPoint(p, Type.Missing, Type.Missing);
                        if (i == 0)
                        {
                            dbl_ValvesEnvelop_Max_X = p.X;
                            dbl_ValvesEnvelop_Min_X = p.X;
                            dbl_ValvesEnvelop_Max_Y = p.Y;
                            dbl_ValvesEnvelop_Min_Y = p.Y;
                        }
                        else if (i > 0)
                        {
                            dbl_ValvesEnvelop_Max_X = dbl_ValvesEnvelop_Max_X > p.X ? dbl_ValvesEnvelop_Max_X : p.X;
                            dbl_ValvesEnvelop_Max_Y = dbl_ValvesEnvelop_Max_Y > p.Y ? dbl_ValvesEnvelop_Max_Y : p.Y;
                            dbl_ValvesEnvelop_Min_X = dbl_ValvesEnvelop_Min_X < p.X ? dbl_ValvesEnvelop_Min_X : p.X;
                            dbl_ValvesEnvelop_Min_Y = dbl_ValvesEnvelop_Min_Y < p.Y ? dbl_ValvesEnvelop_Min_Y : p.Y;
                        }
                        //定位成功
                        m_bln_HasLocated = true;
                        //设置选中行阀门的样式：红点
                        IRgbColor pRgbColor = new RgbColorClass();
                        pRgbColor.Red = 255;
                        ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbolClass();
                        pSimpleMarkerSymbol.Color = pRgbColor;
                        pSimpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
                        pSimpleMarkerSymbol.Size = 0xa;
                        IMarkerElement pMe = new MarkerElementClass();
                        pMe.Symbol = pSimpleMarkerSymbol as IMarkerSymbol;
                        IElement pE = pMe as IElement;
                        pE.Geometry = p;
                        cont.AddElement(pE, 0);
                    }

                }

                IMultipoint pMp = pPc as IMultipoint;
                if (pMp.IsEmpty == true)
                {
                    MessageBox.Show("阀门定位失败，未找到阀门[" + ValveIDs + "],点击【确定】定位管线");
                }
                else
                {
                    IEnvelope elp = new EnvelopeClass();
                    //单点定位      
                    if (pPc.PointCount == 1)
                    {
                        IPoint pm = pPc.get_Point(0);
                        elp = pm.Envelope;
                        elp.Width = 30;
                        elp.Height = 30;
                        m_pMxdoc.ActiveView.Extent.CenterAt(pm);
                        m_pMxdoc.ActivatedView.Extent = elp.Envelope;
                        m_pMxdoc.ActivatedView.FocusMap.MapScale = 1000;
                    }
                    else
                    {
                        //多点显示multiPoint的包络
                        elp = pMp.Envelope;
                        m_pMxdoc.ActiveView.Extent = elp.Envelope;
                    }
                    m_pMxdoc.ActiveView.Refresh();
                }


            }
            //定位失败并且
            if (!m_bln_HasLocated || ValveIDs.ToString().Trim() == "")
            {
                objFeature = getFeature(layername, oid);
                //若获取的管线为空，则给出提示
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

                    MessageBox.Show("设备[" + msgLayer + ":" + oid + "]定位失败");
                    return;
                }
                //否则获取管线起始点进行定位
                switch (layername.Trim().ToUpper())
                {
                    case mc_TablePIPESECTIONMAIN:
                    case mc_TablePIPESECTIONUSER:
                    case mc_TablePIPESECTIONSOURCE:
                        gc.AddGeometryCollection((IGeometryCollection)objFeature.Feature.ShapeCopy);

                        plt = (IPolyline)gc;
                        pFromPointOfLine = plt.FromPoint as IPoint;//管线的From点；
                        pToPointOfLine = plt.ToPoint as IPoint;//管线的To点；
                        break;
                    default:
                        MessageBox.Show("不能定位该设备！", "提示！");
                        return;
                }
                //po = objFeature.Feature.ShapeCopy as IPoint;
                if (pFromPointOfLine != null)
                {
                    IPoint p = new PointClass();

                    p.X = (pFromPointOfLine.X + pToPointOfLine.X) / 2;
                    p.Y = (pFromPointOfLine.Y + pToPointOfLine.Y) / 2;

                    ITopologicalOperator pto = plt as ITopologicalOperator;
                    IPolygon pPlygn = pto.Buffer(2) as IPolygon;

                    m_bln_HasLocated = true;

                    IEnvelope elp = pPlygn.Envelope;

                    //elp.Width = 100;
                    //elp.Height = 60;

                    m_pMxdoc.ActiveView.Extent.CenterAt(p);
                    m_pMxdoc.ActiveView.Extent = elp.Envelope;
                    m_pMxdoc.ActivatedView.FocusMap.MapScale = 2500;
                    m_pMxdoc.ActiveView.Refresh();

                    //设置选中行阀门的样式：点划黄线 
                    IRgbColor pRgbColor = new RgbColorClass();
                    pRgbColor.Red = 255;
                    pRgbColor.Green = 255;


                    ISimpleLineSymbol pSimpleLineSymbol = new SimpleLineSymbolClass();
                    pSimpleLineSymbol.Color = pRgbColor;
                    pSimpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDash;
                    pSimpleLineSymbol.Width = 3;
                    ILineElement pLe = new LineElementClass();
                    pLe.Symbol = pSimpleLineSymbol as ILineSymbol;
                    IElement pE = pLe as IElement;
                    pE.Geometry = plt;
                    cont.AddElement(pE, 0);
                }
                else
                {

                }

            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFromPointOfLine);


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
        //王长明
        private void btn_Locate_Click(object sender, EventArgs e)
        {
            if (dataGrid_Detail4clsid.Rows.Count < 1)
            {
                MessageBox.Show("结果为空，不能定位！", "操作提示");
                return;
            }
            ClearSelection();
            int rownumber = dataGrid_Detail4clsid.CurrentCell.RowIndex;
            string strLocLayername = string.Empty;
            string strLocLineID = string.Empty;
            string strLoc1stValveID = string.Empty;
            string strLocUID = string.Empty;
            string strVpath = string.Empty;



            try
            {
                #region 定位阀门设备
                //定位设备图层
                rownumber = dataGrid_QueryResult.CurrentCell.RowIndex;
                strLocLayername = dataGrid_QueryResult.Rows[rownumber].Cells[1].Value.ToString();
                //设备ID
                strLocLineID = dataGrid_QueryResult.Rows[rownumber].Cells[3].Value.ToString();

                //拼接的UID  序号+图层
                strLocUID = dataGrid_QueryResult.Rows[rownumber].Cells[0].Value.ToString() + dataGrid_QueryResult.Rows[rownumber].Cells[1].Value.ToString();

                //阀门ID
                strLoc1stValveID = dataGrid_QueryResult.Rows[rownumber].Cells[2].Value.ToString();

                IList<int> lstM4L = new List<int>();
                IList<int> lstU4L = new List<int>();
                IList<int> lstS4L = new List<int>();
                IList<int> lstOthers = new List<int>();

                #region 管线路径 dicFeatures4Located

                if (m_dicTableUidPath.ContainsKey(strLocUID))
                {
                    strVpath = m_dicTableUidPath[strLocUID];

                    for (int i = 0; i <= (Regex.Matches(strVpath, ";").Count); i++)
                    {
                        //分号的位置
                        int iSemicolon = strVpath.IndexOf(";");
                        if (iSemicolon > 0)
                        {
                            string sTempPath = "";
                            sTempPath = strVpath.Substring(0, iSemicolon);
                            {
                                string sLName = "";
                                string sLID = "";

                                sLName = sTempPath.Substring(0, sTempPath.IndexOf(","));
                                sLID = sTempPath.Substring(sTempPath.IndexOf(",") + 1, sTempPath.Length - sTempPath.IndexOf(",") - 1);
                                switch (sLName)
                                {
                                    case "配水干线":
                                        if (!lstM4L.Contains(Convert.ToInt32(sLID)))
                                        {
                                            lstM4L.Add(Convert.ToInt32(sLID));
                                        }

                                        break;
                                    case "配水支线":
                                        if (!lstU4L.Contains(Convert.ToInt32(sLID)))
                                        {
                                            lstU4L.Add(Convert.ToInt32(sLID));
                                        }
                                        break;
                                    case "输水管线":
                                        if (!lstS4L.Contains(Convert.ToInt32(sLID)))
                                        {
                                            lstS4L.Add(Convert.ToInt32(sLID));
                                        }
                                        break;
                                    default:
                                        MessageBox.Show("设备信息有误");
                                        break;
                                }
                            }
                            
                            strVpath = strVpath.Remove(0, iSemicolon + 1);
                        }

                    }


                    if (lstM4L.Count > 0)
                    {
                        if (!dicFeatures4Located.ContainsKey(mc_TablePIPESECTIONMAIN))
                            dicFeatures4Located.Add(mc_TablePIPESECTIONMAIN, lstM4L);
                    }
                    if (lstU4L.Count > 0)
                    {
                        if (!dicFeatures4Located.ContainsKey(mc_TablePIPESECTIONUSER))
                            dicFeatures4Located.Add(mc_TablePIPESECTIONUSER, lstU4L);
                    }
                    if (lstS4L.Count > 0)
                    {
                        if (!dicFeatures4Located.ContainsKey(mc_TablePIPESECTIONSOURCE))
                            dicFeatures4Located.Add(mc_TablePIPESECTIONSOURCE, lstS4L);
                    }
                    if (lstOthers.Count > 0)
                    {
                        MessageBox.Show("未发现设备" + lstOthers.ToString(), "温馨提示");
                        return;
                    }



                }
                else
                {
                    return;
                }
                #endregion

                #region 管网设备

                string OTableName = objMap.GetLayerByName(strLocLayername).LayerTableName;
                lstOthers.Add(Convert.ToInt32(strLocLineID));
                if (!dicFeatures4Located.ContainsKey(OTableName))
                {
                    dicFeatures4Located.Add(OTableName, lstOthers);
                }

                #endregion

               
                foreach (var o in dicFeatures4Located)
                {
                    if (o.Value.Count > 0)
                    {

                        int[] oidList = o.Value.ToArray();
                        objFeatureLayer = objMap.GetLayerByName(o.Key.ToString());
                        IFeatureCursor pFCursor = objFeatureLayer.FeatureLayer.FeatureClass.GetFeatures(oidList, false);
                        IFeature pFeature = pFCursor.NextFeature();
                        while (pFeature != null)
                        {
                            m_pFeatureSelection = objFeatureLayer.FeatureLayer as IFeatureSelection;
                            m_pFeatureSelection.Add(pFeature);
                            pFeature = pFCursor.NextFeature();
                        }
                      
                    }
                }
               
                GetFirstValvePosition(mc_TableValve, "", strLoc1stValveID);

                #endregion
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序遇到错误，查询失败！");
                return;
            }

        }

        private void FrmQueryRangeResult_FormClosed(object sender, FormClosedEventArgs e)
        {
            //清除定位画的要素
            IGraphicsContainer cont;
            cont = m_pMxdoc.ActivatedView as IGraphicsContainer;
            cont.DeleteAllElements();
        }
    }
}
