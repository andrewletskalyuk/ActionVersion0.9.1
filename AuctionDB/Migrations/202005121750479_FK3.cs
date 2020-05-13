namespace AuctionDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FK3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lots", "BuyerId", "dbo.Buyers");
            DropForeignKey("dbo.Lots", "SellerId", "dbo.Sellers");
            DropIndex("dbo.Lots", new[] { "SellerId" });
            DropIndex("dbo.Lots", new[] { "BuyerId" });
            AlterColumn("dbo.Lots", "IsSell", c => c.Boolean());
            AlterColumn("dbo.Lots", "SoldPrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Lots", "SellerId", c => c.Int());
            AlterColumn("dbo.Lots", "BuyerId", c => c.Int());
            CreateIndex("dbo.Lots", "SellerId");
            CreateIndex("dbo.Lots", "BuyerId");
            AddForeignKey("dbo.Lots", "BuyerId", "dbo.Buyers", "Id");
            AddForeignKey("dbo.Lots", "SellerId", "dbo.Sellers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lots", "SellerId", "dbo.Sellers");
            DropForeignKey("dbo.Lots", "BuyerId", "dbo.Buyers");
            DropIndex("dbo.Lots", new[] { "BuyerId" });
            DropIndex("dbo.Lots", new[] { "SellerId" });
            AlterColumn("dbo.Lots", "BuyerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Lots", "SellerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Lots", "SoldPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Lots", "IsSell", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Lots", "BuyerId");
            CreateIndex("dbo.Lots", "SellerId");
            AddForeignKey("dbo.Lots", "SellerId", "dbo.Sellers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Lots", "BuyerId", "dbo.Buyers", "Id", cascadeDelete: true);
        }
    }
}
