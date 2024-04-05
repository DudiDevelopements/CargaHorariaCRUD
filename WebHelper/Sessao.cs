using CargaHorariaCRUD.Models;
using CargaHorariaCRUD.WebHelper.Interfaces;
using Newtonsoft.Json;

namespace CargaHorariaCRUD.WebHelper
{
    public class Sessao:ISessao
    {
        private IHttpContextAccessor _httpContext;

        public Sessao(IHttpContextAccessor httpContext) => _httpContext = httpContext;

        public bool IsLogged()
        {
            string? usuarioLogado = _httpContext.HttpContext?.Session.GetString("UsuarioLogado");
            if(usuarioLogado != null)
            {
                return true;
            }
            else return false;
        }

        public void CriarSessao(UsuarioModel usuario)
        {
            string usuarioLogado = JsonConvert.SerializeObject(usuario);
            _httpContext.HttpContext?.Session.SetString("UsuarioLogado", usuarioLogado);

        }

        public void DestruirSessao()
        {
            _httpContext.HttpContext?.Session.Remove("UsuarioLogado");
        }

        public UsuarioModel? GetSessao()
        {
            string? usuarioLogado = _httpContext.HttpContext?.Session.GetString("UsuarioLogado");
            if(usuarioLogado == null)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<UsuarioModel>(usuarioLogado) ?? throw new Exception("Erro ao recuperar sessão");
        }
    }
}
