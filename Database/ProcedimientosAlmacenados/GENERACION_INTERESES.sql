USE [M_VPSA_V3]
GO

/****** Object:  StoredProcedure [dbo].[GENERACION_INTERESES]    Script Date: 24/3/2023 21:54:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Batch submitted through debugger: SQLQuery2.sql|7|0|C:\Users\roman\AppData\Local\Temp\~vs4668.sql
-- Batch submitted through debugger: SQLQuery2.sql|7|0|C:\Users\roman\AppData\Local\Temp\~vs2493.sql
-- Batch submitted through debugger: SQLQuery2.sql|7|0|C:\Users\roman\AppData\Local\Temp\~vs10C2.sql
-- Batch submitted through debugger: SQLQuery2.sql|7|0|C:\Users\roman\AppData\Local\Temp\~vs4860.sql
-- Batch submitted through debugger: SQLQuery4.sql|9|0|C:\Users\roman\AppData\Local\Temp\~vs825D.sql
CREATE
	OR

ALTER PROCEDURE [dbo].[GENERACION_INTERESES]
AS
-- DECLARACIÓN DE VARIABLES DE TRABAJO
DECLARE @id_impuesto INT,
	@importe_final DECIMAL
DECLARE @contador INT = 0;
DECLARE @anio VARCHAR(4);

SET @anio = YEAR(GetDate());

DECLARE @mes VARCHAR(2);

SET @mes = MONTH(GetDate());

DECLARE @dia VARCHAR(2);

SET @dia = DAY(GetDate());

DECLARE @result BIT;

SET @result = 0;

-- DECLARACIÓN DEL CURSOR
DECLARE MI_CURSOR CURSOR
FOR
SELECT I.IdImpuesto AS id_impuesto,
	I.ImporteFinal AS importe_final
FROM IMPUESTOINMOBILIARIO I
WHERE I.Año = @anio
	AND I.mes <= @mes
	AND I.Estado = 0
	AND @dia > 1

-- APERTURA DEL CURSOR
OPEN MI_CURSOR

-- LECTURA DEL PRIMER REGISTRO
DECLARE @importe_int DECIMAL
DECLARE @interes DECIMAL

FETCH NEXT
FROM MI_CURSOR
INTO @id_impuesto,
	@importe_final;

-- RECORRER EL CURSOS MIENTRAS HAYAN REGISTROS
WHILE @@FETCH_STATUS = 0
BEGIN
	SET @importe_int = 0
	SET @interes = 0
	SET @importe_int = @importe_final * 1.03
	SET @interes = @importe_int - @importe_final

	UPDATE IMPUESTOINMOBILIARIO
	SET InteresValor = @interes + InteresValor,
		ImporteFinal = @importe_int
	WHERE IdImpuesto = @id_impuesto

	SET @contador = + 1;

	FETCH NEXT
	FROM MI_CURSOR
	INTO @id_impuesto,
		@importe_final;
END

IF (@contador > 0)
	INSERT INTO CONTROL_PROCESOS (IdProceso)
	VALUES (2)

SET @result = 1;

CLOSE MI_CURSOR

-- LIBERAR EL RECURSO
DEALLOCATE MI_CURSOR;

SELECT @result AS Result