using CargaHorariaCRUD.Enums;
using CargaHorariaCRUD.Models.FormModels;
using CargaHorariaCRUD.Models.Models;
using CargaHorariaCRUD.Repositories.Interfaces;
using CargaHorariaCRUD.WebHelper.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
            object? usuario = _sessao.GetSessao();
            if(usuario is UsuarioModel) return RedirectToAction("Index", "Home", EnumUsuario.Aluno);
            else if (usuario is AdmModel) return RedirectToAction("Index", "Home", EnumUsuario.Admin);
            else
            {
                TempData["MensagemErro"] = "Você precisa estar logado pra acessar essa página.";
                return RedirectToAction("Index", "EstudanteLogin"); 
            }
        }

        [HttpPost]
        [Route("Submit")]
        public async Task<IActionResult> OnPostEnviar(EnvioFormModel form)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    object? usuario = _sessao.GetSessao();
                    if(usuario is UsuarioModel usuarioLogado) {  
                        int idDoAluno = usuarioLogado.Id;
                        string nomeAluno = usuarioLogado.Nome;
                
                        string? caminhoArquivo = await EnviarArquivo(idDoAluno, nomeAluno, form.FormArquivo);
                        EnvioModel novoEnvio = new(idDoAluno, form.FormEmail, form.FormTurma, form.FormProf, form.FormTipo, form.FormObs, caminhoArquivo, DateTime.Now);

                        await _envioRepository.Add(novoEnvio);

                        TempData["MensagemSucesso"] = "Comprovante enviado com sucesso!";
                        return RedirectToAction("Index");
                    } else
                        return View("Index");
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
        protected async Task<string?> EnviarArquivo(int alunoid, string nomeAluno, IFormFile? file)
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
                    await file.CopyToAsync(stream);
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
