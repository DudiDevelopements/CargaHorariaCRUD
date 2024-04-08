using CargaHorariaCRUD.Controllers;
using CargaHorariaCRUD.Models.Models;
using CargaHorariaCRUD.Repositories.Interfaces;
using CargaHorariaCRUD.WebHelper;
using Microsoft.EntityFrameworkCore;

namespace CargaHorariaCRUD.Repositories {
    public class EnvioRepository : IEnvioRepository {
        private readonly ILogger<RecebidosController> _logger;

        public EnvioRepository(ILogger<RecebidosController> logger) {
            _logger = logger;
        }

        private readonly UsuariosIfmsContext _context = new();
        public async Task<bool> Add(EnvioModel envio) {
            if(envio != null) {
                await _context.Envios.AddAsync(envio);
                await _context.SaveChangesAsync();
                return true;
            } else
                return false;
        }

        public async Task<bool> Delete(EnvioModel envio) {
            if(envio != null) {
                _context.Envios.Remove(envio);
                await _context.SaveChangesAsync();
                return true;
            } else
                return false;
        }

        public async Task<List<EnvioModel>> GetAll() {
            return await _context.Envios.Include(e => e.IdAlunoNavigation).ToListAsync();
        }

        public async Task<List<EnvioModel>> GetEnviosPorIdAluno(int idaluno) {
            List<EnvioModel> envios = await _context.Envios.Where(e => e.IdAluno == idaluno).ToListAsync();
            return envios;
        }

        public int? CargaHorariaTotalById(int idaluno) {
            int? cgTotal = _context.Envios.Where(e => e.IdAluno == idaluno).Sum(e => e.CargaHoraria);
            return cgTotal;
        }

        public async Task<bool> UpdateValidado(int id, int? novaCargaHoraria = null) {
            var envio = _context.Envios.Find(id);

            _logger.LogInformation(id + "    " + novaCargaHoraria);
            if(envio != null) {
                //Será chamado em validar()
                if(novaCargaHoraria != null) {
                    envio.CargaHoraria = novaCargaHoraria;
                    envio.Validado = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
                //Será chamado em revogar()
                else {
                    envio.CargaHoraria = null;
                    envio.Validado = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
            } else
                return false;
        }
    }
}
