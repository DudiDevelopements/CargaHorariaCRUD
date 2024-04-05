using CargaHorariaCRUD.Models;

namespace CargaHorariaCRUD.WebHelper.Interfaces
{
    public interface ISessao
    {
        void CriarSessao(UsuarioModel usuario);
        void DestruirSessao();
        UsuarioModel? GetSessao();
        bool IsLogged();
    }
}
