using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Danh_Ba_Dien_Thoai
{
    public partial class FrmSearch : Form
    {
        List<DanhBa> ds;
        public FrmSearch()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int flag = 0;
            List<DanhBa> temp = new List<DanhBa>();
            string input= txtInput.Text;
            foreach(DanhBa danhBa in ds)
            {//Kiểm tra xem họ tên có trong danh sách hay chưa
                if (danhBa.HoVaTen.IndexOf(input) != -1)
                {
                    foreach(DanhBa db in temp)
                    {
                        if (db.SoDienThoai == danhBa.SoDienThoai)
                        {//Nếu đã có trong danh sách thì chỉnh flag =1
                            flag = 1;
                            break;
                        }
                       
                    }
                    // flag =0 tương đương ko có trong danh sách tiến hành thêm
                    if(flag==0)
                    temp.Add(danhBa);
                }
                //Kiểm tra số điện thoại có trong danh sách hay khoong
                if(danhBa.SoDienThoai.IndexOf(input)!=-1)
                {
                    foreach (DanhBa db in temp)
                    {
                        if (db.SoDienThoai == danhBa.SoDienThoai)
                        {
                            flag = 1;
                            break;
                        }

                    } // flag =0 tương đương ko có trong danh sách tiến hành thêm
                    if (flag == 0)
                        temp.Add(danhBa);
                }    
            }
            //Hiển thị danh sách ra list
            dgvData.DataSource = temp.ToList();
        }

        private void FrmSearch_Load(object sender, EventArgs e)
        {
            //ĐỌc dữ liệu từ file gán vào ds 
            ds= new List<DanhBa>();
            try
            {
                FileStream fs = new FileStream("QLDB.txt", FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();

                ds = bf.Deserialize(fs) as List<DanhBa>;
                fs.Close();
            }
            catch { MessageBox.Show("Khong the load"); }
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
