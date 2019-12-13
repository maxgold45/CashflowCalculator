using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CashflowCalculator.Models
{
    public class Loan
    {
        [Key]
        public int LoanId { get; set; }

        public double Principal { get; set; }

        public int Term { get; set; }

        public double Rate { get; set; }

        public ICollection<CashflowRow> CashflowRows { get; set; }
    }
}