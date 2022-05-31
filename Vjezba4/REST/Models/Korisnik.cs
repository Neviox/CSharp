using System;
using System.Collections.Generic;

namespace Transakcije.Models
{
    public partial class Korisnik
    {
        public int? IdKorisnika { get; set; }
        public string? Ime { get; set; }
        public string? Grad { get; set; }
    }
}
