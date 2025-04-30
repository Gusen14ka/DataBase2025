using STO.Service.Requests.Car;
using STO.Service.Responses.Car;

namespace STO.Service.Interfaces;

public interface ICarService
{
    Task<List<ResponseCarDetailed>> GetAllCarsAsync();
    Task<ResponseCarDetailed?> GetCarByIdAsync(int id);
    Task<ResponseCarBrief> AddCarAsync(RequestCarCreate request);
    Task<bool> DeRegisterCarAsync(int id);
    Task<bool> DeRegisterCarAsyncCarsByCustomerIdAsync(int customerId);
    Task<List<ResponseCarWithModelAndBrand>> SearchByVinAndCustomerIdAsync(string vinQuery, int customerId);
    Task<ResponseCarWithModelAndBrand?> GetCarWithModelAndBrandByIdAsync(int carId);
}

