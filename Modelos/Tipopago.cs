﻿using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos;

public partial class Tipopago
{
    public int IdTipoPago { get; set; }

    public int? Bhabilitado { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
