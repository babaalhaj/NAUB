namespace NAUB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsReturnedColumnToborrowsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Borrows", "IsReturned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Borrows", "IsReturned");
        }
    }
}
