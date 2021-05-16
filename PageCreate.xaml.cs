using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace HopDongBaoHiem
{
    /// <summary>
    /// Interaction logic for PageCreate.xaml
    /// </summary>
    public partial class PageCreate : Page
    {
        public PageCreate()     //Gọi khi tạo hợp đồng
        {
            InitializeComponent();

            btnDel.Visibility = Visibility.Hidden;  //Ẩn nút xóa 
            btnSave.IsEnabled = false;  //nút Save không kích hoạt

            //Sự kiện thay đổi Text trên Typtb
            Typetb.TextChanged += (s, e) =>
            {
                //Nếu Type = BASIC hoặc ADVANCED thì nút Save được kích hoạt
                if (Typetb.Text.ToUpper() == "BASIC" || Typetb.Text.ToUpper() == "ADVANCED")
                {
                    btnSave.IsEnabled = true;
                }
            };
            
            //Sự kiện ấn nút Save
            btnSave.MouseLeftButtonDown += (s, e) =>
            {
                //Thêm hợp đồng
                InsuranceContractsManager.InsuranceContractsManagement.AddContract(Typetb.Text.ToUpper(), Custb.Text, Bentb.Text, Convert.ToInt64(Valtb.Text), Convert.ToInt32(Termtb.Text), DateTime.Now);

                //Hiển thị danh sách mới
                var listView = new PageList();
                NavigationService.GetNavigationService(this).Navigate(listView);
            };
        }

        public PageCreate(Contract contract)    //Gọi khi sửa hoặc xóa hợp đồng
        {
            InitializeComponent();

            btnSave.IsEnabled = false;  //nút Save không kích hoạt

            //Các TextBox hiển thị thông tin của hợp đồng được truyền vào
            //Thông tin Type không cho phép sửa
            //Thay đổi thông tin hợp đồng thì kích hoạt nút Save và nút Delete không kích hoạt
            Typetb.Text = contract.type;
            Typetb.IsReadOnly = true; 
            Custb.Text = contract.customer;
            Custb.TextChanged += (s, e) => { btnSave.IsEnabled = true; btnDel.IsEnabled = false; };
            Bentb.Text = contract.benificiary;
            Bentb.TextChanged += (s, e) => { btnSave.IsEnabled = true; btnDel.IsEnabled = false; };
            Valtb.Text = contract.value.ToString();
            Valtb.TextChanged += (s, e) => { btnSave.IsEnabled = true; btnDel.IsEnabled = false; };
            Termtb.Text = contract.term.ToString();
            Termtb.TextChanged += (s, e) => { btnSave.IsEnabled = true; btnDel.IsEnabled = false; };

            //Sự kiện ấn nút Save
            btnSave.MouseLeftButtonDown += (s, e) =>
            {
                //Sửa hợp đồng
                InsuranceContractsManager.InsuranceContractsManagement.ModifyContract(contract.ID, Typetb.Text.ToUpper(), Custb.Text, Bentb.Text, Convert.ToInt64(Valtb.Text), Convert.ToInt32(Termtb.Text));
                
                //Hiển thị lại danh sách mới
                var listView = new PageList();
                NavigationService.GetNavigationService(this).Navigate(listView);
            };

            //Sự kiện ấn nút Delete
            btnDel.MouseLeftButtonDown += (s, e) =>
            {
                //Xóa hợp đồng
                InsuranceContractsManager.InsuranceContractsManagement.RemoveContract(contract.ID);
                
                //Hiện thị danh sách mới
                var listView = new PageList();
                NavigationService.GetNavigationService(this).Navigate(listView);
            };
        }
    }
}
