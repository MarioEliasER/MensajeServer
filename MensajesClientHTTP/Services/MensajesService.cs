using MensajesClientHTTP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MensajesClientHTTP.Services
{
    public class MensajesService
    {
        public async void EnviarMensaje(ServerModel server, MensajeDTO mensaje)
        {
            var url = $"http://{server.IPEndPoint.Address.ToString()}:7002/mensajitos/?texto={mensaje.Texto}&colorletra={mensaje.ColorLetra}&colorfondo={mensaje.ColorFondo}";
            HttpClient cliente = new();
            await cliente.GetStringAsync(url);
        }
    }
}
