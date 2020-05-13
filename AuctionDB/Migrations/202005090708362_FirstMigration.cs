namespace AuctionDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lots", "Buyer_Id", "dbo.Buyers");
            DropIndex("dbo.Lots", new[] { "Buyer_Id" });
            RenameColumn(table: "dbo.Lots", name: "Buyer_Id", newName: "BuyerId");
            AlterColumn("dbo.Lots", "BuyerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Lots", "BuyerId");
            AddForeignKey("dbo.Lots", "BuyerId", "dbo.Buyers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lots", "BuyerId", "dbo.Buyers");
            DropIndex("dbo.Lots", new[] { "BuyerId" });
            AlterColumn("dbo.Lots", "BuyerId", c => c.Int());
            RenameColumn(table: "dbo.Lots", name: "BuyerId", newName: "Buyer_Id");
            CreateIndex("dbo.Lots", "Buyer_Id");
            AddForeignKey("dbo.Lots", "Buyer_Id", "dbo.Buyers", "Id");
        }
    }
}
