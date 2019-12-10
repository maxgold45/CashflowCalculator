using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CashflowCalculator.Models;


namespace CashflowCalculator.Controllers
{
    public class LoanController : ApiController
    {

        [HttpPost]
        public int RemoveRow(CashflowRow[] allCashflows, int index, CashflowRow[] aggregate)
        {
            return 91293;
        }

        [HttpPost]
        public CashflowRow[][] GetRow(double balance, int term, double rate, CashflowRow[] aggregate)
        {
            if (rate <= 1)
                rate *= 100;
            int oldLength = 0;
            if (aggregate == null)
            {
                aggregate = new CashflowRow[term];
                for (int i = 0; i < term; i++)
                    aggregate[i] = new CashflowRow();
            }
            else if (aggregate.Length < term)
            {
                CashflowRow[] newAgg = new CashflowRow[term];
                for (int i = 0; i < term; i++)
                {

                    if (i >= aggregate.Length)
                        newAgg[i] = new CashflowRow();
                    else
                        newAgg[i] = aggregate[i];
                }
                oldLength = aggregate.Length;
                aggregate = newAgg;
            }

            double totalMonthlyPayment = (balance) * (rate / 1200) / (1 - Math.Pow((1 + rate / 1200), (term * -1)));
            CashflowRow[] cashflow = new CashflowRow[term];

            CashflowRow row = new CashflowRow
            {
                Month = 1,
                InterestPayment = balance * rate / 1200
            };
            row.PrincipalPayment = totalMonthlyPayment - row.InterestPayment;
            row.RemainingBalance = balance - row.PrincipalPayment;
            cashflow[0] = row;
            
            aggregate[0].Month = 1;
            aggregate[0].InterestPayment += row.InterestPayment;
            aggregate[0].PrincipalPayment += row.PrincipalPayment;
            aggregate[0].RemainingBalance += row.RemainingBalance;

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

                aggregate[i].Month = i + 1;
                aggregate[i].InterestPayment += row.InterestPayment;
                aggregate[i].PrincipalPayment += row.PrincipalPayment;
                aggregate[i].RemainingBalance += row.RemainingBalance;
            }

            CashflowRow[][] res = { cashflow, aggregate };
            return res;
        }
    }
}




