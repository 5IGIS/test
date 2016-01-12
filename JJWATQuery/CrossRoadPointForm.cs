using System;
using System.Linq;
using System.Windows.Forms;
using AOBaseLibC;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Utilitys;
using AOBaseLibC.AFGeodatabase;


namespace JJWATQuery
{
    public partial class CrossRoadPointForm : Form
    {
        public static IApplication Application;
        private UtilitysMgs Mgs;
        IFeatureLayer fLayer;
        CrossRoadPositionModels models;
        IMxDocument doc;
        IGraphicsContainer cont;
        public static AFMap m_objMap = new AFMap();

        public CrossRoadPointForm()
        {
            InitializeComponent();
        }

        private void CrossRoadPointForm_Load(object sender, EventArgs e)
        {
            //判断是否加载道路中心线图层
            IMap m_pMap;
            Mgs = new UtilitysMgs(Application) { _DataSetName = "DXTDATASET" };//数据集的名称
            Mgs.GetLineLayers();
            string[] groups = XMLConfig.GetRoadLayerName().Split(';');
            string[] strs = null;
            if (groups.Length == 0)
            {
                MsgBox.Show("没查到道路图层，请检查道路配置!");
                return;
            }
            strs = groups[0].Split(',');
            doc = (Application.Document) as IMxDocument;
            m_pMap = doc.FocusMap;
            m_objMap.Map = m_pMap;
            AFFeatureLayer objFeatureLayer = m_objMap.GetLayerByName(strs[0]);
            if (objFeatureLayer != null)
            {
                fLayer = objFeatureLayer.FeatureLayer;
            }
            else
            {
                MessageBox.Show("确认已加载道路中心线图层！");
                this.Close();
            }
            models = new CrossRoadPositionModels();
            IQueryFilter qFilter = new QueryFilterClass();
            qFilter.WhereClause = "";
            IFeatureSelection feas = fLayer as IFeatureSelection;
            IFeatureCursor fCursor = fLayer.Search(qFilter, false);
            IFeature pFeature = fCursor.NextFeature();
            while (pFeature != null)
            {
                CrossRoadPositionModel mode = new CrossRoadPositionModel()
                {
                    FLayer = pFeature,
                    RoadName = pFeature.get_Value(pFeature.Fields.FindField(strs[1])).ToString()
                };
                models.Add(mode);
                pFeature = fCursor.NextFeature();
            }
        }
        //退出
        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //定位
        private void pBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (roadAList.SelectedIndex == -1 || roadBList.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择两条道路！");
                    return;
                }

                string roadNameA = roadAList.SelectedItem.ToString();
                string roadNameB = roadBList.SelectedItem.ToString();

                CrossRoadPositionModel modelA = models.First(a => a.RoadName == roadNameA);
                IPolyline lineA = modelA.FLayer.Shape as IPolyline;
                //IGeometry geoA = modelA.FLayer.Shape;
                //geoA = UnionGeo(geoA, roadNameA);

                CrossRoadPositionModel modelB = models.First(a => a.RoadName == roadNameB);
                IPolyline lineB = modelB.FLayer.Shape as IPolyline;
                //geoB = UnionGeo(geoB, roadNameB);

