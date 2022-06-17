using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace ti.Models.DbModels
{
    /// <summary>
    /// Klasa publiczna SalaWynajmujacy
    /// </summary>
    public class SalaWynajmujacy
    {

        /// <summary>
        /// Id wynajmujecego i sali
        /// </summary>
        [Key, Column(Order = 1)]
        public int SalaWynajmujacyId { get; set; }

        /// <summary>
        /// Id sali
        /// </summary>
        [Key, Column(Order = 2)]
        [Display(Name = "Sala")]
        public int SalaId { get; set; }

        /// <summary>
        /// Id wynajmujacego
        /// </summary>
        [Key, Column(Order = 3)]
        [Display(Name = "Wynajmujący")]
        public int WynajmujacyId { get; set; }

        /// <summary>
        /// Wynajmujacy
        /// </summary>
        public Wynajmujacy Wynajmujacy{ get; set; }

        /// <summary>
        /// Sala
        /// </summary>
        public Sala Sala { get; set; }

        /// <summary>
        /// Data wynajecia
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataPoczatkowa { get; set; }

        /// <summary>
        /// Godzina rozpoczecia
        /// </summary>
        public TimeSpan GodzinaPoczatkowa { get; set; }

        /// <summary>
        /// Godzina zakonczenia
        /// </summary>
        public TimeSpan GodzinaKoncowa { get; set; }

        /// <summary>
        /// konstruktor nieparametryczny
        /// </summary>
        public SalaWynajmujacy() { }
        
        /// <summary>
        /// Konstruktor parametryczny
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="wynajmujacy">wynajmujacy</param>
        /// <param name="sala">sala</param>
        /// <param name="dataPoczatkowa">data</param>
        /// <param name="godzinaPoczatkowa">godzina rozpoczecia</param>
        /// <param name="godzinaKoncowa">godzina zakonczenia</param>
        public SalaWynajmujacy(int id, int wynajmujacyId, int salaId, DateTime dataPoczatkowa, TimeSpan godzinaPoczatkowa, TimeSpan godzinaKoncowa) :this()
        {
            WynajmujacyId = wynajmujacyId;
            SalaId = salaId;
            GodzinaPoczatkowa = godzinaPoczatkowa;
            GodzinaKoncowa = godzinaKoncowa;
            SalaWynajmujacyId = id;
            DataPoczatkowa = dataPoczatkowa;
        }

        /// <summary>
        /// Metoda tostring
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\nData: ").AppendLine(DataPoczatkowa.ToShortDateString());
            sb.Append("Od godziny: ").Append(GodzinaPoczatkowa.ToString(@"hh\:mm"));
            sb.Append(" do godziny: ").AppendLine(GodzinaKoncowa.ToString(@"hh\:mm"));
            return sb.ToString();
        }


    }
}