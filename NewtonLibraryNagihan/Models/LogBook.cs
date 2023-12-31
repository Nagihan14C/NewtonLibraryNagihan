﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtonLibraryNagihan.Models
{
    internal class LogBook
    {
            public int Id { get; set; }

            public static int Count;

            public string Name { get; set; } = "Returned:" + " " + ++Count;

            public string? Title { get; set; }

            public DateTime ReturnDate { get; set; }
            public Book? Book { get; set; }

        }
    }

