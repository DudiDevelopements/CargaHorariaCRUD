using CargaHorariaCRUD.Models;
using CargaHorariaCRUD.Repositories.Interfaces;
using CargaHorariaCRUD.WebHelper.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CargaHorariaCRUD.Controllers
{
    [Route("Enviar")]
    public class EnvioController(IEnvioRepository envioRepository,ISessao sessao,IWebHostEnvironment enviroment):Controller
    {
        private IWebHostEnvironment Enviroment = enviroment;
        private readonly IEnvioRepository _envioRepository = envioRepository;
        private readonly ISessao _sessao = sessao;

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Submit")]
        public async Task<IActionResult> OnPostEnviar(EnvioFormModel form)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    UsuarioModel? usuarioLogado = _sessao.GetSessao();
                    int idDoAluno = usuarioLogado.Id;
                    string nomeAluno = usuarioLogado.Nome;
                
                    string? caminhoArquivo = EnviarArquivo(idDoAluno, nomeAluno, form.FormArquivo);
                    EnvioModel novoEnvio = new(idDoAluno, form.FormEmail, form.FormTurma, form.FormProf, form.FormTipo, form.FormObs, caminhoArquivo, DateTime.Now);

                    await _envioRepository.Add(novoEnvio);

                    TempData["MensagemSucesso"] = "Comprovante enviado com sucesso!";
                    return RedirectToAction("Index");
                }
                catch
                {
                    TempData["MensagemErroEnvio"] = "Ocorreu um erro ao enviar seu comprovante, tente novamente mais tarde.";
                    return View("Index");
                }
            } else {
                return View("Index");
            }
        }

            /* --- Envio do arquivo de comprovante --- */
        protected string? EnviarArquivo(int alunoid, string nomeAluno, IFormFile? file)
        {
            string wwwPath = Enviroment.WebRootPath;
            string idDoAluno = alunoid.ToString();
            //string contentPath = Enviroment.ContentRootPath;
            
            string path = Path.Combine(wwwPath,"Envios",idDoAluno);
            if(!Directory.Exists(path)) Directory.CreateDirectory(path);

            try
            {
                string extensao = Path.GetExtension(file.FileName);
                string nomeComprovante = $"{idDoAluno}_{nomeAluno}_{Guid.NewGuid()}.{extensao}";
                string fullPath = Path.Combine(path,nomeComprovante);
                using(FileStream stream = new(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    //TempData["MensagemSucesso"] = "Comprovante enviado com sucesso!";
                }
                //return RedirectToAction("Index");
                return Path.Combine("Envios", idDoAluno, nomeComprovante);
            } catch
            {
                //TempData["MensagemErroEnvio"] = "Ocorreu um erro ao enviar seu comprovante, tente novamente mais tarde.";
                return null;
            }

        }
            /* --- Fim do envio do arquivo de comprovante --- */
    }
}
