using CargaHorariaCRUD.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CargaHorariaCRUD.Models.ViewComponents
{
    public class Header : ViewComponent
    {
        public async Task<IViewComponentResult?> InvokeAsync()
        {
            string? usuarioJSON = HttpContext.Session.GetString("UsuarioLogado");
            if(usuarioJSON != null)
            {
                UsuarioModel? usuarioLogado = JsonConvert.DeserializeObject<UsuarioModel>(usuarioJSON);
                return View(usuarioLogado);
            } else
                return null;
        }
    }
}
