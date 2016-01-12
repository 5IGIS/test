using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using AOBaseLibC;
using AOBaseLibC.AFGeodatabase;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Utilitys;
namespace JJWATQuery
{
    public partial class FrmQuerySheet : Form
    {
        public FrmQuerySheet()
        {
            InitializeComponent();
        }

        private IApplication m_App = null;
        private IMxDocument m_MxDoc = null;
        private IMap m_pMap = null;
        private UtilitysMgs Mgs;
        private List<string> LineEName = new List<string>();
        private List<string> LineCName = new List<string>();
        private List<string> LineWhere = new List<string>();
        private List<string> m_ListDate = new List<string>();
        string sTypeNamesE,sTypeNamesE1, sLastName;
        private Dictionary<string, string> dListWhere = new Dictionary<string, string>();
        private Dictionary<string, string> dSubType = new Dictionary<string, string>();
        private Dictionary<string, string> dSubTypes = new Dictionary<string, string>();

        private Dictionary<string, string> m_LineSubType = new Dictionary<string, string>();
        private Dictionary<string, string> m_LineSubTypes = new Dictionary<string, string>();

        private Dictionary<string, string> dSubTypeLines = new Dictionary<string, string>();
        private List<string> m_ListType;
        List<string> Layer = new List<string>();
        private lsConDataBll ConData = new lsConDataBll();
        private Dictionary<string, List<string>> dNameList = new Dictionary<string, List<string>>();
        Boolean bYesN = true;
        AFMap objMap = new AFMap();
        IFeatureLayer m_FeatureLayer = null;
        List<string> lisLayerLine = new List<string>();
        List<AFFeatureLayer> m_ObjFLayers = new List<AFFeatureLayer>();
        //List<AFFeatureLayer> m_BFObjFLayers = new List<AFFeatureLayer>();
        IField pField;
        int m_ExecuteResult;

        public void inApplication(IApplication Application)
        {
            m_App = Application;
            m_MxDoc = (m_App.Document) as IMxDocument;
            m_pMap = m_MxDoc.FocusMap;
            Mgs = new UtilitysMgs(m_App);
            objMap.Map = m_pMap;
        }

        private void FrmQuerySheet_Load(object sender, EventArgs e)
        {
            dSubTypeLines.Clear();
            lisLayerLine.Clear();
            lisLayerLine = Mgs.GetLineLayers();
            dNameList = Mgs.GetAllDomains();
            cbTime.Enabled = false;
            DTimeB.Enabled = false;
            cmb_CHECKEDATE.Enabled = false;
            time_CHECKEDATE1.Enabled = false;
            cbTime.SelectedIndex = 0;
            cmb_CHECKEDATE.SelectedIndex = 0;
            Get_LayerName();
        }

