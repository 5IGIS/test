using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AOBaseLibC.AFGeodatabase;
using ESRI.ArcGIS.Framework;
using Utilitys;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Display;

namespace JJWATQuery
{
    public partial class CrossRoadPositionForm : Form
    {
        static IApplication _application;
        private UtilitysMgs Mgs;
        IFeatureLayer fLayer;
        string roadFileName = "roadname";
        CrossRoadPositionModels models;
        IMxDocument doc;
        IGraphicsContainer cont;
        
        public static IApplication Application
        {
            get { return _application; }
            set { _application = value; }
        }

        public CrossRoadPositionForm()
        {
            InitializeComponent();
        }

        private void CrossRoadPositionForm_Load(object sender, EventArgs e)
        {
            Mgs = new UtilitysMgs(_application) {_DataSetName = XMLConfig.GetDXT()};
            Mgs.GetLayers();
            var roadNames = XMLConfig.GetRoadCenterLine().Split(new char[]{','});
            modMain.Init(_application);
            if (roadNames.Any())
            {
                var objFeatureLayer = modMain.m_objMap.GetLayerByName(roadNames[0]);
                if (objFeatureLayer != null)
                {
                    fLayer = objFeatureLayer.FeatureLayer;
                }
                else
                {
                    MessageBox.Show(string.Format("没有加载{0}图层！",roadNames[0]),  "错误提示",  MessageBoxButtons.OK,  MessageBoxIcon.Information);
                    this.Close();
                }
            }

            models = new CrossRoadPositionModels();
            doc = _application.Document as IMxDocument;

            IQueryFilter qFilter = new QueryFilterClass();
            qFilter.WhereClause = "1=1";
            IFeatureSelection feas = fLayer as IFeatureSelection;
            IFeatureCursor fCursor = fLayer.Search(qFilter, false);

            IFeature pFeature = fCursor.NextFeature();

            while (pFeature != null)
            {
                CrossRoadPositionModel mode = new CrossRoadPositionModel()
                {
                    FLayer = pFeature,
                    RoadName = pFeature.get_Value(pFeature.Fields.FindField(roadFileName)).ToString()
                };

                models.Add(mode);
                pFeature = fCursor.NextFeature();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            if (cont != null)
            {
                cont.DeleteAllElements();
                doc.ActiveView.Refresh();
            }
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(domainUpDownBL.Text.Trim())) {
                MessageBox.Show("请填写比例尺");
                return;
            }

            if (roadAList.SelectedIndex == -1 || roadBList.SelectedIndex == -1) {
                MessageBox.Show("请选择两条道路！");
                return;
            }

            string roadNameA = roadAList.SelectedItem.ToString();
            string roadNameB = roadBList.SelectedItem.ToString();

            CrossRoadPositionModel modelA = models.First(a => a.RoadName == roadNameA);
            IGeometry geoA = modelA.FLayer.Shape; 
            geoA = UnionGeo(geoA, roadNameA); 

            CrossRoadPositionModel modelB = models.First(a => a.RoadName == roadNameB);
            IGeometry geoB = modelB.FLayer.Shape;
            geoB = UnionGeo(geoB, roadNameB);

            ITopologicalOperator to = geoA as ITopologicalOperator;
            to = to.Buffer(0.1) as ITopologicalOperator;
            IGeometry geo = to.Intersect(geoB, esriGeometryDimension.esriGeometry0Dimension);

            if (geo != null) {

                IPointCollection pCol = geo as IPointCollection;

                if (pCol.PointCount == 0) {

                    MessageBox.Show("两条街道没有交点！");
                    return;
                }

                int bl = Convert.ToInt32(domainUpDownBL.Text);
                doc.ActiveView.FocusMap.MapScale = Convert.ToDouble(domainUpDownBL.Text.Trim());

                IPoint point = new PointClass();

                double x = 0, y = 0;
                

                for (int i = 0; i < pCol.PointCount; i++)
                {
                    x += pCol.get_Point(i).X;
                    y += pCol.get_Point(i).Y;
                }

                point.PutCoords(x / pCol.PointCount, y / pCol.PointCount);

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
                doc.ActiveView.Refresh();
            }
        }

        private IGeometry UnionGeo(IGeometry geo, string roadName)
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

        private void isECBx_CheckedChanged(object sender, EventArgs e)
        {
            if (isECBx.Checked)
            {
                domainUpDownBL.Enabled = false;
            }
            else {
                domainUpDownBL.Enabled = true;
            }
        }


        private void roadNameATxt_TextChanged(object sender, EventArgs e)
        {
            string roadName = roadNameATxt.Text;

            if (string.IsNullOrEmpty(roadName))
            {
                return;
            }

            var item = models.Where(a => a.RoadName.Contains(roadName)).GroupBy(a => a.RoadName).Take(10);

            roadAList.Items.Clear();

            foreach (var m in item)
            {
                roadAList.Items.Add(m.Key);
            }
        }

        private void roadNameBTxt_TextChanged(object sender, EventArgs e)
        {
            string roadName = roadNameBTxt.Text;

            if (string.IsNullOrEmpty(roadName))
            {
                return;
            }

            var item = models.Where(a => a.RoadName.Contains(roadName)).GroupBy(a => a.RoadName).Take(10);

            roadBList.Items.Clear();

            foreach (var m in item)
            {
                roadBList.Items.Add(m.Key);
            }
        }

    }
}
