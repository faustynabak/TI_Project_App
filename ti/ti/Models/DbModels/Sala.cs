using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ti.Models.DbModels
{
    /// <summary>
    /// Tworze typy dla sali
    /// </summary>
    public enum EnumTypSali { komputerowa, cwiczeniowa, wykladowa }
    
    /// <summary>
    /// Klasa publiczna Sala
    /// </summary>
    public class Sala
    {   
        /// <summary>
         /// Id sali wraz z metodami akcesorowymi
         /// </summary>
        public int SalaId { get; set; }

        /// <summary>
        /// Id budynku  z metodami akcesorowymi
        /// </summary>
        [Required]
        [Display(Name = "Nazwa budynku")]
        public int? BudynekId { get; set; }

        /// <summary>
        /// Budynek
        /// </summary>
        public virtual Budynek Budynek { get; set; }

        /// <summary>
        /// Lista wynajmujacych sale
        /// </summary>
        public virtual ICollection<SalaWynajmujacy> SalaWynajmujacyList { get; set; }

        /// <summary>
        /// Numer sali
        /// </summary>
        [Display(Name = "Numer sali")]
        public string Numer { get; set; }

        [Display(Name = "Ilość miejsc")]
        /// <summary>
        /// ilosc miejsc w sali
        /// </summary>
        public int IloscMiejsca { get; set; }
        /// <summary>
        /// typ sali enumerowany
        /// </summary>
        [Display(Name = "Typ sali")]
        public EnumTypSali TypSali { get; set; }
        
        /// <summary>
        /// konstruktor nieparametryczny
        /// </summary>
        public Sala()
        {
           
        }

        /// <summary>
        /// Kontruktor parametryczny
        /// </summary>
        /// <param name="budynekId">id budynku</param>
        /// <param name="SalaId">id sali</param>
        /// <param name="numer">numer sali</param>
        /// <param name="iloscMiejsca">ilosc miejsc w sali</param>
        /// <param name="typSali">typ sali enum</param>
        public Sala(int budynekId, int SalaId, string numer, int iloscMiejsca, EnumTypSali typSali) : this()
        {
            this.BudynekId = budynekId;
            this.SalaId = SalaId;
            this.IloscMiejsca = iloscMiejsca;
            this.TypSali = typSali;
            this.Numer = $"{budynekId}.{numer}";
        }


        /// <summary>
        /// Metoda to string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"numer: {Numer}, miejsca: {IloscMiejsca}, typ sali: {TypSali}";
        }


    }
}