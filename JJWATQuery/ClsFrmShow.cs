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

        public static IApplication m_App;
        public static Map m_Map = null;
        public static IMap m_pMap = null;
        public static IMxDocument m_MxDoc;
        public static AFMap m_objMap = new AFMap();
        private FrmQueryGF frmQCVNform;
        private FrmQueryRange frmQEPRform;
        private RouteRecordsList frmsp;

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
        /// 打印路由本
        /// </summary>
        public void ShowStatPanelForm()
        {
            if (frmsp == null || frmsp.IsDisposed)
            {
                frmsp = new RouteRecordsList();
                if (frmsp.Init(m_App) == true)
                {
                    frmsp.Show();
                }
            }
            else
            {
                frmsp.Activate();
            }
        }

        public void ShowQueryValvebyNumberForm()
        {
            if (frmQCVNform == null || frmQCVNform.IsDisposed)
            {
                frmQCVNform = new FrmQueryGF();
                if (frmQCVNform.inLication(m_App))
                {
                    frmQCVNform.Show();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("程序遇到错误，请联系技术人员！", "提示！");
                    return;
                }
            }
            else
            {
                frmQCVNform.Activate();
            }
        }
        //显示距离查询界面
        public void ShowQueryEquipmentbyRangerForm()
        {
            if (frmQEPRform == null || frmQEPRform.IsDisposed)
            {
                frmQEPRform = new FrmQueryRange();
                if (frmQEPRform.inLication(m_App))
                {
                    frmQEPRform.Show();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("程序遇到错误，请联系技术人员！", "提示！");
                    frmQEPRform = null;
                    return;
                }
            }
            else
            {
                frmQEPRform.Activate();
            }
        }

        /// <summary>
        /// 统计报表
        /// </summary>
        public void ShowReportForm()
        {
            if (frmReport == null || frmReport.IsDisposed)
            {
                frmReport = new FrmDoState();
                if (frmReport.Init(m_App))
                {
                    frmReport.Show();
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
        public void ShowAreaStatisForm()
        {
            if (frmAreaStatis == null || frmAreaStatis.IsDisposed)
            {
                frmAreaStatis = new FrmAreaStatis();
                frmAreaStatis.Show();
            }
            else
            {
                frmAreaStatis.Activate();
            }
        }


        /// <summary>
        /// 地理位置定位
        /// </summary>
        public void ShowRoadPositionForm(IApplication app)
        {
            XMLConfig.MxdFileName = app.Document.Title;
            if (frmRoad == null || frmRoad.IsDisposed)
            {
                frmRoad = new FrmRoadPosition();
                frmRoad.Init(app);
                frmRoad.Show();
            }
            else
            {
                resultFrm.Activate();
            }
        }

        /// <summary>
        /// 查询选择集
        /// </summary>
        public void ShowResultForm(IApplication app)
        {
            XMLConfig.MxdFileName = app.Document.Title;
            UtilitysResultForm.QueryClass pQuery = new UtilitysResultForm.QueryClass();
            pQuery.isSelQuery = true;
            if (resultFrm == null || resultFrm.IsDisposed)
            {
                resultFrm = new UtilitysResultForm();
                resultFrm.Init(pQuery, app);
                if (!resultFrm.IsShow)
                {
                    resultFrm = null;
                    return;
                }
                resultFrm.Show();
            }
            else
            {
                resultFrm.Activate();
            }
        }

        /// <summary>
        /// 预警分析查询
        /// </summary>
        public void ShowWarnForm(IApplication app)
        {
            XMLConfig.MxdFileName = app.Document.Title;
            if (warn == null || warn.IsDisposed)
            {
                warn = new FrmWarnAnalyse();
                warn.inApplication(app);
                warn.Show();
            }
            else
                warn.Activate();
        }

        /// <summary>
        /// 预警分析设置
        /// </summary>
        public void ShowWarnSetForm(IApplication app)
        {
            XMLConfig.MxdFileName = app.Document.Title;
            if (warnSet == null || warnSet.IsDisposed)
            {
                warnSet = new FrmWarnSet();
                warnSet.inApplication(app);
                warnSet.Show();
            }
            else
                warnSet.Activate();
        }

        /// <summary>
        /// SQL语句查询
        /// </summary>
        public void ShowSQLForm(IApplication app)
        {
            XMLConfig.MxdFileName = app.Document.Title;
            if (querySql == null || querySql.IsDisposed)
            {
                querySql = new FrmQuerySQL();
                querySql.inApplication(app);
                querySql.Show();
            }
            else
                inter.Activate();
        }

        /// <summary>
        /// 竣工时间查询
        /// </summary>
        public void ShowTimeForm(IApplication app)
        {
            XMLConfig.MxdFileName = app.Document.Title;
            if (time == null || time.IsDisposed)
            {
                time = new FrmQueryTime();
                time.inApplication(app);
                time.Show();
            }
            else
                time.Activate();
        }

        /// <summary>
        /// 接口方式
        /// </summary>
        public void ShowInterForm(IApplication app)
        {
            XMLConfig.MxdFileName = app.Document.Title;
            if (inter == null || inter.IsDisposed)
            {
                inter = new FrmInterface();
                inter.inApplication(app);
                inter.Show();
            }
            else
                inter.Activate();
        }

        /// <summary>
        /// 管径查询
        /// </summary>
        public void ShowCaliberForm(IApplication app)
        {
            XMLConfig.MxdFileName = app.Document.Title;
            if (caliber == null || caliber.IsDisposed)
            {
                caliber = new FrmQueryCaliber();
                caliber.inApplication(app);
                caliber.Show();
            }
            else
                caliber.Activate();
        }

        /// <summary>
        /// 分组查询
        /// </summary>
        public void ShowGroupForm(IApplication app)
        {
            XMLConfig.MxdFileName = app.Document.Title;
            if (group == null || group.IsDisposed)
            {
                group = new FrmQueryGroup();
                group.inApplication(app);
                group.Show();
            }
            else
                group.Activate();
        }

        /// <summary>
        /// 材质查询
        /// </summary>
        public void ShowMaterForm(IApplication app)
        {
            XMLConfig.MxdFileName = app.Document.Title;
            if (mater == null || mater.IsDisposed)
            {
                mater = new FrmQueryMaterial();
                mater.inApplication(app);
                mater.Show();
            }
            else
                mater.Activate();
        }

        /// <summary>
        /// 编号查询
        /// </summary>
        public void ShowNumForm(IApplication app)
        {
            XMLConfig.MxdFileName = app.Document.Title;
            if (num == null || num.IsDisposed)
            {
                num = new FrmQueryNum();
                num.inApplication(app);
                num.Show();
            }
            else
                num.Activate();
        }

        /// <summary>
        /// 道路查询
        /// </summary>
        public void ShowRoadForm(IApplication app)
        {
            XMLConfig.MxdFileName = app.Document.Title;
            if (road == null || road.IsDisposed)
            {
                road = new FrmQueryRoad();
                road.inApplication(app);
                road.Show();
            }
            else
                road.Activate();
        }

        /// <summary>
        /// 查询报表
        /// </summary>
        public void ShowSheetForm(IApplication app)
        {
            XMLConfig.MxdFileName = app.Document.Title;
            if (sheet == null || sheet.IsDisposed)
            {
                sheet = new FrmQuerySheet();
                sheet.inApplication(app);
                sheet.Show();
            }
            else
                sheet.Activate();
        }

        /// <summary>
        /// 空间查询
        /// </summary>
        public void ShowSpaceForm(IApplication app)
        {
            XMLConfig.MxdFileName = app.Document.Title;
            if (space == null || space.IsDisposed)
            {
                space = new FrmQuerySpace();
                space.inApplication(app);
                space.Show();
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