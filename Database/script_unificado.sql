/**
* Tabla Alicuotas
*/

CREATE TABLE [dbo].[ALICUOTA](
	[IdAlicuota] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](1500) NULL,
	[FechaGenerada] [datetime] NULL,
	[ImporteBase] [decimal](18, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdAlicuota] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

ALTER TABLE [dbo].[ALICUOTA] ADD  DEFAULT (getdate()) FOR [FechaGenerada];

/**
* Tabla area
*/
CREATE TABLE [dbo].[AREA] (
    [nro_area]    INT           IDENTITY (1, 1) NOT NULL,
    [nombre]      VARCHAR (100) NOT NULL,
    [cod_area]    VARCHAR (20)  NOT NULL,
    [bhabilitado] INT           CONSTRAINT [DEFAULT_AREA_bhabilitado] DEFAULT 1 NOT NULL,
    CONSTRAINT [PK_AREA] PRIMARY KEY CLUSTERED ([nro_area] ASC)
);

SET IDENTITY_INSERT [dbo].[AREA] ON;

INSERT INTO AREA (nro_area, nombre, cod_area, bhabilitado)
VALUES(1, 'MESA DE ENTRADA', 'ME', 1);
INSERT INTO AREA (nro_area, nombre, cod_area, bhabilitado)
VALUES(2, 'ESPACIOS VERDES', 'EV', 1);
INSERT INTO AREA (nro_area, nombre, cod_area, bhabilitado)
VALUES(3, 'ALUMBRADO PUBLICO', 'AP', 1);
INSERT INTO AREA (nro_area, nombre, cod_area, bhabilitado)
VALUES(4, 'OBRAS VIALES', 'OV', 1);
INSERT INTO AREA (nro_area, nombre, cod_area, bhabilitado)
VALUES(5, 'RECOLECCION DE RESIDUOS', 'RS', 1);

SET IDENTITY_INSERT [dbo].[AREA] OFF;

SET IDENTITY_INSERT [dbo].[AREA] ON;

INSERT INTO AREA (nro_area, nombre, cod_area, bhabilitado)
VALUES(6, 'SISTEMAS Y TELECOMUNICACIONES', 'SI', 1);

SET IDENTITY_INSERT [dbo].[AREA] OFF;


/**
* Tabla Boletas
*/

