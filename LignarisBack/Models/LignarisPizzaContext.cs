using System;
using System.Collections.Generic;
using LignarisBack.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LignarisBack.Models;

public partial class LignarisPizzaContext : IdentityDbContext<AppUser>
{
    public LignarisPizzaContext(DbContextOptions<LignarisPizzaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<CompraDetalle> CompraDetalles { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<MateriaPrima> MateriaPrimas { get; set; }

    public virtual DbSet<MateriaProveedorIntermedium> MateriaProveedorIntermedia { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Produccion> Produccions { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<RecetaDetalle> RecetaDetalles { get; set; }

    public virtual DbSet<Recetum> Receta { get; set; }

    public virtual DbSet<RegistroSesione> RegistroSesiones { get; set; }

    public virtual DbSet<AppUser> AppUser { get; set; }

    public virtual DbSet<VentaDetalle> VentaDetalles { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    public virtual DbSet<CarritoCompras> CarritoCompras { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__cliente__677F38F5A3993B3C");

            entity.ToTable("cliente");

            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdPersona).HasColumnName("id_persona");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_id_persona_Cliente");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_id_usuario_Cliente");

            entity.HasMany(car => car.CarritoCompras)
            .WithOne(rec => rec.Cliente)
            .HasForeignKey(rec => rec.IdCliente)
            .OnDelete(DeleteBehavior.Cascade);

        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.IdCompra).HasName("PK__compra__C4BAA6044664E5EF");

            entity.ToTable("compra");

            entity.Property(e => e.IdCompra).HasColumnName("id_compra");
            entity.Property(e => e.FechaCompra).HasColumnName("fecha_compra");
            entity.Property(e => e.IdEmpleado).HasColumnName("Id_empleado");
            entity.Property(e => e.IdProveedor).HasColumnName("Id_proveedor");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_empleado_compra");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_proveedor_compra");
        });

