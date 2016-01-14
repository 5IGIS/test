using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Utilitys;
using JJWATBaseLibC;
using JJWATSysTool;

namespace JJWATQuery
{
    public partial class FrmQuerySQL : Form
    {
        public FrmQuerySQL()
        {
            InitializeComponent();
        }
        private IApplication m_App = null;
        private IMxDocument m_MxDoc = null;
        private IMap m_pMap = null;
        private Dictionary<string, string> dicFieldName = new Dictionary<string, string>();
        Dictionary<string, string> pdiclist = new Dictionary<string, string>();
        private List<string> iListN = new List<string>();
        private Map m_pMapEvent;
        private UtilitysMgs Mgs;
        private IFeatureLayer pFLayer;
        string sLayerE = "", SqlSign = "";
        private IGeometry m_WGpSearchPolygon;
        private IGeometry m_pSearchGeometry;
        FrmWG fWg;
        private List<string> m_lsLayer = new List<string>();
        UtilitysResultForm frm;
        lsConDataBll ConData = new lsConDataBll();
        IList<IFeatureLayer> pListFeatL = new List<IFeatureLayer>();
        string sAdminName = "";
        string sSqls = "";
        int ints;
        bool bStrDub;
        public CUserInfo g_objUserInfo = CUserInfo.GetInstance();
        public void inApplication(IApplication Application)
        {

            m_App = Application;
            m_MxDoc = m_App.Document as IMxDocument;
            m_pMap = m_MxDoc.FocusMap;
            m_pMapEvent = m_pMap as Map;
            Mgs = new UtilitysMgs(m_App);
            Mgs.SfrmName = "SQL";
            sAdminName = Mgs.get_LoginUserName();
            bool bools = g_objUserInfo.UserPower.MenuItemEnable("SQL语句管理");
            if (bools == true)
            {
                btnDelt.Visible = true;
            }
            else
            {
                btnDelt.Visible = false;
            }

        }

