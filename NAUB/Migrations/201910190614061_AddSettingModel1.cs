namespace NAUB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSettingModel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Settings", "NumberOfDays", c => c.Short(nullable: false));
            DropColumn("dbo.Settings", "DefaultNumberOfDays");
            DropColumn("dbo.Settings", "MaximumNumberOfDays");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Settings", "MaximumNumberOfDays", c => c.Short(nullable: false));
            AddColumn("dbo.Settings", "DefaultNumberOfDays", c => c.Short(nullable: false));
            DropColumn("dbo.Settings", "NumberOfDays");
        }
    }
}
