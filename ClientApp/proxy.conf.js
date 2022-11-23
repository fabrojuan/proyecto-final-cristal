const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:58274';

const PROXY_CONFIG = [
  {
    context: [
      "/api/Usuario",
      "/api/denuncias",
      "/api/vecino",
      "/api/Rol",
      "/api/Impuestos",
      "/api/reclamos",
      "/api/Trabajo",
      "/api/PruebaGrafica",
      "/api/Pagina",
      "/api/Vecino",
      "/api/Lote",
      "/api/pagos",
      "/api/Persona"
    ],
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIG;
