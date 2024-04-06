using CargaHorariaCRUD.Models.Models;
using CargaHorariaCRUD.Repositories.Interfaces;
using CargaHorariaCRUD.WebHelper;
using Microsoft.EntityFrameworkCore;

namespace CargaHorariaCRUD.Repositories
{
    public class EnvioRepository : IEnvioRepository
    {
        private readonly UsuariosIfmsContext _context = new();
        public async Task<bool> Add(EnvioModel envio)
        {
            if (envio != null) 
            {
                await _context.Envios.AddAsync(envio);
                await _context.SaveChangesAsync();
                return true;
            } 
            else return false;
        }

        public async Task<bool> Delete(EnvioModel envio)
        {
            if (envio != null)
            {
                _context.Envios.Remove(envio);
                await _context.SaveChangesAsync();
                return true;
            } 
            else return false;
        }

        public async Task<List<EnvioModel>> GetAll()
        {
            return await _context.Envios.ToListAsync();
        }
        /*
        public async Task<List<EnvioModel>> GetEnviosPorIdPagina(int itensPorPagina, int tamanhoPagina, int idaluno)
        {
            List<EnvioModel> envios = await _context.Envios.Where(e => e.IdAluno == idaluno)
                                .Skip((itensPorPagina - 1) * tamanhoPagina)
                                .Take(tamanhoPagina).ToListAsync();
            return envios;
        }
        */
        public async Task<List<EnvioModel>> GetEnviosPorIdAluno(int idaluno)
        {
            List<EnvioModel> envios = await _context.Envios.Where(e => e.IdAluno == idaluno).ToListAsync();
            return envios;
        }

        public int? CargaHorariaTotalById(int idaluno)
        {
            int? cgTotal = _context.Envios.Where(e => e.IdAluno == idaluno).Sum(e => e.CargaHoraria);
            return cgTotal;
        }

        public async Task<bool> Update(EnvioModel envio)
        {
            throw new NotImplementedException();
        }
    }
}
