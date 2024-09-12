using Business_Layer.EmployeeJobHistory;
using Data_Access_Layer.ApplicationContext;
using Data_Access_Layer.Model;
using Infrastructure.Common.ViewModel.EmployeeHistory;
using Infrastructure.Common.ViewModel.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeJobHistoriesController : ControllerBase
    {
            private readonly IEmployeeJobHistories _employeeJobHistoriesService;

            public EmployeeJobHistoriesController(IEmployeeJobHistories employeeJobHistoriesService)
            {
                _employeeJobHistoriesService = employeeJobHistoriesService;
            }

            [HttpPost("addEmployeeJob/")]
            public async Task<IActionResult> AddEmployeeJob(AddEmployeeJob addEmployeeJob)
            {
                var addJob = await _employeeJobHistoriesService.AddEmployeeJobs(addEmployeeJob);
                return Ok(addJob);
            }
        [HttpGet("ByEmployeeId")]
            public async Task<IActionResult> GetEmployeeHistoryByEmployeeId(String id)
            {
                var employeeHistory = await _employeeJobHistoriesService.GetEmployeeHistory(id);

                if (employeeHistory == null)
                {
                    return NotFound();
                }

                return Ok(employeeHistory);
            }

            [HttpPost("GetAllHistory")]
            public async Task<IActionResult> GetEmployeeHistory(PaginationModel paginationModel)
            {
                var employeeHistory = await _employeeJobHistoriesService.GetAllEmployeeJobHistory(paginationModel);

                if (employeeHistory.IsSuccess)
                {
                return Ok(employeeHistory);
                }

                    return BadRequest();
            }
        [HttpPut("JobHistoryEdit")]
        public async Task<IActionResult> UpdateJobHistoryDates(String employeeJobHistoryId, UpdateEmployeeJobHistoryDto updateEmployeeJobHistoryDto)
        {
            if (updateEmployeeJobHistoryDto == null)
            {
                return BadRequest("JobHistoryDto is null.");
            }

            await _employeeJobHistoriesService.UpdateEmployeeJobHistory(employeeJobHistoryId, updateEmployeeJobHistoryDto);
            return Ok(updateEmployeeJobHistoryDto);
        }
        }
    }
