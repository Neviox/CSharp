using System;
using System.Collections.Generic;

namespace Transakcije.Models
{
    public partial class Uplatnica
    {
        public long IdUplatnica { get; set; }
        public string Platitelj { get; set; } = null!;
        public string Primatelj { get; set; } = null!;
        public long Iznos { get; set; }
        public string Valuta { get; set; } = null!;
        public string Iban { get; set; } = null!;
        public string? VrijemeUplate { get; set; }
    }
}
