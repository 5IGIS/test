using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AOBaseLibC.AFCommon;
using ESRI.ArcGIS.Framework;
using Utilitys;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.ArcMapUI;
using AOBaseLibC;

namespace JJWATQuery
{
    public class ClsFrmShow
    {
        private FrmInterface inter;
        private FrmQueryCaliber caliber;
        private FrmQueryGroup group;
        private FrmQueryMaterial mater;
        private FrmQueryNum num;
        private FrmQueryRoad road;
        private FrmQuerySheet sheet;
        private FrmQuerySpace space;
        private FrmQuerySQL querySql;
        private FrmQueryTime time;
        private FrmWarnAnalyse warn;
        private FrmWarnSet warnSet;
        private UtilitysResultForm resultFrm;
        private CrossRoadPointForm _form;
        private FrmRoadPosition frmRoad;
        private FrmAreaStatis frmAreaStatis;
        private FrmDoState frmReport;
        private RouteRecordsList frmRecord;

        public static IApplication m_App;
        public static Map m_Map = null;
        public static IMap m_pMap = null;
        public static IMxDocument m_MxDoc;
        public static AFMap m_objMap = new AFMap();

        public ClsFrmShow(IApplication app)
        {
            XMLConfig.MxdFileName = app.Document.Title;
            Utilitys.XMLConfig.isBASys = false;
            m_App = app;
            m_MxDoc = (m_App.Document) as IMxDocument;
            m_pMap = m_MxDoc.FocusMap;
            m_Map = m_pMap as Map;
            m_objMap.Map = m_pMap;
        }

        /// <summary>
        /// 打印路由
        /// </summary>
        /// <param name="owner"></param>
        public void ShowRecordList(System.Windows.Forms.IWin32Window owner)
        {
            if (frmRecord == null || frmRecord.IsDisposed)
            {
                frmRecord = new RouteRecordsList();
                frmRecord.Init(m_App);
                frmRecord.Show(owner);
            }
            else
            {
                frmRecord.Activate();
            }
        }

        /// <summary>
        /// 统计报表
        /// </summary>
        public void ShowReportForm(System.Windows.Forms.IWin32Window owner)
        {
            if (frmReport == null || frmReport.IsDisposed)
            {
                frmReport = new FrmDoState();
                if (frmReport.Init(m_App))
                {
                    frmReport.Show(owner);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("程序遇到错误，请联系技术人员！", "提示！");
                    return;
                }
            }
            else
            {
                frmReport.Activate();
            }
        }

        /// <summary>
        /// 显示行政区统计
        /// </summary>
        public void ShowAreaStatisForm(System.Windows.Forms.IWin32Window owner)
        {
            if (frmAreaStatis == null || frmAreaStatis.IsDisposed)
            {
                frmAreaStatis = new FrmAreaStatis();
                frmAreaStatis.Show(owner);
            }
            else
            {
                frmAreaStatis.Activate();
            }
        }


        /// <summary>
        /// 地理位置定位
        /// </summary>
        public void ShowRoadPositionForm(System.Windows.Forms.IWin32Window owner)
        {
            XMLConfig.MxdFileName = m_App.Document.Title;
            if (frmRoad == null || frmRoad.IsDisposed)
            {
                frmRoad = new FrmRoadPosition();
                frmRoad.Init(m_App);
                frmRoad.Show(owner);
            }
            else
            {
                resultFrm.Activate();
            }
        }

        /// <summary>
        /// 查询选择集
        /// </summary>
        public void ShowResultForm(System.Windows.Forms.IWin32Window owner)
        {
            XMLConfig.MxdFileName = m_App.Document.Title;
            UtilitysResultForm.QueryClass pQuery = new UtilitysResultForm.QueryClass();
            pQuery.isSelQuery = true;
            if (resultFrm == null || resultFrm.IsDisposed)
            {
                resultFrm = new UtilitysResultForm();
                resultFrm.Init(pQuery, m_App);
                if (!resultFrm.IsShow)
                {
                    resultFrm = null;
                    return;
                }
                resultFrm.Show(owner);
            }
            else
            {
                resultFrm.Activate();
            }
        }

        /// <summary>
        /// 预警分析查询
        /// </summary>
        public void ShowWarnForm(System.Windows.Forms.IWin32Window owner)
        {
            XMLConfig.MxdFileName = m_App.Document.Title;
            if (warn == null || warn.IsDisposed)
            {
                warn = new FrmWarnAnalyse();
                warn.inApplication(m_App);
                warn.Show(owner);
            }
            else
                warn.Activate();
        }

        /// <summary>
        /// 预警分析设置
        /// </summary>
        public void ShowWarnSetForm(System.Windows.Forms.IWin32Window owner)
        {
            XMLConfig.MxdFileName = m_App.Document.Title;
            if (warnSet == null || warnSet.IsDisposed)
            {
                warnSet = new FrmWarnSet();
                warnSet.inApplication(m_App);
                warnSet.Show(owner);
            }
            else
                warnSet.Activate();
        }

