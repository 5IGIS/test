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
using ESRI.ArcGIS.Geodatabase;
using JJWATSysTool;
using AOBaseLibC;
using AOBaseLibC.AFGeodatabase;

namespace JJWATQuery
{
    public partial class FrmWarnAnalyse : Form
    {
        public FrmWarnAnalyse()
        {
            InitializeComponent();
        }
        private IApplication m_App = null;
        private IMxDocument m_MxDoc = null;
        private IMap m_pMap = null;
        private UtilitysMgs Mgs;
        private Map m_pMapEvent;
        private IGeometry m_WGpSearchPolygon;
        private IGeometry m_pSearchGeometry;
        private List<string> m_lsLayer = new List<string>();
        private List<string> m_lsLine = new List<string>();
        FrmWG fWg;
        UtilitysResultForm frm;
        IList<IFeatureLayer> pListFeatL = new List<IFeatureLayer>();
        private lsConDataBll ConData = new lsConDataBll();
        AFMap afMap = new AFMap();
        public void inApplication(IApplication Application)
        {

            m_App = Application;
            m_MxDoc = (m_App.Document) as IMxDocument;
            m_pMap = m_MxDoc.FocusMap;
            afMap.Map = m_pMap;
            m_pMapEvent = m_pMap as Map;
            Mgs = new UtilitysMgs(m_App);
        }

        private void FrmWarnAnalyse_Shown(object sender, EventArgs e)
        {
            Mgs.GeoType = esriGeometryType.esriGeometryPolyline;
            m_lsLine = Mgs.GetLineLayers();
            m_lsLayer = ConData.get_LayerNameC();
            if (m_lsLayer.Count > 0)
            {
                if (m_lsLayer.Count > 0)
                {
                    for (int i = 0; i < m_lsLayer.Count; i++)
                    {
                        cblLayer.Items.Add(m_lsLayer[i]);
                    }
                }
                cbArea.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("未配置参数设置", "提示!");
                this.Close();
            }
        }

        private void cbLayerAll_CheckedChanged(object sender, EventArgs e)
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
            lbArea.Items.Clear();
            UtilityDrawTool.ClearLayer();
            m_WGpSearchPolygon = null;
            m_pSearchGeometry = null;
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

        private void btnQuery_Click(object sender, EventArgs e)
        {
            AFFeatureLayer objFLayer = null;
            IFeatureLayer pFeatureLayer = null;
            IGeometry pGenmetry = null;
            pListFeatL.Clear();
            if (cblLayer.CheckedItems.Count != 0)
            {
                for (int i = 0; i < cblLayer.CheckedItems.Count; i++)
                {
                    if (cblLayer.CheckedItems[i].ToString() != "管线")
                    {
                        objFLayer = afMap.GetLayerByName(cblLayer.CheckedItems[i].ToString());
                        if (objFLayer == null)
                        {
                            Utilitys.MsgBox.Show("对不起，" + cblLayer.CheckedItems[i].ToString() + "图层没有加载到地图上!");
                            return;
                        }
                        pFeatureLayer = objFLayer.FeatureLayer;
                        pListFeatL.Add(pFeatureLayer);
                    }
                    else
                    {
                        if (m_lsLine.Count > 0)
                        {
                            for (int j = 0; j < m_lsLine.Count; j++)
                            {
                                pFeatureLayer = Mgs.GetFLayer(m_lsLine[j].ToString());
                                pListFeatL.Add(pFeatureLayer);
                            }
                        }
                    }

                }
                if (cbArea.SelectedIndex == 0)
                {
                    //pFilters = (Mgs.GetsSpatialFilter(m_pSearchGeometry, sSqlWhere, 0, sTypeNamesC, sTypeNamesE));
                }
                else
                {
                    if (cbArea.SelectedIndex == 1)
                    {
                        if (m_pSearchGeometry == null)
                        {
                            MessageBox.Show("未选择查询区域", "提示!");
                            return;
                        }
                        else
                            pGenmetry = m_pSearchGeometry;
                    }
                    else
                    {
                        m_WGpSearchPolygon = fWg.m_pGeometry;
                        if (m_WGpSearchPolygon == null)
                        {

                            MessageBox.Show("未选择查询区域", "提示!");
                            return;
                        }
                        else
                            pGenmetry = m_WGpSearchPolygon;
                    }
                }
                this.Cursor = Cursors.WaitCursor;
                UtilitysResultForm.QueryClass pQuery = new UtilitysResultForm.QueryClass();
                pQuery.isWarnAnalyse = true;
                pQuery.pFLayers = pListFeatL;
                ISpatialFilter pSFlt = new SpatialFilterClass();
                if (pGenmetry != null)
                {
                    pSFlt.Geometry = pGenmetry;
                    pSFlt.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                }
                pQuery.pFilter = pSFlt;
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
                MessageBox.Show("请选择查询图层", "提示!");
        }
    }
}