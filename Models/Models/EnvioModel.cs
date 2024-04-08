using System;
using System.Collections.Generic;

namespace CargaHorariaCRUD.Models.Models;

public partial class EnvioModel {
    public EnvioModel(int idAluno, string? email, string? turma, string? prof, string? tipo, string? obs, string? path, DateTime horarioEnviado) {
        IdAluno = idAluno;
        Email = email;
        Turma = turma;
        Prof = prof;
        Tipo = tipo;
        Obs = obs;
        Path = path;
        HorarioEnviado = horarioEnviado;
    }

    public int Id { get; set; }

    public int IdAluno { get; set; }

    public string Email { get; set; } = null!;

    public string Turma { get; set; } = null!;

    public string? Prof { get; set; }

    public string? Tipo { get; set; }

    public string? Obs { get; set; }

    public string Path { get; set; } = null!;

    public bool Validado { get; set; }

    public DateTime HorarioEnviado { get; set; }

    public int? CargaHoraria { get; set; }

    public virtual UsuarioModel IdAlunoNavigation { get; set; } = null!;

    public string? CargaHorariaEmHorasF() {
        if(CargaHoraria.HasValue) {
            float horas = (float)CargaHoraria / 60;
            return horas.ToString("F1");
        } else
            return null;
    }
}
