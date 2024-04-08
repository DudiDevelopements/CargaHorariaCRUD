using CargaHorariaCRUD.Enums;
using CargaHorariaCRUD.Models.Models;
using CargaHorariaCRUD.Repositories.Interfaces;
using CargaHorariaCRUD.WebHelper.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CargaHorariaCRUD.Controllers {
    [Route("Adm")]
    public class RecebidosController(IEnvioRepository envioRepository, ISessao sessao) : Controller {
        [Route("Recebidos")]
        public async Task<IActionResult> Index() {
            var usuario = sessao.GetSessao();
            switch (usuario) {
                case AdmModel: {
                    IEnumerable<EnvioModel> todosOsEnvios = await envioRepository.GetAll();
                    return View(todosOsEnvios);
                }
                case UsuarioModel:
                    TempData["MsgPopup"] = "Você não tem permissão pra acessar essa página.!";
                    return RedirectToAction("Index", "Home", EnumUsuario.Aluno);
                default:
                    TempData["MensagemErro"] = "Você precisa estar logado pra acessar essa página.";
                    return RedirectToAction("Index", "EstudanteLogin");
            }
        }

        [HttpPost]
        [Route("Validar")]
        public async Task<IActionResult> Validar(int idcomprovante, int novaCargaHoraria) {
            if(await envioRepository.UpdateValidado(idcomprovante, novaCargaHoraria))
                return Json(new {
                    success = true,
                    newCgHoraria = ((envioRepository.GetById(idcomprovante).Result.CargaHoraria)/60).ToString()
                });
            else
                return Json(new {
                    success = false
                });
        }

        [HttpPost]
        [Route("Revogar")]
        public async Task<IActionResult> Revogar(int idcomprovante) {
            if(await envioRepository.UpdateValidado(idcomprovante))
                return Json(new {
                    success = true
                });
            else
                return Json(new {
                    success = false
                });
        }
    }
}
