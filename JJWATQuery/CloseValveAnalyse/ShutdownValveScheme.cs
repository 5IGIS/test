using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJWATQuery
{
    public class ShutdownValveScheme
    {
        //序号
        public string strNo { get; set; }
        //关闭方案号
        public string strClsID { get; set; }
        //
        public int lineID { get; set; }

        public string strLineName { get; set; }

        public string strValveIDs { get; set; }

        public int valveCount { get; set; }
    }

    public class StringClsIDComparer : IEqualityComparer<ShutdownValveScheme>
    {
        public bool Equals(ShutdownValveScheme s1, ShutdownValveScheme s2)
        {
            if (System.Object.ReferenceEquals(s1, s2)) return true;
            if (System.Object.ReferenceEquals(s1, null) || System.Object.ReferenceEquals(s2, null)) return false;
            return s1.strClsID == s2.strClsID && s1.strNo != s2.strNo;

        }
        public int GetHashCode(ShutdownValveScheme svv)
        {
            if (Object.ReferenceEquals(svv, null)) return 0;
            int hashSchemeNumber = svv.strNo == null ? 0 : svv.strNo.GetHashCode();
            int hashSchemeCode = svv.strClsID.GetHashCode();
            return hashSchemeNumber ^ hashSchemeCode;

        }
    }
}


