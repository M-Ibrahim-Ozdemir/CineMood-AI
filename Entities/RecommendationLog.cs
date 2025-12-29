using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMoodAI.Domain.Entities
{
    public class RecommendationLog
    {
        public int Id { get; set; }
        public string Mood { get; set; }
        public string SuggestedMovieTitle { get; set; }
        public string UserId { get; set; } //alttakinin ıdsı
        public AppUser User { get; set; }   //bu filmi kime tavsiye ettik
        public DateTime CreateAt { get; set; }   //yaratilma tarihi
    }//bunlara navigation properties deniyo tabloların iliskini kurmak icin, appuser e gifipte iliski urcaz Icolkection 1'w cok iliski
}  
