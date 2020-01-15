namespace NAUB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSettingModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Settings", "OverdueFee");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Settings", "OverdueFee", c => c.String(nullable: false));
        }
    }
}
