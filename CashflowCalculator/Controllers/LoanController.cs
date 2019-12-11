using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CashflowCalculator.Models;
using CashflowCalculator;
using System.Data.SqlClient;

namespace CashflowCalculator.Controllers
{
    public class LoanController : ApiController
    {
        public CashflowsContext context = new CashflowsContext();

        [HttpPost]
        public int RemoveRow(CashflowRow[] allCashflows, int index, CashflowRow[] aggregate)
        {
            return 91293;
        }


        [HttpGet]
        public List<Loan> GetAllLoans()
        {
            List<Loan> loans = context.Loans.SqlQuery("SELECT * FROM dbo.Loans ORDER BY LoanId ASC").ToList();
            return loans;
        }

        [HttpGet]
        public List<CashflowRow> GetCashflowRows(int LoanId)
        {
            List<CashflowRow> cashflow = context.CashflowRows.SqlQuery("SELECT * FROM dbo.CashflowRows WHERE LoanId = @id", new SqlParameter("id", LoanId)).ToList(); 
            return cashflow;
        }

        [HttpPost]
        public void AddLoan(double principal, int term, double rate, CashflowRow[] aggregate)
        {
            var loan = new Loan { Principal = principal, Term = term, Rate = rate };
            context.Loans.Add(loan);

            if (rate <= 1)
                rate *= 100;

            double totalMonthlyPayment = (principal) * (rate / 1200) / (1 - Math.Pow((1 + rate / 1200), (term * -1)));
            CashflowRow[] cashflow = new CashflowRow[term];

            CashflowRow row = new CashflowRow
            {
                Month = 1,
                InterestPayment = principal * rate / 1200
            };
            row.PrincipalPayment = totalMonthlyPayment - row.InterestPayment;
            row.RemainingBalance = principal - row.PrincipalPayment;
            cashflow[0] = row;
            context.CashflowRows.Add(row);

            for (int i = 1; i <= term - 1; i++)
            {
                row = new CashflowRow
                {
                    Month = i + 1,
                    InterestPayment = cashflow[i - 1].RemainingBalance * rate / 1200
                };
                row.PrincipalPayment = totalMonthlyPayment - row.InterestPayment;
                row.RemainingBalance = cashflow[i - 1].RemainingBalance - row.PrincipalPayment;
                cashflow[i] = row;

                context.CashflowRows.Add(row);
            }
            context.SaveChanges();
        }
    }
}




////public CashflowRow[][] AddLoan(double principal, int term, double rate, CashflowRow[] aggregate)
////        {   

////            if (rate <= 1)
////                rate *= 100;
////            int oldLength = 0;
////            if (aggregate == null)
////            {
////                aggregate = new CashflowRow[term];
////                for (int i = 0; i < term; i++)
////                    aggregate[i] = new CashflowRow();
////            }
////            else if (aggregate.Length < term)
////            {
////                CashflowRow[] newAgg = new CashflowRow[term];
////                for (int i = 0; i < term; i++)
////                {

////                    if (i >= aggregate.Length)
////                        newAgg[i] = new CashflowRow();
////                    else
////                        newAgg[i] = aggregate[i];
////                }
////                oldLength = aggregate.Length;
////                aggregate = newAgg;
////            }

////            double totalMonthlyPayment = (principal) * (rate / 1200) / (1 - Math.Pow((1 + rate / 1200), (term * -1)));
////            CashflowRow[] cashflow = new CashflowRow[term];

////            CashflowRow row = new CashflowRow
////            {
////                Month = 1,
////                InterestPayment = principal * rate / 1200
////            };
////            row.PrincipalPayment = totalMonthlyPayment - row.InterestPayment;
////            row.RemainingBalance = principal - row.PrincipalPayment;
////            cashflow[0] = row;

////            aggregate[0].Month = 1;
////            aggregate[0].InterestPayment += row.InterestPayment;
////            aggregate[0].PrincipalPayment += row.PrincipalPayment;
////            aggregate[0].RemainingBalance += row.RemainingBalance;

////            for (int i = 1; i <= term - 1; i++)
////            {
////                row = new CashflowRow
////                {
////                    Month = i + 1,
////                    InterestPayment = cashflow[i - 1].RemainingBalance * rate / 1200
////                };
////                row.PrincipalPayment = totalMonthlyPayment - row.InterestPayment;
////                row.RemainingBalance = cashflow[i - 1].RemainingBalance - row.PrincipalPayment;
////                cashflow[i] = row;

////                aggregate[i].Month = i + 1;
////                aggregate[i].InterestPayment += row.InterestPayment;
////                aggregate[i].PrincipalPayment += row.PrincipalPayment;
////                aggregate[i].RemainingBalance += row.RemainingBalance;
////            }

////            CashflowRow[][] res = { cashflow, aggregate };
////            return res;
////        }
////    }
////}