        private void Set_Stats()
        {
            try
            {
                clbStats.Items.Clear();
                dListWhere.Clear();
                IFeatureLayer p_FeatureLayer = null;
                if (cbLayer.SelectedIndex != cbLayer.Items.Count - 1)
                {
                    p_FeatureLayer = Mgs.GetFLayer(cbLayer.SelectedItem.ToString());
                }
                else
                {
                    //p_FeatureLayer = objMap.GetLayerByName(lisLayerLine[0]).FeatureLayer;
                    m_ObjFLayers = objMap.GetLayerByWorkSpaceAndShapeType(XMLConfig.GetProName(), esriGeometryType.esriGeometryPolyline);
                    if (m_ObjFLayers.Count == 0)
                    {
                        MsgBox.Show("没有查到管线图层！");
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < m_ObjFLayers.Count; i++)
                        {
                            if (m_ObjFLayers[i].FeatureLayer.Name == XMLConfig.ValveLine())
                            {
                                p_FeatureLayer = m_ObjFLayers[i].FeatureLayer;
                            }
                        }
                    }
                }
                m_FeatureLayer = p_FeatureLayer;
                string sName;
                if (cbLayer.SelectedIndex != cbLayer.Items.Count - 1)
                {
                    for (int i = 0; i < p_FeatureLayer.FeatureClass.Fields.FieldCount; i++)
                    {
                        if (p_FeatureLayer.FeatureClass.Fields.Field[i].Type != esriFieldType.esriFieldTypeGeometry)
                        {
                            sName = p_FeatureLayer.FeatureClass.Fields.Field[i].AliasName;
                            if (sName == "OBJECTID" || sName == "新编号" || sName == "起点编号" || sName == "止点编号")
                            {
                                continue;
                            }
                            clbStats.Items.Add(sName);
                            dListWhere.Add(sName, "");
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < p_FeatureLayer.FeatureClass.Fields.FieldCount; i++)
                    {
                        if (p_FeatureLayer.FeatureClass.Fields.Field[i].Type != esriFieldType.esriFieldTypeGeometry)
                        {
                            sName = p_FeatureLayer.FeatureClass.Fields.Field[i].AliasName;
                            if (sName != "类型")
                            {
                                if (sName == "OBJECTID" || sName == "新编号" || sName == "起点编号" || sName == "止点编号")
                                {
                                    continue;
                                }
                                clbStats.Items.Add(sName);
                                if (!dListWhere.ContainsKey(sName))
                                {
                                    dListWhere.Add(sName, "");
                                }
                            }
                            else
                            {
                                clbStats.Items.Add("类型");
                                for (int j = 0; j < m_ObjFLayers.Count; j++)
                                {
                                    sName = m_ObjFLayers[j].FeatureLayer.FeatureClass.Fields.Field[i].AliasName;
                                    if (!dListWhere.ContainsKey(sName))
                                    {
                                        dListWhere.Add(sName, "");
                                    }
                                }
                                //m_BFObjFLayers = objMap.GetLayerByWorkSpaceAndShapeType("BFGASDATASET", esriGeometryType.esriGeometryPolyline);
                                //for (int j = 0; j < m_BFObjFLayers.Count; j++)
                                //{
                                //    m_ObjFLayers.Add(m_BFObjFLayers[j]);
                                //}
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误提示");
            }
        }

        private void Get_LayerName()
        {
            cbLayer.Items.Clear();
            List<string> plsLayer = new List<string>();
            plsLayer = Mgs.GetLayers();
            if (plsLayer.Count > 0)
            {
                for (int i = 0; i < plsLayer.Count; i++)
                {
                    cbLayer.Items.Add(plsLayer[i]);
                }
                cbLayer.Items.Add("管线");
            }
            cbLayer.SelectedIndex = 0;
        }

        private void cbTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbTime.Text)
            {
                case "大于":
                    sTypeNamesE = ">";
                    break;
                case "大于等于":
                    sTypeNamesE = ">=";
                    break;
                case "等于":
                    sTypeNamesE = "=";
                    break;
                case "小于等于":
                    sTypeNamesE = "<=";
                    break;
                case "小于":
                    sTypeNamesE = "<";
                    break;
                case "介于":
                    sTypeNamesE = "!";
                    break;
            }
            if (sTypeNamesE != "!")
            {
                DTimeE.Enabled = false;
            }
            else
            {
                DTimeE.Enabled = true;
            }
        }

        private void cmb_CHECKEDATE_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmb_CHECKEDATE.Text)
            {
                case "大于":
                    sTypeNamesE1 = ">";
                    break;
                case "大于等于":
                    sTypeNamesE1 = ">=";
                    break;
                case "等于":
                    sTypeNamesE1 = "=";
                    break;
                case "小于等于":
                    sTypeNamesE1 = "<=";
                    break;
                case "小于":
                    sTypeNamesE1 = "<";
                    break;
                case "介于":
                    sTypeNamesE1 = "!";
                    break;
            }
            if (sTypeNamesE != "!")
            {
                time_CHECKEDATE2.Enabled = false;
            }
            else
            {
                time_CHECKEDATE2.Enabled = true;
            }
        }

