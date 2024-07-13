using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace MVPSA_V2022.Modelos;

public partial class MensajesError
{
    public const String USUARIO_CONTRASENIA_NO_VALIDOS = "Usuario y/o contraseña no válidos";
    public const String EMAIL_YA_REGISTRADO = "La dirección de correo electrónico ya se encuentra registrada";
    public const String LOTE_NO_ENCONTRADO = "No se encontró el número de cuenta";
    public const String ERROR_INESPERADO = "Ha ocurrido un error, por favor intente nuevamente más tarde";
    public const String TIPO_RECLAMO_SIENDO_USADO = "El tipo de requerimiento está siendo utilizado";
}
