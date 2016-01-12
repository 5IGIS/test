using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using Utilitys;
using AOBaseLibC;
using ESRI.ArcGIS.Geodatabase;
using AOBaseLibC.AFGeodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using JJWATSysTool;
using JJWATBaseLibC;
using AOBaseLibC.AFCommon;
namespace JJWATQuery
{
    public partial class FrmQueryGroup : Form
    {
        public FrmQueryGroup()
        {
            InitializeComponent();
        }

        private IApplication m_App = null;
        private IMxDocument m_MxDoc = null;
        private IMap m_pMap = null;
        private UtilitysMgs Mgs;
        private Map m_pMapEvent = null;
        private Dictionary<string, List<string>> dTypeDomain = new Dictionary<string, List<string>>();
        private Dictionary<string, string> dType = new Dictionary<string, string>();
        private List<string> m_lsLayer = new List<string>();
        private lsConDataBll ConData = new lsConDataBll();
        private IGeometry m_WGpSearchPolygon;
        private IGeometry m_pSearchGeometry;
        string sSqlSign;
        List<string> ListSqlWhere = new List<string>();
        private string sTypeNamesE;
        FrmWG fWg;
        UtilitysResultForm frm;
        IList<IFeatureLayer> pListFeatL = new List<IFeatureLayer>();
        DataTable dTable = new DataTable("Table_CanShu");
        public static SysParameters m_CurParrmeter = SysParameters.GetInstance();

        public void inApplication(IApplication Application)
        {
            m_App = Application;
            m_MxDoc = (m_App.Document) as IMxDocument;
            m_pMap = m_MxDoc.FocusMap;
            m_pMapEvent = m_pMap as Map;
            Mgs = new UtilitysMgs(m_App);
            Mgs.SfrmName = "组合";
        }

