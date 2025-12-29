using CineMoodAI.Aplication.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMoodAI.Aplication.interfaces
{
    public interface IAIService
    {
        //not: bu oenkte 1 cevap donecek eger 1den cok prim oneri istersek List <RecommendationResponse yapcaz open AI a 5 3 don dicez
        //bu inerface ydi. Bunun servisini yazcaz AI service
        //Fakat Aı service buraya yazmıyoz INF ra katmanı dısarı ile iliskileri yonetır
        //yapayzekada gelip gidecek sevice INF raya yazxcaz
        Task<RecommendationResponse> GetRecommendationAsync(string mood); //bunu implemente eden servis lerde GetRecommendationAsync diye metot olsun. gerşyetde REcomRespınse donsun
   
    }
}
