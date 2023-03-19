using System;
using System.Text;

namespace BIBLIOTEK
{
    internal class Program
    {
        static LibrarySystem system = LibrarySystem.GetInstance();

        static void Main(string[] args)
        {
            int delay = 1500;
            bool run = true;
            Console.OutputEncoding = Encoding.UTF8;
            string booksTXT;
            if (File.Exists("books.txt"))
            { booksTXT = "books.txt"; }





            Console.WriteLine("Välkommen till Bibloteket!");
            Thread.Sleep(delay);
            Console.Clear();

            while (run == true)
            {
                string userType = "null";
                string username = "null";
                string password = "null";
                string personalNumber = "null";
                string currentUserType = "null";
                string currentUser = "null";

                //Meny 
                Console.Clear();
                Console.WriteLine("1. Registrera ett nytt konto");
                Console.WriteLine("2. Logga in");
                Console.WriteLine("3. Se information för lärare och förslag på förbättringar");
                Console.WriteLine("\nX. Stäng av");

                Console.Write("Välj: ");
                string choice = Console.ReadLine();
                

                if (choice == "1")     //Registrera konto
                {

                    Console.Clear();
                    Console.WriteLine("För att skapa ett konto måste du ange ett användarnamn, ditt personnummer och ett lösenord");
                    Console.WriteLine("OBS! Använd inte mellanrum eller - i någon av din information, detta kommer göra ditt konto oanvändbart!!! ");
                    Console.Write("Användarnamn: ");
                    username = Console.ReadLine();

                    Console.Write("Personnummer: ");
                    personalNumber = Console.ReadLine();

                    Console.Write("Lösenord: ");
                    password = Console.ReadLine();
                    string AccountType = "member";

                    if (username.Length > 0 && personalNumber.Length > 0 && password.Length > 0 && username.Length < 30 && personalNumber.Length < 22 && password.Length < 18) 
                    {
                        User user = new User(username, personalNumber, password, AccountType);
                        user.Create(username, personalNumber, password, AccountType);
                    }
                    else 
                    {
                        if (username.Length <= 0 || personalNumber.Length <= 0 || password.Length <= 0)
                        {
                            Console.WriteLine("Du måste fylla i all information!");
                            Thread.Sleep(delay);
                            Console.Clear();
                        }
                        if (username.Length > 30 || personalNumber.Length > 22 || password.Length > 18)
                        {
                            Console.WriteLine("Ditt användarnamn får max vara 30 bokstäver, ditt personnummer max 22 och ditt lösenord max 18!");
                            Thread.Sleep(delay*3);
                            Console.Clear();
                        }
                    }
                          
                    
                    

                }
                if (choice == "2") //LOGGA IN
                {
                    bool userLoggedIn = false;

                    Console.WriteLine("Vill du logga in med användarnamn eller personnummer?");
                    Console.WriteLine("1 - Användarnamn");
                    Console.WriteLine("2 - Personnummer");
                    string LogInChoice = Console.ReadLine();
                    if (LogInChoice == "1") //Logga in med användarnamn
                    {
                        Console.WriteLine("Ange Användarnamn:");
                        username = Console.ReadLine();

                        Console.WriteLine("Ange Lösenord:");
                        password = Console.ReadLine();

                        string[] personalNumbersFromDb = System.IO.File.ReadAllLines("users.txt");

                        foreach (string line in personalNumbersFromDb)
                        {
                            string[] values = line.Split(' ');
                            string usernameFromFile = values[0];
                            string numberFromFile = values[1];
                            string passwordFromFile = values[2];
                            string accountType = values[3];


                            if (username == usernameFromFile && password == passwordFromFile)
                            {
                                currentUser = usernameFromFile;
                                if (accountType == "member")
                                {
                                    userType = "member";
                                    Console.WriteLine("Välkommen " + usernameFromFile + "!");
                                    Thread.Sleep(delay);
                                    Console.Clear();
                                    userLoggedIn = true;

                                }
                                if (accountType == "admin")
                                {
                                    userType = "admin";
                                    Console.WriteLine("Välkommen " + usernameFromFile + "! (bibliotikarie)");
                                    Thread.Sleep(delay);
                                    Console.Clear();
                                    userLoggedIn = true;
                                }
                                personalNumber = values[1];

                                //Animation
                                Console.Clear();
                                Console.WriteLine("     ,   ,\r\n    /////|\r\n   ///// |\r\n  |~~~|  |\r\n  |===|  |\r\n  |   |  |\r\n  |   |  |\r\n  |   | /\r\n  |===|/\r\n  '---'\r\n");
                                Thread.Sleep(delay / 2);
                                Console.Clear();
                                Console.WriteLine(",         ,\r\n|\\\\\\\\ ////|\r\n| \\\\\\V/// |\r\n|  |~~~|  |\r\n|  |===|  |\r\n|  |   |  |\r\n|  |   |  |\r\n \\ |   | /\r\n  \\|===|/\r\n   '---'");
                                Thread.Sleep(delay);
                                Console.Clear();


                            }
                        }
                        if (userLoggedIn == false)
                        {
                            Console.WriteLine("Användare och/eller lösenord hittades inte.");
                            Thread.Sleep(delay);
                            Console.Clear();
                        }

                       
                    }

                    if (LogInChoice == "2") //Logga in med personnummer
                    {
                        Console.WriteLine("Ange Personnummer:");
                        personalNumber = Console.ReadLine();

                        Console.WriteLine("Ange Lösenord:");
                        password = Console.ReadLine();

                        string[] personalNumbersFromDb = System.IO.File.ReadAllLines("users.txt");

                        foreach (string line in personalNumbersFromDb)
                        {
                            string[] values = line.Split(' ');
                            string usernameFromFile = values[0];
                            string numberFromFile = values[1];
                            string passwordFromFile = values[2];
                            string accountType = values[3];


                            if (personalNumber == numberFromFile && password == passwordFromFile)
                            {
                                currentUser = usernameFromFile;
                                if (accountType == "member")
                                {
                                    userType = "member";
                                    Console.WriteLine("Välkommen " + usernameFromFile + "!");
                                    Thread.Sleep(delay);
                                    Console.Clear();

                                    userLoggedIn = true;

                                }
                                if (accountType == "admin")
                                {
                                    userType = "admin";
                                    Console.WriteLine("Välkommen " + usernameFromFile + "! (bibliotikarie)");
                                    Thread.Sleep(delay);
                                    Console.Clear();

                                    userLoggedIn = true;
                                }
                                username = values[0];
                                //Animation
                                Console.Clear();
                                Console.WriteLine("     ,   ,\r\n    /////|\r\n   ///// |\r\n  |~~~|  |\r\n  |===|  |\r\n  |   |  |\r\n  |   |  |\r\n  |   | /\r\n  |===|/\r\n  '---'\r\n");
                                Thread.Sleep(delay / 2);
                                Console.Clear();
                                Console.WriteLine(",         ,\r\n|\\\\\\\\ ////|\r\n| \\\\\\V/// |\r\n|  |~~~|  |\r\n|  |===|  |\r\n|  |   |  |\r\n|  |   |  |\r\n \\ |   | /\r\n  \\|===|/\r\n   '---'");
                                Thread.Sleep(delay);
                                Console.Clear();


                            }
                        }
                        if (userLoggedIn == false)
                        {
                            Console.WriteLine("Användare och/eller lösenord hittades inte.");
                            Thread.Sleep(delay);
                            Console.Clear();
                        }


                    }
                    
                    //Inloggad meny, en meny för member och en meny admin och en meny för låta admin välja meny
                    while (userLoggedIn == true)
                    {
                        string adminChoocie = "null";
                        if (userType == "admin")
                        {
                            Console.WriteLine("Välkommen bibliotikarie!");
                            Console.WriteLine("1. Admin meny");
                            Console.WriteLine("2. Biblioteks meny");
                            Console.WriteLine("\nX. Logga ut");
                            adminChoocie = Console.ReadLine();
                            Console.Clear();
                        }
                        //Admin meny
                        while (adminChoocie == "1")
                        {
                            Console.Clear();
                            Console.WriteLine("1. Lista alla böcker");
                            Console.WriteLine("2. Sök efter bok");
                            Console.WriteLine("3. Lista alla användare");
                            Console.WriteLine("4. Redigera informationen på någons konto");
                            Console.WriteLine("5. Ta bort användare");
                            Console.WriteLine("6. Lägg till en ny användare");
                            Console.WriteLine("7. Radera en bok");
                            Console.WriteLine("8. Redigera en bok");
                            Console.WriteLine("9. Lägg till en ny bok");
                            Console.WriteLine("\nX. Gå tillbaka");

                            string AdminMenuChoice =Console.ReadLine();

                            if (AdminMenuChoice == "1") // Lista alla böcker
                            {
                                Console.Clear();
                               
                                
                                List<string> lines = File.ReadAllLines("Books.txt").ToList();
                                system.listBooks(lines);
                                Console.WriteLine("Klicka valfriknapp för att gå tillbaka");
                                Console.ReadLine();
                                Console.Clear();
                            }
                            if (AdminMenuChoice == "2") // Sök efter en bok
                            {
                                Console.WriteLine("Du kan söka efter title, författare, genre och ISBN");
                                Console.WriteLine("SÖK:");
                                string searchQuery = Console.ReadLine();
                                Console.Clear();
                                system.SearchBooks(searchQuery);
                                Console.WriteLine("Klicka valfriknapp för att gå tillbaka");
                                Console.ReadLine();
                                Console.Clear();
                            }

                            if (AdminMenuChoice == "3") // Lista alla användare
                            {
                                Console.Clear();
                                system.ListUsers();
                                Console.WriteLine("Klicka valfriknapp för att gå tillbaka");
                                Console.ReadLine();
                                Console.Clear();
                            }
                            if (AdminMenuChoice == "4") // Redigera informationen på ett konto
                            {
                                string selectedPersonalNumber = "null";
                                string selectedPassword = "null";
                                string selectedUserType = "null";
                                Console.Clear();
                                Console.WriteLine("Ange användarnamnet på kontot du vill ändra:");
                                
                                string selectedUsername = Console.ReadLine();

                                string[] personalNumbersFromDb = System.IO.File.ReadAllLines("users.txt");

                                foreach (string line in personalNumbersFromDb)
                                {
                                    string[] values = line.Split(' ');
                                    string usernameFromFile = values[0];
                                    string numberFromFile = values[1];
                                    string passwordFromFile = values[2];
                                    string accountType = values[3];


                                    if (selectedUsername == usernameFromFile)
                                    {
                                        selectedPersonalNumber = numberFromFile;
                                        selectedPassword = passwordFromFile;
                                        selectedUserType = accountType;
                                    }
                                }

                                if (selectedPersonalNumber != "null" && selectedPassword != "null" && selectedUserType != "null")
                                {
                                    currentUserType = userType;
                                    User user = new User(selectedUsername, selectedPersonalNumber, selectedPassword, selectedUserType);
                                    user.ChangeInformation(selectedUsername, selectedPersonalNumber, selectedPassword, selectedUserType, currentUserType);
                                    Console.WriteLine("Användarens information är uppdaterad!");
                                    Thread.Sleep(delay * 2);
                                }
                                else
                                {
                                    Console.WriteLine("Användaren hittades inte!");
                                }
                                
                                Console.Clear();
                            }
                            if (AdminMenuChoice == "5")// Ta bort ett konto 
                            {
                                bool userFound = false;
                                string selectedPersonalNumber = "null";
                                string selectedPassword = "null";
                                string selectedUserType = "null";
                                Console.Clear();
                                Console.WriteLine("Ange användarnamnet på kontot du vill ta bort:");

                                string selectedUsername = Console.ReadLine();

                                string[] personalNumbersFromDb = System.IO.File.ReadAllLines("users.txt");

                                foreach (string line in personalNumbersFromDb)
                                {
                                    string[] values = line.Split(' ');
                                    string usernameFromFile = values[0];
                                    string numberFromFile = values[1];
                                    string passwordFromFile = values[2];
                                    string accountType = values[3];


                                    if (selectedUsername == usernameFromFile)
                                    {
                                        userFound = true; 
                                        selectedPersonalNumber = numberFromFile;
                                        selectedPassword = passwordFromFile;
                                        selectedUserType = accountType;
                                        if (selectedUsername == currentUser)
                                        {
                                            Console.WriteLine("Du kan inte radera ditt egna konto!");
                                            Thread.Sleep(delay);
                                            Console.Clear();
                                        }
                                        else
                                        {
                                            User user = new User(selectedUsername, selectedPersonalNumber, selectedPassword, selectedUserType);
                                            user.deleteUser(selectedUsername, selectedPersonalNumber, selectedPassword, selectedUserType);
                                        }
                                    }
                                    
                                }
                                if (userFound == false)
                                {
                                    Console.WriteLine("Användaren kunde inte hittas");
                                    Thread.Sleep(delay);
                                    Console.Clear();
                                }
                                
                            }
                            if (AdminMenuChoice == "6") //Skapa nytt konto 
                            {
                                string AccountType = "member";

                                Console.Clear();
                                Console.WriteLine(" Ange ett användarnamn, ett personnummer, ett lösenord och vilket typ av konto som användaren ska ha");
                                Console.WriteLine("OBS! Använd inte mellanrum eller - i någon av din information, detta kommer göra användarens konto oanvändbart!!! ");
                                Console.Write("Användarnamn: ");
                                username = Console.ReadLine();

                                Console.Write("Personnummer: ");
                                personalNumber = Console.ReadLine();

                                Console.Write("Lösenord: ");
                                password = Console.ReadLine();
                                Console.WriteLine("Ange klass på konto");
                                Console.WriteLine("1. Admin");
                                Console.WriteLine("2. Member");
                                string accountTypeChoice = Console.ReadLine();
                                if (accountTypeChoice == "1")
                                {
                                    AccountType = "admin";
                                }

                                if (username.Length > 0 && personalNumber.Length > 0 && password.Length > 0 && username.Length < 30 && personalNumber.Length < 22 && password.Length < 18)
                                {
                                    User user = new User(username, personalNumber, password, AccountType);
                                    user.Create(username, personalNumber, password, AccountType);
                                }
                                else
                                {
                                    if (username.Length <= 0 || personalNumber.Length <= 0 || password.Length <= 0)
                                    {
                                        Console.WriteLine("Du måste fylla i all information!");
                                        Thread.Sleep(delay);
                                        Console.Clear();
                                    }
                                    if (username.Length > 30 || personalNumber.Length > 22 || password.Length > 18)
                                    {
                                        Console.WriteLine("Användarnamnet får max vara 30 bokstäver, personnummeret max 22 och lösenordet max 18!");
                                        Thread.Sleep(delay * 3);
                                        Console.Clear();
                                    }
                                }
                            }
                            if (AdminMenuChoice == "7")//Radera en bok
                            {
                                string title = "null";
                                string author = "null";
                                string genre = "null";
                                string isbn = "null";
                                int amount = 0;
                                Book book = new Book(title, author, genre, isbn, amount);
                                Console.WriteLine("Ange ISBN för den bok du vill radera");
                                string bookToDelete = Console.ReadLine();
                                book.deleteBook(bookToDelete);

                            }
                            if (AdminMenuChoice == "8")//Redigera en bok
                            {
                                Console.WriteLine("Om du vill redigera en bok måste du ange ny title, författare, genre och mängd kopior (Den nya informationen kan vara samma som den gamla). OBS! Du kan inte redigera en boks ISBN");
                                Console.WriteLine("Ange ISBN på boken du vill redigera");
                                string bookToEdit = Console.ReadLine();
                                string title = "null";
                                string author = "null";
                                string genre = "null";
                                string isbn = "null";
                                int amount = 0;
                                Book book = new Book(title, author, genre, isbn, amount);
                                book.editBook(bookToEdit);
                            }
                            if (AdminMenuChoice == "9")//Lägg till en bok
                            {
                                string title = "null";
                                string author = "null";
                                string genre = "null";
                                string isbn = "null";
                                int amount = 0;
                                Book book = new Book(title, author, genre, isbn, amount);
                                book.addBook();
                            }
                            if (AdminMenuChoice == "X" || AdminMenuChoice == "x")
                            {
                                Console.Clear();
                                adminChoocie = "null"; 
                            }

                        }
                        //Biblioteks meny
                        while (userType == "member" || adminChoocie == "2")
                        {
                            Console.WriteLine("1. Lista alla böcker");
                            Console.WriteLine("2. Sök efter bok");
                            Console.WriteLine("3. Låna en bok");
                            Console.WriteLine("4. Lämmna tillbaka en bok");
                            Console.WriteLine("5. Redigera information om ditt konto");
                            Console.WriteLine("6. Lista alla mina lånade böcker");

                         
                            Console.WriteLine("\nX. Logga ut");


                            string LibraryInChoice = Console.ReadLine();

                            if (LibraryInChoice == "1") //Lista alla böcker
                            {
                                Console.Clear();

                                List<string> lines = File.ReadAllLines("Books.txt").ToList();
                                system.listBooks(lines);
                                Console.WriteLine("Klicka valfriknapp för att gå tillbaka");
                                Console.ReadLine();
                                Console.Clear();
                            }
                            if (LibraryInChoice == "2") //Sök böcker
                            {
                                Console.WriteLine("Du kan söka efter title, författare, genre och ISBN");
                                Console.WriteLine("SÖK:");
                                string searchQuery = Console.ReadLine();
                                Console.Clear();
                                system.SearchBooks(searchQuery);
                                Console.WriteLine("Klicka valfriknapp för att gå tillbaka");
                                Console.ReadLine();
                                Console.Clear();
                            }
                            if (LibraryInChoice == "3") //Låna en bok
                            {
                                string title = "null";
                                string author = "null";
                                string genre = "null";
                                string isbn = "null";
                                int amount = 0;
                                Book book = new Book(title, author, genre, isbn, amount);
                                Console.WriteLine("För att låna en bok måste du ange bokens ISBN, vill du gå tillbaka och lista/söka efter böcker eller ange bokens ISBN?");
                                Console.WriteLine("1. Gå tillbaka");
                                Console.WriteLine("2. Ange bok");
                                string lendChoice = Console.ReadLine();
                                
                                if (lendChoice == "2")
                                {
                                    Console.WriteLine("Ange ISBN på boken du vill låna");
                                    string requestedBook = Console.ReadLine();
                                    book.lendBook(currentUser, requestedBook);
                                }
                                Console.Clear();
                            }
                            
                            if (LibraryInChoice == "4") // Lämmna tillbaka en bok 
                            {
                                Console.Clear();
                                string title = "null";
                                string author = "null";
                                string genre = "null";
                                string isbn = "null";
                                int amount = 0;
                                Book book = new Book(title, author, genre, isbn, amount);
                                book.returnBook(currentUser);
                                Console.WriteLine("Boken har lämnats tillbaka!");
                                Thread.Sleep(delay);
                                Console.Clear();
                            }
                            if (LibraryInChoice == "5") //Redigera information om ditt konto 
                            {
                                Console.Clear();
                                Console.WriteLine("Vill du ändra ditt användarnamn, personnummer och lösenord? Du måste logga in igen om du redgierar din information!");
                                Console.WriteLine("1. Ja, jag vill redigera min information");
                                Console.WriteLine("2. Nej, jag vill inte redigera min information");
                                string changeChoice = Console.ReadLine();
                                if (changeChoice == "1")
                                {
                                    currentUserType = userType;
                                    User user = new User(username, personalNumber, password, userType);
                                    user.ChangeInformation(currentUser, personalNumber, password, userType, currentUserType); //SKICKAR MED INFORMATION OM SISTA ANVÄNDAREN INTE CURRENT ANVÄNDARE
                                    Thread.Sleep(delay*2);
                                    userLoggedIn = false;
                                    break;
                                }
                                Console.Clear();
                            }
                            if (LibraryInChoice == "6") //Lista alla användarens lånade böcker
                            {
                                Console.Clear();
                                string title = "null";
                                string author = "null";
                                string genre = "null";
                                string isbn = "null";
                                int amount = 0;
                                Book book = new Book(title, author, genre, isbn, amount);
                                book.listUsersBooks(currentUser);
                                Console.WriteLine("Klicka valfri knapp för att gå tillbaka");
                                Console.ReadLine();
                                Console.Clear();
                            }
                            if (LibraryInChoice == "X" || LibraryInChoice == "x") // Logga ut
                            {
                                if (userType == "member")
                                {
                                    Console.WriteLine("Tack för ditt besök på biblioteket!");
                                    Thread.Sleep(delay);    //Animation av bok som stängs för det är kul :)
                                    Console.Clear();
                                    Console.WriteLine(",         ,\r\n|\\\\\\\\ ////|\r\n| \\\\\\V/// |\r\n|  |~~~|  |\r\n|  |===|  |\r\n|  |   |  |\r\n|  |   |  |\r\n \\ |   | /\r\n  \\|===|/\r\n   '---'");
                                    Thread.Sleep(delay / 2);
                                    Console.Clear();
                                    Console.WriteLine("     ,   ,\r\n    /////|\r\n   ///// |\r\n  |~~~|  |\r\n  |===|  |\r\n  |   |  |\r\n  |   |  |\r\n  |   | /\r\n  |===|/\r\n  '---'\r\n");
                                    Thread.Sleep(delay);
                                    Console.Clear();
                                   
                                    userLoggedIn = false;
                                    break;
                                }
                                if (userType == "admin")
                                {
                                    Console.Clear();
                                    adminChoocie = "null";
                                }

                            }
                            
                        }
                        if (adminChoocie == "x" || adminChoocie == "X")
                        {
                            Console.WriteLine("Tack för ditt besök på biblioteket!");
                            Thread.Sleep(delay);    //Animation av bok som stängs för det är kul :)
                            Console.Clear();
                            Console.WriteLine(",         ,\r\n|\\\\\\\\ ////|\r\n| \\\\\\V/// |\r\n|  |~~~|  |\r\n|  |===|  |\r\n|  |   |  |\r\n|  |   |  |\r\n \\ |   | /\r\n  \\|===|/\r\n   '---'");
                            Thread.Sleep(delay / 2);
                            Console.Clear();
                            Console.WriteLine("     ,   ,\r\n    /////|\r\n   ///// |\r\n  |~~~|  |\r\n  |===|  |\r\n  |   |  |\r\n  |   |  |\r\n  |   | /\r\n  |===|/\r\n  '---'\r\n");
                            Thread.Sleep(delay);
                            Console.Clear();
                            userLoggedIn = false; 
                        }

                    }
                }
                if (choice == "3")
                {
                    string[] lines = File.ReadAllLines("improvments.txt", Encoding.GetEncoding("iso-8859-1"));
                    foreach (string line in lines)
                    {
                        Console.WriteLine(line);
                    }
                    Console.WriteLine("klicka valfri knapp för att gå tillbaka");
                    Console.ReadLine();
                    Console.Clear();

                }
                if (choice == "x" || choice == "X") 
                {
                    break;
                }
            }
            
        }
    }
}