using System;
using System.Collections.Generic;

namespace InsuranceContractsManager
{
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
}