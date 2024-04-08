using CargaHorariaCRUD.Models.Models;
using CargaHorariaCRUD.Repositories.Interfaces;
using CargaHorariaCRUD.WebHelper.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CargaHorariaCRUD.Models.ViewComponents {
    public class Header(ISessao sessao) : ViewComponent {
        private readonly ISessao _sessao = sessao;

        public async Task<IViewComponentResult?> InvokeAsync() {
            string? usuarioJSON = HttpContext.Session.GetString("UsuarioLogado");
            if(usuarioJSON != null) {
                object? usuarioLogado = _sessao.GetSessao();
                return View(usuarioLogado);
            } else
                return View(new EmptyResult());
        }
    }
}
