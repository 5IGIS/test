using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Geodatabase;
using AOBaseLibC;
using AOBaseLibC.AFCommon;
using AOBaseLibC.AFGeodatabase;
using System.Collections;
using ESRI.ArcGIS.Framework;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using Visifire.Charts;
using Utilitys;
using ESRI.ArcGIS.Geometry;
using JJWATBaseLibC;
using ESRI.ArcGIS.CartoUI;
namespace JJWATQuery
{
    public partial class UtilitysResultForm : Form
    {
        public UtilitysResultForm()
        {
            InitializeComponent();
            // 创建一个ListView排序类的对象，并设置listView1的排序器
            lvwColumnSorter = new ListViewColumnSorter();
            this.lvwQueryResult.ListViewItemSorter = lvwColumnSorter;
        }

        private Entitys pEntitys = new Entitys();
        private Details pDetails = new Details();
        private ListViewColumnSorter lvwColumnSorter;
        int recordNum = 50;     //一页多少条数据
        int pageNum;            //当前页数
        int pFLayerIndex;       //当前图层
        int recordAllNum;       //总记录数
        int pageAllNum;         //总多少页
        TreeNode lastClickNode; //最后一次点击的节点
        IList<IFeature> m_SelFeatures = new List<IFeature>();        //添加选择集所需的Feature集合
        IList<IFeature> m_Features = new List<IFeature>();           //分组Feature集合        
        //地图选择集里的Feature
        Dictionary<string, IList<IFeature>> m_DicSelFeatures = new Dictionary<string, IList<IFeature>>();
        TreeNode m_Node;                                             //向该节点下添加分组的数据
        string m_SqlWhereClause = "";                                //分组后追加的条件
        TreeNode m_ParentNode;                                       //标识分组的父节点
        bool m_IsFirst = true;                                       //标识是否是第一次点击节点查询数据
        IList<string> pValues;                                       //生成图表时查出来的数据集合
        public static bool isClose = false;                          //标识选择字段窗体是否关闭
        IMap pMap;
        int pFeatureCount;
        IField pField;
        IFields pFields;
        IFeature pFeature;
        IMxDocument pMxDoc;
        IApplication m_App;
        IQueryFilter pFilter;
        IQueryFilter pQueryFilter;
        IFeatureCursor pFCursor;
        AFMap objMap = new AFMap();
        bool isWarn = false;
        bool isSel = false;
        IList<IFeatureLayer> pFeatureLayers;
        Dictionary<string, string> objFieldNames = new Dictionary<string, string>();
        string m_QueryResultNode = "图层对象列表";
        //所有分组菜单名集合
        Dictionary<string, string> m_DicGroups = new Dictionary<string, string>();
        bool m_WhereIsNull = false;
        ContextMenuStrip menu;
        ToolStripItem m_SelMenu;
        string[] strLayerNum = null;
        public static UtilitysMgs mgs;
        Dictionary<string, string> dicSubtypes;
        double m_ShapeLen_Value;
        IFeatureClass m_ObjFClass;
        string m_ShapeLen_Field = "SHAPE.LEN";
        bool isLine = false;
        string subtypeName = "";
        bool m_IsClick = false;
        object value;
        Dictionary<string, string> m_Analyses = new Dictionary<string, string>();   //预警分析条件
        string pMaterial, pRepairNum, pFinishDate;
        string[] LineVales;
        string tableName;
        public string pLoginName;
        public static SysParameters m_CurParrmeter = SysParameters.GetInstance();

        private bool m_IsShow;
        /// <summary>
        /// 是否显示窗体
        /// </summary>
        public bool IsShow
        {
            get { return m_IsShow; }
            set { m_IsShow = value; }
        }

        /// <summary>
        /// 查询条件结构集
        /// </summary>
        public struct QueryClass
        {
            /// <summary>
            /// 所有图层
            /// </summary>
            public IList<IFeatureLayer> pFLayers;
            /// <summary>
            /// 条件
            /// </summary>
            public IQueryFilter pFilter;
            /// <summary>
            /// 是否是预警分析查询
            /// </summary>
            public bool isWarnAnalyse;
            /// <summary>
            /// 是否是选择集查询
            /// </summary>
            public bool isSelQuery;
        }

        /// <summary>
        /// 查询结果初始化
        /// </summary>
        /// <param name="pQuery">查询条件</param>
        public void Init(QueryClass pQuery, IApplication app)
        {
            m_App = app;
            pMxDoc = m_App.Document as IMxDocument;
            pMap = pMxDoc.FocusMap;
            objMap.Map = pMap;
            mgs = new UtilitysMgs(m_App);
            pLoginName = mgs.get_LoginUserName();
            isSel = pQuery.isSelQuery;
            isWarn = pQuery.isWarnAnalyse;
            pFeatureLayers = pQuery.pFLayers;
            pQueryFilter = pQuery.pFilter;
            if (pQueryFilter != null)
            {
                if (pQueryFilter.WhereClause.Trim() == "")
                {
                    m_WhereIsNull = true;
                }
            }
            if (isWarn)
            {
                LineVales = XMLConfig.FrmWarnSetLine().Split(',');
                pMaterial = LineVales[0];
                pRepairNum = LineVales[2];
                pFinishDate = LineVales[3];
                SetWarnAnalyse();
            }
            CreateContextStrip();
            OnLoad();
            if (!this.IsDisposed)
            {
                GetXmlStats();
            }
        }

