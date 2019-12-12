using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CashflowCalculator.Models;
using CashflowCalculator;
using System.Data.SqlClient;
using CashflowCalculator.DTOs;

namespace CashflowCalculator.Controllers
{
    public class LoanController : ApiController
    {
        public CashflowsContext context = new CashflowsContext();

        [HttpDelete]
        public void RemoveLoan(int index)
        {
            context.Loans.Remove(context.Loans.Single(a => a.LoanId == index));
            context.SaveChanges();
        }


        [HttpGet]
        public List<Loan> GetAllLoans()
        {
            List<Loan> loans = context.Loans.Include("CashflowRows").OrderBy(x => x.LoanId).ToList();
            return loans;
        }

        [HttpPost]
        public Loan AddLoan(Loan inputLoan)
        {
            double principal = inputLoan.Principal;
            double rate = inputLoan.Rate;
            int term = inputLoan.Term;
            if (rate <= 1)
                rate *= 100;

            Loan result = context.Loans.Add(new Loan { Principal = inputLoan.Principal, Term = inputLoan.Term, Rate = inputLoan.Rate });

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


            return result;
        }

        
        [HttpGet]
        public List<AggregateCashflowRowDTO> GetAggregate()
        {

            List<AggregateCashflowRowDTO> aggregate =
                context
                .CashflowRows
                .GroupBy(x => x.Month)
                .AsEnumerable()
                .Select(grouping => new AggregateCashflowRowDTO()
                {
                    Month = grouping.Key,
                    PrincipalPayment = grouping.Sum(row => row.PrincipalPayment),
                    InterestPayment = grouping.Sum(row => row.InterestPayment),
                    RemainingBalance = grouping.Sum(row => row.RemainingBalance)
                })
                .ToList();

            return aggregate;
        }

    }
}