namespace CashflowCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CashflowRows",
                c => new
                    {
                        CashflowRowId = c.Int(nullable: false, identity: true),
                        Month = c.Int(nullable: false),
                        InterestPayment = c.Double(nullable: false),
                        PrincipalPayment = c.Double(nullable: false),
                        RemainingBalance = c.Double(nullable: false),
                        LoanId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CashflowRowId)
                .ForeignKey("dbo.Loans", t => t.LoanId, cascadeDelete: true)
                .Index(t => t.LoanId);
            
            CreateTable(
                "dbo.Loans",
                c => new
                    {
                        LoanId = c.Int(nullable: false, identity: true),
                        Principal = c.Double(nullable: false),
                        Term = c.Int(nullable: false),
                        Rate = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.LoanId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CashflowRows", "LoanId", "dbo.Loans");
            DropIndex("dbo.CashflowRows", new[] { "LoanId" });
            DropTable("dbo.Loans");
            DropTable("dbo.CashflowRows");
        }
    }
}
