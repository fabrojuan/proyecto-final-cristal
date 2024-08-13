using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos;

public partial class RecuperacionCuentum
{
    public int IdRecuperacionCuenta { get; set; }

    public int IdUsuario { get; set; }

    public string Mail { get; set; } = null!;

    public string Uuid { get; set; } = null!;

    public DateTime FechaAlta { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
