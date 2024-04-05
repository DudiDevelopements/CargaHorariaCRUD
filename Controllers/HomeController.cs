using CargaHorariaCRUD.Models;
using CargaHorariaCRUD.WebHelper.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CargaHorariaCRUD.Controllers
{
    [Route("Home")]
    public class HomeController:Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISessao _session;

        public HomeController(ILogger<HomeController> logger, ISessao session)
        {
            _logger = logger;
            _session = session;
        }

        [Route("")]
        public IActionResult Index()
        {
            if(_session.IsLogged()) {
                return View();
            } else return RedirectToAction("Entrar", "EstudanteLogin");
        }

        [Route("Error")]
        [ResponseCache(Duration = 0,Location = ResponseCacheLocation.None,NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
