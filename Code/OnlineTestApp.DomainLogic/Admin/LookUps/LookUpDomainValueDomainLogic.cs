using OnlineTestApp.DataAccess.LookUps;
using System;

namespace OnlineTestApp.DomainLogic.Admin.LookUps
{
    public class LookUpDomainValueDomainLogic : BaseClasses.DomainLogicBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookUpDomainId"></param>
        /// <returns></returns>
        public ViewModel.LookUps.ViewLookUpsViewModel GetLookUpDomainValuesByLookUpDomainCode(ViewModel.LookUps.ViewLookUpsViewModel viewLookUpsViewModel, Enums.LookUps.LookUpDomainCode lookUpDomainCode)
        {
            using (LookUpDomainValueDataAccess obj = new LookUpDomainValueDataAccess())
            {
                return obj.GetLookUpDomainValuesByLookUpDomainCode(viewLookUpsViewModel, lookUpDomainCode);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookUpDomainValues"></param>
        /// <returns></returns>
        public bool AddNewLookUpValue(Domain.LookUps.LookUpDomainValues lookUpDomainValues)
        {
            using (LookUpDomainValueDataAccess obj = new LookUpDomainValueDataAccess())
            {
                lookUpDomainValues.FkCreatedBy = UserVariables.LoggedInUserId;
                return obj.AddNewLookUpValue(lookUpDomainValues);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookUpDomainValueId"></param>
        /// <returns></returns>
        public Domain.LookUps.LookUpDomainValues GetLookUpDomainValueById(Guid lookUpDomainValueId)
        {
            using (LookUpDomainValueDataAccess obj = new LookUpDomainValueDataAccess())
            {
                return obj.GetLookUpDomainValueById(lookUpDomainValueId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookUpDomainValues"></param>
        /// <returns></returns>
        public bool UpdateNewLookUpValue(Domain.LookUps.LookUpDomainValues lookUpDomainValues)
        {
            using (LookUpDomainValueDataAccess obj = new LookUpDomainValueDataAccess())
            {
                lookUpDomainValues.LookUpDomainCode = lookUpDomainValues.LookUpDomainValue;
                return obj.UpdateNewLookUpValue(lookUpDomainValues);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookUpDomainValueId"></param>
        /// <param name="loggedInUserId"></param>
        public void DeleteLookUpValue(Guid lookUpDomainValueId)
        {
            using (LookUpDomainValueDataAccess obj = new LookUpDomainValueDataAccess())
            {
                obj.DeleteLookUpValue(lookUpDomainValueId, UserVariables.LoggedInUserId);
            }
        }
    }
}
