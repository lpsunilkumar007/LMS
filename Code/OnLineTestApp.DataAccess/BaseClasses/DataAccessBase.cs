using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestApp.DataAccess.BaseClasses
{
    public abstract class DataAccessBase : IDisposable
    {
        protected internal DataLayer.OnlineTestAppContext _DbContext;
        protected internal List<SqlParameter> _SqlParameter;
        public DataAccessBase()
        {
            _DbContext = new DataLayer.OnlineTestAppContext();
        }

        /// <summary>
        /// 
        /// </summary>
        protected List<SqlParameter> GetSqlParameters
        {
            get
            {
                if (_SqlParameter == null)
                {
                    _SqlParameter = new List<SqlParameter>();
                }
                return _SqlParameter;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procNameWithParams"></param>
        /// <returns></returns>
        protected List<T> CallStoreProcToList<T>(string procNameWithParams)
        {
            if (GetSqlParameters != null)
            {
                return _DbContext.Database.SqlQuery<T>(procNameWithParams, GetSqlParameters.ToArray()).ToList();
            }
            return _DbContext.Database.SqlQuery<T>(procNameWithParams).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procNameWithParams"></param>
        /// <returns></returns>
        protected T CallStoreProcToSingleOrDefault<T>(string procNameWithParams)
        {
            if (GetSqlParameters != null)
            {
                return _DbContext.Database.SqlQuery<T>(procNameWithParams, GetSqlParameters.ToArray()).SingleOrDefault();
            }
            return _DbContext.Database.SqlQuery<T>(procNameWithParams).SingleOrDefault();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    if (_DbContext != null)
                    {
                        _DbContext.Dispose();
                        _DbContext = null;
                    }
                }
                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~DataMapperBase() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
