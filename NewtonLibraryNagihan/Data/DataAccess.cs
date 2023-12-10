using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NewtonLibraryNagihan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;


namespace NewtonLibraryNagihan.Data
{
    internal class DataAccess
    {
        private readonly Context _context;

        public DataAccess(Context context)
        {
            _context = context;
        }
        public void Seed()
        {
            Context context = new Context();
            
            Author author1 = new Author();
            author1.firstName = "Charles";
            author1.lastName = "Dickens";

            Author author2 = new Author();
            author2.firstName = "William";
            author2.lastName = "Shakespeare";

            Author author3 = new Author();
            author3.firstName = "Jane";
            author3.lastName = " Austen";

            Author author4 = new Author();
            author4.firstName = "Newton";
            author4.lastName = " YH";


            Book Book1 = new Book();
            Book1.Title = "A Tale Of Two Cities";
            Book1.PublishedYear = 1859;
            Book1.ISBN = Guid.NewGuid().ToString();
            Book1.Grade = 3.5;

            Book Book2 = new Book();
            Book2.Title = "Romeo And Juliet";
            Book2.PublishedYear = 1597;
            Book2.ISBN = Guid.NewGuid().ToString();
            Book2.Grade = 4.5;

            Book Book3 = new Book();
            Book3.Title = "Sense and Sensibility";
            Book3.PublishedYear = 1811;
            Book3.ISBN = Guid.NewGuid().ToString(); 
            Book3.Grade = 2.4;

            Book Book4 = new Book();
            Book4.Title = "Test ";
            Book4.PublishedYear = 2023;
            Book4.ISBN = Guid.NewGuid().ToString();
            Book4.Grade = 3.4;


            Borrower borrower1 = new Borrower();
            borrower1.FirstName = "Nagihan";
            borrower1.LastName = "Cifoglu";
            borrower1.LoanCard = "123567890";
            borrower1.LoanCardPIN = "884422";
            DateTime? loanDate1 = DateTime.Now;
            borrower1.LoanDate = loanDate1;


            Borrower borrower2 = new Borrower();
            borrower2.FirstName = "Oliver";
            borrower2.LastName = "Thomas";
            borrower2.LoanCard = "098654321";
            borrower2.LoanCardPIN = "123123";
            DateTime? loanDate2 = DateTime.Now;
            borrower2.LoanDate = loanDate2;


            Borrower borrower3 = new Borrower();
            borrower3.FirstName = "John";
            borrower3.LastName = "Brown";
            borrower3.LoanCard = "572098263";
            borrower3.LoanCardPIN = "663300";
            DateTime? loanDate3 = DateTime.Now;
            borrower3.LoanDate = loanDate3;


            Book1.Authors.Add(author1);
            Book1.Authors.Add(author3);
            Book2.Authors.Add(author2);
            Book3.Authors.Add(author3);
            Book4.Authors.Add(author4);

            author1.Books.Add(Book1);
            author3.Books.Add(Book1);
            author2.Books.Add(Book2);
            author1.Books.Add(Book3);
            author4.Books.Add(Book4);

            borrower1.Books.Add(Book1);
            borrower2.Books.Add(Book2);
            borrower3.Books.Add(Book3);
            borrower3.Books.Add(Book4);
            Book1.IsAvailable = false;
            Book2.IsAvailable = false;
            Book3.IsAvailable = false;
            Book4.IsAvailable = false;


            context.Authors.AddRange(author1, author2, author3, author4);
            context.Books.AddRange(Book1, Book2, Book3, Book4);
            context.Borrowers.AddRange(borrower1, borrower2, borrower3);


            context.SaveChanges();

        }

