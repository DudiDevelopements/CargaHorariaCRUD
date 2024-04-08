using CargaHorariaCRUD.Enums;
using CargaHorariaCRUD.Models.FormModels;
using CargaHorariaCRUD.Models.Models;
using CargaHorariaCRUD.Repositories.Interfaces;
using CargaHorariaCRUD.WebHelper.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CargaHorariaCRUD.Controllers {
    [Route("Adm")]
    public class AdmLoginController : Controller {
        private readonly IAdmRepository _context;
        private readonly ISessao _sessao;

        public AdmLoginController(IAdmRepository context, ISessao sessao) {
            _context = context;
            _sessao = sessao;
        }
        [Route("")]
        public IActionResult Index() {
            object? usuario = _sessao.GetSessao();
            if(usuario is UsuarioModel)
                return RedirectToAction("Index", "Home", EnumUsuario.Aluno);
            else if(usuario is AdmModel)
                return RedirectToAction("Index", "Home", EnumUsuario.Admin);
            else
                return View();
        }

        [Route("Logar")]
        [HttpPost]
        public async Task<IActionResult> Logar(AdmLoginModel login) {
            try {
                if(ModelState.IsValid) {
                    try {
                        AdmModel user = await _context.GetAdmByLogin(login);
                        _sessao.CriarSessaoAdm(user);
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
