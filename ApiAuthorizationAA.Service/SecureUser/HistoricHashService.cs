
namespace ApiAuthorizationAA.Service.Secure
{
    using ApiAuthorizationAA.Common.IPersistence.Secure;
    using ApiAuthorizationAA.Common.IService.Secure;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class HistoricHashService : IHistoricHashService
    {
        private readonly IHistoricHashPersistence historicHashPersistence;

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="historicHashPersistence"></param>
        public HistoricHashService(IHistoricHashPersistence historicHashPersistence)
        {
            this.historicHashPersistence = historicHashPersistence;
        }
        #endregion

        #region Public Method

        #endregion
    }
}
