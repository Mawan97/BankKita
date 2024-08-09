using BankKita.Web.API.Contract;
using BankKita.Web.API.Models;
using ViewModels;

namespace BankKita.Web.API.Repositories
{
    public class JenisRekeningRepository : IJenisRekeningRepository
    {
        private readonly BankKitaContext context;

        public JenisRekeningRepository(BankKitaContext context)
        {
            this.context = context;
        }

        public List<DropDownModel> GetJenisRekeningDropDown()
        {
            try
            {
                var result = context.JenisRekenings.Select(x => new DropDownModel()
                {
                    Text = x.Deskripsi,
                    Value = x.JenisRekeningId
                }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
