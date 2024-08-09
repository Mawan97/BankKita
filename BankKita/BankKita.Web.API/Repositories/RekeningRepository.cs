using BankKita.Web.API.Contract;
using BankKita.Web.API.Models;
using ViewModels.Rekening;

namespace BankKita.Web.API.Repositories
{
    public class RekeningRepository : IRekeningRepository
    {
        private readonly BankKitaContext context;

        public RekeningRepository(BankKitaContext context)
        {
            this.context = context;
        }

        public  List<RekeningGrid> GetRekeningAll()
        {
            try
            {
                var result = context.RekeningNasabahs.Join(context.JenisRekenings,
                                r => r.JenisRekeningId,
                                j => j.JenisRekeningId,
                                (r, j) => new RekeningGrid()
                                {
                                    RekeningNasabahId = r.RekeningNasabahId,
                                    NoRekening = r.NoRekening,
                                    JenisRekening = j.Deskripsi,
                                    Saldo = r.Saldo.ToString(),
                                    TanggalBuka = r.TanggalBuka.ToString("dd MMM yyyy"),
                                }).ToList();

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public RekeningGrid GetRekeningByNo(string noRekening)
        {
            try
            {
                var result = context.RekeningNasabahs.Join(context.JenisRekenings,
                                r => r.JenisRekeningId,
                                j => j.JenisRekeningId,
                                (r, j) => new RekeningGrid()
                                {
                                    RekeningNasabahId = r.RekeningNasabahId,
                                    NoRekening = r.NoRekening,
                                    JenisRekening = j.Deskripsi,
                                    Saldo = r.Saldo.ToString(),
                                    TanggalBuka = r.TanggalBuka.ToString("dd MMM yyyy"),
                                }).SingleOrDefault(x => x.NoRekening == noRekening);

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void UpsertRekening(string? noRekening, RekeningUpsert dataInput)
        {
            try
            {
                if (!string.IsNullOrEmpty(noRekening))
                {
                    var oldRekening = context.RekeningNasabahs.SingleOrDefault(x => x.NoRekening == noRekening);
                    oldRekening.JenisRekeningId = dataInput.JenisrekeningId;
                    oldRekening.Saldo = dataInput.Saldo;
                }
                else
                {
                    var newRekening = new RekeningNasabah()
                    {
                        NoRekening = GenerateNoRekering(),
                        JenisRekeningId = dataInput.JenisrekeningId,
                        Saldo = dataInput.Saldo,
                        TanggalBuka = DateTime.Now

                    };

                    context.RekeningNasabahs.Add(newRekening);
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool DeleteRekening(string noRekening)
        {
            try
            {
                var rekeningDeleted = context.RekeningNasabahs.SingleOrDefault(x => x.NoRekening == noRekening);

                if (rekeningDeleted is null)
                {
                    return false;
                }
                else
                {
                    context.RekeningNasabahs.Remove(rekeningDeleted);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private string GenerateNoRekering()
        {
            var rekeningList = context.RekeningNasabahs.ToList();
            string newNumberString = "0001";
            var nowDateString = DateTime.Now.ToString("yyyyMMdd");

            if (rekeningList.Count > 0)
            {
                var noRekeningLast = rekeningList.OrderByDescending(x => x.RekeningNasabahId).Select(x => x.NoRekening).Single();

                var oldDateString = noRekeningLast.Substring(0, 8);

                if (oldDateString == nowDateString)
                {
                    int newNumber = Convert.ToInt32(noRekeningLast.Substring(8, 4)) + 1;
                    newNumberString = newNumber.ToString().PadLeft(4, '0');
                }
            }

            return nowDateString + newNumberString;
        }
    }
}