        /// <summary>
        /// 设置预警分析查询条件
        /// </summary>
        public void SetWarnAnalyse()
        {
            try
            {
                string strWhere;
                System.Data.DataTable dt;
                Dictionary<string, string> pDicSubs;
                Dictionary<string, List<string>> pDicMaterial;
                IField pfd = m_ObjFClass.Fields.get_Field(m_ObjFClass.Fields.FindFieldByAliasName(pMaterial));
                pDicMaterial = mgs.GetDomainsByName(pfd);
                for (int i = 0; i < pFeatureLayers.Count; i++)
                {
                    if (pFeatureLayers[i].FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                    {
                        if (pDicMaterial.Count > 0)
                        {
                            foreach (string key in pDicMaterial.Keys)
                            {
                                for (int j = 0; j < pDicMaterial[key].Count; j++)
                                {
                                    dt = SDTBLL.QueryAlarmpara(pDicMaterial[key][j]);
                                    if (dt != null)
                                    {
                                        foreach (DataRow row in dt.Rows)
                                        {
                                            if (m_Analyses.ContainsKey(pFeatureLayers[i].Name))
                                            {
                                                strWhere = ExtractWhere("MATERIAL", row);
                                                if (strWhere == "")
                                                    continue;
                                                if (m_Analyses[pFeatureLayers[i].Name] == "")
                                                {
                                                    m_Analyses[pFeatureLayers[i].Name] += strWhere;
                                                }
                                                else
                                                {
                                                    m_Analyses[pFeatureLayers[i].Name] += " or " + strWhere;
                                                }
                                            }
                                            else
                                            {
                                                m_Analyses.Add(pFeatureLayers[i].Name, ExtractWhere("MATERIAL", row));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        pDicSubs = mgs.GetSubtypes(pFeatureLayers[i]);
                        foreach (string key in pDicSubs.Keys)
                        {
                            dt = SDTBLL.QueryAlarmpara(pDicSubs[key]);
                            if (dt != null)
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    if (m_Analyses.ContainsKey(pFeatureLayers[i].Name))
                                    {
                                        strWhere = ExtractWhere("SUBTYPE", row);
                                        if (strWhere == "")
                                            continue;
                                        m_Analyses[pFeatureLayers[i].Name] += " or " + strWhere;
                                    }
                                    else
                                    {
                                        m_Analyses.Add(pFeatureLayers[i].Name, ExtractWhere("SUBTYPE", row));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 提取条件
        /// </summary>
        /// <returns></returns>
        public string ExtractWhere(string colName, DataRow row)
        {
            string sql = "";
            int pRepairNum = Convert.ToInt32(row["REPAIRNUM"]);
            int pYears = Convert.ToInt32(row["WORKAGEYEAR"]);
            int pMonths = Convert.ToInt32(row["WORKAGEMONTH"]);
            if (pRepairNum == 0 && pYears == 0 && pMonths == 0)
                return "";
            if (colName == "SUBTYPE")
                sql = colName + "=" + row["CODE"];
            else
                sql = colName + "='" + row["CODE"] + "'";
            if (pRepairNum != 0)
                sql += " and REPAIRNUM>=" + pRepairNum;
            if (pYears != 0)
                pMonths += pYears * 12;
            if (pMonths != 0)
            {
                switch (m_CurParrmeter.GWDataSourceType)
                {
                    case AFConst.DataSourceType.AFisSQLServer:
                        sql += " and " + pFinishDate + "< dateadd(MM," + pMonths + ",getdate()) ";
                        break;
                    case AFConst.DataSourceType.AFisAccess:
                        sql = "";
                        break;
                    case AFConst.DataSourceType.AFisOracle:
                        sql += " and " + pFinishDate + "< add_months(sysdate,-'" + pMonths + "')";
                        break;
                }
            }
            return "(" + sql + ")";
        }

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        public void OnLoad()
        {
            try
            {
                IList<string> nodes = new List<string>();
                if (isSel || isWarn)
                {
                    lab_PageInfo.Visible = false;
                    btn_BackPage.Visible = false;
                    btn_FirstPage.Visible = false;
                    btn_LastPage.Visible = false;
                    btn_NextPage.Visible = false;
                    tabControl1.Top -= 20;
                    tabControl1.Height += 20;
                }
                if (isSel)
                {
                    //btn_Selection.Visible = false;
                    QuerySelection();
                    if (!IsShow)
                        return;
                    foreach (var key in m_DicSelFeatures.Keys)
                    {
                        nodes.Add(key + "：" + m_DicSelFeatures[key].Count.ToString());
                    }
                }
                else
                {
                    for (int i = 0; i < pFeatureLayers.Count; i++)
                    {
                        pFeatureCount = 0;
                        if (isWarn)
                        {
                            if (m_Analyses.ContainsKey(pFeatureLayers[i].Name))
                            {
                                pQueryFilter.WhereClause = m_Analyses[pFeatureLayers[i].Name];
                                pFeatureCount += pFeatureLayers[i].FeatureClass.FeatureCount(pQueryFilter);
                            }
                        }
                        else
                        {
                            pFeatureCount = pFeatureLayers[i].FeatureClass.FeatureCount(pQueryFilter);
                        }
                        if (pFeatureCount > 0)
                        {
                            nodes.Add(pFeatureLayers[i].FeatureClass.AliasName + "：" + pFeatureCount.ToString());
                        }
                    }
                }
                if (nodes.Count == 0)
                {
                    IsShow = false;
                    MsgBox.Show("对不起，数据库中没有符合您条件的数据！！！");
                    this.Dispose();
                }
                else
                {
                    AddNodes(nodes);
                    trvQueryResult.Select();
                    trvQueryResult.SelectedNode = trvQueryResult.Nodes[0].FirstNode;
                    AddDataLvwResult(trvQueryResult.SelectedNode, MouseButtons.Left);
                    IsShow = true;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 读取统计字段xml
        /// </summary>
        public void GetXmlStats()
        {
            System.Data.DataTable dt = XMLConfig.GetStatsFields();
            objFieldNames.Clear();
            foreach (DataRow row in dt.Rows)
            {
                objFieldNames.Add(row["aliasName"].ToString(), row["fieldName"].ToString());
            }
        }

        /// <summary>
        /// 添加树节点
        /// </summary>
        /// <param name="strs"></param>
        public void AddNodes(IList<string> strs)
        {
            TreeNode node;
            trvQueryResult.Nodes.Clear();
            TreeNode nodes = new TreeNode();
            nodes.Text = m_QueryResultNode;
            for (int i = 0; i < strs.Count; i++)
            {
                node = new TreeNode();
                node.Text = strs[i];
                nodes.Nodes.Add(node);
            }
            trvQueryResult.Nodes.Add(nodes);
            trvQueryResult.ExpandAll();
        }

        /// <summary>
        /// 查询地图选择集
        /// </summary>
        public void QuerySelection()
        {
            ISelection pSelection;
            IEnumFeature pEnumFeature;
            IFeature pFeature;
            IEnumFeatureSetup pEnumFeatureSetup;
            IList<IFeature> layers;
            string dsName = XMLConfig.GetProName();
            string fdsName = "";
            if (pMap.SelectionCount > 0)
            {
                pSelection = pMap.FeatureSelection;
                pEnumFeatureSetup = (IEnumFeatureSetup)pSelection;
                pEnumFeatureSetup.AllFields = true;
                pEnumFeature = (IEnumFeature)pSelection;
                IFeatureSelection psel = pEnumFeature as IFeatureSelection;
                pFeature = pEnumFeature.Next();
                while (pFeature != null)
                {
                    if (((IFeatureClass)pFeature.Class).FeatureDataset == null)
                    {
                        pFeature = pEnumFeature.Next();
                        continue;
                    }
                    fdsName = ((IFeatureClass)pFeature.Class).FeatureDataset.Name;
                    if (fdsName.LastIndexOf(".") != -1)
                    {
                        fdsName = fdsName.Substring(fdsName.LastIndexOf(".") + 1, fdsName.Length - fdsName.LastIndexOf(".") - 1);
                    }
                    if (fdsName != dsName)
                    {
                        pFeature = pEnumFeature.Next();
                        continue;
                    }
                    if (!m_DicSelFeatures.ContainsKey(pFeature.Class.AliasName))
                    {
                        layers = new List<IFeature>();
                        layers.Add(pFeature);
                        m_DicSelFeatures.Add(pFeature.Class.AliasName, layers);
                    }
                    else
                    {
                        m_DicSelFeatures[pFeature.Class.AliasName].Add(pFeature);
                    }
                    pFeature = pEnumFeature.Next();
                }
                IsShow = true;
            }
            else
            {
                IsShow = false;
                MsgBox.Show("对不起，地图选择集中没有数据！！！");
            }
        }

        /// <summary>
        /// 动态创建右键菜单
        /// </summary>
        public void CreateContextStrip()
        {
            m_DicGroups.Clear();
            menu = new ContextMenuStrip();
            menu.ItemClicked += new ToolStripItemClickedEventHandler(menu_ItemClicked);
            System.Data.DataTable dt = XMLConfig.GetContextMenu();

            foreach (DataRow row in dt.Rows)
            {
                m_SelMenu = new ToolStripMenuItem();
                m_SelMenu.Text = row["aliasName"].ToString();
                m_SelMenu.Name = row["fieldName"].ToString();
                menu.Items.Add(m_SelMenu);
                m_DicGroups.Add(m_SelMenu.Text, m_SelMenu.Name);
            }
        }

        /// <summary>
        /// 选择树分组事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                bool isNull = false;
                m_Node.Nodes.Clear();
                m_SelMenu = e.ClickedItem;
                e.ClickedItem.Enabled = false;
                foreach (ToolStripItem item in menu.Items)
                {
                    if (item.Name != m_SelMenu.Name)
                        item.Enabled = true;
                }
                if (m_SelMenu.Text == "取消分组")
                {
                    if (m_Node.Nodes.Count > 0)
                        m_Node.Nodes.Clear();
                    this.Cursor = Cursors.Default;
                    return;
                }
                AddDataLvwResult(m_Node, MouseButtons.Left);
                var q = from p in m_Features group p by p.get_Value(p.Fields.FindField(e.ClickedItem.Name)) into g select new { g.Key };
                IList<string> strs = new List<string>();
                IList<int> dias = new List<int>();
                foreach (var u in q)
                {
                    if (u.Key.ToString() != "")
                        strs.Add(u.Key.ToString());
                    else
                        isNull = true;
                }
                if ("DIAMETER" == m_SelMenu.Name.ToUpper())
                {
                    foreach (string str in strs)
                    {
                        if (ResultManager.IsNumeric(str))
                            dias.Add(Convert.ToInt32(str));
                    }
                    var query = from s in dias orderby s ascending select s;
                    AddNodeGroup(query.ToList(), isNull);
                }
                else
                {
                    var query = from s in strs orderby s ascending select s;
                    AddNodeGroup(query.ToList(), isNull);
                }
                m_Node.Expand();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 添加材质分组节点
        /// </summary>
        public void AddNodeGroup(IList<string> list, bool isNull)
        {
            TreeNode n;
            for (int i = 0; i < list.Count; i++)
            {
                n = new TreeNode();
                n.Text = list[i];
                n.Tag = m_SelMenu.Name;
                m_Node.Nodes.Add(n);
            }
            if (isNull)
            {
                n = new TreeNode();
                n.Text = "其他";
                n.Tag = m_SelMenu.Name;
                m_Node.Nodes.Add(n);
            }
        }

        /// <summary>
        /// 添加管径分组节点 
        /// </summary>
        public void AddNodeGroup(IList<int> list, bool isNull)
        {
            TreeNode n;
            for (int i = 0; i < list.Count; i++)
            {
                n = new TreeNode();
                n.Text = list[i].ToString();
                n.Tag = m_SelMenu.Name;
                m_Node.Nodes.Add(n);
            }
            if (isNull)
            {
                n = new TreeNode();
                n.Text = "其他";
                n.Tag = m_SelMenu.Name;
                m_Node.Nodes.Add(n);
            }
        }

        /// <summary>
        /// 鼠标按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvQueryResult_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                System.Drawing.Point point = new System.Drawing.Point(e.X, e.Y);
                TreeNode selectedNode = trvQueryResult.GetNodeAt(point);
                trvQueryResult.SelectedNode = selectedNode;
                trvQueryResult.ContextMenuStrip = null;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 分页按钮是否可用
        /// </summary>
        /// <param name="isEnabled"></param>
        public void IsEnabled(bool isEnabled)
        {
            btn_FirstPage.Enabled = isEnabled;
            btn_LastPage.Enabled = isEnabled;
            btn_NextPage.Enabled = isEnabled;
            btn_BackPage.Enabled = isEnabled;
        }

        /// <summary>
        /// 选择树节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvQueryResult_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                TreeNode node = e.Node;
                MouseButtons btn = e.Button;
                AddDataLvwResult(node, e.Button);
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 向lvwQueryResult中添加数据
        /// </summary>
        /// <param name="node"></param>
        /// <param name="btn"></param>
        public void AddDataLvwResult(TreeNode node, MouseButtons btn)
        {
            pageNum = 0;
            IsEnabled(false);
            if (node == null || node.Text == m_QueryResultNode)
            {
                btn_ExportExcel.Enabled = false;
                lastClickNode = node;
                //btn_Selection.Enabled = false;
                return;
            }
            btn_ExportExcel.Enabled = true;
            btn_Selection.Enabled = true;
            if (btn == MouseButtons.Left)
            {
                if (node == lastClickNode)
                {
                    if (pageAllNum > 1)
                    {
                        IsEnabled(true);
                    }
                    return;
                }
                m_Features.Clear();
                m_IsFirst = true;
                m_SelFeatures.Clear();
                lastClickNode = node;
                lvwQueryResult.Items.Clear();
                this.Cursor = Cursors.WaitCursor;
                if (isSel)
                {
                    m_ShapeLen_Value = 0;
                    if (node.Parent.Parent != null)
                    {
                        m_IsFirst = false;
                        strLayerNum = node.Parent.Text.Split('：');
                        if (m_DicSelFeatures.ContainsKey(strLayerNum[0]))
                        {
                            string txt = node.Text == "其他" ? "" : node.Text;
                            var query = m_DicSelFeatures[strLayerNum[0]].Where(a => a.get_Value(a.Fields.FindField(node.Tag.ToString())).ToString() == txt);
                            foreach (IFeature pf in query)
                            {
                                if (pf.Fields.FindField(m_ShapeLen_Field) != -1)
                                    m_ShapeLen_Value += Math.Round(Convert.ToDouble(pf.get_Value(pf.Fields.FindField(m_ShapeLen_Field))), 2);
                                m_Features.Add(pf);
                            }
                            recordAllNum = m_Features.Count;
                            ResultDataBinding(m_Features, lvwQueryResult);
                        }
                    }
                    else
                    {
                        strLayerNum = node.Text.Split('：');
                        if (m_DicSelFeatures.ContainsKey(strLayerNum[0]))
                        {
                            lvwQueryResult.Columns.Clear();
                            m_ParentNode = node;
                            recordAllNum = Convert.ToInt32(strLayerNum[1]);
                            AddColumnHeader(m_DicSelFeatures[strLayerNum[0]][0].Class.Fields, lvwQueryResult);
                            foreach (IFeature pf in m_DicSelFeatures[strLayerNum[0]])
                            {
                                if (pf.Fields.FindField(m_ShapeLen_Field) != -1)
                                    m_ShapeLen_Value += Math.Round(Convert.ToDouble(pf.get_Value(pf.Fields.FindField(m_ShapeLen_Field))), 2);
                                m_Features.Add(pf);
                            }
                            ResultDataBinding(m_Features, lvwQueryResult);
                        }
                    }
                    if (m_Features.Count > 0)
                    {
                        m_ObjFClass = m_Features[0].Class as IFeatureClass;
                    }
                    if (lvwQueryResult.Items.Count > 0)
                    {
                        lvwQueryResult.Select();
                        lvwQueryResult.Items[0].Selected = true;
                        ChooseFeature();
                    }
                }
                else
                {
                    for (int i = 0; i < pFeatureLayers.Count; i++)
                    {
                        m_ObjFClass = pFeatureLayers[i].FeatureClass;
                        if (node.Parent.Parent != null)
                        {
                            strLayerNum = node.Parent.Text.Split('：');
                            if (strLayerNum[0] == m_ObjFClass.AliasName)
                            {
                                m_IsFirst = false;
                                pFLayerIndex = i;
                                if (isWarn)
                                {
                                    recordAllNum = 0;
                                    pQueryFilter.WhereClause = "(" + m_Analyses[m_ObjFClass.AliasName] + ")";
                                    if (m_ParentNode.Text != node.Parent.Text)
                                    {
                                        m_ParentNode = node.Parent;
                                        lvwQueryResult.Columns.Clear();
                                        AddColumnHeader(m_ObjFClass.Fields, lvwQueryResult);
                                        if (m_SqlWhereClause != "")
                                        {
                                            pQueryFilter.WhereClause = m_SqlWhereClause;
                                            m_SqlWhereClause = "";
                                        }
                                    }
                                    SetWhereClause(node);
                                    recordAllNum += m_ObjFClass.FeatureCount(pQueryFilter);
                                    QueryGroups();
                                    QueryWarn(pQueryFilter);
                                    ResultDataBinding(m_SelFeatures, lvwQueryResult);
                                    if (lvwQueryResult.Items.Count > 0)
                                    {
                                        lvwQueryResult.Select();
                                        lvwQueryResult.Items[0].Selected = true;
                                        ChooseFeature();
                                    }
                                }
                                else
                                {
                                    pageNum = 1;
                                    if (m_ParentNode.Text != node.Parent.Text)
                                    {
                                        m_ParentNode = node.Parent;
                                        lvwQueryResult.Columns.Clear();
                                        AddColumnHeader(m_ObjFClass.Fields, lvwQueryResult);
                                        if (m_SqlWhereClause != "")
                                        {
                                            pQueryFilter.WhereClause = m_SqlWhereClause;
                                            m_SqlWhereClause = "";
                                        }
                                    }
                                    if (m_SqlWhereClause == "")
                                    {
                                        m_SqlWhereClause = pQueryFilter.WhereClause;
                                    }
                                    else
                                    {
                                        pQueryFilter.WhereClause = m_SqlWhereClause;
                                    }
                                    if (m_WhereIsNull)
                                        pQueryFilter.WhereClause = "";
                                    SetWhereClause(node);
                                    recordAllNum = m_ObjFClass.FeatureCount(pQueryFilter);
                                    QueryGroups();
                                    QueryByPage();
                                }
                                break;
                            }
                        }
                        else
                        {
                            strLayerNum = node.Text.Split('：');
                            if (strLayerNum[0] == m_ObjFClass.AliasName)
                            {
                                lvwQueryResult.Columns.Clear();
                                pFLayerIndex = i;
                                if (m_SqlWhereClause != "")
                                {
                                    pQueryFilter.WhereClause = m_SqlWhereClause;
                                    m_SqlWhereClause = "";
                                }
                                m_ParentNode = node;
                                recordAllNum = Convert.ToInt32(strLayerNum[1]);
                                if (recordAllNum == 0)
                                {
                                    IsEnabled(false);
                                    pageAllNum = 0;
                                    dgv_QueryResult.DataSource = null;
                                    lab_PageInfo.Text = "第" + 0 + "/" + 0 + "页,共" + 0 + "/" + 0 + "条";
                                    break;
                                }
                                if (isWarn)
                                {
                                    if (!m_Analyses.ContainsKey(m_ObjFClass.AliasName))
                                    {
                                        continue;
                                    }
                                    AddColumnHeader(m_ObjFClass.Fields, lvwQueryResult);
                                    pQueryFilter.WhereClause = m_Analyses[m_ObjFClass.AliasName];
                                    QueryGroups();
                                    QueryWarn(pQueryFilter);
                                    ResultDataBinding(m_SelFeatures, lvwQueryResult);
                                    if (lvwQueryResult.Items.Count > 0)
                                    {
                                        lvwQueryResult.Select();
                                        lvwQueryResult.Items[0].Selected = true;
                                        ChooseFeature();
                                    }
                                    ResultManager.AutoResizeColumnWidth(lvwQueryResult);
                                }
                                else
                                {
                                    AddColumnHeader(m_ObjFClass.Fields, lvwQueryResult);
                                    if (m_WhereIsNull)
                                        pQueryFilter.WhereClause = "";
                                    pageNum = 1;
                                    QueryGroups();
                                    QueryByPage();
                                }
                                break;
                            }
                        }
                    }
                }
                label1.Text = "图层：" + strLayerNum[0];
                if (m_ObjFClass != null && m_ObjFClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    isLine = true;
                    groupBox_Count.Text = "共" + recordAllNum + "条记录，总长度：" + Math.Round(m_ShapeLen_Value, 2) + "米";
                }
                else
                {
                    isLine = false;
                    groupBox_Count.Text = "共" + recordAllNum + "条记录，总个数：" + recordAllNum + "个";
                }
                this.Cursor = Cursors.Default;
            }
            //如果点击的是鼠标右键，弹出右键菜单
            if (btn == MouseButtons.Right)
            {
                m_Node = node;
                if (m_Node.Text.IndexOf("栓点图") != -1)
                {
                    trvQueryResult.ContextMenuStrip = null;
                    return;
                }
                if (node.Parent.Parent != null)
                    return;
                trvQueryResult.ContextMenuStrip = menu;
                if (node != lastClickNode)
                {
                    m_SelMenu.Enabled = true;
                }
            }
            if (pageAllNum > 1)
                IsEnabled(true);
        }

        /// <summary>
        /// 根据登陆用户权限创建属性分页
        /// </summary>
        public void CreateTabPage()
        {
            for (int i = 1; i < tabControl1.TabPages.Count; i++)
            {
                tabControl1.TabPages.RemoveAt(i);
            }
            CDBCon objDBCon = CDBCon.GetInstance();
            System.Data.DataTable tabsdt = SDTBLL.QueryTabsByUser(tableName, objDBCon.LoginUserName());
            if (tabsdt.Rows.Count != 0)
            {
                tabControl1.TabPages[0].Text = tabsdt.Rows[0][0].ToString();//strLayerNum[0]
                for (int i = 1; i < tabsdt.Rows.Count; i++)
                {
                    tabControl1.TabPages.Add(tabsdt.Rows[i][0].ToString());
                }
            }
        }

        /// <summary>
        /// 设置查询条件
        /// </summary>
        public void SetWhereClause(TreeNode node)
        {
            if (node.Text == "其他")
                pQueryFilter.WhereClause += SetWhereClause(pQueryFilter.WhereClause) + node.Tag + " is null";
            else
                pQueryFilter.WhereClause += SetWhereClause(pQueryFilter.WhereClause) + node.Tag + "='" + trvQueryResult.SelectedNode.Text + "'";
        }

        /// <summary>
        /// 设置联合查询条件串
        /// </summary>
        /// <param name="Clause"></param>
        /// <returns></returns>
        public string SetWhereClause(string Clause)
        {
            if (Clause.Trim() == "")
                return "";
            else
                return " and ";
        }

        /// <summary>
        /// 添加列头
        /// </summary>
        public void AddColumnHeader(IFields pFields, ListView listview)
        {
            ResultManager.CreateColumnHeader("序号", listview);
            ResultManager.CreateColumnHeader("OBJECTID", listview);
            tableName = objMap.GetLayerByName(strLayerNum[0]).LayerTableName;
            System.Data.DataTable dt = SDTBLL.QueryAllFields(tableName, pLoginName);
            for (int j = 0; j < pFields.FieldCount; j++)
            {
                pField = pFields.get_Field(j);
                if (pField.Name.ToUpper() == "OBJECTID" || pField.Name.ToUpper() == "SHAPE")
                {
                    continue;
                }
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    if (dt.Rows[k]["FIELDNAME"].ToString() == pField.Name)
                    {
                        ResultManager.CreateColumnHeader(dt.Rows[k]["ALIASNAME"].ToString(), listview);
                    }
                }
            }
            if (cmb_StatSort.Items.Count > 0)
            {
                cmb_StatSort.SelectedIndex = 0;
                cmb_IconStyle.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="i">标识是哪个图层</param>
        /// <param name="pageNum">第几页</param>
        /// <param name="recordAllNum">总记录数</param>
        public void QueryByPage()
        {
            m_SelFeatures.Clear();
            lvwQueryResult.Items.Clear();
            int index = 0;
            int n = pageNum * recordNum;
            pFCursor = pFeatureLayers[pFLayerIndex].Search(pQueryFilter, false);
            pFeature = pFCursor.NextFeature();
            while (pFeature != null)
            {
                index++;
                if (index > n)
                {
                    break;
                }
                if (pageNum == 1 || index > n - recordNum)
                {
                    m_SelFeatures.Add(pFeature);
                }
                pFeature = pFCursor.NextFeature();
            }
            ResultDataBinding(m_SelFeatures, lvwQueryResult);
            if (lvwQueryResult.Items.Count > 0)
            {
                lvwQueryResult.Select();
                lvwQueryResult.Items[0].Selected = true;
                ChooseFeature();
            }
            pageAllNum = recordAllNum % recordNum == 0 ? recordAllNum / recordNum : recordAllNum / recordNum + 1;
            lab_PageInfo.Text = "第" + pageNum + "/" + pageAllNum + "页,共" + lvwQueryResult.Items.Count + "/" + recordAllNum + "条";
        }

        /// <summary>
        /// 查询分组数据及管线总长度
        /// </summary>
        public void QueryGroups()
        {
            m_ShapeLen_Value = 0;
            string subField = "";
            foreach (var key in m_DicGroups.Keys)
            {
                subField += "," + m_DicGroups[key];
            }
            subField = subField.Remove(0, 1);
            subField = subField.Substring(0, subField.LastIndexOf(","));
            if (m_ObjFClass.ShapeType == esriGeometryType.esriGeometryPolyline)
            {
                if (m_ObjFClass.FindField(m_ShapeLen_Field) != -1)
                    subField += "," + m_ShapeLen_Field;
            }
            pQueryFilter.SubFields = subField;
            pFCursor = pFeatureLayers[pFLayerIndex].Search(pQueryFilter, false);
            pFeature = pFCursor.NextFeature();
            while (pFeature != null)
            {
                if (m_ObjFClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    if (pFeature.Fields.FindField(m_ShapeLen_Field) != -1)
                    {
                        try
                        {
                            m_ShapeLen_Value += Convert.ToDouble(pFeature.get_Value(pFeature.Fields.FindField(m_ShapeLen_Field)));
                        }
                        catch (Exception ex)
                        {
                            MsgBox.Show("管线长度为空！" + ex.ToString());
                        }
                    }
                }
                m_Features.Add(pFeature);
                pFeature = pFCursor.NextFeature();
            }
            pQueryFilter.SubFields = "";
        }

        /// <summary>
        /// 列表绑定数据
        /// </summary>
        public void ResultDataBinding(IList<IFeature> pFeatures, ListView listview)
        {
            IFeatureClass pFeatClass = null;
            ListViewItem pViewItem;
            if (pFeatures.Count > 0)
            {
                pFeatClass = pFeatures[0].Class as IFeatureClass;
                subtypeName = ((ISubtypes)pFeatClass).SubtypeFieldName;
                subtypeName = pFeatClass.Fields.get_Field(pFeatClass.FindField(subtypeName)).AliasName;
                dicSubtypes = mgs.GetSubtypes(pFeatClass);
            }
            tableName = GetTableName(pFeatures[0]);
            for (int i = 0; i < pFeatures.Count; i++)
            {
                pFields = pFeatures[i].Fields;
                pViewItem = new ListViewItem();
                pViewItem.Text = (listview.Items.Count + 1).ToString();
                pViewItem.SubItems.Add(pFeatures[i].get_Value(pFeatClass.FindField("OBJECTID")).ToString());
                for (int j = 2; j < lvwQueryResult.Columns.Count; j++)
                {
                    if (pFeatures[i].Fields.FindFieldByAliasName(lvwQueryResult.Columns[j].Name) == -1)
                    {
                        MsgBox.Show(pFeatClass.AliasName + "没有" + lvwQueryResult.Columns[j].Name + "字段！");
                        return;
                    }
                    value = pFeatures[i].get_Value(pFeatures[i].Fields.FindFieldByAliasName(lvwQueryResult.Columns[j].Name));
                    if (lvwQueryResult.Columns[j].Name == subtypeName)
                    {
                        if (dicSubtypes.ContainsKey(value.ToString().Trim()))
                        {
                            pViewItem.SubItems.Add(dicSubtypes[value.ToString().Trim()]);
                            continue;
                        }
                    }
                    pViewItem.SubItems.Add(value == null ? "" : value.ToString().Trim());
                }
                listview.Items.Add(pViewItem);
            }
            if (m_IsFirst)
            {
                ResultManager.AutoResizeColumnWidth(listview);
            }
        }

        /// <summary>
        /// 查询预警
        /// </summary>
        public void QueryWarn(IQueryFilter pQFilter)
        {
            pFCursor = pFeatureLayers[pFLayerIndex].Search(pQFilter, false);
            pFeature = pFCursor.NextFeature();
            while (pFeature != null)
            {
                m_SelFeatures.Add(pFeature);
                pFeature = pFCursor.NextFeature();
            }
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_FirstPage_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (pageNum == 1)
                {
                    this.Cursor = Cursors.Default;
                    MsgBox.Show("已经是第一页了！！！");
                    return;
                }
                m_IsFirst = false;
                pageNum = 1;
                QueryByPage();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 尾页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_LastPage_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (pageNum == pageAllNum)
                {
                    this.Cursor = Cursors.Default;
                    MsgBox.Show("已经是最后一页了！！！");
                    return;
                }
                m_IsFirst = false;
                pageNum = pageAllNum;
                QueryByPage();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_BackPage_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (pageNum == 1)
                {
                    this.Cursor = Cursors.Default;
                    MsgBox.Show("已经是第一页了！！！");
                    return;
                }
                m_IsFirst = false;
                pageNum--;
                QueryByPage();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_NextPage_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (pageNum == pageAllNum)
                {
                    this.Cursor = Cursors.Default;
                    MsgBox.Show("已经是最后一页了！！！");
                    return;
                }
                m_IsFirst = false;
                pageNum++;
                QueryByPage();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString());
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 绑定属性显示基本信息
        /// </summary>
        /// <param name="rs"></param>
        /// <param name="objFeature"></param>
        public void DataSource(DataGridView dgv)
        {
            try
            {
                pDetails = new Details();
                IField pField;
                DetailInfo d;
                IFields pFields = pFeature.Class.Fields;
                tableName = GetTableName(null);
                System.Data.DataTable dt = SDTBLL.QueryFieldsByTabs(tableName, pLoginName, tabControl1.SelectedTab.Text);
                for (int j = 0; j < pFields.FieldCount; j++)
                {
                    pField = pFields.get_Field(j);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["FIELDNAME"].ToString() == pField.Name)
                        {
                            value = pFeature.get_Value(j);
                            if (value == null)
                                continue;
                            d = new DetailInfo();
                            d.AttributeName = pField.AliasName;
                            if (pField.AliasName == subtypeName)
                            {
                                if (dicSubtypes.ContainsKey(value.ToString().Trim()))
                                {
                                    d.AttributeValue = dicSubtypes[value.ToString().Trim()];
                                }
                            }
                            else
                            {
                                d.AttributeValue = value.ToString().Trim();
                            }
                            pDetails.Add(d);
                        }
                    }
                }
                dgv.DataSource = pDetails;
                dgv.Refresh();
                dgv.Columns[0].DefaultCellStyle.BackColor = dgv.ColumnHeadersDefaultCellStyle.BackColor;
                for (int i = 2; i < dgv.Columns.Count; i++)
                {
                    dgv.Columns[i].Visible = false;
                }
                dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <returns></returns>
        public string GetTableName(IFeature objFeature)
        {
            AFFeatureLayer objLayer = null;
            if (isSel)
            {
                if (objFeature != null)
                    pFeature = objFeature;
                objLayer = objMap.GetLayerByName(pFeature.Class.AliasName);
            }
            else
            {
                objLayer = new AFFeatureLayer(pFeatureLayers[pFLayerIndex]);
            }
            return objLayer.LayerTableName;
        }

        /// <summary>
        /// 选择单条记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvwQueryResult_Click(object sender, EventArgs e)
        {
            m_IsClick = true;
            ChooseFeature();
        }

        /// <summary>
        /// 选择单条记录 查看详细信息
        /// </summary>
        public void ChooseFeature()
        {
            if (lvwQueryResult.Items.Count != 0)
            {
                string objID = lvwQueryResult.SelectedItems[0].SubItems[lvwQueryResult.Columns.IndexOfKey("OBJECTID")].Text;
                if (lvwQueryResult.SelectedItems.Count == 0)
                {
                    pFeature = GetFeatureByOid(lvwQueryResult.Items[0].SubItems[lvwQueryResult.Columns.IndexOfKey("OBJECTID")].Text);
                }
                else
                {
                    if (isSel)
                    {
                        var query = m_DicSelFeatures[strLayerNum[0]].Where(a => a.get_Value(0).ToString() == objID);
                        foreach (IFeature pf in query)
                        {
                            pFeature = pf;
                        }
                    }
                    else
                        pFeature = GetFeatureByOid(objID);
                }
                if (pFeature != null)
                {
                    if (!m_IsClick)
                    {
                        tableName = GetTableName(null);
                        CreateTabPage();
                    }
                    m_IsClick = false;
                    DataSource(dgv_QueryResult);
                    btn_GraphicPosition.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 根据图层别名,OBJECTID查询
        /// </summary>
        /// <param name="layerName"></param>
        /// <param name="OID"></param>
        /// <returns></returns>
        public IFeature GetFeatureByOid(string OID)
        {
            pFilter = new QueryFilterClass();
            pFilter.WhereClause = "OBJECTID=" + OID;
            pFCursor = pFeatureLayers[pFLayerIndex].Search(pFilter, true);
            pFeature = pFCursor.NextFeature();
            return pFeature;
        }

        /// <summary>
        /// 查询结果定位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_GraphicPosition_Click(object sender, EventArgs e)
        {
            try
            {
                if (isSel)
                {
                    PositionGraph(null);
                }
                else
                {
                    PositionGraph(pFeatureLayers[pFLayerIndex]);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 定位图形
        /// </summary>
        /// <param name="pFLayer"></param>
        /// <param name="pFeature"></param>
        public void PositionGraph(IFeatureLayer pFLayer)
        {
            AFFlash objFlash = new AFFlash(m_App);
            //AFGraphOrientation objGraphOri = new AFGraphOrientation(m_App);
            AFCollection objLayers = objMap.GetLayers(AFConst.AFLayterType.AFGeoFeatureLayer);
            //objGraphOri.OrientFeature(pFeature);
            OrientFeature(pFeature);
            //objFlash.Features = pFeature;
            //objFlash.StartFlash();
            //objMap.RefreshMap();
            t1.Interval = 1000;
            t1.Tick += new EventHandler(t1_Tick);
            FlashShape(pFeature);
        }
        public void OrientFeature(IFeature pFeature)
        {
            IEnvelope pEnv;
            IGeometry pGeometry;

            pEnv = new EnvelopeClass();

            if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                pGeometry = pFeature.Shape;
                pEnv = pGeometry.Envelope;

                if (pGeometry.SpatialReference is IGeographicCoordinateSystem)
                {
                    pEnv.Height = 0.0005;
                    pEnv.Width = 0.0005;
                }
                else
                {
                    pEnv.Height = 20;
                    pEnv.Width = 20;
                }
            }
            else
            {
                pEnv = pFeature.Shape.Envelope; // .Extent;
            }
            //.Expand(1.5, 1.5, true);
            pEnv.XMin = pEnv.XMin - 2;
            pEnv.XMax = pEnv.XMax + 2;
            pEnv.YMin = pEnv.YMin - 2;
            pEnv.YMax = pEnv.YMax + 2;
            if (pEnv != null)
            {
                OrientGeometry(pEnv);
            }
        }
        /// <summary>
        /// 定位指定图形到当前窗口
        /// </summary>
        /// <param name="pEnvelope">需要定位的图形</param>
        public void OrientGeometry(IEnvelope pEnvelope)
        {
            IActiveView pActiveView;

            pActiveView = objMap.Map as IActiveView;
            pActiveView.Extent = pEnvelope;
            pActiveView.Refresh();
        }
        void t1_Tick(object sender, EventArgs e)
        {
            m_FlashNum++;
            identifyObj.Flash(ClsFrmShow.m_MxDoc.ActiveView.ScreenDisplay);
            if (m_FlashNum > 4)
            {
                t1.Enabled = false;
            }
        }

        IIdentifyObj identifyObj;   //闪烁
        int m_FlashNum;             //闪烁次数
        Timer t1 = new Timer();

        /// <summary>
        /// 图形闪烁
        /// </summary>
        private void FlashShape(IFeature pf)
        {
            IFeatureIdentifyObj featureIdentifyObj = new FeatureIdentifyObjectClass();
            featureIdentifyObj.Feature = pf;
            identifyObj = featureIdentifyObj as IIdentifyObj;
            m_FlashNum = 0;
            t1.Enabled = true;
        }

        /// <summary>
        /// 生成选择集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Selection_Click(object sender, EventArgs e)
        {
            try
            {
                int pSFCount = 0;
                string layerName;
                AFFeatureLayer objFLayer;
                this.Cursor = Cursors.WaitCursor;
                ISelectionEvents pSelectionEvents;
                pSelectionEvents = objMap.Map as ISelectionEvents;
                //AFFlash objFlash = new AFFlash(m_App);
                //AFGraphOrientation objGraphOri = new AFGraphOrientation(m_App);
                AFCollection objLayers = objMap.GetLayers(AFConst.AFLayterType.AFGeoFeatureLayer);
                //if (lastClickNode.Parent == null)
                //{
                SelectLayerForm frm = new SelectLayerForm(trvQueryResult.Nodes[0]);
                frm.ShowDialog();
                if (isClose)
                {
                    this.Cursor = Cursors.Default;
                    return;
                }
                foreach (DictionaryEntry item in objLayers)
                {
                    objFLayer = item.Value as AFFeatureLayer;
                    ResultManager.pFeatureSel = objFLayer.FeatureLayer as IFeatureSelection;
                    ResultManager.pFeatureSel.Clear();
                }
                if (isSel)
                {
                    for (int i = 0; i < frm.checkLayers.CheckedItems.Count; i++)
                    {
                        layerName = frm.checkLayers.CheckedItems[i].ToString();
                        objFLayer = objMap.GetLayerByName(layerName);
                        ResultManager.pFeatureSel = objFLayer.FeatureLayer as IFeatureSelection;
                        foreach (IFeature pf in m_DicSelFeatures[layerName])
                        {
                            ResultManager.pFeatureSel.Add(pf);
                        }
                    }
                    //ResultManager.pFeatureSel.SelectionChanged();
                    pSelectionEvents.SelectionChanged();
                }
                else
                {
                    for (int i = 0; i < frm.checkLayers.CheckedItems.Count; i++)
                    {
                        for (int j = 0; j < pFeatureLayers.Count; j++)
                        {
                            if (pFeatureLayers[j].Name == frm.checkLayers.CheckedItems[i].ToString())
                            {
                                ResultManager.pFeatureSel = pFeatureLayers[j] as IFeatureSelection;
                                pSFCount = m_ObjFClass.FeatureCount(pQueryFilter);
                                //if (pSFCount > 30000)
                                //{
                                //    if (!MsgBox.Show_Q("图层'" + strLayerNum[0] + "'共查出" + pSFCount + "条记录，添加选择集可能需要几分钟时间，是否继续？"))
                                //    {
                                //        this.Cursor = Cursors.Default;
                                //        return;
                                //    }
                                //}
                                ResultManager.pFeatureSel.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, false);
                                break;
                            }
                        }
                    }
                    //ResultManager.pFeatureSel.SelectionChanged();
                    pSelectionEvents.SelectionChanged();
                }
                //}
                //else
                //{
                //foreach (DictionaryEntry item in objLayers)
                //{
                //    objFLayer = item.Value as AFFeatureLayer;
                //    ResultManager.pFeatureSel = objFLayer.FeatureLayer as IFeatureSelection;
                //    ResultManager.pFeatureSel.Clear();
                //}
                //if (isSel)
                //{
                //    if (lastClickNode.Parent.Parent != null)
                //    {
                //        strLayerNum = lastClickNode.Parent.Text.Split('：');
                //    }
                //    else
                //    {
                //        strLayerNum = lastClickNode.Text.Split('：');
                //    }
                //    objFLayer = objMap.GetLayerByName(strLayerNum[0]);
                //    ResultManager.pFeatureSel = objFLayer.FeatureLayer as IFeatureSelection;
                //    if (m_Features.Count > 100)
                //    {
                //        if (!MsgBox.Show_Q("图层'" + objFLayer.FeatureLayer.Name + "'共查出" + m_Features.Count + "条记录，添加选择集可能需要几分钟时间，是否继续？"))
                //        {
                //            this.Cursor = Cursors.Default;
                //            return;
                //        }
                //    }
                //    for (int i = 0; i < m_Features.Count; i++)
                //    {
                //        ResultManager.pFeatureSel.Add(m_Features[i]);
                //    }
                //}
                //else
                //{
                //    ResultManager.pFeatureSel = pFeatureLayers[pFLayerIndex] as IFeatureSelection;
                //    pSFCount = m_ObjFClass.FeatureCount(pQueryFilter);
                //    if (pSFCount > 30000)
                //    {
                //        if (!MsgBox.Show_Q("图层'" + strLayerNum[0] + "'共查出" + pSFCount + "条记录，添加选择集可能需要几分钟时间，是否继续？"))
                //        {
                //            this.Cursor = Cursors.Default;
                //            return;
                //        }
                //    }
                //    ResultManager.pFeatureSel.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, false);
                //}
                //}
                //ISelectionSet set = pFeatureLayers[pFLayerIndex].FeatureClass.Select(pQueryFilter, esriSelectionType.esriSelectionTypeHybrid, esriSelectionOption.esriSelectionOptionNormal, pFeatureLayers[pFLayerIndex].FeatureClass.FeatureDataset.Workspace);                
                //pFCursor = pFeatureLayers[pFLayerIndex].FeatureClass.Search(pQueryFilter, false);
                //pFeature = pFCursor.NextFeature();
                //while (pFeature != null)
                //{
                //    ResultManager.pFeatureSel.Add(pFeature);
                //    pFeature = pFCursor.NextFeature();
                //}
                //objGraphOri.OrientFeatures(m_Features);
                //objFlash.Features = m_Features;
                //objFlash.StartFlash();                
                objMap.RefreshMap();
                this.Cursor = Cursors.Default;
                //MsgBox.Show("添加选择集成功！！！");
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MsgBox.Show_Q(ex.ToString());
            }
        }

        /// <summary>
        /// lvwQueryResult排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvwQueryResult_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // 检查点击的列是不是现在的排序列.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // 重新设置此列的排序方法.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // 设置排序列，默认为正向排序
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }
            // 用新的排序方法对ListView排序
            this.lvwQueryResult.Sort();
        }

        /// <summary>
        /// 查询结果转出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (isSel)
                {
                    ListViewToExcel(recordAllNum, pageAllNum, lvwQueryResult, pFeature.Class as IFeatureClass);
                }
                else
                {
                    ListViewToExcel(recordAllNum, pageAllNum, lvwQueryResult, m_ObjFClass);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// ListView转出Excel
        /// </summary>
        /// <param name="aliasName"></param>
        /// <param name="allNum"></param>
        public void ListViewToExcel(int allNum, int pageCount, ListView listview, IFeatureClass pFClass)
        {
            UtilitysFields frm = new UtilitysFields(pFClass);
            frm.tableName = GetTableName(null);
            frm.ShowDialog();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Execl files(*.xlsx)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "导出Excel文件到";
            DateTime now = DateTime.Now;
            saveFileDialog.FileName = now.Year.ToString().PadLeft(2) + now.Month.ToString().PadLeft(2, '0') + now.Day.ToString().PadLeft(2, '0') + "-" +
                now.Hour.ToString().PadLeft(2, '0') + now.Minute.ToString().PadLeft(2, '0') + now.Second.ToString().PadLeft(2, '0');
            if (!isClose && saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                frm.Close();
                if (allNum > 2000)
                {
                    if (!MsgBox.Show_Q("图层'" + pFClass.AliasName + "'共查出" + allNum + "条记录，转出Excel可能需要几分钟时间，是否继续？"))
                    {
                        return;
                    }
                }
                this.Cursor = Cursors.WaitCursor;
                ExportExcel(frm, saveFileDialog.FileName, pageCount, listview);
                this.Cursor = Cursors.Default;
                MsgBox.Show("转出Excel成功！！！");
            }
            isClose = false;
        }

        /// <summary>
        /// 转出Excel
        /// </summary>
        public void ExportExcel(UtilitysFields frm, string pFileName, int pageCount, ListView listview)
        {
            string strTitle = "";
            string strValue = "";
            object missing = Missing.Value;
            Microsoft.Office.Interop.Excel.Application m_objExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbooks m_objWorkBooks = m_objExcel.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook m_objBook = m_objWorkBooks.Add(true);
            Microsoft.Office.Interop.Excel.Sheets m_objWorkSheets = m_objBook.Sheets; ;
            Microsoft.Office.Interop.Excel.Worksheet m_objWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)m_objWorkSheets[1];
            //标题列
            int index = frm.checkFileds.CheckedItems.Count % 2 == 0 ? frm.checkFileds.CheckedItems.Count / 2 : Convert.ToInt32(frm.checkFileds.CheckedItems.Count / 2) + 1;
            if (lastClickNode.Parent.Parent != null)
            {
                strTitle = lastClickNode.Parent.Text.Substring(0, lastClickNode.Parent.Text.IndexOf("："));
            }
            else
            {
                strTitle = lastClickNode.Text.Substring(0, lastClickNode.Text.IndexOf("："));
            }
            m_objExcel.Cells[1, index] = strTitle + "统计";
            //字段名
            for (int i = 0; i < frm.checkFileds.CheckedItems.Count; i++)
            {
                m_objExcel.Cells[2, i + 1] = frm.checkFileds.CheckedItems[i];
            }
            //写内容
            int rowIndex = 0;
            if (pageCount > 1)
            {
                pFCursor = pFeatureLayers[pFLayerIndex].Search(pQueryFilter, false);
                pFeature = pFCursor.NextFeature();
                while (pFeature != null)
                {
                    for (int i = 0; i < frm.checkFileds.CheckedItems.Count; i++)
                    {
                        strValue = pFeature.get_Value(pFeature.Fields.FindFieldByAliasName(frm.checkFileds.CheckedItems[i].ToString())).ToString();
                        m_objExcel.Cells[rowIndex + 3, i + 1] = strValue;
                    }
                    rowIndex++;
                    pFeature = pFCursor.NextFeature();
                }
            }
            else
            {
                for (int i = 0; i < listview.Items.Count; i++)
                {
                    for (int j = 0; j < frm.checkFileds.CheckedItems.Count; j++)
                    {
                        strValue = listview.Items[i].SubItems[listview.Columns.IndexOfKey(frm.checkFileds.CheckedItems[j].ToString())].Text;
                        m_objExcel.Cells[i + 3, j + 1] = strValue;
                    }
                }
                rowIndex = listview.Items.Count;
            }
            m_objExcel.Cells[4 + rowIndex, frm.checkFileds.CheckedItems.Count] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            Microsoft.Office.Interop.Excel.Range rangeTitle = m_objWorkSheet.get_Range(m_objExcel.Cells[1, index], m_objExcel.Cells[1, index]);
            rangeTitle.Font.Size = 16;
            rangeTitle.Font.Bold = true;
            //合并单元格
            rangeTitle.Merge(rangeTitle.MergeCells);
            m_objWorkSheet.get_Range("A1", m_objExcel.Cells[1, frm.checkFileds.CheckedItems.Count]).Merge(m_objWorkSheet.get_Range("A1", m_objExcel.Cells[1, frm.checkFileds.CheckedItems.Count]).MergeCells);
            Microsoft.Office.Interop.Excel.Range rangeContent = m_objWorkSheet.get_Range(m_objExcel.Cells[1, 1], m_objExcel.Cells[3 + listview.Items.Count, frm.checkFileds.CheckedItems.Count]);
            //居中对齐
            rangeContent.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            //获取单元格
            rangeContent = m_objWorkSheet.get_Range(m_objExcel.Cells[2, 1], m_objExcel.Cells[3 + rowIndex, frm.checkFileds.CheckedItems.Count]);
            //不提示是否覆盖,保存
            m_objExcel.DisplayAlerts = false;
            m_objExcel.AlertBeforeOverwriting = false;
            //自动调整列宽
            rangeContent.EntireColumn.AutoFit();
            //rangeContent.ColumnWidth = 13;
            //填充颜色为淡紫色
            //rangeContent.Interior.ColorIndex = 39;
            //rangeContent.Select();
            //m_objExcel.ActiveWindow.FreezePanes = true;
            //单元格边框线类型(线型,虚线型)
            rangeContent.Borders.LineStyle = 1;
            //rangeContent.Borders.get_Item(XlBordersIndex.xlEdgeTop).LineStyle = XlLineStyle.xlContinuous;
            //指定单元格下边框线粗细,和色彩
            //rangeContent.Borders.get_Item(XlBordersIndex.xlEdgeBottom).Weight = XlBorderWeight.xlMedium;
            //给单元格加边框
            rangeContent.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThick, XlColorIndex.xlColorIndexAutomatic, System.Drawing.Color.Black.ToArgb());
            m_objBook.SaveAs(pFileName, missing, missing, missing, missing, missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    missing, missing, missing, missing, missing);
            m_objBook.Close(missing, missing, missing);
            m_objExcel.Workbooks.Close();
            m_objWorkBooks.Close();
            m_objExcel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(m_objBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(m_objExcel);
            m_objBook = null;
            m_objExcel = null;
            GC.Collect();
        }

        /// <summary>
        /// 生成图表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SetReportMgs_Click(object sender, EventArgs e)
        {
            try
            {
                if (isSel)
                {
                    CreateReportMgs(strLayerNum[0], recordAllNum, null, lvwQueryResult);
                }
                else
                {
                    CreateReportMgs(m_ObjFClass.AliasName, recordAllNum, m_ObjFClass, lvwQueryResult);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 创建图表
        /// </summary>
        public void CreateReportMgs(string aliasName, int recordCount, IFeatureClass pFeaClass, ListView listview)
        {
            pValues = new List<string>();
            IList<IFeature> reportFeatures = new List<IFeature>();
            if (recordCount > 50000)
            {
                if (!MsgBox.Show_Q("图层'" + aliasName + "'共查出" + recordCount + "条记录，生成图表可能需要几分钟时间，是否继续？"))
                {
                    return;
                }
            }
            this.Cursor = Cursors.WaitCursor;
            if (pageAllNum > 1)
            {
                string subFields = "";
                if (isLine)
                {
                    subFields = m_ShapeLen_Field + "," + objFieldNames[cmb_StatSort.Text];
                }
                else
                {
                    if (cmb_StatSort.Text == "中压管径")
                    {
                        subFields = objFieldNames["管径"];
                    }
                    else if (cmb_StatSort.Text == "中压材质")
                    {
                        subFields = objFieldNames["材质"];
                    }
                    else
                    {
                        subFields = objFieldNames[cmb_StatSort.Text];
                    }
                }
                pQueryFilter.SubFields = subFields;
                pFCursor = pFeaClass.Search(pQueryFilter, false);
                pFeature = pFCursor.NextFeature();
                object obj;
                while (pFeature != null)
                {
                    if (isLine)
                    {
                        reportFeatures.Add(pFeature);
                    }
                    else
                    {
                        obj = pFeature.get_Value(pFeature.Fields.FindFieldByAliasName(cmb_StatSort.Text));
                        if (objFieldNames[cmb_StatSort.Text].ToUpper() == ((ISubtypes)pFeature.Class).SubtypeFieldName)
                        {
                            dicSubtypes = mgs.GetSubtypes(pFeaClass);
                            if (dicSubtypes.ContainsKey(obj.ToString()))
                                pValues.Add(dicSubtypes[obj.ToString()]);
                        }
                        else
                        {
                            if (obj != null)
                                pValues.Add(obj.ToString());
                        }

                    }
                    pFeature = pFCursor.NextFeature();
                }
            }
            else
            {
                foreach (ListViewItem itm in listview.Items)
                {
                    if (itm.SubItems[listview.Columns.IndexOfKey(cmb_StatSort.Text)] != null)
                    {
                        if (isLine)
                        {
                            if (isSel)
                                reportFeatures = m_Features;
                            else
                                reportFeatures = m_SelFeatures;
                            break;
                        }
                        else
                        {
                            pValues.Add(itm.SubItems[listview.Columns.IndexOfKey(cmb_StatSort.Text)].Text);
                        }
                    }
                }
            }
            pEntitys.Clear();
            ResultEntity rc;
            if (isLine)
            {
                var queryLine = from p in reportFeatures group p by p.get_Value(p.Fields.FindFieldByAliasName(cmb_StatSort.Text)) into g select new { g.Key, Num = g.Sum(a => Convert.ToDouble(a.get_Value(a.Fields.FindField(m_ShapeLen_Field)))) };
                foreach (var o in queryLine)
                {
                    rc = new ResultEntity();
                    rc.Count = Math.Round(o.Num, 2);
                    rc.Name = o.Key.ToString();
                    pEntitys.Add(rc);
                }
            }
            else
            {
                var queryPoint = from p in pValues group p by p into g select new { g.Key, Num = g.Count() };
                foreach (var o in queryPoint)
                {
                    rc = new ResultEntity();
                    rc.Count = o.Num;
                    rc.Name = o.Key;
                    pEntitys.Add(rc);
                }
            }
            DataSeries ds = new DataSeries();
            if (cmb_IconStyle.SelectedIndex == 0)
            {
                ds.RenderAs = RenderAs.Pie;
            }
            else
            {
                ds.RenderAs = RenderAs.Column;
            }
            DataPoint dp;
            foreach (ResultEntity ety in pEntitys)
            {
                dp = new DataPoint();
                dp.YValue = ety.Count;
                dp.AxisXLabel = ety.Name == "" ? "其他" : ety.Name;
                dp.LabelText = ety.Name == "" ? "其他" : ety.Name;
                if (isLine && cmb_StatSort.Text == "类型")
                {
                    dp.LabelText = strLayerNum[0];
                    dp.AxisXLabel = strLayerNum[0];
                }
                dp.ToolTipText = dp.LabelText + "：" + ety.Count;
                dp.ToolTipText = isLine ? dp.ToolTipText + "米" : dp.ToolTipText + "个";
                //if (pEntitys.Count < 10)
                //{
                //    dp.AxisXLabel = ety.Name;
                //}
                ds.DataPoints.Add(dp);
            }
            if (SelReportControl.rePort != null)
            {
                SelReportControl.rePort.Series.Clear();
                SelReportControl.rePort.Series.Add(ds);
            }
            if (!isSel)
                pQueryFilter.SubFields = "";
            pValues.Clear();
            this.Cursor = Cursors.Default;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabControl2_Selected(object sender, TabControlEventArgs e)
        {
            cmb_StatSort.Items.Clear();
            IField pField;
            if (e.TabPageIndex == 1)
            {
                if (m_ObjFClass != null)
                {
                    for (int i = 0; i < m_ObjFClass.Fields.FieldCount; i++)
                    {
                        pField = m_ObjFClass.Fields.get_Field(i);
                        foreach (string key in objFieldNames.Keys)
                        {
                            if (pField.Name == objFieldNames[key])
                            {
                                //if (pField.AliasName == "中压管径")
                                //{
                                //    cmb_StatSort.Items.Add("管径");
                                //}
                                //else if (pField.AliasName == "中压材质")
                                //{
                                //    cmb_StatSort.Items.Add("材质");
                                //}
                                //else
                                //{
                                cmb_StatSort.Items.Add(pField.AliasName);
                                //}
                            }
                        }
                    }
                }
            }
            btn_SetReportMgs.Enabled = false;
            if (cmb_StatSort.Items.Count > 0)
            {
                cmb_StatSort.SelectedIndex = 0;
                btn_SetReportMgs.Enabled = true;
            }
            if (cmb_IconStyle.Items.Count > 0)
                cmb_IconStyle.SelectedIndex = 0;
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            DataSource(dgv_QueryResult);
            e.TabPage.Controls.Add(dgv_QueryResult);
        }

        /// <summary>
        /// FeatureClass转FeatureLayer
        /// </summary>
        /// <param name="pfc"></param>
        /// <returns></returns>
        public IFeatureLayer OpenAnnotationLayer(IFeatureClass pfc)
        {
            IFDOGraphicsLayerFactory pfdof = new FDOGraphicsLayerFactoryClass();
            IFeatureDataset pFDS = pfc.FeatureDataset;
            IWorkspace pWS = pFDS.Workspace;
            IFeatureWorkspace pFWS = pWS as IFeatureWorkspace;
            ILayer pLayer = pfdof.OpenGraphicsLayer(pFWS, pFDS, (pfc as IDataset).Name);
            return pLayer as IFeatureLayer;
        }

        private void lvwQueryResult_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (isSel)
                {
                    PositionGraph(null);
                }
                else
                {
                    PositionGraph(pFeatureLayers[pFLayerIndex]);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString());
            }
        }
    }
}