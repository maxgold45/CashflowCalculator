namespace CashflowCalculator.DTOs
{
    public class AggregateCashflowRowDTO
    {
        public int Month { get; set; }

        public double InterestPayment { get; set; }

        public double PrincipalPayment { get; set; }

        public double RemainingBalance { get; set; }
    }
}