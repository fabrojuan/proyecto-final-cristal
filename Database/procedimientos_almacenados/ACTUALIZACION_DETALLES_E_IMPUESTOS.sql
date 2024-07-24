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

    ------------------ Si se pago una cuota mensual deben anularse la cuota unica -----------------
    UPDATE IMPUESTOINMOBILIARIO
    SET Estado = 3
    FROM IMPUESTOINMOBILIARIO ii
    INNER JOIN (
        SELECT ii.IdLote, ii.Año
        FROM BOLETA bol
        INNER JOIN DETALLEBOLETA det ON bol.IdBoleta = det.IdBoleta
        INNER JOIN IMPUESTOINMOBILIARIO ii ON ii.IdImpuesto = det.IdImpuesto
        WHERE bol.IdBoleta = @IdBoleta_par
        AND ii.Mes != 0
    ) subquery ON ii.IdLote = subquery.IdLote AND ii.Año = subquery.Año
    WHERE ii.Mes = 0;

    ------------------ Si se pago una cuota unica deben anularse las mensuales -----------------
    UPDATE ii
    SET ii.Estado = 3
    FROM IMPUESTOINMOBILIARIO ii
    INNER JOIN (
        SELECT ii.IdLote, ii.Año 
        FROM BOLETA bol
        INNER JOIN DETALLEBOLETA det ON det.IdBoleta = bol.IdBoleta
        INNER JOIN IMPUESTOINMOBILIARIO ii ON ii.IdImpuesto = det.IdImpuesto
        WHERE bol.IdBoleta = @IdBoleta_par
        AND ii.Mes = 0
    ) subquery ON ii.IdLote = subquery.IdLote AND ii.Año = subquery.Año
    WHERE ii.Mes != 0;
	
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
