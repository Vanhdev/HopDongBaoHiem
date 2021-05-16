using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Navigation;
using InsuranceContractsManager;

namespace HopDongBaoHiem
{
    public partial class PageList : Page
    {
        public PageList()   //Gọi khi hiển thị tất cả hợp đồng
        {
            InitializeComponent();

            List<Contract> contracts = new List<Contract>();    //Tạo danh sách để liên kết với DataGrid

            //Copy thông tin từ danh sách hợp đồng ở InsuranceContractsManagement sang danh sách vừa tạo
            foreach(InsuranceContract con in InsuranceContractsManagement.GetListContract())
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
            ContractData.ItemsSource = contracts;   //Liên kết DataGrid với danh sách vừa tạo để hiển thị danh sách

            //Sự kiện double click vào dòng trong danh sách
            ContractData.MouseDoubleClick += (s, e) =>
             {
                 Contract contract = (Contract)ContractData.SelectedItem;   //Lấy thông tin hợp đồng trên dòng

                 //Hiển thị Form sửa/xóa với thông tin của hợp đồng trên dòng
                 var createForm = new PageCreate(contract);
                 NavigationService.GetNavigationService(this).Navigate(createForm);
             };
        }
        public PageList(List<InsuranceContract> cons)   //Gọi khi thống kê, tìm kiếm hợp đồng
        {
            InitializeComponent();

            List<Contract> contracts = new List<Contract>();    //Tạo một danh sách hợp đồng

            //Copy thông tin từ danh sách được truyền vào sang danh sách vừa tạo
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
            ContractData.ItemsSource = contracts;   //Liên kết DataGrid với danh sách vừa tạo để hiển thị danh sách

            //Sự kiện double click vào dòng trong danh sách
            ContractData.MouseDoubleClick += (s, e) =>
            {
                Contract contract = (Contract)ContractData.SelectedItem;    //Lấy thông tin hợp đồng trên dòng

                //Hiển thị Form sửa/xóa với thông tin của hợp đồng trên dòng
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
