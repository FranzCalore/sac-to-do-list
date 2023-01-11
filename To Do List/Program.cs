// See https://aka.ms/new-console-template for more information
using Azure.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using To_Do_List;

bool access;
string username;
bool flag = true;
do
{   using(ToDoListContext db = new ToDoListContext())
    {
        if (!db.Dipendenti.Any())
        {
            Console.WriteLine("- - - - - - - - - BENVENUTO NEL SOFTWARE DI GESTIONE DELLE ATTIVITA' DELL'AZIENDA DEL DOTTOR SFORZA - - - - - - - - - -" +
                "\n\nGuida al primo accesso:" +
                "\nPresto verrai aggiunto al database dipendenti, ti verranno richieste le tue credenziali" +
                "\ne ti verranno conferiti i privilegi di amministrazione. Salva il tuo username, perché" +
                "\nti verrà chiesto per accedere. Attento a non rivelarlo!" +
                "\nBuona giornata!\n\n");
            AggiungiDipendente();
            foreach(Dipendente dipendente in db.Dipendenti)
            {
                dipendente.Admin = true;
            }
            db.SaveChanges();

        }
    }
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
    using (ToDoListContext db = new ToDoListContext())
    {
        Dipendente utenteAttivo = (from d in db.Dipendenti
                                   where d.Username == username
                                   select d).FirstOrDefault();
        if (utenteAttivo.Admin)
        {
            Console.WriteLine(
                "- - - - - - - COMANDI ADMIN - - - - - - -" +
                "\na) Aggiungi Dipendente" +
                "\nb) Rimuovi Dipendente" +
                "\nc) Modifica Dipendente" +
                "\nd) Aggiungi privilegi a utente");
        }

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
                            Console.WriteLine("Inserisci il nome della categoria per cui vuoi filtrare");
                            string categoriaDaCercare = Console.ReadLine();
                            Dipendente.StampaListaCompiti(RicercaPerCategoria(categoriaDaCercare));
                            break;
                        case "3":
                            Console.WriteLine("Inserisci la mail del cliente per cui vuoi filtrare");
                            string emailDaCercare = Console.ReadLine();
                            Dipendente.StampaListaCompiti(RicercaPerCliente(emailDaCercare));
                            break;
                        case "4":
                            Console.WriteLine("Inserisci la data per cui vuoi filtrare (dd/mm/yyyy)");
                            string dataDaCercare = Console.ReadLine();
                            Dipendente.StampaListaCompiti(RicercaPerData(dataDaCercare));
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

            case "a":
                if (utenteAttivo.Admin)
                {
                    AggiungiDipendente();
                }
                else
                {
                    flag = false;
                }
                break;
            case "b":
                if (utenteAttivo.Admin)
                {
                    string utenteDaEliminare = RaccoltaInputStringa("Dipendente da eliminare");
                    RimuoviDipendente(utenteDaEliminare);
                }
                else
                {
                    flag = false;
                }
                break;
            case "c":
                if (utenteAttivo.Admin)
                {
                    string utenteDaModificare = RaccoltaInputStringa("Dipendente da modificare");
                    ModificaDipendente(utenteDaModificare);
                }
                else
                {
                    flag = false;
                }
                break;
            case "d":
                if (utenteAttivo.Admin)
                {
                    string utenteAcuiCambiarePrivilegi = RaccoltaInputStringa("Dipendente a cui cambiare i privilegi da admin");
                    CambiaPrivilegi(utenteAcuiCambiarePrivilegi);
                }
                else
                {
                    flag = false;
                }
                break;

            default:
                Console.WriteLine("Buona Giornata!");
                flag = false;
                break;
        }


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
        Console.WriteLine("Username errato, prego provare di nuovo:\nSe il problema persiste, contattare un amministratore");
        return false;

    }
}

string RaccoltaInputStringa(string nomeParametroDaRaccogliere)
{
    bool confirmFlag = false;
    string inputUtente = "";
    Console.WriteLine("Inserisci " + nomeParametroDaRaccogliere);
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
        Nome = RaccoltaInputStringa("nome del dipendente"),
        Cognome = RaccoltaInputStringa("cognome del dipendente"),
        Email = RaccoltaInputStringa("email del dipendente"),
        NumeroTelefono = RaccoltaInputStringa("numero di telefono del dipendente"),
        Username = RaccoltaInputStringa("username del dipendente")
    };
    using (ToDoListContext db = new ToDoListContext())
    {
        db.Add(dipendente);
        db.SaveChanges();
        Console.WriteLine("Il tuo dipendente è stato aggiunto al database");
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
        Console.WriteLine("La tua attività è stata aggiunta al Database, buona giornata!");
    }
}

void ModificaAttività(int ID)
{
    using (ToDoListContext db = new ToDoListContext())
    {
        if (db.Compiti.Any())
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
                        default:
                            break;

                    }
                    Console.WriteLine("L'attività è stata correttamente modificata...Procedo alla stampa\n\n\n");
                    Console.WriteLine(compito);

                }
            }

            db.SaveChanges();
        }
    }
}
void ListaAttivita()
{
    using (ToDoListContext db = new ToDoListContext())
    {
        if (db.Compiti.Any())
        {
            foreach (Compito compito in db.Compiti)
            {
                Console.WriteLine(compito);
            }
        }
    }
}

