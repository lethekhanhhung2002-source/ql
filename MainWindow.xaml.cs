using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _1_12_2025
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        Entities1 db = new Entities1();
        public MainWindow()
        {
            InitializeComponent();
            loadData();
        }
        private void loadData()
        {
           DG_SinhVien.ItemsSource = db.SinhViens.ToList();

            cbb_MonHoc.ItemsSource = db.MonHocs.ToList();
            cbb_MonHoc.DisplayMemberPath = "TenMon";
            cbb_MonHoc.SelectedValuePath = "MaMon";
        }
        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            string maSV = txtMaSV.Text.Trim();
            if (string.IsNullOrEmpty(maSV))
            {
                MessageBox.Show("Mã sinh viên không được để trống!");
                return;
            }
            var svTonTai = db.SinhViens.FirstOrDefault(s => s.MaSV == maSV);
            if (svTonTai != null)
            {
                MessageBox.Show("Mã sinh viên đã tồn tại! Vui lòng nhập mã khác.");
                return;
            }
            SinhVien svMoi = new SinhVien()
            {
                MaSV = maSV,
                HoTen = txtHoTen.Text,
                DiaChi = txtDiaChi.Text,
                NgaySinh = dpNgaySinh.SelectedDate,
                MaMon = cbb_MonHoc.SelectedValue?.ToString(),
                // Kiểm tra RadioButton nào đang chọn
                GioiTinh = rdoNam.IsChecked == true ? "Nam" : (rdoNu.IsChecked == true ? "Nữ" : "Khác")
            };

            db.SinhViens.Add(svMoi);
            db.SaveChanges(); // Lưu vào CSDL
            loadData();
            MessageBox.Show("Thêm thành công!");
        }

        private void DG_SinhVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
