
namespace ApiAuthorizationAA
{
   using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

  /// <summary>
    /// Lee el archivo de configuración para actualizar las opciones de la subaplicación.
    /// </summary>
    /// <threadsafety>
    /// Cualquier miembro publico estático de este tipo se consideran seguros para múltiples 
    /// hilos de ejecución. No se garantiza que sean seguros cualquiera de los miembros de 
    /// instancia.
    /// </threadsafety>
    internal static class ReadConfiguration
    {
        #region Methods
        /// <summary>
        /// Inicializa la clase con los valores del archivo de configuración.
        /// </summary>
        /// <exception cref="System.ArgumentException">
        /// Cuando no puede leer el dominio actual.
        /// </exception>
        /// <exception cref="System.Configuration.SettingsPropertyNotFoundException">
        /// Cuando una entrada no existe en el archivo de configuración.
        /// </exception>
        internal static void Initialize()
        {
            StateValidations.ThrowIfInvalidOperation(
              () => Settings.Default.CurrentDomain.IsNullOrEmpty(), ResourcesStiv.IOEEmptyDomain);

            // Obtiene objeto de conexión para la base en SQLServer
            CustomSettings.StivServerInfo =
              ReadSqlStivConnectionInfo(Settings.Default.CurrentDomain + ResourcesStiv.ServerSectionStiv);

            // Obtiene objeto de conexión para la base en SQLServer
            CustomSettings.AutorizaServerInfo =
              ReadSqlAutorizacionesConnectionInfo(Settings.Default.CurrentDomain + ResourcesStiv.ServerSectionAutoriza);
        }

        /// <summary>
        /// Inicializa la clase <see cref="DatabaseServerConfigurationSection" /> con los valores del 
        /// usuario, aplicación y contraseña.
        /// </summary>
        /// <param name="section">
        /// Sección a reir del archivo de configuración..
        /// </param>
        /// <returns>
        /// Un objeto <see cref="DatabaseServerConfigurationSection" /> con la información del archivo 
        /// de configuración.
        /// </returns>
        private static DatabaseServerConfigurationSection ReadSqlStivConnectionInfo(string section)
        {
            DatabaseServerConfigurationSection info =
              ReadConfigurationEntry<DatabaseServerConfigurationSection>(section);
            info.Application = ResourcesStiv.DatabaseSqlAppDescription;
            info.UserId = ResourcesStiv.DatabaseSqlStivUserId;

            GenericUserService service = new GenericUserService(ResourcesStiv.DatabaseSqlStivUserId, Resources.AppResources.SubaId, Resources.AppResources.CipherKey, Resources.AppResources.ClaveDefectoStiv);
            info.Password = service.ObtainPassword();

            return info;
        }

        /// <summary>
        /// Inicializa la clase <see cref="DatabaseServerConfigurationSection" /> con los valores del 
        /// usuario, aplicación y contraseña.
        /// </summary>
        /// <param name="section">
        /// Sección a leer del archivo de configuración..
        /// </param>
        /// <returns>
        /// Un objeto <see cref="DatabaseServerConfigurationSection" /> con la información del archivo 
        /// de configuración.
        /// </returns>
        private static DatabaseServerConfigurationSection ReadSqlAutorizacionesConnectionInfo(string section)
        {
            DatabaseServerConfigurationSection info =
              ReadConfigurationEntry<DatabaseServerConfigurationSection>(section);
            info.Application = ResourcesStiv.DatabaseSqlAppDescription;
            info.UserId = ResourcesStiv.DatabaseSqlSeaUserId;

            GenericUserService service = new GenericUserService(ResourcesStiv.DatabaseSqlSeaUserId, Resources.AppResources.SubaIdSea, Resources.AppResources.CipherKeySea, Resources.AppResources.ClaveDefectoSea);
            info.Password = service.ObtainPassword();
            return info;
        }

        /// <summary>
        /// Inicializa la clase <see cref="DatabaseServerConfigurationSection" /> con los valores del 
        /// usuario, aplicación y contraseña.
        /// </summary>
        /// <param name="section">
        /// Sección a reir del archivo de configuración..
        /// </param>
        /// <returns>
        /// Un objeto <see cref="DatabaseServerConfigurationSection" /> con la información del archivo 
        /// de configuración.
        /// </returns>
        private static DatabaseServerConfigurationSection ReadSybaseConnectionInfo(string section)
        {
            DatabaseServerConfigurationSection info =
              ReadConfigurationEntry<DatabaseServerConfigurationSection>(section);
            info.Application = ResourcesStiv.DatabaseSybaseAppDescription;
            info.UserId = ResourcesStiv.DatabaseSybaseUserId;
            info.Password = ResourcesStiv.DatabaseSybaseUserPassword;
            return info;
        }

        /// <summary>
        /// Lee del archivo de configuración la entrada especificada.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo del objeto a leer.
        /// </typeparam>
        /// <param name="path">
        /// Ruta al elemento a leer en el archivo de configuración.
        /// </param>
        /// <returns>
        /// Un objeto {T} con la información del archivo de configuración.
        /// </returns>
        private static T ReadConfigurationEntry<T>(string path) where T : ConfigurationSection
        {
            return (T)ConfigurationManager.GetSection(path);
        }
        #endregion
    }
}