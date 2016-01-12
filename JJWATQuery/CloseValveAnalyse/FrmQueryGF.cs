using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using AOBaseLibC;
using JJWATBaseLibC;

namespace JJWATQuery
{
    public partial class FrmQueryGF : Form
    {
        private IApplication pApp;
        private ICommandItem m_pOldItem = null;
        private Boolean m_blnNewDraw;
        private IMap m_pMap;
        private Map m_pMapEvent;
        private IMxDocument pMxDoc;
        private IPolygon m_pSearchPolygon;
        private IGeometry m_pSearchGeometry;
        IList<string> ListGXlayer = new List<string>();               
        string con = "Provider=MSDAORA.1;Password=jjwater;User ID=jjwater;Data Source=jjwater;Persist Security Info=True";
        private CDBCon m_objDBCON = null;
        private SysParameters m_CurParameter =null;    
        strctData4ValveQuery sd = new strctData4ValveQuery();

        public FrmQueryGF()
        {
            InitializeComponent();
          
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
                //设置combox中的Items
                try
                {
                    string sql = "select distinct (t.valvecount) from " + m_CurParameter.GWUserName + ".closevalve t order by t.valvecount";
                    OleDbDataReader dbReader = null;
                    m_objDBCON.ExecuteSQLReturn(sql, ref dbReader);
                    clbNumber.Items.Clear();
                    if (dbReader != null)
                    {
                        while (dbReader.Read())
                        {
                            clbNumber.Items.Add(dbReader["valvecount"].ToString());
                        }

                        ListGXlayer.Add("VALVE");//阀门
                        chkAllCity.Checked = true;
                        btnRegional.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("数据库中没有可查询的关阀方案！");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误！");
                return false;
            }
        }

       

        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            int n = clbNumber.Items.Count;
            if (chkAll.Checked == true)
            {
                chkAllNot.Checked = false;
                for (int i = 0; i < n; i++)
                {
                    clbNumber.SetItemChecked(i, true);
                }
            }
            else
            {
                chkAllNot.Checked = true;
            }

        }

        private void chkAllNot_CheckedChanged(object sender, EventArgs e)
        {
            int n = clbNumber.Items.Count;
            if (chkAllNot.Checked == true)
            {
                chkAll.Checked = false;
                for (int i = 0; i < n; i++)
                {
                    clbNumber.SetItemChecked(i, false);
                }
            }
            else
            {
                chkAll.Checked = true;
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (QueryGFSL() == true)
            {
                FrmQueryResult FrmResult = new FrmQueryResult();
                FrmResult.InitForm(sd, pApp);
                FrmResult.Show();
                this.Close();
            }
            else
            {
                return;
            }
          

        }

        private bool QueryGFSL()
        {
            try
            {
                this.TopMost = false;
                ClsQueryData QueryData = new ClsQueryData();
                QueryData.clear();
                QueryData.clearGroup();
                sd = new strctData4ValveQuery();
                //关阀数量不为空
                if (clbNumber.CheckedItems.Count != 0)
                {
                    List<string> lstCL = new List<string>();
                    for (int i = 0; i < clbNumber.CheckedItems.Count; i++)
                    {
                        lstCL.Add(clbNumber.CheckedItems[i].ToString());
                    }
                    sd.lstCheckLists = lstCL;
                    IList<string> lstVID = new List<string>();

                    //全市查询
                    if (chkAllCity.Checked == true)
                    {
                        sd.bIsAll = true;
                        return true;

                    }
                    //区域查询
                    else if (chkRegional.Checked == true && m_pSearchGeometry != null)
                    {
                        sd.bIsAll = false;
                        lstVID = QueryData.Get_ValveID(m_pMap, m_pSearchGeometry, ListGXlayer);
                        sd.lstValveIDs = (List<string>)lstVID;
                        if (lstVID.Count == 0)
                        {
                            MessageBox.Show("未查询到阀门", "提示!");
                            DeleteOldElement();
                            return false;
                        }
                        return true;
                    }

                    else
                    {
                        MessageBox.Show("操作不正确,不能完成查询！", "提示");
                        return false;

                    }
                }
                else
                {
                    MessageBox.Show("请选择关阀的数量","提示");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }

        }


 

        private void DeleteOldElement()
        {
            IActiveView pAV;
            IGraphicsContainer pContainer;
            IGraphicsContainerSelect pContainerSel;
            IElement pElement;          
            IMxDocument pMxDoc;
            pMxDoc = pApp.Document as IMxDocument;
            pAV = pMxDoc.FocusMap as IActiveView;
            pContainer = pAV as IGraphicsContainer;
            //不是很清楚
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

        private void btnRegional_Click(object sender, EventArgs e)
        {
            //清楚选择区域
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
            //Objects which manage a collection of graphic elements implement this interface
            IGraphicsContainer pContainer = pAV as IGraphicsContainer;
            //Provides access to members that control graphic container selection.
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
            //if (phase.ToString() == "esriViewForeground" && m_pSearchPolygon != null)
            //{
            //    //DrawPolygonXOR(m_pMap, null, m_pSearchPolygon, true);
            //}
        }

        private void FrmQueryGF_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_objDBCON.CloseAndDisposeCon();
        }

    }
}
