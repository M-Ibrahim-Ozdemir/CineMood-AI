using CineMoodAI.Aplication.DTO;
using CineMoodAI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMoodAI.Aplication.interfaces
{
    public interface IRecommendationService
    {
        Task<RecommendationResponse> GetRecommendationAsync(RecommendationRequest request);
        Task<IEnumerable<RecommendationLog>> GetUserRecommendationHistoryAsync(int userId); //bunu ihtiyac olur diye uydurduk
    }
}
