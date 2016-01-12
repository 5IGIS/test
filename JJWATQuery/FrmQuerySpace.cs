using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Framework;
using Utilitys;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using JJWATSysTool;

namespace JJWATQuery
{
    public partial class FrmQuerySpace : Form
    {
        public FrmQuerySpace()
        {
            InitializeComponent();
        }
        private IApplication m_App = null;
        private IMxDocument m_MxDoc = null;
        IActiveView m_ActiveView;
        private IMap m_pMap = null;
        private IGraphicsContainer pGraCont;
        private UtilitysMgs Mgs;
        private Dictionary<string, List<string>> dTypeDomain = new Dictionary<string, List<string>>();
        private List<string> iListSelectType = new List<string>();
        private List<string> m_lsLayer = new List<string>();
        private List<string> listWGName = new List<string>();
        private Map m_pMapEvent = null;
        private IGeometry m_pSearchGeometry;
        private string sTypeNamesE;
        UtilitysResultForm frm;
        IList<IFeatureLayer> pListFeatL = new List<IFeatureLayer>();
        Dictionary<string, IList<IFeature>> p_DicFeatures = new Dictionary<string, IList<IFeature>>();
        public void inApplication(IApplication Application)
        {
           
            m_App = Application;
            m_MxDoc = (m_App.Document) as IMxDocument;
            m_ActiveView = m_MxDoc.ActiveView; 
            m_pMap = m_MxDoc.FocusMap;
            pGraCont = m_pMap as IGraphicsContainer;
            m_pMapEvent = m_pMap as Map;
            Mgs = new UtilitysMgs(m_App);
            Mgs.SfrmName = "空间";
        }
        private void frmQuerySpace_Shown(object sender, EventArgs e)
        { 
            m_lsLayer = Mgs.GetLayers();
            if (m_lsLayer.Count > 0)
            {
                for (int i = 0; i < m_lsLayer.Count; i++)
                {
                    cblLayer.Items.Add(m_lsLayer[i]);
                }
            }
            cbArea.SelectedIndex = 0;
            cbSelcet.SelectedIndex = 0;

        
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            IFeatureLayer pFeatureLayer = null;
            IQueryFilter pFilters = null;
            pListFeatL.Clear();
            UtilitysResultForm.QueryClass pQuery = new UtilitysResultForm.QueryClass();
            double dBuff = 0;
            try
            {
                dBuff = Convert.ToDouble(tbBuff.Text);
            }
            catch (Exception )
            {
                MessageBox.Show("缓冲区输入非法", "提示!");
                return;
            }
            
            if (cblLayer.CheckedItems.Count != 0)
            { 
                for (int i = 0; i < cblLayer.CheckedItems.Count; i++)
                {
                    pFeatureLayer = Mgs.GetFLayer(cblLayer.CheckedItems[i].ToString());
                    pListFeatL.Add(pFeatureLayer);
                }
                if (cbArea.SelectedIndex == 0)
                {
                    if (m_pSearchGeometry != null)
                    {
                        UtilityDrawTool.ClearLayer();
                        IGeometry pGeometry = m_pSearchGeometry ;
                        ITopologicalOperator pTopo = pGeometry as ITopologicalOperator;
                        pGeometry = pTopo.Buffer(dBuff);
                        DrawBuff(pGeometry);
                        pFilters = (Mgs.GetsSpatialFilter(pGeometry, "", 1, sTypeNamesE));
                    }
                    else
                    {
                        MessageBox.Show("未选择查询区域", "提示!");
                        return;
                    }
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
                {
                    if (m_pMap.SelectionCount == 0)
                    {
                        MessageBox.Show("数据集中没有选择图层数据！", "提示!");
                        return;
                    }
                    else
                    {
                        pQuery.isSelQuery = true;
                        this.Cursor = Cursors.WaitCursor;
                        if (frm == null || frm.IsDisposed)
                        {
                            frm = new UtilitysResultForm();
                            frm.Init(pQuery, m_App);
                            frm.Show();
                        }
                        else
                        {
                            frm.Activate();
                        }
                        this.Cursor = Cursors.Default;
                    }
                }
            }
            else
                MessageBox.Show("请选择查询图层", "提示!");
        }
        private void DrawBuff(IGeometry Geometry)
        {
            ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbolClass();
            pSimpleFillSymbol.Style = esriSimpleFillStyle.esriSFSForwardDiagonal;
            IElement pElem = new PolygonElementClass();
            IFillShapeElement pFillShaple = pElem as IFillShapeElement;
            ISimpleFillSymbol pSimpleSymbol = new SimpleFillSymbolClass();
            pSimpleSymbol.Style = esriSimpleFillStyle.esriSFSDiagonalCross;
            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.Red = 255;

            ISimpleLineSymbol pSimpleLineSymbol = new SimpleLineSymbolClass();
            pSimpleLineSymbol.Color = pRgbColor;
            pSimpleSymbol.Outline = pSimpleLineSymbol;
            pSimpleSymbol.Color = pRgbColor;

            pFillShaple.Symbol = pSimpleSymbol;
            pElem.Geometry = Geometry;
            pGraCont.AddElement(pElem, 0);
            m_ActiveView.Refresh();
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
            tbBuff.Text = "0";
            m_pMap.ClearSelection();
            m_MxDoc.ActiveView.Refresh();
            UtilityDrawTool.ClearLayer();
            m_pSearchGeometry = null;
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

        private void cbSelcet_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbSelcet.SelectedItem.ToString())
            {
                case "包含":
                    sTypeNamesE = "1";
                    break;
                case "叠加":
                    sTypeNamesE = "2";
                    break;
                case "跨越":
                    sTypeNamesE = "3";
                    break;
                case "相接":
                    sTypeNamesE = "4";
                    break;
                case "相交":
                    sTypeNamesE = "5";
                    break;
            }
        }

