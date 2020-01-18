namespace NAUB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifiedBorrowsTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Borrows", "Isbn", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Borrows", "Isbn", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
