using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace ti.Models.DbModels
{   
    /// <summary>
    /// Klasa budynek
    /// </summary>
    public class Budynek
    {  
        /// <summary>
        /// id budynku wraz z metodami akcesorowymi
        /// </summary>
        public int BudynekId { get; set; }




        /// <summary>
        /// Nazwa budynku z metodami akcesorowymi
        /// </summary>
        [Display(Name = "Nazwa budynku")]
        public string Nazwa { get; set; }

        /// <summary>
        /// Lista sal z metodami akcesorowymi
        /// </summary>
        public virtual IList<Sala> Sale { get; set; }
        
        /// <summary>
        /// Konstruktor nieparametryczny
        /// </summary>
        public Budynek()
        {
            Sale = new List<Sala>();
        }

        /// <summary>
        /// Konstruktor parametryczny
        /// </summary>
        /// <param name="BudyekId">id budynku</param>
        /// <param name="nazwa">nazwa budynku</param>
        public Budynek(int BudyekId, string nazwa) : this()
        {
            this.BudynekId = BudyekId;
            this.Nazwa = nazwa;
        }

        /// <summary>
        /// Metoda tostring
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Nazwa);
            foreach (Sala s in Sale)
            {
                sb.Append(s.ToString());
            }
            return sb.ToString();
        }
    }
}