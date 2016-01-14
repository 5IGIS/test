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
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using AOBaseLibC;
using JJWATSysTool;
//using BLL;
namespace JJWATQuery
{
    public partial class FrmQueryNum : Form
    {
        public FrmQueryNum()
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
            Mgs.SfrmName ="编号";
            modMain.Init(Application);
        }

        private void tbSelect_KeyPress(object sender, KeyPressEventArgs e)
        {
            //base.OnKeyPress(e);
            //if ((int)e.KeyChar <= 32) // 特殊键(含空格), 不处理 
            //{
            //    return;
            //}
            //if (!char.IsDigit(e.KeyChar)) // 非数字键, 放弃该输入
            //{
            //    e.Handled = true;
            //    return;
            //}
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
            tbSelect.Text = "";
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
            IFeatureLayer pFeatureLayer = null;
            IQueryFilter pFilters = null;
            pListFeatL.Clear();
            Mgs.Out_SqlSign();
            if (cblLayer.CheckedItems.Count != 0)
            {

                if (string.IsNullOrEmpty(tbSelect.Text))
                {
                    MessageBox.Show("请输入编号", "提示!");
                    return;
                }
                string sSqlWhere = tbSelect.Text.Trim();
                for (int i = 0; i < cblLayer.CheckedItems.Count; i++)
                {
                    pFeatureLayer = Mgs.GetFLayer(cblLayer.CheckedItems[i].ToString());
                    pListFeatL.Add(pFeatureLayer);
                }
                if (cbArea.SelectedIndex == 0)
                {
                    pFilters = (Mgs.GetsSpatialFilter(m_pSearchGeometry, sSqlWhere, 0, sTypeNamesE));
                }
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

        private void FrmQueryNum_Load(object sender, EventArgs e)
        {
            try
            {
                m_lsLayer = Mgs.GetPtFLayerList();
                if (m_lsLayer.Count > 0)
                {
                    foreach (var layer in m_lsLayer)
                    {
                        bool blnHaveField = true;
                        //var objLayer = modMain.m_objMap.GetLayerByName(layer);
                        foreach (string value in modMain.m_CurParrmeter.QueryOldNo())
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
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString());
            }
        }
    }
}
