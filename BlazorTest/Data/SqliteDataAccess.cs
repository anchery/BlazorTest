using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using System.IO;

namespace BlazorTest.Data
{
    public class SqliteDataAccess
    {
        private readonly IConfiguration _config;

        public SqliteDataAccess(IConfiguration configuration)
        {
            _config = configuration;
        }
        public bool TestDBConnection()
        {
            try
            {
                string connectionStirng = _config.GetConnectionString("Default");
                if (connectionStirng == null)
                {
                    Log.Error("Missing configuration-Default");
                    return false;
                }
                string db = connectionStirng.ToString().Split(";")[0].Replace("Data Source=", "");
                if (!File.Exists(db))
                {
                    Log.Error("Cannot find database-" + db);
                    return false;
                }
                using (IDbConnection connection = new SQLiteConnection(connectionStirng))
                {
                    try
                    {
                        connection.Open();
                        Console.WriteLine(connection.State);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return false;
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Log.Error(e.Message,"DB Connection");
                return false;
                
            }

        }
        public async Task<List<T>> GetData<T, U>(string sql, U parameters)
        {
            string connectionStirng = _config.GetConnectionString("Default");
            using (IDbConnection connection = new SQLiteConnection(connectionStirng))
            {
                try
                {
                    var data = await connection.QueryAsync<T>(sql, parameters);
                    return data.ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
        }
    }
}
