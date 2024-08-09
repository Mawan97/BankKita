using System;
using System.Collections.Generic;

namespace BankKita.Web.API.Models;

public partial class RekeningNasabah
{
    public long RekeningNasabahId { get; set; }

    public string NoRekening { get; set; } = null!;

    public long JenisRekeningId { get; set; }

    public decimal Saldo { get; set; }

    public DateTime TanggalBuka { get; set; }

    public virtual JenisRekening JenisRekening { get; set; } = null!;
}
