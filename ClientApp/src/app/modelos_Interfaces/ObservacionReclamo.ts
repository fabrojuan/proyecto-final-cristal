export interface ObservacionReclamo {
    id: number;
    nroReclamo: number;
    observacion: string;
    codEstadoReclamo: number,
    estadoReclamo: string;
    idUsuarioAlts: number;
    usuarioAlta: string;
    fechaAlta: Date;
    codAccion: string;
}