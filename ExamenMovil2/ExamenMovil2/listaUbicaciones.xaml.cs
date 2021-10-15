using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ExamenMovil2.Models;

namespace ExamenMovil2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class listaUbicaciones : ContentPage
    {

        private Localizacion temporal = new Localizacion();
        public listaUbicaciones()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var listaUbicaciones = await App.BaseDatos.ObtenerListaLocalizacion();

            listUbicaciones.ItemsSource = listaUbicaciones;
        }

        private async void refrescar()
        {
            var listUbicate = await App.BaseDatos.ObtenerListaLocalizacion();

            if (listUbicate != null)
            {
                listUbicaciones.ItemsSource = listUbicate;
            }
        }

        private async void listUbicaciones_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var objetoSeleccionado = (Localizacion)e.SelectedItem;
            btnmapa.IsVisible = true;
            btneliminar.IsVisible = true;

            var idObjetoSeleccionado = objetoSeleccionado.id;

            
            if (objetoSeleccionado.id != 0)
            {
                var objetoExtraidoBasedeDatos = await App.BaseDatos.ObtenerLocalizacion(idObjetoSeleccionado);
                if (objetoExtraidoBasedeDatos != null)
                {
                    temporal.id = objetoExtraidoBasedeDatos.id;
                    temporal.latitud = objetoExtraidoBasedeDatos.latitud;
                    temporal.longitud = objetoExtraidoBasedeDatos.longitud;
                    temporal.descripcionLarga = objetoExtraidoBasedeDatos.descripcionLarga;
                    temporal.descripcionCorta = objetoExtraidoBasedeDatos.descripcionCorta;
                }
            }

        }

        private void btnmapa_Clicked(object sender, EventArgs e)
        {

        }

        private async void btneliminar_Clicked(object sender, EventArgs e)
        {
            var obtenerLocalizacion = await App.BaseDatos.ObtenerLocalizacion(temporal.id);
            if (obtenerLocalizacion != null)
            {
                await App.BaseDatos.EliminarLocalizacion(obtenerLocalizacion);

                await DisplayAlert("Eliminacion", "El registro se elimino correctamente", "OK");

                btneliminar.IsVisible = false;
                btnmapa.IsVisible = false;
                refrescar();
            }
        }
    }
}