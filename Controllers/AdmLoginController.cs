using CargaHorariaCRUD.Enums;
using CargaHorariaCRUD.Models.FormModels;
using CargaHorariaCRUD.Models.Models;
using CargaHorariaCRUD.Repositories.Interfaces;
using CargaHorariaCRUD.WebHelper.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CargaHorariaCRUD.Controllers {
    [Route("Adm")]
    public class AdmLoginController(IAdmRepository context, ISessao sessao) : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            var usuario = sessao.GetSessao();
            return usuario switch
            {
                UsuarioModel => RedirectToAction("Index", "Home", EnumUsuario.Aluno),
                AdmModel => RedirectToAction("Index", "Home", EnumUsuario.Admin),
                _ => View()
            };
        }

        [Route("Logar")]
        [HttpPost]
        public async Task<IActionResult> Logar(AdmLoginModel login) {
            try {
                if(ModelState.IsValid) {
                    try {
                        var user = await context.GetAdmByLogin(login);
                        sessao.CriarSessaoAdm(user);
                        return RedirectToAction("Index", "Home", EnumUsuario.Admin);
                    } catch(Exception err) {
                        TempData["MensagemErro"] = $"{err.Message}";
                        return View("Index");
                    }
                } else
                    return View("Index");
            } catch(Exception err) {
                TempData["MensagemErro"] = $"{err.Message}";
                return View("Index");
            }
        }
    }
}
