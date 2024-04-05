using System;
using System.Collections.Generic;

namespace CargaHorariaCRUD.Models;

public partial class AdmModel
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly? DataNasc { get; set; }

    public string Senha { get; set; } = null!;
}
