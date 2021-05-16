using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace HopDongBaoHiem
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        public SearchPage()
        {
            InitializeComponent();

            Termtxt.TextChanged += (s, e) => { btnSearchByTerm.IsEnabled = true; };     //Kích hoạt nút Search bên trên khi thay đổi Term

            //Sự kiện thay đổi ngày trên From
            dateFrom.SelectedDateChanged += (s, e) =>
            {
                //Sự kiện thay đổi ngày trên To
                dateTo.SelectedDateChanged += delegate { btnSearchByDate.IsEnabled = true; };   //Kích hoạt nút Search bên dưới khi thay đổi ngày trên From và To
            };

            //Sự kiện ấn nút Search bên trên
            btnSearchByTerm.MouseLeftButtonDown += (s, e) =>
            {
                //Tạo một danh sách hợp đồng, gán bằng danh sách hợp đồng tìm kiếm được
                List<InsuranceContractsManager.InsuranceContract> contracts = InsuranceContractsManager.InsuranceContractsManagement.Search(Convert.ToInt32(Termtxt.Text));

                //Hiển thị danh sách hợp đồng tìm kiếm được
                var listView = new PageList(contracts);
                mainFrame.Navigate(listView);

                //Xóa thông tin lợi nhuận nếu có
                profitShow.Children.Clear();
            };

            //Sự kiện ấn nút Search bên dưới
            btnSearchByDate.MouseLeftButtonDown += (s, e) =>
             {
                 //Tạo một danh sách hợp đồng, gán bằng danh sách hợp đồng tìm kiếm được
                 List<InsuranceContractsManager.InsuranceContract> contracts = InsuranceContractsManager.InsuranceContractsManagement.Search(Convert.ToDateTime(dateFrom.SelectedDate), Convert.ToDateTime(dateTo.SelectedDate));

                 //Tính lợi nhuận thu được
                 double x = InsuranceContractsManager.InsuranceContractsManagement.CalculateProfit(contracts);

                 //Thông tin lợi nhuận cần hiển thị
                 string profit = "Profit = " + x.ToString();

                 //Hiển thị danh sách hợp đồng tìm được
                 var listView = new PageList(contracts);
                 mainFrame.Navigate(listView);

                 //Xóa thông tin lợi nhuận cũ thay bằng thông tin lợi nhuận mới
                 profitShow.Children.Clear();
                 profitShow.Children.Add(new Label { Content = profit, FontSize = 20});
             };
        }
    }
}