                ITopologicalOperator to = lineA as ITopologicalOperator;
                //to = to.Buffer(0.001) as ITopologicalOperator;//XY Tolerance、Resolution*10;
                IGeometry geo = to.Intersect(lineB, esriGeometryDimension.esriGeometry0Dimension);
                bool n = false;
                if (geo != null)
                {
                    IPointCollection ps = geo as IPointCollection;
                    if (ps.PointCount > 0)
                    {
                        n = true;
                        IPoint p = ps.get_Point(0);
                        IEnvelope elp = p.Envelope;
                        elp.Width = 30;
                        elp.Height = 30;
                        doc.ActiveView.Extent.CenterAt(p);
                        doc.ActiveView.Extent = elp.Envelope;
                        doc.ActivatedView.FocusMap.MapScale = 2000;

                        IRgbColor pRgbColor = new RgbColorClass();
                        pRgbColor.Green = 0xff;

                        ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbolClass();
                        pSimpleMarkerSymbol.Color = pRgbColor;
                        pSimpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;

                        pSimpleMarkerSymbol.Size = 0xa;

                        IMarkerElement me = new MarkerElementClass();
                        me.Symbol = pSimpleMarkerSymbol as IMarkerSymbol;

                        IElement ele = me as IElement;
                        ele.Geometry = p;

                        cont = doc.ActiveView as IGraphicsContainer;
                        cont.DeleteAllElements();
                        cont.AddElement(ele, 0);
                        doc.ActiveView.Refresh();
                    }
                }
                if (!n)
                {
                    MsgBox.Show("两条道路没有交叉点！");
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString());
            }
        }
        //搜索标点
        private void MarkPintinLargeBuffer(IPointCollection pColMore)
        {
            IPoint point = new PointClass();
            double x = 0.0, y = 0.0;

            for (int i = 0; i < pColMore.PointCount; i++)
            {
                x += pColMore.get_Point(i).X;
                y += pColMore.get_Point(i).Y;
            }

            point.PutCoords(x / pColMore.PointCount, y / pColMore.PointCount);

            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.Red = 255;

            ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbolClass();
            pSimpleMarkerSymbol.Color = pRgbColor;
            pSimpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;

            pSimpleMarkerSymbol.Size = 10;

            IMarkerElement me = new MarkerElementClass();
            me.Symbol = pSimpleMarkerSymbol as IMarkerSymbol;

            IElement ele = me as IElement;
            ele.Geometry = point;

            cont = doc.ActiveView as IGraphicsContainer;
            cont.AddElement(ele, 0);

            IEnvelope elp = doc.ActiveView.Extent;
            elp.CenterAt(point);
            doc.ActiveView.Extent = elp.Envelope;
        }
        //搜索标点
        private void MarkCrossPoint(IPointCollection pCol)
        {
            for (int i = 0; i < pCol.PointCount; i++)
            {

                IPoint point = new PointClass();
                double x = 0.0, y = 0.0;

                //x += pCol.get_Point(i).X;
                //y += pCol.get_Point(i).Y;
                x = pCol.get_Point(i).X;
                y = pCol.get_Point(i).Y;

                point.PutCoords(x, y);
                IRgbColor pRgbColor = new RgbColorClass();
                pRgbColor.Green = 0xff;

                ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbolClass();
                pSimpleMarkerSymbol.Color = pRgbColor;
                pSimpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;

                pSimpleMarkerSymbol.Size = 0xa;

                IMarkerElement me = new MarkerElementClass();
                me.Symbol = pSimpleMarkerSymbol as IMarkerSymbol;

                IElement ele = me as IElement;
                ele.Geometry = point;

                cont = doc.ActiveView as IGraphicsContainer;
                cont.AddElement(ele, 0);
                IEnvelope elp = doc.ActiveView.Extent;
                elp.CenterAt(point);
                doc.ActiveView.Extent = elp.Envelope;
            }
        }

        IGeometry UnionGeo(IGeometry geo, string roadName)
        {
            ITopologicalOperator topoOperator = geo as ITopologicalOperator;
            var item = models.Where<CrossRoadPositionModel>(a => a.RoadName == roadName);
            int count = 0;
            foreach (var o in item)
            {
                geo = topoOperator.Union(o.FLayer.Shape);
                topoOperator = geo as ITopologicalOperator;
                count++;
            }
            return geo;
        }
        //textbox路名改变
        private void roadNameATxt_TextChanged(object sender, EventArgs e)
        {
            string roadName = roadNameATxt.Text;
            if (string.IsNullOrEmpty(roadName))
            {
                return;
            }
            var item = models.Where(a => a.RoadName.Contains(roadName)).GroupBy(a => a.RoadName).Take(7);
            roadAList.Items.Clear();

            foreach (var m in item)
            {
                roadAList.Items.Add(m.Key);
            }
        }
        //textbox路名改变
        private void roadNameBTxt_TextChanged(object sender, EventArgs e)
        {
            string roadName = roadNameBTxt.Text;
            if (string.IsNullOrEmpty(roadName))
            {
                return;
            }
            var item = models.Where(a => a.RoadName.Contains(roadName)).GroupBy(a => a.RoadName).Take(7);
            roadBList.Items.Clear();

            foreach (var m in item)
            {
                roadBList.Items.Add(m.Key);
            }
        }

        private void CrossRoadPointForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (cont != null)
            {
                cont.DeleteAllElements();
                doc.ActivatedView.Refresh();
            }
        }
    }
}