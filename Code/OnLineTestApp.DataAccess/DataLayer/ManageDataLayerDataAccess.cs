using System;
using System.Data.Entity;

namespace OnlineTestApp.DataAccess.DataLayer
{
    internal class ManageDataLayerDataAccess
    {
        public static void SetInitializer()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<OnlineTestAppContext>());

            //forcing to create database 
            using (OnlineTestAppContext obj = new OnlineTestAppContext())
            {
                try
                {
                    obj.Database.Initialize(true);
                }
                catch (Exception ex)
                {
                    Utilities.ErrorLog.LogError(ex, "", "ManageDataLayerDataAccess : SetInitializer");
                }
            }
        }
    }
}