        private void get_disFeature(string Name)
        {
            IFeatureLayer pFeatureLayer = null;
            IFeatureSelection pFeatureSelect = null;
            ISelectionSet2 pSelectionSet;
            pFeatureLayer = objMap.GetLayerByName(Name).FeatureLayer;
            IDataset pDataset = (IDataset)pFeatureLayer;
            pFeatureSelect = (IFeatureSelection)pFeatureLayer;
            pSelectionSet = (ISelectionSet2)pFeatureSelect.SelectionSet;//管理特点
            if (pSelectionSet.Count > 0)
            {
                ICursor pCursor;//指针
                pSelectionSet.Search(null, false, out pCursor);//返回一个指针
                IFeatureCursor pFeatureCursor = (IFeatureCursor)pCursor;
                IFeature pFeature = pFeatureCursor.NextFeature();
                while (pFeature != null)
                {
                    //sObjID = sObjID + pFeature.get_Value(0) + ",";
                    m_ExecuteResult = BaseCon.ExecuteNonQueryOra(string.Format("insert into jjparameter.STATISSHEET(layername,objectid) values('{0}',{1})", pDataset.BrowseName, pFeature.OID));
                    pFeature = pFeatureCursor.NextFeature();
                }
            }
            //if (string.IsNullOrEmpty(sObjID) == false)
            //{
            //    sObjID = sObjID.Substring(0, sObjID.Length - 1);
            //    DlistFeature.Add(pDataset.BrowseName, sObjID);
            //}
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            try
            {
                string lTimeB = "", lTimnE = ""; string checkTime1 = "", checkTime2 = "";
                IFeatureLayer p_FeatureLayer = null;
                Layer.Clear();
                dGridView.Columns.Clear();
                dGridView.DataSource = null;
                if (cbData.Checked == true)
                {
                    if (cbData.Checked == true)
                    {
                        m_ExecuteResult = 0;
                        BaseCon.ExecuteNonQueryOra("delete from jjparameter.STATISSHEET");
                        if (cbLayer.SelectedIndex == cbLayer.Items.Count - 1)
                        {
                            for (int i = 0; i < lisLayerLine.Count; i++)
                            {
                                get_disFeature(lisLayerLine[i].ToString());
                            }
                        }
                        else
                        {
                            get_disFeature(cbLayer.SelectedItem.ToString());
                        }
                    }
                    if (m_ExecuteResult == 0)
                    {
                        MessageBox.Show("数据集为空!", "提示!");
                        return;
                    }
                }
                if (cbLayer.SelectedIndex != cbLayer.Items.Count - 1)
                {
                    p_FeatureLayer = Mgs.GetFLayer(cbLayer.SelectedItem.ToString());
                    if (p_FeatureLayer != null)
                    {
                        IDataset pData = p_FeatureLayer as IDataset;
                        Layer.Add(pData.BrowseName);
                    }
                    else
                    {
                        MessageBox.Show("选择图层不存在!", "提示!");
                        return;
                    }
                }
                else
                {
                    for (int i = 0; i < lisLayerLine.Count; i++)
                    {
                        p_FeatureLayer = objMap.GetLayerByName(lisLayerLine[i]).FeatureLayer;
                        if (cbData.Checked == true)
                        {
                            IFeatureSelection pFeaSele = p_FeatureLayer as IFeatureSelection;
                            ISelectionSet2 pSelectionSet = (ISelectionSet2)pFeaSele.SelectionSet;//管理特点
                            if (p_FeatureLayer != null && p_FeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline && pSelectionSet.Count > 0)
                            {
                                IDataset pData = p_FeatureLayer as IDataset;
                                Layer.Add(pData.BrowseName);
                            }
                        }
                        else
                        {
                            if (p_FeatureLayer != null && p_FeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                            {
                                IDataset pData = p_FeatureLayer as IDataset;
                                Layer.Add(pData.BrowseName);
                            }
                        }
                    }
                }

                if (clbStats.CheckedItems.Count < 1)
                {
                    MessageBox.Show("请选择分项统计", "提示!");
                    return;
                }
                else
                {
                    LineCName.Clear();
                    LineWhere.Clear();
                    LineEName.Clear();
                    for (int i = 0; i < clbStats.CheckedItems.Count; i++)
                    {
                        int iFied = p_FeatureLayer.FeatureClass.Fields.FindFieldByAliasName(clbStats.CheckedItems[i].ToString());
                        if (iFied > -1)
                        {
                            if (dListWhere.ContainsKey(p_FeatureLayer.FeatureClass.Fields.Field[iFied].AliasName) == true)
                            {
                                if (!string.IsNullOrEmpty(dListWhere[p_FeatureLayer.FeatureClass.Fields.Field[iFied].AliasName]))
                                {
                                    LineCName.Add(p_FeatureLayer.FeatureClass.Fields.Field[iFied].AliasName);
                                    LineEName.Add(p_FeatureLayer.FeatureClass.Fields.Field[iFied].Name + "|" + p_FeatureLayer.FeatureClass.Fields.Field[iFied].Type.ToString());
                                    LineWhere.Add(dListWhere[p_FeatureLayer.FeatureClass.Fields.Field[iFied].AliasName]);
                                }
                            }
                        }
                    }
                    if (LineCName.Count <= 0)
                    {
                        MessageBox.Show("图层没有统计字段", "提示!");
                        return;
                    }
                    //竣工日期
                    if (chbTime.Checked == true)
                    {
                        if ((sTypeNamesE == "!"))
                        {
                            if (DTimeB.Value.Date > DTimeE.Value.Date)
                            {
                                MessageBox.Show("开始时间不可以大于结束时间", "提示!");
                                return;
                            }
                        }
                        lTimeB = Convert.ToString(DTimeB.Value.Date.ToShortDateString());
                        lTimnE = Convert.ToString(DTimeE.Value.Date.ToShortDateString());
                    }
                    else
                    {
                        lTimeB = "";
                        lTimnE = "";
                    }
                    //审核日期
                    if (check_CHECKEDATE.Checked)
                    {
                        if ((sTypeNamesE == "!"))
                        {
                            if (time_CHECKEDATE1.Value.Date > time_CHECKEDATE2.Value.Date)
                            {
                                MessageBox.Show("开始时间不可以大于结束时间", "提示!");
                                return;
                            }
                        }
                        checkTime1 = Convert.ToString(time_CHECKEDATE1.Value.Date.ToShortDateString());
                        checkTime2 = Convert.ToString(time_CHECKEDATE2.Value.Date.ToShortDateString());
                    }
                    else
                    {
                        checkTime1 = "";
                        checkTime2 = "";
                    }
                    this.Cursor = Cursors.WaitCursor;
                    string sCName = string.Empty;
                    System.Data.DataTable dDataTableB = new System.Data.DataTable();
                    System.Data.DataTable dDataTableA = new System.Data.DataTable();
                    int iType = 0;
                    if (cbLayer.SelectedIndex != cbLayer.Items.Count - 1)
                    {
                        if (p_FeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint)
                        {
                            iType = 0;
                            sCName = "个数";
                            dDataTableB = ConData.Get_QuerySheet(Layer, LineEName, lTimeB, lTimnE,checkTime1,checkTime2, LineWhere, sTypeNamesE,sTypeNamesE1, iType, cbData.Checked);
                        }
                        if (p_FeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                        {
                            iType = 1;
                            sCName = "长度(m)";
                            dDataTableB = ConData.Get_QuerySheet(Layer, LineEName, lTimeB, lTimnE, checkTime1, checkTime2, LineWhere, sTypeNamesE, sTypeNamesE1, iType, cbData.Checked);
                        }
                        if (p_FeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                        {
                            iType = 2;
                            sCName = "周长(m)/面积(㎡)";
                            dDataTableB = ConData.Get_QuerySheet(Layer, LineEName, lTimeB, lTimnE, checkTime1, checkTime2, LineWhere, sTypeNamesE, sTypeNamesE1, iType, cbData.Checked);
                        }
                    }
                    else
                    {
                        iType = 1;
                        sCName = "长度(m)";
                        dDataTableB = ConData.Get_QuerySheet(Layer, LineEName, lTimeB, lTimnE, checkTime1, checkTime2, LineWhere, sTypeNamesE, sTypeNamesE1, iType, cbData.Checked);
                    }
                    for (int i = 0; i < dDataTableB.Columns.Count; i++)
                    {
                        DataColumn column = null;
                        if (iType == 2)
                        {
                            if (i == dDataTableB.Columns.Count - 2 || i == dDataTableB.Columns.Count - 1)
                            {
                                if (i == dDataTableB.Columns.Count - 2)
                                {
                                    column = new DataColumn(sCName.Split('/')[0], Type.GetType("System.Decimal"));
                                }
                                else
                                {
                                    column = new DataColumn(sCName.Split('/')[1], Type.GetType("System.Decimal"));
                                }
                            }
                            else
                            {
                                column = new DataColumn(LineCName[i], Type.GetType("System.String"));
                            }
                        }
                        else
                        {
                            if (i == dDataTableB.Columns.Count - 1)
                            {
                                column = new DataColumn(sCName, Type.GetType("System.Decimal"));
                            }
                            else
                            {
                                column = new DataColumn(LineCName[i], Type.GetType("System.String"));
                            }
                        }
                        if (column != null)
                        {
                            dDataTableA.Columns.Add(column);
                        }
                    }
                    for (int j = 0; j < dDataTableB.Rows.Count; j++)
                    {
                        DataRow row = dDataTableA.NewRow();
                        for (int i = 0; i < dDataTableB.Columns.Count; i++)
                        {
                            row[i] = dDataTableB.Rows[j][i].ToString();
                        }
                        dDataTableA.Rows.Add(row);
                    }
                    object[] o = new object[dDataTableA.Columns.Count];
                    if (iType == 2)
                    {
                        Decimal sum = dDataTableA.AsEnumerable().Sum(a => a.Field<Decimal>(sCName.Split('/')[0]));
                        Decimal sum1 = dDataTableA.AsEnumerable().Sum(a => a.Field<Decimal>(sCName.Split('/')[1]));
                        o[0] = "合计";
                        o[o.Length - 1] = sum;
                        o[o.Length - 2] = sum1;
                    }
                    else
                    {
                        Decimal sum = dDataTableA.AsEnumerable().Sum(a => a.Field<Decimal>(sCName));
                        o[0] = "合计";
                        o[o.Length - 1] = sum;
                    }
                    dDataTableA.Rows.Add(o);
                    dGridView.DataSource = dDataTableA;
                    dGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
                    for (int i = 0; i < dGridView.ColumnCount; i++)
                    {
                        if (dGridView.Columns[i].Name == "类型")
                        {
                            for (int j = 0; j < dGridView.RowCount; j++)
                            {
                                string sValue = dGridView.Rows[j].Cells[i].Value.ToString();
                                if (dSubTypes.ContainsKey(sValue))
                                    dGridView.Rows[j].Cells[i].Value = dSubTypes[sValue].ToString();
                            }
                        }
                    }
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "提示");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btn_ExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dGridView.Rows.Count > 0)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Execl files(*.xlsx)|*.xls";
                    saveFileDialog.FilterIndex = 0;
                    saveFileDialog.RestoreDirectory = true;
                    saveFileDialog.CreatePrompt = true;
                    saveFileDialog.Title = "导出Excel文件到";
                    DateTime now = DateTime.Now;
                    object missing = Missing.Value;
                    saveFileDialog.FileName = now.Year.ToString().PadLeft(2) + now.Month.ToString().PadLeft(2, '0') + now.Day.ToString().PadLeft(2, '0') + "-" + now.Hour.ToString().PadLeft(2, '0') + now.Minute.ToString().PadLeft(2, '0') + now.Second.ToString().PadLeft(2, '0');
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Microsoft.Office.Interop.Excel.Application m_objExcel = new Microsoft.Office.Interop.Excel.Application();
                        Microsoft.Office.Interop.Excel.Workbooks m_objWorkBooks = m_objExcel.Workbooks;
                        Microsoft.Office.Interop.Excel.Workbook m_objBook = m_objWorkBooks.Add(true);
                        Microsoft.Office.Interop.Excel.Sheets m_objWorkSheets = m_objBook.Sheets;
                        Microsoft.Office.Interop.Excel.Worksheet m_objWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)m_objWorkSheets[1];
                        this.Cursor = Cursors.WaitCursor;
                        for (int i = 1; i < dGridView.Columns.Count + 1; i++)
                        {
                            m_objExcel.Cells[1, i] = dGridView.Columns[i - 1].Name;
                            for (int j = 1; j < dGridView.Rows.Count + 1; j++)
                            {
                                m_objExcel.Cells[j + 1, i] = dGridView.Rows[j - 1].Cells[dGridView.Columns[i - 1].Name].Value.ToString();
                            }
                        }
                        this.Cursor = Cursors.Default;
                        m_objBook.SaveAs(saveFileDialog.FileName, missing, missing, missing, missing, missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                                    missing, missing, missing, missing, missing);
                        m_objBook.Close(missing, missing, missing);
                        m_objExcel.Workbooks.Close();
                        m_objWorkBooks.Close();
                        m_objExcel.Quit();
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(m_objBook);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(m_objExcel);
                        m_objBook = null;
                        m_objExcel = null;
                        GC.Collect();
                        this.Cursor = Cursors.Default;
                        MsgBox.Show("转出Excel成功！！！");
                    }
                }
                else
                    MsgBox.Show("数据为空不可以导出!");
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString());
            }
        }

        private void cbLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Set_Stats();
            cbAll.Checked = false;
            dGridView.Columns.Clear();
            dGridView.DataSource = null;
            cbData.Checked = false;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            string sOldName, sNewName;
            Boolean bOldStatus, bNewStatus;
            if (clbStats.SelectedIndex != 0)
            {
                bYesN = false;
                sNewName = clbStats.SelectedItem.ToString();
                bNewStatus = clbStats.GetItemChecked(clbStats.SelectedIndex);

                sOldName = clbStats.Items[(clbStats.SelectedIndex - 1)].ToString();
                bOldStatus = clbStats.GetItemChecked(clbStats.SelectedIndex - 1);

                clbStats.Items[(clbStats.SelectedIndex - 1)] = sNewName;
                clbStats.SetItemChecked(clbStats.SelectedIndex - 1, bNewStatus);
                clbStats.SetItemChecked(clbStats.SelectedIndex, bOldStatus);
                clbStats.SelectedIndex = clbStats.SelectedIndex - 1;
                clbStats.Items[(clbStats.SelectedIndex) + 1] = sOldName;
                bYesN = true;
            }
            else
                MessageBox.Show("已经是第一位了!", "提示！");
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            string sOldName, sNewName;
            Boolean bOldStatus, bNewStatus;
            if (clbStats.SelectedIndex != clbStats.Items.Count - 1)
            {
                bYesN = false;
                sNewName = clbStats.SelectedItem.ToString();
                bNewStatus = clbStats.GetItemChecked(clbStats.SelectedIndex);

                sOldName = clbStats.Items[(clbStats.SelectedIndex + 1)].ToString();
                bOldStatus = clbStats.GetItemChecked(clbStats.SelectedIndex + 1);

                clbStats.Items[(clbStats.SelectedIndex + 1)] = sNewName;

                clbStats.SetItemChecked(clbStats.SelectedIndex + 1, bNewStatus);
                clbStats.SetItemChecked(clbStats.SelectedIndex, bOldStatus);


                clbStats.SelectedIndex = clbStats.SelectedIndex + 1;
                clbStats.Items[(clbStats.SelectedIndex) - 1] = sOldName;
                bYesN = true;
            }
            else
                MessageBox.Show("已经是最后一位了!", "提示！");
        }

        private void clbStats_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sWhere;
                if (bYesN)
                {
                    cbAll.Checked = false;
                    sLastName = clbStats.SelectedItem.ToString();
                    clbList.Items.Clear();
                    string strDomainName = "";
                    pField = m_FeatureLayer.FeatureClass.Fields.get_Field(m_FeatureLayer.FeatureClass.Fields.FindFieldByAliasName(clbStats.SelectedItem.ToString()));
                    IDomain pDomain = pField.Domain;
                    if (pDomain != null) strDomainName = pDomain.Description;
                    this.Cursor = Cursors.WaitCursor;
                    cbAll.Enabled = true; cbAll.Visible = true;
                    m_ListType = new List<string>();
                    if (clbStats.SelectedItem.ToString() == "类型")
                    {
                        m_LineSubTypes.Clear();
                        m_LineSubType.Clear();
                        if (cbLayer.SelectedIndex != cbLayer.Items.Count - 1)
                        {
                            dSubType = Mgs.GetSubtypes(m_FeatureLayer);
                            dSubTypes = Mgs.GetSubtypes(m_FeatureLayer.FeatureClass);
                            foreach (var item in dSubType)
                            {
                                m_ListType.Add(item.Key);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < m_ObjFLayers.Count; i++)
                            {
                                dSubType = Mgs.GetSubtypes(m_ObjFLayers[i].FeatureLayer);
                                dSubTypes = Mgs.GetSubtypes(m_ObjFLayers[i].FeatureLayer.FeatureClass);
                                foreach (var item in dSubTypes)
                                {
                                    m_LineSubTypes.Add(item.Key, dSubTypes[item.Key]);
                                }
                                foreach (var item in dSubType)
                                {
                                    m_LineSubType.Add(item.Key, dSubType[item.Key]);
                                    m_ListType.Add(item.Key);
                                }
                            }
                            dSubTypes = m_LineSubTypes;
                            dSubType = m_LineSubType;
                        }
                    }
                    else if (dNameList.ContainsKey(strDomainName))
                    {
                        m_ListType = dNameList[strDomainName];
                    }
                    else
                    {
                        string sql = string.Format("select count(*) as num from(select distinct({0}) from {1} t)", pField.Name, (m_FeatureLayer.FeatureClass as IDataset).Name);
                        DataTable dt = BaseCon.ExecuteQueryOra(sql);
                        sql = string.Format("select {0},count({1}) as num from {2} t group by {3} order by num desc", pField.Name, pField.Name, (m_FeatureLayer.FeatureClass as IDataset).Name, pField.Name);
                        dt = BaseCon.ExecuteQueryOra(sql);
                        foreach (DataRow r in dt.Rows)
                        {
                            m_ListType.Add(r[pField.Name].ToString());
                        }
                        if (m_ListType.Count > 1000)
                        {
                            cbAll.Visible = false;
                        }
                        if (!dNameList.ContainsKey(pField.Name))
                        {
                            dNameList.Add(pField.Name, m_ListType);
                        }
                    }
                    for (int i = 0; i < m_ListType.Count; i++)
                    {
                        clbList.Items.Add(m_ListType[i]);
                    }
                    this.Cursor = Cursors.Default;
                    if (clbStats.GetItemChecked(clbStats.SelectedIndex) != true)
                    {
                        cbAll.Enabled = false;
                        clbList.Enabled = false;
                    }
                    else
                    {
                        cbAll.Enabled = true;
                        clbList.Enabled = true;
                        sWhere = dListWhere[sLastName];
                        if (string.IsNullOrEmpty(sWhere) == false)
                        {
                            sWhere = sWhere.Replace("'", "");
                            string[] sName = sWhere.Split(',');
                            foreach (string sNam in sName)
                            {
                                for (int i = 0; i < clbList.Items.Count; i++)
                                {
                                    if (clbStats.SelectedItem.ToString() == "类型")
                                    {
                                        if (clbList.Items[i].ToString() == dSubTypes[sNam])
                                        {
                                            clbList.SetItemChecked(i, true);
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (clbList.Items[i].ToString() == sNam)
                                        {
                                            clbList.SetItemChecked(i, true);
                                            break;
                                        }
                                    }
                                }
                            }
                            if (clbList.Items.Count == clbList.CheckedItems.Count)
                            {
                                cbAll.Checked = true;
                            }
                            else
                            {
                                cbAll.Checked = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "提示！");
            }
        }


        private void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            if (clbList.Items.Count > 0)
            {
                if (cbAll.Checked == true)
                {
                    for (int i = 0; i < clbList.Items.Count; i++)
                    {
                        clbList.SetItemChecked(i, true);
                    }
                }
                else
                {
                    for (int i = 0; i < clbList.Items.Count; i++)
                    {
                        clbList.SetItemChecked(i, false);
                    }
                }
                clbList.SelectedIndex = 0;
            }
        }

        private void clbList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Get_clbList();
        }

        private void Get_clbList()
        {
            string sWhereName = "";
            if (clbList.CheckedItems.Count > 0)
            {
                dListWhere[sLastName] = "";
                if (clbStats.SelectedItem.ToString() == "类型")
                {
                    for (int i = 0; i < clbList.CheckedItems.Count; i++)
                    {
                        if (cbLayer.SelectedIndex != cbLayer.Items.Count - 1)
                        {
                            sWhereName = sWhereName + "'" + dSubType[clbList.CheckedItems[i].ToString()] + "',";
                        }
                        else
                        {
                            sWhereName = sWhereName + "'" + m_LineSubType[clbList.CheckedItems[i].ToString()] + "',";
                        }
                    }
                    dListWhere[sLastName] = sWhereName.Substring(0, sWhereName.Length - 1);
                }
                else
                {
                    for (int i = 0; i < clbList.CheckedItems.Count; i++)
                    {
                        sWhereName = sWhereName + "'" + clbList.CheckedItems[i].ToString() + "',";
                    }
                    dListWhere[sLastName] = sWhereName.Substring(0, sWhereName.Length - 1);
                }
            }
            else
            {
                //dListWhere[sLastName] = "";
            }
        }

        private void chbTime_CheckedChanged(object sender, EventArgs e)
        {
            if (chbTime.Checked == true)
            {
                cbTime.Enabled = true;
                DTimeB.Enabled = true;
            }
            else
            {
                cbTime.Enabled = false;
                DTimeB.Enabled = false;
            }
            if (sTypeNamesE != "!")
            {
                DTimeE.Enabled = false;
            }
            else
            {
                DTimeE.Enabled = true;
            }
        }

        /// <summary>
        /// 获取唯一值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_GetUniqueKey_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("select count(*) as num from(select distinct({0}) from {1} t)", pField.Name, (m_FeatureLayer.FeatureClass as IDataset).Name);
                DataTable dt = BaseCon.ExecuteQueryOra(sql);
                if (Convert.ToDouble(dt.Rows[0]["num"]) > 1000)
                {
                    if (!MsgBox.Show_Q(m_FeatureLayer.Name + "的" + clbStats.SelectedItem + " 数据大于1000条，是否加载？"))
                        return;
                }
                sql = string.Format("select {0},count({1}) as num from {2} t group by {3} order by num desc", pField.Name, pField.Name, (m_FeatureLayer.FeatureClass as IDataset).Name, pField.Name);
                dt = BaseCon.ExecuteQueryOra(sql);
                foreach (DataRow r in dt.Rows)
                {
                    clbList.Items.Add(r[pField.Name]);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString());
            }
        }

        private void check_CHECKEDATE_CheckedChanged(object sender, EventArgs e)
        {
            if (check_CHECKEDATE.Checked == true)
            {
                cmb_CHECKEDATE.Enabled = true;
                time_CHECKEDATE1 .Enabled = true;
            }
            else
            {
                cmb_CHECKEDATE.Enabled = false;
                time_CHECKEDATE1.Enabled = false;
            }
            if (sTypeNamesE != "!")
            {
                time_CHECKEDATE2.Enabled = false;
            }
            else
            {
                time_CHECKEDATE2.Enabled = true;
            }
        }
    }
}
