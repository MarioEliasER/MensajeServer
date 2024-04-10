using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MensajesClientHTTP.Models;
using MensajesClientHTTP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MensajesClientHTTP.ViewModels
{
    public partial class MensajesViewModel : ObservableObject
    {
        //Servicios para que mi app reciba servidores y envie mensajes.
        MensajesService mensajesService = new();
        DiscoveryService discoveryService = new();

        public MensajeDTO Mensajes { get; set; } = new();
        public ServerModel Seleccionado { get; set; }

        [RelayCommand]
        private void Enviar()
        {
            mensajesService.EnviarMensaje(Seleccionado, Mensajes);
        }

        public List<SolidColorBrush> colores { get; set; } = new List<SolidColorBrush>();

        public ObservableCollection<ServerModel> Servidores { get; set; } = new();

        public MensajesViewModel()
        {
            foreach (var propiedad in typeof(Brushes).GetProperties())
            {
                colores.Add((SolidColorBrush)(propiedad.GetValue(null)??new SolidColorBrush()));
            }
            //colores = typeof(Brushes).GetProperties().Select(c => (SolidColorBrush)(c.GetValue(null) ?? new SolidColorBrush())).ToList();
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
