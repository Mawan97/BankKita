using System;
using System.Collections.Generic;

namespace BankKita.Web.API.Models;

public partial class JenisRekening
{
    public long JenisRekeningId { get; set; }

    public string Deskripsi { get; set; } = null!;

    public virtual ICollection<RekeningNasabah> RekeningNasabahs { get; set; } = new List<RekeningNasabah>();
}
