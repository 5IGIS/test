using System;
using System.Windows.Forms;
using AOBaseLibC;
using AOBaseLibC.AFCommon;
using AOBaseLibC.AFGeodatabase;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using JJWATSysTool.Tools;
using Utilitys;
using JJWATSysTool;
using ESRI.ArcGIS.CartoUI;
using System.Collections.Generic;

namespace JJWATQuery
{
    public partial class FrmRoadPosition : Form
    {
        public FrmRoadPosition()
        {
            InitializeComponent();
        }

        IApplication m_App;
        Map map;
        IMap m_Map;
        IMxDocument m_MxDoc;
        AFMap m_objMap = new AFMap();

        public void Init(IApplication app)
        {
            m_App = app;
            m_MxDoc = m_App.Document as IMxDocument;
            m_Map = m_MxDoc.FocusMap;
            map = m_Map as Map;
            m_objMap.Map = m_Map;
        }

        /// <summary>
        /// 拾取事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_pickup_Click(object sender, EventArgs e)
        {
            try
            {
                UID uid = new UIDClass();
                uid.Value = "JJWATSysTool.Tools.PickUpRoadTool";
                ICommandItem pSelectTool = m_App.Document.CommandBars.Find(uid, false, false);
                PickUpRoadTool.listBox = listBox1;
                PickUpRoadTool.txtBox = txt_Name;
                if (PickUpRoadTool.pGraphicsContainer != null)
                {
                    PickUpRoadTool.pGraphicsContainer.DeleteAllElements();
                }
                m_App.CurrentTool = pSelectTool;
                map.AfterDraw += new IActiveViewEvents_AfterDrawEventHandler(map_AfterDraw);
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString());
            }
        }

        void map_AfterDraw(ESRI.ArcGIS.Display.IDisplay Display, esriViewDrawPhase phase)
        {
            if (listBox1.Items.Count > 0)
            {
                map.AfterDraw -= map_AfterDraw;
                this.Activate();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_Name.Text = listBox1.SelectedItem.ToString();
        }

        private void FrmRoadPosition_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (PickUpRoadTool.pGraphicsContainer != null)
            {
                PickUpRoadTool.pGraphicsContainer.DeleteAllElements();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 模糊查询道路
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Query_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_Name.Text.Trim() == "")
                {
                    MsgBox.Show("道路名不能为空！");
                    return;
                }
                listBox1.Items.Clear();
                PickUpRoadTool.m_DicFeatures.Clear();
                string[] groups = XMLConfig.GetRoadLayerName().Split(';');
                for (int i = 0; i < groups.Length; i++)
                {
                    if (groups[i].Trim() == "")
                        continue;
                    PickUpRoadTool.strs = groups[i].Split(',');
                    string pFieldName = PickUpRoadTool.strs[1];
                    IQueryFilter pQFilter = new QueryFilterClass();
                    pQFilter.WhereClause = pFieldName + " like '%" + txt_Name.Text.Trim() + "%'";
                    objFLayer = m_objMap.GetLayerByName(PickUpRoadTool.strs[0]);
                    if (objFLayer==null)
                    {
                        MsgBox.Show("当前地图上没有加载'" + PickUpRoadTool.strs[0] + "'图层！");
                        return;
                    }
                    IFeatureCursor pFCursor = objFLayer.FeatureLayer.Search(pQFilter, false);
                    IFeature pFeature = pFCursor.NextFeature();
                    string valve;
                    while (pFeature != null)
                    {
                        valve = pFeature.get_Value(pFeature.Fields.FindField(pFieldName)).ToString();
                        if (!PickUpRoadTool.m_DicFeatures.ContainsKey(objFLayer.LayerTableName))
                        {
                            PickUpRoadTool.m_Dics = new Dictionary<string, IFeature>();
                            PickUpRoadTool.m_Dics.Add(valve, pFeature);
                            PickUpRoadTool.m_DicFeatures.Add(objFLayer.LayerTableName, PickUpRoadTool.m_Dics);
                        }
                        else
                        {
                            if (!PickUpRoadTool.m_DicFeatures[objFLayer.LayerTableName].ContainsKey(valve))
                            {
                                PickUpRoadTool.m_DicFeatures[objFLayer.LayerTableName].Add(valve, pFeature);
                            }
                        }
                        listBox1.Items.Add(valve);
                        pFeature = pFCursor.NextFeature();
                    }
                }
                if (listBox1.Items.Count > 0)
                {
                    listBox1.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 定位所选择道路
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Position_Click(object sender, EventArgs e)
        {
            try
            {
                if (PickUpRoadTool.pGraphicsContainer != null)
                {
                    PickUpRoadTool.pGraphicsContainer.DeleteAllElements();
                }
                foreach (string key in PickUpRoadTool.m_DicFeatures.Keys)
                {
                    if (!PickUpRoadTool.m_DicFeatures[key].ContainsKey(txt_Name.Text))
                    {
                        //MsgBox.Show("对不起,请选择您需要定位的道路！");
                        continue;
                    }
                    PositionGeometry(PickUpRoadTool.m_DicFeatures[key][txt_Name.Text], key);
                    FlashShape(PickUpRoadTool.m_DicFeatures[key][txt_Name.Text]);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 定位图层
        /// </summary>
        /// <param name="pOid"></param>
        public void PositionGeometry(IFeature pf, string name)
        {
            IFeatureSelection pFeatureSel = null;
            objFLayer = m_objMap.GetLayerByName(name);
            pFeatureSel = objFLayer.FeatureLayer as IFeatureSelection;
            pFeatureSel.Clear();
            AFFlash objFlash = new AFFlash(m_App);
            AFGraphOrientation objGraphOri = new AFGraphOrientation(m_App);
            IQueryFilter pFilter = new QueryFilterClass();
            pFeatureSel.Add(pf);
            objGraphOri.OrientFeature(pf);
            objFlash.Features = pf;
            objFlash.StartFlash();
            m_objMap.RefreshMap();
        }

        IIdentifyObj identifyObj;   //闪烁
        int m_FlashNum;             //闪烁次数
        AFFeatureLayer objFLayer;

        /// <summary>
        /// 图形闪烁
        /// </summary>
        private void FlashShape(IFeature pf)
        {
            IFeatureIdentifyObj featureIdentifyObj = new FeatureIdentifyObjectClass();
            featureIdentifyObj.Feature = pf;
            identifyObj = featureIdentifyObj as IIdentifyObj;
            m_FlashNum = 0;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            m_FlashNum++;
            identifyObj.Flash(m_MxDoc.ActiveView.ScreenDisplay);
            if (m_FlashNum > 2)
            {
                timer1.Enabled = false;
            }
        }
    }
}