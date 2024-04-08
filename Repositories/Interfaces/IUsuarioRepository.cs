using CargaHorariaCRUD.Models.Models;

namespace CargaHorariaCRUD.Repositories.Interfaces {
    public interface IUsuarioRepository {
        public Task<UsuarioModel> GetUsuarioByCPF(string cpf);
    }
}
