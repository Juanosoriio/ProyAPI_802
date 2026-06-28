using Microsoft.EntityFrameworkCore;

namespace ProyAPI_802.Models;

public partial class LatiendaContext : DbContext
{
    public LatiendaContext() { }

    public LatiendaContext(DbContextOptions<LatiendaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }
    public virtual DbSet<Producto> Productos { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<UsuarioRole> UsuarioRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK_cat_id_categoria");
            entity.ToTable("categorias");
            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.Nombre).HasMaxLength(100).HasColumnName("nombre");
            entity.Property(e => e.Estado).HasColumnName("estado");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK_pro_id_producto");
            entity.ToTable("productos");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.Nombre).HasMaxLength(150).HasColumnName("nombre");
            entity.Property(e => e.Precio).HasColumnType("decimal(13,2)").HasColumnName("precio");
            entity.Property(e => e.Stock).HasColumnName("stock");
            entity.Property(e => e.Estado).HasColumnName("estado");

            entity.HasOne(d => d.objCategoria)
                .WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK_pro_id_categoria");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK_Usuarios");
            entity.ToTable("usuarios");
            entity.Property(e => e.UsuarioId).HasColumnName("usuarioId");
            entity.Property(e => e.TipoDoc).HasMaxLength(3).HasColumnName("tipoDoc");
            entity.Property(e => e.NroDoc).HasMaxLength(13).HasColumnName("nroDoc");
            entity.Property(e => e.Nombre).HasMaxLength(200).HasColumnName("nombre");
            entity.Property(e => e.Email).HasMaxLength(100).HasColumnName("email");
            entity.Property(e => e.PasswordHash).HasColumnName("PasswordHash");
            entity.Property(e => e.PasswordSalt).HasColumnName("PasswordSalt");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK_Roles");
            entity.ToTable("roles");
            entity.Property(e => e.RolId).HasColumnName("rolId");
            entity.Property(e => e.Nombre).HasMaxLength(50).HasColumnName("nombre");
        });

        modelBuilder.Entity<UsuarioRole>(entity =>
        {
            entity.HasKey(e => e.UsuarioRolId).HasName("PK_UsuarioRoles");
            entity.ToTable("usuarioRoles");
            entity.Property(e => e.UsuarioRolId).HasColumnName("usuarioRolId");
            entity.Property(e => e.UsuarioId).HasColumnName("usuarioId");
            entity.Property(e => e.RolId).HasColumnName("rolId");

            entity.HasOne(d => d.Rol).WithMany(p => p.UsuarioRoles)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK_UsuarioRoles_Roles");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuarioRoles)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_UsuarioRoles_Usuarios");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}