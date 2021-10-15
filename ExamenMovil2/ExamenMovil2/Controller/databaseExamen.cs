using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ExamenMovil2.Models;
using SQLite;

namespace ExamenMovil2.Controller
{
    public class databaseExamen
    {
        readonly SQLiteAsyncConnection db;

        //Constructor de la clase DataBaseSqlite
        public databaseExamen(String pathdb)
        {
            //Crear una conexion a la Base de datos
            db = new SQLiteAsyncConnection(pathdb);

            //Creacion de la tabla personas dentro de SQLite
            db.CreateTableAsync<Localizacion>().Wait();
        }

        //Operaciones CRUD con SQLite
        //READ List Way

        public Task<List<Localizacion>> ObtenerListaLocalizacion()
        {
            return db.Table<Localizacion>().ToListAsync();
        }

        // READ one by one
        public Task<Localizacion> ObtenerLocalizacion(int idLocalizacion)
        {
            return db.Table<Localizacion>()
                .Where(i => i.id == idLocalizacion)
                .FirstOrDefaultAsync();
        }

        //CREATE
        public Task<int> GrabarLocalizacion(Localizacion localizacion)
        {
            if (localizacion.id != 0)
            {
                return db.UpdateAsync(localizacion);
            }
            else
            {
                return db.InsertAsync(localizacion);
            }
        }

        //DELETE
        public Task<int> EliminarLocalizacion(Localizacion localizacion)
        {
            return db.DeleteAsync(localizacion);
        }
    }
}