namespace NAUB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedBorrowType : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO BorrowTypes(Id,Name) VALUES(1,'Staff')");
            Sql("INSERT INTO BorrowTypes(Id,Name) VALUES(2,'Student')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM BorrowTypes WHERE Id IN (1,2)");
        }
    }
}
