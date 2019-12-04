using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashflowCalculator.Models
{
    public class Loan
    {
        public double balance { get; set; }
        public int term { get; set; }
        public double rate { get; set; }
    }
}