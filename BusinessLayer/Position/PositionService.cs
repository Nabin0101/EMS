using Common.ViewModel.Position;
using Data_Access_Layer.Model;
using DataAccessLayer;
using Entities.Employee;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Position
{
    public class PositionService : IPosition
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly APIResponseModel _apiResponse;

        public PositionService(ApplicationDBContext dbContext, APIResponseModel apiResponse)
        {
            _dbContext = dbContext;
            _apiResponse = apiResponse;
        }
        public async Task<APIResponseModel> AddPosition(PositionDTO positionDTO)
        {
            try
            {
                var position = new Positions
                {
                    PositionName = positionDTO.PositionName
                };
                await _dbContext.Positions.AddAsync(position);
                await _dbContext.SaveChangesAsync();
                _apiResponse.Data = position;
                return _apiResponse;
            }
            catch (Exception ex)
            {
                _apiResponse.Message = ex.ToString();
                _apiResponse.IsSuccess = false;
                return _apiResponse;
            }

        }
        public async Task<APIResponseModel> GetAllPositions()
        {
            try
            {
                var positions = await _dbContext.Positions
                                                 .Select(p => new
                                                 {
                                                     p.Id,
                                                     p.PositionName
                                                 }).ToListAsync();

                _apiResponse.Data = positions;
                _apiResponse.IsSuccess = true;
                _apiResponse.Message = "Positions retrieved successfully.";

                return _apiResponse;
            }
            catch (Exception ex)
            {
                _apiResponse.Message = $"An error occurred while retrieving positions: {ex.Message}";
                _apiResponse.IsSuccess = false;
                return _apiResponse;
            }
        }
    }
}
