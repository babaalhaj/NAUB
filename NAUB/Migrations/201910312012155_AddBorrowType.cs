namespace NAUB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBorrowType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BorrowTypes",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BorrowTypes");
        }
    }
}
