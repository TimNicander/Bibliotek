using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BIBLIOTEK
{
    public class Book
    {
        int delay = 1500;
        static LibrarySystem system = LibrarySystem.GetInstance();

        public string title { get; set; }
        public string author { get; set; }
        public string genre { get; set; }
        public string isbn { get; set; }
        public int amount { get; set; }

        public Book(string title, string author, string genre, string isbn, int amount)
        {
            this.title = title;
            this.author = author;
            this.genre = genre;
            this.isbn = isbn;
            this.amount = amount;
        }
    


        public void lendBook(string User, string requestedBook)
        {
                string[] booksFromDb = File.ReadAllLines("Books.txt");
                int lineOfBook = -1;
                int i = 0;
                int newAmount = 0;
                List<string> results = new List<string>();

                foreach (string line in booksFromDb) //Hittar boken som användaren vill låna
                {
                    string[] values = line.Split('-');
                    string titleFromFile = values[0];
                    string authorFromFile = values[1];
                    string genreFromFile = values[2];
                    string isbnFromFile = values[3];
                    int amountFromFile = int.Parse(values[4]);

                    if (requestedBook == isbnFromFile)
                    {
                    string usersQued = ""; //string för användare som reserverat boken
                    if (values.Length > 4) //Om boken är reserverad av någon är längeden >4
                    {
                        for (int t = 5; t < values.Length; t++)
                        {
                            if (t == 5)
                            {
                                usersQued += values[t];
                            }
                            else
                            {
                                usersQued += "-" + values[t];
                            }

                        }
                    }
                        results.Add(line);
                        lineOfBook = i;
                        newAmount = amountFromFile - 1;
                    if (usersQued != "")
                    {
                        booksFromDb[lineOfBook] = titleFromFile + "-" + authorFromFile + "-" + genreFromFile + "-" + isbnFromFile + "-" + newAmount + "-" + usersQued;
                    }
                    else
                    {
                        booksFromDb[lineOfBook] = titleFromFile + "-" + authorFromFile + "-" + genreFromFile + "-" + isbnFromFile + "-" + newAmount + usersQued;
                    }
                    }
                    i++;
                }
                if (lineOfBook != -1) //Om boken hittades 
                {
                    system.listBooks(results);
                    Console.WriteLine("Vill du låna denna bok?");
                    Console.WriteLine("1. Ja");
                    Console.WriteLine("2. Nej");
                    string lendChoice = Console.ReadLine();
                    if (lendChoice == "1")
                    {
                        if (newAmount > -1) //Om båken är tillgänlig läggs den till hos användaren och boklistan uppdateras
                        {
                        
                            string[] information = System.IO.File.ReadAllLines("users.txt");
                            int o = 0; //Index för att spara vilken linje som den valda användarens information ligger på 


                            foreach (string line in information) //Hittar användare och sparar vilken index för linjen som ska redigeras och information om användaren. 
                            {
                                string[] values = line.Split(' ');
                                string usernameFromFile = values[0];


                                if (User == usernameFromFile)
                                {
                                    information[o] = information[o] + "-" + requestedBook; //Uppdaterar användarens information för att lägga till ISBN på lånad bok 

                                    File.WriteAllLines("users.txt", information);
                                    break;
                                }
                                o++;
                            }

                            //Uppdatera bokens information med den nya sparade informationen 
                            File.WriteAllLines("Books.txt", booksFromDb);
                            Console.WriteLine("Boken har lagts till på ditt konto!");
                            Thread.Sleep(delay);

                        }
                        else
                        {
                            Console.WriteLine("Boken är inte tillgänglig, ");
                            Console.WriteLine("Vill du reservera den? OBS! Du kan inte redigera information om ditt konto när du har reserverat en bok!");
                            Console.WriteLine("1. Ja");
                            Console.WriteLine("2. Nej");
                            string queChoice = Console.ReadLine();
                            if (queChoice == "1")
                            {
                                Console.WriteLine("Du har reserverat boken");
                                string status = "request";
                                reserveBook(User, requestedBook, status);
                            }
                        }
                        
                    }
                }
                else
                {
                    Console.WriteLine("Boken hittades inte");
                    Thread.Sleep(delay);
                    Console.Clear();
                }
          
        }
        public void listUsersBooks(string User)
        {
            string[] personalNumbersFromDb = System.IO.File.ReadAllLines("users.txt");
            string booksToList = "null"; //Böckerna som ska skrivas ut
            List<string> results = new List<string>();


            foreach (string line in personalNumbersFromDb) //Hitta användare och spara de böcker de har lånade på sitt konto
            {
                string[] values = line.Split(' ');
                string usernameFromFile = values[0];
            
                if (User == usernameFromFile)
                {
                    if (values.Length > 4)
                    {
                        booksToList = values[4]; //Sparar böckerna från användarens information
                    }
                }
            }
            if (booksToList != "null") 
            {
                string[] booksISBN = booksToList.Split('-');
                for (int t = 0; t < booksISBN.Length; t++ )//Går igenom alla användarens böcker och matchar den till information om böckerna 
                {
                    string[] information = System.IO.File.ReadAllLines("Books.txt");
                    foreach (string line in information) 
                    {
                        string[] values = line.Split('-');
                        string title = values[0];
                        string author = values[1];
                        string genre = values[2];
                        string isbn = values[3];
                    


                        if (isbn == booksISBN[t])
                        {
                            results.Add(line);
                        }
                    }
                }
                system.listBooks(results);




            }
            else
            {
                Console.WriteLine("Du har inga böcker lånade på ditt konto");
            }

        }
        public void returnBook(string User)
        {
            Console.WriteLine("Du har dessa böcker på ditt konto:");
            listUsersBooks(User);
            Console.WriteLine("Ange ISBN på den bok du vill lämmna tillbaka:");
            string bookToReturn = Console.ReadLine();
            bool userHasBook = false;

            string[] personalNumbersFromDb = System.IO.File.ReadAllLines("users.txt");
            string books = "null";
            foreach (string line in personalNumbersFromDb) //Hitta användare och spara de böcker de har lånade på sitt konto
            {
                string[] values = line.Split(' ');
                string usernameFromFile1 = values[0];

                if (User == usernameFromFile1)
                {
                    if (values.Length > 4)
                    {
                        books = values[4]; //Sparar böckerna från användarens information
                    }
                }
            }
            List<string> booksISBN = books.Split('-').ToList();  //Gå igenom användarens böcker och om den angivna boken matchar med användarens tas den bort ur listan för anvndarens böcker
            for (int i = 0; i <= booksISBN.Count; i++)
            {
                if (booksISBN[i] == bookToReturn)
                {
                    userHasBook = true;
                    booksISBN.RemoveAt(i); //Uppdatear listan för att ta bort den tillbaka lämnade boken
                    i = booksISBN.Count + 1;
                    
                }
            }

            string updatedBooks = string.Join("-", booksISBN);

            ////////////Uppdatera användarens information 
            string[] information = System.IO.File.ReadAllLines("users.txt");
            int lineToEdit = -1;

            string usernameFromFile = "null";
            string personalFromFile = "null";
            string passwordFromFile = "null";
            string userTypeFromFile = "null";
            for (int i = 0; i <= information.Length; i++) //Hittar användare och sparar dess nuvarande information. 
            {
                
                    string[] values = information[i].Split(' ');
                    usernameFromFile = values[0];
                    personalFromFile = values[1];
                    passwordFromFile = values[2];
                    userTypeFromFile = values[3];
                    if (User == usernameFromFile)
                    {
                        lineToEdit = i;
                        break;
                    }
                
                
            }
            if (lineToEdit > -1)
            {
                information[lineToEdit] = usernameFromFile + " " + personalFromFile + " " + passwordFromFile + " " + userTypeFromFile + " " + updatedBooks;

                File.WriteAllLines("users.txt", information);
            }
            ///////Uppdatera bokens information
            string[] booksFromDb = File.ReadAllLines("Books.txt");
            int lineOfBook = -1;
            int o = 0;
            int newAmount = 0;
            List<string> results = new List<string>();
            foreach (string line in booksFromDb) //Hittar boken som användaren vill lämmna tillbaka
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
                    for (int i = 5; i < values.Length; i++)
                    {
                        if (i == 5)
                        {
                            usersQued += values[i];
                        }
                        else
                        {
                            usersQued += "-" + values[i]; 
                        }

                    }
                }

                if (bookToReturn == isbnFromFile && userHasBook == true)
                {
                    results.Add(line);
                    lineOfBook = o;
                    newAmount = amountFromFile + 1;
                    if (usersQued != "")
                    {
                        booksFromDb[lineOfBook] = titleFromFile + "-" + authorFromFile + "-" + genreFromFile + "-" + isbnFromFile + "-" + newAmount + "-" + usersQued;
                    }
                    else
                    {
                        booksFromDb[lineOfBook] = titleFromFile + "-" + authorFromFile + "-" + genreFromFile + "-" + isbnFromFile + "-" + newAmount + usersQued;
                    }
                }
                o++;
            }
            File.WriteAllLines("Books.txt", booksFromDb);
            if (userHasBook == true)
            {
                string status = "return";
                reserveBook(User, bookToReturn, status);

            }


        }
        public void reserveBook(string user, string bookISBN, string status)
        {
            if (status == "return") /////////////////En bok har retunerats, koden kollar om boken är reserverad av någon.
            {
                string[] booksFromDb = File.ReadAllLines("Books.txt");

                List<string> results = new List<string>();
                int lineOfBook = -1;
                int i = 0;
                foreach (string line in booksFromDb) //Hitta boken som har retunerats
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
                        for (int t = 6; t < values.Length; t++) //Skippar den första användaren i kön eftersom att den ska tas bort
                        {
                            if (t == 6)
                            {
                                usersQued += values[t];
                            }
                            else
                            {
                                usersQued += "-" + values[t];
                            }

                        }
                    }
                    if (bookISBN == isbnFromFile)
                    {
                        lineOfBook = i;
                        if (values.Length > 5) //Om någon har reserverat boken 
                        {
                            string reservedUser = values[5];
                            
                            string[] information = System.IO.File.ReadAllLines("users.txt");
                            int o = 0; //Index för att spara vilken linje som den reserverade användarens information ligger på 

                            bool userFound = false; 
                            foreach (string lineUser in information) //Hittar användare och sparar vilken index för linjen som ska redigeras och information om användaren. 
                            {
                                string[] valuesUser = lineUser.Split(' ');
                                string usernameFromFile = valuesUser[0];


                                if (reservedUser == usernameFromFile)
                                {
                                    information[o] = information[o] + "-" + bookISBN; //Uppdaterar användarens information för att lägga till ISBN på lånad bok 
                                    userFound = true;
                                    File.WriteAllLines("users.txt", information);
                                    break;
                                }
                                o++;
                            }
                            if (userFound == true)
                            {
                                int newAmount = amountFromFile - 1;
                                if (usersQued != "")
                                {
                                    booksFromDb[lineOfBook] = titleFromFile + "-" + authorFromFile + "-" + genreFromFile + "-" + isbnFromFile + "-" + newAmount + "-" + usersQued;
                                }
                                else
                                {
                                    booksFromDb[lineOfBook] = titleFromFile + "-" + authorFromFile + "-" + genreFromFile + "-" + isbnFromFile + "-" + newAmount + usersQued;
                                }


                                //Uppdatera bokens information med den nya sparade informationen 
                                File.WriteAllLines("Books.txt", booksFromDb);

                            }
                        }    
                    }
                    i++;
                }
            }
            if (status == "request") ///////////////Någon vill reservera boken 
            {
                string[] booksFromDb = File.ReadAllLines("Books.txt");
                int i = 0;
                foreach (string line in booksFromDb) //Hitta boken som vill reserveras
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
                            if (t == 5)
                            {
                                usersQued += values[t] + "-" + user;
                            }
                            else
                            {
                                usersQued += "-" + values[t];
                            }

                        }
                    }
                    if (bookISBN == isbnFromFile)
                    {
                        usersQued += user;
                        booksFromDb[i] = titleFromFile + "-" + authorFromFile + "-" + genreFromFile + "-" + isbnFromFile + "-" + amountFromFile + "-" + usersQued;
          
                        //Uppdatera bokens information med den nya sparade informationen 
                        File.WriteAllLines("Books.txt", booksFromDb);
                        Thread.Sleep(delay);
                    }
                    i++;
                }
            }
        }
        public void editBook(string bookISBN)
        {
            string[] information = System.IO.File.ReadAllLines("Books.txt");
            int i = 0;
            int lineToEdit = 0; //Index för att spara vilken linje som den valda användarens information ligger på 
            List<string> results = new List<string>();
            string usersQued = "";
            bool bookFound = false;
            foreach (string line in information) //Går igenom alla böcker och sparar index för vart boken som ska redigeras ligger
            {
                string[] values = line.Split('-');
                string isbnFromFile = values[3];
               
                if (bookISBN == isbnFromFile)
                {
                    if (values.Length > 4) //Om boken är reserverad av någon är längeden >4
                    {
                        for (int t = 5; t < values.Length; t++)
                        {
                            if (t == 5)
                            {
                                usersQued += values[t];
                            }
                            else
                            {
                                usersQued += "-" + values[t];
                            }

                        }
                    }
                    bookFound = true;
                    lineToEdit = i;
                    results.Add(line);

                }
                i++;
            }
            if (bookFound == true)
            {
                system.listBooks(results);
                Console.WriteLine("OBS! Använd inte - i någon del av bokens information!");
                Console.WriteLine("Ange den nya titlen på boken");
                string title = Console.ReadLine();
                Console.WriteLine("Ange den nya författaren av boken");
                string author = Console.ReadLine();
                Console.WriteLine("Ange den nya genre för boken");
                string genre = Console.ReadLine();
                Console.WriteLine("Ange hur många kopior det ska finnas av boken, skriv i siffror!");
                string amount = Console.ReadLine();

                if (usersQued != "")
                {
                    information[lineToEdit] = title + "-" + author + "-" + genre + "-" + bookISBN + "-" + amount + "-" + usersQued;
                }
                else
                {
                    information[lineToEdit] = title + "-" + author + "-" + genre + "-" + bookISBN + "-" + amount;
                }
                File.WriteAllLines("Books.txt", information);
                Console.WriteLine("Bokens information är uppdaterad!");
            }
           if (bookFound == false)
            {
                Console.WriteLine("Boken hittades inte");
            }
            Thread.Sleep(delay);
            Console.Clear();
        }
        public void deleteBook(string bookISBN)
        {
            int delay = 1500;
            string filePath = "Books.txt";
            List<string> information = System.IO.File.ReadAllLines(filePath).ToList();
            int i = 0;
            int lineToEdit = 0; //Index för att spara vilken linje som den valda bokens information ligger på 
            string borrowedBooks = "";
            string title = "null";
            string author = "null";
            string genre = "null";
            string isbn = "null";
            string amount = "null";
            List<string> results = new List<string>();
            bool bookFound = false;
            foreach (string line in information) //Hittar boken  och sparar vilken index för linjen som ska redigeras. 
            {
                string[] values = line.Split('-');
                string isbnFromFile = values[3];
                if (bookISBN == isbnFromFile)
                {
                    lineToEdit = i;
                    results.Add(line);
                    bookFound = true;
                }
                i++;
            }
            if (bookFound == true)
            {
                system.listBooks(results);

                Console.WriteLine("Vill du radera den här användaren? Det är permanent!");
                Console.WriteLine("1. Ja, jag vill permanent radera denna bok");
                Console.WriteLine("2. Nej jag vill inte radera denna bok");
                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    information.RemoveAt(lineToEdit);
                    File.WriteAllLines("Books.txt", information);
                    Console.WriteLine("Boken har raderats.");

                }
            }
            if (bookFound == false)
            {
                Console.WriteLine("Boken hittades inte");
            }
           
            Thread.Sleep(delay);
            Console.Clear();
        }
        public void addBook()
        {
            int delay = 1500;
            Console.WriteLine("OBS! Använd inte - i någon del av bokens information!");
            Console.WriteLine("Ange title på den nya boken");
            string title = Console.ReadLine();
            Console.WriteLine("Ange författaren av den nya boken");
            string author = Console.ReadLine();
            Console.WriteLine("Ange genre för den nya boken");
            string genre = Console.ReadLine();
            Console.WriteLine("Ange ISBN för den nya boken");
            string isbn = Console.ReadLine();
            Console.WriteLine("Ange hur många kopior det ska finnas av den nya boken, skriv i siffror!");
            string amount = Console.ReadLine();

            string[] information = System.IO.File.ReadAllLines("Books.txt");

            ///// Kolla så att boken inte finns 
            bool bookExists = false;
            foreach (string line in information)
            {
                string[] values = line.Split('-');
                string titleFromFile = values[0];
                string authorFromFile = values[1];
                string genreFromFile = values[2];
                string isbnFromFile = values[3];
                int amountFromFile = int.Parse(values[4]);
                string usersQued = "";

                if (isbn == isbnFromFile || title == titleFromFile)
                {
                    Console.WriteLine("Boken finns redan!");
                    Thread.Sleep(delay);
                    Console.Clear();
                    bookExists = true;
                    break;
                }
            }

            if (bookExists == false)
            {
                var line = $"{title}-{author}-{genre}-{isbn}-{amount}";
                string[] lines = { line };

                File.AppendAllLines("Books.txt", lines);
                Console.WriteLine("Den nya boken har registrerats!");
                Thread.Sleep(delay);
                Console.Clear();
            }
        }
    }
}

