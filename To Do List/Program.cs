// See https://aka.ms/new-console-template for more information
using Azure.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using To_Do_List;


/*using (ToDoListContext db = new ToDoListContext())
{
    Dipendente Admin = new Dipendente() { 
        Nome = "Diego", 
        Cognome="Sforza", 
        Email="email", 
        NumeroTelefono="3349453423", 
        Username="DiegoSforza87" };

    db.Add(Admin);
    db.SaveChanges();

}*/

bool access;
string username;
bool flag = true;
do
{
    username = RaccoltaInputStringa("username");
    access = Login(username);
}
while (!access);


while (flag)
{

    Console.WriteLine(
        "- - - - - - - COMANDI - - - - - - -" +
        "\n1) Visualizza tutte le attività" +
        "\n2) Aggiungi un'attività" +
        "\n3) Rimuovere un'attività" +
        "\n4) Modificare un'attività" +
        "\n5) Modificare lo stato di un'attività" +
        "\n6) Prendi in carico un'attività" +
        "\n7) Visualizza le attività da svolgere" +
        "\n8) Visualizza le attività in scadenza" +
        "\n9) Visualizza le attività completate" +
        "\n*) Esci\n\n");

    string input = Console.ReadLine();

    switch (input)
    {
        case "1":
            ListaAttivita();
            Console.WriteLine("Vuoi filtrare le attività secondo qualche parametro? (s/n)\n\n");
            string siONo = Console.ReadLine();
            if (siONo.ToLower() == "s")
            {
                Console.WriteLine("\n1)Filtra per dipendente" +
                    "\n2)Filtra per categoria" +
                    "\n3)Filtra per cliente" +
                    "\n4)Filtra per data di scadenza" +
                    "\n*)Torna al menu precedente\n\n");
                string inputfiltro = Console.ReadLine();
                switch (inputfiltro)
                {
                    case "1":
                        Console.WriteLine("Inserisci l'username del dipedente per cui vuoi filtrare");
                        string usernameDaCercare = Console.ReadLine();
                        Dipendente.StampaListaCompiti(RicercaPerDipendente(usernameDaCercare));
                        break;
                    case "2":

                        break;
                    case "3":

                        break;
                    default:
                        break;

                }
            }

            break;
        case "2":
            Console.WriteLine("Aggiungi una nuova attività: ");
            AggiungiAttività();
            break;

        case "3":
            ListaAttivita();
            Console.WriteLine("Quale attività vuoi rimuovere?\nInserire il suo ID");
            int idDaRimuovere = int.Parse(Console.ReadLine()); // eccezione/metodo per prendere solo interi
            RimuoviAttivita(idDaRimuovere);
            break;

        case "4":
            ListaAttivita();
            Console.WriteLine("Inserisci l'ID dell'attività che vuoi modificare");
            int IdAttivitaDaModificare = int.Parse(Console.ReadLine());
            ModificaAttività(IdAttivitaDaModificare);
            break;

        case "5":
            ListaAttivita();
            Console.WriteLine("Inserisci l'ID dell'attività di cuoi vuoi modificare lo stato");
            int IdStatoDaModificare = int.Parse(Console.ReadLine());
            Modificastato(IdStatoDaModificare);
            break;

        case "6":
            ListaAttivitaDaSvolgere();
            Console.WriteLine("Inserisci l'ID dell'attività che vuoi prendere in carico");
            int IdAttivitaDaPrendereinCarico = int.Parse(Console.ReadLine());
            PrendiInCaricaAttivita(IdAttivitaDaPrendereinCarico);

            break;

        case "7":
            ListaAttivitaDaSvolgere();

            break;

        case "8":
            ListaAttivitaInScadenza();

            break;

        case "9":
            ListaAttivitaCompletate();

            break;

        default:
            Console.WriteLine("Buona Giornata!");
            flag = false;
            break;
    }


}


// FUNZIONI



bool Login(string username)
{
    using (ToDoListContext db = new ToDoListContext())
    {
        foreach (var dipendente in db.Dipendenti)
        {
            if (dipendente.Username == username)
            {
                return true;
            }
        }
        return false;

    }
}

string RaccoltaInputStringa(string nomeParametroDaRaccogliere)
{
    bool confirmFlag = false;
    string inputUtente = "";
    Console.WriteLine("Inserisci il tuo " + nomeParametroDaRaccogliere);
    do
    {
        inputUtente = Console.ReadLine();
        Console.WriteLine("Hai inserito " + inputUtente + " come " + nomeParametroDaRaccogliere + " è corretto? (s/n)");
        string siONo = Console.ReadLine();
        if (siONo.ToLower() == "s")
        {
            confirmFlag = true;
        }
        else
        {
            Console.WriteLine("\nInserisci nuovamente " + nomeParametroDaRaccogliere);
        }
    }
    while (!confirmFlag);
    return inputUtente;
}

