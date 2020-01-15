namespace NAUB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBorrowModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Borrows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BorrowerType = c.String(nullable: false, maxLength: 10),
                        BorrowerId = c.String(nullable: false, maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 50),
                        Isbn = c.String(nullable: false, maxLength: 50),
                        BorrowDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Borrows");
        }
    }
}
