export enum TipoImagenEnum {
    ICONO,
    FOTO
}   

export interface Titulo {
    nombreComponente: string;
    titulo: string;
    tipoImagen: TipoImagenEnum,
    // Si se elige tipoImagen == TipoImagenEnum.ICONO entonces debe darse a pathImagen el nombre de las clases del icono boostrap. 
    // Ej. "bi bi-asterisk"
    // Si se elige tipoImagen == TipoImagenEnum.FOTO entonces debe darse a pathImagen la ruta a la imagen que contiene la foto. 
    pathImagen: string;
    link: string;
    tituloLink: string;
}

