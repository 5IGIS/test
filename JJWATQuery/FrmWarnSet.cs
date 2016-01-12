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
using Utilitys;
using ESRI.ArcGIS.Carto;
using System.Xml;
using System.IO;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using System.Text.RegularExpressions;
using AOBaseLibC;
namespace JJWATQuery
{
    public partial class FrmWarnSet : Form
    {
        public FrmWarnSet()
        {
            InitializeComponent();
        }

        private IApplication m_App = null;
        private UtilitysMgs Mgs;
        string sLineNames;
        string[] LineVales;
        private List<string> m_lsLayer = new List<string>();
        private Dictionary<string, string> dType = new Dictionary<string, string>();
        private Dictionary<string, string> dSqlWhere = new Dictionary<string, string>();
        FrmWarnAnalyse QueryFrom;
        Dictionary<string, List<string>> dItem = new Dictionary<string, List<string>>();
        Boolean bKG;
        AFMap afMap = new AFMap();
        private lsConDataBll ConData = new lsConDataBll();
        public void inApplication(IApplication Application)
        {
            m_App = Application;
            string pLineVale;
            IMxDocument pDocument = m_App.Document as IMxDocument;
            afMap.Map = pDocument.FocusMap;
            LineVales = XMLConfig.FrmWarnSetLine().Split(',');
            pLineVale = LineVales[0];
            sLineNames = LineVales[1];
            Mgs = new UtilitysMgs(m_App);
            dItem = Mgs.GetDomainsByName(pLineVale);
            Mgs.GeoType = esriGeometryType.esriGeometryPoint;
            Mgs.GetLayers();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lbLayer.SelectedIndex == -1)
            {
                MessageBox.Show("请选择图层", "提示");
                return;
            }
            if (lbSelect.SelectedIndex == -1)
            {
                MessageBox.Show("请选择类型方式", "提示");
                return;
            }
        }

