using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using TestMovil.Models;
using TestMovil.Services;
using Xamarin.Forms;

namespace TestMovil.ViewModels
{
    public class ClienteViewModel : BaseViewModel
    {
        public ICommand Guardar { get; set; }
        public ICommand Eliminar { get; set; }
        public ICommand Modificar { get; set; }
        public ICommand GetCliente { get; set; }
        public ICommand GetClientes { get; set; }

        public ObservableCollection<Cliente> Lista { get; set; }
        private Cliente cliente;

        public Cliente Cliente
        {
            get { return cliente; }
            set
            {
                cliente = value; OnPropertyChanged();
            }
        }

        public ClienteServices Services { get; set; }
        public ClienteViewModel()
        {
            Cliente = new Cliente();
            Services = new ClienteServices();
            Commandos();
        }


        private void Commandos()
        {
            Guardar = new Command(async () =>
            {
                var codigoCliente = await Services.AddItemAsync(Cliente);

            });

            Eliminar = new Command(async () =>
            {
                var paso = await Services.DeleteItemAsync(Convert.ToInt32(Cliente.Codigo));
            });

            Modificar = new Command(async () =>
           {
               var paso = await Services.UpdateItemAsync(Cliente);
           });

            GetCliente = new Command(async () =>
            {
                Cliente = await Services.GetItemAsync(Convert.ToInt32(Cliente.Codigo));
            });

            GetClientes = new Command(async () =>
           {
               Lista = new ObservableCollection<Cliente>(await Services.GetItemsAsync());
           });
        }


    }
}