        private void tbBuff_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if ((int)e.KeyChar <= 32) // 特殊键(含空格), 不处理 
            {
                return;
            }
            if (!char.IsDigit(e.KeyChar)) // 非数字键, 放弃该输入
            {
                e.Handled = true;
                return;
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

        private void btnAreaSelect_Click(object sender, EventArgs e)
        {
            if (cbArea.SelectedIndex == 0)
            {
                try
                {
                    //if (string.IsNullOrEmpty(tbBuff.Text) == true)
                    //{
                    //    tbBuff.Text = "0";
                    //}
                    UtilityDrawTool.IsKeyDown = true;
                    UtilityDrawTool._DrawTag = 3;
                    UtilityDrawTool.BufferNum = 0;
                    UID u = new UID();
                    u.Value = "JJWATSysTool.UtilityDrawTool";
                    IDocument pdoc = m_App.Document;
                    ICommandItem item = pdoc.CommandBars.Find(u, false, false);
                    m_App.CurrentTool = item;
                    m_pMapEvent.AfterDraw += new IActiveViewEvents_AfterDrawEventHandler(m_pMapEvent_AfterDraw);
                }
                catch (Exception ee)
                {

                    MessageBox.Show(ee.ToString());
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
            m_pSearchGeometry = null;
            UtilityDrawTool.ClearLayer();
        }

        private void cbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnQuery.Enabled = true;
            if (cbArea.SelectedIndex==0)
            {
                btnAreaSelect.Enabled = true;
                btnAreaClear.Enabled = true;
                cbSelcet.Enabled = true;
                tbBuff.Enabled = true;
                cblLayer.Enabled = true;
                cbLayerAll.Enabled = true;
                
            }
            if (cbArea.SelectedIndex== 1)
            {
                if (m_pSearchGeometry!=null)
                {
                    UtilityDrawTool.ClearLayer();
                }
                btnAreaSelect.Enabled = false;
                btnAreaClear.Enabled = false;
                cbSelcet.Enabled = false;
                tbBuff.Enabled = false;
                cblLayer.Enabled = false;
                cbLayerAll.Enabled = false;
                p_DicFeatures = ResultManager.m_DicFeatures;
                if (m_pMap.SelectionCount == 0)
                {
                    MessageBox.Show("数据集为空！", "提示!");
                    btnQuery.Enabled = false;
                }
            }
            
        }
    }
}
