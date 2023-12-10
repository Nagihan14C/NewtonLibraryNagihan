using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.EntityFrameworkCore;
using NewtonLibraryNagihan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NewtonLibraryNagihan.Models
{
    internal class Context : DbContext
    {
     
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<LogBook> LogBooks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             
            optionsBuilder.UseSqlServer(@"Server=tcp:newtonyh.database.windows.net,1433;Initial Catalog = NewtonLibrary; Persist Security Info=False;User ID = NewtonLibraryNagihan; Password=NewtonLibrary123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30");
             
        }
     
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

       // {
          //  optionsBuilder.UseSqlServer(@"
          //  Server=localhost; 
          //  Database=NewtonLibraryNagihan; 
          //  Trusted_Connection=True; 
           // Trust Server Certificate =Yes; 
           // User Id=NewtonLibrary; 
          //  password=NewtonLibrary");

       // }



        private readonly IEncryptionProvider _provider;
        public Context()
        {
            this._provider = new GenerateEncryptionProvider("hkjb43sp9vbtgd4a12fgh67mnb789nkh");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseEncryption(this._provider);
        }
    }
}

