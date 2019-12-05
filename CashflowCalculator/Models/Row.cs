using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashflowCalculator.Models
{
    public class Row
    {
        public int month { get; set; }
        public double interest { get; set; }
        public double principal { get; set; }
        public double remBalance { get; set; }
    }
}