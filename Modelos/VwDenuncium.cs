using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos;
[Keyless]
public partial class VwDenuncium
{
    public int NroDenuncia { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Descripcion { get; set; }

    public int? CodTipoDenuncia { get; set; }

    public int? CodEstadoDenuncia { get; set; }

    public int? Bhabilitado { get; set; }

    public string? Calle { get; set; }

    public string? Altura { get; set; }

    public string? EntreCalles { get; set; }

    public int? IdUsuario { get; set; }

    public string? MailNotificacion { get; set; }

    public int? NroPrioridad { get; set; }

    public string? MovilNotificacion { get; set; }

    public string? NombreInfractor { get; set; }

    public string? ApellidoInfractor { get; set; }

    public string? TipoDenuncia { get; set; }

    public string? EstadoDenuncia { get; set; }

    public string? PrioridadDenuncia { get; set; }

    public string? Usuario { get; set; }

    public string Empleado { get; set; } = null!;
}
