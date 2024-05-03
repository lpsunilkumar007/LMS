using System;
using System.Collections.Generic;

namespace OnlineTestApp.DataAccess.DataLayer
{
    internal class OnlineTestAppInitializer : System.Data.Entity.CreateDatabaseIfNotExists<OnlineTestAppContext>
    {
        protected override void Seed(OnlineTestAppContext context)
        {
            Guid FkCreatedBy;

            #region Adding default Roles to database
            IList<Domain.User.ApplicationUserRoles> defaultRoles = new List<Domain.User.ApplicationUserRoles>();
            defaultRoles.Add(new Domain.User.ApplicationUserRoles { UserRoleId = Enums.User.UserRoles.SuperAdmin.ToType<short>(), UserRoleName = "System Admin", UserRoleCode = Enums.User.UserRoles.SuperAdmin, UserRoleDescription = "Super admin of webiste", IsDeleted = false });
            defaultRoles.Add(new Domain.User.ApplicationUserRoles { UserRoleId = Enums.User.UserRoles.Admin.ToType<short>(), UserRoleName = "Admin", UserRoleCode = Enums.User.UserRoles.Admin, UserRoleDescription = "Create papers", IsDeleted = false });
            defaultRoles.Add(new Domain.User.ApplicationUserRoles { UserRoleId = Enums.User.UserRoles.CompanyAdmin.ToType<short>(), UserRoleName = "Company Admin", UserRoleCode = Enums.User.UserRoles.CompanyAdmin, UserRoleDescription = "Company Admin", IsDeleted = false });

            foreach (Domain.User.ApplicationUserRoles std in defaultRoles)
            {
                context.ApplicationUserRoles.Add(std);
            }
            context.SaveChanges(createLog: false);
            #endregion

            #region Company
            Domain.Company.Companies company = new Domain.Company.Companies
            {
                CompanyName = "Super Admin Default company",
                CanDisplayCompany = false,
                IsActive = true,
                IsDeleted = false,
                OrgNo = "NA",
                Telephone = "NA",
                EmailAddress = "noemail@company.com"
            };
            context.Company.Add(company);
            context.SaveChanges(createLog: false);
            #endregion

            #region Adding System Admin User
            Domain.User.ApplicationUsers applicationUsers = new Domain.User.ApplicationUsers
            {
                UserName = "Super Admin",
                UserPassword = Utilities.Encryption.EncryptText(
                 text: "123456",
                 saltValue: "Super Admin",
                 passPhrase: SystemSettings.PasswordPassPhrase,
                 passwordIterations: SystemSettings.PasswordIterations,
                 initVector: SystemSettings.PasswordInitVector
                 ),
                FkUserRoleId = Enums.User.UserRoles.SuperAdmin.ToType<short>(),
                UserRegistrationType = Enums.User.UserRegistrationType.Manual,
                FirstName = "Super",
                LastName = "Admin",
                FkCompanyId = company.CompanyId,
                EmailAddress = "noemail@superadmin.com",
                MobileNumber = "9876144195",
            };
            context.ApplicationUsers.Add(applicationUsers);
            context.SaveChanges(createLog: false);

            //settings
            Domain.User.ApplicationUserSettings userSetting = new Domain.User.ApplicationUserSettings
            {
                ApplicationUserId = applicationUsers.ApplicationUserId,
            };
            context.ApplicationUserSettings.Add(userSetting);
            context.SaveChanges(createLog: false);


            //creating Super User //hidden ONE :P
            applicationUsers = new Domain.User.ApplicationUsers
            {
                UserName = "System User",
                UserPassword = Utilities.Encryption.EncryptText(
                 text: "123456",
                 saltValue: "Super User", passPhrase: SystemSettings.PasswordPassPhrase,
                 passwordIterations: SystemSettings.PasswordIterations,
                 initVector: SystemSettings.PasswordInitVector),
                FkUserRoleId = Enums.User.UserRoles.SuperAdmin.ToType<short>(),
                UserRegistrationType = Enums.User.UserRegistrationType.Manual,
                FirstName = "System",
                LastName = "User",
                IsSystemUser = true,
                FkCompanyId = company.CompanyId,
                EmailAddress = "systemuser@systemuser.com",
                MobileNumber = "9876144195",
            };
            context.ApplicationUsers.Add(applicationUsers);
            context.SaveChanges(createLog: false);


            FkCreatedBy = applicationUsers.ApplicationUserId;

            //settings
            userSetting = new Domain.User.ApplicationUserSettings
            {
                ApplicationUserId = applicationUsers.ApplicationUserId,
            };
            context.ApplicationUserSettings.Add(userSetting);
            context.SaveChanges(createLog: false);


            #endregion

            #region LookUp Codes
            List<Domain.LookUps.LookUpDomains> listLookUpDomainsToBeAdded = new List<Domain.LookUps.LookUpDomains>();
            listLookUpDomainsToBeAdded.Add(new Domain.LookUps.LookUpDomains
            {
                LookUpDomainCode = Enums.LookUps.LookUpDomainCode.QuestionLevels,
                LookUpDomainDescription = "Question Levels",
                FkCreatedBy = FkCreatedBy
            });

            listLookUpDomainsToBeAdded.Add(new Domain.LookUps.LookUpDomains
            {
                LookUpDomainCode = Enums.LookUps.LookUpDomainCode.Technology,
                LookUpDomainDescription = "Technology",
                FkCreatedBy = FkCreatedBy
            });
            foreach (Domain.LookUps.LookUpDomains lookUpDomains in listLookUpDomainsToBeAdded)
            {
                context.LookUpDomains.Add(lookUpDomains);
            }
            context.SaveChanges(createLog: false);
            #endregion

            #region QuestionFieldTypes
            List<Domain.Question.QuestionFieldTypes> lstQuestionFieldTypes = new List<Domain.Question.QuestionFieldTypes>();
            lstQuestionFieldTypes.Add(new Domain.Question.QuestionFieldTypes
            {
                FieldType = Enums.Question.FieldType.RadioButtonList,
                FieldDisplayName = "Radio button list",
                DisplayOrder = 1,
                ErrorMessageRequired = "This is required field",
                RegularExpression = "",
                ErrorMessageRegularExpression = "",
                ValidExtensions = "",
                ErrorExtensions = "",
            });

            lstQuestionFieldTypes.Add(new Domain.Question.QuestionFieldTypes
            {
                FieldType = Enums.Question.FieldType.CheckBoxList,
                FieldDisplayName = "Checkbox list",
                DisplayOrder = 2,
                ErrorMessageRequired = "This is required field",
                RegularExpression = "",
                ErrorMessageRegularExpression = "",
                ValidExtensions = "",
                ErrorExtensions = "",
            });
            foreach (Domain.Question.QuestionFieldTypes qouestionFieldTypes in lstQuestionFieldTypes)
            {
                context.QuestionFieldTypes.Add(qouestionFieldTypes);
            }
            context.SaveChanges(createLog: false);

            #endregion

            base.Seed(context);
        }
    }
}
