using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Danh_Ba_Dien_Thoai
{
    internal class Ten
    {
        string _ten;
        public Ten()
        {
            _ten = "";
        }
        public Ten(string ten)
        {
            _ten = ten;
        }   
        public string HoVaTen
        {
            get { return _ten; }
            set { _ten = value; }
        }
        public static List<Ten> getName(List<DanhBa> ds)
        {
            List<Ten> dsname =new List<Ten>();  

            foreach (DanhBa d in ds)
            {
                Ten t = new Ten(d.HoVaTen);
                dsname.Add(t);
            } 
            return dsname;
        }

    }
    public class SortTheoName : IComparer //Tao 1 class sort theo ho
    {
        int IComparer.Compare(object x, object y)
        {
            Ten _acount1 = x as Ten;
            Ten _acount2 = y as Ten;
            return String.Compare(_acount1.HoVaTen, _acount2.HoVaTen);
        }
    }
}
