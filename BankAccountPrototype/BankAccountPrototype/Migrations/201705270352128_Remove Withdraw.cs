namespace BankAccountPrototype.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveWithdraw : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Accounts", "Withdraw");
            DropColumn("dbo.Accounts", "Deposit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accounts", "Deposit", c => c.Boolean(nullable: false));
            AddColumn("dbo.Accounts", "Withdraw", c => c.Boolean(nullable: false));
        }
    }
}
