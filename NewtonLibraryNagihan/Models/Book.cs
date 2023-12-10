using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtonLibraryNagihan.Models
{
    internal class Book
    {
        public int BookID { get; set; }
        public string? Title { get; set; }
        public string? ISBN { get; set; }
        public int? PublishedYear { get; set; }
        public double? Grade { get; set; }
        public Borrower? Borrower { get; set; }
        public ICollection<Author> Authors { get; set; } = new List<Author>();
        public ICollection<LogBook> LogBooks { get; set; }

        public bool IsAvailable { get; set; } = true;


        public Book()
        {

        }
    }
}

