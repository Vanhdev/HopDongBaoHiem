﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace HopDongBaoHiem
{
    public partial class PageCreate : Page
    {
        public PageCreate()     //Gọi khi tạo hợp đồng
        {
            InitializeComponent();

            btnDel.Visibility = Visibility.Hidden;  //Ẩn nút xóa 

            //Chỉ được chọn một trong hai
            Adbtn.Checked += (s, e) => { Babtn.IsChecked = false; };
            Babtn.Checked += (s, e) => { Adbtn.IsChecked = false; };

            //Sự kiện ấn nút Save
            btnSave.MouseLeftButtonDown += (s, e) =>
            {
                try
                {
                    string contype;
                    //Thay đổi Type theo lựa chọn của người dùng
                    if(Adbtn.IsChecked == true) contype = Adbtn.Content.ToString();
                    else contype = Babtn.Content.ToString();
                    //Thêm hợp đồng
                    InsuranceContractsManager.InsuranceContractsManagement.AddContract(contype, Custb.Text, Bentb.Text, Convert.ToInt64(Valtb.Text), Convert.ToInt32(Termtb.Text), DateTime.Now);

                    //Hiển thị danh sách mới
                    var listView = new PageList();
                    NavigationService.GetNavigationService(this).Navigate(listView);
                }
                catch
                {
                    //Kiểm tra đã điền đầy đủ thông tin chưa
                    if ((Adbtn.IsChecked == false && Babtn.IsChecked == false) || Bentb.Text.Equals("") || Valtb.Text.Equals("") || Termtb.Text.Equals("")) MessageBox.Show("Please, fill out this form!");
                }
            };
        }

        public PageCreate(Contract contract)    //Gọi khi sửa hoặc xóa hợp đồng
        {
            InitializeComponent();

            btnSave.IsEnabled = false;  //nút Save không kích hoạt

            //Chọn ADVANCED hoặc BASIC theo thông tin truyền vào
            if (contract.type == Adbtn.Content.ToString())
            {
                Adbtn.IsChecked = true;
            }
            else
            {
                Babtn.IsChecked = true;
            }

            //Không cho phép chỉnh sửa Type
            Babtn.IsEnabled = Adbtn.IsEnabled = false;

            //Hiển thị thông tin truyền vào
            //Kích hoạt nút Save và khóa nút Delete khi thay đổi thông tin
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
                try
                {
                    //Sửa hợp đồng
                    InsuranceContractsManager.InsuranceContractsManagement.ModifyContract(contract.ID, contract.type, Custb.Text, Bentb.Text, Convert.ToInt64(Valtb.Text), Convert.ToInt32(Termtb.Text));

                    //Hiển thị danh sách mới
                    var listView = new PageList();
                    NavigationService.GetNavigationService(this).Navigate(listView);
                }
                catch
                {
                    if (Bentb.Text == "" || Valtb.Text == "" || Termtb.Text == "") MessageBox.Show("Please, fill out this form!");
                }
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
