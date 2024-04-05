using CargaHorariaCRUD.Models;

namespace CargaHorariaCRUD.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        public Task<UsuarioModel> GetUsuarioByCPF(string cpf);
    }
}
