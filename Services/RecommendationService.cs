using CineMoodAI.Aplication.DTO;
using CineMoodAI.Aplication.interfaces;
using CineMoodAI.Domain.Entities;
using CineMoodAI.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMoodAI.Aplication.Services
{
    public class RecommendationService : IRecommendationService   //ctrl implmente dedik kod otomatik geddli. tabı extra ekledik
    {
        private readonly IAIService _aiService; //ai inerfacesi  alttakide service i
        private readonly IRepository<RecommendationLog> _LogRepo; //IRepository iterfacesi recommendationlog tipinde prvate oldugu icin _logrepo (jargon = _ ).  Bu recommendationlogunun repositorysi. Biz zaten gidip özel bi repository yazmadık (zaten generic olan seyleri repository olarak kullanmak icin).  recommenMovie dersen  movie olur . oyzuden generic diyoz buna
      
        //contructor yazak, sonra ustte aı service
        public RecommendationService(IAIService aiService, IRepository<RecommendationLog> LogRepo)  // RecommendationService bu servis cagirilirken bize aiService i vercek ve Repository vercek. bizde alcaz altta logrepo yu kullnacuk. Biz bu repository yi napcaz  GetRecommendationAsync cagırldiginda devamı orada :d
        {
            _aiService = aiService;
            _LogRepo = LogRepo;
        }
        
        
        
        
        public async Task<RecommendationResponse> GetRecommendationAsync(RecommendationRequest request)  // Biz bu repository yi napcaz GetRecommendationAsync cagırldiginda, web api cagrcak. web api bu servicesi carcak srsviste gitcek repositoryyi carcak
        {
            var result = await _aiService.GetRecommendationAsync(request.Mood);  //burda bi ai service olcak. ai service icinden bi metot carcaz. yani bu recommandation service ai service i carcak. aiservicenden gitcek open ai dan datayı alcak result diye
//recomantatio  controller decamı. buraya cardı cunku.  Bu da gidecek ai dan recommentadonları alcak


            var log = new RecommendationLog()  //sonra recommndationlog objesi yarattık. gelen request moodu falan open ai dan gelenler 
            {                             // bu logu gidip vertabanına kaydetmek icin yarattık
                Mood = request.Mood,
                SuggestedMovieTitle = result.Title,
                UserId = request.UserId,
                CreateAt = DateTime.UtcNow
            };
//repository kullnarak veritabanına kaydedicek       
            

            await _LogRepo.AddAsync(log);          //sordugum cevapları log kutucu icinden addasync carcaz log u vercez bekle dicez sonra kaydet dicez 
            await _LogRepo.SaveChangesAsync();     //ekledik ve verştabanına kaydetteik

            return new RecommendationResponse //sonrada bir response donmesi lazım. Web api ye bir ceap doncek servis amacımız bu
            {
                Title = result.Title, //ai den gelen title e atadık 
                Genre = result.Genre,
                Description = result.Description
            };
            //ve response doncek api ye. ve bunu cagıran sonra tekrar recommandationcontrollere git -> recommandationcontrollere api a yına return olark cevabı arayuze doncek



            //history icidne yapılabilir

        }

        public Task<IEnumerable<RecommendationLog>> GetUserRecommendationHistoryAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
