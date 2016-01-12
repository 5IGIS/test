using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JJWATBaseLibC;
using System.Data.OleDb;

namespace JJWATQuery
{
    class CalculateProjectCost
    {
        private CDBCon m_objDBCON = CDBCon.GetInstance();
        private SysParameters m_CurParameter = SysParameters.GetInstance();
        //管网表名
        const string mc_Tn_PipeMain = "PIPESECTIONMAIN";
        const string mc_Tn_PipeUser = "PIPESECTIONUSER";
        const string mc_Tn_PipeSource = "PIPESECTIONSOURCE";      

        const string mc_strXSTable = "PIPEQUOTIETY";
        const string mc_strPipePrjTable = "PIPEPROJECT";
                
        
        //批量执行选中行的原值计算
        /// <summary>
        /// 计算选中行的原值，系数从数据库中读取；计算成功 0 失败1
        /// </summary>
        /// <param name="strSelectedPipeFile">记录的档号</param>
        public int CalculateProjectCosting(string strSelectedPipeFile)
        {
            double dblXiShu = 0.0;
            OleDbDataReader adoReader = null;
            string strSQLXS = string.Format("select xishu from jjparameter.PIPEPROJECT where pipefile='{0}'", strSelectedPipeFile);
            m_objDBCON.ExecuteSQLReturn(strSQLXS, ref adoReader);
            if (adoReader.Read())
            {
                if (adoReader.GetValue(0) != null && adoReader.GetValue(0).ToString().Trim() != "")
                {
                    dblXiShu = Convert.ToDouble(adoReader.GetValue(0).ToString());
                    batchCalculateOriValue(strSelectedPipeFile, dblXiShu);
                }
                else
                {
                    MessageBox.Show("系数未计算，请先执行计算！");
                    return 1;
                }
            }
            return 0;
        }

