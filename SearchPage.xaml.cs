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

            Termtxt.TextChanged += (s, e) => { btnSearchByTerm.IsEnabled = true; };

            dateFrom.SelectedDateChanged += (s, e) =>
            {
                dateTo.SelectedDateChanged += delegate { btnSearchByDate.IsEnabled = true; };
            };

            btnSearchByTerm.MouseLeftButtonDown += (s, e) =>
            {
                List<InsuranceContractsManager.InsuranceContract> contracts = InsuranceContractsManager.InsuranceContractsManagement.Search(Convert.ToInt32(Termtxt.Text));

                var listView = new PageList(contracts);
                mainFrame.Navigate(listView);

                profitShow.Children.Clear();
            };

            btnSearchByDate.MouseLeftButtonDown += (s, e) =>
             {
                 List<InsuranceContractsManager.InsuranceContract> contracts = InsuranceContractsManager.InsuranceContractsManagement.Search(Convert.ToDateTime(dateFrom.SelectedDate), Convert.ToDateTime(dateTo.SelectedDate));


                 double x = InsuranceContractsManager.InsuranceContractsManagement.CalculateProfit(contracts);

                 string profit = "Profit = " + x.ToString();

                 var listView = new PageList(contracts);
                 mainFrame.Navigate(listView);

                 profitShow.Children.Clear();
                 profitShow.Children.Add(new Label { Content = profit, FontSize = 20});
             };
        }
    }
}
