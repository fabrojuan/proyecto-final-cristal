using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MVPSA_V2022.Modelos;

public partial class MVpsaV3Context : DbContext
{
    public MVpsaV3Context()
    {
    }

    public MVpsaV3Context(DbContextOptions<MVpsaV3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Alicuotum> Alicuota { get; set; }

    public virtual DbSet<Boletum> Boleta { get; set; }

    public virtual DbSet<ControlProceso> ControlProcesos { get; set; }

    public virtual DbSet<DatosAbierto> DatosAbiertos { get; set; }

    public virtual DbSet<Denuncium> Denuncia { get; set; }

    public virtual DbSet<Detalleboletum> Detalleboleta { get; set; }

    public virtual DbSet<EstadoDenuncium> EstadoDenuncia { get; set; }

    public virtual DbSet<EstadoReclamo> EstadoReclamos { get; set; }

    public virtual DbSet<EstadoSolicitud> EstadoSolicituds { get; set; }

    public virtual DbSet<Impuestoinmobiliario> Impuestoinmobiliarios { get; set; }

    public virtual DbSet<Lote> Lotes { get; set; }

    public virtual DbSet<MobbexPago> MobbexPagos { get; set; }

    public virtual DbSet<Pagina> Paginas { get; set; }

    public virtual DbSet<Paginaxrol> Paginaxrols { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<ParametriaProceso> ParametriaProcesos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Prioridad> Prioridads { get; set; }

    public virtual DbSet<PrioridadReclamo> PrioridadReclamos { get; set; }

    public virtual DbSet<PruebaGraficaDenuncium> PruebaGraficaDenuncia { get; set; }

    public virtual DbSet<PruebaGraficaReclamo> PruebaGraficaReclamos { get; set; }

    public virtual DbSet<Recibo> Recibos { get; set; }

    public virtual DbSet<Reclamo> Reclamos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Sesione> Sesiones { get; set; }

    public virtual DbSet<Solicitud> Solicituds { get; set; }

    public virtual DbSet<Sugerencium> Sugerencia { get; set; }

    public virtual DbSet<TipoDatoAbierto> TipoDatoAbiertos { get; set; }

    public virtual DbSet<TipoDenuncium> TipoDenuncia { get; set; }

    public virtual DbSet<TipoLote> TipoLotes { get; set; }

    public virtual DbSet<TipoReclamo> TipoReclamos { get; set; }

    public virtual DbSet<TipoSolicitud> TipoSolicituds { get; set; }

    public virtual DbSet<Tipopago> Tipopagos { get; set; }

    public virtual DbSet<Trabajo> Trabajos { get; set; }

    public virtual DbSet<TrabajoReclamo> TrabajoReclamos { get; set; }

    public virtual DbSet<TrabajoSolicitud> TrabajoSolicituds { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioVecino> UsuarioVecinos { get; set; }

    public virtual DbSet<ValuacionInmobiliario> ValuacionInmobiliarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=ROMANS;Initial Catalog=M_VPSA_V3;Integrated Security=True ;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alicuotum>(entity =>
        {
            entity.HasKey(e => e.IdAlicuota).HasName("PK__ALICUOTA__381F0CC3574E5C1C");

            entity.ToTable("ALICUOTA");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(1500)
                .IsUnicode(false);
            entity.Property(e => e.FechaGenerada)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ImporteBase).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<Boletum>(entity =>
        {
            entity.HasKey(e => e.IdBoleta).HasName("PK__BOLETA__362F6EB60C805C1E");

            entity.ToTable("BOLETA");

            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.FechaGenerada)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaPago).HasColumnType("datetime");
            entity.Property(e => e.FechaVencimiento)
                .HasDefaultValueSql("(dateadd(day,(1),getdate()))")
                .HasColumnType("datetime");
            entity.Property(e => e.Importe)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("importe");
            entity.Property(e => e.TipoMoneda)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Url)
                .HasMaxLength(350)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ControlProceso>(entity =>
        {
            entity.HasKey(e => e.IdEjecucion).HasName("PK__CONTROL___C0B117615717B859");

            entity.ToTable("CONTROL_PROCESOS");

            entity.Property(e => e.FechaEjecucion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdProcesoNavigation).WithMany(p => p.ControlProcesos)
                .HasForeignKey(d => d.IdProceso)
                .HasConstraintName("FK__CONTROL_P__IdPro__17F790F9");
        });

        modelBuilder.Entity<DatosAbierto>(entity =>
        {
            entity.HasKey(e => e.IdArchivo).HasName("PK__DATOS_AB__DF161F50BC6B881C");

            entity.ToTable("DATOS_ABIERTOS");

            entity.Property(e => e.IdArchivo).HasColumnName("idArchivo");
            entity.Property(e => e.Bhabilitado)
                .HasDefaultValueSql("((1))")
                .HasColumnName("BHabilitado");
            entity.Property(e => e.Extension)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.NombreArchivo)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Ubicacion).HasColumnType("text");

            entity.HasOne(d => d.IdTipoDatoNavigation).WithMany(p => p.DatosAbiertos)
                .HasForeignKey(d => d.IdTipoDato)
                .HasConstraintName("FK_TipoDatoAbierto");
        });

        modelBuilder.Entity<Denuncium>(entity =>
        {
            entity.HasKey(e => e.NroDenuncia).HasName("PK__DENUNCIA__D763C1F9741D664A");

            entity.ToTable("DENUNCIA");

            entity.Property(e => e.NroDenuncia).HasColumnName("Nro_Denuncia");
            entity.Property(e => e.Altura)
                .HasMaxLength(18)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoInfractor)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("Apellido_Infractor");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.Calle)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CodEstadoDenuncia).HasColumnName("Cod_Estado_Denuncia");
            entity.Property(e => e.CodTipoDenuncia).HasColumnName("Cod_Tipo_Denuncia");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.EntreCalles)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Entre_Calles");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MailNotificacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Mail_Notificacion");
            entity.Property(e => e.MovilNotificacion)
                .HasMaxLength(18)
                .IsUnicode(false)
                .HasColumnName("Movil_Notificacion");
            entity.Property(e => e.NombreInfractor)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("Nombre_Infractor");
            entity.Property(e => e.NroPrioridad).HasColumnName("Nro_Prioridad");

            entity.HasOne(d => d.CodEstadoDenunciaNavigation).WithMany(p => p.Denuncia)
                .HasForeignKey(d => d.CodEstadoDenuncia)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__DENUNCIA__Cod_Es__19DFD96B");

            entity.HasOne(d => d.CodTipoDenunciaNavigation).WithMany(p => p.Denuncia)
                .HasForeignKey(d => d.CodTipoDenuncia)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__DENUNCIA__Cod_Ti__1AD3FDA4");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Denuncia)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__DENUNCIA__IdUsua__1BC821DD");

            entity.HasOne(d => d.NroPrioridadNavigation).WithMany(p => p.Denuncia)
                .HasForeignKey(d => d.NroPrioridad)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Prioridad");
        });

        modelBuilder.Entity<Detalleboletum>(entity =>
        {
            entity.HasKey(e => e.IdDetalleBoleta).HasName("PK__DETALLEB__70845C443627DD0D");

            entity.ToTable("DETALLEBOLETA");

            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.Importe).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<EstadoDenuncium>(entity =>
        {
            entity.HasKey(e => e.CodEstadoDenuncia).HasName("PK__ESTADO_D__FA18336DEE5CFB6A");

            entity.ToTable("ESTADO_DENUNCIA");

            entity.Property(e => e.CodEstadoDenuncia).HasColumnName("Cod_Estado_Denuncia");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstadoReclamo>(entity =>
        {
            entity.HasKey(e => e.CodEstadoReclamo).HasName("PK__ESTADO_R__40DB848001696C73");

            entity.ToTable("ESTADO_RECLAMO");

            entity.Property(e => e.CodEstadoReclamo).HasColumnName("Cod_Estado_Reclamo");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstadoSolicitud>(entity =>
        {
            entity.HasKey(e => e.CodEstadoSolicitud).HasName("PK__ESTADO_S__AB5EC4AF40D49A37");

            entity.ToTable("ESTADO_SOLICITUD");

            entity.Property(e => e.CodEstadoSolicitud).HasColumnName("Cod_Estado_Solicitud");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Impuestoinmobiliario>(entity =>
        {
            entity.HasKey(e => e.IdImpuesto).HasName("PK__IMPUESTO__A9B889285C1B2522");

            entity.ToTable("IMPUESTOINMOBILIARIO");

            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.FechaEmision).HasColumnType("datetime");
            entity.Property(e => e.FechaVencimiento).HasColumnType("datetime");
            entity.Property(e => e.ImporteBase).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ImporteFinal).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InteresValor).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdLoteNavigation).WithMany(p => p.Impuestoinmobiliarios)
                .HasForeignKey(d => d.IdLote)
                .HasConstraintName("FK__IMPUESTOI__IdLot__1DB06A4F");
        });

        modelBuilder.Entity<Lote>(entity =>
        {
            entity.HasKey(e => e.IdLote).HasName("PK__LOTE__38C4EE90831996C5");

            entity.ToTable("LOTE");

            entity.Property(e => e.Altura)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BaseImponible).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.Calle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaAlta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Alta");
            entity.Property(e => e.IdPersona).HasColumnName("idPersona");
            entity.Property(e => e.IdTipoLote).HasColumnName("Id_Tipo_Lote");
            entity.Property(e => e.NomenclaturaCatastral)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.SupEdificada).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.SupTerreno).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ValuacionTotal).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Lotes)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__LOTE__idPersona__1EA48E88");

            entity.HasOne(d => d.IdTipoLoteNavigation).WithMany(p => p.Lotes)
                .HasForeignKey(d => d.IdTipoLote)
                .HasConstraintName("FK_TipoFrLote");
        });

        modelBuilder.Entity<MobbexPago>(entity =>
        {
            entity.HasKey(e => e.IdMobbexPago);

            entity.ToTable("MOBBEX_PAGO");

            entity.Property(e => e.IdMobbexPago).HasColumnName("id_mobbex_pago");
            entity.Property(e => e.CheckoutUid)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("checkout_uid");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("customer_email");
            entity.Property(e => e.CustomerUid)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("customer_uid");
            entity.Property(e => e.FechaAlta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_alta");
            entity.Property(e => e.PaymentCreated)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("payment_created");
            entity.Property(e => e.PaymentCurrencyCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("payment_currency_code");
            entity.Property(e => e.PaymentCurrencySymbol)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("payment_currency_symbol");
            entity.Property(e => e.PaymentId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("payment_id");
            entity.Property(e => e.PaymentReference)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("payment_reference");
            entity.Property(e => e.PaymentStatusCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("payment_status_code");
            entity.Property(e => e.PaymentStatusMessage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("payment_status_message");
            entity.Property(e => e.PaymentStatusText)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("payment_status_text");
            entity.Property(e => e.PaymentTotal)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("payment_total");
            entity.Property(e => e.PaymentUpdated)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("payment_updated");
            entity.Property(e => e.Type)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("type");
            entity.Property(e => e.ViewType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("view_type");
        });

        modelBuilder.Entity<Pagina>(entity =>
        {
            entity.HasKey(e => e.IdPagina).HasName("PK__PAGINA__034F88B8B1CE4DC2");

            entity.ToTable("PAGINA");

            entity.Property(e => e.Accion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Bvisible).HasColumnName("BVisible");
            entity.Property(e => e.Mensaje)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Paginaxrol>(entity =>
        {
            entity.HasKey(e => e.IdPaginaxRol).HasName("PK__PAGINAXR__6E5913D8E3F1724E");

            entity.ToTable("PAGINAXROL");

            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.IdRol).HasColumnName("idRol");

            entity.HasOne(d => d.IdPaginaNavigation).WithMany(p => p.Paginaxrols)
                .HasForeignKey(d => d.IdPagina)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__PAGINAXRO__IdPag__208CD6FA");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Paginaxrols)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__PAGINAXRO__idRol__2180FB33");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PK__PAGO__FC851A3A853657A8");

            entity.ToTable("PAGO");

            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.FechaDePago)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Importe).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdTipoPagoNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdTipoPago)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__PAGO__IdTipoPago__22751F6C");
        });

        modelBuilder.Entity<ParametriaProceso>(entity =>
        {
            entity.HasKey(e => e.IdProceso).HasName("PK__PARAMETR__036D074330271BD2");

            entity.ToTable("PARAMETRIA_PROCESOS");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Periodicidad)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__PERSONA__A47881419B6CE7AE");

            entity.ToTable("PERSONA");

            entity.HasIndex(e => e.Mail, "UQ__PERSONA__2724B2D1C50ABE88").IsUnique();

            entity.Property(e => e.IdPersona).HasColumnName("idPersona");
            entity.Property(e => e.Altura)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Apellido)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.BtieneUser).HasColumnName("BTieneUser");
            entity.Property(e => e.Cuil)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.Dni)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Domicilio)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaNac).HasColumnType("date");
            entity.Property(e => e.Mail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Prioridad>(entity =>
        {
            entity.HasKey(e => e.NroPrioridad).HasName("PK__PRIORIDA__07C3E6AECD7B0B12");

            entity.ToTable("Prioridad");

            entity.Property(e => e.NroPrioridad).HasColumnName("Nro_Prioridad");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.NombrePrioridad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre_Prioridad");
        });

        modelBuilder.Entity<PrioridadReclamo>(entity =>
        {
            entity.HasKey(e => e.NroPrioridad).HasName("PK__PRIORIDA__07C3E6AEAF767DB2");

            entity.ToTable("Prioridad_RECLAMO");

            entity.Property(e => e.NroPrioridad).HasColumnName("Nro_Prioridad");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.NombrePrioridad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre_Prioridad");
        });

        modelBuilder.Entity<PruebaGraficaDenuncium>(entity =>
        {
            entity.HasKey(e => e.NroImagen).HasName("PK__PRUEBA_G__B8690AD8910DD918");

            entity.ToTable("PRUEBA_GRAFICA_DENUNCIA");

            entity.Property(e => e.NroImagen).HasColumnName("Nro_Imagen");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.Foto)
                .IsUnicode(false)
                .HasColumnName("foto");
            entity.Property(e => e.NroDenuncia).HasColumnName("Nro_Denuncia");
            entity.Property(e => e.NroTrabajo)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Nro_Trabajo");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.PruebaGraficaDenuncia)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__PRUEBA_GR__IdUsu__236943A5");
        });

        modelBuilder.Entity<PruebaGraficaReclamo>(entity =>
        {
            entity.HasKey(e => e.NroImagen).HasName("PK__PRUEBA_G__B8690AD8A8F91D33");

            entity.ToTable("PRUEBA_GRAFICA_RECLAMO");

            entity.Property(e => e.NroImagen).HasColumnName("Nro_Imagen");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.Foto)
                .IsUnicode(false)
                .HasColumnName("foto");
            entity.Property(e => e.NroReclamo).HasColumnName("Nro_Reclamo");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.PruebaGraficaReclamos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__PRUEBA_GR__IdUsu__245D67DE");

            //entity.HasOne(d => d.IdVecinoNavigation).WithMany(p => p.PruebaGraficaReclamos)
            //    .HasForeignKey(d => d.IdVecino)
            //    .HasConstraintName("FK__PRUEBA_GR__IdVec__25518C17");

            entity.HasOne(d => d.NroReclamoNavigation).WithMany(p => p.PruebaGraficaReclamos)
                .HasForeignKey(d => d.NroReclamo)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__PRUEBA_GR__Nro_R__2645B050");
        });

        modelBuilder.Entity<Recibo>(entity =>
        {
            entity.HasKey(e => e.IdRecibo).HasName("PK__RECIBO__2FEC4731E1757D27");

            entity.ToTable("RECIBO");

            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.FechaPago)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Importe).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Mail)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.IdBoletaNavigation).WithMany(p => p.Recibos)
                .HasForeignKey(d => d.IdBoleta)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__RECIBO__IdBoleta__2739D489");
        });

        modelBuilder.Entity<Reclamo>(entity =>
        {
            entity.HasKey(e => e.NroReclamo).HasName("PK__RECLAMO__2D3C904FDDC9B31D");

            entity.ToTable("RECLAMO");

            entity.Property(e => e.NroReclamo).HasColumnName("Nro_Reclamo");
            entity.Property(e => e.Altura)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.Calle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CodEstadoReclamo).HasColumnName("Cod_Estado_Reclamo");
            entity.Property(e => e.CodTipoReclamo).HasColumnName("Cod_Tipo_Reclamo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.EntreCalles)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CodEstadoReclamoNavigation).WithMany(p => p.Reclamos)
                .HasForeignKey(d => d.CodEstadoReclamo)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__RECLAMO__Cod_Est__282DF8C2");

            entity.HasOne(d => d.CodTipoReclamoNavigation).WithMany(p => p.Reclamos)
                .HasForeignKey(d => d.CodTipoReclamo)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__RECLAMO__Cod_Tip__29221CFB");

            //entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Reclamos)
            //    .HasForeignKey(d => d.IdUsuario)
            //    .HasConstraintName("FK__RECLAMO__IdUsuar__2A164134");

            //entity.HasOne(d => d.IdVecinoNavigation).WithMany(p => p.Reclamos)
            //    .HasForeignKey(d => d.IdVecino)
            //    .OnDelete(DeleteBehavior.Cascade)
            //    .HasConstraintName("FK__RECLAMO__IdVecin__2B0A656D");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__ROL__3C872F76470A219B");

            entity.ToTable("ROL");

            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Sesione>(entity =>
        {
            entity.HasKey(e => e.IdSesion).HasName("PK__SESIONES__22EC535BA6ACEC11");

            entity.ToTable("SESIONES");

            entity.Property(e => e.IdSesion)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Maquina)
                .HasMaxLength(50)
                .IsUnicode(false);


        });

        modelBuilder.Entity<Solicitud>(entity =>
        {
            entity.HasKey(e => e.NroSolicitud).HasName("PK__SOLICITU__CB3EA8FA5D1AFD10");

            entity.ToTable("SOLICITUD");

            entity.Property(e => e.NroSolicitud).HasColumnName("Nro_Solicitud");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.CodEstadoSolicitud).HasColumnName("Cod_Estado_Solicitud");
            entity.Property(e => e.CodTipoSolicitud).HasColumnName("Cod_Tipo_Solicitud");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("date");

            entity.HasOne(d => d.CodEstadoSolicitudNavigation).WithMany(p => p.Solicituds)
                .HasForeignKey(d => d.CodEstadoSolicitud)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__SOLICITUD__Cod_E__2CF2ADDF");

            entity.HasOne(d => d.CodTipoSolicitudNavigation).WithMany(p => p.Solicituds)
                .HasForeignKey(d => d.CodTipoSolicitud)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__SOLICITUD__Cod_T__2DE6D218");

            //entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Solicituds)
            //    .HasForeignKey(d => d.IdUsuario)
            //    .HasConstraintName("FK__SOLICITUD__IdUsu__2EDAF651");

        //    entity.HasOne(d => d.IdVecinoNavigation).WithMany(p => p.Solicituds)
        //        .HasForeignKey(d => d.IdVecino)
        //        .OnDelete(DeleteBehavior.Cascade)
        //        .HasConstraintName("FK__SOLICITUD__IdVec__2FCF1A8A");
        });

        modelBuilder.Entity<Sugerencium>(entity =>
        {
            entity.HasKey(e => e.IdSugerencia).HasName("PK__SUGERENC__A4668EAA7BE11EDE");

            entity.ToTable("SUGERENCIA");

            entity.Property(e => e.IdSugerencia).HasColumnName("idSugerencia");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(1500)
                .IsUnicode(false);
            entity.Property(e => e.FechaGenerada)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
        });

        modelBuilder.Entity<TipoDatoAbierto>(entity =>
        {
            entity.HasKey(e => e.IdTipoDato).HasName("PK__TIPO_DAT__C8C67CEB4DF68502");

            entity.ToTable("TIPO_DATO_ABIERTO");

            entity.Property(e => e.IdTipoDato).HasColumnName("idTipoDato");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(90)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoDenuncium>(entity =>
        {
            entity.HasKey(e => e.CodTipoDenuncia).HasName("PK__TIPO_DEN__86CD78F63BEF01CB");

            entity.ToTable("TIPO_DENUNCIA");

            entity.Property(e => e.CodTipoDenuncia).HasColumnName("Cod_Tipo_Denuncia");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.FechaAlta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(90)
                .IsUnicode(false);
            entity.Property(e => e.TiempoMaxTratamiento).HasColumnName("Tiempo_Max_Tratamiento");
        });

        modelBuilder.Entity<TipoLote>(entity =>
        {
            entity.HasKey(e => e.CodTipoLote).HasName("PK__TIPO_LOT__B769E6D064CDB437");

            entity.ToTable("TIPO_LOTE");

            entity.Property(e => e.CodTipoLote).HasColumnName("Cod_Tipo_Lote");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(90)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoReclamo>(entity =>
        {
            entity.HasKey(e => e.CodTipoReclamo).HasName("PK__TIPO_REC__116232333626A69A");

            entity.ToTable("TIPO_RECLAMO");

            entity.Property(e => e.CodTipoReclamo).HasColumnName("Cod_Tipo_Reclamo");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.FechaAlta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(90)
                .IsUnicode(false);
            entity.Property(e => e.TiempoMaxTratamiento).HasColumnName("Tiempo_Max_Tratamiento");
        });

        modelBuilder.Entity<TipoSolicitud>(entity =>
        {
            entity.HasKey(e => e.CodTipoSolicitud).HasName("PK__TIPO_SOL__6B3944959A186AF2");

            entity.ToTable("TIPO_SOLICITUD");

            entity.Property(e => e.CodTipoSolicitud).HasColumnName("Cod_Tipo_Solicitud");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(90)
                .IsUnicode(false);
            entity.Property(e => e.TiempoMaxTratamiento).HasColumnName("Tiempo_Max_Tratamiento");
        });

        modelBuilder.Entity<Tipopago>(entity =>
        {
            entity.HasKey(e => e.IdTipoPago).HasName("PK__TIPOPAGO__EB0AA9E7B7CCC8EE");

            entity.ToTable("TIPOPAGO");

            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Trabajo>(entity =>
        {
            entity.HasKey(e => e.NroTrabajo).HasName("PK__TRABAJO__87806897ED904074");

            entity.ToTable("TRABAJO");

            entity.Property(e => e.NroTrabajo).HasColumnName("Nro_trabajo");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NroDenuncia).HasColumnName("Nro_Denuncia");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Trabajos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__TRABAJO__IdUsuar__30C33EC3");
        });

        modelBuilder.Entity<TrabajoReclamo>(entity =>
        {
            entity.HasKey(e => e.NroTrabajo).HasName("PK__TRABAJO___87806897B312A587");

            entity.ToTable("TRABAJO_RECLAMO");

            entity.Property(e => e.NroTrabajo).HasColumnName("Nro_trabajo");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(2500)
                .IsUnicode(false);
            //entity.Property(e => e.Fecha)
            //    .HasDefaultValueSql("(getdate())")
            //    .HasColumnType("datetime");
            //entity.Property(e => e.NroReclamo).HasColumnName("Nro_Reclamo");

            //entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.TrabajoReclamos)
            //    .HasForeignKey(d => d.IdUsuario)
            //    .HasConstraintName("FK__TRABAJO_R__IdUsu__31B762FC");

            //entity.HasOne(d => d.IdVecinoNavigation).WithMany(p => p.TrabajoReclamos)
            //    .HasForeignKey(d => d.IdVecino)
            //    .HasConstraintName("FK__TRABAJO_R__IdVec__32AB8735");

            entity.HasOne(d => d.NroReclamoNavigation).WithMany(p => p.TrabajoReclamos)
                .HasForeignKey(d => d.NroReclamo)
                .HasConstraintName("FK__TRABAJO_R__Nro_R__339FAB6E");
        });

        modelBuilder.Entity<TrabajoSolicitud>(entity =>
        {
            entity.HasKey(e => e.NroTrabajo).HasName("PK__TRABAJO___878068979C304396");

            entity.ToTable("TRABAJO_SOLICITUD");

            entity.Property(e => e.NroTrabajo).HasColumnName("Nro_trabajo");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NroSolicitud).HasColumnName("Nro_Solicitud");

            //entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.TrabajoSolicituds)
            //    .HasForeignKey(d => d.IdUsuario)
            //    .HasConstraintName("FK__TRABAJO_S__IdUsu__3493CFA7");

            //entity.HasOne(d => d.IdVecinoNavigation).WithMany(p => p.TrabajoSolicituds)
            //    .HasForeignKey(d => d.IdVecino)
            //    .HasConstraintName("FK__TRABAJO_S__IdVec__3587F3E0");

            entity.HasOne(d => d.NroSolicitudNavigation).WithMany(p => p.TrabajoSolicituds)
                .HasForeignKey(d => d.NroSolicitud)
                .HasConstraintName("FK__TRABAJO_S__Nro_S__367C1819");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__USUARIO__645723A6264F50CD");

            entity.ToTable("USUARIO");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.Contrasenia)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaAlta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.FechaBaja).HasColumnType("date");
            entity.Property(e => e.IdPersona).HasColumnName("idPersona");
            entity.Property(e => e.IdTipoUsuario).HasColumnName("idTipoUsuario");
            entity.Property(e => e.NombreUser)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__USUARIO__idPerso__37703C52");

            entity.HasOne(d => d.IdTipoUsuarioNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdTipoUsuario)
                .HasConstraintName("FK__USUARIO__idTipoU__3864608B");
        });

        modelBuilder.Entity<UsuarioVecino>(entity =>
        {
            entity.HasKey(e => e.IdVecino).HasName("PK__USUARIO___E7C50E6935FE4585");

            entity.ToTable("USUARIO_VECINO");

            entity.Property(e => e.IdVecino).HasColumnName("idVecino");
            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.Contrasenia)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaAlta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.FechaBaja).HasColumnType("date");
            entity.Property(e => e.IdPersona).HasColumnName("idPersona");
            entity.Property(e => e.NombreUser)
                .HasMaxLength(100)
                .IsUnicode(false);

            //entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.UsuarioVecinos)
            //    .HasForeignKey(d => d.IdPersona)
            //    .OnDelete(DeleteBehavior.Cascade)
            //    .HasConstraintName("FK__USUARIO_V__idPer__395884C4");
        });

        modelBuilder.Entity<ValuacionInmobiliario>(entity =>
        {
            entity.HasKey(e => e.Idvaluacion).HasName("PK__VALUACIO__5430C5996C73A612");

            entity.ToTable("VALUACION_INMOBILIARIO");

            entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");
            entity.Property(e => e.FechaDesde)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ValorSupEdificada).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ValorSupTerreno).HasColumnType("decimal(18, 0)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
