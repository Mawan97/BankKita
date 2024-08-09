using ViewModels.Rekening;

namespace BankKita.Web.API.Contract
{
    public interface IRekeningRepository
    {
        List<RekeningGrid> GetRekeningAll();
        RekeningGrid GetRekeningByNo(string noRekening);
        void UpsertRekening(string? noRekening, RekeningUpsert dataInput);
        bool DeleteRekening(string noRekening);
    }
}
