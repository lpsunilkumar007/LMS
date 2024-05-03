using System;
using System.Data.Entity;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading.Tasks;

using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace OnlineTestApp.DataAccess.DataLayer
{
    public class OnlineTestAppContext : DbContext
    {
        static OnlineTestAppContext()
        {
            ManageDataLayerDataAccess.SetInitializer();
        }
        public OnlineTestAppContext() : base("DbOnlineTestApp")
        {
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer(new OnlineTestAppInitializer());
        }

        public DbSet<Domain.Company.Companies> Company { get; set; }
        /*Application User Starts*/
        public DbSet<Domain.User.ApplicationUserRoles> ApplicationUserRoles { get; set; }
        public DbSet<Domain.User.ApplicationUsers> ApplicationUsers { get; set; }
        public DbSet<Domain.User.ApplicationUserSettings> ApplicationUserSettings { get; set; }
        /*Application User ends*/

        /*LooksUps Starts*/
        public DbSet<Domain.LookUps.LookUpDomains> LookUpDomains { get; set; }
        public DbSet<Domain.LookUps.LookUpDomainValues> LookUpDomainValues { get; set; }
        /*LooksUps Ends*/

        public DbSet<Domain.Log.DataChangeLog> DataChangeLog { get; set; }
        public DbSet<Domain.Question.QuestionFieldTypes> QuestionFieldTypes { get; set; }
        public DbSet<Domain.Question.QuestionLevel> QuestionLevel { get; set; }
        public DbSet<Domain.Question.QuestionOptions> QuestionOptions { get; set; }
        public DbSet<Domain.Question.Questions> Questions { get; set; }
        public DbSet<Domain.Question.QuestionTechnology> QuestionTechnology { get; set; }

        public DbSet<Domain.Email.EmailTemplates> EmailTemplates { get; set; }


        public DbSet<Domain.Test.TestDetails> TestDetails { get; set; }
        public DbSet<Domain.Test.TestQuestionOptions> TestQuestionOptions { get; set; }
        public DbSet<Domain.Test.TestQuestions> TestQuestions { get; set; }
        public DbSet<Domain.Test.TestTechnology> TestTechnology { get; set; }
        public DbSet<Domain.Test.TestLevel> TestLevel { get; set; }
        public DbSet<Domain.Test.TempTests> TempTests { get; set; }

        public DbSet<Domain.Candidate.Candidates> Candidates { get; set; }
        //public DbSet<Domain.Candidate.CandidateAssignedTest> CandidateAssignedTest { get; set; }

        public DbSet<Domain.Email.EmailLog> EmailLog { get; set; }

       // public DbSet<Domain.Candidate.CandidateTestDetails> CandidateTestDetails { get; set; }
        public DbSet<Domain.Candidate.CandidateTestQuestions> CandidateTestQuestions { get; set; }
        public DbSet<Domain.Candidate.CandidateTestQuestionOptions> CandidateTestQuestionOptions { get; set; }


        /*SampleTest Mockup Starts*/
        public DbSet<Domain.SampleTest.SampleTestMockups> SampleTestMockups { get; set; }
        public DbSet<Domain.SampleTest.SampleTestTechnologies> SampleTestTechnologies { get; set; }
        public DbSet<Domain.SampleTest.SampleTestQuestions> SampleTestQuestions { get; set; }
        /*SampleTest Mockup  End*/



        /*TestPapers Starts*/
        public DbSet<Domain.TestPaper.TestPapers> TestPapers { get; set; }
        public DbSet<Domain.TestPaper.TestPaperQuestions> TestPaperQuestions { get; set; }
        public DbSet<Domain.TestPaper.TestPaperTechnologies> TestPaperTechnologies { get; set; }
        public DbSet<Domain.TestPaper.TestInvitations> TestInvitations { get; set; }
        /*TestPapers  End*/





        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryToExecute"></param>
        /// <returns></returns>
        internal IList<T> Set<T>(IQueryable<T> queryToExecute)
        {
            return queryToExecute.ToList();
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public override int SaveChanges()
        //{
        //    throw new NotImplementedException();
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public override Task<int> SaveChangesAsync()
        //{
        //    throw new NotImplementedException();
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int SaveChanges(bool createLog)
        {
            try
            {
                CreateChangeLog(createLog);
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw new InvalidOperationException(LogDbEntityValidationExceptionError(ex));
            }
            catch (Exception ex)
            {
                Utilities.ErrorLog.LogError(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveChangesAsync(bool createLog)
        {
            try
            {
                CreateChangeLog(createLog);
                return base.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                throw new InvalidOperationException(LogDbEntityValidationExceptionError(ex));
            }
            catch (Exception ex)
            {
                Utilities.ErrorLog.LogError(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        string LogDbEntityValidationExceptionError(DbEntityValidationException exception)
        {
            StringBuilder st = new StringBuilder();
            foreach (var eve in exception.EntityValidationErrors)
            {
                st.Append(string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State));
                foreach (var ve in eve.ValidationErrors)
                {
                    st.Append(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                }
            }
            string message = st.ToString();
            //Utilities.ErrorLog.LogError(e, message, "SystemApiContext - SaveChanges");
            return message;
        }

        #region Change Log

        /// <summary>
        /// 
        /// </summary>
        /// <param name="createLog"></param>
        void CreateChangeLog(bool createLog = true)
        {
            if (!createLog) return;

            var modifiedEntities =
                   ChangeTracker.Entries()
                       .Where(
            t =>
                   t.State == EntityState.Deleted ||
                    t.State == EntityState.Modified);
            //t =>
            //    t.State == EntityState.Added || t.State == EntityState.Deleted ||
            //    t.State == EntityState.Modified);

            foreach (var change in modifiedEntities)
            {
                var originalValues = new Dictionary<string, string>();
                var newValues = new Dictionary<string, string>();
                string primaryKey = GetPrimaryKeyValue(change);
                var state = change.State;
                switch (state)
                {
                    case EntityState.Added:
                        foreach (var propertyName in change.CurrentValues.PropertyNames)
                        {
                            newValues.Add(propertyName, change.CurrentValues.GetValue<object>(propertyName) == null ? "" : change.CurrentValues.GetValue<object>(propertyName).ToString());
                        }
                        break;
                    case EntityState.Modified:
                        bool isDeleted = false;
                        if (change.CurrentValues.PropertyNames.Contains("IsDeleted"))
                        {
                            isDeleted = change.CurrentValues.GetValue<object>("IsDeleted") != null && (bool)change.CurrentValues.GetValue<object>("IsDeleted");
                        }
                        if (isDeleted) { state = EntityState.Deleted; }
                        foreach (var propertyName in change.CurrentValues.PropertyNames)
                        {
                            if (!Equals(change.OriginalValues.GetValue<object>(propertyName), change.CurrentValues.GetValue<object>(propertyName)))
                            {
                                originalValues.Add(propertyName,
                                    change.OriginalValues.GetValue<object>(propertyName) == null
                                        ? ""
                                        : change.OriginalValues.GetValue<object>(propertyName).ToString());
                                newValues.Add(propertyName,
                                    change.CurrentValues.GetValue<object>(propertyName) == null
                                        ? ""
                                        : change.CurrentValues.GetValue<object>(propertyName).ToString());
                            }
                        }
                        break;
                }
                if (newValues.Count > 0 || originalValues.Count > 0)
                {
                    //var modelName = change.Entity.GetType().BaseType.Name == "Object"
                    //    ? change.Entity.GetType().Name
                    //    : change.Entity.GetType().BaseType.Name;
                    var modelName = change.Entity.GetType().Name;

                    var log = new Domain.Log.DataChangeLog
                    {
                        OriginalValue = Utilities.Conversions.DictionaryToJson(originalValues),
                        NewValue = Utilities.Conversions.DictionaryToJson(newValues),
                        RecordId = primaryKey,
                        EventType = state.ToString(),
                        Model = modelName
                    };
                    if (UserVariables.IsAuthenticated)
                    {
                        log.FkCreatedBy = UserVariables.LoggedInUserId;
                    }
                    DataChangeLog.Add(log);
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        string GetPrimaryKeyValue(DbEntityEntry entry)
        {
            try
            {
                var objectStateEntry = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
                if (objectStateEntry.EntityKey.EntityKeyValues != null && objectStateEntry.EntityKey.EntityKeyValues.Count() > 0)
                {
                    return Convert.ToString(objectStateEntry.EntityKey.EntityKeyValues[0].Value);
                }
            }
            catch { }
            return "Add State";
        }
        #endregion
    }
}
