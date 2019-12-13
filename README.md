# CashflowCalculator

This program is an application to calculate fixed rate loan cashflows. It takes loan information such as Balance, Term, and Rate as inputs from the end user and displays a tabular cashflow for each loan. It also calculates and displays an aggregate cashflow for all loans.

We developed our cashflow calculator using Microsoft's .NET Framework (C#) for the back end and AngularJS for the front end. We used CSS (including Bootstrap CSS) for our front end as well. Our program allows the user to add and remove loans. It will also validate user input so that the user will not crash the calculator. If the user inputs a decimal interest rate, then it will be converted to a number greater than 1.

We use Entity Framework to store, add, and remove loans from the SQL database. We also use the database only when necessary. If the user removes a loan, then the program will remove it from the database, and remove it locally. It does not call the database to update all cashflows.

We have one known bug in which the cashflow will not terminate to 0. It happens with very large term lengths and rates, but a very low starting balance. 
