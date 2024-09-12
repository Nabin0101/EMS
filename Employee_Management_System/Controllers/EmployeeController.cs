using Business_Layer.EmployeeService;
using Business_Layer.Users;
using Data_Access_Layer.Model;
using Infrastructure.Common.ViewModel.Employee;
using Infrastructure.Common.ViewModel.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IUserService _userService;


        public EmployeeController(IEmployeeService employeeService, IUserService userService)
        {
            _employeeService = employeeService;
            _userService = userService;
        }

        [HttpPost("SaveEmployeeDetails")]
        public async Task<IActionResult> SaveEmployeeDetails(EmployeeDTO employeeDto)
        {
            var employee = await _employeeService.SaveEmployee(employeeDto);
            if (employee.IsSuccess)
            {
                return Ok(employeeDto);
            }
            return BadRequest(employeeDto);
        }



        [HttpPost("GetEmployeeList")]
       [Authorize]
        public async Task<IActionResult> GetEmployeeList(PaginationModel paginationModel)
        {
            //var employeeList = await _employeeService.GetListOfEmployee();
            //if (employeeList.IsSuccess)
            //{
            //    return Ok(employeeList);
            //}
            //return BadRequest(employeeList);

            var tokenString = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var tokenEntity = _userService.IsTokenValid(tokenString);
            if (tokenEntity == false)
            {
                return Unauthorized("Token is invalid or expired.");
            }
            else
            {
                var employeeList = await _employeeService.GetListOfEmployee(paginationModel);
                return Ok(employeeList);
            }
        }

        [HttpGet("GetEmployee/{id}")]
        public async Task<IActionResult> GetEmployee(String id)
        {
            var employee = await _employeeService.GetEmployee(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }


        [HttpPut("UpdateEmployee/{id}")]
        public async Task<IActionResult> UpdateEmployee(String id, EmployeeUpdateDto employee)
        {
            var existingEmployee = await _employeeService.UpdateEmployee(employee, id);
            if (existingEmployee.IsSuccess)
            {
                return Ok(existingEmployee);
            }
            return BadRequest(existingEmployee);
        }

        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(String id)
        {
            var employee = await _employeeService.DeleteEmployee(id);
            if (employee.IsSuccess)
            {
                return Ok(employee);
            }

            return BadRequest(employee);
        }

        [HttpGet("SearchEmployeeByFirstName")]
        public async Task<IActionResult> SearchEmployeeByFirstName( string firstName)
        {
            var employee = await _employeeService.SearchEmployeesByFirstName(firstName);
            if (employee.IsSuccess)
            {
                return Ok(employee);
            }
            return BadRequest(employee);
        }

        [HttpGet("GroupBySalary")]
        public async Task<IActionResult> GroupBySalary()
        {
            var employee = await _employeeService.GroupBySalary();
            if (employee.IsSuccess)
            {
                return Ok(employee);
            }
            return BadRequest(employee);
        }

    }



}