        /// <summary>
        /// SQL语句查询
        /// </summary>
        public void ShowSQLForm(System.Windows.Forms.IWin32Window owner)
        {
            XMLConfig.MxdFileName = m_App.Document.Title;
            if (querySql == null || querySql.IsDisposed)
            {
                querySql = new FrmQuerySQL();
                querySql.inApplication(m_App);
                querySql.Show(owner);
            }
            else
                inter.Activate();
        }

        /// <summary>
        /// 竣工时间查询
        /// </summary>
        public void ShowTimeForm(System.Windows.Forms.IWin32Window owner)
        {
            XMLConfig.MxdFileName = m_App.Document.Title;
            if (time == null || time.IsDisposed)
            {
                time = new FrmQueryTime();
                time.inApplication(m_App);
                time.Show(owner);
            }
            else
                time.Activate();
        }

        /// <summary>
        /// 接口方式
        /// </summary>
        public void ShowInterForm(System.Windows.Forms.IWin32Window owner)
        {
            XMLConfig.MxdFileName = m_App.Document.Title;
            if (inter == null || inter.IsDisposed)
            {
                inter = new FrmInterface();
                inter.inApplication(m_App);
                inter.Show(owner);
            }
            else
                inter.Activate();
        }

        /// <summary>
        /// 管径查询
        /// </summary>
        public void ShowCaliberForm(System.Windows.Forms.IWin32Window owner)
        {
            XMLConfig.MxdFileName = m_App.Document.Title;
            if (caliber == null || caliber.IsDisposed)
            {
                caliber = new FrmQueryCaliber();
                caliber.inApplication(m_App);
                caliber.Show(owner);
            }
            else
                caliber.Activate();
        }

        /// <summary>
        /// 分组查询
        /// </summary>
        public void ShowGroupForm(System.Windows.Forms.IWin32Window owner)
        {
            XMLConfig.MxdFileName = m_App.Document.Title;
            if (group == null || group.IsDisposed)
            {
                group = new FrmQueryGroup();
                group.inApplication(m_App);
                group.Show(owner);
            }
            else
                group.Activate();
        }

        /// <summary>
        /// 材质查询
        /// </summary>
        public void ShowMaterForm(System.Windows.Forms.IWin32Window owner)
        {
            XMLConfig.MxdFileName = m_App.Document.Title;
            if (mater == null || mater.IsDisposed)
            {
                mater = new FrmQueryMaterial();
                mater.inApplication(m_App);
                mater.Show(owner);
            }
            else
                mater.Activate();
        }

        /// <summary>
        /// 编号查询
        /// </summary>
        public void ShowNumForm(System.Windows.Forms.IWin32Window owner)
        {
            XMLConfig.MxdFileName = m_App.Document.Title;
            if (num == null || num.IsDisposed)
            {
                num = new FrmQueryNum();
                num.inApplication(m_App);
                num.Show(owner);
            }
            else
                num.Activate();
        }

        /// <summary>
        /// 道路查询
        /// </summary>
        public void ShowRoadForm(System.Windows.Forms.IWin32Window owner)
        {
            XMLConfig.MxdFileName = m_App.Document.Title;
            if (road == null || road.IsDisposed)
            {
                road = new FrmQueryRoad();
                road.inApplication(m_App);
                road.Show(owner);
            }
            else
                road.Activate();
        }

        /// <summary>
        /// 固定格式报表
        /// </summary>
        public void ShowSheetForm(System.Windows.Forms.IWin32Window owner)
        {
            XMLConfig.MxdFileName = m_App.Document.Title;
            if (sheet == null || sheet.IsDisposed)
            {
                sheet = new FrmQuerySheet();
                sheet.inApplication(m_App);
                sheet.Show(owner);
            }
            else
                sheet.Activate();
        }

        /// <summary>
        /// 空间查询
        /// </summary>
        public void ShowSpaceForm(System.Windows.Forms.IWin32Window owner)
        {
            XMLConfig.MxdFileName = m_App.Document.Title;
            if (space == null || space.IsDisposed)
            {
                space = new FrmQuerySpace();
                space.inApplication(m_App);
                space.Show(owner);
            }
            else
                space.Activate();
        }

        /// <summary>
        /// 交叉路口定位
        /// </summary>
        public void ShowRoadPosForm(IApplication app)
        {
            XMLConfig.MxdFileName = app.Document.Title;
            CrossRoadPointForm.Application = app;
            if (_form == null || _form.IsDisposed)
            {
                _form = new CrossRoadPointForm();
                _form.Show();
            }
            _form.Activate();
        }
    }
}