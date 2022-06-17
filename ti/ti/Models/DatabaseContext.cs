using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ti.Models.DbModels;

namespace ti.Models
{
    /// <summary>
    /// Klasa publiczna databeseContext dziedzicząca po DbContext
    /// </summary>
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("TIConnectionString")
        {
           //Database.SetInitializer(new DBInitializer());
        }
        /// <summary>
        /// baza budynkow
        /// </summary>
        public DbSet<Budynek> Budynki { get; set; }
        /// <summary>
        /// baza sal
        /// </summary>
        public DbSet<Sala> Sale { get; set; }
        /// <summary>
        /// baza wynajetych przez wynajmujacych sal
        /// </summary>
        public DbSet<SalaWynajmujacy> SalaWynajmujacy { get; set; }
        /// <summary>
        ///  baza wynajmujacych
        /// </summary>
        public DbSet<Wynajmujacy> Wynajmujacy { get; set; }

        

    }

}