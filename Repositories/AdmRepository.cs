using CargaHorariaCRUD.Models.FormModels;
using CargaHorariaCRUD.Models.Models;
using CargaHorariaCRUD.Repositories.Interfaces;
using CargaHorariaCRUD.WebHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CargaHorariaCRUD.Repositories {
    public class AdmRepository : IAdmRepository {
        private readonly UsuariosIfmsContext _context = new();
        public async Task<AdmModel> GetAdmByLogin(AdmLoginModel login) {
            AdmModel? usuario = await _context.Administradores.FirstOrDefaultAsync(
                a => a.Email == login.Email && a.Senha == login.Senha)
                ?? throw new Exception("Dados inválidos");

            return usuario;
        }
    }
}
