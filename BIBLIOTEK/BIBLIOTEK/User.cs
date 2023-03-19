using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTEK
{
    public class User
    {
        public string username;
        public string personalnumber;
        public string password;
        public string accountType;

        public User(string username, string personalnumber, string password, string accountType)
        {
            this.username = username;
            this.personalnumber = personalnumber;
            this.password = password;
            this.accountType = accountType;
        }
       public User Create(string username, string personalNumber, string password, string AccountType)
        {
            
            int delay = 1500;
            

            string[] personalNumbersFromDb = System.IO.File.ReadAllLines("users.txt");

            ///// Kolla så att användaren inte finns och registrerar ny användare till textfil 
            bool userExists = false;
            foreach (string line in personalNumbersFromDb)
            {
                string[] values = line.Split(' ');
                string usernameFromFile = values[0];
                string personalNumberFromFile = values[1];

                if (username == usernameFromFile || personalNumber == personalNumberFromFile)
                {
                    Console.WriteLine("Användare finns redan!");
                    Thread.Sleep(delay);
                    Console.Clear();
                    userExists = true;
                    break;
                }
            }

            if (userExists == false)
            {
                var line = $"{username} {personalNumber} {password} {accountType} ";
                string[] lines = { line };

                File.AppendAllLines("users.txt", lines);

                Console.WriteLine("Välkommen " + username + ", ditt konto är registrerat!");
                Thread.Sleep(delay);
                Console.Clear();
            }
            User createdUser = new User(username, personalnumber, password, accountType);

            return createdUser;
        }
        public User deleteUser(string username, string personalNumber, string password, string AccountType)
        {
            int delay = 1500;
            string usernameFromFile = "null";
            string filePath = "users.txt"; 
            List<string> information = System.IO.File.ReadAllLines(filePath).ToList();
            int i = 0;
            int lineToEdit = 0; //Index för att spara vilken linje som den valda användarens information ligger på 
            string borrowedBooks = "";
            foreach (string line in information) //Hittar användare och sparar vilken index för linjen som ska redigeras. 
            {
                string[] values = line.Split(' ');
                usernameFromFile = values[0];
                if (username == usernameFromFile)
                {
                    lineToEdit = i;
                    if (values.Length > 4)
                    {
                        borrowedBooks = values[4];
                    }  
                }
                i++;
            }
            Console.WriteLine("+------------------------------+----------------------+------------------+-------------+---------------+");
            Console.WriteLine("|        Användarnamn          |    Personnummer      |     Lösenord     |  Konto typ  | Lånade böcker |");
            Console.WriteLine("+------------------------------+----------------------+------------------+-------------+---------------+");

            string formattedUsername = string.Format("{0,-30}", username);
            string formattedPersonal = string.Format("{0,-22}", personalNumber);
            Console.WriteLine("|{0}|{1,-22}|{2,-18}|{3,-13}|{4, -15}|", formattedUsername, formattedPersonal, password, AccountType, borrowedBooks);
            Console.WriteLine("+------------------------------+----------------------+------------------+-------------+---------------+");
            Console.WriteLine("Vill du radera den här användaren? Det är permanent!");
            Console.WriteLine("1. Ja, jag vill permanent radera detta konto");
            Console.WriteLine("2. Nej jag vill inte radera detta konto");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                information.RemoveAt(lineToEdit);
                File.WriteAllLines("users.txt", information);
                Console.WriteLine("Användaren har raderats.");

            }
            Thread.Sleep(delay);
            Console.Clear();
            User updatedUser = new User(username, personalNumber, password, AccountType);
            return updatedUser;
        }
       

        public void ChangeInformation(string username, string personalNumber, string password, string accountType, string currentUserType)
        {
            bool userHasReservedBook = false;
            string[] booksFromDb = File.ReadAllLines("Books.txt");

            foreach (string line in booksFromDb) //går igenom alla böcker och söker efter böcker som har reserverats
            {
                string[] values = line.Split('-');
                string titleFromFile = values[0];
                string authorFromFile = values[1];
                string genreFromFile = values[2];
                string isbnFromFile = values[3];
                int amountFromFile = int.Parse(values[4]);
                string usersQued = "";
                if (values.Length > 4) //Om boken är reserverad av någon är längeden >4
                {
                    for (int t = 5; t < values.Length; t++)
                    {
                        if (values[t] == username)
                        {
                            userHasReservedBook = true;
                        }
                    }
                }
            }


                if (userHasReservedBook == true)
            {
                Console.WriteLine("Du kan inte redigera ditt konto när du har reserverat en bok! Kom tillbaka när du har fått den reserverade boken!");
                Console.WriteLine("Vänligen logga in igen");
            }

            if (userHasReservedBook == false) //Om användaren inte har reserverat en bok får de redigera sin informaiton 
            {
                string[] information = System.IO.File.ReadAllLines("users.txt");
                int i = 0;
                int lineToEdit = 0; //Index för att spara vilken linje som den valda användarens information ligger på 
                string newAccountType = "member";
                string updatedBooks = "";


                foreach (string line in information) //Hittar användare och sparar vilken index för linjen som ska redigeras. 
                {
                    string[] values = line.Split(' ');
                    string usernameFromFile = values[0];
                    string personalFromFile = values[1];
                    string passwordFromFile = values[2];
                    string userTypeFromFile = values[3];
                    string borrowedBooks = "";
                    if (values.Length > 4)
                    {
                        borrowedBooks = values[4];
                    }


                    if (personalNumber == personalFromFile || username == usernameFromFile)
                    {
                        lineToEdit = i;
                        updatedBooks = borrowedBooks;
                        Console.WriteLine("+------------------------------+----------------------+------------------+-------------+---------------+");
                        Console.WriteLine("|        Användarnamn          |    Personnummer      |     Lösenord     |  Konto typ  | Lånade böcker |");
                        Console.WriteLine("+------------------------------+----------------------+------------------+-------------+---------------+");
                        string formattedUsername = string.Format("{0,-30}", usernameFromFile);
                        string formattedPersonal = string.Format("{0,-22}", personalFromFile);
                        Console.WriteLine("|{0}|{1,-22}|{2,-18}|{3,-13}|{4,-15}|", formattedUsername, formattedPersonal, passwordFromFile, userTypeFromFile, borrowedBooks);
                        Console.WriteLine("+------------------------------+----------------------+------------------+-------------+---------------+");
                    }
                    i++;
                }
                ////////


                Console.WriteLine("Ange nytt användarnamn:");
                string newUsername = Console.ReadLine();
                Console.WriteLine("Ange nytt personnummer:");
                string newPersonalnumber = Console.ReadLine();
                Console.WriteLine("Ange nytt lösenord:");
                string newPassword = Console.ReadLine();

                if (currentUserType == "admin")
                {
                    Console.WriteLine("Ange vilket ny klass på konto");
                    Console.WriteLine("1. Admin");
                    Console.WriteLine("2. Member");
                    string accountTypeChoice = Console.ReadLine();
                    if (accountTypeChoice == "1")
                    {
                        newAccountType = "admin";
                    }

                }

                information[lineToEdit] = newUsername + " " + newPersonalnumber + " " + newPassword + " " + newAccountType + " " + updatedBooks;

                File.WriteAllLines("users.txt", information);
                Console.WriteLine("Din information är uppdaterad, vänligen logga in igen");



            }

        }


     }
}
