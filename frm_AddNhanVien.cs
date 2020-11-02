using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL
{
    public partial class frm_AddNhanVien : Form
    {
        public frm_AddNhanVien()
        {
            InitializeComponent();
        }


        private string _MaNV = "";
        public string MaNV
        {
            get { return _MaNV; }
            set { _MaNV = value; }
        }


        private void frm_AddNhanVien_Load(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(MaNV) )
            {
                //Gán lại tiên đề frm
                this.Text = "Sửa thông tin nhân viên";
                txtMaNV.Enabled = false;
                HienThiChiTietNhanVien();
            }
            else
            {
                this.Text = "Thêm mới";
            }    
        }

        private void HienThiChiTietNhanVien()
        {
            //Lấy thông tin theo mã nv ở frm trước
            DataTable dtNV = ConnectSQL.ActNhanVien.ChiTietTheoMa(MaNV);

            if(dtNV.Rows.Count>0)
            {
                txtMaNV.Text = MaNV;
                txtHoTen.Text = "" + dtNV.Rows[0]["TenNhanVien"];
                txtSdt.Text = "" + dtNV.Rows[0]["SoDT"];
                txtDiaChi.Text = "" + dtNV.Rows[0]["DiaChi"];
                txtTaiKhoan.Text = "" + dtNV.Rows[0]["TaiKhoan"];
                cboChucVu.Text = "" + dtNV.Rows[0]["ChucVu"];

                DateTime NgaySinh;
                DateTime.TryParse("" + dtNV.Rows[0]["NgaySinh"], out NgaySinh);
                dtpNgaySinh.Value = NgaySinh;

                string GioiTinh = "" + dtNV.Rows[0]["GioiTinh"];
                if (GioiTinh == "1") rbNam.Checked = true;
                if(GioiTinh =="0") rbNu.Checked = true;
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            NhanVien objNV = new NhanVien();

            objNV.MaNV = txtMaNV.Text;
            objNV.TenNhanVien = txtHoTen.Text;
            objNV.TaiKhoan = txtTaiKhoan.Text;
            objNV.SDT = txtSdt.Text;
            objNV.DiaChi = txtDiaChi.Text;
            objNV.NgaySinh = dtpNgaySinh.Value;
            objNV.ChucVu = cboChucVu.Text;
            if (rbNam.Checked) objNV.GioiTinh = 1; 
            else objNV.GioiTinh = 0;

            bool kq = false;
            if(!string.IsNullOrWhiteSpace(MaNV))
            {
                kq = ConnectSQL.ActNhanVien.CapNhat(objNV);
            }
            else
            {
                kq = ConnectSQL.ActNhanVien.ThemMoi(objNV);
            }
            if(kq)
            {
                MessageBox.Show("Cập nhật thông tin thành công");
            }    

        }
    }
}
