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



string username = RaccoltaInputStringa("username");
bool access = Login(username);

if (access)
{
    Console.WriteLine("Puoi entrare");
}
else
{
    Console.WriteLine("Accesso Negato");
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