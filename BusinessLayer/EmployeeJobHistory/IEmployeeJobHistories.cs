using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ViewModel.EmployeeHistory;
using Models;

namespace BusinessLayer.EmployeeJobHistory
{
    public interface IEmployeeJobHistories
    {
        Task<APIResponseModel> GetEmployeeHistory(string id);
        Task<APIResponseModel> UpdateEmployeeJobHistory(string id, UpdateEmployeeJobHistoryDto updateEmployeeJobHistoryDto);
        Task<APIResponseModel> GetAllEmployeeJobHistory(PaginationModel paginationModel);
        Task<APIResponseModel> AddEmployeeJobs(AddEmployeeJob addEmployeeJob);
    }
}
