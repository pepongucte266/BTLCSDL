﻿using System;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            btnDangNhap.BackColor = Color.Transparent;
        }


        private void btnDangNhap_Click_1(object sender, EventArgs e)
        {
            string tk = txtTaiKhoan.Text;
            string mk = txtMatKhau.Text;
            if(Login(tk,mk))
            {
                Form2 f = new Form2();
                DataTable kq = GetMaNV(tk,mk);
                f.MANV = kq;
                this.Hide();
                f.ShowDialog();
                this.Show();
            }    
            else
            {
                MessageBox.Show("Sai tên dn hoặc mk");
            }
        }

        private bool Login(string tk, string mk)
        {
            string sqlstr = "select * from NhanVien where TaiKhoan = N'" + tk + "' and MatKhau = N'" + mk + "'";
            DataTable kq = ConnectSQL.GetData(sqlstr);
            return kq.Rows.Count > 0;
        }

        private DataTable GetMaNV(string tk, string mk)
        {
            string sqlstr = "select * from NhanVien where TaiKhoan = N'" + tk + "' and MatKhau = N'" + mk + "'";
            DataTable kq = ConnectSQL.GetData(sqlstr);
            return kq;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
