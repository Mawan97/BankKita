using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Rekening
{
    public class RekeningUpsert
    {
        [Required]
        public long JenisrekeningId { get; set; }

        [Required]
        public decimal Saldo {  get; set; }
    }
}
