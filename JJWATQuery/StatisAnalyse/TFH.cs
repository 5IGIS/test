using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJWATQuery
{
    public class TuFuHaoModel
    {
        string tfh;

        public string Tfh
        {
            get { return tfh; }
            set { tfh = value; }
        }
        
    }

    public class TuFuHaoModels : List<TuFuHaoModel>
    { 

    }

    public class THFComparer : IEqualityComparer<TuFuHaoModel>
    {
        public static THFComparer Default = new THFComparer();
        public bool Equals(TuFuHaoModel x, TuFuHaoModel y)
        {
            if (System.Object.ReferenceEquals(x, y)) return true;
            if (System.Object.ReferenceEquals(x, null) || System.Object.ReferenceEquals(y, null)) return false;
            return x.Tfh == y.Tfh;

        }
        public int GetHashCode(TuFuHaoModel tfh)
        {
            if (Object.ReferenceEquals(tfh, null)) return 0;
            int hashtfh = tfh.Tfh == null ? 0 : tfh.Tfh.GetHashCode();
            return hashtfh;
        }


    }
}
