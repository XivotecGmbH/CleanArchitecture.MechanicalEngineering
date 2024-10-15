const {env} = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:7252';

const PROXY_CONFIG = [
  {
    context: [
      "/api/recipe",
      "/api/featherdevice",
      "/api/todolist",
      "/api/todoitem",
      "/api/settings",
      "/api/process",
      "/api/notification",
      "/api/temperature",
      "/api/hubs/notification",
      "/api/hubs/device/connectionstate",
      "/api/hubs/device/distance",
      "/api/hubs/device/lumen",
      "/api/hubs/device/temperature",
    ],
    target,
    secure: false,
    ws: true
  }
]

module.exports = PROXY_CONFIG;