        private void FrmWarnSet_Shown(object sender, EventArgs e)
        {
            dSqlWhere.Clear();
            m_lsLayer = ConData.get_LayerNameC();
            if (m_lsLayer.Count > 0)
            {
                for (int i = 0; i < m_lsLayer.Count; i++)
                {
                    lbLayer.Items.Add(m_lsLayer[i]);
                }

                lbLayer.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("未配置参数设置", "提示!");
                this.Close();
            }
        }
        private void lbLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            IFeatureLayer pFLayer = null;
            if (lbLayer.SelectedItem.ToString() != "管线")
            {
                pFLayer = Mgs.GetFLayer(lbLayer.SelectedItem.ToString());

            }
            else
            {
                pFLayer = afMap.GetLayerByName(sLineNames).FeatureLayer;
            }
            dType.Clear();
            lbSelect.Items.Clear();
            if (pFLayer == null)
            {
                Status();
                bKG = false;
                upNum.Value = 0;
                upYear.Value = 0;
                cbMonth.SelectedIndex = 0;
                bKG = true;
                return;
            }
            else
            {
                if (pFLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolyline)
                {
                    dType = Mgs.GetSubtypes(pFLayer);
                }
                else
                {
                    List<string> lItem = new List<string>();
                    foreach (var item in dItem.Keys)
                    {
                        lItem = dItem[item.ToString()];
                        for (int i = 0; i < lItem.Count; i++)
                        {
                            dType.Add(lItem[i].ToString(), lItem[i].ToString());
                        }
                    }
                }
                Status();
                foreach (object obj in dType.Keys)
                {
                    lbSelect.Items.Add(obj.ToString());
                }
                if (lbSelect.Items.Count > 0)
                {
                    lbSelect.SelectedIndex = 0;
                }
            }
        }
        private void Status()
        {
            if (dType.Count == 0)
            {
                upNum.Enabled = false;
                upYear.Enabled = false;
                cbMonth.Enabled = false;
                return;
            }
            else
            {
                upNum.Enabled = true;
                upYear.Enabled = true;
                cbMonth.Enabled = true;
            }
        }
        private void lbSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            bKG = false;
            set_lbSelect(lbSelect.SelectedItem.ToString());
            bKG = true;
        }
        private void set_lbSelect(string sSelect)
        {
            string code, name, slist = "";
            code = dType[sSelect];
            name = sSelect;
            slist = ConData.get_LayerNameC(code, name);
            if (string.IsNullOrEmpty(slist) == false)
            {
                upNum.Value = Convert.ToInt16(slist.Split('|')[0]);
                upYear.Value = Convert.ToInt16(slist.Split('|')[1]);
                cbMonth.SelectedIndex = Convert.ToInt16(slist.Split('|')[2]);
            }
            else
            {
                upNum.Value = 0;
                upYear.Value = 0;
                cbMonth.SelectedIndex = 0;
            }
        }
        private void set_Where()
        {
            string Swhere = "";
            if (upNum.Value == 0 && upYear.Value == 0 && Convert.ToInt16(cbMonth.SelectedItem.ToString()) == 0)
            {
                if (dSqlWhere.ContainsKey(dType[lbSelect.SelectedItem.ToString()] + "|" + lbSelect.SelectedItem.ToString()) == true)
                {
                    string sLaName = dType[lbSelect.SelectedItem.ToString()] + "|" + lbSelect.SelectedItem.ToString();
                    string sWhere = dSqlWhere[dType[lbSelect.SelectedItem.ToString()] + "|" + lbSelect.SelectedItem.ToString()];
                    ConData.Set_ExecuteSQL(sLaName, sWhere, 0);
                    dSqlWhere.Remove(dType[lbSelect.SelectedItem.ToString()] + "|" + lbSelect.SelectedItem.ToString());
                }
            }
            else
            {
                if (dSqlWhere.ContainsKey(dType[lbSelect.SelectedItem.ToString()] + "|" + lbSelect.SelectedItem.ToString()) == true)
                {
                    Swhere = get_SQL();
                    dSqlWhere[dType[lbSelect.SelectedItem.ToString()] + "|" + lbSelect.SelectedItem.ToString()] = Swhere;
                }
                else
                {
                    dSqlWhere.Add(dType[lbSelect.SelectedItem.ToString()] + "|" + lbSelect.SelectedItem.ToString(), get_SQL());
                }

            }
        }
        /// <summary>
        /// 获得SQL语句
        /// </summary>
        /// <returns>SQL语句</returns>
        private string get_SQL()
        {
            string sSql = "";
            string sYear = "", sMonth = "", sNum = "";
            if (upNum.Enabled == true)
            {
                if (upNum.Value != 0)
                {
                    sNum = (upNum.Value.ToString());
                }
                if (upYear.Value != 0)
                {
                    sYear = (upYear.Value.ToString());
                }
                if (Convert.ToInt16(cbMonth.SelectedItem.ToString()) != 0)
                {
                    sMonth = (cbMonth.SelectedItem.ToString());
                }
                if (string.IsNullOrEmpty(sNum))
                {
                    sNum = "0";
                }
                if (string.IsNullOrEmpty(sYear))
                {
                    sYear = "0";
                }
                if (string.IsNullOrEmpty(sMonth))
                {
                    sMonth = "0";
                }
                sSql = sNum + "|" + sYear + "|" + sMonth;
            }
            return sSql;
        }
        private void upYear_ValueChanged(object sender, EventArgs e)
        {
            cbMonth.SelectedIndex = 0;
            if (bKG == true)
            {
                set_Where();
                set_Save();
            }
        }
        private void upNum_ValueChanged(object sender, EventArgs e)
        {
            if (bKG == true)
            {
                set_Where();
                set_Save();
            }
        }
        private void cbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bKG == true)
            {
                set_Where();
                set_Save();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnOK_Click_1(object sender, EventArgs e)
        {
            set_Where();
            set_Save();
            this.Close();
        }
        private void set_Save()
        {
            if (dSqlWhere.Count > 0)
            {
                foreach (var vKey in dSqlWhere.Keys)
                {
                    string sLaName, sWhere;
                    sLaName = vKey.ToString();
                    sWhere = dSqlWhere[vKey];
                    ConData.Set_ExecuteSQL(sLaName, sWhere, 1);
                }
            }
        }
        private void btnFrom_Click(object sender, EventArgs e)
        {
            set_Where();
            set_Save();
            if (QueryFrom == null || QueryFrom.IsDisposed)
            {
                QueryFrom = new FrmWarnAnalyse();
                QueryFrom.inApplication(m_App);
                QueryFrom.Show();
            }
            else
            {
                QueryFrom.Activate();
            }
            this.Close();
        }
    }
}
