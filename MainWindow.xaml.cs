using System.Windows;

namespace HopDongBaoHiem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Sự kiện ấn nút Contract lits
            btnList.Click += (s, e) =>
            {
                //Hiển thị danh sách ở mainContent
                var listView = new PageList();
                mainContent.Navigate(listView);
            };

            //Sự kiện ấn nút Create
            btnCreate.Click += (s, e) =>
            {
                //Hiển thị Form tạo hợp đồng mới ở mainContent
                var createForm = new PageCreate();
                mainContent.Navigate(createForm);
            };

            //Sự kiên ấn nút Search
            btnSearch.Click += (s, e) =>
            {
                //Hiển thị trang tìm kiếm ở mainContent
                 var searchPage = new SearchPage();
                 mainContent.Navigate(searchPage);
            };

        }

    }
}
