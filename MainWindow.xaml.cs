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