        modelBuilder.Entity<CompraDetalle>(entity =>
        {
            entity.HasKey(e => e.IdCompraDetalle).HasName("PK__compra_d__C08AA0066404E084");

            entity.ToTable("compra_detalle");

            entity.Property(e => e.IdCompraDetalle).HasColumnName("id_compra_detalle");
            entity.Property(e => e.Cantidad)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("cantidad");
            entity.Property(e => e.FechaCaducidad).HasColumnName("fecha_caducidad");
            entity.Property(e => e.IdCompra).HasColumnName("id_compra");
            entity.Property(e => e.IdMateriaPrima).HasColumnName("id_materia_prima");
            entity.Property(e => e.NumLote)
                .HasMaxLength(255)
                .HasColumnName("num_lote");
            entity.Property(e => e.PrecioUnitario).HasColumnName("precio_unitario");

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.CompraDetalles)
                .HasForeignKey(d => d.IdCompra)
                .HasConstraintName("fk_id_compra_detalle");

            entity.HasOne(d => d.IdMateriaPrimaNavigation).WithMany(p => p.CompraDetalles)
                .HasForeignKey(d => d.IdMateriaPrima)
                .HasConstraintName("fk_id_detalle_compra_materia_prima");
        });

        modelBuilder.Entity<CarritoCompras>(entity =>
        {
            entity.HasKey(e => e.IdCarritoCompras).HasName("PK__cliente__677F38F5A3993B9J");

            entity.ToTable("carritocompras");

            entity.Property(e => e.IdCarritoCompras).HasColumnName("id_carritocompras");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");


            entity.HasOne(rec => rec.Cliente)
            .WithMany(car => car.CarritoCompras)
            .HasForeignKey(rec => rec.IdCliente)
            .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(rec => rec.Recetum)
            .WithMany(car => car.CarritoCompras)
            .HasForeignKey(rec => rec.IdRecetas)
            .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__empleado__88B513943E6A5F08");

            entity.ToTable("empleado");

            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");
            entity.Property(e => e.IdPersona).HasColumnName("id_persona");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Puesto)
                .HasMaxLength(255)
                .HasColumnName("puesto");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_id_persona_empleado");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_id_usuario_empleado");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.IdInventario).HasName("PK__inventar__013AEB511138B038");

            entity.ToTable("inventario");

            entity.Property(e => e.IdInventario).HasColumnName("id_inventario");
            entity.Property(e => e.CantidadDisponible)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("cantidad_disponible");
            entity.Property(e => e.Estatus)
                .HasDefaultValue(0)
                .HasColumnName("estatus");
            entity.Property(e => e.IdCompraDetalle).HasColumnName("id_compra_detalle");

            entity.HasOne(d => d.IdCompraDetalleNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.IdCompraDetalle)
                .HasConstraintName("fk_id_compra_detalle_inventario");
        });

        modelBuilder.Entity<MateriaPrima>(entity =>
        {
            entity.HasKey(e => e.IdMateriaPrima).HasName("PK__materia___1BCDA74B50B9D580");

            entity.ToTable("materia_prima");

            entity.Property(e => e.IdMateriaPrima).HasColumnName("id_materia_prima");
            entity.Property(e => e.CantidadMinima)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("cantidad_minima");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.TipoMedida)
                .HasMaxLength(10)
                .HasColumnName("tipo_medida");
        });

        modelBuilder.Entity<MateriaProveedorIntermedium>(entity =>
        {
            entity.HasKey(e => e.IdMateriaProveedor).HasName("PK__materia___C380E4DD145ECA3E");

            entity.ToTable("materia_proveedor_intermedia");

            entity.Property(e => e.IdMateriaProveedor).HasColumnName("id_materia_proveedor");
            entity.Property(e => e.IdMateriaPrima).HasColumnName("id_materia_prima");
            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");

            entity.HasOne(d => d.IdMateriaPrimaNavigation).WithMany(p => p.MateriaProveedorIntermedia)
                .HasForeignKey(d => d.IdMateriaPrima)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_id_materia_prima");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.MateriaProveedorIntermedia)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_id_proveedor");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__persona__228148B05E9E6BC2");

            entity.ToTable("persona");

            entity.Property(e => e.IdPersona).HasColumnName("id_persona");
            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(255)
                .HasColumnName("apellido_materno");
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(255)
                .HasColumnName("apellido_paterno");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(255)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Produccion>(entity =>
        {
            entity.HasKey(e => e.IdProduccion).HasName("PK__producci__9EBBA4330F7A26BC");

            entity.ToTable("produccion");

            entity.Property(e => e.IdProduccion).HasColumnName("id_produccion");
            entity.Property(e => e.FechaProduccion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_produccion");
            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");
            entity.Property(e => e.IdVenta).HasColumnName("id_venta");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Produccions)
                .HasForeignKey(d => d.IdEmpleado)
                .HasConstraintName("fk_id_empleado_produccion");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.Produccions)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("fk_id_solicitud_produccion");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__proveedo__8D3DFE289762FB9F");

            entity.ToTable("proveedor");

            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");
            entity.Property(e => e.Estatus)
                .HasDefaultValue(0)
                .HasColumnName("estatus");
            entity.Property(e => e.IdPersona).HasColumnName("id_persona");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Proveedors)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_id_persona_proveedor");
        });

        modelBuilder.Entity<RecetaDetalle>(entity =>
        {
            entity.HasKey(e => e.IdRecetaDetalle).HasName("PK__receta_d__F085822D7618FBF1");

            entity.ToTable("receta_detalle");

            entity.Property(e => e.IdRecetaDetalle).HasColumnName("id_receta_detalle");
            entity.Property(e => e.Cantidad)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("cantidad");
            entity.Property(e => e.IdMateriaPrima).HasColumnName("id_materia_prima");
            entity.Property(e => e.IdReceta).HasColumnName("id_receta");

            entity.HasOne(d => d.IdMateriaPrimaNavigation).WithMany(p => p.RecetaDetalles)
                .HasForeignKey(d => d.IdMateriaPrima)
                .HasConstraintName("fk_id_materia_prima_receta_detalle");

            entity.HasOne(d => d.IdRecetaNavigation).WithMany(p => p.RecetaDetalles)
                .HasForeignKey(d => d.IdReceta)
                .HasConstraintName("fk_id_receta_receta_detalle");
        });

        modelBuilder.Entity<Recetum>(entity =>
        {
            entity.HasKey(e => e.IdReceta).HasName("PK__receta__11DB53ABB651B1C6");

            entity.ToTable("receta");

            entity.Property(e => e.IdReceta).HasColumnName("id_receta");
            entity.Property(e => e.Estatus)
                .HasDefaultValue(0)
                .HasColumnName("estatus");
            entity.Property(e => e.Foto)
                .HasColumnType("text")
                .HasColumnName("foto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioUnitario).HasColumnName("precio_unitario");
            entity.Property(e => e.Tamanio).HasColumnName("tamanio");

            entity.HasMany(car => car.CarritoCompras)
            .WithOne(rec => rec.Recetum)
            .HasForeignKey(rec => rec.IdRecetas)
            .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<RegistroSesione>(entity =>
        {
            entity.HasKey(e => e.IdRegistro).HasName("PK__registro__48155C1F728AC443");

            entity.ToTable("registro_sesiones");

            entity.Property(e => e.IdRegistro).HasColumnName("id_registro");
            entity.Property(e => e.EstatusSesion).HasColumnName("estatus_sesion");
            entity.Property(e => e.FechaHoraAccion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_accion");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.RegistroSesiones)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("fk_id_usuario_registro");
        });

        modelBuilder.Entity<VentaDetalle>(entity =>
        {
            entity.HasKey(e => e.IdVentaDetalle).HasName("PK__venta_de__7AA8F41B68D795FB");

            entity.ToTable("venta_detalle");

            entity.Property(e => e.IdVentaDetalle).HasColumnName("id_venta_detalle");
            entity.Property(e => e.Cantidad)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("cantidad");
            entity.Property(e => e.IdReceta).HasColumnName("id_receta");
            entity.Property(e => e.IdVenta).HasColumnName("id_venta");

            entity.HasOne(d => d.IdRecetaNavigation).WithMany(p => p.VentaDetalles)
                .HasForeignKey(d => d.IdReceta)
                .HasConstraintName("fk_id_receta");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.VentaDetalles)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("fK_id_venta_detalle");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__venta__459533BF9A62FD80");

            entity.ToTable("venta");

            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.FechaVenta).HasColumnName("fecha_venta");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");
            entity.Property(e => e.Total).HasColumnName("total");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("fk_id_cliente_compra");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdEmpleado)
                .HasConstraintName("fk_id_empleado_venta");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
