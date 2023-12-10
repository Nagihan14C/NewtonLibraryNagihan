using NewtonLibraryNagihan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtonLibraryNagihan.Models
{
    internal class Author
    {
        public int AuthorID { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}