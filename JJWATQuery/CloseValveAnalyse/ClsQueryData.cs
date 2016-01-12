using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AOBaseLibC.AFGeodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using AOBaseLibC;

namespace JJWATQuery
{
    class ClsQueryData
    {

        /// <summary>
        /// 每种关阀数量所有对的信息
        /// </summary>
        /// 
        private static Dictionary<string, IList<ClsResultSet>> dicResults = new Dictionary<string, IList<ClsResultSet>>();

        public static Dictionary<string, IList<ClsResultSet>> DicResults
        {
            get { return dicResults; }
        }

        private static Dictionary<string, IList<string>> dicGroups = new Dictionary<string, IList<string>>();

        public IList<string> Get_ValveID(IMap pMap, IGeometry iGeom, IList<string> lisGXlayer)
        {
            AFMap objMap = new AFMap();
            objMap.Map = pMap;
            //Dictionary<string, IList<string>> dicGXlayer = new Dictionary<string, IList<string>>();
            IList<string> lisGXobjectid = new List<string>();
            for (int i = 0; i < lisGXlayer.Count; i++)
            {
                try
                {
                    
                    AFFeatureLayer objFeaturelayer = objMap.GetLayerByName(lisGXlayer[i].ToString());
                    IFeatureLayer pDeatureLayer = objFeaturelayer.FeatureLayer;
                    IFeatureClass pFeatureClass = pDeatureLayer.FeatureClass;
                    ISpatialFilter pSFlt = new SpatialFilterClass();
                    pSFlt.Geometry = iGeom;
                    pSFlt.GeometryField = pFeatureClass.ShapeFieldName;
                    pSFlt.set_OutputSpatialReference(pFeatureClass.ShapeFieldName, pMap.SpatialReference);//什么意思？
                    pSFlt.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;//为什么要用包含关系？、??
                    IFeatureCursor pFCursor = pFeatureClass.Search(pSFlt, false);
                    IFeature pFeature = pFCursor.NextFeature();
                    while (pFeature != null)
                    {
                        //获取objectid字段的列号，通过列好获取ID
                        lisGXobjectid.Add(pFeature.get_Value(pFeature.Fields.FindField("objectid")).ToString());
                        pFeature = pFCursor.NextFeature();
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "错误");

                }

            }
            return lisGXobjectid;
        }

        public void clear()
        {
            dicResults.Clear();
        }
        public void clearGroup()
        {
            dicGroups.Clear();
        }
        public void addGroup(string key, IList<string> lis)
        {
            dicGroups.Add(key, lis);
        }
        public Dictionary<string, IList<string>> Get_GXlayer(IMap pMap, IGeometry iGeom, IList<string> lisGXlayer)
        {
            AFMap objMap = new AFMap();
            objMap.Map = pMap;
            Dictionary<string, IList<string>> dicGXlayer = new Dictionary<string, IList<string>>();
            for (int i = 0; i < lisGXlayer.Count; i++)
            {
                try
                {
                    IList<string> lisGXobjectid = new List<string>();
                    AFFeatureLayer objFeaturelayer = objMap.GetLayerByName(lisGXlayer[i].ToString());
                    IFeatureLayer pDeatureLayer = objFeaturelayer.FeatureLayer;
                    IFeatureClass pFeatureClass = pDeatureLayer.FeatureClass;
                    ISpatialFilter pSFlt = new SpatialFilterClass();
                    pSFlt.Geometry = iGeom;
                    pSFlt.GeometryField = pFeatureClass.ShapeFieldName;
                    pSFlt.set_OutputSpatialReference(pFeatureClass.ShapeFieldName, pMap.SpatialReference);
                    pSFlt.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                    IFeatureCursor pFCursor = pFeatureClass.Search(pSFlt, false);
                    IFeature pFeature = pFCursor.NextFeature();
                    while (pFeature != null)
                    {
                        lisGXobjectid.Add(pFeature.get_Value(pFeature.Fields.FindField("objectid")).ToString());
                        pFeature = pFCursor.NextFeature();
                    }
                    if (lisGXobjectid.Count > 0)
                        dicGXlayer.Add(lisGXlayer[i].ToString(), lisGXobjectid);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "错误");

                }

            }
            return dicGXlayer;
        }


    }
}
