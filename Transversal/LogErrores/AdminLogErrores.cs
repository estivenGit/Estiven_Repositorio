using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Transversal.db;
using Transversal.Entidades;
using Transversal.Interfaces;

namespace Transversal.LogErrores
{
    /// <summary>
    /// Clase que proporciona funciones para registrar y gestionar errores en la aplicación.
    /// Implementa la interfaz IAdminLogErrores.
    /// </summary>
    public class AdminLogErrores: IAdminLogErrores
    {
        public AdminLogErrores()
        {
        }

        public LogError_DTO Error;
        CatagoloProductosDBEntities _context = new CatagoloProductosDBEntities();

        /// <summary>
        /// Método para registrar un error en el log.
        /// </summary>
        /// <param name="ex">Excepción que se va a registrar.</param>
        /// <param name="classNeme">Nombre de la clase donde se produjo el error.</param>
        /// <param name="methodName">Nombre del método donde se produjo el error (por defecto, se obtiene automáticamente).</param>
        public void LogError(Exception ex, string classNeme, [CallerMemberName] string methodName = "")
        {
            newError(ex, $"{methodName} en {classNeme}");
        }

        /// <summary>
        /// Registra un nuevo error en el log.
        /// </summary>
        /// <param name="ex">Excepción que se va a registrar.</param>
        /// <param name="metodo">Nombre del método donde se produjo el error.</param>
        public void newError(Exception ex, string metodo)
        {
            this.Error = new LogError_DTO
            {
                Mensaje = ex.Message,
                Linea = ex.StackTrace,
                Ayuda_en_linea = ex.HelpLink,
                Fuente = ex.Source,
                Metodo = metodo,
                fecha = DateTime.Now
            };
            guardarError(Error);
        }

        /// <summary>
        /// Guarda un registro de error en la base de datos.
        /// </summary>
        /// <param name="error">Objeto LogError_DTO que contiene la información del error.</param>
        private void guardarError(LogError_DTO error)
        {
            Transversal.db.LogErrores entity = new Transversal.db.LogErrores
            {
                Mensaje = error.Mensaje,
                Linea = error.Linea,
                Ayuda_en_linea = error.Ayuda_en_linea,
                Fuente = error.Fuente,
                Metodo = error.Metodo,
                fecha = error.fecha
            };

            // Almacenar                
            _context.LogErrores.Add(entity);
            _context.SaveChanges();
        }
    }
}
