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
        // Original balance

        private double TotalMonthlyPayment(double balance, int term, double rate)
        {
            return (balance) * (rate / 1200) / (1 - Math.Pow((1 + rate / 1200), (term * -1)));
        }

        [HttpPost]
        public int RemoveRow(Row[] allCashflows, int index, Row[] aggregate)
        {
            return 91293;
        }

        [HttpPost]
        public Row[][] GetRow(double balance, int term, double rate, Row[] aggregate)
        {
            if (rate <= 1)
                rate *= 100;
            int oldLength = 0;
            if (aggregate == null)
            {
                aggregate = new Row[term];
                for (int i = 0; i < term; i++)
                    aggregate[i] = new Row();
            }
            else if (aggregate.Length < term)
            {
                Row[] newAgg = new Row[term];
                for (int i = 0; i < term; i++)
                {

                    if (i >= aggregate.Length)
                        newAgg[i] = new Row();
                    else
                        newAgg[i] = aggregate[i];
                }
                oldLength = aggregate.Length;
                aggregate = newAgg;
            }

            double totalMonthlyPayment = TotalMonthlyPayment(balance, term, rate);
            Row[] cashflow = new Row[term];

            Row row = new Row();
            row.month = 1;
            row.interest = balance * rate / 1200;
            row.principal = totalMonthlyPayment - row.interest;
            row.remBalance = balance - row.principal;
            cashflow[0] = row;

            //if (term > oldLength)
            //    aggregate[0] = new Row();
            aggregate[0].month = 1;
            aggregate[0].interest += row.interest;
            aggregate[0].principal += row.principal;
            aggregate[0].remBalance += row.remBalance;

            for (int i = 1; i <= term - 1; i++)
            {
                row = new Row();
                row.month = i + 1;
                row.interest = cashflow[i - 1].remBalance * rate / 1200;
                row.principal = totalMonthlyPayment - row.interest;
                row.remBalance = cashflow[i - 1].remBalance - row.principal;
                cashflow[i] = row;

                // if (term > oldLength)
                //       aggregate[i] = new Row();
                aggregate[i].month = i + 1;
                aggregate[i].interest += row.interest;
                aggregate[i].principal += row.principal;
                aggregate[i].remBalance += row.remBalance;
            }

            Row[][] res = { cashflow, aggregate };
            return res;
        }
    }
}




