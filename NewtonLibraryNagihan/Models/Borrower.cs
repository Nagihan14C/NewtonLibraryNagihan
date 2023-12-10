using EntityFrameworkCore.EncryptColumn.Attribute;
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
    internal class Borrower
    {
        public int BorrowerID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? LoanCard { get; set; }

        [EncryptColumn]
        public string? LoanCardPIN { get; set; }

        public DateTime? LoanDate { get; set; }
        public DateTime? ExpectedReturnDate
        {
            get
            {
                return LoanDate?.AddDays(14);

            }
            set
            {
                LoanDate = value;
            }
        }
        public ICollection<Book> Books { get; set; } = new List<Book>();

    }
}
