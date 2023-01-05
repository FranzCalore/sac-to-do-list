using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_List
{
    [Table("Cliente")]
    [Index(nameof(ClienteID), IsUnique = true)]
    public class Cliente
    {
        [Key]
        public int ClienteID { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cognome { get; set; }

        [Required]
        public string Indirizzo { get; set; }

        [Required]
        public string NumeroTelefono { get; set; }

        [Required]
        public string Email { get; set; }

        public List<Compito>? ListaCommissioni { get; set; }


    }
}