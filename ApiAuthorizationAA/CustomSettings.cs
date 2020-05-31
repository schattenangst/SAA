

namespace ApiAuthorizationAA
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    //using Cnbv.Sait.Configuration;

    /// <summary>
    /// Almacena las configuraciónes de la aplicación.
    /// </summary>
    /// <threadsafety>
    /// Cualquier miembro publico estático de este tipo se consideran seguros para múltiples 
    /// hilos de ejecución. No se garantiza que sean seguros cualquiera de los miembros de 
    /// instancia.
    /// </threadsafety>
    internal static class CustomSettings
    {
        #region Properties
        /// <summary>
        /// Almacena un objeto <see cref="DatabaseServerConfigurationSection"/> con los datos necesarios 
        /// para conectarse al servidor de base de datos STIV.
        /// </summary>
        internal static DatabaseServerConfigurationSection StivServerInfo
        {
            get;
            set;
        }

        /// <summary>
        /// Almacena un objeto <see cref="DatabaseServerConfigurationSection"/> con los datos necesarios 
        /// para conectarse al servidor de base de datos SEA.
        /// </summary>
        internal static DatabaseServerConfigurationSection AutorizaServerInfo
        {
            get;
            set;
        }
        #endregion
    }
}