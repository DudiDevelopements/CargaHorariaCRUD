using CargaHorariaCRUD.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CargaHorariaCRUD.Models.ViewComponents
{
    public abstract class BaseViewComponent : ViewComponent
    {
        protected readonly IEnvioRepository _envioRepository;

        protected BaseViewComponent(IEnvioRepository envioRepository)
        {
            _envioRepository = envioRepository;
        }

        protected async Task<List<EnvioModel>?> GetEnviosDoUsuarioAsync()
        {
            string? usuarioJSON = HttpContext.Session.GetString("UsuarioLogado");
            if(usuarioJSON != null)
            {
                UsuarioModel? usuarioLogado = JsonConvert.DeserializeObject<UsuarioModel>(usuarioJSON);
                return await _envioRepository.GetEnviosPorIdAluno(usuarioLogado.Id);
            }
            else return null;
        }

        protected int? GetCargaHorariaTotal()
        {
            string? usuarioJSON = HttpContext.Session.GetString("UsuarioLogado");
            if(usuarioJSON != null)
            {
                UsuarioModel? usuarioLogado = JsonConvert.DeserializeObject<UsuarioModel>(usuarioJSON);
                return _envioRepository.CargaHorariaTotalById(usuarioLogado.Id);
            }

            else return null;
        }
    }
}
