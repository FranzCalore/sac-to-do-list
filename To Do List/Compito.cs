using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_List
{
    [Table("Compito")]
    [Index(nameof(Compito_Id), IsUnique = true)]
    public class Compito
    {
        [Key]
        public int Compito_Id { get; set; }

        [Required]
        public string Categoria { get; set; }

        [Required]
        public string Descrizione { get; set; }

        [Required]
        public DateTime Scadenza { get; set; }

        [Required]
        public bool Stato { get; set; }

        public Cliente Cliente { get; set; }

        public List<Dipendente> ListaDipendenti { get; set; }




    }
}
