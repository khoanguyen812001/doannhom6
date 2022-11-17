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
    public partial class FrmAddform : Form
    {
       List<DanhBa> ds;
        public FrmAddform()
        {
            InitializeComponent();
        }
        //ĐÓng form
        private void button3_Click(object sender, EventArgs e)
        {
            //ĐÓng form
            Close();
        }
        //Lưu dữ liệu
        private void button1_Click(object sender, EventArgs e)
        {//Lấy thôn tin từ input
            string sdt = txtSDT.Text;
            string hoten=txtName.Text;
            string email = txtEmail.Text;
            string diachi = txtDiaChi.Text;
            string gt = radNam.Checked == true ? "Nam" : "Nu";
            DanhBa db = new DanhBa(hoten, sdt, email, diachi, gt);
            try

            {
                
                List<DanhBa> dic =new List<DanhBa>();
                //dọc dữ liệu từ file
                FileStream fs = new FileStream("QLDB.txt", FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();

                dic = bf.Deserialize(fs) as List<DanhBa>;
                fs.Close();
                //Kiểm tra xem đối tượng muốn thêm đã có trong danh sách chưa
                foreach (DanhBa k in dic)
                {
                    //Nếu có thì không thêm
                    if (sdt == k.SoDienThoai)
                    {
                       
                        return;
                    }    
                       
                }
                //Nếu chưa có thì tiến hành thêm và xóa input form
                ds.Add( db);
                txtDiaChi.Text="";
                txtName.Text = "";
                txtSDT.Text = "";
                txtEmail.Text = "";
                txtName.Focus();
               
                
            }
            catch
            {
               
            }
            try
            {//Sau khi thêm xong tiến hành ghi dữ liệu vào file
                //Ghi dữ liệu vào file
                FileStream fs = new FileStream("danhba.txt", FileMode.OpenOrCreate);

                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, ds);
                fs.Close();
                Close();
            }
            catch
            {
                MessageBox.Show("Khong the luu");
            }
        }
        private void FrmAddform_Load(object sender, EventArgs e)
        {
            ds = new List<DanhBa>();
            radNam.Checked = true;
            txtName.Focus();
        }
    }
}