        public void RunMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Hi! Welcome to the library. \nPlease select an option:\n");
                Console.WriteLine("1 - Add an author");
                Console.WriteLine("2 - Add a book");
                Console.WriteLine("3 - Add a borrower");
                Console.WriteLine("4 - Borrow a book");
                Console.WriteLine("5 - Return a book");
                Console.WriteLine("6 - Show logbook for a borrower.");
                Console.WriteLine("7 - Show logbook for a book");
                Console.WriteLine("8 - Remove All Data from the list including author, book and borrower)");
                Console.WriteLine("9 - Exit");
                Console.Write("\nEnter a number between 1-9: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddAnAuthor();
                        break;

                    case "2":
                        AddABook();
                        break;

                    case "3":
                        AddABorrower();
                        break;

                    case "4":
                        Console.Write("Enter Book ID to borrow: ");
                        if (int.TryParse(Console.ReadLine(), out int borrowBookId))
                        {
                            Console.Write("Enter Borrower ID: ");
                            if (int.TryParse(Console.ReadLine(), out int borrowBorrowerId))
                            {
                                BorrowABook(borrowBookId, borrowBorrowerId);
                            }
                            else
                            {
                                Console.WriteLine("Invalid Borrower ID.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Book ID.");
                        }
                        break;

                    case "5":
                        Console.Write("Enter Book ID to return: ");
                        if (int.TryParse(Console.ReadLine(), out int returnBookId))
                        {
                            Console.Write("Enter Borrower ID: ");
                            if (int.TryParse(Console.ReadLine(), out int returnBorrowerId))
                            {
                                ReturnABook(returnBookId, returnBorrowerId);
                            }
                            else
                            {
                                Console.WriteLine("Invalid Borrower ID.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Book ID.");
                        }
                        break;

                    case "6":
                        Console.Write("Enter Borrowerid to show logbook for a borrower: ");
                        string BorrowerId = Console.ReadLine();
                        int Borrowerid;
                        if(int.TryParse(BorrowerId,out Borrowerid))      
                        BorrowerLogBook(Borrowerid);

                        break;

                    case "7":
                        Console.Write("Enter Bookid to show logbook for a book: ");
                        string BookId = Console.ReadLine();
                        int Bookid;
                        if (int.TryParse(BookId, out Bookid))
                            BookLogBook(Bookid);
                        break;

                    case "8":
                        RemoveAllData();
                        break;

                    case "9":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid input. Please enter a number between 1 and 3.");
                        break;
                }

                Console.WriteLine();
            }
        }

        public void AddAnAuthor()
        {
            Console.WriteLine($"Write the name of the author:");
            string AuthorFirstName;
            string AuthorLastName;
            do
            {
                Console.Write("Write the first name of the author: ");
                AuthorFirstName = Console.ReadLine();

                Console.Write("Write the last name of the author: ");
                AuthorLastName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(AuthorFirstName))
                {
                    Console.WriteLine("You have to write the first name of the author!");
                }

                if (string.IsNullOrWhiteSpace(AuthorLastName))
                {
                    Console.WriteLine("You have to write the last name of the author!");
                }

            } while (string.IsNullOrWhiteSpace(AuthorFirstName) || string.IsNullOrWhiteSpace(AuthorLastName));

            var author = new Author
            {

                firstName = AuthorFirstName,
                lastName = AuthorLastName,
            };

            _context.Authors.Add(author);
            _context.SaveChanges();
            Console.WriteLine("You added an author!");
        }

        public void AddABook()
        {
            Console.WriteLine($"Write the name of the book:");
            string BookTitle;
            Random rnd = new Random();
            do
            {
                BookTitle = Console.ReadLine();
                if (BookTitle == null || BookTitle == "")
                {
                    Console.WriteLine("You have to write a title of the book!");
                }

            } while (BookTitle == null || BookTitle == "");

            var book = new Book
            {
                
                Title = BookTitle,
                ISBN = new Guid().ToString(),
                PublishedYear = rnd.Next(1999,2023),
                Grade = rnd.Next(1,6)
            };
            _context.Books.Add(book);
            _context.SaveChanges();
            Console.WriteLine("You added a book!");
        }




        public void AddABorrower()
        {
            Console.WriteLine($"Write the name of the author:");
            string BorrowerFirstName;
            string BorrowerLastName;
            do
            {
                Console.Write("Write the first name of the borrower: ");
                BorrowerFirstName = Console.ReadLine();

                Console.Write("Write the last name of the borrower: ");
                BorrowerLastName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(BorrowerFirstName))
                {
                    Console.WriteLine("You have to write the first name of the borrower!");
                }

                if (string.IsNullOrWhiteSpace(BorrowerLastName))
                {
                    Console.WriteLine("You have to write the last name of the borrower!");
                }

            } while (string.IsNullOrWhiteSpace(BorrowerFirstName) || string.IsNullOrWhiteSpace(BorrowerLastName));

            Random rnd = new Random();
            var borrower = new Borrower

