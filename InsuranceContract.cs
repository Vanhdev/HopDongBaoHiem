using System;
using System.Collections.Generic;

namespace InsuranceContractsManager
{
    public static class InsuranceContractsManager
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
            var removeContract = contracts.Find(x => x.GetID() == ID);
            contracts.Remove(removeContract);
        }
        public static void ModifyContract(string ID, string new_type, string new_buyer_name, string new_beneficiary_name,
            long new_amount, int new_contract_term)
        {
            var modiContract = contracts.Find(x => x.GetID() == ID);
            modiContract.SetType(new_type);
            modiContract.SetBuyerName(new_buyer_name);
            modiContract.SetBeneficiaryName(new_beneficiary_name);
            modiContract.SetAmount(new_amount);
            modiContract.SetContractTerm(new_contract_term);
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
    public abstract class InsuranceContract
    {
        protected string id;
        protected string buyer_name;
        protected string beneficiary_name;
        protected long amount;
        protected int contract_term;
        protected DateTime signing_date;
        protected string type;

        public string GetID() { return id; }
        public string GetBuyerName() { return buyer_name; }
        public string GetBeneficiaryName() { return beneficiary_name; }
        public long GetAmount() { return amount; }
        public int GetContractTerm() { return contract_term; }
        public DateTime GetSigningDate() { return signing_date; }
        public string Gettype() { return type; }
        public void SetBuyerName(string val) { buyer_name = val; }
        public void SetBeneficiaryName(string val) { beneficiary_name = val; }
        public void SetAmount(long val) { amount = val; }
        public void SetContractTerm(int val) { contract_term = val; }
        public void SetSigningDate(DateTime val) { signing_date = val; }
        public void SetType(string val) { type = val; }
        public InsuranceContract(string buyer_name, string beneficiary_name,
            long amount, int contract_term, DateTime signing_date)
        {
            this.buyer_name = buyer_name;
            this.beneficiary_name = beneficiary_name;
            this.amount = amount;
            this.contract_term = contract_term;
            this.signing_date = signing_date;
        }
        public abstract double CalculateProfit();
    }
    internal class BasicInsuranceContract : InsuranceContract
    {
        public BasicInsuranceContract(string buyer_name, string beneficiary_name,
            long amount, int contract_term, DateTime signing_date)
            : base(buyer_name, beneficiary_name, amount, contract_term, signing_date)
        {
            this.type = "BASIC";
            this.id = "B" + signing_date.ToString("ddMMyyyyHHmm");
        }

        public override double CalculateProfit()
        {
            return (double)(0.1 * amount);
        }
    }
    internal class AdvancedInsuranceContract : InsuranceContract
    {
        public AdvancedInsuranceContract(string buyer_name, string beneficiary_name,
            long amount, int contract_term, DateTime signing_date)
            : base(buyer_name, beneficiary_name, amount, contract_term, signing_date)
        {
            this.type = "ADVANCED";
            this.id = "A" + signing_date.ToString("ddMMyyyyHHmm");
        }

        public override double CalculateProfit()
        {
            return (double)(0.17 * amount);
        }
    }
}
