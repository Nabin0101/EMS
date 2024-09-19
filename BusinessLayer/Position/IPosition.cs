using Common.ViewModel.Position;
using Models;

namespace BusinessLayer.Position
{
    public interface IPosition
    {
        Task<APIResponseModel> AddPosition(PositionDTO positionDTO);
        Task<APIResponseModel> GetAllPositions();
    }
}
