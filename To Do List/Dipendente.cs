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
    [Table("Dipendente")]
    [Index(nameof(Username), IsUnique = true)]
    public class Dipendente
    {
        [Key]
        public string Username { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cognome { get; set; }

        [Required]
        public string NumeroTelefono { get; set; }

        [Required]
        public string Email { get; set; }

        public bool Admin { get; set; }

        public List<Compito>? ListaCompitiAssegnati { get; set; }




        public override string ToString()
        {

            return $"{Username} - {Nome} {Cognome} - {Email} - {NumeroTelefono} Admin: {Admin}";
        }

        public static void StampaListaCompiti(List<Compito> listacompiti)
        {
            foreach (Compito compito in listacompiti)
            {
                Console.WriteLine(compito);
            }
        }
    }
}