void ListaAttivitaDaSvolgere()
{
    using (ToDoListContext db = new ToDoListContext())
    {
        if (db.Compiti.Any())
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
}

void ListaAttivitaInScadenza()
{
    using (ToDoListContext db = new ToDoListContext())
    {
        if (db.Compiti.Any())
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
}

void ListaAttivitaCompletate()
{
    using (ToDoListContext db = new ToDoListContext())
    {
        if (db.Compiti.Any())
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
}

void RimuoviAttivita(int ID)
{
    using (ToDoListContext db = new ToDoListContext())
    {
        if (db.Compiti.Any())
        {
            foreach (Compito compito in db.Compiti)
                if (compito.CompitoID == ID)
                {
                    db.Remove(compito);
                    Console.WriteLine($"L'attività {compito.Categoria} è rimossa con successo!");
                }
            db.SaveChanges();
        }
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
                if (compito.ListaDipendenti == null)
                {
                    compito.ListaDipendenti = new List<Dipendente>();
                }
                compito.ListaDipendenti.Add(dipendenteAttivo);                
                Console.WriteLine("Hai preso correttamente in carico l'attività richiesta. Buon Lavoro!\n");
                break;
            }
        }
        db.SaveChanges();
        foreach (Compito compito in db.Compiti)
        {
            if(ID == compito.CompitoID)
            {
                Console.WriteLine(compito);
                Console.WriteLine("\n\n");
            }
        }

    }
}



void Modificastato(int ID)
{
    using (ToDoListContext db = new ToDoListContext())
    {
        if (db.Compiti.Any())
        {
            foreach (Compito compito in db.Compiti)
                if (compito.CompitoID == ID)
                {
                    compito.Stato = !compito.Stato;
                    Console.WriteLine(compito);
                    Console.WriteLine("\n\n");
                }
            db.SaveChanges();
        }
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
            if (db.Clienti.Any())
            {
                foreach (Cliente cliente in db.Clienti)
                {
                    if (emailDaControllare == cliente.Email)
                    {
                        return cliente;
                    }
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
                                            where d.Username.ToLower() == usernameUtente.ToLower()
                                            select d.ListaCompitiAssegnati).FirstOrDefault();
        return CompitixDipendente;
    }

}

List<Compito> RicercaPerCategoria(string categoria)
{
    using (ToDoListContext db = new ToDoListContext())
    {
        List<Compito> CompitixCategoria = (from c in db.Compiti
                                           where c.Categoria.ToLower() == categoria.ToLower()
                                           select c).ToList();
        return CompitixCategoria;
    }

}

List<Compito> RicercaPerCliente(string email)
{
    using (ToDoListContext db = new ToDoListContext())
    {
        List<Compito> CompitixCliente = (from cl in db.Clienti
                                         where cl.Email.ToLower() == email.ToLower()
                                         select cl.ListaCommissioni).FirstOrDefault();
        return CompitixCliente;
    }

}

List<Compito> RicercaPerData(string data)
{
    using (ToDoListContext db = new ToDoListContext())
    {
        DateTime datadacercare = DateTime.Parse(data);
        List<Compito> CompitixDipendente = (from c in db.Compiti
                                            where c.Scadenza == datadacercare
                                            select c).ToList();
        return CompitixDipendente;
    }

}

void RimuoviDipendente(string UsernameUtentedaRimuovere)
{
    using (ToDoListContext db = new ToDoListContext())
    {
        if (UsernameUtentedaRimuovere != username)
        {
            foreach (Dipendente dipendente in db.Dipendenti)
                if (dipendente.Username == UsernameUtentedaRimuovere)
                {
                    db.Remove(dipendente);
                    Console.WriteLine($"Il dipendente {dipendente.Nome} {dipendente.Cognome} è stato rimosso correttamente!");
                }
            db.SaveChanges();
        }
        else
        {
            Console.WriteLine("Non puoi cancellare un utente attivo!");
        }
    }
}

void CambiaPrivilegi(string UsernamedaCambiare)
{
    using (ToDoListContext db = new ToDoListContext())
    {
        if (UsernamedaCambiare != username)
        {
            foreach (Dipendente dipendente in db.Dipendenti)
                if (dipendente.Username == UsernamedaCambiare)
                {
                    dipendente.Admin = !dipendente.Admin;
                    if (dipendente.Admin)
                    {
                        Console.WriteLine($"Ora il dipendente {dipendente.Nome} {dipendente.Cognome} ha privilegi da Admin.\n\n");
                    }
                    else
                    {
                        Console.WriteLine($"Ora il dipendente {dipendente.Nome} {dipendente.Cognome} non ha più privilegi da Admin.\n\n");
                    }
                    Console.WriteLine(dipendente);
                }
        }

        else
        {
            Console.WriteLine("ATTENZIONE! Non è possibile modificare i privilegi dell'utente attivo!");
        }
        db.SaveChanges();
    }
}

void ModificaDipendente(string Username)
{
    using (ToDoListContext db = new ToDoListContext())
    {
        foreach (Dipendente dipendente in db.Dipendenti)
        {
            if (Username == dipendente.Username)
            {
                Console.WriteLine(dipendente);
                Console.WriteLine("\n\nCosa vuoi modificare?");
                Console.WriteLine("\n1) Nome\n2) Cognome\n3) Email\n4) Numero di Telefono\n\n");
                string modifica = Console.ReadLine();
                switch (modifica)
                {
                    case "1":
                        dipendente.Nome = RaccoltaInputStringa("nome del dipendente");
                        break;
                    case "2":
                        dipendente.Cognome = RaccoltaInputStringa("cognome del dipendente");
                        break;
                    case "3":
                        dipendente.Email = RaccoltaInputStringa("email del dipendente");
                        break;
                    case "4":
                        dipendente.NumeroTelefono = RaccoltaInputStringa("numero di telefono del dipendente");
                        break;
                    default:
                        break;

                }
                Console.WriteLine("Utente modificato...\n\n" + dipendente);

            }
        }
        db.SaveChanges();
    }
}