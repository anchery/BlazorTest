using BlazorTest.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OBSCompents.Models;

namespace BlazorTest.Data
{
    public class SqlDataAccess
    {
        private readonly IConfiguration _config;
        public string ConnectionStringName { get; set; } = "SqlServer";

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public string GetData<T,U>(string sql, U parameters)
        {
            string connecitonString = _config.GetConnectionString(ConnectionStringName);
            using (IDbConnection connection = new SqlConnection(connecitonString))
            {
                try
                {
                    var data = connection.Query<string>(sql, parameters);
                    return data.First().ToString();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }
        public async Task<List<T>> LoadData<T, U>(string sql, U parameters)
        {
            string connecitonString = _config.GetConnectionString(ConnectionStringName);
            using (IDbConnection connection = new SqlConnection(connecitonString))
            {
                try
                {
                    var data = await connection.QueryAsync<T>(sql, parameters);
                    return data.ToList();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }
        public int SaveData<T>(string sql, T parameters)
        {
            int rows = 0;
            try
            {
                string connecitonString = _config.GetConnectionString(ConnectionStringName);
                using (IDbConnection connection = new SqlConnection(connecitonString))
                {
                    rows = connection.Execute(sql, parameters);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //Log.Error(e.Message);
                throw new Exception(e.Message);
            }
            return rows;
        }

        public Task<List<ObservationGroupModel>> GetObservationGroups(string filter)
        {
            string sql = string.Empty;
            if (filter != null)
                sql = "Select Distinct GrpId,GrpText From Meta_Observations where obstext like '%" + filter + "%'";
            else
                sql = "Select Distinct GrpId,GrpText From Meta_Observations";

            return LoadData<ObservationGroupModel, dynamic>(sql, new { });
        }
        public Task<List<ObservationModel>> GetObservations(string filter)
        {
            string sql = string.Empty;
            if (filter != null)
                sql = "Select ObsId,ObsText From Meta_Observations where GrpId =" + filter;
            else
                sql = "Select ObsId,ObsText From Meta_Observations";

            return LoadData<ObservationModel, dynamic>(sql, new { });
        }
        public bool ValidateLogin(string username,string password)
        {
            string sql = "Select count(*) From Users where Username='" + username +"' and Password='"+password+"'";
            string result = GetData<dynamic,dynamic>(sql, new { });
            return result == "0" ? false : true;
                
        }
    }
}
