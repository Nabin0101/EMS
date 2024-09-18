using Common.ViewModel.Position;
using Data_Access_Layer.Model;
using Models;

namespace BusinessLayer.Position
{
    public interface IPosition
    {
        Task<APIResponseModel> AddPosition(PositionDTO positionDTO);
        Task<APIResponseModel> GetAllPositions();
    }
}
