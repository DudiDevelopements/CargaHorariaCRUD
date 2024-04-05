using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CargaHorariaCRUD.Models;

public partial class UsuarioModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Por favor insira seu CPF")]
    public string Cpf { get; set; } = null!;

    public string Nome { get; set; } = null!;

    [Required(ErrorMessage = "Por favor insira sua data de nascimento")]
    [DataType(DataType.Date)]
    public DateOnly DataNascimento { get; set; }

    public virtual ICollection<EnvioModel> Envios { get; set; } = new List<EnvioModel>();
}
