using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL
{
    public partial class Form2 : Form
    {
        private DataTable _MANV;
        public DataTable MANV
        {
            get { return _MANV; }
            set { _MANV = value; }
        }
        public Form2()
        {
            InitializeComponent();
        }

        private void btnLichBay_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage1);
        }

        private void btnQLMB_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage2);
        }

        private void thêmĐườngĐiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_ThemDuongDi frmdd = new frm_ThemDuongDi();
            frmdd.ShowDialog();
        }

        private void thêmChuyếnBayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_ThemChuyenBay frmcb = new frm_ThemChuyenBay();
            frmcb.ShowDialog();
        }

        private void btnMuaVe_Click(object sender, EventArgs e)
        {
            frm_BanVeMB frmbv = new frm_BanVeMB();
            frmbv.ShowDialog();
        }

        private void btnQLNV_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage4);
        }

        private void thôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_ThongTinTaiKhoan frmTK = new frm_ThongTinTaiKhoan();
            frmTK.TT = MANV;
            
            frmTK.ShowDialog();
        }

        private void btnTimKiemNV_Click(object sender, EventArgs e)
        {
            HienThiDanhSachNV();


        }

        private void HienThiDanhSachNV()
        {
            string tukhoa = txtTimKiemNV.Text;
            ActNhanVien dsnv = new ActNhanVien();
            DataTable dt = dsnv.TimKiemNhanVien(tukhoa);
            
            dgrNhanVien.DataSource = dt;
        }

        private void HienThiDanhSachMB()
        {
            ActMayBay dsmb = new ActMayBay();
            DataTable dt = dsmb.GetDataMayBay();
            dgrMayBay.DataSource = dt;

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            HienThiDanhSachNV();
            HienThiDanhSachMB();
            this.Text = MANV.Rows[0]["TenNhanVien"].ToString().Trim()+"_"+ MANV.Rows[0]["ChucVu"].ToString();
            
        }


        private void btnThemNV_Click(object sender, EventArgs e)
        {
            frm_AddNhanVien frm_Add = new frm_AddNhanVien();

            frm_Add.ShowDialog();

            HienThiDanhSachNV();
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
 
        }

        private void btnTimKiemLB_Click(object sender, EventArgs e)
        {
          
        }

        private void btnSuaNV_Click(object sender, EventArgs e)
        {
            string manv = "";
            frm_AddNhanVien frm_Update = new frm_AddNhanVien();
            
            manv = "" + dgrNhanVien.CurrentRow.Cells[0].Value;
            frm_Update.MaNV = manv;
            frm_Update.ShowDialog();
            HienThiDanhSachNV();
        }

        private void btnThemMB_Click(object sender, EventArgs e)
        {
            MayBay objMB = new MayBay();

            //Lấy giá trị từ giao diện
            objMB.MaMayBay = txtMaMB.Text;
            objMB.TenMayBay = txtTenMB.Text;
            objMB.HangSanXuat = txtHangSX.Text;
            objMB.SoLuongGhe1 = Convert.ToInt32(txtGhe1.Text);
            objMB.SoLuongGhe2 = Convert.ToInt32(txtGhe2.Text);

            bool kq = false;
            kq = ConnectSQL.ActMayBay.ThemMoi(objMB);
            if (kq)
            {
                MessageBox.Show("Thêm máy bay thành công");
                //reload lại danh sách sau khi thêm
                HienThiDanhSachMB();
            }
        }

        private void btnSuaMB_Click(object sender, EventArgs e)
        {
            string mamb = ""+ dgrMayBay.CurrentRow.Cells[0].Value;

            MayBay objMB = new MayBay();

            //Lấy giá trị từ giao diện
            objMB.MaMayBay = txtMaMB.Text;
            objMB.TenMayBay = txtTenMB.Text;
            objMB.HangSanXuat = txtHangSX.Text;
            objMB.SoLuongGhe1 = Convert.ToInt32(txtGhe1.Text);
            objMB.SoLuongGhe2 = Convert.ToInt32(txtGhe2.Text);

            bool kq = false;
            kq = ConnectSQL.ActMayBay.CapNhat(objMB);
            if (kq)
            {
                MessageBox.Show("Cập nhật máy bay thành công");
                //reload lại danh sách sau khi thêm
                HienThiDanhSachMB();
            }

        }

        private void dgrMayBay_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //nếu có dữ liệu
            if(e.RowIndex!=null)
            {
                DataGridViewRow row = dgrMayBay.Rows[e.RowIndex];
                txtMaMB.Text = row.Cells[0].Value.ToString();
                txtTenMB.Text = row.Cells[1].Value.ToString();
                txtHangSX.Text = row.Cells[2].Value.ToString();
                txtGhe1.Text = row.Cells[3].Value.ToString();
                txtGhe2.Text = row.Cells[4].Value.ToString();
            }    
        }

        private void btnXoaMB_Click(object sender, EventArgs e)
        {
            //Lấy kết quả người dùng nhấn
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            //Nếu muốn xóa
            if (dr == DialogResult.Yes)
            {
                //Lấy mã sv
                string MaMB = "" + dgrMayBay.CurrentRow.Cells[0].Value;

                bool kq = ConnectSQL.ActMayBay.Xoa(MaMB);
                if (kq)
                {
                    //Reload lại danh sách
                    HienThiDanhSachMB();
                }
            }
        }
    }
}
