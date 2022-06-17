using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ti.Models.DbModels
{
    
    public enum EnumWydzial { WZ, WH, WIMIR, WINIP, SJO }
    /// <summary>
    /// Klasa wynajmujacy
    /// </summary>
    public class Wynajmujacy
    {
        /// <summary>
        /// Id wynajmujacego 
        /// </summary>
        public int WynajmujacyId { get; set; }
        /// <summary>
        /// Imie wynajmujacego
        /// </summary>
        public string Imie { get; set; }
        /// <summary>
        /// Nazwisko wynajmujacego
        /// </summary>
        public string Nazwisko { get; set; }
        /// <summary>
        /// Wydzial
        /// </summary>
        [Display(Name = "Wydział")]
        public EnumWydzial Wydzial { get; set; }
        /// <summary>
        /// Kolekcja wynajetych sal
        /// </summary>
        public virtual ICollection<SalaWynajmujacy> SalaWynajmujacyList { get; set; }

        /// <summary>
        /// Konstruktor nieparametryczny
        /// </summary>

        public Wynajmujacy()
        {
           
        }
        /// <summary>
        /// Konstruktor parametryczny
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="imie">imie</param>
        /// <param name="nazwisko">nazwisko</param>
        /// <param name="wydzial">wydzial</param>

        public Wynajmujacy(int id,string imie, string nazwisko, EnumWydzial wydzial)
        {
            this.Imie = imie;
            this.Nazwisko = nazwisko;
            this.Wydzial = wydzial;
        }
        /// <summary>
        /// Metoda tostring
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Imie} {Nazwisko}, {Wydzial}";
        }


    }
}