using CargaHorariaCRUD.Models.Models;

namespace CargaHorariaCRUD.Repositories.Interfaces {
    public interface IEnvioRepository {
        public Task<bool> Add(EnvioModel envio);
        public Task<bool> Delete(EnvioModel envio);
        public Task<List<EnvioModel>> GetAll();
        public Task<List<EnvioModel>> GetEnviosPorIdAluno(int idaluno);
        public int? CargaHorariaTotalById(int idaluno);
        //public Task<EnvioModel> GetById(int id);
        public Task<bool> UpdateValidado(int id, int? novaCargaHoraria = null);

    }
}
