using CommunityToolkit.Mvvm.ComponentModel;
using MensajesClientHTTP.Models;
using MensajesClientHTTP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensajesClientHTTP.ViewModels
{
    public class MensajesViewModel : ObservableObject
    {
        //Servicios para que mi app reciba servidores y envie mensajes.
        MensajesService mensajesService = new();
        DiscoveryService discoveryService = new();

        public ObservableCollection<ServerModel> Servidores { get; set; } = new();

        public MensajesViewModel()
        {
            discoveryService.ServidorRecibido += DiscoveryService_ServidorRecibido;
        }

        private void DiscoveryService_ServidorRecibido(object? sender, ServerModel e)
        {
            var server = Servidores.FirstOrDefault(x=>x.NombreServer == e.NombreServer);
            if (server == null)
            {
                //Agregar si no está
                Servidores.Add(e);
            }
            else
            {
                //Editar el keepalive si está
                server.KeepAlive = e.KeepAlive;
            }
            foreach (var s in Servidores.ToList())
            {
                //Eliminar si excedió el keepalive
                if ((DateTime.Now - e.KeepAlive).TotalSeconds > 30)
                {
                    Servidores.Remove(s);
                }
            }
        }
    }
}
