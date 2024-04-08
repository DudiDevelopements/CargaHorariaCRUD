using System.ComponentModel.DataAnnotations;

namespace CargaHorariaCRUD.Models.FormModels {
    public class AdmLoginModel {
        [Required(ErrorMessage = "Por favor insira seu email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Por favor insira sua senha")]
        public string Senha { get; set; } = string.Empty;
    }
}
