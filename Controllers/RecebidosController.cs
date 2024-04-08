using CargaHorariaCRUD.Enums;
using CargaHorariaCRUD.Models.Models;
using CargaHorariaCRUD.Repositories.Interfaces;
using CargaHorariaCRUD.WebHelper.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CargaHorariaCRUD.Controllers {
    [Route("Adm")]
    public class RecebidosController(IEnvioRepository envioRepository, ISessao sessao, ILogger<RecebidosController> logger) : Controller {
        private readonly ILogger<RecebidosController> _logger;
        private readonly IEnvioRepository _envioRepository = envioRepository;
        private readonly ISessao _sessao = sessao;

        [Route("Recebidos")]
        public async Task<IActionResult> Index() {
            object? usuario = _sessao.GetSessao();
            if(usuario is AdmModel) {
                IEnumerable<EnvioModel> todosOsEnvios = await _envioRepository.GetAll();
                return View(todosOsEnvios);
            } else if(usuario is UsuarioModel) {
                TempData["MsgPopup"] = "Você não tem permissão pra acessar essa página.!";
                return RedirectToAction("Index", "Home", EnumUsuario.Aluno);
            } else {
                TempData["MensagemErro"] = "Você precisa estar logado pra acessar essa página.";
                return RedirectToAction("Index", "EstudanteLogin");
            }
        }

        [HttpPost]
        [Route("Validar")]
        public async Task<IActionResult> Validar(int idcomprovante, int novaCargaHoraria) {
            if(await _envioRepository.UpdateValidado(idcomprovante, novaCargaHoraria))
                return Json(new {
                    success = true
                });
            else
                return Json(new {
                    success = false
                });
        }

        [HttpPost]
        [Route("Revogar")]
        public async Task<IActionResult> Revogar(int idcomprovante) {
            if(await _envioRepository.UpdateValidado(idcomprovante))
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
