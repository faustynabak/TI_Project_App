namespace ti.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Budyneks",
                c => new
                    {
                        BudynekId = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(),
                    })
                .PrimaryKey(t => t.BudynekId);
            
            CreateTable(
                "dbo.Salas",
                c => new
                    {
                        SalaId = c.Int(nullable: false, identity: true),
                        BudynekId = c.Int(),
                        Numer = c.String(),
                        IloscMiejsca = c.Int(nullable: false),
                        TypSali = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SalaId)
                .ForeignKey("dbo.Budyneks", t => t.BudynekId)
                .Index(t => t.BudynekId);
            
            CreateTable(
                "dbo.SalaWynajmujacies",
                c => new
                    {
                        SalaId = c.Int(nullable: false),
                        WynajmujacyId = c.Int(nullable: false),
                        DataPoczatkowa = c.DateTime(nullable: false),
                        GodzinaPoczatkowa = c.Time(nullable: false, precision: 7),
                        GodzinaKoncowa = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => new { t.SalaId, t.WynajmujacyId })
                .ForeignKey("dbo.Salas", t => t.SalaId, cascadeDelete: true)
                .ForeignKey("dbo.Wynajmujacies", t => t.WynajmujacyId, cascadeDelete: true)
                .Index(t => t.SalaId)
                .Index(t => t.WynajmujacyId);
            
            CreateTable(
                "dbo.Wynajmujacies",
                c => new
                    {
                        WynajmujacyId = c.Int(nullable: false, identity: true),
                        Imie = c.String(),
                        Nazwisko = c.String(),
                        Wydzial = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WynajmujacyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SalaWynajmujacies", "WynajmujacyId", "dbo.Wynajmujacies");
            DropForeignKey("dbo.SalaWynajmujacies", "SalaId", "dbo.Salas");
            DropForeignKey("dbo.Salas", "BudynekId", "dbo.Budyneks");
            DropIndex("dbo.SalaWynajmujacies", new[] { "WynajmujacyId" });
            DropIndex("dbo.SalaWynajmujacies", new[] { "SalaId" });
            DropIndex("dbo.Salas", new[] { "BudynekId" });
            DropTable("dbo.Wynajmujacies");
            DropTable("dbo.SalaWynajmujacies");
            DropTable("dbo.Salas");
            DropTable("dbo.Budyneks");
        }
    }
}
