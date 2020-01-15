namespace NAUB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedSettingsModel : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Settings(MaximumNumberOfBooksPerBorrow,NumberOfDays,OverdueFeePerBook) VALUES(5,30,50)");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM Settings WHERE Id IN 1");
        }
    }
}
