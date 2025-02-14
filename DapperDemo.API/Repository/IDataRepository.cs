using DapperDemo.API.Models;

namespace DapperDemo.API.Repository
{
    public interface IDataRepository
    {
        Task SaveWeatherData(WeatherData weatherData);
    }
}