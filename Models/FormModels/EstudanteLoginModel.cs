using System.ComponentModel.DataAnnotations;

namespace CargaHorariaCRUD.Models.FormModels {
    public class EstudanteLoginModel {
        [Required(ErrorMessage = "Por favor insira seu CPF")]
        public string CPF { get; set; } = string.Empty;

        [Required(ErrorMessage = "Por favor insira sua data de nascimento")]
        [DataType(DataType.Date)]
        public string? Data_nasc { get; set; }
    }
}
