using System;

//Vũ Xuân Bắc
namespace InsuranceContractsManager
{
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
}
