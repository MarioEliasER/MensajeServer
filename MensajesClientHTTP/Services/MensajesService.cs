using MensajesClientHTTP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MensajesClientHTTP.Services
{
    public class MensajesService
    {
        public async void EnviarMensaje(ServerModel server, MensajeDTO mensaje)
        {
            //querystring: mandar los datos por la url.
            var url = $"http://{server.IPEndPoint?.Address.ToString()}:7002/mensajitos/?texto={mensaje.Texto}&colorletra={HttpUtility.UrlEncode(mensaje.ColorLetra)}&colorfondo={HttpUtility.UrlEncode(mensaje.ColorFondo)}";
            HttpClient cliente = new();
            await cliente.GetStringAsync(url);
        }
    }
}
