using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CashflowCalculator.Models
{
    public class CashflowRow
    {
        [Key]
        public int CashflowRowID { get; set; }

        public int Month { get; set; }

        public double InterestPayment { get; set; }

        public double PrincipalPayment { get; set; }

        public double RemainingBalance { get; set; }

        [ForeignKey("LoanID")]
        public int LoanID { get; set; }

        public Loan Loan { get; set; }
    }
}