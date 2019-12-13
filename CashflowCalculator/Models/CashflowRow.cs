using System.ComponentModel.DataAnnotations;

namespace CashflowCalculator.Models
{
    public class CashflowRow
    {
        [Key]
        public int CashflowRowId { get; set; }

        public int Month { get; set; }

        public double InterestPayment { get; set; }

        public double PrincipalPayment { get; set; }

        public double RemainingBalance { get; set; }

        public int LoanId { get; set; }
    }
}