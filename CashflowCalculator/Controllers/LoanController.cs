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

        [HttpGet]
        public double getInterestPayment(Loan loan)
        {
            return loan.balance * loan.rate / 1200;
        }
    }
}
