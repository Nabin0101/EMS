using Business_Layer.EmployeeService;
using Business_Layer.Position;
using Data_Access_Layer.Model;
using Infrastructure.Common.ViewModel.Position;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPosition _positionService;

        public PositionController(IPosition positionService)
        {
            _positionService = positionService;
        }


        [HttpPost("AddPosition")]
        public async  Task<IActionResult>  AddPosition(PositionDTO positionDTO)
        {
            var result = await _positionService.AddPosition(positionDTO);
            if(result.IsSuccess)
            {
            return Ok();
            }
            return BadRequest();
        }

        [HttpGet("GetAllPositions")]
        public async Task<IActionResult> GetAllPositions()
        {
            var result = await _positionService.GetAllPositions();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
