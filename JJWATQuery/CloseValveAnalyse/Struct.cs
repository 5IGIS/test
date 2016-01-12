using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJWATQuery
{    
    
    public struct strctData4ValveQuery
    {        
        //勾选的关阀数量
        public List<string> lstCheckLists;
        //关闭阀门ID
        public List<string> lstValveIDs;
        //是否选择了全市进行查询
       
        //存储选择的管网设备：水表、消火栓、节点
        public List<string> Pipelinequipment;
        //查询的最大距离范围
        public double dMaxDistance;
        //查询的最小距离范围
        public double dMinDistance;
        //全市范围查询？
        public bool bIsAll;
        //区域查询后给出阀门列表，否则为空;
        public IList<string> lstStrValveIDs;
    }

    //struct strctData4PilelinEquipmentQuery
    //{
    //  //存储选择的管网设备：水表、消火栓、节点
    //  public List<string> Pipelinequipment;
    //  //查询的最大距离范围
    //  public double dMaxDistance;
    //  //查询的最小距离范围
    //  public double dMinDistance;
    //  //全市范围查询？
    //  public bool bIsAll;
    //  //区域查询后给出阀门列表，否则为空;
    //  public List<string>lstStrValveIDs;


    //}


}
