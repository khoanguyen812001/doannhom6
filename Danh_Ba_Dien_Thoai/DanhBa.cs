using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Danh_Ba_Dien_Thoai
{
    [Serializable]
    internal class DanhBa
    {
        
        private string _hovaten;
        private string _sodienthoai;
        private string _email;
       
        private string _diachi;
        private string _gt;
        public DanhBa()
        {
            
            _hovaten = "";
            _email = "";
            _gt = "";
            _diachi = "";
        }
        public DanhBa( string hovaten, string sodienthoai, string email, string diachi,string gt)
        {
           
            _hovaten = hovaten;
            _sodienthoai = sodienthoai;
            _email = email;
            _gt = gt;
            _diachi = diachi;
        }
        
        public string HoVaTen
        {
            get
            {
                return _hovaten;
            }
            set
            {
                _hovaten = value;
            }
        }
        public string SoDienThoai
        {
            get { return _sodienthoai; }
            set { _sodienthoai = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        
        public string DiaChi
        {
            get { return _diachi; }
            set
            {
                _diachi = value;
            }
        }
        public string GT
        {
            get { return _gt; }
            set { _gt = value; }
        }
    }
}
