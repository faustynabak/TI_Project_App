namespace ti.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.SalaWynajmujacies");
            AddColumn("dbo.SalaWynajmujacies", "SalaWynajmujacyId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.SalaWynajmujacies", new[] { "SalaWynajmujacyId", "SalaId", "WynajmujacyId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.SalaWynajmujacies");
            DropColumn("dbo.SalaWynajmujacies", "SalaWynajmujacyId");
            AddPrimaryKey("dbo.SalaWynajmujacies", new[] { "SalaId", "WynajmujacyId", "DataPoczatkowa", "GodzinaPoczatkowa" });
        }
    }
}
