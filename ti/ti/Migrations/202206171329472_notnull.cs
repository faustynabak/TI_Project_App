namespace ti.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notnull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Salas", "BudynekId", "dbo.Budyneks");
            DropIndex("dbo.Salas", new[] { "BudynekId" });
            AlterColumn("dbo.Salas", "BudynekId", c => c.Int(nullable: false));
            CreateIndex("dbo.Salas", "BudynekId");
            AddForeignKey("dbo.Salas", "BudynekId", "dbo.Budyneks", "BudynekId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Salas", "BudynekId", "dbo.Budyneks");
            DropIndex("dbo.Salas", new[] { "BudynekId" });
            AlterColumn("dbo.Salas", "BudynekId", c => c.Int());
            CreateIndex("dbo.Salas", "BudynekId");
            AddForeignKey("dbo.Salas", "BudynekId", "dbo.Budyneks", "BudynekId");
        }
    }
}
