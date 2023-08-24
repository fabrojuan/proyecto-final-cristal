using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos;

public partial class ValuacionInmobiliario
{
    public int Idvaluacion { get; set; }

    public DateTime? FechaDesde { get; set; }

    public int? Bhabilitado { get; set; }

    public decimal? IncrementoEsquina { get; set; }

    public decimal? IncrementoAsfalto { get; set; }

    public decimal? ValorSupTerreno { get; set; }

    public decimal? ValorSupEdificada { get; set; }
}
