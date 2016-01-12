using System;
using System.Collections.Generic;
using System.Text;
namespace JJWATQuery
{
    public class ClsResultSet
    {
        private string layerName;
        /// <summary>
        /// 图层名称(表名)
        /// </summary>
        public string LayerName
        {
            get { return layerName; }
            set { layerName = value; }
        }
        private string lineID;
        /// <summary>
        /// 管线ID（OBJECTID）
        /// </summary>
        public string LineID
        {
            get { return lineID; }
            set { lineID = value; }
        }
        /// <summary>
        /// 为关阀距离准备
        /// </summary>
        public struct MyStruct
        {
            /// <summary>
            /// 
            /// </summary>
            public string lineID;
            /// <summary>
            /// 
            /// </summary>
            public string lineName;
            /// <summary>
            /// 
            /// </summary>
            public string valveId; 
        }        
        public IList<MyStruct> list = new List<MyStruct>();
        public void addLine(MyStruct m) 
        {
            list.Add(m);
        }
               
        private string valveID;
        /// <summary>
        /// 阀门ID（OBJECTID）
        /// </summary>
        public string ValveID
        {
            get { return valveID; }
            set { valveID = value; }
        }
        private string checkList;
        /// <summary>
        /// 关阀数量
        /// </summary>
        public string CheckList
        {
            get { return checkList; }
            set { checkList = value; }
        }
        private string stopID;
        /// <summary>
        /// 终止设备ID（OBJECTID）
        /// </summary>
        public string StopID
        {
            get { return stopID; }
            set { stopID = value; }
        }
        private string vpath;
        /// <summary>
        /// 阀门到终止设备走过的管线及ID
        /// </summary>
        public string Vpath
        {
            get { return vpath; }
            set { vpath = value; }
        }
        private string distance;
        /// <summary>
        /// 阀门到终止设备距离
        /// </summary>
        public string Distance
        {
            get { return distance; }
            set { distance = value; }
        }
        private string sqlCount;
        /// <summary>
        /// 查询出的数量
        /// </summary>
        public string SqlCount
        {
            get { return sqlCount; }
            set { sqlCount = value; }
        }
        private string sqlStr;
        /// <summary>
        /// 查询条件
        /// </summary>
        public string SqlStr
        {
            get { return sqlStr; }
            set { sqlStr = value; }
        }

        private string clsId;

        public string ClsId
        {
            get { return clsId; }
            set { clsId = value; }
        }


       
        /// <summary>
        /// 图层名称(表名)
        /// </summary>
        /// 
       
        private string disId;
        /// <summary>
        /// 距离序号
        /// </summary>
        public string DisId
        {
            get { return disId; }
            set { disId = value; }
        }

        private string uidPath;

        public string UidPath
        {
            get { return uidPath; }
            set { uidPath = value; }
        }
    }    

}