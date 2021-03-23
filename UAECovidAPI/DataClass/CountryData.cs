using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAECovidAPI.DataAccess;
using UAECovidAPI.Models;

namespace UAECovidAPI.DataClass
{
    public class CountryData : ICountryData
    {
        
        private readonly ISqlDataAccess _sql;

        public CountryData( ISqlDataAccess sql)
        { 
            _sql = sql;
        }
        public CountryClass GetCountry(int id)
        {
            var parameter = new { Id  = id};
            CountryClass model = _sql.LoadData<CountryClass, dynamic>("GetCountryById", parameter, ConnectionStrings.CovidAPIDatabase.ToString()).FirstOrDefault();

            return model;
        }
        public List<CountryClass> GetAllCountries()
        {
            var parameter = new { };
            List<CountryClass> models = _sql.LoadData<CountryClass, dynamic>("uspGetAllCountries", parameter, ConnectionStrings.CovidAPIDatabase.ToString());

            return models;
        }
        public int AddCountry(CountryClass model)
        {
           
            Dapper.DynamicParameters parameter = new Dapper.DynamicParameters();
            //@Country nvarchar(500),
            parameter.Add("Country", model.Country);
            //@Slug nvarchar(500),
            parameter.Add("Slug", model.Slug);
            //@Code nchar(10),
            parameter.Add("Code", model.Code);
            //@CountryId INT OUTPUT           
            parameter.Add("CountryId", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            parameter.Add("p", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);

            int outValue;
            int retValue = _sql.SaveDataWithReturnAndOut<int, int>("uspAddCountry", parameter, "p", ConnectionStrings.CovidAPIDatabase.ToString(), "CountryId", out outValue);
            model.Id = outValue;
            return retValue;
        }

        public int UpdateCountry(CountryClass model)
        {
            
            Dapper.DynamicParameters parameter = new Dapper.DynamicParameters();
            //@Id int,
            parameter.Add("Id", model.Id);
            //@Country nvarchar(500),
            parameter.Add("Country", model.Country);
            //@Slug nvarchar(500),
            parameter.Add("Slug", model.Slug);
            //@Code nchar(10),
            parameter.Add("Code", model.Code);

            parameter.Add("p", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);

            int outValue;
            int retValue = _sql.SaveDataWithReturnAndOut<int, int>("uspUpdateCountry", parameter, "p", ConnectionStrings.CovidAPIDatabase.ToString(), "", out outValue);

            return retValue;
        }

        public int DeleteCountry(int code)
        {
            
            Dapper.DynamicParameters parameter = new Dapper.DynamicParameters();
            //@Id int
            parameter.Add("Id", code);

            parameter.Add("p", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);

            int outValue;
            int retValue = _sql.SaveDataWithReturnAndOut<int, int>("uspDeleteCountry", parameter, "p", ConnectionStrings.CovidAPIDatabase.ToString(), "", out outValue);

            return retValue;
        }
    }
}
