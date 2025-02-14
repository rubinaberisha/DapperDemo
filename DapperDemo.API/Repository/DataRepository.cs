using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperDemo.API.Models;

namespace DapperDemo.API.Repository
{
    public class DataRepository : IDataRepository
    {
        private readonly string _connectionString;

        public DataRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task SaveWeatherData(WeatherData weatherData)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                INSERT INTO Weather (City, Description, Temperature, Humidity, WindSpeed)
                VALUES (@City, @Description, @Temperature, @Humidity, @WindSpeed)";

                await connection.ExecuteAsync(query, weatherData);
            }
        }
    }
}
