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
        [HttpGet]
        private double TotalMonthlyPayment(double balance, int term, double rate)
        {
            return (balance) * (rate / 1200) / (1 - Math.Pow((1 + rate / 1200), (term * -1)));
        }


        private double GetInterestPayment(double balance, int term, double rate)
        {

            Loan loan = new Loan();
            loan.balance = balance;
            loan.term = term;
            loan.rate = rate;

            return loan.balance * loan.rate / 1200;
        }

        [HttpGet]
        public Row[] GetRow(double balance, int term, double rate)
        {
            //if (rate >= 1)
            //    rate /= 100;

            double totalMonthlyPayment = TotalMonthlyPayment(balance, term, rate);
            Row[] cashflow = new Row[term];

            Row row = new Row();
            row.month = 1;
            row.interest = balance * rate / 1200;
            row.principal = totalMonthlyPayment - row.interest;
            row.remBalance = balance - row.principal;
      
            cashflow[0] = row;

            for (int i = 1; i <= term - 1; i++)
            {
                row = new Row();
                row.month = i + 1;
                row.interest = cashflow[i - 1].remBalance * rate / 1200; 
                row.principal = totalMonthlyPayment - row.interest;
                row.remBalance = cashflow[i - 1].remBalance - row.principal;
 /*               if (totalMonthlyPayment > row.remBalance)
                {
                    row.principal += row.remBalance;
                    row.remBalance = 0;
                }*/
                cashflow[i] = row;
            }

            return cashflow;
        }


    }
}
