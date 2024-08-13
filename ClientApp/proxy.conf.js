const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
    env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:58274';

const PROXY_CONFIG = [{
    context: [
        "/api/usuarios",
        "/api/denuncias",
        "/api/Denuncia",
        "/api/vecinos",
        "/api/roles",
        "/api/impuestos",
        "/api/reclamos",
        "/api/Trabajo",
        "/api/PruebaGrafica",
        "/api/paginas",
        "/api/Lote",
        "/api/pagos",
        "/api/personas",
        "/api/DatosAbiertos",
        "/api/Indicadores",
        "/api/areas",
        "/api/Sugerencia"
    ],
    target: target,
    secure: false,
    headers: {
        Connection: 'Keep-Alive'
    }
}]

module.exports = PROXY_CONFIG;
