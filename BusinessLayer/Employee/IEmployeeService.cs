using Common.ViewModel.Employee;
using Data_Access_Layer.Model;
using Microsoft.EntityFrameworkCore;
using Models;
using Sieve.Models;

namespace BusinessLayer.Employee
{
    public interface IEmployeeService
    {
        Task<APIResponseModel> SaveEmployee(EmployeeDTO employeeDto);
        Task<APIResponseModel> GetListOfEmployee(PaginationModel paginationModel);
        Task<APIResponseModel> DeleteEmployee(string id);
        Task<APIResponseModel> GetEmployee(string id);
        Task<APIResponseModel> UpdateEmployee(EmployeeUpdateDto employee, string id);
        Task<APIResponseModel> SearchEmployeesByFirstName(string firstName);
        Task<APIResponseModel> GroupBySalary();
    }
}
