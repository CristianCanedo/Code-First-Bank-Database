namespace BankAccountPrototype.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountId = c.Int(nullable: false),
                        AccountBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccountType = c.String(),
                    })
                .PrimaryKey(t => t.AccountId)
                .ForeignKey("dbo.Customers", t => t.AccountId)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionId = c.Int(nullable: false),
                        TransactionType = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.Accounts", t => t.TransactionId)
                .Index(t => t.TransactionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "TransactionId", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "AccountId", "dbo.Customers");
            DropIndex("dbo.Transactions", new[] { "TransactionId" });
            DropIndex("dbo.Accounts", new[] { "AccountId" });
            DropTable("dbo.Transactions");
            DropTable("dbo.Customers");
            DropTable("dbo.Accounts");
        }
    }
}
