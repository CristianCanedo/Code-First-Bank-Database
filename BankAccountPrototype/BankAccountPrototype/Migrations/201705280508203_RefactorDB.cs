namespace BankAccountPrototype.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "FirstName", c => c.String());
            AddColumn("dbo.Accounts", "LastName", c => c.String());
            AddColumn("dbo.Accounts", "BirthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Accounts", "AccountType", c => c.String());
            DropColumn("dbo.Customers", "FirstName");
            DropColumn("dbo.Customers", "LastName");
            DropColumn("dbo.Customers", "BirthDate");
            DropColumn("dbo.Customers", "AccountType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "AccountType", c => c.String());
            AddColumn("dbo.Customers", "BirthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Customers", "LastName", c => c.String());
            AddColumn("dbo.Customers", "FirstName", c => c.String());
            DropColumn("dbo.Accounts", "AccountType");
            DropColumn("dbo.Accounts", "BirthDate");
            DropColumn("dbo.Accounts", "LastName");
            DropColumn("dbo.Accounts", "FirstName");
        }
    }
}
