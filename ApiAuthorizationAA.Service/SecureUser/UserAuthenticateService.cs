
namespace ApiAuthorizationAA.Service.Secure
{
    using Common.Dto;
    using EncryptConfiguration;
    using Model.Context.Authenticate;
    using Model.Entities.EncryptConfiguration;
    using Model.Entities.User;
    using Persistence.SecureUser;
    using SecureUser;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public class UserAuthenticateService : IUserAuthenticateService
    {
        #region Fields
        /// <summary>
        /// 
        /// </summary>
        private readonly IControlEncryptPersistence controlEncryptPersistence;

        /// <summary>
        /// 
        /// </summary>
        private readonly IUserHistoricHashPersistence userHistoricHashPersistence;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controlEncryptPersistence"></param>
        public UserAuthenticateService(IControlEncryptPersistence controlEncryptPersistence)
        {
            this.controlEncryptPersistence = controlEncryptPersistence;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Autentica usuarios utilizando usuario y contraseña
        /// </summary>
        /// <param name="userEntity"></param>
        public void AuthenticateUser(UserEntity userEntity)
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Valida si la nueva contraseña coincide con algun historico
        /// </summary>
        /// <param name="userEntity"></param>
        public void ValidateHistoricPassword(UserEntity userEntity)
        {

        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Para validar si el password ha sido utilizado en las 5 ultimas actualizaciones
        /// </summary>
        /// <returns></returns>
        private async Task<ResponseDto<bool>> ValidateProcessPassword(UserEntity userEntity, string idSiaraWebUser, int pageSize)
        {
            ResponseDto<bool> response = null;

            try
            {
                // Get last historic records
                ResponseDto<ICollection<SiaraHistoricHash>> historicRecords = await GetHistoricUserHashesAsync(idSiaraWebUser, pageSize);

                if (historicRecords != null && historicRecords.Response.Count() > 0)
                {
                    ResponseDto<ControlEncryptEntity> controlEncrypt = null;

                    // foreach one by one historic and get control encrypt
                    foreach (SiaraHistoricHash siaraHistoricHash in historicRecords.Response)
                    {
                        // Get control info by id
                        controlEncrypt = await GetControlEncrypt(siaraHistoricHash.IdControlEncrypt);

                        if (controlEncrypt != null && !controlEncrypt.IsError)
                        {
                            // Compare 
                            CompareNewPasswordWithHistoricRecords(userEntity, controlEncrypt.Response, siaraHistoricHash);
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                response = new ResponseDto<bool>($"Ocurrió un error al validar historial de contraseñas para el usuario {idSiaraWebUser}", ex);
            }

            return response;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="idControlEncrypt"></param>
        /// <returns></returns>
        private async Task<ResponseDto<ControlEncryptEntity>> GetControlEncrypt(int idControlEncrypt)
        {
            ResponseDto<ControlEncryptEntity> response = null;

            try
            {
                response = await controlEncryptPersistence.GetControlEncryptByIdAsync(idControlEncrypt);
            }
            catch (Exception ex)
            {
                response = new ResponseDto<ControlEncryptEntity>("Ocurrio un error al consultar configuración de cifrado.", ex);
            }

            return response;
        }

        /// <summary>
        /// Get last 5 user records
        /// </summary>
        /// <param name="idSiaraWebUser"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private async Task<ResponseDto<ICollection<SiaraHistoricHash>>> GetHistoricUserHashesAsync(string idSiaraWebUser, int pageSize)
        {
            ResponseDto<ICollection<SiaraHistoricHash>> response = null;

            try
            {
                response = await userHistoricHashPersistence.GetUserHistoricHashAsync(idSiaraWebUser, pageSize);
            }
            catch (Exception ex)
            {
                response = new ResponseDto<ICollection<SiaraHistoricHash>>($"Ocurrió un error al obtener los registros historicos del usuario {idSiaraWebUser}", ex);
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="controlEncryptEntity"></param>
        /// <param name="siaraHistoricHash"></param>
        private ResponseDto<bool> CompareNewPasswordWithHistoricRecords(UserEntity userEntity, ControlEncryptEntity controlEncryptEntity, SiaraHistoricHash siaraHistoricHash)
        {
            ResponseDto<bool> status = null;

            try
            {
                bool isMatch = false;



                status = new ResponseDto<bool>(isMatch);
            }
            catch (Exception ex)
            {
                status = new ResponseDto<bool>("Ocurrió un error en el proceso de cifrado ", ex);
            }

            return status;
        }
        #endregion
    }
}
