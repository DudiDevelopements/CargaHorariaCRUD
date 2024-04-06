using CargaHorariaCRUD.Enums;
using CargaHorariaCRUD.Models.Models;
using CargaHorariaCRUD.WebHelper.Interfaces;
using Newtonsoft.Json;

namespace CargaHorariaCRUD.WebHelper
{
    public class Sessao:ISessao
    {
        private IHttpContextAccessor _httpContext;

        public Sessao(IHttpContextAccessor httpContext) => _httpContext = httpContext;

        public void CriarSessao(UsuarioModel usuario)
        {
            string usuarioLogado = JsonConvert.SerializeObject(usuario);
            _httpContext.HttpContext?.Session.SetString("UsuarioLogado", usuarioLogado);

        }

        public void CriarSessaoAdm(AdmModel adm)
        {
            //Gera json de usuario com senha nula (segurança)
            AdmModel usuarioSecurity = new(adm.Id, adm.Nome, adm.Email, adm.DataNasc);
            string usuarioLogado = JsonConvert.SerializeObject(usuarioSecurity);
            _httpContext.HttpContext?.Session.SetString("UsuarioLogado",usuarioLogado);
        }

        public void DestruirSessao()
        {
            _httpContext.HttpContext?.Session.Remove("UsuarioLogado");
        }

        public object? GetSessao()
        {
            string? usuarioLogado = _httpContext.HttpContext?.Session.GetString("UsuarioLogado");

            //Verifica se a string é vazia ou nula
            if(string.IsNullOrEmpty(usuarioLogado)) return null;

            // Verifica se é um UsuarioModel
            if(usuarioLogado.Contains("Envios")) return JsonConvert.DeserializeObject<UsuarioModel>(usuarioLogado);

            // Verifica se é um AdmModel
            else if(usuarioLogado.Contains("Senha")) return JsonConvert.DeserializeObject<AdmModel>(usuarioLogado);

            // Se não for nenhum dos dois manda erro
            else throw new Exception("Tipo de objeto desconhecido na sessão");
        }
    }
}