        private void frmQueryGroup_Shown(object sender, EventArgs e)
        {
            m_lsLayer = Mgs.GetLayers();
            if (m_lsLayer.Count > 0)
            {
                for (int i = 0; i < m_lsLayer.Count; i++)
                {
                    cbLayer.Items.Add(m_lsLayer[i]);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                dTable.Columns.Add("column" + i.ToString() + "", System.Type.GetType("System.String"));
            }
            cbLayer.SelectedIndex = 0;
            cbArea.SelectedIndex = 0;


        }
        //添加选择条件控件
        private void Addfields(string LayerName)
        {
            try
            {
                dTable.Clear();
                if (plSelect.Controls.Count > 0)
                {
                    plSelect.Controls.Clear();
                }
                IFeatureLayer pFLayer = Mgs.GetFLayer(cbLayer.Text);
                List<string> lTypes = new List<string>();
                if (pFLayer == null) return;
                IFeatureClass pFeaClass = pFLayer.FeatureClass;
                if (pFLayer.FeatureClass.FeatureDataset.Workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                    sSqlSign = "*";
                else
                    sSqlSign = "%";
                for (int i = 0; i < pFeaClass.Fields.FieldCount; i++)
                {

                    if (pFeaClass.Fields.get_Field(i).Type.ToString() == "esriFieldTypeString" || pFeaClass.Fields.get_Field(i).Type.ToString() == "esriFieldTypeInteger" || pFeaClass.Fields.get_Field(i).Type.ToString() == "esriFieldTypeDate")
                    {
                        DataRow dr = dTable.NewRow();
                        if (pFeaClass.Fields.get_Field(i).Name == "SUBTYPE")
                        {
                            dr["column0"] = "类型";
                        }
                        else
                            dr["column0"] = pFeaClass.Fields.get_Field(i).AliasName;
                        dr["column1"] = pFeaClass.Fields.get_Field(i).Name;
                        dr["column2"] = pFeaClass.Fields.get_Field(i).Type.ToString();
                        dTable.Rows.Add(dr);
                    }
                }
                for (int j = 0; j < dTable.Rows.Count; j++)
                {
                    dTypeDomain.Clear();
                    string sName = "";
                    dTypeDomain = Mgs.GetDomainsByName(dTable.Rows[j][0].ToString());
                    foreach (var item in dTypeDomain)
                    {
                        sName = item.Key;
                    }
                    if (dTypeDomain.Count == 0)
                    {
                        if (dTable.Rows[j][1].ToString() == "SUBTYPE")
                        {
                            IDataset pData = pFLayer as IDataset;
                            dType.Clear();
                            lTypes.Clear();
                            dType = Mgs.GetSubtypes(pFLayer);
                            foreach (object obj in dType.Keys)
                            {
                                lTypes.Add(obj.ToString());
                            }
                            if (lTypes.Count > 0)
                            {
                                UserCoGroupList UserList = new UserCoGroupList();
                                UserList.Stypename = dTable.Rows[j][0].ToString();
                                UserList.Listname = lTypes;
                                UserList.Top = j * 30;
                                plSelect.Controls.Add(UserList);
                                TableLayoutPanel tLayPan = plSelect.Controls[j].Controls[0] as TableLayoutPanel;
                                FieldsValue(tLayPan, dTable.Rows[j][0].ToString());
                            }
                            else
                            {
                                UserCoTet UserText = new UserCoTet();
                                UserText.Top = j * 30;
                                plSelect.Controls.Add(UserText);
                                TableLayoutPanel tLayPan = plSelect.Controls[j].Controls[0] as TableLayoutPanel;
                                FieldsValue(tLayPan, dTable.Rows[j][0].ToString());
                            }
                        }
                        else
                        {
                            if (dTable.Rows[j][2].ToString() == "esriFieldTypeString" || dTable.Rows[j][2].ToString() == "esriFieldTypeInteger")
                            {
                                UserCoTet UserText = new UserCoTet();
                                UserText.Top = j * 30;
                                plSelect.Controls.Add(UserText);
                                TableLayoutPanel tLayPan = plSelect.Controls[j].Controls[0] as TableLayoutPanel;
                                FieldsValue(tLayPan, dTable.Rows[j][0].ToString());
                            }
                            if (dTable.Rows[j][2].ToString() == "esriFieldTypeDate")
                            {
                                UserCoDate UserDate = new UserCoDate();
                                UserDate.Top = j * 30;
                                plSelect.Controls.Add(UserDate);
                                TableLayoutPanel tLayPan = plSelect.Controls[j].Controls[0] as TableLayoutPanel;
                                FieldsValue(tLayPan, dTable.Rows[j][0].ToString());

                            }
                        }
                    }
                    else
                    {
                        UserCoGroupList UserList = new UserCoGroupList();
                        UserList.Stypename = dTable.Rows[j][0].ToString();
                        UserList.Listname = dTypeDomain[sName];//["PL_" + dTable.Rows[j][1].ToString()];
                        UserList.Top = j * 30;
                        plSelect.Controls.Add(UserList);
                        TableLayoutPanel tLayPan = plSelect.Controls[j].Controls[0] as TableLayoutPanel;
                        FieldsValue(tLayPan, dTable.Rows[j][0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void FieldsValue(TableLayoutPanel LayPan, string sNameC)
        {
            for (int i = 0; i < LayPan.Controls.Count; i++)
            {
                CheckBox cbx = LayPan.Controls[i] as CheckBox;
                if (cbx != null)
                {
                    cbx.Text = sNameC;
                    return;
                }
            }
        }
        private void cbLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Addfields(cbLayer.Text);
        }

        private void cbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbArea.SelectedIndex == 0)
            {
                btnAreaSelect.Enabled = false;
                btnAreaClear.Enabled = false;
            }
            else
            {
                btnAreaSelect.Enabled = true;
                btnAreaClear.Enabled = true;
            }
        }

        private void btnAreaSelect_Click(object sender, EventArgs e)
        {
            if (cbArea.SelectedIndex == 1)
            {
                UtilityDrawTool.IsKeyDown = true;
                UtilityDrawTool._DrawTag = 3;
                UID u = new UID();
                u.Value = "JJWATSysTool.UtilityDrawTool";
                IDocument pdoc = m_App.Document;
                ICommandItem item = pdoc.CommandBars.Find(u, false, false);
                m_App.CurrentTool = item;
                m_pMapEvent.AfterDraw += new IActiveViewEvents_AfterDrawEventHandler(m_pMapEvent_AfterDraw);
            }
            else
            {
                if (fWg == null || fWg.IsDisposed)
                {
                    fWg = new FrmWG();
                    fWg.lbx = lbArea;
                    fWg.inApplication(m_App);
                    fWg.Show();
                }
                else
                {
                    fWg.SetMain();
                    fWg.Visible = true;
                }
            }
        }

        void m_pMapEvent_AfterDraw(ESRI.ArcGIS.Display.IDisplay Display, esriViewDrawPhase phase)
        {
            m_pSearchGeometry = UtilityDrawTool.Geo;
            if (m_pSearchGeometry != null)
            {
                m_App.CurrentTool = null;
                m_pMapEvent.AfterDraw -= m_pMapEvent_AfterDraw;
            }
        }

        private void btnAreaClear_Click(object sender, EventArgs e)
        {
            if (cbArea.SelectedIndex == 1)
            {
                m_pSearchGeometry = null;
                UtilityDrawTool.ClearLayer();
            }
            else
            {
                lbArea.Items.Clear();
                m_WGpSearchPolygon = null;
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            ListSqlWhere.Clear();
            Dictionary<string, string> DEnglish = DintCandE();
            string sSign = "";
            if (rbOr.Checked == true)
                sSign = " or ";
            else
                sSign = " and ";
            foreach (var obj in plSelect.Controls)
            {
                string sWhere = "";
                if (obj is UserCoTet)
                {
                    UserCoTet UserTet = obj as UserCoTet;
                    TableLayoutPanel TablePanel = UserTet.Controls[0] as TableLayoutPanel;
                    CheckBox cbx = TablePanel.Controls[0] as CheckBox;
                    if (cbx is CheckBox)
                    {
                        if (cbx.Checked == true)
                        {
                            TextBox tbx = TablePanel.Controls[1] as TextBox;
                            if (string.IsNullOrEmpty(tbx.Text.Trim()) == false)
                            {
                                sWhere = DEnglish[cbx.Text] + " like '" + sSqlSign + "" + tbx.Text + "" + sSqlSign + "'";
                                ListSqlWhere.Add(sWhere);
                            }
                        }
                    }
                }
                if (obj is UserCoGroupList)
                {
                    UserCoGroupList Userlist = obj as UserCoGroupList;
                    TableLayoutPanel TablePanel = Userlist.Controls[0] as TableLayoutPanel;
                    CheckBox cbx = TablePanel.Controls[0] as CheckBox;
                    if (cbx is CheckBox)
                    {
                        if (cbx.Checked == true)
                        {
                            TextBox tbx = TablePanel.Controls[2] as TextBox;
                            if (string.IsNullOrEmpty(tbx.Text.Trim()) == false)
                            {
                                string ss;
                                if (cbx.Text == "类型")
                                {
                                    ss = tbx.Text.Trim() + ",";
                                    sWhere = "SUBTYPE in (" + SnameLX(ss) + ")";
                                }
                                else
                                {
                                    sWhere = "" + DEnglish[cbx.Text] + " in (" + tbx.Text.Substring(0, tbx.Text.Length) + ")";
                                }
                                ListSqlWhere.Add(sWhere);
                            }

                        }
                    }
                }
                if (obj is UserCoDate)
                {
                    UserCoDate Userdate = obj as UserCoDate;
                    TableLayoutPanel TablePanel = Userdate.Controls[0] as TableLayoutPanel;
                    CheckBox cbx = TablePanel.Controls[0] as CheckBox;
                    if (cbx is CheckBox)
                    {
                        if (cbx.Checked == true)
                        {
                            DateTimePicker dTimeB = TablePanel.Controls[2] as DateTimePicker;
                            DateTimePicker dTimeE = TablePanel.Controls[4] as DateTimePicker;
                            string lTimeB, lTimnE;
                            if (dTimeB is DateTimePicker && dTimeE is DateTimePicker)
                            {
                                if (dTimeB.Value.Date > dTimeE.Value.Date)
                                {
                                    MessageBox.Show("开始时间不可以大于结束时间", "提示!");
                                    return;
                                }
                                else
                                {
                                    lTimeB = Convert.ToString(dTimeB.Value.Date.ToShortDateString());
                                    lTimnE = Convert.ToString(dTimeE.Value.Date.ToShortDateString());
                                    switch (m_CurParrmeter.GWDataSourceType)
                                    {
                                        case AFConst.DataSourceType.AFisSQLServer:
                                            sWhere = DEnglish[cbx.Text] + ">=convert(datetime,'" + lTimeB + " 00:00:00') and " + DEnglish[cbx.Text] + "<=convert(datetime,'" + lTimnE + " 23:59:59')";
                                            break;
                                        case AFConst.DataSourceType.AFisAccess:
                                            sWhere = "";
                                            break;  
                                        case AFConst.DataSourceType.AFisOracle:
                                            sWhere = DEnglish[cbx.Text] + ">= to_date('" + lTimeB + " 00:00:00','yyyy/mm/dd hh24:mi:ss') and " + DEnglish[cbx.Text] + "<=to_date('" + lTimnE + " 23:59:59','yyyy/mm/dd hh24:mi:ss')";
                                            break;
                                    }
                                    ListSqlWhere.Add(sWhere);
                                }
                            }
                        }
                    }
                }
            }
            if (ListSqlWhere.Count > 0)
            {
                IFeatureLayer pFeatureLayer = Mgs.GetFLayer(cbLayer.Text);
                IQueryFilter pFilters = null;
                pListFeatL.Clear();
                pListFeatL.Add(pFeatureLayer);
                string sSql = "";
                for (int i = 0; i < ListSqlWhere.Count; i++)
                {
                    sSql = sSql + ListSqlWhere[i] + sSign;
                }
                if (string.IsNullOrEmpty(sSql) == false)
                    sSql = sSql.Substring(0, sSql.Length - 4);
                if (cbArea.SelectedIndex == 0)
                    pFilters = (Mgs.GetsSpatialFilter(m_pSearchGeometry, sSql, 0, sTypeNamesE));
                else
                {
                    if (cbArea.SelectedIndex == 1)
                    {
                        if (m_pSearchGeometry != null)
                        {
                            pFilters = (Mgs.GetsSpatialFilter(m_pSearchGeometry, sSql, 1, sTypeNamesE));
                        }
                        else
                        {
                            MessageBox.Show("未选择查询区域", "提示!");
                            return;
                        }
                    }
                    else
                    {
                        m_WGpSearchPolygon = fWg.m_pGeometry;
                        if (m_WGpSearchPolygon != null)
                        {
                            pFilters = (Mgs.GetsSpatialFilter(m_WGpSearchPolygon, sSql, 1, sTypeNamesE));
                        }
                        else
                        {
                            MessageBox.Show("未选择查询区域", "提示!");
                            return;
                        }
                    }
                }
                UtilitysResultForm.QueryClass pQuery = new UtilitysResultForm.QueryClass();
                pQuery.pFLayers = pListFeatL;
                pQuery.pFilter = pFilters;
                this.Cursor = Cursors.WaitCursor;
                if (frm == null || frm.IsDisposed)
                {
                    frm = new UtilitysResultForm();

                    frm.Init(pQuery, m_App);
                    if (!frm.IsShow)
                    {
                        this.Cursor = Cursors.Default;
                        frm = null;
                        return;
                    }
                    frm.Show();
                }
                else
                {
                    frm.Init(pQuery, m_App);
                    frm.Activate();
                }
                this.Cursor = Cursors.Default;
            }
            else
                MessageBox.Show("请选择查询条件", "提示!");
        }
        private string SnameLX(string sName)
        {
            string sNameC = sName, sNameReplace = "", sNameE = "";
            int iWz;
            iWz = sName.IndexOf(",", 0, sName.Length);
            if (iWz > -1)
            {
                while (iWz > -1)
                {
                    sNameReplace = sNameC.Substring(0, iWz);
                    sNameReplace = sNameReplace.Replace("'", "");
                    sNameC = sNameC.Substring(iWz + 1, sNameC.Length - iWz - 1);
                    sNameE = sNameE + "'" + dType[sNameReplace] + "',";
                    iWz = sNameC.IndexOf(",", 0, sNameC.Length);
                }
                sNameE = sNameE.Substring(0, sNameE.Length - 1);
            }
            return sNameE;
        }
        private Dictionary<string, string> DintCandE()
        {
            Dictionary<string, string> DEnglish = new Dictionary<string, string>();
            if (dTable.Columns.Count > 0)
            {
                for (int i = 0; i < dTable.Rows.Count; i++)
                {
                    DEnglish.Add(dTable.Rows[i][0].ToString(), dTable.Rows[i][1].ToString());
                }

            }

            return DEnglish;
        }

        protected override void OnClosed(EventArgs e)
        {
            m_pMap.ClearSelection();
            m_MxDoc.ActiveView.Refresh();
            UtilityDrawTool.ClearLayer();
            m_App.CurrentTool = null;
            base.OnClosed(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (var obj in plSelect.Controls)
            {
                if (obj is UserCoTet)
                {
                    UserCoTet UserTet = obj as UserCoTet;
                    TableLayoutPanel TablePanel = UserTet.Controls[0] as TableLayoutPanel;
                    CheckBox cbx = TablePanel.Controls[0] as CheckBox;
                    if (cbx is CheckBox)
                    {
                        if (cbx.Checked == true)
                        {
                            cbx.Checked = false;
                            TextBox tbx = TablePanel.Controls[1] as TextBox;
                            if (string.IsNullOrEmpty(tbx.Text.Trim()) == false)
                            {
                                tbx.Text = "";
                            }
                        }
                    }
                }
                if (obj is UserCoGroupList)
                {
                    UserCoGroupList Userlist = obj as UserCoGroupList;
                    TableLayoutPanel TablePanel = Userlist.Controls[0] as TableLayoutPanel;
                    CheckBox cbx = TablePanel.Controls[0] as CheckBox;
                    if (cbx is CheckBox)
                    {
                        if (cbx.Checked == true)
                        {
                            cbx.Checked = false;
                            TextBox tbx = TablePanel.Controls[2] as TextBox;
                            if (string.IsNullOrEmpty(tbx.Text.Trim()) == false)
                            {
                                tbx.Text = "";
                            }
                        }
                    }
                }
                if (obj is UserCoDate)
                {
                    UserCoDate Userdate = obj as UserCoDate;
                    TableLayoutPanel TablePanel = Userdate.Controls[0] as TableLayoutPanel;
                    CheckBox cbx = TablePanel.Controls[0] as CheckBox;
                    if (cbx is CheckBox)
                    {
                        if (cbx.Checked == true)
                        {
                            cbx.Checked = false;
                        }
                    }
                }
            }
            lbArea.Items.Clear();
            UtilityDrawTool.ClearLayer();
            m_WGpSearchPolygon = null;
            m_pSearchGeometry = null;
        }
    }
}