void AggiungiDipendente()
{
    Dipendente dipendente = new Dipendente()
    {
        Nome = RaccoltaInputStringa("nome"),
        Cognome = RaccoltaInputStringa("cognome"),
        Email = RaccoltaInputStringa("email"),
        NumeroTelefono = RaccoltaInputStringa("numero di telefono"),
        Username = RaccoltaInputStringa("username")
    };
    using (ToDoListContext db = new ToDoListContext())
    {
        db.Add(dipendente);
        db.SaveChanges();
    }
}
void AggiungiAttività()
{
    Compito attivita = new Compito()
    {
        Categoria = RaccoltaInputStringa("categoria"),
        Descrizione = RaccoltaInputStringa("descrizione"),
        Scadenza = DateTime.Parse(RaccoltaInputStringa("scadenza")),
        Stato = false,
        Cliente = AggiungiClienteAAttività()
    };
    using (ToDoListContext db = new ToDoListContext())
    {
        db.Add(attivita);
        db.SaveChanges();
    }
}

void ModificaAttività(int ID)
{
    using (ToDoListContext db = new ToDoListContext())
    {
        foreach (Compito compito in db.Compiti)
        {
            if (ID == compito.CompitoID)
            {
                Console.WriteLine(compito);
                Console.WriteLine("\n\nCosa vuoi modificare?");
                Console.WriteLine("\n1) Categoria\n2) Descrizione\n3) Scadenza\n4) Stato");
                string modifica = Console.ReadLine();
                switch (modifica)
                {
                    case "1":
                        compito.Categoria = RaccoltaInputStringa("categoria");
                        break;
                    case "2":
                        compito.Descrizione = RaccoltaInputStringa("descrizione");
                        break;
                    case "3":
                        compito.Scadenza = DateTime.Parse(RaccoltaInputStringa("scadenza"));
                        break;
                    case "4":
                        compito.Stato = !compito.Stato;
                        break;

                }
                Console.WriteLine(compito);

            }
        }
        db.SaveChanges();
    }
}
void ListaAttivita()
{
    using (ToDoListContext db = new ToDoListContext())
    {
        foreach (Compito compito in db.Compiti)
        {
            Console.WriteLine(compito);
        }
    }
}

void ListaAttivitaDaSvolgere()
{
    using (ToDoListContext db = new ToDoListContext())
    {
        foreach (Compito compito in db.Compiti)
        {
            if (!compito.Stato)
            {
                Console.WriteLine(compito);
            }
        }
    }
}

void ListaAttivitaInScadenza()
{
    using (ToDoListContext db = new ToDoListContext())
    {
        foreach (Compito compito in db.Compiti)
        {
            if (DateTime.Today == compito.Scadenza.Date)
            {
                Console.WriteLine(compito);
            }
        }
    }
}

void ListaAttivitaCompletate()
{
    using (ToDoListContext db = new ToDoListContext())
    {
        foreach (Compito compito in db.Compiti)
        {
            if (compito.Stato)
            {
                Console.WriteLine(compito);
            }
        }
    }
}

void RimuoviAttivita(int ID)
{
    using (ToDoListContext db = new ToDoListContext())
    {
        foreach (Compito compito in db.Compiti)
            if (compito.CompitoID == ID)
            {
                db.Remove(compito);
            }
        db.SaveChanges();
    }
}

void PrendiInCaricaAttivita(int ID)
{
    using (ToDoListContext db = new ToDoListContext())
    {
        Dipendente dipendenteAttivo = null;
        foreach (Dipendente dipendente in db.Dipendenti)
        {
            if (username == dipendente.Username)
            {
                dipendenteAttivo = dipendente;
                break;
            }
        }

        foreach (Compito compito in db.Compiti)
        {
            if (ID == compito.CompitoID)
            {
                compito.ListaDipendenti.Add(dipendenteAttivo);
                break;
            }
            Console.WriteLine(compito);
        }
        db.SaveChanges();

    }
}



void Modificastato(int ID)
{
    using (ToDoListContext db = new ToDoListContext())
    {
        foreach (Compito compito in db.Compiti)
            if (compito.CompitoID == ID)
            {
                compito.Stato = !compito.Stato;
                Console.WriteLine(compito);
            }
        db.SaveChanges();
    }
}

Cliente AggiungiCliente()
{
    Cliente cliente = new Cliente()
    {
        Nome = RaccoltaInputStringa("Nome"),
        Cognome = RaccoltaInputStringa("Cognome"),
        Indirizzo = RaccoltaInputStringa("Indirizzo"),
        NumeroTelefono = RaccoltaInputStringa("Numero di telefono"),
        Email = RaccoltaInputStringa("Email")
    };
    return cliente;
}

Cliente AggiungiClienteAAttività()
{
    Console.WriteLine("Il cliente ha già commissionato qualcosa in passato? (s/n)");
    string yesOrNot = Console.ReadLine();
    if (yesOrNot.ToLower() == "s")
    {
        using (ToDoListContext db = new ToDoListContext())
        {
            Console.WriteLine("Inserisci la mail del cliente");
            string emailDaControllare = Console.ReadLine();
            foreach (Cliente cliente in db.Clienti)
            {
                if (emailDaControllare == cliente.Email)
                {
                    return cliente;
                }
            }
            Console.WriteLine("Cliente non trovato, aggiungere nuovo cliente");
            return AggiungiCliente();
        }
    }
    else
    {
        return AggiungiCliente();
    }
}

List<Compito> RicercaPerDipendente(string usernameUtente)
{
    using (ToDoListContext db = new ToDoListContext())
    {
        List<Compito> CompitixDipendente = (from d in db.Dipendenti
                                            where d.Username == usernameUtente
                                            select d.ListaCompitiAssegnati).FirstOrDefault();
        return CompitixDipendente;
    }

}