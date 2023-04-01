CREATE
	OR

ALTER PROCEDURE [dbo].[GENERACION_IMPUESTOS_LOTES]
	-- Add the parameters for the stored procedure here
	-- <@Param1, sysname, @p1> <Datatype_For_Param1, , int> = <Default_Value_For_Param1, , 0>,
	-- <@Param2, sysname, @p2> <Datatype_For_Param2, , int> = <Default_Value_For_Param2, , 0>
	(
	@montoSupTerreno DECIMAL(18, 2),
	@montoSupEdificada DECIMAL(18, 2),
	@interes_esquina DECIMAL(18, 2),
	@interes_asfalto DECIMAL(18, 2)
	)
AS
BEGIN
	DECLARE @lote INT,
		@supTerreno DECIMAL(18, 2),
		@supEdificada DECIMAL(18, 2)

	-- DECLARACIÓN DEL CURSOR
	DECLARE CURSOR_LOTE CURSOR
	FOR
	SELECT IdLote,
		SupTerreno,
		SupEdificada
	FROM LOTE

	-- APERTURA DEL CURSOR
	OPEN CURSOR_LOTE

	-- LECTURA DEL PRIMER REGISTRO
	FETCH NEXT
	FROM CURSOR_LOTE
	INTO @lote,
		@supTerreno,
		@supEdificada;

	-- RECORRER EL CURSOR MIENTRAS HAYAN REGISTROS
	WHILE @@FETCH_STATUS = 0
	BEGIN
		UPDATE LOTE
		SET BaseImponible = @supTerreno * @montoSupTerreno,
			ValuacionTotal = (@supTerreno - @supEdificada) * @montoSupTerreno + @supEdificada * @montoSupEdificada
		WHERE IdLote = @Lote

		FETCH NEXT
		FROM CURSOR_LOTE
		INTO @lote,
			@supTerreno,
			@supEdificada;
	END

	CLOSE CURSOR_LOTE

	DEALLOCATE CURSOR_LOTE

	-- fin de actualizacion tabla lotes
	DECLARE @Valuacion DECIMAL(15, 2),
		@Esquina BIT,
		@Principal BIT,
		@TOTAL DECIMAL(18, 2) = 0,
		@MONTO DECIMAL(18, 2),
		@anio VARCHAR(4) = YEAR(GetDate()),
		@FechaInicio DATETIME,
		@FechaVencimiento DATETIME,
		@contador INT = 0

	-- DECLARACIÓN DEL CURSOR
	DECLARE MI_CURSOR CURSOR
	FOR
	SELECT IdLote,
		ValuacionTotal,
		esquina,
		asfaltado
	FROM LOTE

	-- APERTURA DEL CURSOR
	OPEN MI_CURSOR

	-- LECTURA DEL PRIMER REGISTRO
	FETCH NEXT
	FROM MI_CURSOR
	INTO @lote,
		@Valuacion,
		@Esquina,
		@Principal;

	-- RECORRER EL CURSOR MIENTRAS HAYAN REGISTROS
	WHILE @@FETCH_STATUS = 0
	BEGIN
		SET @FechaInicio = @anio + '-01-01'
		SET @FechaVencimiento = dateadd(dd, 9, @FechaInicio)
		SET @MONTO = @Valuacion * 0.01

		IF (@Esquina = 1)
			SET @MONTO = @MONTO + (@MONTO * @interes_esquina / 100)

		IF (@Principal = 1)
			SET @MONTO = @MONTO + (@MONTO * @interes_asfalto / 100)
		SET @TOTAL = @MONTO * 10

		INSERT INTO IMPUESTOINMOBILIARIO (
			Mes,
			Año,
			FechaEmision,
			FechaVencimiento,
			Estado,
			ImporteBase,
			ImporteFinal,
			IdLote
			)
		VALUES (
			@contador,
			YEAR(GETDATE()),
			GETDATE(),
			@FechaVencimiento,
			0,
			@Valuacion,
			@TOTAL,
			@lote
			)

		WHILE (@contador < 12)
		BEGIN
			SET @contador += 1

			INSERT INTO IMPUESTOINMOBILIARIO (
				Mes,
				Año,
				FechaEmision,
				FechaVencimiento,
				Estado,
				ImporteBase,
				ImporteFinal,
				IdLote
				)
			VALUES (
				@contador,
				YEAR(GETDATE()),
				GETDATE(),
				@FechaVencimiento,
				0,
				@Valuacion,
				@MONTO,
				@lote
				)

			SET @FechaVencimiento = dateadd(mm, 1, @FechaVencimiento)
		END

		FETCH NEXT
		FROM MI_CURSOR
		INTO @lote,
			@Valuacion,
			@Esquina,
			@Principal;

		SET @contador = 0
	END

	INSERT INTO CONTROL_PROCESOS (
		IdProceso,
		FechaEjecucion
		)
	VALUES (
		1,
		GETDATE()
		)

	CLOSE MI_CURSOR

	-- LIBERAR EL RECURSO
	DEALLOCATE MI_CURSOR;
END