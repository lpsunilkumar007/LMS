using System;
using System.Linq;
using System.Data.Entity;


namespace OnlineTestApp.DataAccess.LookUps
{
    public class LookUpDomainValueDataAccess : BaseClasses.DataAccessBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookUpDomainValues"></param>
        /// <returns></returns>
        public bool AddNewLookUpValue(Domain.LookUps.LookUpDomainValues lookUpDomainValues)
        {
            bool isExists = _DbContext.LookUpDomainValues.Where(x => x.LookUpDomainValue ==
            lookUpDomainValues.LookUpDomainValue
            && x.FkLookUpDomainId == lookUpDomainValues.FkLookUpDomainId).Any();

            if (isExists) return false;

            _DbContext.LookUpDomainValues.Add(lookUpDomainValues);
            _DbContext.SaveChanges(createLog: false);

            return true;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="lookUpDomainValueId"></param>
        /// <returns></returns>
        public Domain.LookUps.LookUpDomainValues GetLookUpDomainValueById(Guid lookUpDomainValueId)
        {
            return _DbContext.LookUpDomainValues.Where(x => x.LookUpDomainValueId ==
            lookUpDomainValueId && x.IsDeleted == false).Single();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookUpDomainValues"></param>
        /// <returns></returns>
        public bool UpdateNewLookUpValue(Domain.LookUps.LookUpDomainValues lookUpDomainValues)
        {
            var originalData = _DbContext.LookUpDomainValues.Where(x => x.LookUpDomainValueId ==
            lookUpDomainValues.LookUpDomainValueId && x.IsDeleted == false).Single();

            lookUpDomainValues.FkLookUpDomainId = originalData.FkLookUpDomainId;

            bool isExists = _DbContext.LookUpDomainValues.Where(x => x.LookUpDomainValue == lookUpDomainValues.LookUpDomainValue
        && x.FkLookUpDomainId == originalData.FkLookUpDomainId && x.LookUpDomainValueId != lookUpDomainValues.LookUpDomainValueId).Any();

            if (isExists) return false;



            //originalData.LookUpDomainCode = lookUpDomainValues.LookUpDomainCode;
            if (originalData.CanEditLookUpDomainValue)
            {
                originalData.LookUpDomainValue = lookUpDomainValues.LookUpDomainValue;
            }
            if (originalData.CanEditLookUpDomainValueText)
            {
                originalData.LookUpDomainValueText = lookUpDomainValues.LookUpDomainValueText;
            }
            originalData.DisplayOrder = lookUpDomainValues.DisplayOrder;
            originalData.IsActive = lookUpDomainValues.IsActive;
            _DbContext.Entry(originalData).State = EntityState.Modified;
            _DbContext.SaveChanges(createLog: true);

            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookUpDomainId"></param>
        /// <returns></returns>
        public ViewModel.LookUps.ViewLookUpsViewModel GetLookUpDomainValuesByLookUpDomainCode(ViewModel.LookUps.ViewLookUpsViewModel viewLookUpsViewModel, Enums.LookUps.LookUpDomainCode lookUpDomainCode)
        {
            var query = _DbContext.LookUpDomains.Include(x => x.LstLookUpDomainValue).
                Where(x => x.LookUpDomainCode == lookUpDomainCode && x.IsDeleted == false).Single();

            viewLookUpsViewModel.LookUpDomain = query;
            viewLookUpsViewModel.TotalRecords = query.LstLookUpDomainValue.Where(x => x.IsDeleted == false).Count();
            viewLookUpsViewModel.LookUpDomain.LstLookUpDomainValue = query.LstLookUpDomainValue.Where(x => x.IsDeleted == false)
                            .Skip(viewLookUpsViewModel.SkipRecords)
                        .Take(viewLookUpsViewModel.PageSize).ToList();
            
            return viewLookUpsViewModel;


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookUpDomainId"></param>
        /// <param name="loggedInUserId"></param>
        public void DeleteLookUpValue(Guid lookUpDomainValueId, Guid loggedInUserId)
        {
            var originalData = _DbContext.LookUpDomainValues.Where(x => x.LookUpDomainValueId == lookUpDomainValueId
            && x.IsDeleted == false && x.CanDeleteRecord == true).Single();

            originalData.IsDeleted = true;
            originalData.DeletedDateDateTime = DateTime.Now;
            originalData.FkDeletedBy = loggedInUserId;
            originalData.LookUpDomainCode = lookUpDomainValueId + "__" + originalData.LookUpDomainCode;
            originalData.LookUpDomainValue = lookUpDomainValueId + "__" + originalData.LookUpDomainValue;

            _DbContext.Entry(originalData).State = EntityState.Modified;
            _DbContext.SaveChanges(createLog: false);
        }
    }
}
