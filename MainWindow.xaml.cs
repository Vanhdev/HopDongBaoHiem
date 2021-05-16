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

            btnList.Click += (s, e) =>
            {
                var listView = new PageList();
                mainContent.Navigate(listView);
            };

            btnCreate.Click += (s, e) =>
            {
                var createForm = new PageCreate();
                mainContent.Navigate(createForm);
            };

            btnSearch.Click += (s, e) =>
            {
                 var searchPage = new SearchPage();
                 mainContent.Navigate(searchPage);
            };

        }

    }
}
