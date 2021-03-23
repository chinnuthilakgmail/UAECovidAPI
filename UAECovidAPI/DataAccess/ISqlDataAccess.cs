using Dapper;
using System.Collections.Generic;

namespace UAECovidAPI.DataAccess
{
    public interface ISqlDataAccess
    {
        void Dispose();
        string GetSqlConnectionString(string name);
        List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName);
        void SaveData<T>(string storedProcedure, T parameters, string connectionStringName);
        T SaveDataWithReturnAndOut<T, U>(string storedProcedure, DynamicParameters parameters, string retParameterName, string connectionStringName, string outParameterName, out U outValue);
    }
}