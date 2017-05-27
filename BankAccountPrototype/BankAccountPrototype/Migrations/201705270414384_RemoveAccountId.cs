namespace BankAccountPrototype.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAccountId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Accounts", "AccountId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accounts", "AccountId", c => c.Int(nullable: false));
        }
    }
}
