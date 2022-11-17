using System;
using System.Collections;
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
    public partial class FrmQLDB : Form
    {
        //biến lưu vị trí khi click vào listview
        int indexClick = -1;
       
        List<DanhBa> ds=new List<DanhBa>();
        
        public FrmQLDB()
        {
            InitializeComponent();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            //Gọi form thêm thông tin
            FrmAddform form = new FrmAddform();
            form.ShowDialog();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            //Hiển thị dialog khi thoát
            DialogResult res=MessageBox.Show("Bạn muốn thoát!","Thoát",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(res==DialogResult.Yes)
            {
                Close();
            }    
        }
        private void hienThi()
            
        {
            List<Ten> dsTen = Ten.getName(ds);
           //Chuyển từ list sang arr list
           ArrayList arrayList = new ArrayList();
            foreach(Ten i in dsTen)
            {
                arrayList.Add(i);

            }    
            //Sắp xếp
            arrayList.Sort(new SortTheoName());

            //Lấy dư liệu từ ds hiển thị ra list view
            dgvData.DataSource = arrayList;
        }
        private void FrmQLDB_Load(object sender, EventArgs e)
        {
            ds = new List<DanhBa>();
           
           
            //Đọc dữ liệu từ file
            try
            {
                FileStream fs = new FileStream("QLDB.txt", FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();

                ds = bf.Deserialize(fs) as List<DanhBa>;
                fs.Close();
                hienThi();
            }
            catch { MessageBox.Show("Khong the load"); }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            try
            {
                //Lấy thoog tin vừa thêm được ở frm thêm và gán vào lst
                bool flag = true;
                List<DanhBa> lst = new List<DanhBa>();
                FileStream fs = new FileStream("danhba.txt", FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();

                lst = bf.Deserialize(fs) as List<DanhBa>;
                fs.Close();
                //Kiểm tra xem thông tin lấy được đã có trong danh sách hay chưa
                foreach(DanhBa key in lst)
                {
                   foreach(DanhBa value in ds)
                    {
                        if (value.SoDienThoai == key.SoDienThoai)
                        {
                            flag=false;
                        }
                    }    
                }
                //Nếu chưa có thì tiến hành thêm vào ds
                if (flag)
                {
                    ds.Add(lst[0]);
                    
                }
                hienThi();
            }
            catch { }
            try
            {
                //Lấy thôg tin sau khi cập nhật xong cho detail
                List<DanhBa> detail = new List<DanhBa>();
                FileStream fs = new FileStream("DanhBaUpdate.txt", FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();

                detail = bf.Deserialize(fs) as List<DanhBa>;
                fs.Close();
                //Thay đổi thông tin cũ trong danh sách thành thông tin mới
                foreach (DanhBa db in ds)
                {
                    if (db.SoDienThoai == detail[0].SoDienThoai)
                    {
                        db.HoVaTen = detail[0].HoVaTen;
                        db.Email = detail[0].Email;
                        db.DiaChi = detail[0].DiaChi;
                        db.GT=detail[0].GT;

                    }
                }
                //Hiển thị thông tin mới ra view
                hienThi();
            }
            catch
            {

            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //Lưu dữ liệu vào file
                FileStream fs = new FileStream("QLDB.txt", FileMode.OpenOrCreate);

                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, ds);
                fs.Close();
               
            }
            catch
            {
                MessageBox.Show("Khong the luu");
            }
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Lấy chỉ mục khi click vào row
            int index = dgvData.Rows[e.RowIndex].Index;
            try
            {
                //Tạo 1 danh sách ảo với phần tử là phần được đc click  và lưu vào file
                List<DanhBa> detail = new List<DanhBa>();
                detail.Add(ds[index]);
                //Ghi dữ liệu vào file
                FileStream fs = new FileStream("QLDBDetail.txt", FileMode.OpenOrCreate);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, detail);
                fs.Close();
                //Gọi form thông tin chi tiết
                FrmDetail frm = new FrmDetail();
                frm.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Khong the luu");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(indexClick!=-1)
            {
               //Kiểm tra xem người dùng có click vào dòng không
               //Nếu đã click thì xóa dòng đó
                ds.RemoveAt(indexClick);
                indexClick = -1;
                hienThi();
            }     
        }
        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexClick= dgvData.Rows[e.RowIndex].Index;
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            //Gọi form search
            FrmSearch frm = new FrmSearch();
            frm.Show();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
