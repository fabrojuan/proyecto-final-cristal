const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
    env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:58274';

const PROXY_CONFIG = [{
    context: [
        "/api/usuarios",
        "/api/denuncias",
        "/api/vecinos",
        "/api/Rol",
        "/api/Impuestos",
        "/api/reclamos",
        "/api/Trabajo",
        "/api/PruebaGrafica",
        "/api/Pagina",
        "/api/Lote",
        "/api/pagos",
        "/api/Persona",
        "/api/DatosAbiertos"
    ],
    target: target,
    secure: false,
    headers: {
        Connection: 'Keep-Alive'
    }
}]

module.exports = PROXY_CONFIG;