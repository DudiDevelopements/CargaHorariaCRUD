using CargaHorariaCRUD.Enums;
using CargaHorariaCRUD.Models.FormModels;
using CargaHorariaCRUD.Models.Models;
using CargaHorariaCRUD.Repositories;
using CargaHorariaCRUD.Repositories.Interfaces;
using CargaHorariaCRUD.WebHelper.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CargaHorariaCRUD.Controllers {
    [Route("EstudanteLogin")]
    public class EstudanteLoginController : Controller {
        private readonly IUsuarioRepository _context;
        private readonly ISessao _sessao;

        public EstudanteLoginController(IUsuarioRepository context, ISessao sessao) {
            _context = context;
            _sessao = sessao;
        }

        [Route("/")]
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
        public async Task<IActionResult> Logar(EstudanteLoginModel _login) {
            try {
                if(ModelState.IsValid) {
                    UsuarioModel user = await _context.GetUsuarioByCPF(_login.CPF) ?? throw new Exception("Dados inválidos");

                    if(user != null && user.DataNascimento.ToString() == _login.Data_nasc) {
                        _sessao.CriarSessao(user);
                        return RedirectToAction("Index", "Home", EnumUsuario.Aluno);
                    } else {
                        TempData["MensagemErro"] = "Dados inválidos";
                        return View("Index");
                    }


                } else
                    return View("Index");
            } catch(Exception err) {
                TempData["MensagemErro"] = $"{err.Message}";
                return RedirectToAction("Index");
            }
        }

        [Route("Logout")]
        public IActionResult Logout() {
            _sessao.DestruirSessao();
            return View("Index");
        }
    }
}
