using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMoodAI.Domain.Entities
{
    public class AppUser: IdentityUser  //appuserin ıdentity tarafından kullanilabilmesi icin
    {
        public string? DisplayName { get; set; } //bunun bosta olabileceğini soledik
        public ICollection<RecommendationLog> Recommendations { get; set;} = new List<RecommendationLog>();//yani kullanıcının 1 den cok recomandetionsLogu var ama her recLogunun 1 tane kullanıcısı var yani her kullanıcya ozel film kayıt tutcaz

    }
}
