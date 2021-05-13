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
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        public SearchPage()
        {
            InitializeComponent();

            Termtxt.TextChanged += (s, e) => { btnSearchByTerm.IsEnabled = true; };

            dateFrom.SelectedDateChanged += (s, e) =>
            {
                dateTo.SelectedDateChanged += delegate { btnSearchByDate.IsEnabled = true; };
            };

            btnSearchByTerm.MouseLeftButtonUp += (s, e) =>
            {
                List<InsuranceContractsManager.InsuranceContract> contracts = InsuranceContractsManager.InsuranceContractsManager.Search(Convert.ToInt32(Termtxt.Text));

                var listView = new PageList(contracts);
                mainFrame.Navigate(listView);
            };

            btnSearchByDate.MouseLeftButtonUp += (s, e) =>
             {
                 List<InsuranceContractsManager.InsuranceContract> contracts = InsuranceContractsManager.InsuranceContractsManager.Search(Convert.ToDateTime(dateFrom.SelectedDate), Convert.ToDateTime(dateTo.SelectedDate));
                 double x = InsuranceContractsManager.InsuranceContractsManager.CalculateProfit(contracts);
                 string profit = "Profit = " + x.ToString();

                 var listView = new PageList(contracts);
                 mainFrame.Navigate(listView);

                 profitShow.Children.Clear();
                 profitShow.Children.Add(new TextBlock { Text = profit });
             };
        }
    }
}
