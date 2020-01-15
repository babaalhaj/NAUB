namespace NAUB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Isbn = c.String(nullable: false, maxLength: 50),
                        Title = c.String(nullable: false, maxLength: 50),
                        Author1 = c.String(nullable: false, maxLength: 50),
                        Author2 = c.String(maxLength: 50),
                        Author3 = c.String(maxLength: 50),
                        Publisher = c.String(nullable: false, maxLength: 50),
                        PublicationYear = c.Short(nullable: false),
                        Edition = c.String(nullable: false, maxLength: 50),
                        NumberInStock = c.Short(nullable: false),
                        ShelveNumber = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Books");
        }
    }
}
