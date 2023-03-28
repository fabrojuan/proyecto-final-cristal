SET QUOTED_IDENTIFIER ON
GO

CREATE
	OR

ALTER PROCEDURE [dbo].[PRUEBADEMAIL]
AS
DECLARE @Importe DECIMAL

SET @Importe = 10

DECLARE @mail VARCHAR

SET @mail = 'mazzottaines@gmail.com'

DECLARE @body_1 VARCHAR
DECLARE @IdRecibo INT

SET @IdRecibo = (
		SELECT IdRecibo
		FROM RECIBO
		WHERE IdBoleta = 1
		)
SET @body_1 = N'<body><p>Estimado vecino, se ha emitido el recibo '

-- set @body_1 = @body_1 + @IdRecibo
-- set @body_1 = @body_1 + '</p><p>correspondiente a la boleta </p><p>por $ '
-- set @body_1 = @body_1 + @Importe
-- set @body_1 = @body_1 + '</p></body>'
EXEC msdb.dbo.sp_send_dbmail @profile_name = 'MVPSA',
	@recipients = @mail,
	@subject = 'Recibo de Pago Realizado',
	@body = N'<body><p><H3>Estimado vecino, se ha emitido el recibo </H3></p></body>',
	@body_format = 'HTML';