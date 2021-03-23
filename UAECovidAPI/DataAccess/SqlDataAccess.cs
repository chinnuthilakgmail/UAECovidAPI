using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace UAECovidAPI.DataAccess
{
    public enum ConnectionStrings
    {
        CovidAPIDatabase,
        AspNetDatabase
    }
    public class SqlDataAccess : IDisposable, ISqlDataAccess
    {

        public readonly IConfiguration _config;
        public SqlDataAccess(IConfiguration configuration)
        {
            _config = configuration;
        }
        public string GetSqlConnectionString(string name)
        {
            return _config.GetConnectionString(name);
        }

        public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            string connectionString = GetSqlConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).ToList();

                return rows;
            }
        }

        public void SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            string connectionString = GetSqlConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);


            }
        }

        public T SaveDataWithReturnAndOut<T, U>(string storedProcedure, DynamicParameters parameters, string retParameterName, string connectionStringName, string outParameterName, out U outValue)
        {
            string connectionString = GetSqlConnectionString(connectionStringName);
            outValue = default(U);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Query<int>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

                if (outParameterName != null && !string.IsNullOrEmpty(outParameterName))
                {
                    outValue = parameters.Get<U>(outParameterName);
                }
                return parameters.Get<T>(retParameterName);

            }
        }

        #region Manage Memory

        private bool isDisposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
            {
                // free managed resources
                //managedResource.Dispose();
            }

            // free native resources if there are any.
            //if (nativeResource != IntPtr.Zero)
            //{
            //    Marshal.FreeHGlobal(nativeResource);
            //    nativeResource = IntPtr.Zero;
            //}

            isDisposed = true;
        }
        ~SqlDataAccess()
        {
            Dispose(false);
        }
        #endregion
    }
}
