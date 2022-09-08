using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace _010100627701_TTKTS_DT01
{
    public partial class Form1 : Form
    {
        DSNguonTin TINS = new DSNguonTin();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void move1RBtn_Click(object sender, EventArgs e)
        {
            if (tinLst.SelectedItem == null)
                MessageBox.Show("Chưa có tin nào được chọn", "Lưu ý",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                tinProcessLst.Items.Add(tinLst.SelectedItem);
                tinLst.Items.Remove(tinLst.SelectedItem);
            }                
        }

        private void moveAllRBtn_Click(object sender, EventArgs e)
        {
            tinProcessLst.Items.AddRange(tinLst.Items);
            tinLst.Items.Clear();
        }

        private void move1LBtn_Click(object sender, EventArgs e)
        {
            tinLst.Items.Add(tinProcessLst.SelectedItem);
            tinProcessLst.Items.Remove(tinProcessLst.SelectedItem);
        }

        private void moveAllLBtn_Click(object sender, EventArgs e)
        {
            tinLst.Items.AddRange(tinProcessLst.Items);
            tinProcessLst.Items.Clear();
        }

        private void dữLiệu1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tinLst.Items.Clear();
            TINS.A.Clear();
            TINS.nhap("../../Files Data/NguonMaHoaKhongThuTu.xml");
            TINS.sapXepTTTin();
            foreach (TIN t in TINS.A)
                tinLst.Items.Add(t.STenTin);
            TINS.tinhLuongTinRiengTungTIN();
        }

        private void dữLiệu2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tinLst.Items.Clear();
            TINS.A.Clear();
            TINS.nhap("../../Files Data/NguonMaHoaTest.xml");
            TINS.sapXepTTTin();
            foreach (TIN t in TINS.A)
                tinLst.Items.Add(t.STenTin);
            TINS.tinhLuongTinRiengTungTIN();
        }

        private void dữLiệuNhậpTayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tinLst.Items.Clear();
            TINS.A.Clear();
            TINS.nhap("../../Files Data/NguonTinLuuTru.xml");
            TINS.sapXepTTTin();
            foreach (TIN t in TINS.A)
                tinLst.Items.Add(t.STenTin);
            TINS.tinhLuongTinRiengTungTIN();
        }

        void clearDetailInfo()
        {
            tenTinTBox.Text = "";
            xsTinTBox.Text = "";
            hsCBox.Text = "";
            ltriengTintBox.Text = "";
        }

        private void tinLst_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearDetailInfo();
            foreach(TIN t in TINS.A)
                if(t.STenTin.Equals(tinLst.SelectedItem)==true)
                {
                    tenTinTBox.Text = t.STenTin;
                    xsTinTBox.Text = t.DXacSuatTin.ToString();
                    hsCBox.Text = t.IHeCoSo.ToString();
                    ltriengTintBox.Text = Math.Round(t.DLuongTinRieng,2).ToString();
                    break;
                }    
        }

        private void kq1Btn_Click(object sender, EventArgs e)
        {
            kq1TBox.Text = Math.Round(TINS.tinhLuongTinTBCuaNguon(),2).ToString();
        }

        private void kq2Btn_Click(object sender, EventArgs e)
        {
            double sum = 0;
            foreach (string t in tinProcessLst.Items)
                foreach (TIN i in TINS.A)
                    if (i.STenTin.Equals(t) == true)
                    {
                        sum += i.DLuongTinRieng;
                        break;
                    }
            kq2TBox.Text = Math.Round(sum, 2).ToString();
        }

        double xetXSDieuKien(double hesothem)
        {
            double sum = 0;
            foreach (TIN t in TINS.A)
                sum += t.DXacSuatTin;
            if ((sum+hesothem) < 1)
                return 0;
            else
                return 1-sum;
        }

        private void themTinBtn_Click(object sender, EventArgs e)
        {
            foreach (TIN i in TINS.A)
                if (tenTinTBox.Text.Equals(i.STenTin) == true)
                {
                    MessageBox.Show("Tên tin khởi tạo đã tồn tại trong cơ sở dữ liệu", "Báo lỗi điều kiện", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }                            
                else if(xetXSDieuKien(double.Parse(xsTinTBox.Text))!=0)
                {
                    MessageBox.Show("Xác xuất đã đạt giới hạn (Giới hạn còn "+xetXSDieuKien(double.Parse(hsCBox.Text))+" %)", "Báo lỗi điều kiện", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }   
            if(tenTinTBox.Text=="")
            {
                MessageBox.Show("Tên tin chưa được đặt", "Báo lỗi điều kiện", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (xsTinTBox.Text == "")
            {
                MessageBox.Show("Xác xuất chưa được nhập ", "Báo lỗi điều kiện", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (hsCBox.Text == "")
            {
                MessageBox.Show("Hệ cơ số của tin chưa được chọn", "Báo lỗi điều kiện", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Dictionary<string, string> tinMoi = new Dictionary<string, string>();
            tinMoi.Add("Name", tenTinTBox.Text);
            tinMoi.Add("XacXuat", xsTinTBox.Text);
            tinMoi.Add("HeSo", hsCBox.Text);
            clearDetailInfo();
            TINS.themTinVaoNguon("../../Files Data/NguonTinLuuTru.xml", tinMoi);
            dữLiệuNhậpTayToolStripMenuItem.PerformClick();
            MessageBox.Show("Thêm thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void xoaTinBtn_Click(object sender, EventArgs e)
        {
            if(TINS.xoaTinTuNguon("../../Files Data/NguonTinLuuTru.xml", tenTinTBox.Text)==false)
                MessageBox.Show("Không tìm thấy đối tượng để xóa", "Báo Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                MessageBox.Show("Xóa thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tinLst.Items.Remove(tinLst.SelectedItem);
            }                   
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void resetChươngTrìnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
