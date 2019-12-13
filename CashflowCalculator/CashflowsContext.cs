using System.Data.Entity;
using CashflowCalculator.Models;

namespace CashflowCalculator
{
    public class CashflowsContext : DbContext
    {
        public CashflowsContext() : base("CashflowsContext") { }

        public DbSet<CashflowRow> CashflowRows { get; set; }
        public DbSet<Loan> Loans { get; set; }
    }
}