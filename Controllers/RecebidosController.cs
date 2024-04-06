using CargaHorariaCRUD.Enums;
using CargaHorariaCRUD.Models.Models;
using CargaHorariaCRUD.Repositories.Interfaces;
using CargaHorariaCRUD.WebHelper.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CargaHorariaCRUD.Controllers
{
    [Route("Adm")]
    public class RecebidosController(IEnvioRepository envioRepository,ISessao sessao):Controller
    {
        private readonly IEnvioRepository _envioRepository = envioRepository;
        private readonly ISessao _sessao = sessao;

        [Route("Recebidos")]
        public async Task<IActionResult> Index()
        {
            object? usuario = _sessao.GetSessao();
            if(usuario is AdmModel) 
            {
                IEnumerable<EnvioModel> todosOsEnvios = await _envioRepository.GetAll();
                return View(todosOsEnvios);
            }
            else if(usuario is UsuarioModel)
            {
                TempData["MsgPopup"] = "Você não tem permissão pra acessar essa página.!";
                return RedirectToAction("Index","Home",EnumUsuario.Aluno);
            } else
            {
                TempData["MensagemErro"] = "Você precisa estar logado pra acessar essa página.";
                return RedirectToAction("Index","EstudanteLogin");
            }
        }
    }
}