            {
                FirstName = BorrowerFirstName,
                LastName = BorrowerLastName,
                LoanCard = rnd.Next(100000000, 999999999).ToString(),
                LoanCardPIN = rnd.Next(100000, 999999).ToString(),
                LoanDate = DateTime.Now

            };
            _context.Borrowers.Add(borrower);
            _context.SaveChanges();
            Console.WriteLine("You added a borrower!");
        }




        public void BorrowABook(int bookId, int borrowerId)
        {
            var book = _context.Books.Find(bookId);
            var borrower = _context.Borrowers.Find(borrowerId);

            if (book != null && borrower != null && book.IsAvailable)
            {
                book.Borrower = borrower;
                book.IsAvailable = false;

                borrower.Books.Add(book);

                _context.SaveChanges();
                Console.WriteLine("You borrowed a book. Enjoy!");
            }
            else
            {
                Console.WriteLine("Book and borrower could not be found.Book copies may be out of stock. ");
            }
        }
        
        public void ReturnABook(int bookId, int borrowerId)
        {
            var book = _context.Books.SingleOrDefault(b => b.BookID == bookId);
            var borrower = _context.Borrowers.SingleOrDefault(b => b.BorrowerID == borrowerId);

            if (book != null && borrower != null)
            {
                var returnedBook = new LogBook
                {
                    Title = book.Title,
                    ReturnDate = DateTime.Now,
                    Book = book
                };

                _context.LogBooks.Add(returnedBook);

                book.IsAvailable = true;
                borrower.Books.Remove(book);

                Console.WriteLine($"Borrower with Id {borrowerId} returned a book with Id {bookId}. Thank you!");

                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Sorry! The book or borrower could not be found\r\n");
            }
        }


        public void BorrowerLogBook(int BorrowerPIN)
        {
            var borrower = _context.Borrowers
                .Include(b => b.Books)
                .ThenInclude(b => b.Authors)
                .FirstOrDefault(b => b.BorrowerID == BorrowerPIN);

            if (borrower != null)
            {
                Console.WriteLine($"Showing loan History for {borrower.FirstName} {borrower.LastName}:");

                foreach (var book in borrower.Books)
                {
                    Console.WriteLine($"Book Title: {book.Title} with ISBN: {book.ISBN}");
                    Console.WriteLine("Authors:");

                    foreach (var author in book.Authors)
                    {
                        Console.WriteLine($"{author.firstName} {author.lastName}");
                    }

                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Borrower could not be found.");
            }
        }

        public void BookLogBook(int BookId)
        {
            var book = _context.Books
                .Include(b => b.Authors)
                .ThenInclude(a => a.Books)
                .FirstOrDefault(b => b.BookID == BookId);

            if (book != null)
            {
                Console.WriteLine($"Loan History for Book: {book.Title}, ISBN: {book.ISBN}");
                Console.WriteLine("Authors:");

                foreach (var author in book.Authors)
                {
                    Console.WriteLine($"{author.firstName} {author.lastName}");
                }

                Console.WriteLine("Borrowers:");

                foreach (var borrower in book.Authors
                    .SelectMany(a => a.Books)
                    .Where(b => b.Borrower != null)
                    .Select(b => b.Borrower))
                {
                    Console.WriteLine($"{borrower.FirstName} {borrower.LastName}");
                }
            }
            else
            {
                Console.WriteLine("Book could not be found.");
            }

        }

        public void RemoveAllData()
        {
            RemoveAllAuthors();
            RemoveAllBooks();
            RemoveAllBorrowers();

            Console.WriteLine("All data including Authors, Books and Borrowers are removed successfully!");
            _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Borrowers', RESEED, 0)");
            _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Books', RESEED, 0)");
            _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Authors', RESEED, 0)");
            _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('LogBooks', RESEED, 0)");
        }
        private void RemoveAllAuthors()
        {
            var authors = _context.Authors.ToList();
            _context.Authors.RemoveRange(authors);
            _context.SaveChanges();
        }

        private void RemoveAllBooks()
        {
            var Log = _context.LogBooks.ToList();
            var books = _context.Books.ToList();
            _context.LogBooks.RemoveRange(Log);
            _context.Books.RemoveRange(books);
            _context.SaveChanges();
        }

        private void RemoveAllBorrowers()
        {
            var borrowers = _context.Borrowers.ToList();
            _context.Borrowers.RemoveRange(borrowers);
            _context.SaveChanges();
        }
    }
}












    


    

    

