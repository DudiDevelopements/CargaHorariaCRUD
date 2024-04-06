using CargaHorariaCRUD.Models.FormModels;
using CargaHorariaCRUD.Models.Models;

namespace CargaHorariaCRUD.Repositories.Interfaces
{
    public interface IAdmRepository
    {
        public Task<AdmModel> GetAdmByLogin(AdmLoginModel login);

    }
}
