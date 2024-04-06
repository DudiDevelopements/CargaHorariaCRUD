using System;
using System.Collections.Generic;
using CargaHorariaCRUD.Models.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace CargaHorariaCRUD.WebHelper;

public partial class UsuariosIfmsContext : DbContext
{
    public UsuariosIfmsContext()
    {
    }

    public UsuariosIfmsContext(DbContextOptions<UsuariosIfmsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdmModel> Administradores { get; set; }

    public virtual DbSet<EnvioModel> Envios { get; set; }

    public virtual DbSet<UsuarioModel> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=localhost;user=root;database=usuarios_ifms", ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<AdmModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("administradores");

            entity.Property(e => e.Id)
                .HasColumnType("int(30)")
                .HasColumnName("id");
            entity.Property(e => e.DataNasc).HasColumnName("data_nasc");
            entity.Property(e => e.Email)
                .HasMaxLength(512)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.Senha)
                .HasMaxLength(512)
                .HasColumnName("senha");
        });

        modelBuilder.Entity<EnvioModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("envios");

            entity.HasIndex(e => e.IdAluno, "id_aluno");

            entity.Property(e => e.Id)
                .HasColumnType("int(255)")
                .HasColumnName("id");
            entity.Property(e => e.CargaHoraria)
                .HasColumnType("int(11)")
                .HasColumnName("carga_horaria");
            entity.Property(e => e.Email)
                .HasMaxLength(512)
                .HasColumnName("email");
            entity.Property(e => e.HorarioEnviado)
                .HasDefaultValueSql("current_timestamp(6)")
                .HasColumnType("timestamp(6)")
                .HasColumnName("horario_enviado");
            entity.Property(e => e.IdAluno)
                .HasColumnType("int(255)")
                .HasColumnName("id_aluno");
            entity.Property(e => e.Obs)
                .HasMaxLength(512)
                .HasColumnName("obs");
            entity.Property(e => e.Path)
                .HasMaxLength(2048)
                .HasColumnName("path");
            entity.Property(e => e.Prof)
                .HasMaxLength(512)
                .HasColumnName("prof");
            entity.Property(e => e.Tipo)
                .HasMaxLength(120)
                .HasColumnName("tipo");
            entity.Property(e => e.Turma)
                .HasMaxLength(30)
                .HasColumnName("turma");
            entity.Property(e => e.Validado).HasColumnName("validado");

            entity.HasOne(d => d.IdAlunoNavigation).WithMany(p => p.Envios)
                .HasForeignKey(d => d.IdAluno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("envios_ibfk_1");
        });

        modelBuilder.Entity<UsuarioModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.Property(e => e.Id)
                .HasColumnType("int(30)")
                .HasColumnName("id");
            entity.Property(e => e.Cpf)
                .HasMaxLength(20)
                .HasColumnName("cpf");
            entity.Property(e => e.DataNascimento).HasColumnName("data_nascimento");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
