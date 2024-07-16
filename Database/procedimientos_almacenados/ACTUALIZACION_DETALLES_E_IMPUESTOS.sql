USE [M_VPSA_V3]
GO
/****** Object:  StoredProcedure [dbo].[ACTUALIZACION_DETALLES_E_IMPUESTOS]    Script Date: 30/4/2023 17:11:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- PROCESO DE ACTUALIZACION DE ESTADO DE DETALLES E IMPUESTOS
ALTER
	

 PROCEDURE [dbo].[ACTUALIZACION_DETALLES_E_IMPUESTOS]
	-- DECLARACIÓN DE VARIABLES DE TRABAJO
	(
	@IdBoleta_par INT,
	@mail VARCHAR(50),
	@FechaPago DATETIME
	)
AS
BEGIN
	DECLARE @IdBoleta INT
	DECLARE @Importe DECIMAL
-------------------- verifico la existencia de la boleta ------------------
	IF NOT EXISTS
		(SELECT IdBoleta
	     FROM BOLETA
	     WHERE IdBoleta = @IdBoleta_par)
		 THROW 50000, 'No existe la Boleta', 1
-------------------- busca el importe de la boleta ------------------------
    SELECT 
        @Importe = importe
    FROM 
        BOLETA
    WHERE 
         IdBoleta = @IdBoleta_par;
-------------------- actualizo la tabla boleta ------------------
	UPDATE BOLETA
	SET FechaPago = @FechaPago
	WHERE IdBoleta = @IdBoleta_par
    
-------------------- actualizo las tablas IMPUESTOINMOBILIARIO Y DETALLEBOLETA ------------------
	DECLARE @IdDetalleBoleta INT
	DECLARE @IdImpuesto INT

	-- DECLARACIÓN DEL CURSOR
	DECLARE MI_CURSOR CURSOR
	FOR
	SELECT D.IdDetalleBoleta AS IdDetalleBoleta,
		D.IdImpuesto AS IdImpuesto
	FROM DETALLEBOLETA D
	WHERE D.IdBoleta = @IdBoleta_par

	-- APERTURA DEL CURSOR
	OPEN MI_CURSOR

	-- LECTURA DEL PRIMER REGISTRO
	FETCH NEXT
	FROM MI_CURSOR
	INTO @IdDetalleBoleta,
		@IdImpuesto;

	-- RECORRER EL CURSOR MIENTRAS HAYAN REGISTROS
	WHILE @@FETCH_STATUS = 0
	BEGIN
		UPDATE DETALLEBOLETA
		SET estado = 1
		WHERE IdDetalleBoleta = @IdDetalleBoleta

		UPDATE IMPUESTOINMOBILIARIO
		SET estado = 1
		WHERE IdImpuesto = @IdImpuesto

		FETCH NEXT
		FROM MI_CURSOR
		INTO @IdDetalleBoleta,
			@IdImpuesto;
	END

	CLOSE MI_CURSOR

	-- LIBERAR EL RECURSO
	DEALLOCATE MI_CURSOR;
	
-------------------- nuevo registro en tabla RECIBO ------------------
	DECLARE @IdRecibo INT
	
	INSERT INTO RECIBO (
		Fechapago,
		BHabilitado,
		IdBoleta,
		Importe,
		Mail
		)
	VALUES (
	    @FechaPago,
		1,
		@IdBoleta_par,
		@Importe,
		@Mail
		);

-------------------- envio de mail al vecino ------------------

	SET @IdRecibo = @@IDENTITY
	-- DECLARACIÓN DEL CURSOR
	DECLARE @body_1 VARCHAR
			
	SET @body_1 = N'<body><p>Estimado vecino, se ha emitido el recibo </p>'
	SET @body_1 = @body_1 + CAST(@IdRecibo AS VARCHAR(50))
	SET @body_1 = @body_1 + N'<p>correspondiente a la boleta </p></body>'
	SET @body_1 = @body_1 + CAST(@IdBoleta_par AS VARCHAR(50))

 	EXEC msdb.dbo.sp_send_dbmail @profile_name = 'MVPSA',
 		@recipients = @mail,
 		@subject = 'Recibo de Pago Realizado',
 		@body = @body_1,
 		@body_format = 'HTML';
END
GO
