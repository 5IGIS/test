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
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using AOBaseLibC.AFGeodatabase;
using JJWATSysTool;

namespace JJWATQuery
{
    public partial class FrmInterface : Form
    {
        public FrmInterface()
        {
            InitializeComponent();
        }
        private IApplication m_App = null;
        private IMxDocument m_MxDoc = null;
        private IMap m_pMap = null;
        private UtilitysMgs Mgs;
        private Dictionary<string, List<string>> dTypeDomain = new Dictionary<string, List<string>>();
        private List<string> iListSelectType = new List<string>();
        private List<string> m_lsLayer = new List<string>();
        private List<string> listWGName = new List<string>();
        private Map m_pMapEvent = null;
        //private IDataset pIdata; 
        private IGeometry m_WGpSearchPolygon;
        private IGeometry m_pSearchGeometry;
        private string sTypeNamesE = "";
        FrmWG fWg;
        UtilitysResultForm frm;
        //private string sLayerDataName = "";
        IList<IFeatureLayer> pListFeatL = new List<IFeatureLayer>();
        public void inApplication(IApplication Application)
        {
            m_App = Application;
            m_MxDoc = (m_App.Document) as IMxDocument;
            m_pMap = m_MxDoc.FocusMap;
            m_pMapEvent = m_pMap as Map;
            Mgs = new UtilitysMgs(m_App);
            Mgs.SfrmName = "接口方式";
            modMain.Init(Application);
        }

        void m_pMapEvent_AfterDraw(IDisplay Display, esriViewDrawPhase phase)
        {
            m_pSearchGeometry = UtilityDrawTool.Geo;
            if (m_pSearchGeometry != null)
            {
                m_App.CurrentTool = null;
                m_pMapEvent.AfterDraw -= m_pMapEvent_AfterDraw;
            }
        }

        private void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLayerAll.Checked == true)
            {
                for (int i = 0; i < cblLayer.Items.Count; i++)
                {
                    cblLayer.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < cblLayer.Items.Count; i++)
                {
                    cblLayer.SetItemChecked(i, false);
                }
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
        private void btnQuery_Click(object sender, EventArgs e)
        {
            IFeatureLayer pFeatureLayer = null;
            IQueryFilter pFilters = null;
            pListFeatL.Clear();
            string sSqlWhere = "";
            if (cblLayer.CheckedItems.Count != 0)
            {

                if (cblSelect.CheckedItems.Count == 0)
                {
                    MessageBox.Show("请选择接口方式", "提示!");
                    return;
                }
                else
                {
                    for (int i = 0; i < cblSelect.CheckedItems.Count; i++)
                    {
                        sSqlWhere = sSqlWhere + "'" + cblSelect.CheckedItems[i] + "'" + ',';
                    }
                    sSqlWhere = sSqlWhere.Substring(0, sSqlWhere.Length - 1);
                }
                for (int i = 0; i < cblLayer.CheckedItems.Count; i++)
                {
                    pFeatureLayer = Mgs.GetFLayer(cblLayer.CheckedItems[i].ToString());
                    ///此段代码为了测试运行通过
                    int itye = pFeatureLayer.FeatureClass.FindField(sTypeNamesE);
                    if (itye == -1)
                    {
                        sTypeNamesE = "JUNCTION";
                    }
                    ///此段代码为了测试运行通过
                    pListFeatL.Add(pFeatureLayer);
                }
                if (cbArea.SelectedIndex == 0)
                    pFilters = (Mgs.GetsSpatialFilter(m_pSearchGeometry, sSqlWhere, 0, sTypeNamesE));
                else
                {
                    if (cbArea.SelectedIndex == 1)
                    {
                        if (m_pSearchGeometry != null)
                        {
                            pFilters = (Mgs.GetsSpatialFilter(m_pSearchGeometry, sSqlWhere, 1, sTypeNamesE));
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
                            pFilters = (Mgs.GetsSpatialFilter(m_WGpSearchPolygon, sSqlWhere, 1, sTypeNamesE));
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
                    frm.Close();
                    frm = new UtilitysResultForm();
                    frm.Init(pQuery, m_App);
                    frm.Show();
                }
                this.Cursor = Cursors.Default;
            }
            else
                MessageBox.Show("请选择查询图层", "提示!");
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (cbLayerAll.Checked)
            {
                cbLayerAll.Checked = false;
            }
            else
            {
                for (int i = 0; i < cblLayer.Items.Count; i++)
                {
                    cblLayer.SetItemChecked(i, false);
                }
            }
            if (cbSelectAll.Checked)
            {
                cbSelectAll.Checked = false;
            }
            else
            {
                for (int i = 0; i < cblSelect.Items.Count; i++)
                {
                    cblSelect.SetItemChecked(i, false);
                }
            }
            lbArea.Items.Clear();
            UtilityDrawTool.ClearLayer();
            m_WGpSearchPolygon = null;
            m_pSearchGeometry = null;
        }

        private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSelectAll.Checked == true)
            {
                for (int i = 0; i < cblSelect.Items.Count; i++)
                {
                    cblSelect.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < cblSelect.Items.Count; i++)
                {
                    cblSelect.SetItemChecked(i, false);
                }
            }
        }

        private void FrmInterface_Load(object sender, EventArgs e)
        {
            try
            {
                Mgs.GeoType = esriGeometryType.esriGeometryPolyline;
                m_lsLayer = Mgs.GetLineLayers(); //Mgs.GetLayers();
                if (m_lsLayer.Count > 0)
                {
                    foreach (var layer in m_lsLayer)
                    {
                        bool blnHaveField = true;
                        var objLayer = modMain.m_objMap.GetLayerByName(layer);
                        foreach (string value in modMain.m_CurParrmeter.QueryJunction())
                        {
                            if (objLayer.FeatureLayer.FeatureClass.Fields.FindField(value) == -1)
                            {
                                blnHaveField = false;
                            }
                        }
                        if (blnHaveField)
                        {
                            cblLayer.Items.Add(layer);
                        }
                    }
                }
                cbArea.SelectedIndex = 0;
                AFFeatureLayer oLayer = modMain.m_objMap.GetLayerByName(XMLConfig.ValveDevice());
                if (oLayer == null)
                {
                    MsgBox.Show("地图没有加载‘" + XMLConfig.ValveDevice() + "’图层，请修改配置表！");
                    return;
                }
                IField pfd = oLayer.FeatureLayer.FeatureClass.Fields.get_Field(oLayer.FeatureLayer.FeatureClass.Fields.FindFieldByAliasName(Mgs.SfrmName));
                dTypeDomain = Mgs.GetDomainsByName(pfd);
                if (dTypeDomain.Count != 0)
                {
                    iListSelectType.Clear();
                    foreach (object item in dTypeDomain.Keys)
                    {
                        sTypeNamesE = item.ToString();
                    }
                    iListSelectType = dTypeDomain[sTypeNamesE];
                    cblSelect.Items.Clear();
                    for (int i = 0; i < iListSelectType.Count; i++)
                    {
                        cblSelect.Items.Add(iListSelectType[i]);
                    }

                    if (string.IsNullOrEmpty(sTypeNamesE) == false)
                    {
                        int i = sTypeNamesE.IndexOf("_", 0, sTypeNamesE.Length);
                        if (i > -1)
                            sTypeNamesE = sTypeNamesE.Substring(i + 1, sTypeNamesE.Length - i - 1);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString());
            }
        }
    }
}
