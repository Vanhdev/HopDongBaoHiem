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
            using (var sr = new System.IO.StreamReader(file))
            {
                string[] datas = sr.ReadToEnd().Split('\n');
                foreach (string data in datas)
                {
                    string[] dataItem = data.Split('|');

                    if (dataItem[0].Contains("A"))
                    {
                        contracts.Add(new AdvancedInsuranceContract(dataItem[1], dataItem[2], Convert.ToInt64(dataItem[3]), Convert.ToInt32(dataItem[4]), Convert.ToDateTime(dataItem[5])));
                    }
                    if (dataItem[0].Contains("B"))
                    {
                        contracts.Add(new BasicInsuranceContract(dataItem[1], dataItem[2], Convert.ToInt64(dataItem[3]), Convert.ToInt32(dataItem[4]), Convert.ToDateTime(dataItem[5])));
                    }
                }
            }
        }
        public static void SaveData(string file)
        {
            using (var sw = new System.IO.StreamWriter(file))
            {
                foreach (InsuranceContract contract in contracts)
                {
                    string s = contract.GetID() + "|" + contract.GetBuyerName() + "|" + contract.GetBeneficiaryName() + "|" + contract.GetAmount().ToString() + "|" + contract.GetContractTerm().ToString() + "|" + contract.GetSigningDate().ToString();
                    sw.WriteLine(s);
                }
            }
        }
        public static void AddContract(string type, string buyer_name, string beneficiary_name,
            long amount, int contract_term, DateTime signing_date)
        {
            if (type == "BASIC")
                contracts.Add(new BasicInsuranceContract(buyer_name, beneficiary_name, amount, contract_term, signing_date));
            else
                contracts.Add(new AdvancedInsuranceContract(buyer_name, beneficiary_name, amount, contract_term, signing_date));
        }
        public static void RemoveContract(string ID)
        {
            foreach (var removeContract in contracts)
            {
                if (removeContract.GetID() == ID)
                {
                    contracts.Remove(removeContract);
                    return;
                }
            }
        }
        public static void ModifyContract(string ID, string new_type, string new_buyer_name, string new_beneficiary_name,
            long new_amount, int new_contract_term)
        {
            foreach (var modiContract in contracts)
            {
                if (modiContract.GetID() == ID)
                {
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
            List<InsuranceContract> searchTerm = new List<InsuranceContract>();
            foreach (InsuranceContract contr in contracts)
            {
                if (contr.GetContractTerm() == term)
                    searchTerm.Add(contr);
            }
            return searchTerm;
        }
        public static List<InsuranceContract> Search(DateTime sellFrom, DateTime sellTo)
        {
            List<InsuranceContract> searchSell = new List<InsuranceContract>();
            foreach (InsuranceContract contr in contracts)
            {
                int compareFrom = DateTime.Compare(sellFrom, contr.GetSigningDate());
                int compareTo = DateTime.Compare(sellTo, contr.GetSigningDate());
                if (compareFrom <= 0 && compareTo >= 0)
                    searchSell.Add(contr);
            }
            return searchSell;
        }
        public static double CalculateProfit()
        {
            double total = 0;
            foreach (InsuranceContract contr in contracts)
                total += contr.CalculateProfit();
            return total;
        }
        public static double CalculateProfit(List<InsuranceContract> contrs)
        {
            double total = 0;
            foreach (InsuranceContract con in contrs)
                total += con.CalculateProfit();
            return total;
        }
    }
}    