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
using InsuranceContractsManager;

namespace HopDongBaoHiem
{
    /// <summary>
    /// Interaction logic for PageList.xaml
    /// </summary>
    public partial class PageList : Page
    {
        public PageList()
        {
            InitializeComponent();

            List<Contract> contracts = new List<Contract>();
            foreach(InsuranceContract con in InsuranceContractsManager.InsuranceContractsManagement.GetListContract())
            {
                contracts.Add(new Contract()
                {
                    ID = con.GetID(),
                    customer = con.GetBuyerName(),
                    benificiary = con.GetBeneficiaryName(),
                    value = con.GetAmount(),
                    term = con.GetContractTerm(),
                    date = con.GetSigningDate(),
                    type = con.Gettype()
                }); 
            }
            ContractData.ItemsSource = contracts;
            ContractData.MouseDoubleClick += (s, e) =>
             {
                 Contract contract = (Contract)ContractData.SelectedItem;
                 var createForm = new PageCreate(contract);
                 NavigationService.GetNavigationService(this).Navigate(createForm);
             };
        }
        public PageList(List<InsuranceContract> cons)
        {
            InitializeComponent();

            List<Contract> contracts = new List<Contract>();
            foreach (InsuranceContract con in cons)
            {
                contracts.Add(new Contract()
                {
                    ID = con.GetID(),
                    customer = con.GetBuyerName(),
                    benificiary = con.GetBeneficiaryName(),
                    value = con.GetAmount(),
                    term = con.GetContractTerm(),
                    date = con.GetSigningDate(),
                    type = con.Gettype()
                });
            }
            ContractData.ItemsSource = contracts;
            ContractData.MouseDoubleClick += (s, e) =>
            {
                Contract contract = (Contract)ContractData.SelectedItem;
                var createForm = new PageCreate(contract);
                NavigationService.GetNavigationService(this).Navigate(createForm);
            };
        }
    }
    public class Contract
    {
        public string ID { get; set; }
        public string customer { get; set; }
        public string benificiary { get; set; }
        public long value { get; set; }
        public int term { get; set; }
        public DateTime date { get; set; }
        public string type { get; set; }
    }
}
