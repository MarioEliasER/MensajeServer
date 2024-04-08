using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MensajeServer.Models;
using MensajeServer.Services;

namespace MensajeServer.ViewModels
{
    public partial class MensajesViewModel : ObservableObject
    {
        [ObservableProperty]
        private Mensaje mensaje;

        MensajesService server = new();
        DiscoveryService discoveryService = new();

        public MensajesViewModel()
        {
            server.MensajeRecibido += Server_MensajeRecibido;

        }

        private void Server_MensajeRecibido(object? sender, Mensaje e)
        {
            Mensaje = e;
        }

        //[RelayCommand]
        //private void Cancelar()
        //{

        //}
    }
}
