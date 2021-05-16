using System;

//Trần Việt Anh
namespace InsuranceContractsManager
{
    internal class BasicInsuranceContract : InsuranceContract
    {
        public BasicInsuranceContract(string buyer_name, string beneficiary_name,
            long amount, int contract_term, DateTime signing_date)
            : base(buyer_name, beneficiary_name, amount, contract_term, signing_date)
        {
            this.type = "BASIC";
            this.id = "B" + signing_date.ToString("ddMMyyyyHHmmss");    //Quy ước ID bắt đầu bằng B và các số sau là ngày, tháng, năm, giờ, phút, giây
        }

        public override double CalculateProfit()
        {
            return (double)(0.1 * amount);      //Lợi nhuận bằng 10% giá trị hợp đồng
        }
    }
}