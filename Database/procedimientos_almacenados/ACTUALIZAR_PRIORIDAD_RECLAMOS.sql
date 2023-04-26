USE [M_VPSA_V3]
GO
/****** Object:  StoredProcedure [dbo].[ACTUALIZAR_PRIORIDAD_RECLAMOS]    Script Date: 26/4/2023 03:27:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER   PROCEDURE [dbo].[ACTUALIZAR_PRIORIDAD_RECLAMOS] 
AS
BEGIN
	DECLARE @v_nro_reclamo int;
	DECLARE @v_tiempo_max_tratamiento_actual int;
	DECLARE @v_nro_prioridad int;

	DECLARE c_reclamos CURSOR FOR 
		SELECT rec.Nro_Reclamo, 
			   tr.Tiempo_Max_Tratamiento - DATEDIFF(day, rec.fecha, getDate()) Tiempo_Max_Tratamiento_Actual
		  FROM RECLAMO rec 
		  JOIN ESTADO_RECLAMO ER ON (er.Cod_Estado_Reclamo = rec.Cod_Estado_Reclamo)
		  JOIN TIPO_RECLAMO tr ON (tr.Cod_Tipo_Reclamo = rec.Cod_Tipo_Reclamo)
		  WHERE er.Descripcion NOT IN ('Solucionado', 'Cancelado', 'Suspendido');

	OPEN c_reclamos;
	FETCH NEXT FROM c_reclamos INTO @v_nro_reclamo, @v_tiempo_max_tratamiento_actual;

	WHILE @@FETCH_STATUS = 0
	BEGIN
		select @v_nro_prioridad = (
			select TOP 1 
			   pr.Nro_Prioridad
			  from PRIORIDAD_RECLAMO pr 
			 where pr.BHabilitado = 1 
			   and @v_tiempo_max_tratamiento_actual <= pr.Tiempo_Max_Tratamiento
		  order by pr.Tiempo_Max_Tratamiento
		);

		if (@v_nro_prioridad is null) 
			select @v_nro_prioridad = (
				select TOP 1 
					   pr.Nro_Prioridad
				  from PRIORIDAD_RECLAMO pr 
				 where pr.BHabilitado = 1 
			  order by pr.Tiempo_Max_Tratamiento desc
			);
	
		update RECLAMO 
		   set Nro_Prioridad = @v_nro_prioridad
		 where Nro_Reclamo = @v_nro_reclamo;

		FETCH NEXT FROM c_reclamos INTO @v_nro_reclamo, @v_tiempo_max_tratamiento_actual;
	END;	

	CLOSE c_reclamos;
	DEALLOCATE c_reclamos;

END
