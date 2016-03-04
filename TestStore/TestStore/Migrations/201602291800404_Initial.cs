namespace TestStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartProducts",
                c => new
                    {
                        CartProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        ProductQuantity = c.Int(nullable: false),
                        SubPrice = c.Decimal(precision: 18, scale: 2),
                        CartId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CartProductId)
                .ForeignKey("dbo.Carts", t => t.CartId, cascadeDelete: true)
                .Index(t => t.CartId);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        CustomerFIO = c.String(nullable: false),
                        CustomerAddress = c.String(nullable: false),
                        CustomerEmail = c.String(),
                        CustomerPhone = c.String(nullable: false),
                        Date = c.DateTime(),
                        TotalPrice = c.Decimal(precision: 18, scale: 2),
                        CartLogin = c.String(),
                    })
                .PrimaryKey(t => t.CartId);
            
            CreateTable(
                "dbo.ProductNames",
                c => new
                    {
                        ProductNameId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StatCartId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductNameId)
                .ForeignKey("dbo.StatCarts", t => t.StatCartId, cascadeDelete: true)
                .Index(t => t.StatCartId);
            
            CreateTable(
                "dbo.StatCarts",
                c => new
                    {
                        StatCartId = c.Int(nullable: false, identity: true),
                        StatCustomerFIO = c.String(),
                        StatCustomerAddress = c.String(),
                        StatCustomerEmail = c.String(),
                        StatCustomerPhone = c.String(),
                        StatUserName = c.String(),
                        StatDate = c.DateTime(),
                        StatTotalPrice = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.StatCartId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductCategory = c.String(),
                        ProductName = c.String(),
                        ProductDescription = c.String(),
                        ProductQuantity = c.Int(nullable: false),
                        ProductPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImageType = c.String(),
                        ImageData = c.Binary(),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        Roles = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductNames", "StatCartId", "dbo.StatCarts");
            DropForeignKey("dbo.CartProducts", "CartId", "dbo.Carts");
            DropIndex("dbo.ProductNames", new[] { "StatCartId" });
            DropIndex("dbo.CartProducts", new[] { "CartId" });
            DropTable("dbo.Users");
            DropTable("dbo.Products");
            DropTable("dbo.StatCarts");
            DropTable("dbo.ProductNames");
            DropTable("dbo.Carts");
            DropTable("dbo.CartProducts");
        }
    }
}
