using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Framework;
using System.Data.OleDb;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.ArcMapUI;
using JJWATBaseLibC;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace JJWATQuery
{
    public partial class FrmQueryRange : Form
    {
        private IApplication pApp;
        private IMxDocument pMxDoc;
        private IMap m_pMap;
        private Map m_pMapEvent;
        double dfineNumber = 500;//限制显示条数
        private ICommandItem m_pOldItem = null;
        private Boolean m_blnNewDraw;
        private IPolygon m_pSearchPolygon;
        private IGeometry m_pSearchGeometry;
        private strctData4ValveQuery sDPE = new strctData4ValveQuery();

        string con = "Provider=MSDAORA.1;Password=jjwater;User ID=jjwater;Data Source=jjwater;Persist Security Info=True";
        OleDbConnection myconnection = new OleDbConnection();


        private CDBCon m_objDBCON = null;
        private SysParameters m_CurParameter = null;
        public FrmQueryRange()
        {
            InitializeComponent();
        }
        private bool InitLayernameBeforeLoad()
        {
            string sql = "select distinct (t.layername) from " + m_CurParameter.GWUserName + ".CLOSEVALVEDISTANCE t order by t.layername";

            sDPE.Pipelinequipment = new List<string>();
            sDPE.dMinDistance = 0.0;
            sDPE.dMaxDistance = 0.0;
            sDPE.bIsAll = false;
            sDPE.lstStrValveIDs = new List<string>();       
            OleDbDataReader myreader = null;
            m_objDBCON.ExecuteSQLReturn(sql, ref myreader);

            cboSB.Items.Clear();
            cboSB.Items.Add("全部");
            if (myreader.HasRows)
            {
                while (myreader.Read())
                {
                    cboSB.Items.Add(myreader["layername"].ToString());
                }
            }
            else
            {
                MessageBox.Show("库中设备信息读取失败或者信息为空，请检查数据!");
                return false;
            }
            btnRegional.Enabled = false;
            cboSB.SelectedIndex = 0;
            if (cboSB.Items.Count > 1)
            {
                //选中全市
                chkAllCity.Checked = true;
                //区域复选框激活可用
                chkRegional.Enabled = true;
                //查询按钮激活可用
                btnQuery.Enabled = true;
            }
            else
            {
                chkAllCity.Checked = false;
                chkRegional.Enabled = false;
                btnQuery.Enabled = false;
                MessageBox.Show("数据不全,无法进行查询", "提示!");
                return false;
            }

            return true;
        }
        public bool inLication(IApplication Application)
        {
            try
            {
                pApp = Application;
                pMxDoc = (pApp.Document) as IMxDocument;
                m_pMap = pMxDoc.FocusMap;
                m_pMapEvent = m_pMap as Map;

                m_objDBCON = CDBCon.GetInstance();
                m_CurParameter = SysParameters.GetInstance();
                LocalCDBCon lcdb = new LocalCDBCon();
                lcdb.OpenConnection(con);
                this.TopMost = true;
                InitLayernameBeforeLoad();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误！");
                return false;
            }

        }

        private void chkAllCity_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllCity.Checked == true)
            {
                chkRegional.Checked = false;
                btnRegional.Enabled = false;
                DeleteOldElement();
            }
            else
            {
                chkRegional.Checked = true;
                btnRegional.Enabled = true;
            }
        }

        private void chkRegional_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRegional.Checked == true)
            {
                chkAllCity.Checked = false;
                btnRegional.Enabled = true;
            }
            else
            {
                chkAllCity.Checked = true;
                btnRegional.Enabled = false;
            }
        }

        private void btnRegional_Click(object sender, EventArgs e)
        {
            DeleteOldElement();
            ICommandItem pSelectTool;
            ICommandBars pCommandBars;
            UID u = new UID();
            u.Value = "esriArcMapUI.NewPolygonTool";
            IDocument pdoc = pApp.Document;
            this.Visible = false;
            if (this.TopMost == true)
                this.TopMost = false;
            pCommandBars = pdoc.CommandBars;
            pSelectTool = pCommandBars.Find(u, false, false);
            m_pOldItem = pApp.CurrentTool;
            pApp.CurrentTool = pSelectTool;
            m_blnNewDraw = true;
            m_pMapEvent.AfterDraw += new IActiveViewEvents_AfterDrawEventHandler(m_pMapEvent_AfterDraw);
        }

        void m_pMapEvent_AfterDraw(IDisplay Display, esriViewDrawPhase phase)
        {
            IMxDocument pMxDoc = pApp.Document as IMxDocument;
            IActiveView pAV = pMxDoc.FocusMap as IActiveView;
            IGraphicsContainer pContainer = pAV as IGraphicsContainer;
            IGraphicsContainerSelect pContainerSel = pContainer as IGraphicsContainerSelect;
            IElement pElement = pContainerSel.DominantElement;
            //IPointCollection pPointCol;
            IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            ILineSymbol pLineSymbol;
            ISymbol pSymbol = pFillSymbol as ISymbol;
            ISymbol pLSymbol;
            IRgbColor pRGBColor = new RgbColorClass();
            // IActiveView pActiveView;
            ICommandItem pSelectTool;
            ICommandBars pCommandBars;
            UID u = new UID();
            IDocument pdoc;
            pRGBColor.Transparency = 0;
            pFillSymbol.Color = pRGBColor;
            pLineSymbol = pFillSymbol.Outline;
            pLSymbol = pLineSymbol as ISymbol;
            pLSymbol.ROP2 = esriRasterOpCode.esriROPXOrPen;
            pRGBColor.UseWindowsDithering = false;
            pRGBColor.Red = 145;
            pRGBColor.Green = 145;
            pRGBColor.Blue = 145;
            pLineSymbol.Color = pRGBColor;
            pLineSymbol.Width = 0.1;
            pFillSymbol.Outline = pLineSymbol;
            IFillShapeElement pFillShape;
            if (pElement != null)
            {
                pFillShape = pElement as IFillShapeElement;
                pFillShape.Symbol = pSymbol as IFillSymbol;
                m_pSearchPolygon = pElement.Geometry as IPolygon;
                m_pSearchGeometry = pElement.Geometry;
                if (m_blnNewDraw == true)
                {
                    pAV.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, pAV.Extent);
                    if (this.Visible == false)
                    {
                        this.TopMost = true;
                        this.Visible = true;
                    }
                    u.Value = "esriArcMapUI.EditVerticesTool";
                    pdoc = pApp.Document;
                    pCommandBars = pdoc.CommandBars;
                    pSelectTool = pCommandBars.Find(u, true, true);
                    pApp.CurrentTool = pSelectTool;
                    pAV.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                }
                m_blnNewDraw = false;
            }
            if (phase.ToString() == "esriViewForeground" && m_pSearchPolygon != null)
            {
                //DrawPolygonXOR(m_pMap, null, m_pSearchPolygon, true);
            }
        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
               
                //距离范围合理的情况下
                if (CheckDistance() && CheckRegional())
                {
                    SelectedEquipment();
                    ClsQueryData QueryData = new ClsQueryData();
                    QueryData.clear();
                    IList<string> ListGXlayer = new List<string>();
                    this.TopMost = false;
                    Dictionary<string, IList<string>> dicResults = new Dictionary<string, IList<string>>();
                    //区域查询
                    if (chkRegional.Checked == true)
                    {
                        ListGXlayer.Clear();
                        ListGXlayer.Add("阀门");
                        dicResults = QueryData.Get_GXlayer(m_pMap, m_pSearchGeometry, ListGXlayer);
                        if (dicResults.Count == 0)
                        {
                            MessageBox.Show("所选区域没有发现阀门", "提示!");
                            DeleteOldElement();
                            return;
                        }
                        IList<string> lisIDstr = null;
                        lisIDstr = dicResults["阀门"];
                        sDPE.lstStrValveIDs = lisIDstr;
                        DeleteOldElement();
                    }
                    else
                    {
                        //全市
                        sDPE.bIsAll = true;
                    }
                    this.Close();
                    FrmQueryRangeResult frmS = new FrmQueryRangeResult();
                    frmS.InitResultForm(sDPE, pApp);
                    frmS.Show();
                }
                else
                {
                    return;

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        //检查距离的合理性
        private bool CheckDistance()
        {
            string sMin = txtMin.Text.Trim();
            string sMax = txtMax.Text.Trim();

            //判断距离设置是否合理            
            if (string.IsNullOrEmpty(sMin) || string.IsNullOrEmpty(sMax))
            {

                MessageBox.Show("距离信息不正确", "提示!");
                return false;

            }
            else
            {
                if (Convert.ToDouble(sMin) > Convert.ToDouble(sMax))
                {
                    MessageBox.Show("距离设置信息填写错误", "提示!");
                    return false;
                }
                sDPE.dMinDistance = Convert.ToDouble(sMin);
                sDPE.dMaxDistance = Convert.ToDouble(sMax);

            }

            return true;


        }
        //检查选择的区域和理性
        private bool CheckRegional()
        {
            if (chkAllCity.Checked == false && chkRegional.Checked == false)
            {
                MessageBox.Show("请选择查询的区域", "提示");
                return false;

            }

            else if (chkRegional.Checked == true && m_pSearchGeometry == null)
            {
                MessageBox.Show("请绘制区域", "提示");
                return false;
            }

            else
            {
                return true;

            }

        }
        //拿到选择的设备
        private void SelectedEquipment()
        {
            //当选择全部设备时
            if (cboSB.SelectedIndex == 0)
            {

                for (int i = 1; i < cboSB.Items.Count; i++)
                {
                    if (!sDPE.Pipelinequipment.Contains(cboSB.Items[i].ToString()))
                        sDPE.Pipelinequipment.Add(cboSB.Items[i].ToString());
                }
            }
            else
            {
                if (!sDPE.Pipelinequipment.Contains(cboSB.SelectedItem.ToString()))
                    sDPE.Pipelinequipment.Add(cboSB.SelectedItem.ToString());
            }

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            DeleteOldElement();
            this.Close();
        }

        private void DeleteOldElement()
        {
            IActiveView pAV;
            IGraphicsContainer pContainer;
            IGraphicsContainerSelect pContainerSel;
            IElement pElement;
            //IPointCollection pPointCol;
            IMxDocument pMxDoc;
            pMxDoc = pApp.Document as IMxDocument;
            pAV = pMxDoc.FocusMap as IActiveView;
            pContainer = pAV as IGraphicsContainer;
            pContainerSel = pContainer as IGraphicsContainerSelect;
            pElement = pContainerSel.DominantElement;
            pApp.CurrentTool = m_pOldItem;
            if (pElement != null)
            {
                pContainer.DeleteElement(pElement);
                pAV.Refresh();
                m_pSearchPolygon = null;
                m_pSearchGeometry = null;
            }
        }

    }
}
