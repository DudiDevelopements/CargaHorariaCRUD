using CargaHorariaCRUD.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CargaHorariaCRUD.Models.ViewComponents
{
    public class Enviados : BaseViewComponent
    {
        public Enviados(IEnvioRepository envioRepository) : base(envioRepository) { }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var enviosUsuario = await GetEnviosDoUsuarioAsync();
            return View(enviosUsuario);
        }
    }
}
