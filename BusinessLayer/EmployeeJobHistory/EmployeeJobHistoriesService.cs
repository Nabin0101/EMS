using Common.ViewModel.EmployeeHistory;
using DataAccessLayer;
using Entities.Employees;
using Microsoft.EntityFrameworkCore;
using Models;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BusinessLayer.EmployeeJobHistory
{
    public class EmployeeJobHistoriesService : IEmployeeJobHistories
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly APIResponseModel _apiResponse;
        private readonly ISieveProcessor _sieveProcessor;
        public EmployeeJobHistoriesService(ApplicationDBContext dbContext,
                                            APIResponseModel apiResponse,
                                            ISieveProcessor sieveProcessor)
        {
            _dbContext = dbContext;
            _apiResponse = apiResponse;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<APIResponseModel> AddEmployeeJobs(AddEmployeeJob addEmployeeJob)
        {
            try
            {
                var employee = await _dbContext.Employee
                                               .FirstOrDefaultAsync(a => a.Id == addEmployeeJob.EmployeeId);
                if (employee == null)
                {
                    _apiResponse.Message = "Employee with this id is not found ";
                    _apiResponse.IsSuccess = false;
                    return _apiResponse;
                }
                
                var position = await _dbContext.Positions
                               .                FirstOrDefaultAsync(p => p.Id == addEmployeeJob.PositionId);
                if (position == null)
                {
                    _apiResponse.Message = "Position with  this id is not found";
                    _apiResponse.IsSuccess = false;
                    return _apiResponse;
                }

                var utcStartDate = addEmployeeJob.StartDate.ToUniversalTime();
                var utcEndDate = addEmployeeJob.EndDate.ToUniversalTime();

                var employeeJobHistory = new EmployeeJobHistories()
                {
                    EmployeeId = addEmployeeJob.EmployeeId,
                    PositionId = addEmployeeJob.PositionId,
                    StartDate = utcStartDate,
                    EndDate = utcEndDate
                };
                
                _dbContext.EmployeeJobHistories.Add(employeeJobHistory);
                 _dbContext.SaveChanges();

                _apiResponse.Data = employeeJobHistory;
                return _apiResponse;
            }
            catch (Exception ex)
            {
                _apiResponse.Message = ex.ToString();
                _apiResponse.IsSuccess = false;
                return _apiResponse;
            }
        }

        public async Task<APIResponseModel> GetAllEmployeeJobHistory(PaginationModel paginationModel)
        {
            try
            {
                var query = _dbContext.Employee.Include(e => e.People)
                                               .Include(e => e.EmployeeJobHistories)
                                               .ThenInclude(e => e.Position)
                                               .Where(e => !e.IsDeleted)
                                               .AsQueryable()
                                               .AsNoTracking();


                var sieveModel = new SieveModel()
                {
                    PageSize = paginationModel.PageSize,
                    Page = paginationModel.PageNumber,
                    Filters = !string.IsNullOrEmpty(paginationModel.Filter) ? $"StartDate>={paginationModel.Filter}" : null,
                };
                var data = _sieveProcessor.Apply(sieveModel, query, applyPagination: false);


                var result = data.Skip((paginationModel.PageNumber - 1) * paginationModel.PageSize)
                                 .Take(paginationModel.PageSize);

                var response = await result.Select(e => new EmployeeHistoryDto
                {
                    EmployeeId = e.Id,
                    FirstName = e.People.FirstName,
                    MiddleName = e.People.MiddleName,
                    LastName = e.People.LastName,
                    JobHistories = e.EmployeeJobHistories.Select(ejh => new JobHistoryDto
                    {
                        PositionId = ejh.PositionId,
                        PositionName = ejh.Position.PositionName,
                        StartDate = ejh.StartDate,
                        EndDate = ejh.EndDate
                    }).ToList()

                })
                                               .ToListAsync();
                var totalCount = result.Count();

                _apiResponse.Data = new
                {
                    TotalRecords = totalCount,
                    PageNumber = sieveModel.Page,
                    sieveModel.PageSize,
                    Employees = response
                };

                return _apiResponse;
            }
            catch (Exception ex)
            {
                _apiResponse.Message = ex.ToString();
                _apiResponse.IsSuccess = false;
                return _apiResponse;

            }

        }


        public async Task<APIResponseModel> GetEmployeeHistory(string id)
        {
            try
            {
                var employee = await _dbContext.Employee
                                               .Include(e => e.People)
                                               .Include(e => e.EmployeeJobHistories)
                                               .ThenInclude(ejh => ejh.Position)
                                               .Where(e => e.Id == id && !e.IsDeleted)
                                               .Select(e => new EmployeeHistoryDto
                                               {
                                                   EmployeeId = e.Id,
                                                   FirstName = e.People.FirstName,
                                                   MiddleName = e.People.MiddleName,
                                                   LastName = e.People.LastName,
                                                   JobHistories = e.EmployeeJobHistories.Select(ejh => new JobHistoryDto
                                                   {
                                                       PositionId = ejh.PositionId,
                                                       PositionName = ejh.Position.PositionName,
                                                       StartDate = ejh.StartDate,
                                                       EndDate = ejh.EndDate
                                                   }).ToList(),

                                               })
                                               .FirstOrDefaultAsync();

                _apiResponse.Data = employee;
                return _apiResponse;

            }
            catch (Exception ex)
            {
                _apiResponse.Message = ex.ToString();
                _apiResponse.IsSuccess = false;
                return _apiResponse;
            }
        }

        public async Task<APIResponseModel> UpdateEmployeeJobHistory(string id, UpdateEmployeeJobHistoryDto updateEmployeeJobHistoryDto)
        {
            try
            {
                var existingJobHistory = await _dbContext.EmployeeJobHistories
                                                        .FirstOrDefaultAsync(j => j.Id == id);

                if (existingJobHistory == null)
                {
                    _apiResponse.Message = "Employee with this id have no job  history";
                    return _apiResponse;
                }
                else if(existingJobHistory.PositionId==updateEmployeeJobHistoryDto.PositionId)
                {
                    _apiResponse.Message = "Employee already have same position previously";
                    _apiResponse.IsSuccess = false;
                    return _apiResponse;

                }

                existingJobHistory.PositionId = updateEmployeeJobHistoryDto.PositionId;
                existingJobHistory.StartDate = updateEmployeeJobHistoryDto.StartDate.ToUniversalTime();
                existingJobHistory.EndDate = updateEmployeeJobHistoryDto.EndDate.ToUniversalTime();

                _dbContext.EmployeeJobHistories.Update(existingJobHistory);
                _dbContext.SaveChanges();
                _apiResponse.Data = existingJobHistory;
                return _apiResponse;
            }
            catch (Exception ex)
            {
                _apiResponse.Message = ex.ToString();
                _apiResponse.IsSuccess = false;
                return _apiResponse;
            }
        }
    }
}