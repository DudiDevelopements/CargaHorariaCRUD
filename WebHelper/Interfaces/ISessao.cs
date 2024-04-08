using CargaHorariaCRUD.Enums;
using CargaHorariaCRUD.Models.Models;

namespace CargaHorariaCRUD.WebHelper.Interfaces {
    public interface ISessao {
        void CriarSessao(UsuarioModel usuario);
        void CriarSessaoAdm(AdmModel adm);
        void DestruirSessao();
        object? GetSessao();
    }
}
