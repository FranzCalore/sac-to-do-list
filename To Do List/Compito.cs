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
    [Index(nameof(CompitoID), IsUnique = true)]

    public class Compito
    {
        [Key]
        public int CompitoID { get; set; }

        [Required]
        public string Categoria { get; set; }

        [Required]
        public string Descrizione { get; set; }

        [Required]
        public DateTime Scadenza { get; set; }

        [Required]
        public bool Stato { get; set; }

        public int ClienteID { get; set; }

        public Cliente Cliente { get; set; }

        public List<Dipendente>? ListaDipendenti { get; set; }

        //METODO

        private string StatoStringa()
        {
            if (Stato)
            {
                return "Completato";
            }
            else
            {
                if (DateTime.Today == Scadenza.Date)
                {
                    return "DA COMPLETARE, IN SCADENZA!";
                }
                else if (DateTime.Now > Scadenza)
                {
                    return "Scaduto";
                }
                else
                {
                    return "Da Completare";
                }
            }
        }

        public override string ToString()
        {
            Cliente ClienteAttivo = null;
            using (ToDoListContext db = new ToDoListContext())
            {
                foreach (Cliente cliente in db.Clienti)
                {
                    if (cliente.ClienteID == this.ClienteID)
                    {
                        ClienteAttivo = cliente;
                    }
                }

                return "\n\nID: " + CompitoID
                    + "\nCategoria: " + Categoria
                    + "\nDescrizione: " + Descrizione
                    + "\nScadenza: " + Scadenza.ToString("dd/MM/yyyy")
                    + "\nStato: " + StatoStringa()
                    + "\nCliente: " + ClienteAttivo.Nome + " " + ClienteAttivo.Cognome
                    + "\nDipendenti in carica: " + StampaListaDipendenti()
                    + "\n";
            }
        }

        private string StampaListaDipendenti()
        {
            using ToDoListContext db = new ToDoListContext();

            List<Dipendente> Dipendenti = (from c in db.Compiti
                                           where c.CompitoID == CompitoID
                                           select c.ListaDipendenti).FirstOrDefault();
            string stringadipendenti = "";
            foreach (Dipendente dipendente in Dipendenti)
            {
                stringadipendenti += dipendente.ToString();
                stringadipendenti += "\n";
            }
            return stringadipendenti;
        }

    }
}
