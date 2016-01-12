using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utilitys;
using ESRI.ArcGIS.Geometry;
using AOBaseLibC.AFGeodatabase;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using System.Reflection;
using Microsoft.Office.Interop.Excel;

namespace JJWATQuery
{
    public partial class FrmAreaStatis : Form
    {
        public FrmAreaStatis()
        {
            InitializeComponent();
        }

        //所有面图层集合
        Dictionary<string, AFFeatureLayer> m_DicLayers;
        //每个图层对应的统计字段
        Dictionary<string, string> m_Fileds;
        //所有行政区数据
        Dictionary<string, IFeature> m_DicFeatures;
        //每个行政区管线长度总和
        Dictionary<string, double> m_DicSums;
        //每个行政区 包含、接边 数据之和
        Dictionary<string, Dictionary<string, double>> m_DicCounts;

        /// <summary>
        /// 行政区统计窗体加载，读取地形图数据集内所有面图层
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmAreaStatis_Load(object sender, EventArgs e)
        {
            try
            {
                m_Fileds = new Dictionary<string, string>();
                m_DicLayers = new Dictionary<string, AFFeatureLayer>();
                string[] groups = XMLConfig.GetDXT().Split(';');
                string[] strs = null;
                if (groups.Length == 0)
                {
                    MsgBox.Show("没查到行政区图层配置!");
                    return;
                }
                comboBox2.Items.Clear();
                for (int i = 0; i < groups.Length; i++)
                {
                    if (groups[i].Trim() == "")
                        continue;
                    strs = groups[i].Split(',');
                    AFFeatureLayer objLayer = ClsFrmShow.m_objMap.GetLayerByName(strs[0]);
                    if (objLayer == null)
                    {
                        MsgBox.Show("当前地图上没有加载'" + strs[0] + "'图层！");
                        return;
                    }
                    comboBox2.Items.Add(strs[0]);
                    if (!m_DicLayers.ContainsKey(strs[0]))
                    {
                        m_DicLayers.Add(strs[0], objLayer);
                        m_Fileds.Add(strs[0], strs[1]);
                    }
                }
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 选择一个面图层读取所有面记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                m_DicFeatures = new Dictionary<string, IFeature>();
                listBox1.Items.Clear();
                IFeatureCursor pCursor = m_DicLayers[comboBox2.Text].FeatureLayer.Search(null, false);
                IFeature pf = pCursor.NextFeature();
                string name = "";
                while (pf != null)
                {
                    name = pf.get_Value(pf.Fields.FindFieldByAliasName(m_Fileds[comboBox2.Text])).ToString();
                    listBox1.Items.Add(name);
                    if (!m_DicFeatures.ContainsKey(name))
                    {
                        m_DicFeatures.Add(name, pf);
                    }
                    pf = pCursor.NextFeature();
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pCursor);
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 统计按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Statis_Click(object sender, EventArgs e)
        {
            try
            {
                int count;
                double lenght;
                string len;
                IFeature pf;
                IFeatureCursor pCursor;
                this.Cursor = Cursors.WaitCursor;
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                IDataStatistics dataStatistics;
                IStatisticsResults statisticsResults;
                ISpatialFilter pSFilter = new SpatialFilterClass();
                Dictionary<string, double> dic;
                m_DicCounts = new Dictionary<string, Dictionary<string, double>>();
                m_DicSums = new Dictionary<string, double>();
                Dictionary<string, AFFeatureLayer> dicPointLayers = ClsFrmShow.m_objMap.GetDicLayerByWorkSpaceAndShapeType(XMLConfig.GetProName(), esriGeometryType.esriGeometryPoint);

                Dictionary<string, AFFeatureLayer> dicLineLayers = ClsFrmShow.m_objMap.GetDicLayerByWorkSpaceAndShapeType(XMLConfig.GetProName(), esriGeometryType.esriGeometryPolyline);

                dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
                //统计之前清空 接边数据 表
                BaseCon.ExecuteNonQueryMdb("delete from DC_STATIS");
                if (comboBox1.Text == "全部")
                {
                    foreach (string key in m_DicFeatures.Keys)
                    {
                        ResultManager.DisplayCol(dataGridView1, key);
                    }
                    foreach (string key in dicPointLayers.Keys)
                    {
                        ResultManager.DisplayRow(dataGridView1, key);
                    }
                    foreach (string key in dicLineLayers.Keys)
                    {
                        ResultManager.DisplayRow(dataGridView1, key);
                    }
                    ResultManager.DisplayRow(dataGridView1, "管线总计");
                    //统计每个行政区 范围内 包含的管点
                    foreach (string layerName in dicPointLayers.Keys)
                    {
                        foreach (string key in m_DicFeatures.Keys)
                        {
                            pSFilter.GeometryField = dicPointLayers[layerName].FeatureLayer.FeatureClass.ShapeFieldName;
                            pSFilter.Geometry = m_DicFeatures[key].Shape;
                            pSFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                            count = dicPointLayers[layerName].FeatureLayer.FeatureClass.FeatureCount(pSFilter);
                            if (!m_DicCounts.ContainsKey(layerName))
                            {
                                dic = new Dictionary<string, double>();
                                dic.Add(key, count);
                                m_DicCounts.Add(layerName, dic);
                            }
                            else
                            {
                                if (!m_DicCounts[layerName].ContainsKey(key))
                                {
                                    m_DicCounts[layerName].Add(key, count);
                                }
                                else
                                {
                                    m_DicCounts[layerName][key] += count;
                                }
                            }
                        }
                    }
                    //统计每个行政区 范围内 接边的管点
                    foreach (string layerName in dicPointLayers.Keys)
                    {
                        foreach (string key in m_DicFeatures.Keys)
                        {
                            pSFilter.GeometryField = dicPointLayers[layerName].FeatureLayer.FeatureClass.ShapeFieldName;
                            pSFilter.Geometry = m_DicFeatures[key].Shape;
                            pSFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelTouches;
                            pCursor = dicPointLayers[layerName].FeatureLayer.Search(pSFilter, false);
                            pf = pCursor.NextFeature();
                            count = 0;
                            while (pf != null)
                            {
                                if (BaseCon.ExecuteQueryMdb(string.Format("select * from DC_STATIS t where t.LAYERNAME='{0}' and t.OID='{1}'", layerName, pf.OID)).Rows.Count > 0)
                                {
                                    pf = pCursor.NextFeature();
                                    continue;
                                }
                                BaseCon.ExecuteNonQueryMdb(string.Format("insert into DC_STATIS(LAYERNAME,OID) values('{0}','{1}')", layerName, pf.OID));
                                count++;
                                pf = pCursor.NextFeature();
                            }
                            if (!m_DicCounts.ContainsKey(layerName))
                            {
                                dic = new Dictionary<string, double>();
                                dic.Add(key, count);
                                m_DicCounts.Add(layerName, dic);
                            }
                            else
                            {
                                if (!m_DicCounts[layerName].ContainsKey(key))
                                {
                                    m_DicCounts[layerName].Add(key, count);
                                }
                                else
                                {
                                    m_DicCounts[layerName][key] += count;
                                }
                            }
                        }
                    }
                    //统计每个行政区 范围内 包含的管线
                    foreach (string layerName in dicLineLayers.Keys)
                    {
                        foreach (string key in m_DicFeatures.Keys)
                        {
                            pSFilter.Geometry = m_DicFeatures[key].Shape;
                            pSFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                            //查询包含在每个行政区下的管线
                            pCursor = dicLineLayers[layerName].FeatureLayer.Search(pSFilter, false);
                            dataStatistics = new DataStatisticsClass();
                            dataStatistics.Field = dicLineLayers[layerName].FeatureLayer.FeatureClass.LengthField.Name;
                            dataStatistics.Cursor = pCursor as ICursor;
                            statisticsResults = dataStatistics.Statistics;
                            lenght = statisticsResults.Sum;
                            if (!m_DicCounts.ContainsKey(layerName))
                            {
                                dic = new Dictionary<string, double>();
                                dic.Add(key, lenght);
                                m_DicCounts.Add(layerName, dic);
                            }
                            else
                            {
                                if (!m_DicCounts[layerName].ContainsKey(key))
                                {
                                    m_DicCounts[layerName].Add(key, lenght);
                                }
                                else
                                {
                                    m_DicCounts[layerName][key] += lenght;
                                }
                            }
                        }
                    }
                    //统计每个行政区 范围内 相接的管线
                    foreach (string layerName in dicLineLayers.Keys)
                    {
                        foreach (string key in m_DicFeatures.Keys)
                        {
                            pSFilter.Geometry = m_DicFeatures[key].Shape;
                            pSFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelCrosses;
                            //查询包含在每个行政区下的管线
                            pCursor = dicLineLayers[layerName].FeatureLayer.Search(pSFilter, false);
                            pf = pCursor.NextFeature();
                            lenght = 0;
                            while (pf != null)
                            {
                                if (BaseCon.ExecuteQueryMdb(string.Format("select * from DC_STATIS t where t.LAYERNAME='{0}' and t.OID='{1}'", layerName, pf.OID)).Rows.Count > 0)
                                {
                                    pf = pCursor.NextFeature();
                                    continue;
                                }
                                BaseCon.ExecuteNonQueryMdb(string.Format("insert into DC_STATIS(LAYERNAME,OID) values('{0}','{1}')", layerName, pf.OID));
                                lenght = lenght + Convert.ToDouble(pf.get_Value(pf.Fields.FindField(dicLineLayers[layerName].FeatureLayer.FeatureClass.LengthField.Name)));
                                pf = pCursor.NextFeature();
                            }
                            if (!m_DicCounts.ContainsKey(layerName))
                            {
                                dic = new Dictionary<string, double>();
                                dic.Add(key, lenght);
                                m_DicCounts.Add(layerName, dic);
                            }
                            else
                            {
                                if (!m_DicCounts[layerName].ContainsKey(key))
                                {
                                    m_DicCounts[layerName].Add(key, lenght);
                                }
                                else
                                {
                                    m_DicCounts[layerName][key] += lenght;
                                }
                            }
                        }
                    }
                    //向数据空间里添加 每个行政区范围内 包含、相交的管线
                    foreach (string layerName in m_DicCounts.Keys)
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            if (dataGridView1.Rows[i].HeaderCell.Value.ToString() == layerName)
                            {
                                foreach (string key in m_DicCounts[layerName].Keys)
                                {
                                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                                    {
                                        if (dataGridView1.Columns[j].HeaderText == key)
                                        {
                                            if (layerName == "配水支线" || layerName == "配水干线" || layerName == "输水管线")
                                            {
                                                if (!m_DicSums.ContainsKey(key))
                                                {
                                                    m_DicSums.Add(key, Convert.ToInt32(m_DicCounts[layerName][key]));
                                                }
                                                else
                                                {
                                                    m_DicSums[key] += Convert.ToInt32(m_DicCounts[layerName][key]);
                                                }
                                            }
                                            len = Convert.ToInt32(m_DicCounts[layerName][key]).ToString("C").Remove(0, 1);
                                            len = len.Substring(0, len.IndexOf('.'));
                                            dataGridView1.Rows[i].Cells[j].Value = len;
                                            System.Windows.Forms.Application.DoEvents();
                                            break;
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                    //统计管线总和
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        if (m_DicSums.ContainsKey(dataGridView1.Columns[j].HeaderText))
                        {
                            len = Convert.ToInt32(m_DicSums[dataGridView1.Columns[j].HeaderText]).ToString("C").Remove(0, 1);
                            len = len.Substring(0, len.IndexOf('.'));
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[j].Value = len;
                        }
                    }
                }
                else if (comboBox1.Text == "单个行政区")
                {
                    if (listBox1.SelectedItem == null)
                    {
                        MsgBox.Show("请选择您要统计的行政区！");
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    ResultManager.DisplayCol(dataGridView1, listBox1.SelectedItem.ToString());
                    foreach (string key in dicPointLayers.Keys)
                    {
                        ResultManager.DisplayRow(dataGridView1, key);
                    }
                    foreach (string key in dicLineLayers.Keys)
                    {
                        ResultManager.DisplayRow(dataGridView1, key);
                    }
                    ResultManager.DisplayRow(dataGridView1, "管线总计");
                    //统计每个行政区 范围内 包含的管点
                    foreach (string layerName in dicPointLayers.Keys)
                    {
                        pSFilter.GeometryField = dicPointLayers[layerName].FeatureLayer.FeatureClass.ShapeFieldName;
                        pSFilter.Geometry = m_DicFeatures[listBox1.SelectedItem.ToString()].Shape;
                        pSFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                        count = dicPointLayers[layerName].FeatureLayer.FeatureClass.FeatureCount(pSFilter);
                        if (!m_DicCounts.ContainsKey(layerName))
                        {
                            dic = new Dictionary<string, double>();
                            dic.Add(listBox1.SelectedItem.ToString(), count);
                            m_DicCounts.Add(layerName, dic);
                        }
                        else
                        {
                            if (!m_DicCounts[layerName].ContainsKey(listBox1.SelectedItem.ToString()))
                            {
                                m_DicCounts[layerName].Add(listBox1.SelectedItem.ToString(), count);
                            }
                            else
                            {
                                m_DicCounts[layerName][listBox1.SelectedItem.ToString()] += count;
                            }
                        }
                    }
                    //统计每个行政区 范围内 接边的管点
                    foreach (string layerName in dicPointLayers.Keys)
                    {
                        pSFilter.GeometryField = dicPointLayers[layerName].FeatureLayer.FeatureClass.ShapeFieldName;
                        pSFilter.Geometry = m_DicFeatures[listBox1.SelectedItem.ToString()].Shape;
                        pSFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelTouches;
                        pCursor = dicPointLayers[layerName].FeatureLayer.Search(pSFilter, false);
                        pf = pCursor.NextFeature();
                        count = 0;
                        while (pf != null)
                        {
                            if (BaseCon.ExecuteQueryMdb(string.Format("select * from DC_STATIS t where t.LAYERNAME='{0}' and t.OID='{1}'", layerName, pf.OID)).Rows.Count > 0)
                            {
                                pf = pCursor.NextFeature();
                                continue;
                            }
                            BaseCon.ExecuteNonQueryMdb(string.Format("insert into DC_STATIS(LAYERNAME,OID) values('{0}','{1}')", layerName, pf.OID));
                            count++;
                            pf = pCursor.NextFeature();
                        }
                        if (!m_DicCounts.ContainsKey(layerName))
                        {
                            dic = new Dictionary<string, double>();
                            dic.Add(listBox1.SelectedItem.ToString(), count);
                            m_DicCounts.Add(layerName, dic);
                        }
                        else
                        {
                            if (!m_DicCounts[layerName].ContainsKey(listBox1.SelectedItem.ToString()))
                            {
                                m_DicCounts[layerName].Add(listBox1.SelectedItem.ToString(), count);
                            }
                            else
                            {
                                m_DicCounts[layerName][listBox1.SelectedItem.ToString()] += count;
                            }
                        }
                    }
                    //统计每个行政区 范围内 包含的管线
                    foreach (string layerName in dicLineLayers.Keys)
                    {
                        foreach (string key in m_DicFeatures.Keys)
                        {
                            pSFilter.Geometry = m_DicFeatures[key].Shape;
                            pSFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                            //查询包含在每个行政区下的管线
                            pCursor = dicLineLayers[layerName].FeatureLayer.Search(pSFilter, false);
                            dataStatistics = new DataStatisticsClass();
                            dataStatistics.Field = dicLineLayers[layerName].FeatureLayer.FeatureClass.LengthField.Name;
                            dataStatistics.Cursor = pCursor as ICursor;
                            statisticsResults = dataStatistics.Statistics;
                            lenght = statisticsResults.Sum;
                            if (!m_DicCounts.ContainsKey(layerName))
                            {
                                dic = new Dictionary<string, double>();
                                dic.Add(key, lenght);
                                m_DicCounts.Add(layerName, dic);
                            }
                            else
                            {
                                if (!m_DicCounts[layerName].ContainsKey(key))
                                {
                                    m_DicCounts[layerName].Add(key, lenght);
                                }
                                else
                                {
                                    m_DicCounts[layerName][key] += lenght;
                                }
                            }
                        }
                    }
                    //统计每个行政区 范围内 相接的管线
                    foreach (string layerName in dicLineLayers.Keys)
                    {
                        pSFilter.Geometry = m_DicFeatures[listBox1.SelectedItem.ToString()].Shape;
                        pSFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelCrosses;
                        //查询包含在每个行政区下的管线
                        pCursor = dicLineLayers[layerName].FeatureLayer.Search(pSFilter, false);
                        pf = pCursor.NextFeature();
                        lenght = 0;
                        while (pf != null)
                        {
                            if (BaseCon.ExecuteQueryMdb(string.Format("select * from DC_STATIS t where t.LAYERNAME='{0}' and t.OID='{1}'", layerName, pf.OID)).Rows.Count > 0)
                            {
                                pf = pCursor.NextFeature();
                                continue;
                            }
                            BaseCon.ExecuteNonQueryMdb(string.Format("insert into DC_STATIS(LAYERNAME,OID) values('{0}','{1}')", layerName, pf.OID));
                            lenght = lenght + Convert.ToDouble(pf.get_Value(pf.Fields.FindField(dicLineLayers[layerName].FeatureLayer.FeatureClass.LengthField.Name)));
                            pf = pCursor.NextFeature();
                        }
                        if (!m_DicCounts.ContainsKey(layerName))
                        {
                            dic = new Dictionary<string, double>();
                            dic.Add(listBox1.SelectedItem.ToString(), lenght);
                            m_DicCounts.Add(layerName, dic);
                        }
                        else
                        {
                            if (!m_DicCounts[layerName].ContainsKey(listBox1.SelectedItem.ToString()))
                            {
                                m_DicCounts[layerName].Add(listBox1.SelectedItem.ToString(), lenght);
                            }
                            else
                            {
                                m_DicCounts[layerName][listBox1.SelectedItem.ToString()] += lenght;
                            }
                        }
                    }
                    //向数据空间里添加 每个行政区范围内 包含、相交的管线
                    foreach (string layerName in m_DicCounts.Keys)
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            if (dataGridView1.Rows[i].HeaderCell.Value.ToString() == layerName)
                            {
                                foreach (string key in m_DicCounts[layerName].Keys)
                                {
                                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                                    {
                                        if (dataGridView1.Columns[j].HeaderText == key)
                                        {
                                            if (layerName == "配水支线" || layerName == "配水干线" || layerName == "输水管线")
                                            {
                                                if (!m_DicSums.ContainsKey(key))
                                                {
                                                    m_DicSums.Add(key, Convert.ToInt32(m_DicCounts[layerName][key]));
                                                }
                                                else
                                                {
                                                    m_DicSums[key] += Convert.ToInt32(m_DicCounts[layerName][key]);
                                                }
                                            }
                                            len = Convert.ToInt32(m_DicCounts[layerName][key]).ToString("C").Remove(0, 1);
                                            len = len.Substring(0, len.IndexOf('.'));
                                            dataGridView1.Rows[i].Cells[j].Value = len;
                                            System.Windows.Forms.Application.DoEvents();
                                            break;
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                    //统计管线总和
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        if (m_DicSums.ContainsKey(dataGridView1.Columns[j].HeaderText))
                        {
                            len = Convert.ToInt32(m_DicSums[dataGridView1.Columns[j].HeaderText]).ToString("C").Remove(0, 1);
                            len = len.Substring(0, len.IndexOf('.'));
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[j].Value = len;
                        }
                    }
                }
                else if (comboBox1.Text == "2个行政区交集")
                {

                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 判断坐标点是否落在指定的多边形区域内
        /// </summary>
        /// <param name="point">指定的坐标点</param>
        /// <param name="list">多变形区域的节点集合</param>
        /// <returns>True 落在范围内 False 不在范围内</returns>
        //public bool IsWithIn(IPoint point, List<IPoint> list)
        //{
        //    double x = point.X;
        //    double y = point.Y;

        //    int isum, icount, index;
        //    double dLon1 = 0, dLon2 = 0, dLat1 = 0, dLat2 = 0, dLon;

        //    if (list.Count < 3)
        //    {
        //        return false;
        //    }

        //    isum = 0;
        //    icount = list.Count;

        //    try
        //    {
        //        for (index = 0; index < icount - 1; index++)
        //        {
        //            if (index == icount - 1)
        //            {
        //                dLon1 = list[index].X;
        //                dLat1 = list[index].Y;
        //                dLon2 = list[0].X;
        //                dLat2 = list[0].Y;
        //            }
        //            else
        //            {
        //                dLon1 = list[index].X;
        //                dLat1 = list[index].Y;
        //                dLon2 = list[index + 1].X;
        //                dLat2 = list[index + 1].Y;
        //            }

        //            if (((y >= dLat1) && (y < dLat2)) || ((y >= dLat2) && (y < dLat1)))
        //            {
        //                if (Math.Abs(dLat1 - dLat2) > 0)
        //                {
        //                    dLon = dLon1 - ((dLon1 - dLon2) * (dLat1 - y)) / (dLat1 - dLat2);
        //                    if (dLon < x)
        //                        isum++;
        //                }
        //            }
        //        }
        //    }
        //    catch { }

        //    if ((isum % 2) != 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 转出Excel文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Execl files(*.xlsx)|*.xls";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.CreatePrompt = true;
                saveFileDialog.Title = "导出Excel文件到";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    object missing = Missing.Value;
                    Microsoft.Office.Interop.Excel.Application m_objExcel = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbooks m_objWorkBooks = m_objExcel.Workbooks;
                    Microsoft.Office.Interop.Excel.Workbook m_objBook = m_objWorkBooks.Add(true);
                    Microsoft.Office.Interop.Excel.Sheets m_objWorkSheets = m_objBook.Sheets; ;
                    Microsoft.Office.Interop.Excel.Worksheet m_objWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)m_objWorkSheets[1];
                    int index = (dataGridView1.Columns.Count + 1) % 2 == 0 ? (dataGridView1.Columns.Count + 1) / 2 : Convert.ToInt32((dataGridView1.Columns.Count + 1) / 2) + 1;
                    m_objExcel.Cells[1, index] = "行政区统计报表";
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        m_objExcel.Cells[2, i + 2] = dataGridView1.Columns[i].HeaderText;
                    }
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        m_objExcel.Cells[i + 3, 1] = dataGridView1.Rows[i].HeaderCell.Value;
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            m_objExcel.Cells[i + 3, j + 2] = dataGridView1.Rows[i].Cells[j].Value;
                        }
                    }
                    Range rangeTitle = m_objWorkSheet.get_Range(m_objExcel.Cells[1, index], m_objExcel.Cells[1, index]);
                    rangeTitle.Font.Size = 14;
                    rangeTitle.Font.Bold = true;
                    //合并单元格
                    rangeTitle.Merge(rangeTitle.MergeCells);
                    m_objWorkSheet.get_Range("A1", m_objExcel.Cells[1, dataGridView1.Columns.Count + 1]).Merge(m_objWorkSheet.get_Range("A1", m_objExcel.Cells[1, dataGridView1.Columns.Count + 1]).MergeCells);
                    Range rangeContent = m_objWorkSheet.get_Range(m_objExcel.Cells[1, 1], m_objExcel.Cells[2 + dataGridView1.Rows.Count, dataGridView1.Columns.Count + 1]);
                    rangeContent.EntireColumn.AutoFit();
                    rangeContent.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    m_objExcel.DisplayAlerts = false;
                    m_objExcel.AlertBeforeOverwriting = false;
                    //自动调整列宽
                    rangeContent.EntireColumn.AutoFit();
                    rangeContent.Borders.LineStyle = 1;
                    rangeContent.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThick, XlColorIndex.xlColorIndexAutomatic, System.Drawing.Color.Black.ToArgb());
                    m_objBook.SaveAs(saveFileDialog.FileName, missing, missing, missing, missing, missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                            missing, missing, missing, missing, missing);
                    m_objBook.Close(missing, missing, missing);
                    m_objExcel.Workbooks.Close();
                    m_objWorkBooks.Close();
                    m_objExcel.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(m_objBook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(m_objExcel);
                    GC.Collect();
                    m_objBook = null;
                    m_objExcel = null;
                    this.Cursor = Cursors.Default;
                    MsgBox.Show("转出Excel成功！！！");
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString());
            }
        }
    }
}