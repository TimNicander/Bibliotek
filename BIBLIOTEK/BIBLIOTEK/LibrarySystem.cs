using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using static System.Reflection.Metadata.BlobBuilder;

namespace BIBLIOTEK
{
    public class LibrarySystem
    {
        private static LibrarySystem instance = null;
        private static string BooksFilePath = "C:\\Users\tim.nicander\\Desktop\\programmering 2\\BIBLIOTEK\\BIBLIOTEK\\Books.txt";
        private User loggedInUser;

        

        public static LibrarySystem GetInstance()
        {
            if (instance == null)
            {
                instance = new LibrarySystem();
            }
            return instance;
        }

       
        public void listBooks(List<string> books) // Lista böcker utifrån given lista
        {
            string[] booksFromDb = books.ToArray();

            Console.WriteLine("+------------------------------+----------------------+---------------+-------------+------------------+");
            Console.WriteLine("|           Title              |      Författare      |     Genre     |    ISBN     |Antal tillgängliga|");
            Console.WriteLine("+------------------------------+----------------------+---------------+-------------+------------------+");

            foreach (string line in booksFromDb)
            {
                string[] values = line.Split('-');
                string titleFromFile = values[0];
                string authorFromFile = values[1];
                string genreFromFile = values[2];
                string isbnFromFile = values[3];
                string statusFromFile = values[4];

                string titlePart1 = "null";
                string titlePart2 = "null";
                string genrePart1 = "null";
                string genrePart2 = "null";


                if (titleFromFile.Length >= 30) //Splitta upp titlen i två delar om den är för lång för en rad
                {

                    titlePart1 = titleFromFile.Substring(0, 30);

                    titlePart2 = titleFromFile.Substring(30);

                }
                if (genreFromFile.Length >= 15) //Splitta upp genre i två delar om den är för lång för en rad
                {
                    genrePart1 = genreFromFile.Substring(0, 15);

                    genrePart2 = genreFromFile.Substring(15);
                }

                if (titlePart2 == "null" && genrePart2 == "null") //Varken genre eller title är för långt 
                {   //Justera plasering av text
                    string formattedTitle = string.Format("{0,-30}", titleFromFile);
                    string formattedAuthor = string.Format("{0,-22}", authorFromFile);
                    Console.WriteLine("|{0}|{1,-22}|{2,-15}|{3,-13}|{4,-18}|", formattedTitle, formattedAuthor, genreFromFile, isbnFromFile, statusFromFile);
                    Console.WriteLine("+------------------------------+----------------------+---------------+-------------+------------------+");

                }
                if (titlePart2 != "null" || genrePart2 != "null")
                {

                    if (titlePart2 != "null" && genrePart2 == "null") // Enbart title är för lång
                    {   //Justera plasering av text
                        string formattedTitle1 = string.Format("{0,-30}", titlePart1);
                        string formattedTitle2 = string.Format("{0,-30}", titlePart2);
                        string formattedAuthor = string.Format("{0,-22}", authorFromFile);
                        Console.WriteLine("|{0}|{1,-22}|{2,-15}|{3,-13}|{4,-18}|", formattedTitle1, formattedAuthor, genreFromFile, isbnFromFile, statusFromFile);
                        Console.WriteLine("|{0}|{1,-22}|{2,-15}|{3,-13}|{4,-18}|", formattedTitle2, " ", " ", " ", " ");
                        Console.WriteLine("+------------------------------+----------------------+---------------+-------------+------------------+");
                    }
                    if (titlePart2 == "null" && genrePart2 != "null")  // Enbart genre är för lång
                    {   //Justera plasering av text
                        string formattedTitle = string.Format("{0,-30}", titleFromFile);
                        string formattedAuthor = string.Format("{0,-22}", authorFromFile);
                        Console.WriteLine("|{0}|{1,-22}|{2,-15}|{3,-13}|{4,-18}|", formattedTitle, formattedAuthor, genrePart1, isbnFromFile, statusFromFile);
                        Console.WriteLine("|{0}|{1,-22}|{2,-15}|{3,-13}|{4,-18}|", "                              ", " ", genrePart2, " ", " ");
                        Console.WriteLine("+------------------------------+----------------------+---------------+-------------+------------------+");
                    }
                    if (titlePart2 != "null" && genrePart2 != "null")  // Både genre och title är för lång
                    {   //Justera plasering av text
                        string formattedTitle1 = string.Format("{0,-30}", titlePart1);
                        string formattedTitle2 = string.Format("{0,-30}", titlePart2);
                        string formattedAuthor = string.Format("{0,-22}", authorFromFile);
                        Console.WriteLine("|{0}|{1,-22}|{2,-15}|{3,-13}|{4,-18}|", formattedTitle1, formattedAuthor, genrePart1, isbnFromFile, statusFromFile);
                        Console.WriteLine("|{0}|{1,-22}|{2,-15}|{3,-13}|{4,-18}|", formattedTitle2, " ", genrePart2, " ", " ");
                        Console.WriteLine("+------------------------------+----------------------+---------------+-------------+------------------+");
                    }
                }

            }

        }

        public void SearchBooks(string searchQuery)
        {
            string[] booksFromDb = File.ReadAllLines("Books.txt");

            List<string> results = new List<string>();

            foreach (string line in booksFromDb)
            {
                string[] values = line.Split('-');
                string titleFromFile = values[0];
                string authorFromFile = values[1];
                string genreFromFile = values[2];
                string isbnFromFile = values[3];

                if (titleFromFile.ToLower().Contains(searchQuery.ToLower()) || authorFromFile.ToLower().Contains(searchQuery.ToLower()) || genreFromFile.ToLower().Contains(searchQuery.ToLower()) || isbnFromFile.ToLower().Contains(searchQuery.ToLower()))
                {
                    results.Add(line);

                }
            }

            if (results.Count > 0)
            {
                Console.WriteLine($"Resultat efter: '{searchQuery}':");
                listBooks(results);

            }
            else
            {
                Console.WriteLine($"Inga resultat efter: '{searchQuery}'.");
            }


        }
        public void ListUsers()
        {
            string[] usersFromDb = System.IO.File.ReadAllLines("users.txt");

            Console.WriteLine("+------------------------------+----------------------+------------------+-------------+---------------+");
            Console.WriteLine("|        Användarnamn          |    Personnummer      |     Lösenord     |  Konto typ  | Lånade böcker |");
            Console.WriteLine("+------------------------------+----------------------+------------------+-------------+---------------+");
            string borrowedBooks = "";
            foreach (string line in usersFromDb)
            {
                string[] values = line.Split(' ');
                string usernameFromFile = values[0];
                string personalFromFile = values[1];
                string passwordFromFile = values[2];
                string userTypeFromFile = values[3];
                borrowedBooks = "";
                if (values.Length > 4)
                {
                    borrowedBooks = values[4];
                }

                string formattedUsername = string.Format("{0,-30}", usernameFromFile);
                string formattedPersonal = string.Format("{0,-22}", personalFromFile);
                Console.WriteLine("|{0}|{1,-22}|{2,-18}|{3,-13}|{4,-15}|", formattedUsername, formattedPersonal, passwordFromFile, userTypeFromFile, borrowedBooks);
                Console.WriteLine("+------------------------------+----------------------+------------------+-------------+---------------+");
            }

        }
    }
}
