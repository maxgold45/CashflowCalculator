using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CashflowCalculator.Models;

namespace CashflowCalculator
{
    public class CashflowsContext : DbContext
    {
        public CashflowsContext() : base()
        {

        }

        public DbSet<CashflowRow> CashflowRows { get; set; }
        public DbSet<Loan> Loans { get; set; }
    }
}