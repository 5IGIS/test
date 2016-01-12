using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using AOBaseLibC;
using JJWATBaseLibC;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.ArcMapUI;
using AOBaseLibC.AFCommon;
using ESRI.ArcGIS.Framework;
using Utilitys;


namespace JJWATQuery
{
    /// <summary>
    /// 作者：唐睿
    /// 说明：
    /// </summary>
    public class modMain
    {
        static bool blnInit = false;
        public static bool blnVLY = false;
        public static AFMap m_objMap = new AFMap();
        public static CDBCon m_objDBCON = CDBCon.GetInstance();
        public static SysParameters m_CurParrmeter = SysParameters.GetInstance();
        public static AFAOFunction m_AOFunction = null;
        
        public static void Init(IApplication App)
        {
            if (!blnInit)
            {
                m_AOFunction = new AFAOFunction(App);
                IMxDocument pmx = App.Document as IMxDocument;
                m_objMap.Map = pmx.FocusMap;
                blnInit = true;                
            }
        }

        

        

    }
}
