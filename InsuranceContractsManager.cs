using System;
using System.Collections.Generic;

namespace InsuranceContractsManager
{
    public static class InsuranceContractsManagement
    {
        private static List<InsuranceContract> contracts = new List<InsuranceContract>();
        public static List<InsuranceContract> GetListContract() { return contracts; }
        public static void LoadData(string file)
        {
            using (var sr = new System.IO.StreamReader(file))//đọc file text
            {
                //tách thông tin hợp đồng theo dòng
                string[] datas = sr.ReadToEnd().Split('\n');
                foreach (string data in datas)
                {
                    //tách thông tin từng hợp đồng
                    string[] dataItem = data.Split('|');

                    //thêm hợp đồng 
                    if (dataItem[0].Contains("A"))  //Nếu ID chứa chữ A
                    {
                        //Thêm hợp đồng loại advanced
                        contracts.Add(new AdvancedInsuranceContract(dataItem[1], dataItem[2], Convert.ToInt64(dataItem[3]), Convert.ToInt32(dataItem[4]), Convert.ToDateTime(dataItem[5])));
                    }
                    if (dataItem[0].Contains("B"))  //Nếu ID chứa chữ B
                    {
                        //Thêm hợp đồng loại basic
                        contracts.Add(new BasicInsuranceContract(dataItem[1], dataItem[2], Convert.ToInt64(dataItem[3]), Convert.ToInt32(dataItem[4]), Convert.ToDateTime(dataItem[5])));
                    }
                }
            }
        }
        public static void SaveData(string file)
        {
            using (var sw = new System.IO.StreamWriter(file))//ghi lại file text
            {
                //mỗi hợp đồng sẽ ghi thông tin trên một dòng
                foreach (InsuranceContract contract in contracts)   //Duyệt từng hợp đồng
                {
                    //Quy ước từng thuộc tính của hợp đồng ngăn các nhau bởi dấu "|" và mỗi hợp đồng nằm trên một dòng
                    string s = contract.GetID() + "|" + contract.GetBuyerName() + "|" + contract.GetBeneficiaryName() + "|" + contract.GetAmount().ToString() + "|" + contract.GetContractTerm().ToString() + "|" + contract.GetSigningDate().ToString();
                    sw.WriteLine(s);    //Viết lại dòng s vào file
                }
            }
        }
        public static void AddContract(string type, string buyer_name, string beneficiary_name,
            long amount, int contract_term, DateTime signing_date)
        {
            if (type == "BASIC")                                                                                                //Nếu loại BASIC
                contracts.Add(new BasicInsuranceContract(buyer_name, beneficiary_name, amount, contract_term, signing_date));   //Thêm một BasicInsuranceContract và danh sách
            else                                                                                                                //Ngược lại
                contracts.Add(new AdvancedInsuranceContract(buyer_name, beneficiary_name, amount, contract_term, signing_date));//Thêm một AdvancedInsuranceContract vào danh sách
        }
        public static void RemoveContract(string ID)
        {
            foreach (var removeContract in contracts)   //Duyệt từng hợp đồng trong danh sách
            {
                if (removeContract.GetID() == ID)   //Tìm hợp đồng cần xóa
                {
                    contracts.Remove(removeContract);   //Xóa hợp đồng khỏi danh sách
                    return;
                }
            }
        }
        public static void ModifyContract(string ID, string new_type, string new_buyer_name, string new_beneficiary_name,
            long new_amount, int new_contract_term)
        {
            foreach (var modiContract in contracts)     //Duyệt từng hợp đồng trong danh sách
            {
                if (modiContract.GetID() == ID)     //Tìm hợp đồng cần xóa
                {
                    //Thay đổi thông tin hợp đồng
                    modiContract.SetType(new_type);
                    modiContract.SetBuyerName(new_buyer_name);
                    modiContract.SetBeneficiaryName(new_beneficiary_name);
                    modiContract.SetAmount(new_amount);
                    modiContract.SetContractTerm(new_contract_term);
                    return;
                }
            }
        }
        public static List<InsuranceContract> Search(int term)
        {
            List<InsuranceContract> searchTerm = new List<InsuranceContract>();     //Tạo một danh sách hợp đồng
            foreach (InsuranceContract contr in contracts)                          //Duyệt từng hợp đồng
            {
                if (contr.GetContractTerm() == term)                                //Tìm các hợp đồng có thời hạn thỏa mãn
                    searchTerm.Add(contr);                                          //Thêm hợp đồng vào danh sách 
            }
            return searchTerm;                                                      //Trả về danh sách hợp đồng
        }
        public static List<InsuranceContract> Search(DateTime sellFrom, DateTime sellTo)
        {
            List<InsuranceContract> searchSell = new List<InsuranceContract>();         //Tạo danh sách hợp đồng
            foreach (InsuranceContract contr in contracts)                              //Duyệt từng hợp đồng
            {
                //So sánh thời gian đăng kí
                int compareFrom = DateTime.Compare(sellFrom, contr.GetSigningDate());
                int compareTo = DateTime.Compare(sellTo, contr.GetSigningDate());
                if (compareFrom <= 0 && compareTo >= 0)
                    searchSell.Add(contr);                                              //Thêm hợp đồng vào danh sách
            }
            return searchSell;                                                          //Trả về danh sách hợp đồng
        }
        public static double CalculateProfit(List<InsuranceContract> contrs)
        {
            double total = 0;                           //Tạo biến total = 0
            foreach (InsuranceContract con in contrs)   //Duyệt từng hợp đồng
                total += con.CalculateProfit();         //Tính tổng lợi nhuận
            return total;                               //Trả về lợi nhuận
        }
    }
}    