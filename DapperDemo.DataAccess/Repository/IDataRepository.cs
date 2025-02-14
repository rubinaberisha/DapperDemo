using DapperDemo.DataAccess.Models;

namespace DapperDemo.DataAccess.Repository
{
    public interface IDataRepository
    {
        Task SaveWeatherData(WeatherData weatherData);
        Task SaveRootData(Root weatherData);
    }
}