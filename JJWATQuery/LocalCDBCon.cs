using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Data;

namespace JJWATQuery
{    
    public class LocalCDBCon
    {
        OleDbConnection oledbConn=null;
        OleDbCommand oledbCmd = null;

        public void CloseAndDisposeCon()
        {
            oledbConn.Close();
            oledbConn.Dispose();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oledbConn);            
        }

        public void OpenConnection(string ConnectionString)
        {            
            oledbConn = new OleDbConnection(ConnectionString);
            try
            {
                oledbConn.Open();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString(),"错误！");
                return;
            }
        }
      
        public void ExecuteSQLReturn(string strSQL, ref OleDbDataReader adoReader)
        { 
            oledbCmd=new OleDbCommand();
            oledbCmd.Connection=oledbConn;
            oledbCmd.CommandText=strSQL;
            try
            {
                adoReader = oledbCmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString(), "错误！");
                return;
            }
        }
        
        public void DBConnTest()
        {
            if (oledbConn!=null&&oledbConn.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    oledbConn.Open();                                                                   
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message.ToString(), "错误！");
                    return;
                }
                System.Windows.Forms.MessageBox.Show("打开成功！","提示");
            }
            else if (oledbConn != null && oledbConn.State == System.Data.ConnectionState.Open)
            {
                //string sqlinsrt = "insert into table1(ID,strName,strNo) values('2','李四','1002')";
                //oledbCmd = new OleDbCommand();   
                //oledbCmd.Connection = oledbConn;                             
                //oledbCmd.CommandText = sqlinsrt.ToString();
                //oledbCmd.ExecuteNonQuery();
                System.Windows.Forms.MessageBox.Show("已经打开！", "提示");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("失去连接，测试失败！", "提示");
            }
        }

        /// <summary>
        /// 执行SQL语句，返回Reader
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <returns>返回的Reader</returns>
        public OleDbDataReader ReadData(string strSQL)
        {
            if (oledbConn.State == ConnectionState.Closed)
            {
                MessageBox.Show("数据库连接未打开！");
                return null;
            }
            OleDbCommand command = new OleDbCommand(strSQL, oledbConn);
            OleDbDataReader reader = command.ExecuteReader();
            return reader;
        }
    }
}
