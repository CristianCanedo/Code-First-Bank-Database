namespace BankAccountPrototype.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OneToOne : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Accounts", "CustomerId", "dbo.Customers");
            DropPrimaryKey("dbo.Accounts");
            AlterColumn("dbo.Accounts", "AccountId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Accounts", "CustomerId");
            AddForeignKey("dbo.Accounts", "CustomerId", "dbo.Customers", "CustomerId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accounts", "CustomerId", "dbo.Customers");
            DropPrimaryKey("dbo.Accounts");
            AlterColumn("dbo.Accounts", "AccountId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Accounts", "AccountId");
            AddForeignKey("dbo.Accounts", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
        }
    }
}
