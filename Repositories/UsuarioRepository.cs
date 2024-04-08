using CargaHorariaCRUD.Models.Models;
using CargaHorariaCRUD.Repositories.Interfaces;
using CargaHorariaCRUD.WebHelper;
using Microsoft.EntityFrameworkCore;

namespace CargaHorariaCRUD.Repositories {
    public class UsuarioRepository : IUsuarioRepository {
        private readonly UsuariosIfmsContext _context = new();

        public async Task<UsuarioModel> GetUsuarioByCPF(string cpf) {
            if(!string.IsNullOrEmpty(cpf)) {
                UsuarioModel user = await _context.Usuarios.FirstOrDefaultAsync(x => x.Cpf == cpf) ?? throw new Exception("Usuario não encontrado");

                return user;
            } else
                throw new Exception("Campo CPF nulo");
        }

    }
}
