using CargaHorariaCRUD.Models;
using CargaHorariaCRUD.Repositories;
using CargaHorariaCRUD.Repositories.Interfaces;
using CargaHorariaCRUD.WebHelper.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CargaHorariaCRUD.Controllers
{
    [Route("EstudanteLogin")]
    public class EstudanteLoginController : Controller
    {
        private readonly IUsuarioRepository _context;
        private readonly ISessao _sessao;

        public EstudanteLoginController(IUsuarioRepository context, ISessao sessao)
        {
            _context = context;
            _sessao = sessao;
        }

        [Route("/")]
        public IActionResult Index()
        {
            if(_sessao.IsLogged())
            {
                return RedirectToAction("Index","Home");
            }
            else return View();
        }

        [Route("Logar")]
        public async Task<IActionResult> Logar(EstudanteLoginModel _login) 
        {
            try
            {
                if(ModelState.IsValid)
                {
                    UsuarioModel user = await _context.GetUsuarioByCPF(_login.CPF) ?? throw new Exception("Dados inválidos");

                    if(user != null && user.DataNascimento.ToString() == _login.Data_nasc)
                    {
                        _sessao.CriarSessao(user);
                        return RedirectToAction("Index","Home");
                    } else
                    {
                        TempData["MensagemErro"] = "Dados inválidos";
                        return View("Index");
                    }


                } else
                    return View("Index");
            } catch(Exception err)
            {
                TempData["MensagemErro"] = $"{err.Message}";
                return RedirectToAction("Entrar");
            }
        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            _sessao.DestruirSessao();
            return View("Index");
        }
    }
}
