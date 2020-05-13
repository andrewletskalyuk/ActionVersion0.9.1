namespace AuctionDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buyers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
                        Cash = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Lots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsSell = c.Boolean(nullable: false),
                        StartPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SoldPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Photo = c.String(),
                        SellerId = c.Int(nullable: false),
                        Buyer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buyers", t => t.Buyer_Id)
                .ForeignKey("dbo.Sellers", t => t.SellerId, cascadeDelete: true)
                .Index(t => t.SellerId)
                .Index(t => t.Buyer_Id);
            
            CreateTable(
                "dbo.Sellers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lots", "SellerId", "dbo.Sellers");
            DropForeignKey("dbo.Lots", "Buyer_Id", "dbo.Buyers");
            DropIndex("dbo.Lots", new[] { "Buyer_Id" });
            DropIndex("dbo.Lots", new[] { "SellerId" });
            DropTable("dbo.Sellers");
            DropTable("dbo.Lots");
            DropTable("dbo.Buyers");
        }
    }
}
