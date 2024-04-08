using MensajesClientHTTP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MensajesClientHTTP.Services
{
    public class DiscoveryService
    {
        public DiscoveryService()
        {
            SolicitarServidores();
            new Thread(RecibirServidores) { IsBackground = true }.Start();
        }

        void SolicitarServidores()
        {
            //preguntar qué servidores ya están contados cuando se ejecute el cliente.
            UdpClient cliente = new()
            {
                EnableBroadcast = true
            };
            cliente.Send(new byte[] { 1 }, 1, new IPEndPoint(IPAddress.Broadcast, 7001));
            cliente.Close();
        }

        UdpClient cliente = new();

        public event EventHandler<ServerModel>? ServidorRecibido;

        void RecibirServidores()
        {
            while (true)
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] buffer = cliente.Receive(ref endPoint);

                ServerModel server = new()
                {
                    NombreServer = Encoding.UTF8.GetString(buffer),
                    IPEndPoint = endPoint,
                    KeepAlive = DateTime.Now
                };

                Application.Current.Dispatcher.Invoke(() =>
                {
                    ServidorRecibido?.Invoke(this, server);
                });
            }
        }
    }
}
