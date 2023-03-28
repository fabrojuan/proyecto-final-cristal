USE [M_VPSA_V3]
GO

/****** Object:  StoredProcedure [dbo].[ACTUALIZACION_DETALLESBOLETA]    Script Date: 24/3/2023 21:52:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE
	OR

ALTER PROCEDURE [dbo].[ACTUALIZACION_DETALLESBOLETA] (@IdBoleta_par INT)
AS
BEGIN
	DECLARE @IdDetalleBoleta INT
	DECLARE @ImporteFinal DECIMAL
	DECLARE @ImporteBoleta DECIMAL = 0

	DECLARE CURSOR_DETALLEBOLETA CURSOR
	FOR
	SELECT D.IdDetalleBoleta,
		I.ImporteFinal
	FROM DETALLEBOLETA D
	JOIN IMPUESTOINMOBILIARIO I ON I.IdImpuesto = D.IdImpuesto
	WHERE D.IdBoleta = @IdBoleta_par

	-- APERTURA DEL CURSOR
	OPEN CURSOR_DETALLEBOLETA

	FETCH NEXT
	FROM CURSOR_DETALLEBOLETA
	INTO @IdDetalleBoleta,
		@ImporteFinal;

	WHILE @@FETCH_STATUS = 0
	BEGIN
		UPDATE DETALLEBOLETA
		SET Importe = @ImporteFinal
		WHERE IdDetalleBoleta = @IdDetalleBoleta

		SET @ImporteBoleta = @ImporteBoleta + @ImporteFinal

		FETCH NEXT
		FROM CURSOR_DETALLEBOLETA
		INTO @IdDetalleBoleta,
			@ImporteFinal;
	END

	CLOSE CURSOR_DETALLEBOLETA

	DEALLOCATE CURSOR_DETALLEBOLETA

	DECLARE @FechaVencimiento VARCHAR(8)
	DECLARE @FechaHoy VARCHAR(8)

	SET @FechaHoy = GetDate()
	SET @FechaVencimiento = dateadd(dd, 1, @FechaHoy)

	UPDATE BOLETA
	SET importe = @ImporteBoleta,
		FechaVencimiento = @FechaVencimiento,
		url = 'ACTUALIZA'
	WHERE IdBoleta = @IdBoleta_par
END