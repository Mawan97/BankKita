using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Rekening
{
    public class RekeningGrid
    {
        public long RekeningNasabahId { get; set; }
        public string NoRekening { get; set; }
        public required string JenisRekening { get; set; }
        public required string Saldo { get; set; }
        public required string TanggalBuka { get; set; }
    }
}
