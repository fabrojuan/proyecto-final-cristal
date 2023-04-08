using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MVPSA_V2022.Modelos
{
    public partial class M_VPSA_V3Context : DbContext
    {
        public M_VPSA_V3Context()
        {
        }

        public M_VPSA_V3Context(DbContextOptions<M_VPSA_V3Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Alicuotum> Alicuota { get; set; } = null!;
        public virtual DbSet<Boletum> Boleta { get; set; } = null!;
        public virtual DbSet<ControlProceso> ControlProcesos { get; set; } = null!;
        public virtual DbSet<DatosAbierto> DatosAbiertos { get; set; } = null!;
        public virtual DbSet<Denuncium> Denuncia { get; set; } = null!;
        public virtual DbSet<Detalleboletum> Detalleboleta { get; set; } = null!;
        public virtual DbSet<EstadoDenuncium> EstadoDenuncia { get; set; } = null!;
        public virtual DbSet<EstadoReclamo> EstadoReclamos { get; set; } = null!;
        public virtual DbSet<EstadoSolicitud> EstadoSolicituds { get; set; } = null!;
        public virtual DbSet<Impuestoinmobiliario> Impuestoinmobiliarios { get; set; } = null!;
        public virtual DbSet<Lote> Lotes { get; set; } = null!;
        public virtual DbSet<MobbexPago> MobbexPagos { get; set; } = null!;
        public virtual DbSet<Pagina> Paginas { get; set; } = null!;
        public virtual DbSet<Paginaxrol> Paginaxrols { get; set; } = null!;
        public virtual DbSet<Pago> Pagos { get; set; } = null!;
        public virtual DbSet<ParametriaProceso> ParametriaProcesos { get; set; } = null!;
        public virtual DbSet<Persona> Personas { get; set; } = null!;
        public virtual DbSet<Prioridad> Prioridads { get; set; } = null!;
        public virtual DbSet<PrioridadReclamo> PrioridadReclamos { get; set; } = null!;
        public virtual DbSet<PruebaGraficaDenuncium> PruebaGraficaDenuncia { get; set; } = null!;
        public virtual DbSet<PruebaGraficaReclamo> PruebaGraficaReclamos { get; set; } = null!;
        public virtual DbSet<Recibo> Recibos { get; set; } = null!;
        public virtual DbSet<Reclamo> Reclamos { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<Sesione> Sesiones { get; set; } = null!;
        public virtual DbSet<Solicitud> Solicituds { get; set; } = null!;
        public virtual DbSet<Sugerencium> Sugerencia { get; set; } = null!;
        public virtual DbSet<TipoDatoAbierto> TipoDatoAbiertos { get; set; } = null!;
        public virtual DbSet<TipoDenuncium> TipoDenuncia { get; set; } = null!;
        public virtual DbSet<TipoLote> TipoLotes { get; set; } = null!;
        public virtual DbSet<TipoReclamo> TipoReclamos { get; set; } = null!;
        public virtual DbSet<TipoSolicitud> TipoSolicituds { get; set; } = null!;
        public virtual DbSet<Tipopago> Tipopagos { get; set; } = null!;
        public virtual DbSet<Trabajo> Trabajos { get; set; } = null!;
        public virtual DbSet<TrabajoReclamo> TrabajoReclamos { get; set; } = null!;
        public virtual DbSet<TrabajoSolicitud> TrabajoSolicituds { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<UsuarioVecino> UsuarioVecinos { get; set; } = null!;
        public virtual DbSet<ValuacionInmobiliario> ValuacionInmobiliarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Data Source=VAIO-ROMAN;Initial Catalog=M_VPSA_V3;Integrated Security=True");
                optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=M_VPSA_V3;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alicuotum>(entity =>
            {
                entity.HasKey(e => e.IdAlicuota)
                    .HasName("PK__ALICUOTA__381F0CC3C7E1784E");

                entity.ToTable("ALICUOTA");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(1500)
                    .IsUnicode(false);

                entity.Property(e => e.FechaGenerada)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ImporteBase).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Boletum>(entity =>
            {
                entity.HasKey(e => e.IdBoleta)
                    .HasName("PK__BOLETA__362F6EB6E9872461");

                entity.ToTable("BOLETA");

                entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");

                entity.Property(e => e.FechaGenerada)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaPago).HasColumnType("datetime");

                entity.Property(e => e.FechaVencimiento)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(dateadd(day,(1),getdate()))");

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
                entity.HasKey(e => e.IdEjecucion)
                    .HasName("PK__CONTROL___C0B11761FE8020C7");

                entity.ToTable("CONTROL_PROCESOS");

                entity.Property(e => e.FechaEjecucion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdProcesoNavigation)
                    .WithMany(p => p.ControlProcesos)
                    .HasForeignKey(d => d.IdProceso)
                    .HasConstraintName("FK__CONTROL_P__IdPro__7E37BEF6");
            });

            modelBuilder.Entity<DatosAbierto>(entity =>
            {
                entity.HasKey(e => e.IdArchivo)
                    .HasName("PK__DATOS_AB__DF161F50322474FD");

                entity.ToTable("DATOS_ABIERTOS");

                entity.Property(e => e.IdArchivo).HasColumnName("idArchivo");

                entity.Property(e => e.Bhabilitado)
                    .HasColumnName("BHabilitado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Extension)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NombreArchivo)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Ubicacion).HasColumnType("text");

                entity.HasOne(d => d.IdTipoDatoNavigation)
                    .WithMany(p => p.DatosAbiertos)
                    .HasForeignKey(d => d.IdTipoDato)
                    .HasConstraintName("FK_TipoDatoAbierto");
            });

            modelBuilder.Entity<Denuncium>(entity =>
            {
                entity.HasKey(e => e.NroDenuncia)
                    .HasName("PK__DENUNCIA__D763C1F90CC4DC99");

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
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

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

                entity.HasOne(d => d.CodEstadoDenunciaNavigation)
                    .WithMany(p => p.Denuncia)
                    .HasForeignKey(d => d.CodEstadoDenuncia)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__DENUNCIA__Cod_Es__00200768");

                entity.HasOne(d => d.CodTipoDenunciaNavigation)
                    .WithMany(p => p.Denuncia)
                    .HasForeignKey(d => d.CodTipoDenuncia)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__DENUNCIA__Cod_Ti__01142BA1");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Denuncia)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__DENUNCIA__IdUsua__02084FDA");

                entity.HasOne(d => d.NroPrioridadNavigation)
                    .WithMany(p => p.Denuncia)
                    .HasForeignKey(d => d.NroPrioridad)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Prioridad");
            });

            modelBuilder.Entity<Detalleboletum>(entity =>
            {
                entity.HasKey(e => e.IdDetalleBoleta)
                    .HasName("PK__DETALLEB__70845C4412659CFE");

                entity.ToTable("DETALLEBOLETA");

                entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");

                entity.Property(e => e.Importe).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<EstadoDenuncium>(entity =>
            {
                entity.HasKey(e => e.CodEstadoDenuncia)
                    .HasName("PK__ESTADO_D__FA18336D4F5CA3C8");

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
                entity.HasKey(e => e.CodEstadoReclamo)
                    .HasName("PK__ESTADO_R__40DB848079AAF75B");

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
                entity.HasKey(e => e.CodEstadoSolicitud)
                    .HasName("PK__ESTADO_S__AB5EC4AF4BD7964B");

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
                entity.HasKey(e => e.IdImpuesto)
                    .HasName("PK__IMPUESTO__A9B8892834BC6ABF");

                entity.ToTable("IMPUESTOINMOBILIARIO");

                entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");

                entity.Property(e => e.FechaEmision).HasColumnType("datetime");

                entity.Property(e => e.FechaVencimiento).HasColumnType("datetime");

                entity.Property(e => e.ImporteBase).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ImporteFinal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InteresValor).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdLoteNavigation)
                    .WithMany(p => p.Impuestoinmobiliarios)
                    .HasForeignKey(d => d.IdLote)
                    .HasConstraintName("FK__IMPUESTOI__IdLot__03F0984C");
            });

            modelBuilder.Entity<Lote>(entity =>
            {
                entity.HasKey(e => e.IdLote)
                    .HasName("PK__LOTE__38C4EE900269A14C");

                entity.ToTable("LOTE");

                entity.Property(e => e.Altura)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BaseImponible).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");

                entity.Property(e => e.Calle)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Alta")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdPersona).HasColumnName("idPersona");

                entity.Property(e => e.IdTipoLote).HasColumnName("Id_Tipo_Lote");

                entity.Property(e => e.NomenclaturaCatastral)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.SupEdificada).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SupTerreno).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ValuacionTotal).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Lotes)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__LOTE__idPersona__04E4BC85");

                entity.HasOne(d => d.IdTipoLoteNavigation)
                    .WithMany(p => p.Lotes)
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
                entity.HasKey(e => e.IdPagina)
                    .HasName("PK__PAGINA__034F88B8E9AC8AFE");

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
                entity.HasKey(e => e.IdPaginaxRol)
                    .HasName("PK__PAGINAXR__6E5913D8EED581F6");

                entity.ToTable("PAGINAXROL");

                entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.HasOne(d => d.IdPaginaNavigation)
                    .WithMany(p => p.Paginaxrols)
                    .HasForeignKey(d => d.IdPagina)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__PAGINAXRO__IdPag__06CD04F7");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Paginaxrols)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__PAGINAXRO__idRol__07C12930");
            });

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.HasKey(e => e.IdPago)
                    .HasName("PK__PAGO__FC851A3A1F94A043");

                entity.ToTable("PAGO");

                entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");

                entity.Property(e => e.FechaDePago)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Importe).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdTipoPagoNavigation)
                    .WithMany(p => p.Pagos)
                    .HasForeignKey(d => d.IdTipoPago)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__PAGO__IdTipoPago__08B54D69");
            });

            modelBuilder.Entity<ParametriaProceso>(entity =>
            {
                entity.HasKey(e => e.IdProceso)
                    .HasName("PK__PARAMETR__036D0743B280A207");

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
                entity.HasKey(e => e.IdPersona)
                    .HasName("PK__PERSONA__A478814136C68E02");

                entity.ToTable("PERSONA");

                entity.HasIndex(e => e.Mail, "UQ__PERSONA__2724B2D11427EB9E")
                    .IsUnique();

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
                entity.HasKey(e => e.NroPrioridad)
                    .HasName("PK__PRIORIDA__07C3E6AEB947A5B4");

                entity.ToTable("PRIORIDAD");

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
                entity.HasKey(e => e.NroPrioridad)
                    .HasName("PK__PRIORIDA__07C3E6AEE643C348");

                entity.ToTable("PRIORIDAD_RECLAMO");

                entity.Property(e => e.NroPrioridad).HasColumnName("Nro_Prioridad");

                entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NombrePrioridad)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_Prioridad");

                entity.Property(e => e.TiempoMaxTratamiento).HasColumnName("Tiempo_Max_Tratamiento");
            });

            modelBuilder.Entity<PruebaGraficaDenuncium>(entity =>
            {
                entity.HasKey(e => e.NroImagen)
                    .HasName("PK__PRUEBA_G__B8690AD8296390A2");

                entity.ToTable("PRUEBA_GRAFICA_DENUNCIA");

                entity.Property(e => e.NroImagen).HasColumnName("Nro_Imagen");

                entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");

                entity.Property(e => e.Foto)
                    .IsUnicode(false)
                    .HasColumnName("foto");

                entity.Property(e => e.NroDenuncia).HasColumnName("Nro_Denuncia");

                entity.Property(e => e.NroTrabajo)
                    .HasColumnName("Nro_Trabajo")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.PruebaGraficaDenuncia)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__PRUEBA_GR__IdUsu__09A971A2");
            });

            modelBuilder.Entity<PruebaGraficaReclamo>(entity =>
            {
                entity.HasKey(e => e.NroImagen)
                    .HasName("PK__PRUEBA_G__B8690AD8DAB0C528");

                entity.ToTable("PRUEBA_GRAFICA_RECLAMO");

                entity.Property(e => e.NroImagen).HasColumnName("Nro_Imagen");

                entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");

                entity.Property(e => e.Foto)
                    .IsUnicode(false)
                    .HasColumnName("foto");

                entity.Property(e => e.NroReclamo).HasColumnName("Nro_Reclamo");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.PruebaGraficaReclamos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__PRUEBA_GR__IdUsu__0A9D95DB");

                entity.HasOne(d => d.IdVecinoNavigation)
                    .WithMany(p => p.PruebaGraficaReclamos)
                    .HasForeignKey(d => d.IdVecino)
                    .HasConstraintName("FK__PRUEBA_GR__IdVec__0B91BA14");

                entity.HasOne(d => d.NroReclamoNavigation)
                    .WithMany(p => p.PruebaGraficaReclamos)
                    .HasForeignKey(d => d.NroReclamo)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__PRUEBA_GR__Nro_R__0C85DE4D");
            });

            modelBuilder.Entity<Recibo>(entity =>
            {
                entity.HasKey(e => e.IdRecibo)
                    .HasName("PK__RECIBO__2FEC473127482371");

                entity.ToTable("RECIBO");

                entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");

                entity.Property(e => e.FechaPago)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Importe).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Mail)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdBoletaNavigation)
                    .WithMany(p => p.Recibos)
                    .HasForeignKey(d => d.IdBoleta)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__RECIBO__IdBoleta__0D7A0286");
            });

            modelBuilder.Entity<Reclamo>(entity =>
            {
                entity.HasKey(e => e.NroReclamo)
                    .HasName("PK__RECLAMO__2D3C904F677796B5");

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

                entity.Property(e => e.NomApeVecino)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NroPrioridad).HasColumnName("Nro_Prioridad");

                entity.HasOne(d => d.CodEstadoReclamoNavigation)
                    .WithMany(p => p.Reclamos)
                    .HasForeignKey(d => d.CodEstadoReclamo)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__RECLAMO__Cod_Est__0E6E26BF");

                entity.HasOne(d => d.CodTipoReclamoNavigation)
                    .WithMany(p => p.Reclamos)
                    .HasForeignKey(d => d.CodTipoReclamo)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__RECLAMO__Cod_Tip__0F624AF8");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Reclamos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__RECLAMO__IdUsuar__10566F31");

                entity.HasOne(d => d.IdVecinoNavigation)
                    .WithMany(p => p.Reclamos)
                    .HasForeignKey(d => d.IdVecino)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__RECLAMO__IdVecin__114A936A");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__ROL__3C872F76FF689F0E");

                entity.ToTable("ROL");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");

                entity.Property(e => e.NombreRol)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Sesione>(entity =>
            {
                entity.HasKey(e => e.IdSesion)
                    .HasName("PK__SESIONES__22EC535BD8655960");

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

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Sesiones)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__SESIONES__idUsua__123EB7A3");
            });

            modelBuilder.Entity<Solicitud>(entity =>
            {
                entity.HasKey(e => e.NroSolicitud)
                    .HasName("PK__SOLICITU__CB3EA8FAE208728C");

                entity.ToTable("SOLICITUD");

                entity.Property(e => e.NroSolicitud).HasColumnName("Nro_Solicitud");

                entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");

                entity.Property(e => e.CodEstadoSolicitud).HasColumnName("Cod_Estado_Solicitud");

                entity.Property(e => e.CodTipoSolicitud).HasColumnName("Cod_Tipo_Solicitud");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.HasOne(d => d.CodEstadoSolicitudNavigation)
                    .WithMany(p => p.Solicituds)
                    .HasForeignKey(d => d.CodEstadoSolicitud)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__SOLICITUD__Cod_E__1332DBDC");

                entity.HasOne(d => d.CodTipoSolicitudNavigation)
                    .WithMany(p => p.Solicituds)
                    .HasForeignKey(d => d.CodTipoSolicitud)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__SOLICITUD__Cod_T__14270015");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Solicituds)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__SOLICITUD__IdUsu__151B244E");

                entity.HasOne(d => d.IdVecinoNavigation)
                    .WithMany(p => p.Solicituds)
                    .HasForeignKey(d => d.IdVecino)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__SOLICITUD__IdVec__160F4887");
            });

            modelBuilder.Entity<Sugerencium>(entity =>
            {
                entity.HasKey(e => e.IdSugerencia)
                    .HasName("PK__SUGERENC__A4668EAA7BE497BF");

                entity.ToTable("SUGERENCIA");

                entity.Property(e => e.IdSugerencia).HasColumnName("idSugerencia");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(1500)
                    .IsUnicode(false);

                entity.Property(e => e.FechaGenerada)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TipoDatoAbierto>(entity =>
            {
                entity.HasKey(e => e.IdTipoDato)
                    .HasName("PK__TIPO_DAT__C8C67CEB25E60DC2");

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
                entity.HasKey(e => e.CodTipoDenuncia)
                    .HasName("PK__TIPO_DEN__86CD78F657D00AFF");

                entity.ToTable("TIPO_DENUNCIA");

                entity.Property(e => e.CodTipoDenuncia).HasColumnName("Cod_Tipo_Denuncia");

                entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(90)
                    .IsUnicode(false);

                entity.Property(e => e.TiempoMaxTratamiento).HasColumnName("Tiempo_Max_Tratamiento");
            });

            modelBuilder.Entity<TipoLote>(entity =>
            {
                entity.HasKey(e => e.CodTipoLote)
                    .HasName("PK__TIPO_LOT__B769E6D080992895");

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
                entity.HasKey(e => e.CodTipoReclamo)
                    .HasName("PK__TIPO_REC__1162323349DE1CA3");

                entity.ToTable("TIPO_RECLAMO");

                entity.Property(e => e.CodTipoReclamo).HasColumnName("Cod_Tipo_Reclamo");

                entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(90)
                    .IsUnicode(false);

                entity.Property(e => e.TiempoMaxTratamiento).HasColumnName("Tiempo_Max_Tratamiento");

            });

            modelBuilder.Entity<TipoSolicitud>(entity =>
            {
                entity.HasKey(e => e.CodTipoSolicitud)
                    .HasName("PK__TIPO_SOL__6B39449528A82132");

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
                entity.HasKey(e => e.IdTipoPago)
                    .HasName("PK__TIPOPAGO__EB0AA9E7F8DB0CC3");

                entity.ToTable("TIPOPAGO");

                entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Trabajo>(entity =>
            {
                entity.HasKey(e => e.NroTrabajo)
                    .HasName("PK__TRABAJO__87806897E9A04110");

                entity.ToTable("TRABAJO");

                entity.Property(e => e.NroTrabajo).HasColumnName("Nro_trabajo");

                entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NroDenuncia).HasColumnName("Nro_Denuncia");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Trabajos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__TRABAJO__IdUsuar__17036CC0");
            });

            modelBuilder.Entity<TrabajoReclamo>(entity =>
            {
                entity.HasKey(e => e.NroTrabajo)
                    .HasName("PK__TRABAJO___87806897905044D9");

                entity.ToTable("TRABAJO_RECLAMO");

                entity.Property(e => e.NroTrabajo).HasColumnName("Nro_trabajo");

                entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NroReclamo).HasColumnName("Nro_Reclamo");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TrabajoReclamos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__TRABAJO_R__IdUsu__17F790F9");

                entity.HasOne(d => d.IdVecinoNavigation)
                    .WithMany(p => p.TrabajoReclamos)
                    .HasForeignKey(d => d.IdVecino)
                    .HasConstraintName("FK__TRABAJO_R__IdVec__18EBB532");

                entity.HasOne(d => d.NroReclamoNavigation)
                    .WithMany(p => p.TrabajoReclamos)
                    .HasForeignKey(d => d.NroReclamo)
                    .HasConstraintName("FK__TRABAJO_R__Nro_R__19DFD96B");
            });

            modelBuilder.Entity<TrabajoSolicitud>(entity =>
            {
                entity.HasKey(e => e.NroTrabajo)
                    .HasName("PK__TRABAJO___87806897B3379044");

                entity.ToTable("TRABAJO_SOLICITUD");

                entity.Property(e => e.NroTrabajo).HasColumnName("Nro_trabajo");

                entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NroSolicitud).HasColumnName("Nro_Solicitud");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TrabajoSolicituds)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__TRABAJO_S__IdUsu__1AD3FDA4");

                entity.HasOne(d => d.IdVecinoNavigation)
                    .WithMany(p => p.TrabajoSolicituds)
                    .HasForeignKey(d => d.IdVecino)
                    .HasConstraintName("FK__TRABAJO_S__IdVec__1BC821DD");

                entity.HasOne(d => d.NroSolicitudNavigation)
                    .WithMany(p => p.TrabajoSolicituds)
                    .HasForeignKey(d => d.NroSolicitud)
                    .HasConstraintName("FK__TRABAJO_S__Nro_S__1CBC4616");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__USUARIO__645723A6CE700A8B");

                entity.ToTable("USUARIO");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");

                entity.Property(e => e.Contrasenia)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaBaja).HasColumnType("date");

                entity.Property(e => e.IdPersona).HasColumnName("idPersona");

                entity.Property(e => e.IdTipoUsuario).HasColumnName("idTipoUsuario");

                entity.Property(e => e.NombreUser)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__USUARIO__idPerso__1DB06A4F");

                entity.HasOne(d => d.IdTipoUsuarioNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdTipoUsuario)
                    .HasConstraintName("FK__USUARIO__idTipoU__1EA48E88");

                entity.HasMany(e => e.TiposReclamoAlta)
                    .WithOne(e => e.UsuarioAlta)
                    .HasForeignKey(e => e.IdUsuarioAlta)
                    .HasPrincipalKey(e => e.IdUsuario);

                entity.HasMany(e => e.TiposReclamoModificados)
                    .WithOne(e => e.UsuarioModificacion)
                    .HasForeignKey(e => e.IdUsuarioModificacion)
                    .HasPrincipalKey(e => e.IdUsuario);
            });

            modelBuilder.Entity<UsuarioVecino>(entity =>
            {
                entity.HasKey(e => e.IdVecino)
                    .HasName("PK__USUARIO___E7C50E69099C5DA1");

                entity.ToTable("USUARIO_VECINO");

                entity.Property(e => e.IdVecino).HasColumnName("idVecino");

                entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");

                entity.Property(e => e.Contrasenia)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaBaja).HasColumnType("date");

                entity.Property(e => e.IdPersona).HasColumnName("idPersona");

                entity.Property(e => e.NombreUser)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.UsuarioVecinos)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__USUARIO_V__idPer__1F98B2C1");
            });

            modelBuilder.Entity<ValuacionInmobiliario>(entity =>
            {
                entity.HasKey(e => e.Idvaluacion)
                    .HasName("PK__VALUACIO__5430C5997A89E122");

                entity.ToTable("VALUACION_INMOBILIARIO");

                entity.Property(e => e.Bhabilitado).HasColumnName("BHabilitado");

                entity.Property(e => e.FechaDesde)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IncrementoAsfalto).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.IncrementoEsquina).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.ValorSupEdificada).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ValorSupTerreno).HasColumnType("decimal(18, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
