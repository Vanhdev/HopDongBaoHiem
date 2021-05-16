using System;


//Trần Văn Kiên
namespace InsuranceContractsManager
{
    internal class AdvancedInsuranceContract : InsuranceContract
    {
        public AdvancedInsuranceContract(string buyer_name, string beneficiary_name,
            long amount, int contract_term, DateTime signing_date)
            : base(buyer_name, beneficiary_name, amount, contract_term, signing_date)
        {
            this.type = "ADVANCED";
            this.id = "A" + signing_date.ToString("ddMMyyyyHHmmss");    //Quy ước ID bắt đầu bằng A và các số sau là ngày, tháng, năm, giờ, phút, giây
        }

        public override double CalculateProfit()
        {
            return (double)(0.17 * amount);     //Lợi nhuận bằng 17% giá trị hợp đồng
        }
    }
}