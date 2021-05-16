using System;

namespace InsuranceContractsManager
{
    internal class AdvancedInsuranceContract : InsuranceContract
    {
        public AdvancedInsuranceContract(string buyer_name, string beneficiary_name,
            long amount, int contract_term, DateTime signing_date)
            : base(buyer_name, beneficiary_name, amount, contract_term, signing_date)
        {
            this.type = "ADVANCED";
            this.id = "A" + signing_date.ToString("ddMMyyyyHHmmss");
        }

        public override double CalculateProfit()
        {
            return (double)(0.17 * amount);
        }
    }
}