CREATE TABLE [dbo].[BOLETA](
	[IdBoleta] [int] IDENTITY(1,1) NOT NULL,
	[FechaGenerada] [datetime] NULL,
	[FechaPago] [datetime] NULL,
	[Estado] [int] NULL,
	[BHabilitado] [int] NULL,
	[TipoMoneda] [varchar](50) NULL,
	[Url] [varchar](350) NULL,
	[importe] [decimal](18, 0) NULL,
	[FechaVencimiento] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdBoleta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

ALTER TABLE [dbo].[BOLETA] ADD  DEFAULT (getdate()) FOR [FechaGenerada];
ALTER TABLE [dbo].[BOLETA] ADD  DEFAULT (dateadd(day,(1),getdate())) FOR [FechaVencimiento];

/**
* Tabla PARAMETRIA_PROCESOS
*/
CREATE TABLE [dbo].[PARAMETRIA_PROCESOS](
	[IdProceso] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](100) NOT NULL,
	[Periodicidad] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProceso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

SET IDENTITY_INSERT [dbo].[PARAMETRIA_PROCESOS] ON;
INSERT [dbo].[PARAMETRIA_PROCESOS] ([IdProceso], [Descripcion], [Periodicidad]) VALUES (1, N'GENERACION DE IMPUESTOS', N'ANUAL')
INSERT [dbo].[PARAMETRIA_PROCESOS] ([IdProceso], [Descripcion], [Periodicidad]) VALUES (2, N'GENERACION DE INTERESES', N'MENSUAL')
INSERT [dbo].[PARAMETRIA_PROCESOS] ([IdProceso], [Descripcion], [Periodicidad]) VALUES (3, N'BORRADO DE BOLETAS VENCIDAS', N'DIARIA')
SET IDENTITY_INSERT [dbo].[PARAMETRIA_PROCESOS] OFF;

/**
* Tabla CONTROL_PROCESOS
*/

CREATE TABLE [dbo].[CONTROL_PROCESOS](
	[IdEjecucion] [int] IDENTITY(1,1) NOT NULL,
	[IdProceso] [int] NOT NULL,
	[FechaEjecucion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdEjecucion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

ALTER TABLE [dbo].[CONTROL_PROCESOS] ADD  DEFAULT (getdate()) FOR [FechaEjecucion];

ALTER TABLE [dbo].[CONTROL_PROCESOS]  WITH CHECK ADD FOREIGN KEY([IdProceso])
REFERENCES [dbo].[PARAMETRIA_PROCESOS] ([IdProceso])
ON UPDATE CASCADE
ON DELETE CASCADE;

alter TABLE dbo.CONTROL_PROCESOS alter COLUMN FechaEjecucion DATETIME not null;

/**
* Tabla TIPO_DATO_ABIERTO
*/
CREATE TABLE [dbo].[TIPO_DATO_ABIERTO](
	[idTipoDato] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](90) NULL,
	[Descripcion] [varchar](250) NULL,
	[BHabilitado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idTipoDato] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

/**
* Tabla DATOS_ABIERTOS
*/
CREATE TABLE [dbo].[DATOS_ABIERTOS](
	[idArchivo] [int] IDENTITY(1,1) NOT NULL,
	[NombreArchivo] [varchar](150) NULL,
	[Extension] [varchar](10) NULL,
	[Tamaño] [float] NULL,
	[Ubicacion] [text] NULL,
	[BHabilitado] [int] NULL,
	[IdTipoDato] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idArchivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

ALTER TABLE [dbo].[DATOS_ABIERTOS] ADD  DEFAULT ((1)) FOR [BHabilitado];

ALTER TABLE [dbo].[DATOS_ABIERTOS]  WITH CHECK ADD  CONSTRAINT [FK_TipoDatoAbierto] FOREIGN KEY([IdTipoDato])
REFERENCES [dbo].[TIPO_DATO_ABIERTO] ([idTipoDato]);

ALTER TABLE [dbo].[DATOS_ABIERTOS] CHECK CONSTRAINT [FK_TipoDatoAbierto];

/**
* Tabla PERSONA
*/
CREATE TABLE [dbo].[PERSONA](
	[idPersona] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NULL,
	[Apellido] [varchar](150) NULL,
	[Telefono] [varchar](50) NULL,
	[Dni] [varchar](10) NULL,
	[Cuil] [varchar](16) NULL,
	[Mail] [varchar](100) NULL,
	[Domicilio] [varchar](100) NULL,
	[Altura] [varchar](5) NULL,
	[FechaNac] [date] NULL,
	[BHabilitado] [int] NULL,
	[BTieneUser] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

SET IDENTITY_INSERT [dbo].[PERSONA] ON;
INSERT [dbo].[PERSONA] ([idPersona], [Nombre], [Apellido], [Telefono], [Dni], [Cuil], [Mail], [Domicilio], [Altura], [FechaNac], [BHabilitado], [BTieneUser]) VALUES (1, N'ramon', N'valdez', N'45476547568', N'11234543', NULL, N'roman@gmail.com', N'3', N'345', CAST(N'1947-11-13' AS Date), 1, 1)
INSERT [dbo].[PERSONA] ([idPersona], [Nombre], [Apellido], [Telefono], [Dni], [Cuil], [Mail], [Domicilio], [Altura], [FechaNac], [BHabilitado], [BTieneUser]) VALUES (2, N'Rocio', N'Durcal', N'354758945', N'31564789', NULL, N'rocionieva@gmail.com', N'6', N'325|', NULL, 1, NULL)
INSERT [dbo].[PERSONA] ([idPersona], [Nombre], [Apellido], [Telefono], [Dni], [Cuil], [Mail], [Domicilio], [Altura], [FechaNac], [BHabilitado], [BTieneUser]) VALUES (1002, N'luis', N'Altamiran', N'351456897', N'16532489', NULL, N'gaston.cabrera176@gmail.com', N'4', N'35', CAST(N'1961-03-02' AS Date), 1, 1)
INSERT [dbo].[PERSONA] ([idPersona], [Nombre], [Apellido], [Telefono], [Dni], [Cuil], [Mail], [Domicilio], [Altura], [FechaNac], [BHabilitado], [BTieneUser]) VALUES (1003, N'Pablo Andres', N'Navarro', NULL, N'28426065', NULL, N'romansad@gmail.com', NULL, NULL, NULL, 1, NULL)
INSERT [dbo].[PERSONA] ([idPersona], [Nombre], [Apellido], [Telefono], [Dni], [Cuil], [Mail], [Domicilio], [Altura], [FechaNac], [BHabilitado], [BTieneUser]) VALUES (2005, N'David', N'Mazzotta', N'3515305248', N'278966458', NULL, N'romancito@gmail.com', N'3', N'345', NULL, 1, NULL)
INSERT [dbo].[PERSONA] ([idPersona], [Nombre], [Apellido], [Telefono], [Dni], [Cuil], [Mail], [Domicilio], [Altura], [FechaNac], [BHabilitado], [BTieneUser]) VALUES (2011, N'santiago', N'Brandalice', N'0354478998523', N'28975435', NULL, N'romancitodf@gmail.com', N'6', N'300', NULL, 1, NULL)
INSERT [dbo].[PERSONA] ([idPersona], [Nombre], [Apellido], [Telefono], [Dni], [Cuil], [Mail], [Domicilio], [Altura], [FechaNac], [BHabilitado], [BTieneUser]) VALUES (2012, N'Irma', N'Brandalice', N'351456789', N'108456458', NULL, N'irmabranda@gmail.com', N'6', N'347', NULL, 1, NULL)
INSERT [dbo].[PERSONA] ([idPersona], [Nombre], [Apellido], [Telefono], [Dni], [Cuil], [Mail], [Domicilio], [Altura], [FechaNac], [BHabilitado], [BTieneUser]) VALUES (2019, N'Roman', N'Sadowski', N'03515305248', N'26530960', NULL, N'romansad@hotmail.com', N'4', N'220', CAST(N'1978-09-13' AS Date), 1, NULL)
INSERT [dbo].[PERSONA] ([idPersona], [Nombre], [Apellido], [Telefono], [Dni], [Cuil], [Mail], [Domicilio], [Altura], [FechaNac], [BHabilitado], [BTieneUser]) VALUES (2026, N'Juan Ramiro', N'Iraola', N'1111111', N'13962205', NULL, N'noregistrado@gmail.com', N'alvear barrio centro cordoba', N'350', CAST(N'2001-01-01' AS Date), 1, NULL)
INSERT [dbo].[PERSONA] ([idPersona], [Nombre], [Apellido], [Telefono], [Dni], [Cuil], [Mail], [Domicilio], [Altura], [FechaNac], [BHabilitado], [BTieneUser]) VALUES (2027, N'Irena', N'Sadowski', N'3515305248', N'57353951', NULL, N'irenasadowski@gmail.com', N'calle 4', N'220', CAST(N'2018-03-07' AS Date), 1, NULL)
SET IDENTITY_INSERT [dbo].[PERSONA] OFF;

/**
* Tabla MOBBEX_PAGO
*/
CREATE TABLE [dbo].[MOBBEX_PAGO](
	[id_mobbex_pago] [int] IDENTITY(1,1) NOT NULL,
	[type] [varchar](30) NOT NULL,
	[view_type] [varchar](50) NULL,
	[payment_id] [varchar](50) NOT NULL,
	[payment_status_code] [varchar](3) NOT NULL,
	[payment_status_text] [varchar](30) NOT NULL,
	[payment_status_message] [varchar](100) NULL,
	[payment_total] [decimal](18, 2) NOT NULL,
	[payment_currency_code] [varchar](10) NOT NULL,
	[payment_currency_symbol] [varchar](10) NOT NULL,
	[payment_created] [varchar](30) NOT NULL,
	[payment_updated] [varchar](30) NULL,
	[payment_reference] [varchar](30) NOT NULL,
	[customer_uid] [varchar](30) NULL,
	[customer_email] [varchar](50) NULL,
	[checkout_uid] [varchar](30) NOT NULL,
	[fecha_alta] [datetime] NULL,
 CONSTRAINT [PK_MOBBEX_PAGO] PRIMARY KEY CLUSTERED 
(
	[id_mobbex_pago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY];

ALTER TABLE [dbo].[MOBBEX_PAGO] ADD  CONSTRAINT [DF_MOBBEX_PAGO_fecha_alta]  DEFAULT (getdate()) FOR [fecha_alta];

/**
* Tabla VALUACION_INMOBILIARIO
*/
CREATE TABLE [dbo].[VALUACION_INMOBILIARIO](
	[Idvaluacion] [int] IDENTITY(1,1) NOT NULL,
	[FechaDesde] [datetime] NULL,
	[BHabilitado] [int] NULL,
	[IncrementoEsquina] [int] NULL,
	[IncrementoAsfalto] [int] NULL,
	[ValorSupTerreno] [decimal](18, 0) NULL,
	[ValorSupEdificada] [decimal](18, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[Idvaluacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

ALTER TABLE [dbo].[VALUACION_INMOBILIARIO] ADD  DEFAULT (getdate()) FOR [FechaDesde];

/**
* Tabla Rol
*/
CREATE TABLE [dbo].[ROL](
	[idRol] [int] IDENTITY(1,1) NOT NULL,
	[NombreRol] [varchar](100) NULL,
	[BHabilitado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

SET IDENTITY_INSERT [dbo].[ROL] ON;
INSERT [dbo].[ROL] ([idRol], [NombreRol], [BHabilitado]) VALUES (1, N'Operador Tecnico', 1)
INSERT [dbo].[ROL] ([idRol], [NombreRol], [BHabilitado]) VALUES (2, N'Mesa de Entrada', 1)
INSERT [dbo].[ROL] ([idRol], [NombreRol], [BHabilitado]) VALUES (3, N'TribunalDeCuentas', 1)
INSERT [dbo].[ROL] ([idRol], [NombreRol], [BHabilitado]) VALUES (4, N'Intendente', 1)
INSERT [dbo].[ROL] ([idRol], [NombreRol], [BHabilitado]) VALUES (5, N'SecretarioDeGobierno', 1)
INSERT [dbo].[ROL] ([idRol], [NombreRol], [BHabilitado]) VALUES (6, N'Inspector', 1)
INSERT [dbo].[ROL] ([idRol], [NombreRol], [BHabilitado]) VALUES (1002, N'AdministradorDeSistema', 1)
SET IDENTITY_INSERT [dbo].[ROL] OFF;

SET IDENTITY_INSERT [dbo].[ROL] ON;
insert into [dbo].[ROL] (idRol, NombreRol, BHabilitado) values (2000, 'Vecino', 1);
SET IDENTITY_INSERT [dbo].[ROL] OFF;

alter table [dbo].[ROL]
  add tipoRol VARCHAR(15) not null default 'EMPLEADO'; 

UPDATE [dbo].[ROL] 
	set tipoRol = 'VECINO'
	WHERE idRol = 2000;

ALTER TABLE [dbo].[Rol]
ADD codRol VARCHAR(15) not NULL default 'NULL';

update [dbo].[Rol]
set codRol = 'OPT'
where NombreRol = 'Operador Tecnico';

update [dbo].[Rol]
set codRol = 'MDE'
where NombreRol = 'Mesa de Entrada';

update [dbo].[Rol]
set codRol = 'TDC'
where NombreRol = 'TribunalDeCuentas';

update [dbo].[Rol]
set codRol = 'INT'
where NombreRol = 'Intendente';

delete from [dbo].[Rol]
where NombreRol in ('SecretarioDeGobierno', 'Inspector');

update [dbo].[Rol]
set codRol = 'ADS'
where NombreRol = 'AdministradorDeSistema';

update [dbo].[Rol]
set codRol = 'VEC'
where NombreRol = 'Vecino';   

update [dbo].[ROL]
set NombreRol = 'Tribunal de Cuentas'
where idRol = 3;

update [dbo].[ROL]
set NombreRol = 'Operador Técnico'
where idRol = 1;

update [dbo].[ROL]
set NombreRol = 'Administrador de Sistema'
where idRol = 1002;

update [dbo].[ROL]
set BHabilitado = 1
where idRol = 1;

update [dbo].[ROL]
set BHabilitado = 0
where idRol = 3;

/**
* Tabla USUARIO
*/
CREATE TABLE [dbo].[USUARIO](
	[idUsuario] [int] IDENTITY(1,1) NOT NULL,
	[NombreUser] [varchar](100) NULL,
	[Contrasenia] [varchar](100) NULL,
	[idPersona] [int] NULL,
	[BHabilitado] [int] NULL,
	[idTipoUsuario] [int] NULL,
	[FechaAlta] [date] NULL,
	[FechaBaja] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

ALTER TABLE [dbo].[USUARIO] ADD  CONSTRAINT [DF_Usuario]  DEFAULT (getdate()) FOR [FechaAlta];

ALTER TABLE [dbo].[USUARIO]  WITH CHECK ADD FOREIGN KEY([idPersona])
REFERENCES [dbo].[PERSONA] ([idPersona])
ON UPDATE CASCADE
ON DELETE CASCADE;

ALTER TABLE [dbo].[USUARIO]  WITH CHECK ADD FOREIGN KEY([idTipoUsuario])
REFERENCES [dbo].[ROL] ([idRol]);

alter TABLE [dbo].[USUARIO] alter COLUMN NombreUser VARCHAR (100) not null; 
alter TABLE [dbo].[USUARIO] alter COLUMN Contrasenia VARCHAR (100) not null; 
alter TABLE [dbo].[USUARIO] alter COLUMN BHabilitado int not null; 
alter TABLE [dbo].[USUARIO] alter COLUMN idTipoUsuario int not null; 
alter TABLE [dbo].[USUARIO] alter COLUMN FechaAlta DATE not null;

ALTER TABLE dbo.usuario 
ADD nro_area int not null DEFAULT (1);

/**
* Tabla USUARIO_VECINO
*/
CREATE TABLE [dbo].[USUARIO_VECINO](
	[idVecino] [int] IDENTITY(1,1) NOT NULL,
	[NombreUser] [varchar](100) NULL,
	[Contrasenia] [varchar](100) NULL,
	[idPersona] [int] NULL,
	[BHabilitado] [int] NULL,
	[FechaAlta] [date] NULL,
	[FechaBaja] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[idVecino] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

ALTER TABLE [dbo].[USUARIO_VECINO] ADD  CONSTRAINT [DF_USUARIOVECINO]  DEFAULT (getdate()) FOR [FechaAlta];

ALTER TABLE [dbo].[USUARIO_VECINO]  WITH CHECK ADD FOREIGN KEY([idPersona])
REFERENCES [dbo].[PERSONA] ([idPersona])
ON UPDATE CASCADE
ON DELETE CASCADE;

drop TABLE [dbo].[USUARIO_VECINO];

/**
* Tabla PAGINA
*/
CREATE TABLE [dbo].[PAGINA](
	[IdPagina] [int] IDENTITY(1,1) NOT NULL,
	[Mensaje] [varchar](200) NULL,
	[Accion] [varchar](100) NULL,
	[Bhabilitado] [int] NULL,
	[BVisible] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPagina] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

/**
* Tabla PAGINAXROL
*/
CREATE TABLE [dbo].[PAGINAXROL](
	[IdPaginaxRol] [int] IDENTITY(1,1) NOT NULL,
	[IdPagina] [int] NULL,
	[idRol] [int] NULL,
	[BHabilitado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPaginaxRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

ALTER TABLE [dbo].[PAGINAXROL]  WITH CHECK ADD FOREIGN KEY([IdPagina])
REFERENCES [dbo].[PAGINA] ([IdPagina])
ON UPDATE CASCADE
ON DELETE CASCADE;

ALTER TABLE [dbo].[PAGINAXROL]  WITH CHECK ADD FOREIGN KEY([idRol])
REFERENCES [dbo].[ROL] ([idRol]);

/**
* Tabla TIPO_LOTE
*/
CREATE TABLE [dbo].[TIPO_LOTE](
	[Cod_Tipo_Lote] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](90) NULL,
	[Descripcion] [varchar](250) NULL,
	[BHabilitado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Cod_Tipo_Lote] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

SET IDENTITY_INSERT [dbo].[TIPO_LOTE] ON;
INSERT [dbo].[TIPO_LOTE] ([Cod_Tipo_Lote], [Nombre], [Descripcion], [BHabilitado]) VALUES (1, N'Baldio', N'El Lote no posee ningun tipo de Construccion.Ni Arbolado', 1)
INSERT [dbo].[TIPO_LOTE] ([Cod_Tipo_Lote], [Nombre], [Descripcion], [BHabilitado]) VALUES (2, N'En Construccion', N'El Lote posee Construccion. sin techar y no hay habitantes en el lote', 1)
INSERT [dbo].[TIPO_LOTE] ([Cod_Tipo_Lote], [Nombre], [Descripcion], [BHabilitado]) VALUES (3, N'Construido', N'El Lote posee Construccion. techada y la vivenda está habitada', 1)
INSERT [dbo].[TIPO_LOTE] ([Cod_Tipo_Lote], [Nombre], [Descripcion], [BHabilitado]) VALUES (4, N'Arbolado Autóctono', N'El Lote posee arbolado autoctono', 1)
INSERT [dbo].[TIPO_LOTE] ([Cod_Tipo_Lote], [Nombre], [Descripcion], [BHabilitado]) VALUES (5, N'Construccion+Arbolado Autóctono', N'El Lote posee Construccion. y posee mas de 5 arboles autoctonos en le lote', 1)
SET IDENTITY_INSERT [dbo].[TIPO_LOTE] OFF;

/**
* Tabla Lote
*/
CREATE TABLE [dbo].[LOTE](
	[IdLote] [int] IDENTITY(1,1) NOT NULL,
	[Manzana] [int] NULL,
	[NroLote] [int] NULL,
	[Calle] [varchar](50) NULL,
	[Altura] [varchar](50) NULL,
	[SupTerreno] [decimal](18, 0) NULL,
	[SupEdificada] [decimal](18, 0) NULL,
	[NomenclaturaCatastral] [varchar](25) NULL,
	[BaseImponible] [decimal](18, 0) NULL,
	[ValuacionTotal] [decimal](18, 0) NULL,
	[EstadoDeuda] [int] NULL,
	[idPersona] [int] NULL,
	[Fecha_Alta] [datetime] NULL,
	[BHabilitado] [int] NULL,
	[Id_Tipo_Lote] [int] NULL,
	[Esquina] [bit] NULL,
	[Asfaltado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdLote] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

ALTER TABLE [dbo].[LOTE] ADD  DEFAULT (getdate()) FOR [Fecha_Alta];

SET IDENTITY_INSERT [dbo].[LOTE] ON;
INSERT [dbo].[LOTE] ([IdLote], [Manzana], [NroLote], [Calle], [Altura], [SupTerreno], [SupEdificada], [NomenclaturaCatastral], [BaseImponible], [ValuacionTotal], [EstadoDeuda], [idPersona], [Fecha_Alta], [BHabilitado], [Id_Tipo_Lote], [Esquina], [Asfaltado]) VALUES (6, 97, 2, N'4', N'230', CAST(300 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), N'45643456448', CAST(230000 AS Decimal(18, 0)), CAST(250500 AS Decimal(18, 0)), 0, 1002, CAST(N'2022-05-22T20:11:44.713' AS DateTime), 1, NULL, NULL, NULL)
INSERT [dbo].[LOTE] ([IdLote], [Manzana], [NroLote], [Calle], [Altura], [SupTerreno], [SupEdificada], [NomenclaturaCatastral], [BaseImponible], [ValuacionTotal], [EstadoDeuda], [idPersona], [Fecha_Alta], [BHabilitado], [Id_Tipo_Lote], [Esquina], [Asfaltado]) VALUES (7, 97, 1, N'4', N'220', CAST(300 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), N'465667456489', CAST(200000 AS Decimal(18, 0)), CAST(250000 AS Decimal(18, 0)), 0, 1002, CAST(N'2022-05-22T20:11:44.713' AS DateTime), 1, NULL, NULL, NULL)
INSERT [dbo].[LOTE] ([IdLote], [Manzana], [NroLote], [Calle], [Altura], [SupTerreno], [SupEdificada], [NomenclaturaCatastral], [BaseImponible], [ValuacionTotal], [EstadoDeuda], [idPersona], [Fecha_Alta], [BHabilitado], [Id_Tipo_Lote], [Esquina], [Asfaltado]) VALUES (8, 97, 3, N'4', N'256', CAST(450 AS Decimal(18, 0)), CAST(125 AS Decimal(18, 0)), N'4565789456448', CAST(530000 AS Decimal(18, 0)), CAST(590500 AS Decimal(18, 0)), 0, 1002, CAST(N'2022-05-22T20:11:44.713' AS DateTime), 1, NULL, NULL, NULL)
INSERT [dbo].[LOTE] ([IdLote], [Manzana], [NroLote], [Calle], [Altura], [SupTerreno], [SupEdificada], [NomenclaturaCatastral], [BaseImponible], [ValuacionTotal], [EstadoDeuda], [idPersona], [Fecha_Alta], [BHabilitado], [Id_Tipo_Lote], [Esquina], [Asfaltado]) VALUES (9, 24, 1, N'30', N'18', CAST(450 AS Decimal(18, 0)), CAST(200 AS Decimal(18, 0)), N'4564523131156465', CAST(750000 AS Decimal(18, 0)), CAST(850000 AS Decimal(18, 0)), 1, 1003, CAST(N'2022-05-22T20:11:44.713' AS DateTime), 1, NULL, NULL, NULL)
INSERT [dbo].[LOTE] ([IdLote], [Manzana], [NroLote], [Calle], [Altura], [SupTerreno], [SupEdificada], [NomenclaturaCatastral], [BaseImponible], [ValuacionTotal], [EstadoDeuda], [idPersona], [Fecha_Alta], [BHabilitado], [Id_Tipo_Lote], [Esquina], [Asfaltado]) VALUES (10, 45, 45, N'45', N'45', CAST(455 AS Decimal(18, 0)), CAST(45 AS Decimal(18, 0)), NULL, NULL, NULL, NULL, NULL, CAST(N'2022-08-15T20:21:59.790' AS DateTime), 1, 4, NULL, NULL)
INSERT [dbo].[LOTE] ([IdLote], [Manzana], [NroLote], [Calle], [Altura], [SupTerreno], [SupEdificada], [NomenclaturaCatastral], [BaseImponible], [ValuacionTotal], [EstadoDeuda], [idPersona], [Fecha_Alta], [BHabilitado], [Id_Tipo_Lote], [Esquina], [Asfaltado]) VALUES (11, 456, 12, N'4', N'4', CAST(456 AS Decimal(18, 0)), CAST(45 AS Decimal(18, 0)), N'00000', NULL, NULL, NULL, NULL, CAST(N'2022-08-15T20:50:58.273' AS DateTime), 1, 3, NULL, NULL)
INSERT [dbo].[LOTE] ([IdLote], [Manzana], [NroLote], [Calle], [Altura], [SupTerreno], [SupEdificada], [NomenclaturaCatastral], [BaseImponible], [ValuacionTotal], [EstadoDeuda], [idPersona], [Fecha_Alta], [BHabilitado], [Id_Tipo_Lote], [Esquina], [Asfaltado]) VALUES (12, 12, 12, N'12', N'12', CAST(456 AS Decimal(18, 0)), CAST(14 AS Decimal(18, 0)), N'111111', NULL, NULL, NULL, NULL, CAST(N'2022-08-15T21:01:07.430' AS DateTime), 1, 3, NULL, NULL)
INSERT [dbo].[LOTE] ([IdLote], [Manzana], [NroLote], [Calle], [Altura], [SupTerreno], [SupEdificada], [NomenclaturaCatastral], [BaseImponible], [ValuacionTotal], [EstadoDeuda], [idPersona], [Fecha_Alta], [BHabilitado], [Id_Tipo_Lote], [Esquina], [Asfaltado]) VALUES (13, 105, 1, N'11', N'10', CAST(501 AS Decimal(18, 0)), CAST(120 AS Decimal(18, 0)), N'3102390301066007', NULL, NULL, NULL, NULL, CAST(N'2022-08-28T10:35:39.057' AS DateTime), 1, 3, NULL, NULL)
INSERT [dbo].[LOTE] ([IdLote], [Manzana], [NroLote], [Calle], [Altura], [SupTerreno], [SupEdificada], [NomenclaturaCatastral], [BaseImponible], [ValuacionTotal], [EstadoDeuda], [idPersona], [Fecha_Alta], [BHabilitado], [Id_Tipo_Lote], [Esquina], [Asfaltado]) VALUES (1013, 112, 8, N'6', N'220', CAST(460 AS Decimal(18, 0)), CAST(125 AS Decimal(18, 0)), N'12315646546532', NULL, NULL, NULL, 2019, CAST(N'2022-09-06T16:12:40.587' AS DateTime), 1, 2, NULL, NULL)
INSERT [dbo].[LOTE] ([IdLote], [Manzana], [NroLote], [Calle], [Altura], [SupTerreno], [SupEdificada], [NomenclaturaCatastral], [BaseImponible], [ValuacionTotal], [EstadoDeuda], [idPersona], [Fecha_Alta], [BHabilitado], [Id_Tipo_Lote], [Esquina], [Asfaltado]) VALUES (2013, 97, 8, N'3', N'200', CAST(160 AS Decimal(18, 0)), CAST(100 AS Decimal(18, 0)), N'45465464', NULL, NULL, NULL, 2019, CAST(N'2022-11-30T20:43:14.813' AS DateTime), 1, NULL, NULL, NULL)
INSERT [dbo].[LOTE] ([IdLote], [Manzana], [NroLote], [Calle], [Altura], [SupTerreno], [SupEdificada], [NomenclaturaCatastral], [BaseImponible], [ValuacionTotal], [EstadoDeuda], [idPersona], [Fecha_Alta], [BHabilitado], [Id_Tipo_Lote], [Esquina], [Asfaltado]) VALUES (2014, 12, 12, N'5', N'221', CAST(450 AS Decimal(18, 0)), CAST(120 AS Decimal(18, 0)), N'456454', NULL, NULL, NULL, 2019, CAST(N'2022-11-30T20:48:02.503' AS DateTime), 1, NULL, NULL, NULL)
INSERT [dbo].[LOTE] ([IdLote], [Manzana], [NroLote], [Calle], [Altura], [SupTerreno], [SupEdificada], [NomenclaturaCatastral], [BaseImponible], [ValuacionTotal], [EstadoDeuda], [idPersona], [Fecha_Alta], [BHabilitado], [Id_Tipo_Lote], [Esquina], [Asfaltado]) VALUES (2015, 98, 3, N'5', N'12', CAST(500 AS Decimal(18, 0)), CAST(70 AS Decimal(18, 0)), N'5468798', NULL, NULL, NULL, 2019, CAST(N'2022-11-30T20:55:34.110' AS DateTime), 1, NULL, NULL, NULL)
INSERT [dbo].[LOTE] ([IdLote], [Manzana], [NroLote], [Calle], [Altura], [SupTerreno], [SupEdificada], [NomenclaturaCatastral], [BaseImponible], [ValuacionTotal], [EstadoDeuda], [idPersona], [Fecha_Alta], [BHabilitado], [Id_Tipo_Lote], [Esquina], [Asfaltado]) VALUES (2016, 78, 5, N'4', N'654', CAST(650 AS Decimal(18, 0)), CAST(25 AS Decimal(18, 0)), N'4856448', NULL, NULL, NULL, 2019, CAST(N'2022-11-30T21:21:21.193' AS DateTime), 1, NULL, NULL, NULL)
INSERT [dbo].[LOTE] ([IdLote], [Manzana], [NroLote], [Calle], [Altura], [SupTerreno], [SupEdificada], [NomenclaturaCatastral], [BaseImponible], [ValuacionTotal], [EstadoDeuda], [idPersona], [Fecha_Alta], [BHabilitado], [Id_Tipo_Lote], [Esquina], [Asfaltado]) VALUES (2017, 8, 5, N'1', N'10', CAST(600 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), N'4', NULL, NULL, NULL, 2019, CAST(N'2022-11-30T21:24:32.603' AS DateTime), 1, NULL, NULL, NULL)
INSERT [dbo].[LOTE] ([IdLote], [Manzana], [NroLote], [Calle], [Altura], [SupTerreno], [SupEdificada], [NomenclaturaCatastral], [BaseImponible], [ValuacionTotal], [EstadoDeuda], [idPersona], [Fecha_Alta], [BHabilitado], [Id_Tipo_Lote], [Esquina], [Asfaltado]) VALUES (3013, 87, 12, N'5', N'564', CAST(400 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), N'4564598', NULL, NULL, NULL, 2019, CAST(N'2022-12-08T07:23:20.513' AS DateTime), 1, NULL, 1, 1)
INSERT [dbo].[LOTE] ([IdLote], [Manzana], [NroLote], [Calle], [Altura], [SupTerreno], [SupEdificada], [NomenclaturaCatastral], [BaseImponible], [ValuacionTotal], [EstadoDeuda], [idPersona], [Fecha_Alta], [BHabilitado], [Id_Tipo_Lote], [Esquina], [Asfaltado]) VALUES (3014, 12, 2, N'1', N'12', CAST(600 AS Decimal(18, 0)), CAST(300 AS Decimal(18, 0)), N'111', NULL, NULL, NULL, 2019, CAST(N'2022-12-08T11:15:05.797' AS DateTime), 1, NULL, 1, 1)
INSERT [dbo].[LOTE] ([IdLote], [Manzana], [NroLote], [Calle], [Altura], [SupTerreno], [SupEdificada], [NomenclaturaCatastral], [BaseImponible], [ValuacionTotal], [EstadoDeuda], [idPersona], [Fecha_Alta], [BHabilitado], [Id_Tipo_Lote], [Esquina], [Asfaltado]) VALUES (3015, 5, 2, N'9', N'235', CAST(2000 AS Decimal(18, 0)), CAST(22 AS Decimal(18, 0)), N'456459', NULL, NULL, NULL, 2019, CAST(N'2022-12-09T15:42:12.050' AS DateTime), 1, NULL, 1, 1)
SET IDENTITY_INSERT [dbo].[LOTE] OFF;

ALTER TABLE [dbo].[LOTE]  WITH CHECK ADD FOREIGN KEY([idPersona])
REFERENCES [dbo].[PERSONA] ([idPersona])
ON UPDATE CASCADE
ON DELETE CASCADE;

ALTER TABLE [dbo].[LOTE]  WITH CHECK ADD  CONSTRAINT [FK_TipoFrLote] FOREIGN KEY([Id_Tipo_Lote])
REFERENCES [dbo].[TIPO_LOTE] ([Cod_Tipo_Lote]);

ALTER TABLE [dbo].[LOTE] CHECK CONSTRAINT [FK_TipoFrLote];

alter TABLE dbo.lote alter COLUMN Bhabilitado int not null;

/**
* Tabla Prioridad
*/
CREATE TABLE [dbo].[Prioridad](
	[Nro_Prioridad] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_Prioridad] [varchar](50) NULL,
	[Descripcion] [varchar](200) NULL,
	[BHabilitado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Nro_Prioridad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

SET IDENTITY_INSERT [dbo].[Prioridad] ON;
INSERT [dbo].[Prioridad] ([Nro_Prioridad], [Nombre_Prioridad], [Descripcion], [BHabilitado]) VALUES (1, N'Alta', N'La solicitud debe tener un tiempo maximo de tratamiento de 24 hs', 1)
INSERT [dbo].[Prioridad] ([Nro_Prioridad], [Nombre_Prioridad], [Descripcion], [BHabilitado]) VALUES (2, N'Media', N'La solicitud debe tener un tiempo maximo de tratamiento de 48 hs', 1)
INSERT [dbo].[Prioridad] ([Nro_Prioridad], [Nombre_Prioridad], [Descripcion], [BHabilitado]) VALUES (3, N'Baja', N'La solicitud debe tener un tiempo maximo de tratamiento de 72 hs', 1)
INSERT [dbo].[Prioridad] ([Nro_Prioridad], [Nombre_Prioridad], [Descripcion], [BHabilitado]) VALUES (4, N'Sin Priorizar', N'La denuncia ha sido recientemente generada con lo cual aun no ha sido priorizada', 1)
SET IDENTITY_INSERT [dbo].[Prioridad] OFF;

/**
* Tabla TIPOPAGO
*/
CREATE TABLE [dbo].[TIPOPAGO](
	[IdTipoPago] [int] IDENTITY(1,1) NOT NULL,
	[BHabilitado] [int] NULL,
	[Descripcion] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdTipoPago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

/**
* TABLA TIPO_SOLICITUD
*/
CREATE TABLE [dbo].[TIPO_SOLICITUD](
	[Cod_Tipo_Solicitud] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](90) NULL,
	[Descripcion] [varchar](250) NULL,
	[BHabilitado] [int] NULL,
	[Tiempo_Max_Tratamiento] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Cod_Tipo_Solicitud] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

/**
* Tabla ESTADO_DENUNCIA
*/
CREATE TABLE [dbo].[ESTADO_DENUNCIA](
	[Cod_Estado_Denuncia] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](80) NULL,
	[Descripcion] [varchar](250) NULL,
	[BHabilitado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Cod_Estado_Denuncia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

SET IDENTITY_INSERT [dbo].[ESTADO_DENUNCIA] ON;
INSERT [dbo].[ESTADO_DENUNCIA] ([Cod_Estado_Denuncia], [Nombre], [Descripcion], [BHabilitado]) VALUES (1, N'Cerrada', N'Denuncia solucionada y cerrada por la mesa de entrada la gestion ha sido finalizada', 1)
INSERT [dbo].[ESTADO_DENUNCIA] ([Cod_Estado_Denuncia], [Nombre], [Descripcion], [BHabilitado]) VALUES (2, N'Asignada', N'Asignada Solicitud de Gestión asignada a algun Agente o funcionarioPúblico, para su tratamiento', 1)
INSERT [dbo].[ESTADO_DENUNCIA] ([Cod_Estado_Denuncia], [Nombre], [Descripcion], [BHabilitado]) VALUES (3, N'Nueva', N'Denuncia generada por vecino sin tratamiento aún', 1)
INSERT [dbo].[ESTADO_DENUNCIA] ([Cod_Estado_Denuncia], [Nombre], [Descripcion], [BHabilitado]) VALUES (4, N'En Tratamiento', N'Denuncia tomada por algun agente desarrollando las actividades correspondientes', 1)
INSERT [dbo].[ESTADO_DENUNCIA] ([Cod_Estado_Denuncia], [Nombre], [Descripcion], [BHabilitado]) VALUES (5, N'Solucionada', N'Denuncia resuelta por algun agente o funcionario público de municipio, falta cierre de la mesa', 1)
INSERT [dbo].[ESTADO_DENUNCIA] ([Cod_Estado_Denuncia], [Nombre], [Descripcion], [BHabilitado]) VALUES (6, N'Suspendida', N'Gestion suspendida por falta de algún recurso o razón especifica', 1)
INSERT [dbo].[ESTADO_DENUNCIA] ([Cod_Estado_Denuncia], [Nombre], [Descripcion], [BHabilitado]) VALUES (7, N'En Mesa de Entrada', N'En mesa de Entrada para derivar ante quien corresponda o realizar la gestión que corresponda', 1)
INSERT [dbo].[ESTADO_DENUNCIA] ([Cod_Estado_Denuncia], [Nombre], [Descripcion], [BHabilitado]) VALUES (8, N'Finalizada', N' En mesa de Entrada para cerrar la denuncia como solucionada', 1)
SET IDENTITY_INSERT [dbo].[ESTADO_DENUNCIA] OFF;

/**
* Tabla ESTADO_RECLAMO
*/
CREATE TABLE [dbo].[ESTADO_RECLAMO](
	[Cod_Estado_Reclamo] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](80) NULL,
	[BHabilitado] [int] NULL,
	[Descripcion] [varchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[Cod_Estado_Reclamo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

SET IDENTITY_INSERT [dbo].[ESTADO_RECLAMO] ON;
INSERT [dbo].[ESTADO_RECLAMO] ([Cod_Estado_Reclamo], [Nombre], [BHabilitado], [Descripcion]) VALUES (1, N'Nuevo', 1, N'Definir 1')
INSERT [dbo].[ESTADO_RECLAMO] ([Cod_Estado_Reclamo], [Nombre], [BHabilitado], [Descripcion]) VALUES (2, N'Asignado', 1, N'Definir 2')
INSERT [dbo].[ESTADO_RECLAMO] ([Cod_Estado_Reclamo], [Nombre], [BHabilitado], [Descripcion]) VALUES (3, N'En Tratamiento', 1, N'Definir 3')
INSERT [dbo].[ESTADO_RECLAMO] ([Cod_Estado_Reclamo], [Nombre], [BHabilitado], [Descripcion]) VALUES (4, N'Cancelado', 1, N'Definir 4')
INSERT [dbo].[ESTADO_RECLAMO] ([Cod_Estado_Reclamo], [Nombre], [BHabilitado], [Descripcion]) VALUES (5, N'Solucionado', 1, N'Definir 5')
INSERT [dbo].[ESTADO_RECLAMO] ([Cod_Estado_Reclamo], [Nombre], [BHabilitado], [Descripcion]) VALUES (6, N'Suspendido', 1, N'Definir 6')
SET IDENTITY_INSERT [dbo].[ESTADO_RECLAMO] OFF;

-- En Tratamiento
  update ESTADO_RECLAMO
  set Nombre = 'En Curso'
  where Cod_Estado_Reclamo = 3;

  -- Asignado
  update ESTADO_RECLAMO
  set BHabilitado = 0
  where Cod_Estado_Reclamo = 2;

-- Nuevo
  update ESTADO_RECLAMO
  set Nombre = 'Creado'
  where Cod_Estado_Reclamo = 1;

SET IDENTITY_INSERT [dbo].[estado_reclamo] ON;
INSERT INTO estado_reclamo (Cod_Estado_Reclamo, Nombre, BHabilitado)
VALUES(8, 'Pendiente de Cierre', 1);
SET IDENTITY_INSERT [dbo].[estado_reclamo] OFF;

/**
* Tabla ESTADO_SOLICITUD
*/
CREATE TABLE [dbo].[ESTADO_SOLICITUD](
	[Cod_Estado_Solicitud] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](80) NULL,
	[BHabilitado] [int] NULL,
	[Descripcion] [varchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[Cod_Estado_Solicitud] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

/**
* Tabla estado_sugerencia
*/
CREATE TABLE ESTADO_SUGERENCIA(
	Cod_Estado_Sugerencia int IDENTITY(1,1) PRIMARY KEY,
	Nombre varchar(80) NOT NULL,
	BHabilitado int Default(0),
	Descripcion varchar(250) NULL);

INSERT INTO ESTADO_SUGERENCIA(Nombre)
VALUES('Registrado');
INSERT INTO ESTADO_SUGERENCIA(Nombre)
VALUES('Considerado');
INSERT INTO ESTADO_SUGERENCIA(Nombre)
VALUES('Descartado');
INSERT INTO ESTADO_SUGERENCIA(Nombre)
VALUES('Gestionado');

/**
**
**
** HASTA ACA LLEGO
**
**/


































/**
* Tabla TIPO_RECLAMO
*/
CREATE TABLE [dbo].[TIPO_RECLAMO](
	[Cod_Tipo_Reclamo] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](90) NULL,
	[Descripcion] [varchar](250) NULL,
	[BHabilitado] [int] NULL,
	[Tiempo_Max_Tratamiento] [int] NULL,
	[FechaAlta] [datetime] NULL,
	[FechaModificacion] [datetime] NULL,
	[IdUsuarioAlta] [int] NULL,
	[IdUsuarioModificacion] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Cod_Tipo_Reclamo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

SET IDENTITY_INSERT [dbo].[TIPO_RECLAMO] ON;
INSERT [dbo].[TIPO_RECLAMO] ([Cod_Tipo_Reclamo], [Nombre], [Descripcion], [BHabilitado], [Tiempo_Max_Tratamiento], [FechaAlta], [FechaModificacion], [IdUsuarioAlta], [IdUsuarioModificacion]) VALUES (1, N'Recolección de Residuos Domiciliarios', N'Conflictos con el servicio de recoleccion de residus regular o diferenciada', 1, NULL, CAST(N'2022-11-20T23:17:47.477' AS DateTime), CAST(N'2022-11-20T23:17:47.477' AS DateTime), NULL, NULL)
INSERT [dbo].[TIPO_RECLAMO] ([Cod_Tipo_Reclamo], [Nombre], [Descripcion], [BHabilitado], [Tiempo_Max_Tratamiento], [FechaAlta], [FechaModificacion], [IdUsuarioAlta], [IdUsuarioModificacion]) VALUES (2, N'Recolección de Restos de podas', N'gestion de restos de poda y tala de arboles ', 1, NULL, CAST(N'2022-11-20T23:17:47.477' AS DateTime), CAST(N'2022-11-20T23:17:47.477' AS DateTime), NULL, NULL)
INSERT [dbo].[TIPO_RECLAMO] ([Cod_Tipo_Reclamo], [Nombre], [Descripcion], [BHabilitado], [Tiempo_Max_Tratamiento], [FechaAlta], [FechaModificacion], [IdUsuarioAlta], [IdUsuarioModificacion]) VALUES (3, N'Recolección de Escombros', N'Recolección de restos de obras', 1, NULL, CAST(N'2022-11-20T23:17:47.477' AS DateTime), CAST(N'2022-11-20T23:17:47.477' AS DateTime), NULL, NULL)
INSERT [dbo].[TIPO_RECLAMO] ([Cod_Tipo_Reclamo], [Nombre], [Descripcion], [BHabilitado], [Tiempo_Max_Tratamiento], [FechaAlta], [FechaModificacion], [IdUsuarioAlta], [IdUsuarioModificacion]) VALUES (4, N'Bacheo/Mejoramiento de Calle', N'reparacion de baches en calles domiciliarias', 1, NULL, CAST(N'2022-11-20T23:17:47.477' AS DateTime), CAST(N'2022-11-20T23:17:47.477' AS DateTime), NULL, NULL)
INSERT [dbo].[TIPO_RECLAMO] ([Cod_Tipo_Reclamo], [Nombre], [Descripcion], [BHabilitado], [Tiempo_Max_Tratamiento], [FechaAlta], [FechaModificacion], [IdUsuarioAlta], [IdUsuarioModificacion]) VALUES (5, N'Zanjeo', N'Zanjeo de veredas solicitudes reclamos', 1, NULL, CAST(N'2022-11-20T23:17:47.477' AS DateTime), CAST(N'2022-11-20T23:17:47.477' AS DateTime), NULL, NULL)
INSERT [dbo].[TIPO_RECLAMO] ([Cod_Tipo_Reclamo], [Nombre], [Descripcion], [BHabilitado], [Tiempo_Max_Tratamiento], [FechaAlta], [FechaModificacion], [IdUsuarioAlta], [IdUsuarioModificacion]) VALUES (6, N'Arbolado', N'generacion control de espacios verdes', 1, NULL, CAST(N'2022-11-20T23:17:47.477' AS DateTime), CAST(N'2022-11-20T23:17:47.477' AS DateTime), NULL, NULL)
INSERT [dbo].[TIPO_RECLAMO] ([Cod_Tipo_Reclamo], [Nombre], [Descripcion], [BHabilitado], [Tiempo_Max_Tratamiento], [FechaAlta], [FechaModificacion], [IdUsuarioAlta], [IdUsuarioModificacion]) VALUES (7, N'Pérdidas de Agua', N'Caños maestros dañados, perdidas de agua en caños secundarios', 1, NULL, CAST(N'2022-11-20T23:17:47.477' AS DateTime), CAST(N'2022-11-20T23:17:47.477' AS DateTime), NULL, NULL)
INSERT [dbo].[TIPO_RECLAMO] ([Cod_Tipo_Reclamo], [Nombre], [Descripcion], [BHabilitado], [Tiempo_Max_Tratamiento], [FechaAlta], [FechaModificacion], [IdUsuarioAlta], [IdUsuarioModificacion]) VALUES (8, N'Espacios Verdes', N'Gestion de arbolado, daños, renovaciones', 1, NULL, CAST(N'2022-11-20T23:17:47.477' AS DateTime), CAST(N'2022-11-20T23:17:47.477' AS DateTime), NULL, NULL)
INSERT [dbo].[TIPO_RECLAMO] ([Cod_Tipo_Reclamo], [Nombre], [Descripcion], [BHabilitado], [Tiempo_Max_Tratamiento], [FechaAlta], [FechaModificacion], [IdUsuarioAlta], [IdUsuarioModificacion]) VALUES (9, N'Administrativos - Generales', N'Gestions de deudas cobros mal realizados, plan de pagos', 1, NULL, CAST(N'2022-11-20T23:17:47.477' AS DateTime), CAST(N'2022-11-20T23:17:47.477' AS DateTime), NULL, NULL)
INSERT [dbo].[TIPO_RECLAMO] ([Cod_Tipo_Reclamo], [Nombre], [Descripcion], [BHabilitado], [Tiempo_Max_Tratamiento], [FechaAlta], [FechaModificacion], [IdUsuarioAlta], [IdUsuarioModificacion]) VALUES (10, N'Alumbrado Público', N'Luminarias dañadas , faltantes', 1, NULL, CAST(N'2022-11-20T23:17:47.477' AS DateTime), CAST(N'2022-11-20T23:17:47.477' AS DateTime), NULL, NULL)
INSERT [dbo].[TIPO_RECLAMO] ([Cod_Tipo_Reclamo], [Nombre], [Descripcion], [BHabilitado], [Tiempo_Max_Tratamiento], [FechaAlta], [FechaModificacion], [IdUsuarioAlta], [IdUsuarioModificacion]) VALUES (11, N'Plazas y parques', N'cuidados de espacios verdes comunes y publicos', 1, NULL, CAST(N'2022-11-20T23:17:47.477' AS DateTime), CAST(N'2022-11-20T23:17:47.477' AS DateTime), NULL, NULL)
INSERT [dbo].[TIPO_RECLAMO] ([Cod_Tipo_Reclamo], [Nombre], [Descripcion], [BHabilitado], [Tiempo_Max_Tratamiento], [FechaAlta], [FechaModificacion], [IdUsuarioAlta], [IdUsuarioModificacion]) VALUES (12, N'Comercio', N'gestiones comerciales', 1, NULL, CAST(N'2022-11-20T23:17:47.477' AS DateTime), CAST(N'2022-11-20T23:17:47.477' AS DateTime), NULL, NULL)
INSERT [dbo].[TIPO_RECLAMO] ([Cod_Tipo_Reclamo], [Nombre], [Descripcion], [BHabilitado], [Tiempo_Max_Tratamiento], [FechaAlta], [FechaModificacion], [IdUsuarioAlta], [IdUsuarioModificacion]) VALUES (13, N'Obras Públicas', N'retrasos de obras', 1, NULL, CAST(N'2022-11-20T23:17:47.477' AS DateTime), CAST(N'2022-11-20T23:17:47.477' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[TIPO_RECLAMO] OFF;

--ALTER TABLE [dbo].[TIPO_RECLAMO] ADD  DEFAULT (getdate()) FOR [FechaAlta];
ALTER TABLE [dbo].[TIPO_RECLAMO] ADD  DEFAULT (getdate()) FOR [FechaModificacion];

ALTER TABLE dbo.TIPO_RECLAMO 
ADD FechaAlta datetime DEFAULT (getdate());  

ALTER TABLE dbo.TIPO_RECLAMO 
ADD FechaModificacion datetime DEFAULT (getdate());  

ALTER TABLE dbo.TIPO_RECLAMO 
ADD IdUsuarioAlta int; 

ALTER TABLE dbo.TIPO_RECLAMO 
ADD IdUsuarioModificacion int; 
 
update dbo.TIPO_RECLAMO
  set FechaAlta = GETDATE(),
  FechaModificacion = GETDATE();

ALTER TABLE DBO.TIPO_RECLAMO
ADD CONSTRAINT FK__TIPO_RECLAMO__USUARIO_ALTA FOREIGN KEY (IdUsuarioAlta)
    REFERENCES DBO.USUARIO (idUsuario)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION;

ALTER TABLE DBO.TIPO_RECLAMO
ADD CONSTRAINT FK__TIPO_RECLAMO__USUARIO_MODIFICACION FOREIGN KEY (IdUsuarioModificacion)
    REFERENCES DBO.USUARIO (idUsuario)
    ON DELETE  NO ACTION
    ON UPDATE  NO ACTION;


update TIPO_RECLAMO
   set Tiempo_Max_Tratamiento = 100
 where Tiempo_Max_Tratamiento is null;

ALTER TABLE TIPO_RECLAMO ALTER COLUMN Tiempo_Max_Tratamiento int NOT NULL;

alter TABLE [dbo].[TIPO_RECLAMO] alter COLUMN IdUsuarioAlta int not null; 

alter TABLE [dbo].[TIPO_RECLAMO] alter COLUMN IdUsuarioModificacion int not null;

alter TABLE [dbo].[TIPO_RECLAMO] alter COLUMN IdUsuarioModificacion int not null;



























































/**
* Tabla DENUNCIA
*/
CREATE TABLE [dbo].[DENUNCIA](
	[Nro_Denuncia] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NULL,
	[Descripcion] [varchar](2500) NULL,
	[Cod_Tipo_Denuncia] [int] NULL,
	[Cod_Estado_Denuncia] [int] NULL,
	[Nombre_Infractor] [varchar](60) NULL,
	[Apellido_Infractor] [varchar](75) NULL,
	[IdUsuario] [int] NULL,
	[BHabilitado] [int] NULL,
	[Mail_Notificacion] [varchar](100) NULL,
	[Movil_Notificacion] [varchar](18) NULL,
	[Entre_Calles] [varchar](100) NULL,
	[Altura] [varchar](18) NULL,
	[Calle] [varchar](100) NULL,
	[Nro_Prioridad] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Nro_Denuncia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

ALTER TABLE [dbo].[DENUNCIA] ADD  CONSTRAINT [DF_Denuncia]  DEFAULT (getdate()) FOR [Fecha];

ALTER TABLE [dbo].[DENUNCIA]  WITH CHECK ADD FOREIGN KEY([Cod_Estado_Denuncia])
REFERENCES [dbo].[ESTADO_DENUNCIA] ([Cod_Estado_Denuncia])
ON UPDATE CASCADE
ON DELETE CASCADE;

ALTER TABLE [dbo].[DENUNCIA]  WITH CHECK ADD FOREIGN KEY([Cod_Tipo_Denuncia])
REFERENCES [dbo].[TIPO_DENUNCIA] ([Cod_Tipo_Denuncia])
ON UPDATE CASCADE
ON DELETE CASCADE;

ALTER TABLE [dbo].[DENUNCIA]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[USUARIO] ([idUsuario])
ON UPDATE CASCADE
ON DELETE CASCADE;

ALTER TABLE [dbo].[DENUNCIA]  WITH CHECK ADD  CONSTRAINT [FK_Prioridad] FOREIGN KEY([Nro_Prioridad])
REFERENCES [dbo].[Prioridad] ([Nro_Prioridad])
ON UPDATE CASCADE
ON DELETE CASCADE;

ALTER TABLE [dbo].[DENUNCIA] CHECK CONSTRAINT [FK_Prioridad];

/**
* Tabla DETALLEBOLETA
*/
CREATE TABLE [dbo].[DETALLEBOLETA](
	[IdDetalleBoleta] [int] IDENTITY(1,1) NOT NULL,
	[IdBoleta] [int] NOT NULL,
	[IdImpuesto] [int] NOT NULL,
	[BHabilitado] [int] NULL,
	[Importe] [decimal](18, 0) NULL,
	[Estado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDetalleBoleta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];





/**
* Tabla IMPUESTOINMOBILIARIO
*/
CREATE TABLE [dbo].[IMPUESTOINMOBILIARIO](
	[IdImpuesto] [int] IDENTITY(1,1) NOT NULL,
	[Mes] [int] NULL,
	[Año] [int] NULL,
	[FechaEmision] [datetime] NULL,
	[FechaVencimiento] [datetime] NULL,
	[Estado] [int] NULL,
	[ImporteBase] [decimal](18, 0) NULL,
	[InteresValor] [decimal](18, 0) NULL,
	[ImporteFinal] [decimal](18, 0) NULL,
	[BHabilitado] [int] NULL,
	[IdLote] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdImpuesto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

ALTER TABLE [dbo].[IMPUESTOINMOBILIARIO]  WITH CHECK ADD FOREIGN KEY([IdLote])
REFERENCES [dbo].[LOTE] ([IdLote])
ON UPDATE CASCADE
ON DELETE CASCADE;





/**
* Tabla observacion_reclamo
*/
CREATE TABLE [dbo].[OBSERVACION_RECLAMO](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nro_reclamo] [int] NOT NULL,
	[observacion] [varchar](250) NOT NULL,
	[cod_estado_reclamo] [int] NOT NULL,
	[id_usuario_alta] [int] NOT NULL,
	[fecha_alta] [datetime] NOT NULL,
	[codAccion] [varchar](15) NOT NULL
);

ALTER TABLE [dbo].[OBSERVACION_RECLAMO] ADD  CONSTRAINT [PK_OBSERVACION_RECLAMO] PRIMARY KEY CLUSTERED 
(
	[id] ASC
);

ALTER TABLE [dbo].[OBSERVACION_RECLAMO] ADD  CONSTRAINT [DEFAULT_obervacion_reclamo_fecha_alta]  DEFAULT (getdate()) FOR [fecha_alta];
ALTER TABLE [dbo].[OBSERVACION_RECLAMO]  WITH CHECK ADD  CONSTRAINT [FK_OBSERVACION_RECLAMO_ESTADO_RECLAMO] FOREIGN KEY([cod_estado_reclamo])
REFERENCES [dbo].[ESTADO_RECLAMO] ([Cod_Estado_Reclamo]);
ALTER TABLE [dbo].[OBSERVACION_RECLAMO] CHECK CONSTRAINT [FK_OBSERVACION_RECLAMO_ESTADO_RECLAMO];
ALTER TABLE [dbo].[OBSERVACION_RECLAMO]  WITH CHECK ADD  CONSTRAINT [FK_OBSERVACION_RECLAMO_RECLAMO] FOREIGN KEY([nro_reclamo])
REFERENCES [dbo].[RECLAMO] ([Nro_Reclamo]);
ALTER TABLE [dbo].[OBSERVACION_RECLAMO] CHECK CONSTRAINT [FK_OBSERVACION_RECLAMO_RECLAMO];
ALTER TABLE [dbo].[OBSERVACION_RECLAMO]  WITH CHECK ADD  CONSTRAINT [FK_OBSERVACION_RECLAMO_USUARIO] FOREIGN KEY([id_usuario_alta])
REFERENCES [dbo].[USUARIO] ([idUsuario]);
ALTER TABLE [dbo].[OBSERVACION_RECLAMO] CHECK CONSTRAINT [FK_OBSERVACION_RECLAMO_USUARIO];



/**
* Tabla PAGO
*/
CREATE TABLE [dbo].[PAGO](
	[IdPago] [int] IDENTITY(1,1) NOT NULL,
	[FechaDePago] [datetime] NULL,
	[Importe] [decimal](18, 0) NULL,
	[BHabilitado] [int] NULL,
	[IdTipoPago] [int] NULL,
	[IdBoleta] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

ALTER TABLE [dbo].[PAGO] ADD  DEFAULT (getdate()) FOR [FechaDePago];

ALTER TABLE [dbo].[PAGO]  WITH CHECK ADD FOREIGN KEY([IdTipoPago])
REFERENCES [dbo].[TIPOPAGO] ([IdTipoPago])
ON UPDATE CASCADE
ON DELETE CASCADE;







/**
* Tabla Prioridad_RECLAMO
*/
CREATE TABLE [dbo].[Prioridad_RECLAMO](
	[Nro_Prioridad] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_Prioridad] [varchar](50) NULL,
	[BHabilitado] [int] NULL,
	[Descripcion] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[Nro_Prioridad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

alter table dbo.RECLAMO add NomApeVecino VARCHAR(100);

alter table dbo.reclamo add Nro_Prioridad int;

update dbo.RECLAMO SET Nro_Prioridad = 4;

DELETE FROM Prioridad_RECLAMO;

SET IDENTITY_INSERT Prioridad_RECLAMO ON;

ALTER TABLE Prioridad_RECLAMO ADD Tiempo_Max_Tratamiento int NOT NULL;

INSERT INTO Prioridad_RECLAMO (Nro_Prioridad, Nombre_Prioridad, BHabilitado, Descripcion, Tiempo_Max_Tratamiento)
values(1, 'Alta', 1, '', 1);

INSERT INTO Prioridad_RECLAMO (Nro_Prioridad, Nombre_Prioridad, BHabilitado, Descripcion, Tiempo_Max_Tratamiento)
values(2, 'Media', 1, '', 2);

INSERT INTO Prioridad_RECLAMO (Nro_Prioridad, Nombre_Prioridad, BHabilitado, Descripcion, Tiempo_Max_Tratamiento)
values(3, 'Baja', 1, '', 7);

SET IDENTITY_INSERT Prioridad_RECLAMO OFF;

/**
* Tabla PRUEBA_GRAFICA_DENUNCIA
*/
CREATE TABLE [dbo].[PRUEBA_GRAFICA_DENUNCIA](
	[Nro_Imagen] [int] IDENTITY(1,1) NOT NULL,
	[Imagen] [varbinary](max) NULL,
	[Nro_Denuncia] [int] NULL,
	[IdUsuario] [int] NULL,
	[BHabilitado] [int] NULL,
	[foto] [varchar](max) NULL,
	[Nro_Trabajo] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Nro_Imagen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

ALTER TABLE [dbo].[PRUEBA_GRAFICA_DENUNCIA] ADD  CONSTRAINT [C_PGD_FK_TRABAJO]  DEFAULT ((1)) FOR [Nro_Trabajo];

ALTER TABLE [dbo].[PRUEBA_GRAFICA_DENUNCIA]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[USUARIO] ([idUsuario]);

/**
* Tabla PRUEBA_GRAFICA_RECLAMO
*/
CREATE TABLE [dbo].[PRUEBA_GRAFICA_RECLAMO](
	[Nro_Imagen] [int] IDENTITY(1,1) NOT NULL,
	[Imagen] [varbinary](max) NULL,
	[Nro_Reclamo] [int] NULL,
	[IdUsuario] [int] NULL,
	[IdVecino] [int] NULL,
	[BHabilitado] [int] NULL,
	[foto] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Nro_Imagen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

ALTER TABLE [dbo].[PRUEBA_GRAFICA_RECLAMO]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[USUARIO] ([idUsuario]);

ALTER TABLE [dbo].[PRUEBA_GRAFICA_RECLAMO]  WITH CHECK ADD FOREIGN KEY([IdVecino])
REFERENCES [dbo].[USUARIO_VECINO] ([idVecino]);

ALTER TABLE [dbo].[PRUEBA_GRAFICA_RECLAMO]  WITH CHECK ADD FOREIGN KEY([Nro_Reclamo])
REFERENCES [dbo].[RECLAMO] ([Nro_Reclamo])
ON UPDATE CASCADE
ON DELETE CASCADE;

ALTER TABLE dbo.PRUEBA_GRAFICA_RECLAMO ADD foto varchar(MAX);

Alter table [dbo].[PRUEBA_GRAFICA_RECLAMO]
drop constraint FK__PRUEBA_GR__IdVec__47A6A41B;

ALTER TABLE [dbo].[PRUEBA_GRAFICA_RECLAMO]
DROP COLUMN idVecino;

/**
* Tabla RECIBO
*/
CREATE TABLE [dbo].[RECIBO](
	[IdRecibo] [int] IDENTITY(1,1) NOT NULL,
	[FechaPago] [datetime] NULL,
	[BHabilitado] [int] NULL,
	[IdBoleta] [int] NULL,
	[Importe] [decimal](18, 0) NULL,
	[Mail] [varchar](150) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRecibo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

ALTER TABLE [dbo].[RECIBO] ADD  DEFAULT (getdate()) FOR [FechaPago];

ALTER TABLE [dbo].[RECIBO]  WITH CHECK ADD FOREIGN KEY([IdBoleta])
REFERENCES [dbo].[BOLETA] ([IdBoleta])
ON UPDATE CASCADE
ON DELETE CASCADE;

/**
* Tabla RECLAMO
*/
CREATE TABLE [dbo].[RECLAMO](
	[Nro_Reclamo] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NULL,
	[Descripcion] [varchar](2500) NULL,
	[Cod_Tipo_Reclamo] [int] NULL,
	[Cod_Estado_Reclamo] [int] NULL,
	[BHabilitado] [int] NULL,
	[Calle] [varchar](50) NULL,
	[Altura] [varchar](5) NULL,
	[EntreCalles] [varchar](50) NULL,
	[IdVecino] [int] NULL,
	[IdUsuario] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Nro_Reclamo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

ALTER TABLE [dbo].[RECLAMO] ADD  CONSTRAINT [DF_Fecha_Reclamo]  DEFAULT (getdate()) FOR [Fecha];

ALTER TABLE [dbo].[RECLAMO]  WITH CHECK ADD FOREIGN KEY([Cod_Estado_Reclamo])
REFERENCES [dbo].[ESTADO_RECLAMO] ([Cod_Estado_Reclamo])
ON UPDATE CASCADE
ON DELETE CASCADE;

ALTER TABLE [dbo].[RECLAMO]  WITH CHECK ADD FOREIGN KEY([Cod_Tipo_Reclamo])
REFERENCES [dbo].[TIPO_RECLAMO] ([Cod_Tipo_Reclamo])
ON UPDATE CASCADE
ON DELETE CASCADE;

ALTER TABLE [dbo].[RECLAMO]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[USUARIO] ([idUsuario]);

ALTER TABLE [dbo].[RECLAMO]  WITH CHECK ADD FOREIGN KEY([IdVecino])
REFERENCES [dbo].[USUARIO_VECINO] ([idVecino])
ON UPDATE CASCADE
ON DELETE CASCADE;

ALTER TABLE [dbo].[RECLAMO]
ADD MailVecino VARCHAR(100) NULL;

ALTER TABLE [dbo].[RECLAMO]
ADD TelefonoVecino VARCHAR(50) NULL;

Alter table [dbo].[RECLAMO]
drop constraint FK__RECLAMO__IdVecin__4D5F7D71;

ALTER TABLE [dbo].[RECLAMO]
   ADD FOREIGN KEY (idVecino)
      REFERENCES [dbo].[USUARIO] (idUsuario);

alter TABLE [dbo].[RECLAMO] alter COLUMN idUsuario int not null;  

ALTER TABLE dbo.RECLAMO 
ADD nro_area INT;  

ALTER TABLE dbo.RECLAMO 
ADD id_usuario_responsable INT;  

ALTER TABLE DBO.RECLAMO
   ADD FOREIGN KEY (nro_area)
      REFERENCES DBO.AREA (nro_area)
      ON DELETE  NO ACTION
      ON UPDATE  NO ACTION;

ALTER TABLE DBO.RECLAMO
   ADD FOREIGN KEY (id_usuario_responsable)
      REFERENCES DBO.USUARIO (idUsuario)
      ON DELETE  NO ACTION
      ON UPDATE  NO ACTION;

ALTER TABLE [dbo].[RECLAMO]
    ADD [id_sugerencia_origen] INT NULL;

ALTER TABLE [dbo].[RECLAMO]
    ADD [interno] VARCHAR (1) CONSTRAINT [DEFAULT_RECLAMO_interno] DEFAULT ('N') NOT NULL;

ALTER TABLE reclamo ADD CONSTRAINT FK_reclamo_prioridad FOREIGN KEY (Nro_Prioridad)
      REFERENCES Prioridad_reclamo (Nro_Prioridad) ; 

ALTER TABLE [dbo].[RECLAMO]
    ADD [fecha_cierre] DATE NULL;         

/**
* Tabla recuperacion_cuenta
*/
CREATE TABLE [dbo].[RECUPERACION_CUENTA] (
    [id_recuperacion_cuenta] INT            IDENTITY (1, 1) NOT NULL,
    [id_usuario]             INT            NOT NULL,
    [mail]                   VARCHAR (100)  NOT NULL,
    [uuid]                   VARCHAR (1000) NOT NULL,
    [fecha_alta]             DATETIME       NOT NULL,
    CONSTRAINT [PK_RECUPERACION_CUENTA] PRIMARY KEY CLUSTERED ([id_recuperacion_cuenta] ASC),
    CONSTRAINT [FK_RECUPERACION_CUENTA_USUARIO] FOREIGN KEY ([id_usuario]) REFERENCES [dbo].[USUARIO] ([idUsuario])
);  



/**
* Tabla sesiones
*/
CREATE TABLE [dbo].[SESIONES](
	[IdSesion] [varchar](25) NOT NULL,
	[Maquina] [varchar](50) NULL,
	[FechaInicio] [datetime] NULL,
	[FechaFin] [datetime] NULL,
	[idUsuario] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdSesion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

ALTER TABLE [dbo].[SESIONES]  WITH CHECK ADD FOREIGN KEY([idUsuario])
REFERENCES [dbo].[USUARIO] ([idUsuario])
ON UPDATE CASCADE
ON DELETE CASCADE;

drop TABLE [dbo].[SESIONES];

/**
* Tabla solicitud
*/
CREATE TABLE [dbo].[SOLICITUD](
	[Nro_Solicitud] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [date] NULL,
	[Descripcion] [varchar](2500) NULL,
	[Cod_Tipo_Solicitud] [int] NULL,
	[Cod_Estado_Solicitud] [int] NULL,
	[BHabilitado] [int] NULL,
	[IdVecino] [int] NULL,
	[IdUsuario] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Nro_Solicitud] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

ALTER TABLE [dbo].[SOLICITUD]  WITH CHECK ADD FOREIGN KEY([Cod_Estado_Solicitud])
REFERENCES [dbo].[ESTADO_SOLICITUD] ([Cod_Estado_Solicitud])
ON UPDATE CASCADE
ON DELETE CASCADE;

ALTER TABLE [dbo].[SOLICITUD]  WITH CHECK ADD FOREIGN KEY([Cod_Tipo_Solicitud])
REFERENCES [dbo].[TIPO_SOLICITUD] ([Cod_Tipo_Solicitud])
ON UPDATE CASCADE
ON DELETE CASCADE;

ALTER TABLE [dbo].[SOLICITUD]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[USUARIO] ([idUsuario]);

ALTER TABLE [dbo].[SOLICITUD]  WITH CHECK ADD FOREIGN KEY([IdVecino])
REFERENCES [dbo].[USUARIO_VECINO] ([idVecino])
ON UPDATE CASCADE
ON DELETE CASCADE;

Alter table [dbo].[SOLICITUD]
drop constraint FK__SOLICITUD__IdVec__5224328E;

ALTER TABLE [dbo].[SOLICITUD]
   ADD FOREIGN KEY (idVecino)
      REFERENCES [dbo].[USUARIO] (idUsuario);

/**
* Tabla sugerencia
*/
CREATE TABLE [dbo].[SUGERENCIA](
	[idSugerencia] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](1500) NULL,
	[FechaGenerada] [date] NULL,
	[Bhabilitado] [int] NULL,
	[Estado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idSugerencia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

ALTER TABLE [dbo].[SUGERENCIA] ADD  CONSTRAINT [DF_Sugerencia]  DEFAULT (getdate()) FOR [FechaGenerada];

ALTER TABLE SUGERENCIA ADD CONSTRAINT FK_Estado FOREIGN KEY (Estado)
      REFERENCES ESTADO_SUGERENCIA (Cod_Estado_Sugerencia) ;

ALTER TABLE SUGERENCIA ALTER COLUMN Estado int NOT NULL;



/**
* Tabla TIPO_DENUNCIA
*/
CREATE TABLE [dbo].[TIPO_DENUNCIA](
	[Cod_Tipo_Denuncia] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](90) NULL,
	[Descripcion] [varchar](250) NULL,
	[Tiempo_Max_Tratamiento] [int] NULL,
	[BHabilitado] [int] NULL,
	[IdUsuarioAlta] [int] NULL,
	[IdUsuarioModificacion] [int] NULL,
	[FechaAlta] [datetime] NULL,
	[FechaModificacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Cod_Tipo_Denuncia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

SET IDENTITY_INSERT [dbo].[TIPO_DENUNCIA] ON;
INSERT [dbo].[TIPO_DENUNCIA] ([Cod_Tipo_Denuncia], [Nombre], [Descripcion], [Tiempo_Max_Tratamiento], [BHabilitado], [IdUsuarioAlta], [IdUsuarioModificacion], [FechaAlta], [FechaModificacion]) VALUES (1, N'Ruidos Molestos', N'Relacionado con ruidos 
		   que superan los 60 db de manera iterativa y fuera del horario', 72, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[TIPO_DENUNCIA] ([Cod_Tipo_Denuncia], [Nombre], [Descripcion], [Tiempo_Max_Tratamiento], [BHabilitado], [IdUsuarioAlta], [IdUsuarioModificacion], [FechaAlta], [FechaModificacion]) VALUES (2, N'Baldio Sucio y/o con Malezas', N'Baldios con cacharros, objetos cortantes, malezas 
		   o yuyos altos', 96, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[TIPO_DENUNCIA] ([Cod_Tipo_Denuncia], [Nombre], [Descripcion], [Tiempo_Max_Tratamiento], [BHabilitado], [IdUsuarioAlta], [IdUsuarioModificacion], [FechaAlta], [FechaModificacion]) VALUES (3, N'Destruccion Patrimonio', N'Relacionado con Destruccion de Patrimonio Público', 24, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[TIPO_DENUNCIA] ([Cod_Tipo_Denuncia], [Nombre], [Descripcion], [Tiempo_Max_Tratamiento], [BHabilitado], [IdUsuarioAlta], [IdUsuarioModificacion], [FechaAlta], [FechaModificacion]) VALUES (4, N'Conexion ilegal Agua', N'Relacionado con conexiones de Agua Ilegal sin intervencion del muncipio', 96, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[TIPO_DENUNCIA] ([Cod_Tipo_Denuncia], [Nombre], [Descripcion], [Tiempo_Max_Tratamiento], [BHabilitado], [IdUsuarioAlta], [IdUsuarioModificacion], [FechaAlta], [FechaModificacion]) VALUES (5, N'Perro Suelto', N'Relacionado con perros sin dueño o abandonados en la calle.', 24, 1, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[TIPO_DENUNCIA] OFF;

ALTER TABLE [dbo].[TIPO_DENUNCIA] ADD  DEFAULT (getdate()) FOR [FechaAlta];









/**
* Tabla TRABAJO
*/
CREATE TABLE [dbo].[TRABAJO](
	[Nro_trabajo] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](2500) NULL,
	[Fecha] [datetime] NULL,
	[Nro_Denuncia] [int] NOT NULL,
	[IdUsuario] [int] NULL,
	[BHabilitado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Nro_trabajo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

ALTER TABLE [dbo].[TRABAJO] ADD  DEFAULT (getdate()) FOR [Fecha];

ALTER TABLE [dbo].[TRABAJO]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[USUARIO] ([idUsuario]);

ALTER TABLE [dbo].[TRABAJO]  WITH CHECK ADD FOREIGN KEY([Nro_Denuncia])
REFERENCES [dbo].[DENUNCIA] ([Nro_Denuncia]);

/**
* Tabla TRABAJO_RECLAMO
*/
CREATE TABLE [dbo].[TRABAJO_RECLAMO](
	[Nro_trabajo] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](2500) NULL,
	[Fecha] [datetime] NULL,
	[Nro_Reclamo] [int] NOT NULL,
	[IdUsuario] [int] NULL,
	[IdVecino] [int] NULL,
	[BHabilitado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Nro_trabajo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

ALTER TABLE [dbo].[TRABAJO_RECLAMO] ADD  DEFAULT (getdate()) FOR [Fecha];

ALTER TABLE [dbo].[TRABAJO_RECLAMO]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[USUARIO] ([idUsuario]);

ALTER TABLE [dbo].[TRABAJO_RECLAMO]  WITH CHECK ADD FOREIGN KEY([IdVecino])
REFERENCES [dbo].[USUARIO_VECINO] ([idVecino]);

ALTER TABLE [dbo].[TRABAJO_RECLAMO]  WITH CHECK ADD FOREIGN KEY([Nro_Reclamo])
REFERENCES [dbo].[RECLAMO] ([Nro_Reclamo])
ON UPDATE CASCADE
ON DELETE CASCADE;

Alter table [dbo].[TRABAJO_RECLAMO]
drop constraint FK__TRABAJO_R__IdVec__55009F39;

ALTER TABLE [dbo].[TRABAJO_RECLAMO]
   ADD FOREIGN KEY (idVecino)
      REFERENCES [dbo].[USUARIO] (idUsuario);

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TRABAJO_RECLAMO]') AND type in (N'U'))
DROP TABLE [dbo].[TRABAJO_RECLAMO]
GO


CREATE TABLE [dbo].[TRABAJO_RECLAMO] (
    [nro_trabajo]     INT           IDENTITY (1, 1) NOT NULL,
    [nro_reclamo]     INT           NOT NULL,
    [nro_area_trabajo] INT           NOT NULL,
    [fecha_trabajo]   DATE          NOT NULL,
    [descripcion]     VARCHAR (250) NOT NULL,
    [id_usuario_alta] INT           NOT NULL,
    [fecha_alta]      DATETIME      CONSTRAINT [DEFAULT_TRABAJO_RECLAMO_fecha_alta] DEFAULT getDate() NOT NULL,
    [bhabilitado]     INT           CONSTRAINT [DEFAULT_TRABAJO_RECLAMO_bhabilitado] DEFAULT 1 NOT NULL,
    CONSTRAINT [PK_TRABAJO_RECLAMO] PRIMARY KEY CLUSTERED ([nro_trabajo] ASC),
    CONSTRAINT [FK_TRABAJO_RECLAMO_RECLAMO] FOREIGN KEY ([nro_reclamo]) REFERENCES [dbo].[RECLAMO] ([Nro_Reclamo]),
    CONSTRAINT [FK_TRABAJO_RECLAMO_AREA] FOREIGN KEY ([nro_area_trabajo]) REFERENCES [dbo].[AREA] ([nro_area]),
    CONSTRAINT [FK_TRABAJO_RECLAMO_USUARIO] FOREIGN KEY ([id_usuario_alta]) REFERENCES [dbo].[USUARIO] ([idUsuario])
);      

/**
* Tabla TRABAJO_SOLICITUD
*/
CREATE TABLE [dbo].[TRABAJO_SOLICITUD](
	[Nro_trabajo] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](2500) NULL,
	[Fecha] [datetime] NULL,
	[Nro_Solicitud] [int] NOT NULL,
	[IdUsuario] [int] NULL,
	[IdVecino] [int] NULL,
	[BHabilitado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Nro_trabajo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

ALTER TABLE [dbo].[TRABAJO_SOLICITUD] ADD  DEFAULT (getdate()) FOR [Fecha];

ALTER TABLE [dbo].[TRABAJO_SOLICITUD]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[USUARIO] ([idUsuario]);

ALTER TABLE [dbo].[TRABAJO_SOLICITUD]  WITH CHECK ADD FOREIGN KEY([IdVecino])
REFERENCES [dbo].[USUARIO_VECINO] ([idVecino]);

ALTER TABLE [dbo].[TRABAJO_SOLICITUD]  WITH CHECK ADD FOREIGN KEY([Nro_Solicitud])
REFERENCES [dbo].[SOLICITUD] ([Nro_Solicitud])
ON UPDATE CASCADE
ON DELETE CASCADE;

Alter table [dbo].[TRABAJO_SOLICITUD]
drop constraint FK__TRABAJO_S__IdVec__57DD0BE4;

ALTER TABLE [dbo].[TRABAJO_SOLICITUD]
   ADD FOREIGN KEY (idVecino)
      REFERENCES [dbo].[USUARIO] (idUsuario);





/**
* Vista vw_reclamo
*/
create view dbo.vw_reclamo as SELECT        rec.Nro_Reclamo, 
rec.Fecha,
rec.Descripcion, 
rec.Cod_Tipo_Reclamo,
rec.Cod_Estado_Reclamo, 
rec.BHabilitado,
rec.Calle, 
rec.Altura, 
rec.EntreCalles, 
rec.IdVecino, 
rec.IdUsuario, 
rec.NomApeVecino, 
rec.Nro_Prioridad, 
rec.MailVecino,
rec.TelefonoVecino, 
tre.Nombre AS tipo_reclamo ,
est.Nombre AS estado_reclamo , 
pre.Nombre_Prioridad AS prioridad_reclamo  ,
usu.NombreUser AS usuario, 
CASE WHEN usu.NombreUser IS NULL THEN 'Sin Asignar' ELSE concat(per.Apellido, ', ', per.Nombre) END AS empleado,
rec.nro_area
FROM            dbo.RECLAMO AS rec INNER JOIN
                         dbo.TIPO_RECLAMO AS tre ON tre.Cod_Tipo_Reclamo = rec.Cod_Tipo_Reclamo INNER JOIN
                         dbo.ESTADO_RECLAMO AS est ON est.Cod_Estado_Reclamo = rec.Cod_Estado_Reclamo INNER JOIN
                         dbo.PRIORIDAD_RECLAMO AS pre ON pre.Nro_Prioridad = rec.Nro_Prioridad LEFT OUTER JOIN
                         dbo.USUARIO AS usu ON usu.idUsuario = rec.IdUsuario LEFT OUTER JOIN
                         dbo.PERSONA AS per ON per.idPersona = usu.idPersona;

						 /**
* Envio de Correos
*/
CREATE TABLE [dbo].[EmailQueue](
    Id              int             NOT NULL PRIMARY KEY IDENTITY(1,1),
    Recipients      varchar(250)    NOT NULL,
    Cc_recipients   varchar(250)    NULL,
    Email_Subject   varchar(250)    NOT NULL,
    Email_body      varchar(max)    NULL,
    QueueTime       datetime2       NOT NULL,
    SentTime        datetime2       NULL
);
