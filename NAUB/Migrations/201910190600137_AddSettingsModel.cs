namespace NAUB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSettingsModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaximumNumberOfBooksPerBorrow = c.Short(nullable: false),
                        DefaultNumberOfDays = c.Short(nullable: false),
                        MaximumNumberOfDays = c.Short(nullable: false),
                        OverdueFee = c.String(nullable: false),
                        OverdueFeePerBook = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Settings");
        }
    }
}
