using System.ComponentModel.DataAnnotations;

namespace CargaHorariaCRUD.Models
{
    public class EnvioFormModel
    {
        [Required(ErrorMessage = "Por favor insira seu email")]
        public string? FormEmail { get; set; }

        [Required(ErrorMessage = "Por favor insira seu professor")]
        public string? FormProf {  get; set; }

        [Required(ErrorMessage = "Por favor insira sua turma")]
        public string? FormTurma { get; set; }

        [Required(ErrorMessage = "Por favor insira o tipo de comprovante")]
        public string? FormTipo { get; set; }

        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "Por favor insira seu comprovante")]
        public IFormFile? FormArquivo { get; set; }

        public string? FormObs {  get; set; }
    }
}
