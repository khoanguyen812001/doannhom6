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
    public partial class FrmDetail : Form
    {
        List<DanhBa> detail;
        public FrmDetail()
        {
            InitializeComponent();
        }

        private void FrmDetail_Load(object sender, EventArgs e)
        {
            detail = new List<DanhBa>();
            //Đọc dữ liệu từ file
            try
            {
               
                FileStream fs = new FileStream("QLDBDetail.txt", FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();

                //Lấy dữ liệu từ file cho vào detail
                detail = bf.Deserialize(fs) as List<DanhBa>;
                fs.Close();
                //gán giá trị in put bằng thông tin vừa lấy được từ file
                txtName.Text = detail[0].HoVaTen;
                txtEmail.Text = detail[0].Email;
                txtDiaChi.Text = detail[0].DiaChi;
                txtSDT.Text = detail[0].SoDienThoai;
                if (detail[0].GT == "Nam")
                    radNam.Checked = true;
                else
                    radNu.Checked = true;

            }
            catch { MessageBox.Show("Khong the load"); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Lấy thông tin ở input và cập nhật cho detail
            detail[0].HoVaTen = txtName.Text;
            detail[0].Email = txtEmail.Text;
            detail[0].DiaChi = txtDiaChi.Text;
            detail[0].GT = radNam.Checked == true ? "Nam" : "Nu";
            try
            {
                //Ghi dữ liệu vào file
                FileStream fs = new FileStream("DanhBaUpdate.txt", FileMode.OpenOrCreate);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, detail);
                fs.Close();
                Close();
            }
            catch
            {
                MessageBox.Show("Khong the cập nhật");
            }

        }
    }
}
