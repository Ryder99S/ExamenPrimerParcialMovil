using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ExamenMovil2.Controller;
using System.IO;

namespace ExamenMovil2
{
    public partial class App : Application
    {
        static databaseExamen basedatos;
        public static databaseExamen BaseDatos
        {
            get
            {
                if (basedatos == null)
                {
                    basedatos = new databaseExamen(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Examen.db3"));
                }

                return basedatos;
            }
        }

        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            MainPage = new NavigationPage(new menuPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