        //三个管线层进行批量原值计算
        /// <summary>
        /// 循环三个图层进行管线原值计算
        /// </summary>
        /// <param name="pipefile">档号</param>
        /// <param name="coefficient">系数</param>
        public void batchCalculateOriValue(string pipefile, double coefficient)
        {

            string strSQL = string.Empty;
            double dblXS = 0.0;
            string Table_Name = string.Empty;
            string Tab_PipeQuo = string.Empty;            
            string strPipeFile = string.Empty;
            string TN_LEN = string.Empty;
            string strLayerName;
            List<string> lstLineLayers = new List<string>();
            if (lstLineLayers.Count < 1)
            {
                lstLineLayers.Add(mc_Tn_PipeMain);
                lstLineLayers.Add(mc_Tn_PipeUser);
                lstLineLayers.Add(mc_Tn_PipeSource);
            }
            try
            {
                for (int i = 0; i < lstLineLayers.Count; i++)
                {
                    strLayerName = lstLineLayers[i];
                    Table_Name = m_CurParameter.GWUserName + "." + strLayerName;
                    Tab_PipeQuo = m_CurParameter.GWParameterName + "." + mc_strXSTable;
                    TN_LEN = m_CurParameter.GWUserName + ".f" + getPipeLengthLayID(strLayerName);

                    dblXS = coefficient;
                    strPipeFile = pipefile;

                    strSQL = @"update " + Table_Name + " TB set TB.DRAWPRICE=" +
                        //" (select round(L.LEN*B.QUOTIETY*" + dblXS + ",2) from " + Table_Name + " A," + Tab_PipeQuo +
                        " (select L.LEN*B.QUOTIETY*" + dblXS + " from " + Table_Name + " A," + Tab_PipeQuo +
                        " B, " + TN_LEN + " L where A.Shape=L.FID and " +
                        "A.PIPEFILE=TB.PIPEFILE and " +
                        "A.DIAMETER=B.DIAMETER and " +
                        "A.MATERIAL=B.MATERIAL and " +
                        "A.OBJECTID=TB.OBJECTID and " +
                        "A.DIAMETER=TB.DIAMETER and " +
                        "A.material=TB.MATERIAL) " +
                        "where TB.PIPEFILE='" + strPipeFile + "'";
                    m_objDBCON.ExecuteSQLNoReturn(strSQL);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //计算原值
        //private bool CalculateOriValue(string PipeTableName, string Pipefile, double XiShu, double PipeFee)
        //{
        //    string strSQL = string.Empty;
        //    double dblXS = 0.0;
        //    string Table_Name = string.Empty;
        //    string Tab_PipeQuo = string.Empty;            
        //    string strPipeFile = string.Empty;
        //    string TN_LEN = string.Empty;
        //    double dblPrjFee = 0.0;

        //    bool CalculateOriValue = false;

        //    try
        //    {
        //        //if (modMain.m_objDBCON == null)
        //        //    m_objDBCON = CDBCon.GetInstance();
        //        if (m_CurParameter == null)
        //            m_CurParameter = SysParameters.GetInstance();

        //        Table_Name = m_CurParameter.GWUserName + "." + PipeTableName;
        //        Tab_PipeQuo = m_CurParameter.GWParameterName + "." + mc_strXSTable;
                
        //        TN_LEN = m_CurParameter.GWUserName + ".f" + getPipeLengthLayID(PipeTableName);

        //        dblXS = XiShu;
        //        strPipeFile = Pipefile.Trim();
        //        dblPrjFee = PipeFee;


        //        //判断表中是否存在该条记录，存在继续，不存在返回true；


        //        //strSQL = "select round(L.LEN*B.QUOTIETY*" + dblXS + ",2) from " + Table_Name + " A," + Tab_PipeQuo +
        //        //    " B, " + TN_LEN + " L where A.Shape=L.FID and " +
        //        //    "A.PIPEFILE=A.PIPEFILE and " +
        //        //    "A.DIAMETER=B.DIAMETER and " +
        //        //    "A.MATERIAL=B.MATERIAL and " +
        //        //    "A.PIPEFILE='" + strPipeFile + "'";
        //        //OleDbDataReader adoReader = null;
        //        //modMain.m_objDBCON.ExecuteSQLReturn(strSQL, ref adoReader);
        //        ////仅运行1次
        //        //if (adoReader.Read())
        //        //{

        //        //}
        //        //else
        //        //{
        //        //    return true;
        //        //}

        //        //if (m_objDBCON == null)
        //        //    m_objDBCON = CDBCon.GetInstance();
        //        if (m_CurParameter == null)
        //            m_CurParameter = SysParameters.GetInstance();

        //        strSQL = @"update " + Table_Name + " TB set TB.DRAWPRICE=" +
        //            //" (select round(L.LEN*B.QUOTIETY*" + dblXS + ",2) from " + Table_Name + " A," + Tab_PipeQuo +
        //            " (select L.LEN*B.QUOTIETY*" + dblXS + " from " + Table_Name + " A," + Tab_PipeQuo +
        //            " B, " + TN_LEN + " L where A.Shape=L.FID and " +
        //            "A.PIPEFILE=TB.PIPEFILE and " +
        //            "A.DIAMETER=B.DIAMETER and " +
        //            "A.MATERIAL=B.MATERIAL and " +
        //            "A.OBJECTID=TB.OBJECTID and " +
        //            "A.DIAMETER=TB.DIAMETER and " +
        //            "A.material=TB.MATERIAL) " +
        //            "where TB.PIPEFILE='" + strPipeFile + "'";
        //        modMain.m_objDBCON.ExecuteSQLNoReturn(strSQL);

                

        //        ////配置输出结果
        //        //string strTabCn = string.Empty;
        //        //string strResult = string.Empty;
        //        //switch (PipeTableName.ToUpper())
        //        //{
        //        //    case mc_Tn_PipeMain:
        //        //        strTabCn = mc_Tn_PipeMainCn;
        //        //        break;
        //        //    case mc_Tn_PipeUser:
        //        //        strTabCn = mc_Tn_PipeUserCn;
        //        //        break;
        //        //    case mc_Tn_PipeSource:
        //        //        strTabCn = mc_Tn_PipeSourceCn;
        //        //        break;
        //        //    default:
        //        //        strTabCn = "";
        //        //        break;
        //        //}

        //        //m_lstCalcPipeFile.Add                
        //        //int iUpdateCount = 0;
        //        //int iTotalCount = 0;

        //        ////查询多少条记录被更新
        //        //strSQL = "select count(TB.OBJECTID) as cn from " + Table_Name + " TB where TB.PIPEFILE='" + strPipeFile + "' and TB.DRAWPRICE<>0.0 and not TB.DRAWPRICE is null";
        //        //adoReader = null;
        //        //modMain.m_objDBCON.ExecuteSQLReturn(strSQL, ref adoReader);
        //        //if (adoReader.Read())
        //        //{
        //        //    iUpdateCount = Convert.ToInt32(adoReader["cn"].ToString());
        //        //}
        //        ////查询共有多少条记录
        //        //strSQL = "select count(TB.OBJECTID) as cn from " + Table_Name + " TB where TB.PIPEFILE='" + strPipeFile + "'";
        //        //adoReader = null;
        //        //modMain.m_objDBCON.ExecuteSQLReturn(strSQL, ref adoReader);
        //        //if (adoReader.Read())
        //        //{
        //        //    iTotalCount = Convert.ToInt32(adoReader["cn"].ToString());
        //        //}

        //        //strResult = ("档号<" + strPipeFile + "> 在图层<" + strTabCn + ">") + "上更新[" + iUpdateCount + " / " + iTotalCount + "]条记录";

                

        //        //如果更新记录数=总的记录数
        //        //if (iUpdateCount == iTotalCount)
        //        //{
        //        //    string sOIDUpdate = string.Empty;
        //        //    double dPrjfeeUpdate = 0.0;
        //        //    strSQL = "select TB.OBJECTID as A,TB.DRAWPRICE as B from " + Table_Name + " TB " +
        //        //        " where TB.PIPEFILE='" + strPipeFile + "'";

        //        //    adoReader = null;
        //        //    modMain.m_objDBCON.ExecuteSQLReturn(strSQL, ref adoReader);
        //        //    while(adoReader.Read())
        //        //    {
        //        //        sOIDUpdate = adoReader["A"].ToString();
        //        //        dPrjfeeUpdate = Convert.ToDouble(adoReader["B"].ToString());
        //        //    }

        //        //    //获得输入的总价值
        //        //    double dPrjfeeRec = 0.0;
        //        //    double dDivPrjfeeRec = 0.0;
        //        //    strSQL = "select sum(TB.DRAWPRICE) as cn from " + Table_Name + " TB where TB.PIPEFILE='" + strPipeFile + "'";
        //        //    adoReader = null;

        //        //    modMain.m_objDBCON.ExecuteSQLReturn(strSQL, ref adoReader);
        //        //    while(adoReader.Read())
        //        //    {
        //        //        if (adoReader["cn"] != null)
        //        //        {
        //        //            dPrjfeeRec = Convert.ToDouble(adoReader["cn"].ToString());
        //        //            if (dPrjfeeRec == dblPrjFee)
        //        //            {
        //        //                dDivPrjfeeRec = 0.0;
        //        //            }
        //        //            else if (dPrjfeeRec < dblPrjFee)
        //        //            {
        //        //                dDivPrjfeeRec = dblPrjFee = dPrjfeeRec;
        //        //                dPrjfeeUpdate = dPrjfeeUpdate + dDivPrjfeeRec;
        //        //            }
        //        //            else if (dPrjfeeRec > dblPrjFee)
        //        //            {
        //        //                dDivPrjfeeRec = dPrjfeeRec - dblPrjFee;
        //        //                dPrjfeeUpdate = dPrjfeeUpdate - dDivPrjfeeRec;
        //        //            }
        //        //        }

        //        //    }                   

        //        //    if (dDivPrjfeeRec != 0)
        //        //    {
        //        //        strSQL = "update " + Table_Name + " TB set TB.DRAWPRICE=" + dPrjfeeUpdate + " where TB.PIPEFILE='" + strPipeFile + "' and TB.OBJECTID=" + sOIDUpdate;

        //        //        modMain.m_objDBCON.ExecuteSQLNoReturn(strSQL);
        //        //    }
        //        //}
        //        CalculateOriValue = true;
        //        return CalculateOriValue;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("档号:" + Pipefile + "-计算过程出错" + ex.Message, "错误提示！");
        //        return false;
        //    }
        //}

                

        //获取layerid from sde.layers
        public string getPipeLengthLayID(string PipeTableName)
        {
            OleDbDataReader adoReader = null;
            string strLayerID = string.Empty;
            string strSQL = string.Empty;
            try
            {
                strSQL = string.Format("select layer_id from sde.layers where Table_name='{0}'", PipeTableName);
                m_objDBCON.ExecuteSQLReturn(strSQL, ref adoReader);
                if (adoReader.Read())
                {
                    strLayerID = adoReader.GetValue(0).ToString();
                }
                else
                {
                    strLayerID = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误！");
                return "0";
            }
            return strLayerID;
        }    
    }
}
