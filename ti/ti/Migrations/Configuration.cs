namespace ti.Migrations
{
    using System.Data.Entity.Migrations;
    using ti.Models.DbModels;

    internal sealed class Configuration : DbMigrationsConfiguration<ti.Models.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ti.Models.DatabaseContext context)
        {
            Budynek budynek1 = new Budynek(1, "D14");
            Budynek budynek2 = new Budynek(2, "B01");

            Wynajmujacy wynajmujacy1 = new Wynajmujacy(1, "Andrzej", "Duda", EnumWydzial.WH);
            Wynajmujacy wynajmujacy2 = new Wynajmujacy(2, "Tomasz", "Wieroński", EnumWydzial.WZ);

            Sala sala1 = new Sala(1, 1, "12", 300, EnumTypSali.cwiczeniowa);
            Sala sala2 = new Sala(2, 2, "13", 30, EnumTypSali.cwiczeniowa);
            Sala sala3 = new Sala(2, 3, "16", 100, EnumTypSali.komputerowa);

            context.Budynki.AddOrUpdate(budynek1, budynek2);
            context.Wynajmujacy.AddOrUpdate(wynajmujacy1, wynajmujacy2);
            context.Sale.AddOrUpdate(sala1, sala2, sala3);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
