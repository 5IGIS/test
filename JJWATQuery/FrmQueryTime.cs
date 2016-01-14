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
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using JJWATSysTool;

namespace JJWATQuery
{
    public partial class FrmQueryTime : Form
    {
        public FrmQueryTime()
        {
            InitializeComponent();
        }
        private IApplication m_App = null;
        private IMxDocument m_MxDoc = null;
        private IMap m_pMap = null;
        private UtilitysMgs Mgs;
        private Dictionary<string, List<string>> dTypeDomain = new Dictionary<string, List<string>>();
        private List<string> iListSelectType = new List<string>();
        private List<IFeatureLayer> m_lsLayer = new List<IFeatureLayer>();
        private List<string> listWGName = new List<string>();
        private Map m_pMapEvent = null;
        //private IDataset pIdata; 
        private IGeometry m_WGpSearchPolygon;
        private IGeometry m_pSearchGeometry;
        private string sTypeNamesE;
        FrmWG fWg;
        UtilitysResultForm frm;
        IList<IFeatureLayer> pListFeatL = new List<IFeatureLayer>();
        public void inApplication(IApplication Application)
        {

            m_App = Application;
            m_MxDoc = (m_App.Document) as IMxDocument;
            m_pMap = m_MxDoc.FocusMap;
            m_pMapEvent = m_pMap as Map;
            Mgs = new UtilitysMgs(m_App);
            Mgs.SfrmName = "时间";
            modMain.Init(Application);
        }

        private void FrmQueryTime_Load(object sender, EventArgs e)
        {
            try
            {
                m_lsLayer = Mgs.GetFLayerList();
                if (m_lsLayer.Count > 0)
                {
                    foreach (var layer in m_lsLayer)
                    {
                        bool blnHaveField = true;
                        //var objLayer = modMain.m_objMap.GetLayerByName(layer);
                        foreach (string value in modMain.m_CurParrmeter.QueryFinishdate())
                        {
                            if (layer.FeatureClass.Fields.FindField(value) == -1)
                            {
                                blnHaveField = false;
                            }
                        }
                        if (blnHaveField)
                        {
                            cblLayer.Items.Add(layer.Name);
                        }
                    }
                }
                cbArea.SelectedIndex = 0;
                cbTime.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString());
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

        void m_pMapEvent_AfterDraw(IDisplay Display, esriViewDrawPhase phase)
        {
            m_pSearchGeometry = UtilityDrawTool.Geo;
            if (m_pSearchGeometry != null)
            {
                m_App.CurrentTool = null;
                m_pMapEvent.AfterDraw -= m_pMapEvent_AfterDraw;
            } 
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            IFeatureLayer pFeatureLayer = null;
            IQueryFilter pFilters = null;
            pListFeatL.Clear();
            string lTimeB, lTimnE;
            if (cblLayer.CheckedItems.Count != 0)
            {
                string sSqlWhere = "";
                if (sTypeNamesE == "!" || sTypeNamesE == "=")
                {
                    if ((sTypeNamesE == "!"))
                    {


                        if (DTimeB.Value.Date > DTimeE.Value.Date)
                        {
                            MessageBox.Show("开始时间不可以大于结束时间", "提示!");
                            return;
                        }
                        lTimeB = Convert.ToString(DTimeB.Value.Date.ToShortDateString());
                        lTimnE = Convert.ToString(DTimeE.Value.Date.ToShortDateString());
                        sSqlWhere = "FINISHDATE between to_date('" + lTimeB + " 00:00:00','yyyy/mm/dd hh24:mi:ss')  and  to_date('" + lTimnE + " 23:59:59','yyyy/mm/dd hh24:mi:ss')";
                    }
                    else
                    {
                        lTimeB = Convert.ToString(DTimeB.Value.Date.ToShortDateString());
                        sSqlWhere = "FINISHDATE between to_date('" + lTimeB + " 00:00:00','yyyy/mm/dd hh24:mi:ss')  and to_date('" + lTimeB + " 23:59:59','yyyy/mm/dd hh24:mi:ss')";
                    } 
                    
                }
                else 
                {
                    lTimeB = Convert.ToString(DTimeB.Value.Date.ToShortDateString());
                    sSqlWhere = "FINISHDATE" + sTypeNamesE + "to_date('" + lTimeB + "  ','yyyy/mm/dd ') ";
                }
                for (int i = 0; i < cblLayer.CheckedItems.Count; i++)
                {
                    pFeatureLayer = Mgs.GetFLayer(cblLayer.CheckedItems[i].ToString());
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
            UtilityDrawTool.ClearLayer();
            lbArea.Items.Clear();
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

        private void cbTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbTime.Text)
            {
                case "大于":
                    sTypeNamesE = ">";
　　                break;
                case "大于等于":
                  sTypeNamesE = ">=";
                  break;
                case "等于":
                  sTypeNamesE = "=";
                  break;
                case "小于等于":
                  sTypeNamesE = "<=";
                  break;
                case "小于":
                  sTypeNamesE = "<";
                  break;
                case "介于":
                  sTypeNamesE = "!";
                  break;
            }
            if (sTypeNamesE != "!")
            {
                DTimeE.Enabled = false;
            }
            else
            {
                DTimeE.Enabled = true;
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
    }
}
