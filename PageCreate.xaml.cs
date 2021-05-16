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
        public PageCreate()
        {
            InitializeComponent();

            btnDel.Visibility = Visibility.Hidden;
            btnSave.IsEnabled = false;

            Typetb.TextChanged += (s, e) =>
            {
                if (Typetb.Text.ToUpper() == "BASIC" || Typetb.Text.ToUpper() == "ADVANCED")
                {
                    btnSave.IsEnabled = true;
                }
            };
            
            btnSave.MouseLeftButtonDown += (s, e) =>
            {
                InsuranceContractsManager.InsuranceContractsManagement.AddContract(Typetb.Text.ToUpper(), Custb.Text, Bentb.Text, Convert.ToInt64(Valtb.Text), Convert.ToInt32(Termtb.Text), DateTime.Now);

                var listView = new PageList();
                NavigationService.GetNavigationService(this).Navigate(listView);
            };
        }

        public PageCreate(Contract contract)
        {
            InitializeComponent();

            btnSave.IsEnabled = false;

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

            btnSave.MouseLeftButtonDown += (s, e) =>
            {
                InsuranceContractsManager.InsuranceContractsManagement.ModifyContract(contract.ID, Typetb.Text.ToUpper(), Custb.Text, Bentb.Text, Convert.ToInt64(Valtb.Text), Convert.ToInt32(Termtb.Text));
                
                var listView = new PageList();
                NavigationService.GetNavigationService(this).Navigate(listView);
            };

            btnDel.MouseLeftButtonDown += (s, e) =>
            {
                InsuranceContractsManager.InsuranceContractsManagement.RemoveContract(contract.ID);
                
                var listView = new PageList();
                NavigationService.GetNavigationService(this).Navigate(listView);
            };
        }
    }
}
