using ViewModels;

namespace BankKita.Web.API.Contract
{
    public interface IJenisRekeningRepository
    {
        List<DropDownModel> GetJenisRekeningDropDown();
    }
}