        private void FrmQuerySQL_Load(object sender, EventArgs e)
        {
            try
            {
                m_lsLayer = Mgs.GetLayers();
                if (m_lsLayer.Count > 0)
                {
                    for (int i = 0; i < m_lsLayer.Count; i++)
                    {
                        cbLayer.Items.Add(m_lsLayer[i]);
                    }
                }
                Mgs.Out_SqlSign();
                SqlSign = Mgs.sSqlSign;
                cbLayer.SelectedIndex = 0;
                lbFields.SelectedIndex = 0;
                cbArea.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString());
            }
        }
        private void cbLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbSql.Text = "";
            lbArea.Items.Clear();
            lsbSelect.Items.Clear();
            UtilityDrawTool.ClearLayer();
            m_WGpSearchPolygon = null;
            m_pSearchGeometry = null;
            lbFields.Items.Clear();
            dicFieldName.Clear();
            string sfieldsAliasName = "", sfieldsName = "";
            pFLayer = Mgs.GetFLayer(cbLayer.Text);
            if (pFLayer == null)
                return;
            IDataset pDataSet = pFLayer as IDataset;
            sLayerE = pDataSet.BrowseName;
            if (pFLayer.FeatureClass.FeatureDataset.Workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
            {
                btnLikes.Text = "*";
            }
            else
            {
                btnLikes.Text = "%";
            }
            for (int i = 0; i < pFLayer.FeatureClass.Fields.FieldCount; i++)
            {
                sfieldsAliasName = pFLayer.FeatureClass.Fields.get_Field(i).AliasName;
                sfieldsName = pFLayer.FeatureClass.Fields.get_Field(i).Name;
                if (sfieldsAliasName != "Enabled" && sfieldsAliasName != "SHAPE")
                {
                    if (!dicFieldName.ContainsKey(sfieldsAliasName))
                    {
                        dicFieldName.Add(sfieldsAliasName, sfieldsName);
                        lbFields.Items.Add(sfieldsAliasName);
                    }
                }
            }
            lbFields.SelectedIndex = 0;
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (cbArea.SelectedIndex == 1)
            {
                UtilityDrawTool.IsKeyDown = true;
                UtilityDrawTool.BufferNum = 10;
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
                    fWg.inApplication(m_App);
                    fWg.Show();
                }
                fWg.Visible = true;
                fWg.Activate();
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

        private void btnClearQY_Click(object sender, EventArgs e)
        {
            UtilityDrawTool.ClearLayer();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            tbSql.Text = "";
            tbSelect.Text = "";
            lsbSelect.Items.Clear();
            lbArea.Items.Clear();
            UtilityDrawTool.ClearLayer();
            m_WGpSearchPolygon = null;
            m_pSearchGeometry = null;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            IQueryFilter pFilters = null;
            pListFeatL.Clear();
            string sSqlWhere = tbSql.Text;
            if (!ConData.frmSqlSelect(sLayerE, tbSql.Text))
            {
                MessageBox.Show("条件错误,请检查!");
                return;
            }
            if (string.IsNullOrEmpty(sSqlWhere.Trim()) == false)
            {
                if (pFLayer.FeatureClass != null)
                {
                    pListFeatL.Add(pFLayer);
                    if (cbArea.SelectedIndex == 0)
                    {
                        pFilters = Mgs.GetsSpatialFilter(m_pSearchGeometry, sSqlWhere, 0, "");
                    }
                    else
                    {
                        if (cbArea.SelectedIndex == 1)
                        {
                            if (m_pSearchGeometry != null)
                            {
                                pFilters = Mgs.GetsSpatialFilter(m_pSearchGeometry, sSqlWhere, 1, "");
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
                                pFilters = Mgs.GetsSpatialFilter(m_WGpSearchPolygon, sSqlWhere, 1, "");
                            }
                            else
                            {
                                MessageBox.Show("未选择查询区域", "提示!");
                                return;
                            }
                        }
                        UtilityDrawTool.ClearLayer();
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
                    frm.Close();
                    frm = new UtilitysResultForm();
                    frm.Init(pQuery, m_App);
                    frm.Show();
                }
                this.Cursor = Cursors.Default;
            }
            else
            {
                MessageBox.Show("SQL语句不能为空", "提示!");
            }
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

        private void btnValue_Click(object sender, EventArgs e)
        {
            try
            {
                lsbSelect.Items.Clear();
                List<string> plist2 = new List<string>();
                pdiclist.Clear();
                if (lbFields.SelectedItem.ToString() == "类型")
                {
                    pdiclist = Mgs.GetSubtypes(pFLayer.FeatureClass, dicFieldName[lbFields.SelectedItem.ToString()].ToString());
                }
                else
                {
                    pdiclist = Mgs.GetSubtypes(lbFields.SelectedItem.ToString());
                }
                plist2 = ConData.get_Select(sLayerE, dicFieldName[lbFields.SelectedItem.ToString()]);
                if (pFLayer.FeatureClass.Fields.get_Field(pFLayer.FeatureClass.Fields.FindFieldByAliasName(lbFields.SelectedItem.ToString())).Type == esriFieldType.esriFieldTypeString)
                {
                    bStrDub = true;
                    for (int i = 0; i < plist2.Count; i++)
                    {
                        if (pdiclist.Count > 0)
                        {

                            if (pdiclist.ContainsKey(plist2[i].ToString()) == true)
                            {
                                lsbSelect.Items.Add("'" + plist2[i].ToString() + "—" + pdiclist[plist2[i].ToString()].ToString() + "'");
                            }
                            else
                            {
                                if (lbFields.SelectedItem.ToString() != "类型")
                                    lsbSelect.Items.Add("'" + plist2[i].ToString() + "'");
                                else
                                    lsbSelect.Items.Add(plist2[i].ToString() + "—");
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(plist2[i].ToString()))
                                lsbSelect.Items.Add("NULL");
                            else
                                lsbSelect.Items.Add("'" + plist2[i].ToString() + "'");
                        }
                    }
                }
                else
                {
                    bStrDub = false;
                    for (int i = 0; i < plist2.Count; i++)
                    {
                        if (pdiclist.Count > 0)
                        {
                            if (pdiclist.ContainsKey(plist2[i].ToString()) == true)
                            {
                                lsbSelect.Items.Add(plist2[i].ToString() + "—" + pdiclist[plist2[i].ToString()].ToString());
                            }
                            else
                            {
                                if (lbFields.SelectedItem.ToString() != "类型")
                                    lsbSelect.Items.Add(plist2[i].ToString());
                                else
                                    lsbSelect.Items.Add(plist2[i].ToString() + "—");
                            }
                        }
                        else
                        {
                            if (pFLayer.FeatureClass.Fields.get_Field(pFLayer.FeatureClass.Fields.FindFieldByAliasName(lbFields.SelectedItem.ToString())).Type == esriFieldType.esriFieldTypeDate)
                            {
                                if (string.IsNullOrEmpty(plist2[i].ToString()))
                                    lsbSelect.Items.Add("NULL");
                                else
                                    lsbSelect.Items.Add(sDate(plist2[i].ToString()));
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(plist2[i].ToString()))
                                    lsbSelect.Items.Add("NULL");
                                else
                                    lsbSelect.Items.Add(plist2[i].ToString());
                            }
                        }
                    }
                }
                if (lsbSelect.Items.Count == 0)
                {
                    MessageBox.Show("查询结果为空", "提示!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "提示!");
            }
        }

        private string sDate(string Date)
        {
            DateTime dt = Convert.ToDateTime(Date);
            string sTime = dt.ToString("yyyy-MM-dd");
            //sTime = "'" + sTime + "'";  //wy修改日期类型获取的时候加TO_DATE函数
            sTime = "TO_DATE('" + sTime + "','yyyy-MM-dd')";
            return sTime;
        }


        private void tbSelect_TextChanged(object sender, EventArgs e)
        {
            string sSelect = "";
            if (!string.IsNullOrEmpty(tbSelect.Text.Trim()))
            {
                for (int i = 0; i < lsbSelect.Items.Count; i++)
                {
                    sSelect = lsbSelect.Items[i].ToString();
                    if (sSelect.IndexOf(tbSelect.Text.Trim()) != -1)
                    {
                        lsbSelect.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void btnLittleEqual_Click(object sender, EventArgs e)
        {
            set_tbSql(btnLittleEqual.Text);
        }
        private void set_tbSql(string sSql)
        {
            tbSql.SelectedText += sSql + " ";
            tbSql.Focus();
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            set_tbSql(btnEqual.Text);
        }

        private void btnNotEqual_Click(object sender, EventArgs e)
        {
            set_tbSql(btnNotEqual.Text);
        }

        private void btnLike_Click(object sender, EventArgs e)
        {
            set_tbSql(" " + btnLike.Text + " ");
        }

        private void btnBig_Click(object sender, EventArgs e)
        {
            set_tbSql(btnBig.Text);
        }

        private void btnBigEqual_Click(object sender, EventArgs e)
        {
            set_tbSql(btnBigEqual.Text);
        }

        private void btnLittle_Click(object sender, EventArgs e)
        {
            set_tbSql(btnLittle.Text);
        }

        private void btnAnd_Click(object sender, EventArgs e)
        {
            set_tbSql(" " + btnAnd.Text + " ");
        }

        private void btnOr_Click(object sender, EventArgs e)
        {
            set_tbSql(" " + btnOr.Text + " ");
        }

        private void btnNot_Click(object sender, EventArgs e)
        {
            set_tbSql(" " + btnNot.Text + " ");
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            set_tbSql(btnLine.Text);
        }

        private void btnLikes_Click(object sender, EventArgs e)
        {
            set_tbSql(btnLikes.Text);
        }

        private void btnBracket_Click(object sender, EventArgs e)
        {
            set_tbSql(btnBracket.Text);
        }

        private void btnIs_Click(object sender, EventArgs e)
        {
            set_tbSql(btnIs.Text);
        }
        private void lsbSelect_DoubleClick(object sender, EventArgs e)
        {
            string sSelect = "";
            if (lsbSelect.SelectedItem.ToString() == "NULL")
            {
                sSelect = lsbSelect.SelectedItem.ToString().Replace("'", "");
            }
            else
            {
                if (bStrDub == true)
                {
                    if (lsbSelect.SelectedItem.ToString().IndexOf("—") > -1)
                    {

                        sSelect = lsbSelect.SelectedItem.ToString().Split('—')[0].ToString() + "'";
                    }
                    else
                    {
                        sSelect = lsbSelect.SelectedItem.ToString();
                    }
                }
                else
                {
                    if (lsbSelect.SelectedItem.ToString().IndexOf("TO_DATE") == -1)
                    {
                        if (lsbSelect.SelectedItem.ToString().IndexOf("—") > -1)
                        {
                            sSelect = lsbSelect.SelectedItem.ToString().Split('—')[0].ToString();
                        }
                        else
                            sSelect = lsbSelect.SelectedItem.ToString();

                    }
                    else
                        sSelect = lsbSelect.SelectedItem.ToString();
                }
            }
            set_tbSql(sSelect);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            tbSql.Text = "";
        }
        private void btnRead_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            FrmSqlLond fLond = new FrmSqlLond();
            fLond.intSQL(sLayerE, cbLayer.SelectedItem.ToString(), sAdminName, this.Text, SqlSign, tbSql);
            fLond.ShowDialog();
            this.Enabled = true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbSql.Text.Trim()))
            {
                MessageBox.Show("条件不可以为空", "提示!");
            }
            else
            {
                if (!ConData.frmSqlSelect(sLayerE, tbSql.Text))
                {
                    MessageBox.Show("条件错误，不可以保存");
                    return;
                }
                this.Enabled = false;
                FrmSqlSave fSave = new FrmSqlSave();
                fSave.intSQL(sLayerE, tbSql.Text.Trim(), cbLayer.SelectedItem.ToString(), sAdminName, this.Text);
                fSave.ShowDialog();
                this.Enabled = true;

            }
        }

        private void lbFields_DoubleClick(object sender, EventArgs e)
        {
            set_tbSql(dicFieldName[lbFields.SelectedItem.ToString()].ToString());
        }

        private void lbFields_Click(object sender, EventArgs e)
        {
            if (ints != lbFields.SelectedIndex)
            {
                lsbSelect.Items.Clear();
            }
        }

        private void lbFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            ints = lbFields.SelectedIndex;
        }

        private void btnDelt_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            FrmSqlDel fDel = new FrmSqlDel();
            fDel.intSQL(cbLayer.SelectedItem.ToString());
            fDel.ShowDialog();
            this.Enabled = true;
        }
    }
}
