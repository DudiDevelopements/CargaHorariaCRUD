using CargaHorariaCRUD.Models.Models;
using CargaHorariaCRUD.Repositories.Interfaces;
using CargaHorariaCRUD.WebHelper;
using Microsoft.EntityFrameworkCore;

namespace CargaHorariaCRUD.Repositories {
    public class EnvioRepository() : IEnvioRepository
    {
        private readonly UsuariosIfmsContext _context = new();
        public async Task<bool> Add(EnvioModel envio) {
            await _context.Envios.AddAsync(envio);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(EnvioModel envio) {
            _context.Envios.Remove(envio);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<EnvioModel>> GetAll() {
            return await _context.Envios.Include(e => e.IdAlunoNavigation).ToListAsync();
        }

        public async Task<List<EnvioModel>> GetEnviosPorIdAluno(int idaluno) {
            var envios = await _context.Envios.Where(e => e.IdAluno == idaluno).ToListAsync();
            return envios;
        }

        public async Task<EnvioModel> GetById(int id) {
            var envio = await _context.Envios.FirstOrDefaultAsync(x => x.Id == id);
            if (envio != null) return envio;
            else throw new Exception("Não encontrado");
        }

        public int? CargaHorariaTotalById(int idaluno) {
            var cgTotal = _context.Envios.Where(e => e.IdAluno == idaluno).Sum(e => e.CargaHoraria);
            return cgTotal;
        }

        public async Task<bool> UpdateValidado(int id, int? novaCargaHoraria = null) {
            var envio = await _context.Envios.FindAsync(id);

            if (envio == null) return false;
            //Será chamado em validar()
            if(novaCargaHoraria != null) {
                envio.CargaHoraria = novaCargaHoraria;
                envio.Validado = true;
                await _context.SaveChangesAsync();
                return true;
            }
            
            //Será chamado em revogar()
            envio.CargaHoraria = null;
            envio.Validado = false;
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
