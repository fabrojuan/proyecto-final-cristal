using System;
namespace MVPSA_V2022.Enums
{
	public enum EstadoReclamoEnum : uint
    {
        CREADO = 1,
        //ASIGNADO = 2,
        EN_CURSO = 3,
        CANCELADO = 4,
        SOLUCIONADO = 5,
        SUSPENDIDO = 6,
        RECHAZADO = 7,
        PENDIENTE_CIERRE = 8
    }
}

