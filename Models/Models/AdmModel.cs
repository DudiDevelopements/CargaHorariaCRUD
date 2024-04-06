using System;
using System.Collections.Generic;

namespace CargaHorariaCRUD.Models.Models;

public partial class AdmModel
{
    public AdmModel(int id,string nome,string email,DateOnly? dataNasc)
    {
        Id = id;
        Nome = nome;
        Email = email;
        DataNasc = dataNasc;
    }

    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly? DataNasc { get; set; }

    public string Senha { get; set; } = null!;
}
