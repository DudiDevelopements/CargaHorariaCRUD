using CargaHorariaCRUD.Enums;
using CargaHorariaCRUD.Models;
using CargaHorariaCRUD.Models.Models;
using CargaHorariaCRUD.WebHelper.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CargaHorariaCRUD.Controllers {
    [Controller]
    [Route("PaginaInicial")]
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly ISessao _session;

        public HomeController(ILogger<HomeController> logger, ISessao session) {
            _logger = logger;
            _session = session;
        }

        [Route("")]
        public IActionResult Index() {
            object? usuario = _session.GetSessao();
            if(usuario is UsuarioModel)
                return View(EnumUsuario.Aluno);
            else if(usuario is AdmModel)
                return View(EnumUsuario.Admin);
            else {
                TempData["MensagemErro"] = "Você precisa estar logado pra acessar essa página.";
                return RedirectToAction("Index", "EstudanteLogin");
            }
        }

        [Route("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("/Erro/{statusCode}")]
        public IActionResult Erro404ou500(int statusCode) {
            ViewBag.StatusCode = statusCode;
            /*
            string originalpath = "unknown";
            if(HttpContext.Items.ContainsKey("OriginalPath"))
            {
                originalpath = HttpContext.Items["OriginalPath"] as string;
            }
            return View();*/

            return View();
        }
    }
}
