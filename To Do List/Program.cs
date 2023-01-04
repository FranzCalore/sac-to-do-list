// See https://aka.ms/new-console-template for more information
using Azure.Identity;
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
do
{
    string username = RaccoltaInputStringa("username");
    access = Login(username);
}
while (!access);

    Console.WriteLine(
        "- - - - - - - COMANDI - - - - - - -" +
        "\n1) Visualizza tutte le attività" +
        "\n2) Aggiungi un'attività" +
        "\n3) Rimuovere un'attività" +
        "\n4) Modificare un'attività" +
        "\n5) Modificare lo stato di un'attività" +
        "\n6) Modifica data attività" +
        "\n7) Visualizza le attività da svolgere" +
        "\n8) Visualizza le attività in scadenza" +
        "\n9) Visualizza le attività completate" +
        "\n*) Esci\n\n");

string input = Console.ReadLine();

switch (input)
{
    case "1":
        ListaAttivita();

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

        break;
    case "6":

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

        break;
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
    bool confirmFlag=false;
    string inputUtente = "";
    Console.WriteLine("Inserisci il tuo " + nomeParametroDaRaccogliere);
    do
    {
        inputUtente = Console.ReadLine();
        Console.WriteLine("Hai inserito " + inputUtente + " come " + nomeParametroDaRaccogliere + " è corretto? (s/n)");
        string siONo=Console.ReadLine();
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
    Compito attivita = new Compito() {
        Categoria = RaccoltaInputStringa("categoria"),
        Descrizione = RaccoltaInputStringa("descrizione"),
        Scadenza = DateTime.Parse(RaccoltaInputStringa("scadenza")),
        Stato = false
        // Aggiungere cliente
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
            if (ID == compito.Compito_Id)
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

            }
        }
        db.SaveChanges();
    }
}
void ListaAttivita()
    {
        using (ToDoListContext db = new ToDoListContext())
        {
            foreach(Compito compito in db.Compiti)
            {
                Console.WriteLine(compito);
            }
        }
    }

void ListaAttivitaDaSvolgere()
{
    using (ToDoListContext db = new ToDoListContext())
    {
        foreach(Compito compito in db.Compiti)
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
        if(compito.Compito_Id == ID)
        {
                db.Remove(compito);
        }
        db.SaveChanges();
    }
}