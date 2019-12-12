# CashflowCalculator

This program is an application to calculate fixed rate loan cashflows. It takes loan information such as Balance, Term, and Rate as inputs from end user and displays tabular cashflow of each loan. It also calculates and displays a cashflow of an aggregate loan based on the loan information. 

Approach

We developed our cashflow calculator using Microsoft's .NET Framework (C#) for the back end and AngularJS for the front end. We used CSS (including bootstrap CSS) for our front end as well. Our program had the capability to accept multiple loans on a click on a button, display each cashflow loan, and display a cashflow table for the aggregate pool of loans. 

We then repeated our project but this time, by using Entity Framework to store, add, and remove our databases, which includes a cashflow loans database and an aggregate cashflow loans database. 

If we had more time, we could work on speeding up our add and delete time by not pulling in the whole database everytime the user makes a change on the front end.
