using NewtonLibraryNagihan.Data;
using NewtonLibraryNagihan.Models;

namespace NewtonLibraryNagihan
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (Context context = new Context())
            {
                DataAccess dataAccess = new DataAccess(context);
                dataAccess.Seed();
                dataAccess.RunMenu();
               // dataAccess.AddAnAuthor();
                //dataAccess.AddABook();
                //dataAccess.AddABorrower();
                //dataAccess.BorrowABook(1,3); //put bookId and borrowerId
               // dataAccess.ReturnABook(1, 1); //put bookId and borrowerId
               // dataAccess.RemoveAllData();
                //dataAccess.BorrowerLogBook("884422"); // put LoanCardPIN
                //dataAccess.BookLogBook("1-061-96436-1"); //put ISBN
            }

        }
    }
}