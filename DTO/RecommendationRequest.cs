using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMoodAI.Aplication.DTO
{  // bu request . bide response olcak. yani servise gelen ve servisyte giden
    //gelen giden dataları birlestirip bi clas yapıyoz DTP 
    public class RecommendationRequest
    {
        public string UserId { get; set; } //userıd guız olark default olarak tuttugu için string int, log olark lsaydı int yazardık. AppUser.cs dosyasında T deyipde yapabilirdk
        public string Mood { get;set; } = string.Empty;
    }
}
