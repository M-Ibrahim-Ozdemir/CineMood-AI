using CineMoodAI.Aplication.DTO;
using CineMoodAI.Aplication.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace CineMoodAI.WebAPI.Controllers
{
    [Route("api/[Controller]")]    //web api oldugınu gostermek, MVC degil. bu api controller dır ve route budur. controller demek recommendation
    [ApiController]
    [Authorize]  //mutlaka sadece login olmus kllnaıcı buraya eirsebilsin demek 

    public class RecommendationController : ControllerBase //bunun api olamsı icin turetteik mvc degil
    {
        private readonly IRecommendationService _recommendationService; //burda recokmendatonservice i carcaz, api de artık bu en üst katman. Presentation katmanının  bi alt katmanı aslında. presentation react aryuzu olcaj

        public RecommendationController(IRecommendationService recommendationService) //api katmanıda recommentationsservice e gitcek  . recomennattoncontrolle a api a donecek api de cagırana response u doncek
        {
            _recommendationService = recommendationService;
        }
        [HttpPost]

        public async Task<IActionResult> GetRecommendation([FromBody] MoodRequest request) //getrecommendation metotuna request gelcek
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var recommendationRequest = new RecommendationRequest
            {
                UserId = userid,
                Mood = request.Mood
            };

            var recommendation = await _recommendationService.GetRecommendationAsync(recommendationRequest); //recommendationrequest yaratcak gitcek recoomandationservisten getrecommendaion metodunu carcak...Recommendation service gidelim sagdan
            return Ok(recommendation);
        }
    }
}
