
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CineMoodAI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CineMoodAI.Infrastructure.Data
{
    public class CineMoodAIDbContext : IdentityDbContext<AppUser>  //ıdentitydbcontext ten appuserden turettik
    {
        public CineMoodAIDbContext(DbContextOptions<CineMoodAIDbContext>options) : base(options) { }
       
        public DbSet<Movie>Movies { get; set; } //veritabanı icinde 1 tane movie ve recom tablosu var. buraya tanımlıyoruız veri taanı old icin
        public DbSet<RecommendationLog> RecommendationLogs { get; set; }


    }
}
