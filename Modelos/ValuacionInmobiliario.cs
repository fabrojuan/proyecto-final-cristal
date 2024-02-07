using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos;

public partial class ValuacionInmobiliario
{
    public int Idvaluacion { get; set; }

    public DateTime? FechaDesde { get; set; }

    public int? Bhabilitado { get; set; }

    public int? IncrementoEsquina { get; set; }

    public int? IncrementoAsfalto { get; set; }

    public decimal? ValorSupTerreno { get; set; }

    public decimal? ValorSupEdificada { get; set; }
}
