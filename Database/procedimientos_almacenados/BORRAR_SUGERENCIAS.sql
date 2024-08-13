USE [Cristal]
GO
/** Object:  StoredProcedure [dbo].[BORRAR_SUGERENCIAS]    Script Date: 6/3/2024 20:20:12 **/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Inés Mazzotta
-- Create date: 11/10/2023
-- Description:	este procedimiento deshabilita las
-- sugerencias que estén en estado 0 = REGISTRADO hace más de 30 días
-- y las que estén en 2 = DESCARTADO hace más de 15 días, 
-- en base a la fecha de registración de la sugerencia
-- =============================================
 CREATE PROCEDURE [dbo].[BORRAR_SUGERENCIAS] 
AS
BEGIN
	DECLARE @IdSugerencia INT, @FechaVencimiento_registrado DATE, @FechaVencimiento_descartado DATE, @FechaHoy DATE

	set @FechaHoy=GetDate()
    set @FechaVencimiento_registrado = DATEADD (DAY, -30, @FechaHoy)
	PRINT (@FechaVencimiento_registrado)
	set @FechaVencimiento_descartado = DATEADD (DAY, -15, @FechaHoy)
	PRINT (@FechaVencimiento_descartado)
-- DECLARACIÓN DEL CURSOR
	DECLARE CURSOR_SUGERENCIAS CURSOR FOR
	SELECT s.IdSugerencia
    from SUGERENCIA s, ESTADO_SUGERENCIA e 
	-- supongamos que REGISTRADO = 0 y DESCARTADO = 2
	where (s.Estado = e.Cod_Estado_Sugerencia and e.Nombre = 'Registrado' and s.FechaGenerada < @FechaVencimiento_registrado) or
     (s.Estado = e.Cod_Estado_Sugerencia and e.Nombre = 'Descartado' and s.FechaGenerada < @FechaVencimiento_descartado)
-- APERTURA DEL CURSOR
	OPEN CURSOR_SUGERENCIAS
-- LECTURA DEL PRIMER REGISTRO
	FETCH NEXT FROM CURSOR_SUGERENCIAS INTO @IdSugerencia;
-- RECORRER EL CURSOR MIENTRAS HAYAN REGISTROS
	WHILE @@FETCH_STATUS=0 BEGIN
		UPDATE SUGERENCIA
		SET BHabilitado=0
		WHERE IdSugerencia = @IdSugerencia
		FETCH NEXT FROM CURSOR_SUGERENCIAS INTO @IdSugerencia;
	END 
	
	CLOSE CURSOR_SUGERENCIAS
-- LIBERAR EL RECURSO
	DEALLOCATE CURSOR_SUGERENCIAS;
END
