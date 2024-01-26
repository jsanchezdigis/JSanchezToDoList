using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class JsanchezLaudexContext : DbContext
{
    public JsanchezLaudexContext()
    {
    }

    public JsanchezLaudexContext(DbContextOptions<JsanchezLaudexContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Tarea> Tareas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.; Database= JSanchezLaudex; User ID=sa; TrustServerCertificate=True; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.HasKey(e => e.Idtarea).HasName("PK__TAREA__C61D8E5FAB4215A1");

            entity.ToTable("TAREA");

            entity.Property(e => e.Idtarea).HasColumnName("IDTAREA");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.Estado).HasColumnName("ESTADO");
            entity.Property(e => e.Fechavencimiento).HasColumnName("FECHAVENCIMIENTO");
            entity.Property(e => e.Titulo)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("TITULO");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
