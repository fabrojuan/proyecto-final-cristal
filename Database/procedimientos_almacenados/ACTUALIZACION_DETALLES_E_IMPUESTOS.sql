CREATE
	OR

ALTER PROCEDURE [dbo].[ACTUALIZACION_DETALLES_E_IMPUESTOS]
	-- DECLARACIÓN DE VARIABLES DE TRABAJO
	(
	@IdBoleta_par INT,
	@mail VARCHAR(50)
	)
AS
BEGIN
	DECLARE @IdBoleta INT
	DECLARE @Importe DECIMAL
	DECLARE @FechaPago VARCHAR(8)

	SET @FechaPago = GetDate()

	DECLARE CURSOR_BOLETA CURSOR
	FOR
	SELECT IdBoleta,
		importe
	FROM BOLETA
	WHERE IdBoleta = @IdBoleta_par

	-- APERTURA DEL CURSOR
	OPEN CURSOR_BOLETA

	FETCH NEXT
	FROM CURSOR_BOLETA
	INTO @IdBoleta;

	WHILE @@FETCH_STATUS = 0
	BEGIN
		UPDATE BOLETA
		SET FechaPago = @FechaPago
		WHERE IdBoleta = @IdBoleta

		FETCH NEXT
		FROM CURSOR_BOLETA
		INTO @IdBoleta;
	END

	CLOSE CURSOR_BOLETA

	DEALLOCATE CURSOR_BOLETA

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

	-- RECORRER EL CURSOS MIENTRAS HAYAN REGISTROS
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

	INSERT INTO RECIBO (
		BHabilitado,
		IdBoleta,
		Importe,
		Mail
		)
	VALUES (
		1,
		@IdBoleta_par,
		@Importe,
		@Mail
		);

	-- DECLARACIÓN DEL CURSOR
	DECLARE @body_1 VARCHAR
	DECLARE @IdRecibo INT

	SET @IdRecibo = (
			SELECT IdRecibo
			FROM RECIBO
			WHERE IdBoleta = @IdBoleta_par
			)
	SET @body_1 = N'<body><p>Estimado vecino, se ha emitido el recibo </p><p>correspondiente a la boleta </p></body>'
	SET @body_1 = @body_1 + CAST(@IdRecibo AS VARCHAR(50))

	EXEC msdb.dbo.sp_send_dbmail @profile_name = 'MVPSA',
		@recipients = @mail,
		@subject = 'Recibo de Pago Realizado',
		@body = @body_1,
		@body_format = 'HTML';
END